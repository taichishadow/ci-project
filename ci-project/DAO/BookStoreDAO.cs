using System;
using Dapper;
using System.Data.SQLite;
using System.Collections.Generic;
using ci_project.Models;
using ci_project.db;
using System.Linq;

namespace ci_project.DAO
{
    public class BookStoreDAO
    {
        static string connectionString;

        public BookStoreDAO()
        {
            connectionString = Sqlite.obtainConnectionString();
        }

        public List<BookStoreModel> obtainAllBookStores()
        {
            return obtainAllBookStoresBySql();
        }

        public List<BookStoreModel> obtainBookStoreAtCertainDateTime()
        {
            List<BookStoreModel> bookStores = obtainAllBookStoresBySql();

            var dateTimes = from linq in bookStores
                            select new { linq.id, linq.openingHours };

            return bookStores;
        }

        public List<BookStoreModel> obtainBookStoresByParams(DynamicParameters sqlParams)
        {
            List<BookStoreModel> bookStores = new List<BookStoreModel>();
            string sqlQuery = @"select * from book_stores where 1=1;";

            if (sqlParams.Get<string>("storeName") != null)
            {
                sqlQuery += "and storeName = @storeName";
            }

            if (sqlParams.Get<double>("cashBalance")! < 0)
            {
                sqlQuery += "and cashBalance = @cashBalance";
            }

            using (var cn = new SQLiteConnection(connectionString))
            {
                bookStores = cn.Query<BookStoreModel>(sqlQuery, sqlParams).AsList();
            }
            return bookStores;
        }

        public void insert(BookStoreModel bookStore)
        {
            string sqlQuery = @"insert into book_stores (storeName, cashBalance, openingHours) values (@storeName, @cashBalance, @openingHours);";

            using (var cn = new SQLiteConnection(connectionString))
            {
                cn.Execute(sqlQuery, bookStore);
            }
        }

        private List<BookStoreModel> obtainAllBookStoresBySql()
        {
            string sqlQuery = @"select * from book_stores;";
            List<BookStoreModel> bookStores = new List<BookStoreModel>();

            using (var cn = new SQLiteConnection(connectionString))
            {
                bookStores = cn.Query<BookStoreModel>(sqlQuery).AsList();
            }
            return bookStores;
        }
    }
}
