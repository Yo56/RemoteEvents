namespace TDIN_PROJ1.Client
{
    partial class LoginPage
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelLoginError = new System.Windows.Forms.Label();
            this.labeLogin2 = new System.Windows.Forms.Label();
            this.labelLoginPass = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelLoginNick = new System.Windows.Forms.Label();
            this.inputLoginNick = new System.Windows.Forms.TextBox();
            this.inputLoginPass = new System.Windows.Forms.TextBox();
            this.labelRegisterNickError = new System.Windows.Forms.Label();
            this.labelRegisterPassError = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.inputRegisterNick = new System.Windows.Forms.TextBox();
            this.inputRegisterPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.inputRegisterName = new System.Windows.Forms.TextBox();
            this.inputRegisterPassConf = new System.Windows.Forms.TextBox();
            this.labelRegister2 = new System.Windows.Forms.Label();
            this.labelRegister = new System.Windows.Forms.Label();
            this.buttonRegister = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer1.Panel1.Controls.Add(this.labelLoginError);
            this.splitContainer1.Panel1.Controls.Add(this.labeLogin2);
            this.splitContainer1.Panel1.Controls.Add(this.labelLoginPass);
            this.splitContainer1.Panel1.Controls.Add(this.buttonLogin);
            this.splitContainer1.Panel1.Controls.Add(this.labelLogin);
            this.splitContainer1.Panel1.Controls.Add(this.labelLoginNick);
            this.splitContainer1.Panel1.Controls.Add(this.inputLoginNick);
            this.splitContainer1.Panel1.Controls.Add(this.inputLoginPass);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Highlight;
            this.splitContainer1.Panel2.Controls.Add(this.labelRegisterNickError);
            this.splitContainer1.Panel2.Controls.Add(this.labelRegisterPassError);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.inputRegisterNick);
            this.splitContainer1.Panel2.Controls.Add(this.inputRegisterPass);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.inputRegisterName);
            this.splitContainer1.Panel2.Controls.Add(this.inputRegisterPassConf);
            this.splitContainer1.Panel2.Controls.Add(this.labelRegister2);
            this.splitContainer1.Panel2.Controls.Add(this.labelRegister);
            this.splitContainer1.Panel2.Controls.Add(this.buttonRegister);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer1.Size = new System.Drawing.Size(1113, 606);
            this.splitContainer1.SplitterDistance = 370;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // labelLoginError
            // 
            this.labelLoginError.AutoSize = true;
            this.labelLoginError.ForeColor = System.Drawing.Color.Red;
            this.labelLoginError.Location = new System.Drawing.Point(11, 378);
            this.labelLoginError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLoginError.Name = "labelLoginError";
            this.labelLoginError.Size = new System.Drawing.Size(325, 20);
            this.labelLoginError.TabIndex = 6;
            this.labelLoginError.Text = "User not found, please verify your credentials";
            this.labelLoginError.Visible = false;
            // 
            // labeLogin2
            // 
            this.labeLogin2.AutoSize = true;
            this.labeLogin2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeLogin2.Location = new System.Drawing.Point(40, 135);
            this.labeLogin2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labeLogin2.Name = "labeLogin2";
            this.labeLogin2.Size = new System.Drawing.Size(292, 50);
            this.labeLogin2.TabIndex = 0;
            this.labeLogin2.Text = "Already have an account ?\r\nPlease enter your credentials\r\n";
            this.labeLogin2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelLoginPass
            // 
            this.labelLoginPass.AutoSize = true;
            this.labelLoginPass.Location = new System.Drawing.Point(11, 320);
            this.labelLoginPass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLoginPass.Name = "labelLoginPass";
            this.labelLoginPass.Size = new System.Drawing.Size(86, 20);
            this.labelLoginPass.TabIndex = 5;
            this.labelLoginPass.Text = "Password :";
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.SystemColors.Highlight;
            this.buttonLogin.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonLogin.Location = new System.Drawing.Point(121, 439);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(101, 41);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "Log In";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogin.Location = new System.Drawing.Point(131, 62);
            this.labelLogin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(93, 32);
            this.labelLogin.TabIndex = 0;
            this.labelLogin.Text = "Log In";
            this.labelLogin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelLoginNick
            // 
            this.labelLoginNick.AutoSize = true;
            this.labelLoginNick.Location = new System.Drawing.Point(8, 255);
            this.labelLoginNick.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLoginNick.Name = "labelLoginNick";
            this.labelLoginNick.Size = new System.Drawing.Size(87, 20);
            this.labelLoginNick.TabIndex = 4;
            this.labelLoginNick.Text = "Nickname :";
            // 
            // inputLoginNick
            // 
            this.inputLoginNick.Location = new System.Drawing.Point(102, 253);
            this.inputLoginNick.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputLoginNick.Name = "inputLoginNick";
            this.inputLoginNick.Size = new System.Drawing.Size(243, 26);
            this.inputLoginNick.TabIndex = 1;
            // 
            // inputLoginPass
            // 
            this.inputLoginPass.Location = new System.Drawing.Point(102, 318);
            this.inputLoginPass.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputLoginPass.Name = "inputLoginPass";
            this.inputLoginPass.Size = new System.Drawing.Size(243, 26);
            this.inputLoginPass.TabIndex = 2;
            this.inputLoginPass.UseSystemPasswordChar = true;
            // 
            // labelRegisterNickError
            // 
            this.labelRegisterNickError.AutoSize = true;
            this.labelRegisterNickError.ForeColor = System.Drawing.Color.Red;
            this.labelRegisterNickError.Location = new System.Drawing.Point(533, 289);
            this.labelRegisterNickError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRegisterNickError.Name = "labelRegisterNickError";
            this.labelRegisterNickError.Size = new System.Drawing.Size(178, 20);
            this.labelRegisterNickError.TabIndex = 16;
            this.labelRegisterNickError.Text = "Nickname already exists";
            this.labelRegisterNickError.Visible = false;
            // 
            // labelRegisterPassError
            // 
            this.labelRegisterPassError.AutoSize = true;
            this.labelRegisterPassError.ForeColor = System.Drawing.Color.Red;
            this.labelRegisterPassError.Location = new System.Drawing.Point(533, 416);
            this.labelRegisterPassError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRegisterPassError.Name = "labelRegisterPassError";
            this.labelRegisterPassError.Size = new System.Drawing.Size(183, 20);
            this.labelRegisterPassError.TabIndex = 7;
            this.labelRegisterPassError.Text = "Passwords do not match";
            this.labelRegisterPassError.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 351);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Password :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(193, 286);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Nickname :";
            // 
            // inputRegisterNick
            // 
            this.inputRegisterNick.Location = new System.Drawing.Point(286, 284);
            this.inputRegisterNick.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputRegisterNick.Name = "inputRegisterNick";
            this.inputRegisterNick.Size = new System.Drawing.Size(243, 26);
            this.inputRegisterNick.TabIndex = 5;
            // 
            // inputRegisterPass
            // 
            this.inputRegisterPass.Location = new System.Drawing.Point(286, 349);
            this.inputRegisterPass.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputRegisterPass.Name = "inputRegisterPass";
            this.inputRegisterPass.Size = new System.Drawing.Size(243, 26);
            this.inputRegisterPass.TabIndex = 6;
            this.inputRegisterPass.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 416);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Confirm password :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 224);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Name :";
            // 
            // inputRegisterName
            // 
            this.inputRegisterName.Location = new System.Drawing.Point(286, 222);
            this.inputRegisterName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputRegisterName.Name = "inputRegisterName";
            this.inputRegisterName.Size = new System.Drawing.Size(243, 26);
            this.inputRegisterName.TabIndex = 4;
            // 
            // inputRegisterPassConf
            // 
            this.inputRegisterPassConf.Location = new System.Drawing.Point(286, 414);
            this.inputRegisterPassConf.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputRegisterPassConf.Name = "inputRegisterPassConf";
            this.inputRegisterPassConf.Size = new System.Drawing.Size(243, 26);
            this.inputRegisterPassConf.TabIndex = 7;
            this.inputRegisterPassConf.UseSystemPasswordChar = true;
            // 
            // labelRegister2
            // 
            this.labelRegister2.AutoSize = true;
            this.labelRegister2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegister2.Location = new System.Drawing.Point(290, 135);
            this.labelRegister2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRegister2.Name = "labelRegister2";
            this.labelRegister2.Size = new System.Drawing.Size(194, 25);
            this.labelRegister2.TabIndex = 0;
            this.labelRegister2.Text = "Create an account ";
            this.labelRegister2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelRegister
            // 
            this.labelRegister.AutoSize = true;
            this.labelRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRegister.Location = new System.Drawing.Point(316, 62);
            this.labelRegister.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRegister.Name = "labelRegister";
            this.labelRegister.Size = new System.Drawing.Size(121, 32);
            this.labelRegister.TabIndex = 0;
            this.labelRegister.Text = "Register";
            // 
            // buttonRegister
            // 
            this.buttonRegister.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonRegister.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonRegister.Location = new System.Drawing.Point(332, 487);
            this.buttonRegister.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(101, 41);
            this.buttonRegister.TabIndex = 8;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = false;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // LoginPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 606);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LoginPage";
            this.Text = "Chat Service Login/Register";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label labelRegister;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.TextBox inputLoginNick;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox inputLoginPass;
        private System.Windows.Forms.Label labelLoginPass;
        private System.Windows.Forms.Label labelLoginNick;
        private System.Windows.Forms.Label labeLogin2;
        private System.Windows.Forms.Label labelRegister2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox inputRegisterNick;
        private System.Windows.Forms.TextBox inputRegisterPass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox inputRegisterName;
        private System.Windows.Forms.TextBox inputRegisterPassConf;
        private System.Windows.Forms.Label labelLoginError;
        private System.Windows.Forms.Label labelRegisterPassError;
        private System.Windows.Forms.Label labelRegisterNickError;
    }
}