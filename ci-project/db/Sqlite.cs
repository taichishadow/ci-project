using System;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Logging;

using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;

using Microsoft.Extensions.DependencyInjection;

using ci_project.db.migration;

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

        public static void init()
        {
            string connectionString = obtainConnectionString();

            var serviceProvider = obtainServiceProvider(connectionString);
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider obtainServiceProvider(string connectionString)
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddSQLite()
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(CreateBookStoreTable).Assembly).For.Migrations()
                    .ScanIn(typeof(CreateBookTable).Assembly).For.Migrations()
                    .ScanIn(typeof(CreatePurchaseHistoryTable).Assembly).For.Migrations()
                    .ScanIn(typeof(CreateUserTable).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }
    }
}
