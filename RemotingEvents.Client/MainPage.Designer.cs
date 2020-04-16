namespace RemotingEvents.Client
{
    partial class MainPage
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelUserNickname = new System.Windows.Forms.Label();
            this.labelUserFullName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TempSimulateNewChatRequest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chatRequestsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.activeUsersFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonLogOut = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
            this.panel1.Controls.Add(this.TempSimulateNewChatRequest);
            this.panel1.Controls.Add(this.labelUserNickname);
            this.panel1.Controls.Add(this.labelUserFullName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.labelUsername);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Location = new System.Drawing.Point(12, 638);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1460, 108);
            this.panel1.TabIndex = 0;
            // 
            // labelUserNickname
            // 
            this.labelUserNickname.AutoSize = true;
            this.labelUserNickname.Location = new System.Drawing.Point(645, 42);
            this.labelUserNickname.Name = "labelUserNickname";
            this.labelUserNickname.Size = new System.Drawing.Size(104, 25);
            this.labelUserNickname.TabIndex = 3;
            this.labelUserNickname.Text = "nickname";
            // 
            // labelUserFullName
            // 
            this.labelUserFullName.AutoSize = true;
            this.labelUserFullName.Location = new System.Drawing.Point(197, 42);
            this.labelUserFullName.Name = "labelUserFullName";
            this.labelUserFullName.Size = new System.Drawing.Size(93, 25);
            this.labelUserFullName.TabIndex = 2;
            this.labelUserFullName.Text = "fullname";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(43, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Logged in as : ";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUsername.Location = new System.Drawing.Point(517, 42);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(132, 25);
            this.labelUsername.TabIndex = 0;
            this.labelUsername.Text = "Username :";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonLogOut);
            this.splitContainer1.Panel1.Controls.Add(this.activeUsersFlowLayoutPanel);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chatRequestsFlowLayoutPanel);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(1460, 620);
            this.splitContainer1.SplitterDistance = 706;
            this.splitContainer1.TabIndex = 1;
            // 
            // TempSimulateNewChatRequest
            // 
            this.TempSimulateNewChatRequest.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TempSimulateNewChatRequest.Location = new System.Drawing.Point(1057, 25);
            this.TempSimulateNewChatRequest.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TempSimulateNewChatRequest.Name = "TempSimulateNewChatRequest";
            this.TempSimulateNewChatRequest.Size = new System.Drawing.Size(382, 58);
            this.TempSimulateNewChatRequest.TabIndex = 1;
            this.TempSimulateNewChatRequest.Text = "[Temp] Simulate New Chat Request";
            this.TempSimulateNewChatRequest.UseVisualStyleBackColor = true;
            this.TempSimulateNewChatRequest.Click += new System.EventHandler(this.TempSimulateNewChatRequest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(221, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Active Users";
            // 
            // chatRequestsFlowLayoutPanel
            // 
            this.chatRequestsFlowLayoutPanel.AutoScroll = true;
            this.chatRequestsFlowLayoutPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chatRequestsFlowLayoutPanel.Location = new System.Drawing.Point(5, 80);
            this.chatRequestsFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chatRequestsFlowLayoutPanel.Name = "chatRequestsFlowLayoutPanel";
            this.chatRequestsFlowLayoutPanel.Size = new System.Drawing.Size(740, 536);
            this.chatRequestsFlowLayoutPanel.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(212, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(353, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pending Chat Requests";
            // 
            // activeUsersFlowLayoutPanel
            // 
            this.activeUsersFlowLayoutPanel.AutoScroll = true;
            this.activeUsersFlowLayoutPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.activeUsersFlowLayoutPanel.Location = new System.Drawing.Point(3, 80);
            this.activeUsersFlowLayoutPanel.Name = "activeUsersFlowLayoutPanel";
            this.activeUsersFlowLayoutPanel.Size = new System.Drawing.Size(700, 536);
            this.activeUsersFlowLayoutPanel.TabIndex = 1;
            // 
            // buttonLogOut
            // 
            this.buttonLogOut.Location = new System.Drawing.Point(15, 13);
            this.buttonLogOut.Name = "buttonLogOut";
            this.buttonLogOut.Size = new System.Drawing.Size(140, 54);
            this.buttonLogOut.TabIndex = 3;
            this.buttonLogOut.Text = "Logout";
            this.buttonLogOut.UseVisualStyleBackColor = true;
            this.buttonLogOut.Click += new System.EventHandler(this.buttonLogOut_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 758);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainPage";
            this.Text = "Chat Application";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelUserNickname;
        private System.Windows.Forms.Label labelUserFullName;
        private System.Windows.Forms.Button TempSimulateNewChatRequest;
        private System.Windows.Forms.FlowLayoutPanel chatRequestsFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel activeUsersFlowLayoutPanel;
        private System.Windows.Forms.Button buttonLogOut;
    }
}