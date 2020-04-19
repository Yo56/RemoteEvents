namespace RemotingEvents.Client
{
    partial class ChatPage
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
            this.MessageToSend = new System.Windows.Forms.RichTextBox();
            this.CreateMessage = new System.Windows.Forms.Panel();
            this.Send = new System.Windows.Forms.Button();
            this.Messages = new System.Windows.Forms.FlowLayoutPanel();
            this.UserChatIdentification = new System.Windows.Forms.Panel();
            this.RealName = new System.Windows.Forms.Label();
            this.Nickname = new System.Windows.Forms.Label();
            this.CreateMessage.SuspendLayout();
            this.UserChatIdentification.SuspendLayout();
            this.SuspendLayout();
            // 
            // MessageToSend
            // 
            this.MessageToSend.Location = new System.Drawing.Point(23, 21);
            this.MessageToSend.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MessageToSend.Name = "MessageToSend";
            this.MessageToSend.Size = new System.Drawing.Size(1328, 85);
            this.MessageToSend.TabIndex = 1;
            this.MessageToSend.Text = "";
            // 
            // CreateMessage
            // 
            this.CreateMessage.BackColor = System.Drawing.SystemColors.Highlight;
            this.CreateMessage.Controls.Add(this.Send);
            this.CreateMessage.Controls.Add(this.MessageToSend);
            this.CreateMessage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.CreateMessage.Location = new System.Drawing.Point(15, 831);
            this.CreateMessage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CreateMessage.Name = "CreateMessage";
            this.CreateMessage.Size = new System.Drawing.Size(1523, 128);
            this.CreateMessage.TabIndex = 2;
            // 
            // Send
            // 
            this.Send.BackColor = System.Drawing.SystemColors.HighlightText;
            this.Send.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Send.Location = new System.Drawing.Point(1375, 21);
            this.Send.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(129, 86);
            this.Send.TabIndex = 2;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = false;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // Messages
            // 
            this.Messages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Messages.AutoScroll = true;
            this.Messages.Location = new System.Drawing.Point(63, 150);
            this.Messages.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Messages.Name = "Messages";
            this.Messages.Size = new System.Drawing.Size(1473, 675);
            this.Messages.TabIndex = 3;
            // 
            // UserChatIdentification
            // 
            this.UserChatIdentification.BackColor = System.Drawing.SystemColors.Highlight;
            this.UserChatIdentification.Controls.Add(this.RealName);
            this.UserChatIdentification.Controls.Add(this.Nickname);
            this.UserChatIdentification.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.UserChatIdentification.Location = new System.Drawing.Point(16, 14);
            this.UserChatIdentification.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UserChatIdentification.Name = "UserChatIdentification";
            this.UserChatIdentification.Size = new System.Drawing.Size(1523, 128);
            this.UserChatIdentification.TabIndex = 4;
            // 
            // RealName
            // 
            this.RealName.AutoSize = true;
            this.RealName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RealName.Location = new System.Drawing.Point(976, 39);
            this.RealName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RealName.Name = "RealName";
            this.RealName.Size = new System.Drawing.Size(219, 44);
            this.RealName.TabIndex = 1;
            this.RealName.Text = "Real Name";
            // 
            // Nickname
            // 
            this.Nickname.AutoSize = true;
            this.Nickname.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Nickname.Location = new System.Drawing.Point(307, 39);
            this.Nickname.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Nickname.Name = "Nickname";
            this.Nickname.Size = new System.Drawing.Size(198, 44);
            this.Nickname.TabIndex = 0;
            this.Nickname.Text = "Nickname";
            // 
            // ChatPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1552, 972);
            this.Controls.Add(this.UserChatIdentification);
            this.Controls.Add(this.Messages);
            this.Controls.Add(this.CreateMessage);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ChatPage";
            this.Text = "ChatPage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatPage_FormClosed);
            this.CreateMessage.ResumeLayout(false);
            this.UserChatIdentification.ResumeLayout(false);
            this.UserChatIdentification.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox MessageToSend;
        private System.Windows.Forms.Panel CreateMessage;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.FlowLayoutPanel Messages;
        private System.Windows.Forms.Panel UserChatIdentification;
        private System.Windows.Forms.Label RealName;
        private System.Windows.Forms.Label Nickname;
    }
}