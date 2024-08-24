using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaymentsReport
{
	internal class Program
	{
		private const string default_source_file_name = "Contract\\GosContract.xlsx";
		private const string default_report_file_name = "Contract\\GosContract.txt";
		private const string default_report_file_name_xls = "Contract\\GosContractReport.xlsx";
		private const string inn_root = "7704252261"; //УФК

		private static string GetDefaultSourceFileName()
			=> string.IsNullOrEmpty( AppSettings.Default.SourceFileName) 
				? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), default_source_file_name)
				: AppSettings.Default.SourceFileName;
		private static string GetDefaultReportFileName()
			=> string.IsNullOrEmpty( AppSettings.Default.SourceFileName) 
				? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), default_report_file_name)
				: AppSettings.Default.ReportFileName;
		private static string GetDefaultINNRoot()
			=> string.IsNullOrEmpty( AppSettings.Default.InnRoot) 
				? inn_root
				: AppSettings.Default.InnRoot;
		private static string GetDefaultINNFirst()
			=> string.IsNullOrEmpty( AppSettings.Default.InnFirst) 
				? inn_root
				: AppSettings.Default.InnFirst;

		/// <summary>
		/// Если в командной строке не указаны имена файлов он ищет их на рабочем столе в папке Contract
		/// название папки можно поменять в объявлении констант default_source_file_name иdefault_report_file_name 
		/// </summary>
		/// <param name="args"></param>
		[STAThreadAttribute]
		static void Main(string[] args)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			string excelPath = string.Empty;
			string reportPath = string.Empty;

			if (args.Length > 1)
			{
				excelPath = args[0];
				reportPath = args[1];
				//Console.WriteLine($"Для запуска программы необходимо указать в аргументах клммандной строки имя файла с исходными данными (xlsx) и имя файла для сохранения отчета (xlsx или txt)"
			}
			else if (args.Length == 1)
			{
				excelPath = args[0];
				reportPath = GetDefaultReportFileName();
			}
			else
			{
				excelPath = GetDefaultSourceFileName();
				reportPath = GetDefaultReportFileName();
			}


			var parametersForm = new ReportParametrsForm()
			{
				SourcePath = excelPath,
				ReportPath = reportPath,
				INNRoot = GetDefaultINNRoot(),
				INNFirst = GetDefaultINNFirst(),

				ReportAction = f =>
				{
					if (!File.Exists(f.SourcePath))
					{
						f.Log($"Для запуска программы необходимо указать в аргументах командной строки имя файла с исходными данными (xlsx) и имя файла для сохранения отчета (xslx или txt)");
						return;
					}
					AppSettings.Default.SourceFileName = f.SourcePath;
					AppSettings.Default.ReportFileName = f.ReportPath;
					AppSettings.Default.InnRoot = f.INNRoot;
					AppSettings.Default.InnFirst = f.INNFirst;
					AppSettings.Default.Save();
					var service = new ReportService();
					f.Log($"Загружено {service.LoadData(f.SourcePath)} платежей.");
					service.DoReport2(f.ReportPath, f.INNRoot, f.INNFirst);
					f.Log($"Отчет сохранен в папке  {f.ReportPath}.");
				}
			};
			Application.Run(parametersForm);
		}
	}
}
