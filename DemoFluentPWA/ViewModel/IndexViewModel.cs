using DemoFluentPWA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoFluentPWA.ViewModel
{
    public class IndexViewModel
    {
        public IEnumerable<ItemModel> items { get; set; }
    }
}
