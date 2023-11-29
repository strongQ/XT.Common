using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Dtos.Admin.Util;

namespace XT.Common.Dtos.Admin.Region
{
    public class PageRegionInput : BasePageInput
    {
        /// <summary>
        /// 父节点Id
        /// </summary>
        public long Pid { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }

    public class RegionInput : BaseIdInput
    {
    }

    public class AddRegionInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public long Pid { get; set; }



        /// <summary>
        /// 简称
        /// </summary>
        [MaxLength(32)]
        public string ShortName { get; set; }

        /// <summary>
        /// 组合名
        /// </summary>
        [MaxLength(64)]
        public string MergerName { get; set; }

        /// <summary>
        /// 行政代码
        /// </summary>
        [MaxLength(32)]
        public string Code { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        [MaxLength(6)]
        public string ZipCode { get; set; }

        /// <summary>
        /// 区号
        /// </summary>
        [MaxLength(6)]
        public string CityCode { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        [MaxLength(128)]
        public string PinYin { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public float Lng { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public float Lat { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public string Remark { get; set; }

        /// <summary>
        /// 机构子项
        /// </summary>
        public List<AddRegionInput> Children { get; set; }
    }

    public class UpdateRegionInput : AddRegionInput
    {
    }

    public class DeleteRegionInput : BaseIdInput
    {
    }
}
