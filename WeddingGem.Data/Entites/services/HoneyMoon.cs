using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGem.Data.Entites.services
{
    public class HoneyMoon:Items
    {
        public string Destination { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Inclusions { get; set; }

    }
}
