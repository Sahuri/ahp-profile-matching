using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahp.Core.Models
{
    public class MenuItem
    {
        public string kode { get; set; }
        public string parent { get; set; }
        public string title { get; set; }
        public string href { get; set; }
        public string sref { get; set; }
        public string icon { get; set; }
        public Decimal? Index { get; set; }
        public List<MenuItem> items { get; set; }

        public MenuItem()
        {
            this.href = "#";
        }

    }

    public class RecursiveMenu
    {
        public String Kode { get; set; }
        public String Title { get; set; }
        public String TitleWithPath { get; set; }
        public String Url { get; set; }
        public String Icon { get; set; }
        public String Class { get; set; }
        public String Parent { get; set; }
        public Int32? Sequence { get; set; }
        public Int32? Level { get; set; }
        public Decimal? Index { get; set; }
    }

}
