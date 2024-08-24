namespace PaymentsReport
{
	partial class ReportParametrsForm
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
			this.txtSourcePath = new System.Windows.Forms.TextBox();
			this.txtReportPath = new System.Windows.Forms.TextBox();
			this.txtINNRoot = new System.Windows.Forms.TextBox();
			this.txtINNFirst = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.cmdOpenFile = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOK = new System.Windows.Forms.Button();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtSourcePath
			// 
			this.txtSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSourcePath.Location = new System.Drawing.Point(155, 2);
			this.txtSourcePath.Name = "txtSourcePath";
			this.txtSourcePath.Size = new System.Drawing.Size(457, 20);
			this.txtSourcePath.TabIndex = 0;
			// 
			// txtReportPath
			// 
			this.txtReportPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtReportPath.Location = new System.Drawing.Point(155, 28);
			this.txtReportPath.Name = "txtReportPath";
			this.txtReportPath.Size = new System.Drawing.Size(456, 20);
			this.txtReportPath.TabIndex = 1;
			// 
			// txtINNRoot
			// 
			this.txtINNRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtINNRoot.Location = new System.Drawing.Point(155, 54);
			this.txtINNRoot.Name = "txtINNRoot";
			this.txtINNRoot.Size = new System.Drawing.Size(456, 20);
			this.txtINNRoot.TabIndex = 2;
			// 
			// txtINNFirst
			// 
			this.txtINNFirst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtINNFirst.Location = new System.Drawing.Point(155, 80);
			this.txtINNFirst.Name = "txtINNFirst";
			this.txtINNFirst.Size = new System.Drawing.Size(456, 20);
			this.txtINNFirst.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(2, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Имя исходного файла";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(2, 35);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Имя файла отчета";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(2, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(60, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "ИНН УФК";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(2, 83);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(113, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "ИНН ген подрядчика";
			// 
			// cmdOpenFile
			// 
			this.cmdOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOpenFile.Location = new System.Drawing.Point(616, 2);
			this.cmdOpenFile.Name = "cmdOpenFile";
			this.cmdOpenFile.Size = new System.Drawing.Size(24, 20);
			this.cmdOpenFile.TabIndex = 5;
			this.cmdOpenFile.Text = "...";
			this.cmdOpenFile.UseVisualStyleBackColor = true;
			this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(616, 29);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(22, 19);
			this.button1.TabIndex = 5;
			this.button1.Text = "...";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdCancel.Location = new System.Drawing.Point(563, 394);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 33);
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "Закрыть";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.Location = new System.Drawing.Point(408, 394);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.Size = new System.Drawing.Size(149, 33);
			this.cmdOK.TabIndex = 7;
			this.cmdOK.Text = "Сформировать отчет";
			this.cmdOK.UseVisualStyleBackColor = true;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// txtLog
			// 
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog.Location = new System.Drawing.Point(5, 114);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.Size = new System.Drawing.Size(633, 264);
			this.txtLog.TabIndex = 8;
			// 
			// ReportParametrsForm
			// 
			this.AcceptButton = this.cmdOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cmdCancel;
			this.ClientSize = new System.Drawing.Size(651, 434);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cmdOpenFile);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtINNFirst);
			this.Controls.Add(this.txtINNRoot);
			this.Controls.Add(this.txtReportPath);
			this.Controls.Add(this.txtSourcePath);
			this.Name = "ReportParametrsForm";
			this.Text = "Выберите параметры отчета";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtSourcePath;
		private System.Windows.Forms.TextBox txtReportPath;
		private System.Windows.Forms.TextBox txtINNRoot;
		private System.Windows.Forms.TextBox txtINNFirst;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button cmdOpenFile;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.TextBox txtLog;
	}
}