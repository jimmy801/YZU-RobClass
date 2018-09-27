namespace RobClass
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ClassSystem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Account = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Check = new System.Windows.Forms.TextBox();
            this.Remember = new System.Windows.Forms.CheckBox();
            this.CheckPicture = new System.Windows.Forms.PictureBox();
            this.Login = new System.Windows.Forms.Button();
            this.Count = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ClassSystemID = new System.Windows.Forms.ComboBox();
            this.ErrorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CheckPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // ClassSystem
            // 
            this.ClassSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClassSystem.FormattingEnabled = true;
            this.ClassSystem.Location = new System.Drawing.Point(12, 9);
            this.ClassSystem.Name = "ClassSystem";
            this.ClassSystem.Size = new System.Drawing.Size(260, 20);
            this.ClassSystem.TabIndex = 0;
            this.ClassSystem.SelectedIndexChanged += new System.EventHandler(this.ClassSystem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "帳號";
            // 
            // Account
            // 
            this.Account.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Account.Location = new System.Drawing.Point(49, 42);
            this.Account.Name = "Account";
            this.Account.Size = new System.Drawing.Size(223, 22);
            this.Account.TabIndex = 1;
            this.Account.TextChanged += new System.EventHandler(this.stop_count);
            this.Account.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.submit);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密碼";
            // 
            // Password
            // 
            this.Password.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Password.Location = new System.Drawing.Point(49, 70);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(223, 22);
            this.Password.TabIndex = 2;
            this.Password.TextChanged += new System.EventHandler(this.stop_count);
            this.Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.submit);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "驗證碼";
            // 
            // Check
            // 
            this.Check.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Check.Location = new System.Drawing.Point(49, 98);
            this.Check.Name = "Check";
            this.Check.Size = new System.Drawing.Size(223, 22);
            this.Check.TabIndex = 3;
            this.Check.TextChanged += new System.EventHandler(this.stop_count);
            this.Check.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.submit);
            // 
            // Remember
            // 
            this.Remember.AutoSize = true;
            this.Remember.Checked = true;
            this.Remember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Remember.Location = new System.Drawing.Point(80, 126);
            this.Remember.Name = "Remember";
            this.Remember.Size = new System.Drawing.Size(120, 16);
            this.Remember.TabIndex = 4;
            this.Remember.Text = "記錄我的登入資訊";
            this.Remember.UseVisualStyleBackColor = true;
            // 
            // CheckPicture
            // 
            this.CheckPicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckPicture.Location = new System.Drawing.Point(111, 153);
            this.CheckPicture.Name = "CheckPicture";
            this.CheckPicture.Size = new System.Drawing.Size(60, 20);
            this.CheckPicture.TabIndex = 4;
            this.CheckPicture.TabStop = false;
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(80, 208);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(120, 23);
            this.Login.TabIndex = 5;
            this.Login.Text = "登入";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // Count
            // 
            this.Count.AutoSize = true;
            this.Count.Location = new System.Drawing.Point(136, 176);
            this.Count.Name = "Count";
            this.Count.Size = new System.Drawing.Size(0, 12);
            this.Count.TabIndex = 6;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ClassSystemID
            // 
            this.ClassSystemID.FormattingEnabled = true;
            this.ClassSystemID.Location = new System.Drawing.Point(102, 9);
            this.ClassSystemID.Name = "ClassSystemID";
            this.ClassSystemID.Size = new System.Drawing.Size(10, 20);
            this.ClassSystemID.TabIndex = 8;
            this.ClassSystemID.Visible = false;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(100, 188);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 12);
            this.ErrorLabel.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 243);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.Count);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.CheckPicture);
            this.Controls.Add(this.Remember);
            this.Controls.Add(this.Check);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Account);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClassSystem);
            this.Controls.Add(this.ClassSystemID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "元智自動選課";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.CheckPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ClassSystem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Account;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Check;
        private System.Windows.Forms.CheckBox Remember;
        private System.Windows.Forms.PictureBox CheckPicture;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Label Count;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox ClassSystemID;
        private System.Windows.Forms.Label ErrorLabel;
    }
}

