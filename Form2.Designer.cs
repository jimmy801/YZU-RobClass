namespace RobClass
{
    partial class Form2
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
            this.Student = new System.Windows.Forms.Label();
            this.DeptDegree = new System.Windows.Forms.Label();
            this.DeptName = new System.Windows.Forms.ComboBox();
            this.Degree = new System.Windows.Forms.ComboBox();
            this.ClassTime = new System.Windows.Forms.ComboBox();
            this.OverCredit = new System.Windows.Forms.TextBox();
            this.UnderCredit = new System.Windows.Forms.TextBox();
            this.SelCredit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ClassName = new System.Windows.Forms.ComboBox();
            this.ClassList = new System.Windows.Forms.ListBox();
            this.EndSelect = new System.Windows.Forms.Button();
            this.AddBtn = new System.Windows.Forms.Button();
            this.RemoveBtn = new System.Windows.Forms.Button();
            this.StartRob = new System.Windows.Forms.Button();
            this.EndRob = new System.Windows.Forms.Button();
            this.RobStatus = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.selectTime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.RobResult = new System.Windows.Forms.Label();
            this.ReloginBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Student
            // 
            this.Student.AutoSize = true;
            this.Student.Location = new System.Drawing.Point(113, 11);
            this.Student.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Student.Name = "Student";
            this.Student.Size = new System.Drawing.Size(50, 15);
            this.Student.TabIndex = 0;
            this.Student.Text = "Student";
            // 
            // DeptDegree
            // 
            this.DeptDegree.AutoSize = true;
            this.DeptDegree.Location = new System.Drawing.Point(9, 11);
            this.DeptDegree.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DeptDegree.Name = "DeptDegree";
            this.DeptDegree.Size = new System.Drawing.Size(74, 15);
            this.DeptDegree.TabIndex = 1;
            this.DeptDegree.Text = "DeptDegree";
            // 
            // DeptName
            // 
            this.DeptName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptName.FormattingEnabled = true;
            this.DeptName.Location = new System.Drawing.Point(56, 44);
            this.DeptName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DeptName.Name = "DeptName";
            this.DeptName.Size = new System.Drawing.Size(201, 23);
            this.DeptName.TabIndex = 2;
            this.DeptName.SelectedIndexChanged += new System.EventHandler(this.DeptName_SelectedIndexChanged);
            // 
            // Degree
            // 
            this.Degree.Cursor = System.Windows.Forms.Cursors.Default;
            this.Degree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Degree.FormattingEnabled = true;
            this.Degree.Location = new System.Drawing.Point(56, 76);
            this.Degree.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Degree.Name = "Degree";
            this.Degree.Size = new System.Drawing.Size(201, 23);
            this.Degree.TabIndex = 2;
            this.Degree.SelectedIndexChanged += new System.EventHandler(this.Degree_SelectedIndexChanged);
            // 
            // ClassTime
            // 
            this.ClassTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClassTime.FormattingEnabled = true;
            this.ClassTime.Location = new System.Drawing.Point(56, 109);
            this.ClassTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClassTime.Name = "ClassTime";
            this.ClassTime.Size = new System.Drawing.Size(201, 23);
            this.ClassTime.TabIndex = 2;
            this.ClassTime.SelectedIndexChanged += new System.EventHandler(this.ClassTime_SelectedIndexChanged);
            // 
            // OverCredit
            // 
            this.OverCredit.Enabled = false;
            this.OverCredit.Location = new System.Drawing.Point(334, 9);
            this.OverCredit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OverCredit.Name = "OverCredit";
            this.OverCredit.ReadOnly = true;
            this.OverCredit.Size = new System.Drawing.Size(37, 25);
            this.OverCredit.TabIndex = 3;
            this.OverCredit.Text = "0";
            this.OverCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UnderCredit
            // 
            this.UnderCredit.Enabled = false;
            this.UnderCredit.Location = new System.Drawing.Point(415, 8);
            this.UnderCredit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.UnderCredit.Name = "UnderCredit";
            this.UnderCredit.ReadOnly = true;
            this.UnderCredit.Size = new System.Drawing.Size(37, 25);
            this.UnderCredit.TabIndex = 3;
            this.UnderCredit.Text = "0";
            this.UnderCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SelCredit
            // 
            this.SelCredit.Enabled = false;
            this.SelCredit.Location = new System.Drawing.Point(500, 8);
            this.SelCredit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SelCredit.Name = "SelCredit";
            this.SelCredit.ReadOnly = true;
            this.SelCredit.Size = new System.Drawing.Size(37, 25);
            this.SelCredit.TabIndex = 3;
            this.SelCredit.Text = "0";
            this.SelCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(299, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "上限";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(380, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "下限";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(462, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "已選";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 48);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "系別";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 80);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "年級";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 112);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "時間";
            // 
            // ClassName
            // 
            this.ClassName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClassName.FormattingEnabled = true;
            this.ClassName.Location = new System.Drawing.Point(302, 42);
            this.ClassName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClassName.Name = "ClassName";
            this.ClassName.Size = new System.Drawing.Size(575, 23);
            this.ClassName.TabIndex = 2;
            // 
            // ClassList
            // 
            this.ClassList.FormattingEnabled = true;
            this.ClassList.ItemHeight = 15;
            this.ClassList.Location = new System.Drawing.Point(302, 112);
            this.ClassList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ClassList.Name = "ClassList";
            this.ClassList.Size = new System.Drawing.Size(575, 259);
            this.ClassList.TabIndex = 4;
            // 
            // EndSelect
            // 
            this.EndSelect.Location = new System.Drawing.Point(678, 6);
            this.EndSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EndSelect.Name = "EndSelect";
            this.EndSelect.Size = new System.Drawing.Size(87, 29);
            this.EndSelect.TabIndex = 5;
            this.EndSelect.Text = "結束選課";
            this.EndSelect.UseVisualStyleBackColor = true;
            this.EndSelect.Click += new System.EventHandler(this.EndSelect_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(473, 74);
            this.AddBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(100, 29);
            this.AddBtn.TabIndex = 5;
            this.AddBtn.Text = "加入";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // RemoveBtn
            // 
            this.RemoveBtn.Enabled = false;
            this.RemoveBtn.Location = new System.Drawing.Point(621, 74);
            this.RemoveBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RemoveBtn.Name = "RemoveBtn";
            this.RemoveBtn.Size = new System.Drawing.Size(100, 29);
            this.RemoveBtn.TabIndex = 5;
            this.RemoveBtn.Text = "移除";
            this.RemoveBtn.UseVisualStyleBackColor = true;
            this.RemoveBtn.Click += new System.EventHandler(this.RemoveBtn_Click);
            // 
            // StartRob
            // 
            this.StartRob.Enabled = false;
            this.StartRob.Location = new System.Drawing.Point(473, 383);
            this.StartRob.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartRob.Name = "StartRob";
            this.StartRob.Size = new System.Drawing.Size(100, 29);
            this.StartRob.TabIndex = 5;
            this.StartRob.Text = "開始";
            this.StartRob.UseVisualStyleBackColor = true;
            this.StartRob.Click += new System.EventHandler(this.StartRob_Click);
            // 
            // EndRob
            // 
            this.EndRob.Enabled = false;
            this.EndRob.Location = new System.Drawing.Point(621, 383);
            this.EndRob.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EndRob.Name = "EndRob";
            this.EndRob.Size = new System.Drawing.Size(100, 29);
            this.EndRob.TabIndex = 5;
            this.EndRob.Text = "停止";
            this.EndRob.UseVisualStyleBackColor = true;
            this.EndRob.Click += new System.EventHandler(this.EndRob_Click);
            // 
            // RobStatus
            // 
            this.RobStatus.FormattingEnabled = true;
            this.RobStatus.ItemHeight = 15;
            this.RobStatus.Location = new System.Drawing.Point(12, 171);
            this.RobStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RobStatus.Name = "RobStatus";
            this.RobStatus.Size = new System.Drawing.Size(245, 244);
            this.RobStatus.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(547, 14);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "間隔";
            // 
            // selectTime
            // 
            this.selectTime.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.selectTime.Location = new System.Drawing.Point(586, 8);
            this.selectTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.selectTime.Name = "selectTime";
            this.selectTime.Size = new System.Drawing.Size(53, 25);
            this.selectTime.TabIndex = 8;
            this.selectTime.Text = "4.5";
            this.selectTime.TextChanged += new System.EventHandler(this.selectTime_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(642, 14);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "秒";
            // 
            // RobResult
            // 
            this.RobResult.AutoSize = true;
            this.RobResult.Location = new System.Drawing.Point(93, 152);
            this.RobResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RobResult.Name = "RobResult";
            this.RobResult.Size = new System.Drawing.Size(67, 15);
            this.RobResult.TabIndex = 9;
            this.RobResult.Text = "選課結果";
            // 
            // ReloginBtn
            // 
            this.ReloginBtn.Location = new System.Drawing.Point(785, 6);
            this.ReloginBtn.Name = "ReloginBtn";
            this.ReloginBtn.Size = new System.Drawing.Size(93, 29);
            this.ReloginBtn.TabIndex = 10;
            this.ReloginBtn.Text = "重新登入";
            this.ReloginBtn.UseVisualStyleBackColor = true;
            this.ReloginBtn.Click += new System.EventHandler(this.ReloginBtn_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(890, 425);
            this.Controls.Add(this.ReloginBtn);
            this.Controls.Add(this.RobResult);
            this.Controls.Add(this.selectTime);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.RobStatus);
            this.Controls.Add(this.EndRob);
            this.Controls.Add(this.StartRob);
            this.Controls.Add(this.RemoveBtn);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.EndSelect);
            this.Controls.Add(this.ClassList);
            this.Controls.Add(this.SelCredit);
            this.Controls.Add(this.UnderCredit);
            this.Controls.Add(this.OverCredit);
            this.Controls.Add(this.ClassName);
            this.Controls.Add(this.ClassTime);
            this.Controls.Add(this.Degree);
            this.Controls.Add(this.DeptName);
            this.Controls.Add(this.DeptDegree);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Student);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "選課清單";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.SizeChanged += new System.EventHandler(this.Form2_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.Form2_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Student;
        private System.Windows.Forms.Label DeptDegree;
        private System.Windows.Forms.ComboBox DeptName;
        private System.Windows.Forms.ComboBox Degree;
        private System.Windows.Forms.ComboBox ClassTime;
        private System.Windows.Forms.TextBox OverCredit;
        private System.Windows.Forms.TextBox UnderCredit;
        private System.Windows.Forms.TextBox SelCredit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ClassName;
        private System.Windows.Forms.ListBox ClassList;
        private System.Windows.Forms.Button EndSelect;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Button RemoveBtn;
        private System.Windows.Forms.Button StartRob;
        private System.Windows.Forms.Button EndRob;
        private System.Windows.Forms.ListBox RobStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox selectTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label RobResult;
        private System.Windows.Forms.Button ReloginBtn;
    }
}