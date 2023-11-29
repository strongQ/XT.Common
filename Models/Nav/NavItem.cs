using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Models.Nav
{
    public class NavItem : IDefaultItem<NavItem>
    {
        public NavItem()
        {
        }

        public NavItem(string title)
        {
            Title = title;
        }

        public List<NavItem> Children { get; set; }
        public bool Divider { get; set; }
        public string Group { get; set; }
        public string Heading { get; set; }
        public string Href { get; set; }
        public string Icon { get; set; }
        public string Segment => Group ?? Title;
        public string State { get; set; }
        public string SubTitle { get; set; }
        public string Target { get; set; }
        public string Title { get; set; }

        public string Value { get; set; }

        public long ID { get; set; }

        public long ParentID { get; set; }
    }
}
