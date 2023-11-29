using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Themes
{
    public class AppTheme
    {
        public bool IsDark { get; set; }

        public LayoutPrpo LayoutPrpo { get; set; } = new LayoutPrpo();
    }

    public class LayoutPrpo
    {
        public int AppBarHeight = 48;
        public int FooterBarHeight = 36;
        public int PageTabsHeight = 36;
    }
}
