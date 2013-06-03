using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.Storage.Blob;

namespace UI
{
    public partial class Form1 : Form
    {
        private readonly StorageAccount account;
        private readonly TaskScheduler scheduler;

        public Form1()
        {
            InitializeComponent();
            scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            account = new StorageAccount();
            DisplayNumberOfSnapshots(0);

            restoreSelectedBackup.Enabled = (listBoxNamedSnapshots.SelectedIndex != -1 || !string.IsNullOrWhiteSpace((string)listBoxNamedSnapshots.SelectedValue));
            deleteSelectedBackup.Enabled = (listBoxNamedSnapshots.SelectedIndex != -1 || !string.IsNullOrWhiteSpace((string)listBoxNamedSnapshots.SelectedValue));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartProcess();
            operationSummary.Items.Add(string.Format("Finding available containers"));
            account.Load(connectionStringSource.Text, storageAccount => UpdateContainerList());
        }

        private void StartProcess()
        {
            Dispatch(() => { progressBar.Style = ProgressBarStyle.Marquee; });
        }

        private void UpdateContainerList()
        {
            Dispatch(() =>
                         {
                             listBoxContainers.DisplayMember = "Name";
                             listBoxContainers.DataSource = account.Containers;
                             progressBar.Style = ProgressBarStyle.Blocks;
                         });
        }

        private void Dispatch(Action action)
        {
            Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, scheduler);
        }

        private void listBoxContainers_SelectedValueChanged(object sender, EventArgs e)
        {
            var list = (ListBox)sender;
            var container = (CloudBlobContainer)list.SelectedValue;
            StartProcess();
            DisplayNumberOfSnapshots(0);
            operationSummary.Items.Add(string.Format("Finding Backups for: {0}", container.Name));
            account.SelectContainers(container, DisplayBackups());
        }

        private Action<StorageAccount> DisplayBackups()
        {
            return storageAccount => Dispatch(() =>
                    {
                        numberOfSnapshots.Text = DisplayNumberOfSnapshots(account.Backups.Count);
                        listBoxNamedSnapshots.DisplayMember = "Text";
                        listBoxNamedSnapshots.ValueMember = "Text";
                        listBoxNamedSnapshots.DataSource = account.Backups.Select(b => new { Text = b }).ToList();
                        progressBar.Style = ProgressBarStyle.Blocks;
                        progressBar.Value = 0;
                    });
        }

        private string DisplayNumberOfSnapshots(int count)
        {
            return string.Format("{0} backups found", count);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            operationSummary.Items.Add(string.Format("Deleting backup {0} for container: {1}", listBoxNamedSnapshots.SelectedValue, ((CloudBlobContainer)listBoxContainers.SelectedValue).Name));
            account.DeleteBackup((string)listBoxNamedSnapshots.SelectedValue, UpdateProgress, DisplayBackups());
        }

        private void UpdateProgress(int i)
        {
            Dispatch(() =>
                         {
                             progressBar.Style = ProgressBarStyle.Blocks;
                             progressBar.Value = i;
                         });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            operationSummary.Items.Add(string.Format("Restoring backup {0} for container: {1}", listBoxNamedSnapshots.SelectedValue, ((CloudBlobContainer)listBoxContainers.SelectedValue).Name));
            account.RestoreBackup((string)listBoxNamedSnapshots.SelectedValue, UpdateProgress);
        }

        private void listBoxNamedSnapshots_SelectedIndexChanged(object sender, EventArgs e)
        {
            restoreSelectedBackup.Enabled = (listBoxNamedSnapshots.SelectedIndex != -1 ||
            !string.IsNullOrWhiteSpace((string)listBoxNamedSnapshots.SelectedValue));
            deleteSelectedBackup.Enabled = (listBoxNamedSnapshots.SelectedIndex != -1 ||
            !string.IsNullOrWhiteSpace((string)listBoxNamedSnapshots.SelectedValue));
        }

        private void numberOfSnapshots_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            operationSummary.Items.Add(string.Format("Creating backup {0} for container: {1}", newBackupName.Text, ((CloudBlobContainer)listBoxContainers.SelectedValue).Name));
            account.CreateBackup(newBackupName.Text, UpdateProgress, DisplayBackups());
            newBackupName.Text = string.Empty;
        }
    }
}