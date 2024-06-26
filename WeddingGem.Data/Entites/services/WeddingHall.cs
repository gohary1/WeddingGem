using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGem.Data.Entites.services
{
    public class WeddingHall:Items
    {
        public int Capacity { get; set; }
        public string Location { get; set; }

        public string HallType { get; set; }
        public string? AvlDateFrom { get; set; }

    }
}
