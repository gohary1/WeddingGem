using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Repository.Params;

namespace WeddingGem.Repository.Parames
{
    public class WeddingSpecsParams:BaseProductParams
    {
        public string? Capacity { get; set; }

        public string? HallType { get; set; }
    }
}
