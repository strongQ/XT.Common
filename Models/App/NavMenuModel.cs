using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Models.App
{
    public class NavMenuModel
    {
        public string ID { get; set; }

        public string ParentID { get; set; }

        public string Icon { get; set; }

        public string Href { get; set; }

        public string Name { get; set; }


        public List<NavMenuModel> Children { get; set; }
    }
}
