using LOLChampSelector.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLChampSelector.ViewModels
{
    public class ViewModel
    {
        public List<ChampInfo> _champs;
        public string ChampID { get; set; }
        public string ChampName { get; set; }

        public IEnumerable<SelectListItem> ChampListItems
        {
            get { return new SelectList(_champs, "ChampID", "ChampName"); }
        }
    }
}
