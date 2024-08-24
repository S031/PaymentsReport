using System;
using System.IO;
using System.Windows.Forms;

namespace PaymentsReport
{
	public partial class ReportParametrsForm : Form
	{
		public string SourcePath
		{
			get => txtSourcePath.Text;
			set => txtSourcePath.Text = value;
		}
		public string ReportPath
		{
			get => txtReportPath.Text;
			set => txtReportPath.Text = value;
		}
		public string INNRoot
		{
			get => txtINNRoot.Text;
			set => txtINNRoot.Text = value;
		}
		public string INNFirst
		{
			get => txtINNFirst.Text; 
			set => txtINNFirst.Text = value;
		}
		private Action<ReportParametrsForm> _action;
		public Action<ReportParametrsForm> ReportAction
		{
			get => _action;
			set => _action = value;
		}
		public ReportParametrsForm()
		{
			InitializeComponent();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			txtLog.Text = string.Empty;
			if (_action != null)
				_action(this);
		}

		public void Log(string message)
		{
			txtLog.AppendText(message);
			txtLog.AppendText(Environment.NewLine);
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void cmdOpenFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				//openFileDialog.InitialDirectory = "c:\\";
				openFileDialog.Filter = "excel files (*.xlsx)|*.xlsx";
				openFileDialog.FilterIndex = 1;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					SourcePath = openFileDialog.FileName;
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.FileName = Path.Combine(Path.GetDirectoryName(SourcePath),
					Path.GetFileNameWithoutExtension(SourcePath) + "_report" +
					Path.GetExtension(SourcePath));
				saveFileDialog.Filter = "excel files (*.xlsx)|*.xlsx|text files (*.txt)|*.txt";
				saveFileDialog.FilterIndex = 1;
				saveFileDialog.RestoreDirectory = true;

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					ReportPath = saveFileDialog.FileName;
				}
			}
		}
	}
}
