using DbUp;
using System;
using System.Configuration;
using System.Reflection;

namespace ExamWork.DataAcces
{
    public class Migration
    {
        private readonly string _connectionString;

        public Migration()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ExamApp"].ConnectionString;
        }

        public void CheckMigrations()
        {
            EnsureDatabase.For.SqlDatabase(_connectionString);

            var upgrader = DeployChanges.To
           .SqlDatabase(_connectionString)
           .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
           .LogToConsole()
           .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                throw new Exception("Ошибка в базе данных");
            }
        }
    }
}
