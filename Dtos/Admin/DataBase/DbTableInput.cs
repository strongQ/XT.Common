﻿using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Dtos.Admin.DataBase
{
    public class DbTableInput
    {
        public string ConfigId { get; set; }

        public string TableName { get; set; }

        public string Description { get; set; }

        public List<DbColumnInput> DbColumnInfoList { get; set; }
    }

    public class UpdateDbTableInput
    {
        public string ConfigId { get; set; }

        public string TableName { get; set; }

        public string OldTableName { get; set; }

        public string Description { get; set; }
    }

    public class DeleteDbTableInput
    {
        public string ConfigId { get; set; }

        public string TableName { get; set; }
    }
}
