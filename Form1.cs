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

            purgeYear.Text= DateTime.UtcNow.Year.ToString();

            purgeMonth.Text = DateTime.UtcNow.Month.ToString();
            purgeDay.Text = DateTime.UtcNow.Day.ToString();
            purgeHour.Text= DateTime.UtcNow.Hour.ToString();
            purgeMinute.Text = DateTime.UtcNow.Minute.ToString();

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
            account.SelectContainers(container, storageAccount => { });
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
                             if (i > 0)
                             {
                                 progressBar.Style = ProgressBarStyle.Blocks;
                                 progressBar.Value = i;
                             }
                             else
                             {
                                 progressBar.Style = ProgressBarStyle.Marquee;
                             }

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

        private void button3_Click_1(object sender, EventArgs e)
        {
            operationSummary.Items.Add(string.Format("Started to Purge container: {0}", ((CloudBlobContainer)listBoxContainers.SelectedValue).Name));
            DateTime? olderThan = null;

            if (!string.IsNullOrWhiteSpace(purgeYear.Text) && !string.IsNullOrWhiteSpace(purgeMonth.Text) && !string.IsNullOrWhiteSpace(purgeDay.Text) && !string.IsNullOrWhiteSpace(purgeHour.Text) && !string.IsNullOrWhiteSpace(purgeMinute.Text))
                olderThan = new DateTime(Convert.ToInt32(purgeYear.Text), Convert.ToInt32(purgeMonth.Text), Convert.ToInt32(purgeDay.Text), Convert.ToInt32(purgeHour.Text), Convert.ToInt32(purgeMinute.Text), 0, DateTimeKind.Utc);
            account.Purge(olderThan, UpdateProgress, storageAccount => Dispatch(() =>
                                                                         {
                                                                             progressBar.Style = ProgressBarStyle.Blocks;
                                                                             progressBar.Value = 0;
                                                                             operationSummary.Items.Add(string.Format("Completed Purge for container: {0}", ((CloudBlobContainer)listBoxContainers.SelectedValue).Name));
                                                                         }));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var container = (CloudBlobContainer)listBoxContainers.SelectedValue;
            operationSummary.Items.Add(string.Format("Finding Backups for: {0}", container.Name));

            account.FindBackups(DisplayBackups());
        }
    }
}