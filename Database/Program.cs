using System;
using System.Configuration;
using System.Reflection;
using DbUp;
using DbUp.Engine;

namespace Expresso.Database
{
    class Program
    {
        static DatabaseUpgradeResult migrationResult;

        static int Main(string[] args)
        {
            ApplyMigrations();
            PrintResult();
            return StatusCode();
        }

        private static void ApplyMigrations()
        {
            var upgrader = DeployChanges
                .To
                .SqlDatabase(GetConnectionString())
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();
            migrationResult = upgrader.PerformUpgrade();
        }

        static void PrintResult()
        {
            if (MigrationFailed())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(migrationResult.Error);
                Console.WriteLine("Migration failed.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Migration successful.");
                Console.ResetColor();
            }
        }

        static int StatusCode()
        {
            return MigrationFailed() ? -1 : 0;
        }

        static string GetConnectionString()
        {
            return ConfigurationManager
                .ConnectionStrings["Expresso"]
                .ConnectionString;
        }

        static bool MigrationFailed()
        {
            return !migrationResult.Successful;
        }
    }
}
