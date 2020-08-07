using System;
namespace ci_project.Models
{
    public class PurchaseHistoryModel
    {
        public int id { get; set; }
        public int bookId { get; set; }
        public int userId { get; set; }
        public double transactionAmount { get; set; }
        public DateTime transactionDate { get; set; }

        public PurchaseHistoryModel()
        {
        }
    }
}
