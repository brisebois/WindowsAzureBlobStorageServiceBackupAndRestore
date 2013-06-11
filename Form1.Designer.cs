namespace UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.connectionStringSource = new System.Windows.Forms.TextBox();
            this.listBoxContainers = new System.Windows.Forms.ListBox();
            this.listBoxNamedSnapshots = new System.Windows.Forms.ListBox();
            this.numberOfSnapshots = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.deleteSelectedBackup = new System.Windows.Forms.Button();
            this.restoreSelectedBackup = new System.Windows.Forms.Button();
            this.newBackupName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.operationSummary = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.purgeYear = new System.Windows.Forms.TextBox();
            this.purgeMonth = new System.Windows.Forms.TextBox();
            this.purgeDay = new System.Windows.Forms.TextBox();
            this.purgeHour = new System.Windows.Forms.TextBox();
            this.purgeMinute = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // connectionStringSource
            // 
            this.connectionStringSource.Location = new System.Drawing.Point(94, 12);
            this.connectionStringSource.Multiline = true;
            this.connectionStringSource.Name = "connectionStringSource";
            this.connectionStringSource.Size = new System.Drawing.Size(843, 20);
            this.connectionStringSource.TabIndex = 1;
            this.connectionStringSource.Text = "DefaultEndpointsProtocol=https;AccountName={ACCOUNT NAME};AccountKey={ACCOUNT KEY" +
    "}";
            // 
            // listBoxContainers
            // 
            this.listBoxContainers.FormattingEnabled = true;
            this.listBoxContainers.Location = new System.Drawing.Point(12, 67);
            this.listBoxContainers.Name = "listBoxContainers";
            this.listBoxContainers.Size = new System.Drawing.Size(222, 433);
            this.listBoxContainers.TabIndex = 2;
            this.listBoxContainers.SelectedValueChanged += new System.EventHandler(this.listBoxContainers_SelectedValueChanged);
            // 
            // listBoxNamedSnapshots
            // 
            this.listBoxNamedSnapshots.FormattingEnabled = true;
            this.listBoxNamedSnapshots.Location = new System.Drawing.Point(274, 181);
            this.listBoxNamedSnapshots.Name = "listBoxNamedSnapshots";
            this.listBoxNamedSnapshots.Size = new System.Drawing.Size(311, 277);
            this.listBoxNamedSnapshots.TabIndex = 3;
            this.listBoxNamedSnapshots.SelectedIndexChanged += new System.EventHandler(this.listBoxNamedSnapshots_SelectedIndexChanged);
            // 
            // numberOfSnapshots
            // 
            this.numberOfSnapshots.AutoSize = true;
            this.numberOfSnapshots.Location = new System.Drawing.Point(274, 165);
            this.numberOfSnapshots.Name = "numberOfSnapshots";
            this.numberOfSnapshots.Size = new System.Drawing.Size(87, 13);
            this.numberOfSnapshots.TabIndex = 4;
            this.numberOfSnapshots.Text = "0 backups found";
            this.numberOfSnapshots.Click += new System.EventHandler(this.numberOfSnapshots_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 512);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(925, 10);
            this.progressBar.TabIndex = 5;
            // 
            // deleteSelectedBackup
            // 
            this.deleteSelectedBackup.Location = new System.Drawing.Point(452, 464);
            this.deleteSelectedBackup.Name = "deleteSelectedBackup";
            this.deleteSelectedBackup.Size = new System.Drawing.Size(133, 37);
            this.deleteSelectedBackup.TabIndex = 6;
            this.deleteSelectedBackup.Text = "Delete";
            this.deleteSelectedBackup.UseVisualStyleBackColor = true;
            this.deleteSelectedBackup.Click += new System.EventHandler(this.button2_Click);
            // 
            // restoreSelectedBackup
            // 
            this.restoreSelectedBackup.Location = new System.Drawing.Point(274, 464);
            this.restoreSelectedBackup.Name = "restoreSelectedBackup";
            this.restoreSelectedBackup.Size = new System.Drawing.Size(172, 36);
            this.restoreSelectedBackup.TabIndex = 7;
            this.restoreSelectedBackup.Text = "Restore";
            this.restoreSelectedBackup.UseVisualStyleBackColor = true;
            this.restoreSelectedBackup.Click += new System.EventHandler(this.button3_Click);
            // 
            // newBackupName
            // 
            this.newBackupName.Location = new System.Drawing.Point(315, 67);
            this.newBackupName.Name = "newBackupName";
            this.newBackupName.Size = new System.Drawing.Size(270, 20);
            this.newBackupName.TabIndex = 8;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(315, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(270, 37);
            this.button2.TabIndex = 9;
            this.button2.Text = "Create Backup";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Name";
            // 
            // operationSummary
            // 
            this.operationSummary.FormattingEnabled = true;
            this.operationSummary.Location = new System.Drawing.Point(592, 67);
            this.operationSummary.Name = "operationSummary";
            this.operationSummary.Size = new System.Drawing.Size(345, 433);
            this.operationSummary.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkRed;
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(12, 531);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(274, 36);
            this.button3.TabIndex = 12;
            this.button3.Text = "Purge Selected Container";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // purgeYear
            // 
            this.purgeYear.Location = new System.Drawing.Point(400, 540);
            this.purgeYear.Name = "purgeYear";
            this.purgeYear.Size = new System.Drawing.Size(35, 20);
            this.purgeYear.TabIndex = 13;
            this.purgeYear.Text = "2013";
            // 
            // purgeMonth
            // 
            this.purgeMonth.Location = new System.Drawing.Point(441, 540);
            this.purgeMonth.Name = "purgeMonth";
            this.purgeMonth.Size = new System.Drawing.Size(22, 20);
            this.purgeMonth.TabIndex = 14;
            this.purgeMonth.Text = "6";
            // 
            // purgeDay
            // 
            this.purgeDay.Location = new System.Drawing.Point(469, 540);
            this.purgeDay.Name = "purgeDay";
            this.purgeDay.Size = new System.Drawing.Size(22, 20);
            this.purgeDay.TabIndex = 15;
            this.purgeDay.Text = "10";
            // 
            // purgeHour
            // 
            this.purgeHour.Location = new System.Drawing.Point(516, 540);
            this.purgeHour.Name = "purgeHour";
            this.purgeHour.Size = new System.Drawing.Size(22, 20);
            this.purgeHour.TabIndex = 16;
            this.purgeHour.Text = "17";
            // 
            // purgeMinute
            // 
            this.purgeMinute.Location = new System.Drawing.Point(544, 540);
            this.purgeMinute.Name = "purgeMinute";
            this.purgeMinute.Size = new System.Drawing.Size(22, 20);
            this.purgeMinute.TabIndex = 17;
            this.purgeMinute.Text = "55";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 543);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Older Than (UTC)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(572, 543);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "* if all empty then purge all";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(240, 67);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(28, 433);
            this.button4.TabIndex = 20;
            this.button4.Text = ">>";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 579);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.purgeMinute);
            this.Controls.Add(this.purgeHour);
            this.Controls.Add(this.purgeDay);
            this.Controls.Add(this.purgeMonth);
            this.Controls.Add(this.purgeYear);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.operationSummary);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.newBackupName);
            this.Controls.Add(this.restoreSelectedBackup);
            this.Controls.Add(this.deleteSelectedBackup);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.numberOfSnapshots);
            this.Controls.Add(this.listBoxNamedSnapshots);
            this.Controls.Add(this.listBoxContainers);
            this.Controls.Add(this.connectionStringSource);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Alexandre Brisebois – Backup & Restore Storage Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox connectionStringSource;
        private System.Windows.Forms.ListBox listBoxContainers;
        private System.Windows.Forms.ListBox listBoxNamedSnapshots;
        private System.Windows.Forms.Label numberOfSnapshots;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button deleteSelectedBackup;
        private System.Windows.Forms.Button restoreSelectedBackup;
        private System.Windows.Forms.TextBox newBackupName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox operationSummary;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox purgeYear;
        private System.Windows.Forms.TextBox purgeMonth;
        private System.Windows.Forms.TextBox purgeDay;
        private System.Windows.Forms.TextBox purgeHour;
        private System.Windows.Forms.TextBox purgeMinute;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
    }
}

