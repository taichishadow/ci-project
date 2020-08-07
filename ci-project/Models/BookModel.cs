using System;
namespace ci_project.Models
{
    public class BookModel
    {
        public int id { get; set; }
        public int bookStoreId { get; set; }
        public string bookName { get; set; }
        public double price { get; set; }

        public BookModel()
        {
        }
    }
}
