using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TamaguchiWebAPI.DTO
{
    public class PetStatsDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public int Happiness { get; set; }
        public int Hunger { get; set; }
        public int Hygiene { get; set; }
        public int Weight { get; set; }
        public string LifeCycle { get; set; }
        public string Health { get; set; }

    }
}
