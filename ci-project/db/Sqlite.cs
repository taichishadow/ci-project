using System;
using Microsoft.Extensions.Configuration;

namespace ci_project.db
{
    public class Sqlite
    {
        public static string obtainConnectionString()
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: false)
                            .Build();
            string connectionString = config.GetValue<string>("ConnectionString:Sqlite");

            return connectionString;
        }
    }
}
