using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGem.Repository.Params
{
    public class BaseProductParams
    {
        public string? search { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderByDesc { get; set; }
    }
}
