using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TamaguchiWebAPI.DTO
{
    public class ActivityHistoryDTO
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Age { get; set; }
        public string LifeCycle { get; set; }
        public string CategoryName { get; set; }

    }
}
