using System;
using Dapper;
using System.Data.SQLite;
using System.Collections.Generic;
using ci_project.Models;
using ci_project.db;

namespace ci_project.DAO
{
    public class UserDAO
    {
        static string connectionString;

        public UserDAO()
        {
            connectionString = Sqlite.obtainConnectionString();
        }

        public List<UserModel> obtainAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            string sqlQuery = @"select * from users;";

            using (var cn = new SQLiteConnection(connectionString))
            {
                users = cn.Query<UserModel>(sqlQuery).AsList();
            }
            return users;
        }

        public List<UserModel> obtainUsersByParams(DynamicParameters sqlParams)
        {
            List<UserModel> users = new List<UserModel>();
            string sqlQuery = @"select * from users where 1=1";

            if(sqlParams.Get<string>("name") != null)
            {
                sqlQuery += " and name = @name";
            }

            if (sqlParams.Get<double>("cashBalance") !< 0)
            {
                sqlQuery += " and cashBalance = @cashBalance";
            }

            using (var cn = new SQLiteConnection(connectionString))
            {
                users = cn.Query<UserModel>(sqlQuery, sqlParams).AsList();
            }
            return users;
        }

        public void insert(UserModel user)
        {
            string sqlQuery = @"insert into users (name, cashBalance) values (@name, @cashBalance);";

            using (var cn = new SQLiteConnection(connectionString))
            {
                cn.Execute(sqlQuery, user);
            }
        }

        public void update(UserModel user)
        {
            string sqlQuery = @"update users set name = @name, cashBalance = @cashBalance where id = @id;";

            using (var cn = new SQLiteConnection(connectionString))
            {
                cn.Execute(sqlQuery, user);
            }
        }

        public void delete(UserModel user)
        {
            string sqlQuery = @"delete from users where id = @id;";

            using (var cn = new SQLiteConnection(connectionString))
            {
                cn.Execute(sqlQuery, user);
            }
        }
    }
}
