using Dapper;
using ExamWork.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace ExamWork.DataAcces
{
    public class CountryRepository : IRepository<Country>
    {
        private DbConnection _connection;

        private Migration _migration;

        public CountryRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ExamApp"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            _migration = new Migration();
            _migration.CheckMigrations();
        }

        public void Add(Country item)
        {
            var sqlQuery = "insert into Countries (Id,CreationDate,DeletedDate,Name,CityId) values(@Id,@CreationDate,@DeletedDate,@Name,@CityId)";
            var result = _connection.Execute(sqlQuery, item);

            if (result < 1)
            {
                throw new Exception("Запись не вставлена!");
            }
        }

        public void Delete(Guid id)
        {
            var sqlQuery = "update Countries set DeletedDate = GetDate() Where Id = @id";
            var result = _connection.Execute(sqlQuery, new { id });

            if (result < 1)
            {
                throw new Exception("Запись не удалена!");
            }
        }

        public ICollection<Country> GetAll()
        {
            var sqlQuery = "select *from Countries";
            return _connection.Query<Country>(sqlQuery).AsList();
        }

        public Guid GetCountry(string name)
        {
            var sqlQuery = "select Id from Countries where Name = @name";

            var result = _connection.QueryFirst<Guid>(sqlQuery, new { name });

            if (result == null)
            {
                throw new Exception("Запрос не выполнен!");
            }

            return result;
        }

        public void Update(Country item)
        {
            var sqlQuery = "update Countries set Name = @Name, CityId = @CityId Where Id = @Id";
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
