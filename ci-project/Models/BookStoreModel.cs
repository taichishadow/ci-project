using System;
namespace ci_project.Models
{
    public class BookStoreModel
    {
        public int id { get; set; }
        public double cashBalance { get; set; }
        public string openingHours { get; set; }
        public string storeName { get; set; }

        public BookStoreModel()
        {
        }
    }
}
