using ExamWork.DataAcces;
using ExamWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamWork
{
    public class Menu
    {
        public string CheckNullString(string text)
        {
            while (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Ошибка ввода!");
                Console.Write("Введите повторно - ");
                text = Console.ReadLine();
            }
            return text;
        }

        public void AddData()
        {
            Console.Clear();
            Console.WriteLine("Введите название страны - ");
            var countryName = CheckNullString(Console.ReadLine());

            Console.WriteLine($"Введите название города находящегося в стране {countryName} - ");
            var cityName = CheckNullString(Console.ReadLine());

            Console.WriteLine($"Введите название улицы находящейся в городе {cityName} - ");
            var streetName = CheckNullString(Console.ReadLine());

            var street = new Street { Name = streetName };
            var city = new City { Name = cityName, Street = street, StreetId = street.Id, };
            var country = new Country { Name = countryName, City = city, CityId = city.Id  };

            Console.Clear();
            using (var repository = new StreetRepository())
            {
                try
                {
                    repository.Add(street);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                    return;
                }
            }

            using (var repository = new CityRepository())
            {
                try
                {
                    repository.Add(city);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                    return;
                }
            }

            using (var repository = new CountryRepository())
            {
                try
                {
                    repository.Add(country);
                }
                catch (Exception exception)
                {
                    Console.Clear();
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                    return;
                }
            }

            Console.Clear();
            Console.WriteLine("Данные добавлены!");
            Console.WriteLine("Нажмите Enter чтобы продолжить!");
            Console.ReadKey();
        }

        public void DeleteCountry()
        {
            Console.Clear();
            Console.WriteLine("Введите страну которую хотите удалить - ");
            var cityName = CheckNullString(Console.ReadLine());

            using (var repository = new CountryRepository())
            {
                try
                {
                    var id = repository.GetCountry(cityName);
                    repository.Delete(id);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                    return;
                }
            }

            Console.WriteLine("Данные удалены!");
            Console.WriteLine("Нажмите Enter чтобы продолжить!");
            Console.ReadKey();
        }

        public void DeleteCity()
        {
            Console.Clear();
            Console.WriteLine("Введите город который хотите удалить - ");
            var cityName = CheckNullString(Console.ReadLine());

            using (var repository = new CityRepository())
            {
                try
                {
                    var id = repository.GetCity(cityName);
                    repository.Delete(id);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("Данные удалены!");
            Console.WriteLine("Нажмите Enter чтобы продолжить!");
            Console.ReadKey();
        }

        public void DeleteStreet()
        {
            Console.Clear();
            Console.WriteLine("Введите улицу которую хотите удалить - ");
            var streetName = CheckNullString(Console.ReadLine());

            using (var repository = new StreetRepository())
            {
                try
                {
                    var id = repository.GetStreet(streetName);
                    repository.Delete(id);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("Данные удалены!");
            Console.WriteLine("Нажмите Enter чтобы продолжить!");
            Console.ReadKey();
        }

        public void ShowData()
        {
            Console.Clear();
            Console.WriteLine("Введите название страны для вывода информации о ней - ");
            var countryName = CheckNullString(Console.ReadLine());

            List<Country> countries = new List<Country>();

            using (var repository = new CountryRepository())
            {
                try
                {
                    countries = repository.GetAll().ToList();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                    return;
                }
            }

            List<City> cities = new List<City>();

            using (var repository = new CityRepository())
            {
                try
                {
                    cities = repository.GetAll().ToList();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                    return;
                }
            }

            List<Street> streets = new List<Street>();

            using (var repository = new StreetRepository())
            {
                try
                {
                    streets = repository.GetAll().ToList();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Нажмите Enter чтобы продолжить!");
                    Console.ReadKey();
                    return;
                }
            }
            Console.Clear();
            foreach (var country in countries)
            {
                if (country.Name.Contains(countryName) && country.DeletedDate == null)
                {
                    Console.WriteLine($"Название страны {country.Name}");
                    foreach (var city in cities)
                    {
                        if (city.Id == country.CityId && city.DeletedDate == null)
                        {
                            Console.WriteLine($"Города находящиеся в стране {country.Name}");
                            Console.WriteLine(city.Name);
                            foreach (var street in streets)
                            {
                                if (street.Id == city.StreetId && street.DeletedDate == null)
                                {
                                    Console.WriteLine($"Улицы находящиеся в городе {city.Name}");
                                    Console.WriteLine(street.Name);
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Нажмите Enter чтобы продолжить!");
            Console.ReadKey();
        }


    }
}
