using Dapper;
using ExamWork.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace ExamWork.DataAcces
{
    public class CityRepository : IRepository<City>
    {
        private DbConnection _connection;

        private Migration _migration;

        public CityRepository()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ExamApp"].ConnectionString;
            _connection = new SqlConnection(connectionString);
            _migration = new Migration();
            _migration.CheckMigrations();
        }

        public void Add(City item)
        {
            var sqlQuery = "insert into Cities (Id,CreationDate,DeletedDate,Name,StreetId) values(@Id,@CreationDate,@DeletedDate,@Name,@StreetId)";
            var result = _connection.Execute(sqlQuery, item);

            if (result < 1)
            {
                throw new Exception("Запись не вставлена!");
            }
        }

        public void Delete(Guid id)
        {
            var sqlQuery = "update Cities set DeletedDate = GetDate() Where Id = @id";
            var result = _connection.Execute(sqlQuery, new { id });

            if (result < 1)
            {
                throw new Exception("Запись не удалена!");
            }
        }

        public ICollection<City> GetAll()
        {
            var sqlQuery = "select *from Cities";
            return _connection.Query<City>(sqlQuery).AsList();
        }

        public Guid GetCity(string name)
        {
            var sqlQuery = "select Id from Cities where Name = @name";

            var result = _connection.QueryFirst<Guid>(sqlQuery, new { name });

            if (result == null)
            {
                throw new Exception("Вход не выполнен!");
            }

            return result;
        }

        public void Update(City item)
        {
            var sqlQuery = "update Cities set Name = @Name, StreetId = @StreetId Where Id = @Id";
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
