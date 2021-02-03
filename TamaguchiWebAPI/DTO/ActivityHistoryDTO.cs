using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TamaguchiWebAPI.DTO
{
    public class ActivityHistoryDTO
    {
        public int Age { get; set; }
        public DateTime ActivityDate { get; set; }
        public int PetWeight { get; set; }
        public string ActivityName { get; set; }

    }
}
