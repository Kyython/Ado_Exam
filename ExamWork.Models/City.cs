using System;

namespace ExamWork.Models
{
    public class City : Entity
    {
        public string Name { get; set; }

        public Street Street { get; set; }

        public Guid StreetId  { get; set; }
    }
}
