using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Repository.Params;
using WeddingGem.Repository.Specifications;

namespace WeddingGem.Repository.Parames
{
    public class HoneyMoonSepcsParam:BaseProductParams
    {
        public string? Distination { get; set; }
    }
}
