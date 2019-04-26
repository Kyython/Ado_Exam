using Dapper;
using ExamWork.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace ExamWork.DataAcces
{
    public class StreetRepository : IRepository<Street>
    {
        private DbConnection _connection;

        private Migration _migration;

        public StreetRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ExamApp"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            _migration = new Migration();
            _migration.CheckMigrations();
        }

        public void Add(Street item)
        {
            var sqlQuery = "insert into Streets (Id,CreationDate,DeletedDate,Name) values(@Id,@CreationDate,@DeletedDate,@Name)";
            var result = _connection.Execute(sqlQuery, item);

            if (result < 1)
            {
                throw new Exception("Запись не вставлена!");
            }
        }

        public void Delete(Guid id)
        {
            var sqlQuery = "update Streets set DeletedDate = GetDate() Where Id = @id";
            var result = _connection.Execute(sqlQuery, new { id });

            if (result < 1)
            {
                throw new Exception("Запись не удалена!");
            }
        }

        public ICollection<Street> GetAll()
        {
            var sqlQuery = "select *from Streets";
            return _connection.Query<Street>(sqlQuery).AsList();
        }

        public Guid GetStreet(string name)
        {
            var sqlQuery = "select Id from Streets where Name = @name";

            var result = _connection.QueryFirst<Guid>(sqlQuery, new { name });

            if (result == null)
            {
                throw new Exception("Запрос не выполнен!");
            }

            return result;
        }

        public void Update(Street item)
        {
            var sqlQuery = "update Streets set Name = @Name Where Id = @Id";
            var result = _connection.Execute(sqlQuery, item);

            if (result < 1)
            {
                throw new Exception("Запись не обновлена!");
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
