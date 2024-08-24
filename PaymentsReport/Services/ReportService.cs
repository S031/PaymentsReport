using OfficeOpenXml;
using OfficeOpenXml.Style;
using PaymentsReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace PaymentsReport
{
	internal class ReportService
	{
		// Счета
		private readonly Dictionary<int, AccountModel> _accounts = new Dictionary<int, AccountModel>();
		// Индексирование по ИНН и Номеру счета
		private readonly Dictionary<string, int> _indexAccounts = new Dictionary<string, int>();

		// Группы платежей
		private readonly Dictionary<int, PaymentGroupModel> _paymentsGroups = new Dictionary<int, PaymentGroupModel>();
		// Индексирование по Id счета плательщика и Id счета получателя
		private readonly Dictionary<string, int> _indexPaymentGroup = new Dictionary<string, int>();

		//Платежи
		private readonly List<PaymentModel> _payments = new List<PaymentModel>();

		//Счетчики
		private int _accountsSequnce;
		private int _paymentSequence;
		private int _paymentGroupSequence;
		public int LoadData(string path)
		{
			using (var dt = ReadExcelFile(path))
			{
				int i = 0;
				foreach (DataRow row in dt.Rows)
				{
					var payerName = row["Наименование плательщика"]
						.ToString()
						.Replace("Общество с ограниченной ответственностью", "ООО")
						.Replace("ОТКРЫТОЕ АКЦИОНЕРНОЕ ОБЩЕСТВО", "ОАО")
						.Replace("Акционерное общество", "АО");
					
					var recipientName = row["Наименование получателя"]
						.ToString()
						.Replace("Общество с ограниченной ответственностью", "ООО")
						.Replace("ОТКРЫТОЕ АКЦИОНЕРНОЕ ОБЩЕСТВО", "ОАО")
						.Replace("Акционерное общество", "АО");

					// Проверьте, существует ли счет плательщика
					int payerAccountId = GetOrInsertAccount(row["ИНН плательщика"].ToString(), row["Номер счета плательщика"].ToString(), payerName);

					// Проверьте, существует ли счет получателя
					int recipientAccountId = GetOrInsertAccount(row["ИНН получателя"].ToString(), row["Номер счета получателя"].ToString(), recipientName);

					double amount = double.Parse(row["Сумма"].ToString());

					// Ключ поиска групп платежей
					var searchKey = PaymentGroupModel.CreateKey(payerAccountId, recipientAccountId);
					// Ищем по ключу
					if (!_indexPaymentGroup.TryGetValue(searchKey, out int index))
					{
						index = _paymentGroupSequence++;
						_paymentsGroups.Add(index, new PaymentGroupModel(index, payerAccountId, recipientAccountId)
						{
							TotalPayments = 1,
							TotalSum = amount
						}
						);
						_indexPaymentGroup.Add(searchKey, index);
					}
					else
					{
						_paymentsGroups[index].TotalPayments++;
						_paymentsGroups[index].TotalSum += amount;
					}

					var p = new PaymentModel(_paymentSequence++,
						index,
						DateTime.Parse(row["Дата документа"].ToString()),
						row["Номер документа"].ToString(),
						amount,
						row["Номер контракта"].ToString(),
						row["Назначение платежа"].ToString());
					_payments.Add(p);
					i++;
				}
				return i;
			}
		}

		private static DataTable ReadExcelFile(string path)
		{
			FileInfo fileInfo = new FileInfo(path);
			using (ExcelPackage package = new ExcelPackage(fileInfo))
			{
				ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
				DataTable dataTable = new DataTable();

				for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
				{
					dataTable.Columns.Add(worksheet.Cells[1, col].Text);
				}

				for (int row = 3; row <= worksheet.Dimension.End.Row; row++)
				{
					DataRow newRow = dataTable.NewRow();
					for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
					{
						newRow[col - 1] = worksheet.Cells[row, col].Text;
					}
					dataTable.Rows.Add(newRow);
				}
				return dataTable;
			}
		}

		private int GetOrInsertAccount(string inn, string accountNumber, string accountName)
		{
			string keySearch = AccountModel.CreateKey(inn, accountNumber);
			if (!_indexAccounts.TryGetValue(keySearch, out int index))
			{
				index = _accountsSequnce++;
				_accounts.Add(index, new AccountModel(index, inn, accountName, accountNumber));
				_indexAccounts.Add(keySearch, index);
			}
			return index;
		}

		private string _innRoot;
		private string _innFirst;
		public void DoReport2(string reportFileName, string innRoot, string innFirst)
		{
			object output;
			if (Path.GetExtension(reportFileName).ToLower() == ".txt")
				output = new StringBuilder();
			else
				output = new List<object>();

			Dictionary<PaymentGroupModel, int> readyList = new Dictionary<PaymentGroupModel, int>();

			var rootPayerAccounts = _accounts.Where(kvp => kvp.Value.INN == innRoot).Select(kvp => kvp.Value);
			var startPayerAccounts = _accounts.Where(kvp => kvp.Value.INN == innFirst).Select(kvp => kvp.Value);
			var startPaymentGroups = _paymentsGroups.Where(kvp => rootPayerAccounts.Any(a => a.Id == kvp.Value.PayerAccountId) &&
				startPayerAccounts.Any(a => a.Id > kvp.Value.RecipientAccountId)).Select(kvp => kvp.Value);
			
			foreach (var paymentGroup in startPaymentGroups)
				CreatePaymentsRoot(output, paymentGroup, readyList);
			CreatePaymentsTree(output, startPaymentGroups.First(), readyList, 1);

			if (output is StringBuilder sb)
				File.WriteAllText(reportFileName, sb.ToString(), Encoding.UTF8);
			else if (output is List<object> data)
				WriteExcelFile(reportFileName, data);

		}

		private void CreatePaymentsRoot(object output, PaymentGroupModel paymentGroup, Dictionary<PaymentGroupModel, int> readyList)
		{
			//PrintInfo(output, paymentGroup, 0);
			readyList.Add(paymentGroup, 0);
		}

		private void CreatePaymentsTree(object output, PaymentGroupModel paymentGroup, Dictionary<PaymentGroupModel, int> readyList, int level)
		{
			PrintInfo(output, paymentGroup, level);

			foreach (var kvp in _paymentsGroups.Where(p => p.Value.PayerAccountId == paymentGroup.RecipientAccountId))
			{
				var item = kvp.Value;
				if (!readyList.ContainsKey(item))
				{
					readyList.Add(item, level++);
					CreatePaymentsTree(output, item, readyList, level);
				}
				level = readyList[item];
			}
		}

		private void PrintInfo(object output, PaymentGroupModel paymentGroup, int level)
		{
			var payerAccount = _accounts[paymentGroup.PayerAccountId];
			var recipientAccount = _accounts[paymentGroup.RecipientAccountId];
			int currentLevel = level < 0 ? 0 : level;

			if (output is StringBuilder sb)
			{
				var tab = new string('\t', currentLevel);
				sb.AppendFormat(tab + "Платежи {0} (ИНН: {1}, Счет {2}) в пользу  {3} (ИНН: {4}, Счет {5}). Всего {6:G}, на сумму {7:n2}{8}",
					payerAccount.Title,
					payerAccount.INN,
					payerAccount.AccountNumber,
					recipientAccount.Title,
					recipientAccount.INN,
					recipientAccount.AccountNumber,
					paymentGroup.TotalPayments,
					paymentGroup.TotalSum,
					Environment.NewLine);

				foreach (var p in _payments.Where(p => p.PaymentGroupId == paymentGroup.Id))
				{
					sb.AppendFormat(tab + "\tПлатеж №{0} от {1:dd.MM.yyyy} на сумму {2:n2} Назначение платежа: {3} Номер контракта {4}{5}",
						p.Number,
						p.Date,
						p.Amount,
						p.Description,
						p.ContractNumber,
						Environment.NewLine);
				}
			}
			else
			{
				var result = output as List<object>;
				result.Add(new PaymentGroupViewModel(
					payerAccount,
					recipientAccount,
					paymentGroup.TotalPayments,
					paymentGroup.TotalSum,
					currentLevel));

				foreach (var pm in _payments
					.Where(p => p.PaymentGroupId == paymentGroup.Id)
					.Select(p => new PaymentViewModel(p, currentLevel)))
				{
					result.Add(pm);
				}

			}
		}

		private static void WriteExcelFile(string path, List<object> data)
		{
			var file = GetCleanFileInfo(path);
			using (ExcelPackage xlPackage = new ExcelPackage(file))
			{
				ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add("Отчет");

				const int startRow = 1;
				int row = startRow;
				int col = 1;
				
				worksheet.Columns.Width *= 1.3;
				foreach (var obj in data)
				{
					if (obj is PaymentGroupViewModel pgm)
					{
						col = pgm.Level + 1;
						worksheet.Cells[row, col++].Value = "Платежи";
						using (ExcelRange r = worksheet.Cells[row, col++, row, col += 1])
						{
							r.Merge = true;
							r.Value = pgm.PayerAccount.Title;
							col++;
						}
						worksheet.Cells[row, col++].Value = "ИНН";
						worksheet.Cells[row, col++].Value = pgm.PayerAccount.INN;

						worksheet.Cells[row, col++].Value = "Счет";
						using (ExcelRange r = worksheet.Cells[row, col++, row, col])
						{
							r.Merge = true;
							r.Value = pgm.PayerAccount.AccountNumber;
							col++;
						}

						worksheet.Cells[row, col++].Value = "в пользу";
						using (ExcelRange r = worksheet.Cells[row, col++, row, col += 1])
						{
							r.Merge = true;
							r.Value = pgm.RecipientAccount.Title;
							col++;
						}
						worksheet.Cells[row, col++].Value = "ИНН";
						worksheet.Cells[row, col++].Value = pgm.RecipientAccount.INN;

						worksheet.Cells[row, col++].Value = "Счет";
						using (ExcelRange r = worksheet.Cells[row, col++, row, col])
						{
							r.Merge = true;
							r.Value = pgm.RecipientAccount.AccountNumber;
							col++;
						}

						worksheet.Cells[row, col++].Value = "Всего";
						worksheet.Cells[row, col++].Value = pgm.TotalPayments;

						worksheet.Cells[row, col++].Value = "на сумму";
						worksheet.Cells[row, col++].Value = pgm.TotalSum;
						worksheet.Cells[row, col - 1].Style.Numberformat.Format = "###,##0.00";

						worksheet.Rows[row].Style.Font.Bold = true;

					}
					else if (obj is PaymentViewModel pm)
					{
						col = pm.Level + 1;
						worksheet.Cells[row, col++].Value = pm.Payment.Number;

						worksheet.Cells[row, col++].Value = "от";
						worksheet.Cells[row, col++].Value = pm.Payment.Date.ToString("dd.MM.yyyy");

						worksheet.Cells[row, col++].Value = "на сумму";
						worksheet.Cells[row, col++].Value = pm.Payment.Amount;
						worksheet.Cells[row, col - 1].Style.Numberformat.Format = "###,##0.00";

						worksheet.Cells[row, col++].Value = "Назначение платежа:";
						using (ExcelRange r = worksheet.Cells[row, col++, row, col += 2])
						{
							r.Merge = true;
							r.Value = pm.Payment.Description;
							col++;
						}

						worksheet.Cells[row, col++].Value = "Контракт";
						worksheet.Cells[row, col++].Value = pm.Payment.ContractNumber;
					}
					row++;
				}
				// save the new spreadsheet
				xlPackage.Save();
			}
		}

		public static FileInfo GetCleanFileInfo(string path)
		{
			var fi = new FileInfo(path);
			if (fi.Exists)
			{
				fi.Delete();
			}
			return fi;
		}
	}
}


