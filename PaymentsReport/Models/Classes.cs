using System;

namespace PaymentsReport.Models
{
	public class AccountModel
	{
		public int Id { get; }
		public string INN { get; }
		public string Title { get; }
		public string AccountNumber { get; }

		public AccountModel(int id, string inn, string title, string accountNumber)
		{
			Id = id;
			INN = inn;
			Title = title;
			AccountNumber = accountNumber;
		}

		public static string CreateKey(string inn, string accountNumber) => $"{inn}-{accountNumber}";
	}

	public class PaymentGroupModel
	{
		public int Id { get; }
		public int PayerAccountId { get; }
		public int RecipientAccountId { get; }
		public int TotalPayments { get; set; }
		public double TotalSum { get; set; }

		public PaymentGroupModel(int id, int payerAccountId, int recipientAccountId)
		{
			Id = id;
			PayerAccountId = payerAccountId;
			RecipientAccountId = recipientAccountId;
		}

		public static string CreateKey(int payerAccountId, int recipientAccountId)
			=> $"{payerAccountId.ToString().PadLeft(10, '0')}-{recipientAccountId.ToString().PadLeft(10, '0')}";
	}

	public class PaymentGroupViewModel
	{
		public PaymentGroupViewModel(AccountModel payerAccount, AccountModel recipientAccount, int totalPayments, double totalSum, int level)
		{
			PayerAccount = payerAccount;
			RecipientAccount = recipientAccount;
			TotalPayments = totalPayments;
			TotalSum = totalSum;
			Level = level;
		}

		public AccountModel PayerAccount { get; }
		public AccountModel RecipientAccount { get; }
		public int TotalPayments { get; }
		public double TotalSum { get; }
		public int Level { get; }
	}

	public class PaymentModel
	{
		public int PaymentId { get; }
		public int PaymentGroupId { get; }
		public DateTime Date { get; }
		public string Number { get; }
		public double Amount { get; }
		public string ContractNumber { get; }
		public string Description { get; }

		public PaymentModel(int paymentId, int paymentGroupId, DateTime date, string number, double amount, string contractNumber, string description)
		{
			PaymentId = paymentId;
			PaymentGroupId = paymentGroupId;
			Date = date;
			Number = number;
			Amount = amount;
			ContractNumber = contractNumber;
			Description = description;
		}
	}
	public class PaymentViewModel
	{
		public PaymentModel Payment { get; }
		public int Level { get; }
		public PaymentViewModel(PaymentModel payment, int level)
		{
			Level = level;
			Payment = payment;
		}
	}
}
