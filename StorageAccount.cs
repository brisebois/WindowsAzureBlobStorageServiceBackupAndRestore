using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace UI
{
    public class StorageAccount
    {
        private readonly List<CloudBlobContainer> containers = new List<CloudBlobContainer>();
        private readonly List<CloudBlockBlob> snapshots = new List<CloudBlockBlob>();
        private readonly List<string> backups = new List<string>();

        private CloudStorageAccount account;
        private CloudBlobClient client;

        private CloudBlobContainer selectedContainer;

        public List<CloudBlobContainer> Containers
        {
            get { return containers; }
        }

        public List<string> Backups
        {
            get { return backups; }
        }

        public List<CloudBlockBlob> Snapshots
        {
            get { return snapshots; }
        }

        public void Load(string connectionString, Action<StorageAccount> onLoaded)
        {
            account = CloudStorageAccount.Parse(connectionString);
            client = account.CreateCloudBlobClient();

            Containers.Clear();

            Task.Run(() => client.ListContainers())
                .ContinueWith(r =>
                                  {
                                      if (!r.IsCompleted) return;

                                      Containers.AddRange(r.Result);
                                      onLoaded(this);
                                  });
        }

        public void SelectContainers(CloudBlobContainer container,
                                     Action<StorageAccount> onCompleted)
        {
            selectedContainer = container;

            FindBackups(onCompleted);
        }

        private void FindBackups(Action<StorageAccount> onCompleted)
        {
            Task.Run(() =>
                         {
                             var details = BlobListingDetails.Snapshots | BlobListingDetails.Metadata;
                             var list = selectedContainer.ListBlobs(useFlatBlobListing: true,
                                                                    blobListingDetails: details)
                                                         .OfType<CloudBlockBlob>()
                                                         .Where(b => b.SnapshotTime.HasValue)
                                                         .ToList();
                             Snapshots.Clear();
                             Snapshots.AddRange(list);
                             IdentifyBackups();
                         }).ContinueWith(task =>
                                             {
                                                 if (!task.IsCompleted) return;
                                                 onCompleted(this);
                                             });
        }

        private void IdentifyBackups()
        {
            Backups.Clear();
            Backups.AddRange(
                Snapshots.Select(b => b.Metadata["Name"])
                         .Distinct());
        }

        public void DeleteBackup(string backup, 
                                 Action<int> reportCompleted, 
                                 Action<StorageAccount> onCompleted)
        {
            Task.Run(() =>
                         {
                             var snapshotsToDelete =
                                 snapshots.Where(s => s.Metadata["Name"] == backup)
                                          .ToList();

                             var count = snapshotsToDelete.Count;
                             for (var index = 0; index < count; index++)
                             {
                                 snapshotsToDelete[index].DeleteIfExists();
                                 reportCompleted(Convert.ToInt32(((1d + index) / count) * 100));
                             }
                             FindBackups(onCompleted);
                         });
        }

        public void RestoreBackup(string backup, Action<int> reportCompleted)
        {
            Task.Run(() =>
                         {
                             var snapshotsToRestore = snapshots.Where(s => s.Metadata["Name"] == backup)
                                                               .ToList();

                             var count = snapshotsToRestore.Count;
                             for (var index = 0; index < count; index++)
                             {
                                 var backupBlob = snapshotsToRestore[index];

                                 var blob = selectedContainer.GetBlockBlobReference(backupBlob.Name);
                                 blob.StartCopyFromBlob(backupBlob);

                                 reportCompleted(Convert.ToInt32(((1d + index) / count) * 100));
                             }
                         });
        }

        public void CreateBackup(string backupName,
                                 Action<int> reportCompleted,
                                 Action<StorageAccount> onComplete)
        {
            Task.Run(() =>
                         {
                             var dateString = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
                             var dictionary = new Dictionary<string, string>
                                                  {
                                                      {"Name", backupName},
                                                      {"DateTime", dateString}
                                                  };

                             var list = selectedContainer.ListBlobs(useFlatBlobListing: true).ToList();

                             var count = list.Count;
                             for (var index = 0; index < count; index++)
                             {
                                 var backupBlob = list[index];

                                 var blockBlob = backupBlob as CloudBlockBlob;
                                 if (blockBlob == null)
                                     continue;
                                 blockBlob.CreateSnapshot(dictionary);

                                 reportCompleted(Convert.ToInt32(((1d + index) / count) * 100));
                             }
                             onComplete(this);
                         });
        }
    }
}