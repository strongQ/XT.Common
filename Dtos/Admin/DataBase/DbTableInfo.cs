using Mapster.Adapters;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Admin.DataBase
{
    public class DbTableInfo
    {
        public string Name { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// Table View All
        /// </summary>
        public int DbObjectType { get; set; }
    }
}
