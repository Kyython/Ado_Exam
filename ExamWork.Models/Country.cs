using System;

namespace ExamWork.Models
{
    public class Country : Entity
    {
        public string Name { get; set; }

        public City City { get; set; }

        public Guid CityId { get; set; }
    }
}
