using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLChampSelector.Models
{
    public class ChampInfo
    {
        public string ID { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Icon { get; set; }

    }
}

