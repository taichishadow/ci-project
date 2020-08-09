using System;
using Dapper;
using System.Data.SQLite;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using ci_project.Models;

namespace ci_project.DAO
{
    public class UserDAO
    {
        static string connectionString;

        public UserDAO(IConfiguration config)
        {
            connectionString = config.GetValue<string>("ConnectionString:Sqlite");
        }

        public List<UserModel> obtainAllUsers()
        {
            List<UserModel> users = new List<UserModel>();
            string sqlQuery = @"select * from user;";

            using (var cn = new SQLiteConnection(connectionString))
            {
                users = cn.Query<UserModel>(sqlQuery).AsList();
            }
            return users;
        }

        public List<UserModel> obtainUsersByParams(DynamicParameters sqlParams)
        {
            List<UserModel> users = new List<UserModel>();
            string sqlQuery = @"select * from user where 1=1;";

            if(sqlParams.Get<string>("name") != null)
            {
                sqlQuery += "and name = @name";
            }

            if (sqlParams.Get<double>("cashBalance") !< 0)
            {
                sqlQuery += "and cashBalance = @cashBalance";
            }

            using (var cn = new SQLiteConnection(connectionString))
            {
                users = cn.Query<UserModel>(sqlQuery, sqlParams).AsList();
            }
            return users;
        }

        public void insert(UserDAO user)
        {
            string sqlQuery = @"insert into user (name, cashBalance) values (@name, @cashBalance);";

            using (var cn = new SQLiteConnection(connectionString))
            {
                cn.Execute(sqlQuery, user);
            }
        }
    }
}
