using System;

namespace HealthTracker
{
    partial class PersonDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonDialog));
            this.birthDayCtrl = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.okBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.firstNameCtrl = new System.Windows.Forms.TextBox();
            this.lastNameCtrl = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sexBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.heightBar = new System.Windows.Forms.TrackBar();
            this.heightVal = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightBar)).BeginInit();
            this.SuspendLayout();
            // 
            // birthDayCtrl
            // 
            this.birthDayCtrl.Location = new System.Drawing.Point(75, 110);
            this.birthDayCtrl.Name = "birthDayCtrl";
            this.birthDayCtrl.Size = new System.Drawing.Size(203, 20);
            this.birthDayCtrl.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.heightVal);
            this.groupBox1.Controls.Add(this.heightBar);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.sexBox);
            this.groupBox1.Controls.Add(this.lastNameCtrl);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.firstNameCtrl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.birthDayCtrl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 210);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Birthday";
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(221, 228);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(75, 23);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(140, 228);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "First name";
            // 
            // firstNameCtrl
            // 
            this.firstNameCtrl.Location = new System.Drawing.Point(75, 31);
            this.firstNameCtrl.Name = "firstNameCtrl";
            this.firstNameCtrl.Size = new System.Drawing.Size(203, 20);
            this.firstNameCtrl.TabIndex = 2;
            // 
            // lastNameCtrl
            // 
            this.lastNameCtrl.Location = new System.Drawing.Point(75, 57);
            this.lastNameCtrl.Name = "lastNameCtrl";
            this.lastNameCtrl.Size = new System.Drawing.Size(203, 20);
            this.lastNameCtrl.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Last name";
            // 
            // sexBox
            // 
            this.sexBox.FormattingEnabled = true;
            this.sexBox.Location = new System.Drawing.Point(157, 83);
            this.sexBox.Name = "sexBox";
            this.sexBox.Size = new System.Drawing.Size(121, 21);
            this.sexBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sex";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Height";
            // 
            // heightBar
            // 
            this.heightBar.Location = new System.Drawing.Point(75, 147);
            this.heightBar.Maximum = 236;
            this.heightBar.Minimum = 150;
            this.heightBar.Name = "heightBar";
            this.heightBar.Size = new System.Drawing.Size(203, 45);
            this.heightBar.TabIndex = 8;
            this.heightBar.Value = 150;
            this.heightBar.ValueChanged += new System.EventHandler(this.heightBar_ValueChanged);
            // 
            // heightVal
            // 
            this.heightVal.AutoSize = true;
            this.heightVal.Location = new System.Drawing.Point(22, 172);
            this.heightVal.Name = "heightVal";
            this.heightVal.Size = new System.Drawing.Size(13, 13);
            this.heightVal.TabIndex = 9;
            this.heightVal.Text = "?";
            // 
            // PersonDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 261);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PersonDialog";
            this.Text = "Edit Person";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.heightBar)).EndInit();
            this.ResumeLayout(false);

        }       

        #endregion

        private System.Windows.Forms.DateTimePicker birthDayCtrl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.TextBox lastNameCtrl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstNameCtrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox sexBox;
        private System.Windows.Forms.TrackBar heightBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label heightVal;
    }
}