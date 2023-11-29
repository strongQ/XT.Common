
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XT.Common.Attributes;
using XT.Common.Dtos.Admin.Org;
using XT.Common.Dtos.Admin.Pos;
using XT.Common.Dtos.Admin.Util;
using XT.Common.Enums;

namespace XT.Common.Dtos.Admin.User
{
    public class UserInput : BaseIdInput
    {
        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum Status { get; set; }
    }

    public class PageUserInput : BasePageInput
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 查询时所选机构Id
        /// </summary>
        public long OrgId { get; set; }
    }

    public class AddUserInput : BaseDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空")]
        [HeadDescription("账号")]
        public string Account { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required(ErrorMessage = "真实姓名不能为空")]
        [HeadDescription("真实姓名")]
        public string RealName { get; set; }



        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(512)]
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual string Password { get; set; }



        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(32)]
        [HeadDescription("昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(512)]
        [HeadDescription("头像")]
        public string Avatar { get; set; }

        /// <summary>
        /// 性别-男_1、女_2
        /// </summary>
        [HeadDescription("性别")]
        public GenderEnum Sex { get; set; } = GenderEnum.Male;

        /// <summary>
        /// 年龄
        /// </summary>
        [HeadDescription("年龄")]
        public int Age { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [HeadDescription("出生日期")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [MaxLength(32)]
        public string Nation { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(16)]
        [HeadDescription("手机号码")]
        public string Phone { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public CardTypeEnum CardType { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [MaxLength(32)]
        public string IdCardNum { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(64)]
        public string Email { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [MaxLength(256)]
        [HeadDescription("地址")]
        public string Address { get; set; }

        /// <summary>
        /// 文化程度
        /// </summary>
        [Description("文化程度")]
        public CultureLevelEnum CultureLevel { get; set; }

        /// <summary>
        /// 政治面貌
        /// </summary>
        [MaxLength(16)]
        public string PoliticalOutlook { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>COLLEGE
        [MaxLength(128)]
        public string College { get; set; }

        /// <summary>
        /// 办公电话
        /// </summary>
        [MaxLength(16)]
        public string OfficePhone { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        [MaxLength(32)]
        public string EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        [MaxLength(16)]
        public string EmergencyPhone { get; set; }

        /// <summary>
        /// 紧急联系人地址
        /// </summary>
        [MaxLength(256)]
        public string EmergencyAddress { get; set; }

        /// <summary>
        /// 个人简介
        /// </summary>
        [MaxLength(512)]
        public string Introduction { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderNo { get; set; } = 100;

        /// <summary>
        /// 状态
        /// </summary>
        [HeadDescription("状态")]
        public StatusEnum Status { get; set; } = StatusEnum.Enable;

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(128)]
        public string Remark { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public AccountTypeEnum AccountType { get; set; } = AccountTypeEnum.None;

        /// <summary>
        /// 机构Id
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 机构
        /// </summary>    
        public AddOrgInput SysOrg { get; set; }

        /// <summary>
        /// 职位Id
        /// </summary>
        public long PosId { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public AddPosInput SysPos { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        [MaxLength(32)]
        [Description("工号")]
        public string JobNum { get; set; }

        /// <summary>
        /// 职级
        /// </summary>
        [MaxLength(32)]
        public string PosLevel { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// 最新登录Ip
        /// </summary>
        [MaxLength(256)]
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 最新登录地点
        /// </summary>
        [MaxLength(128)]
        public string LastLoginAddress { get; set; }

        /// <summary>
        /// 最新登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最新登录设备
        /// </summary>
        [MaxLength(128)]
        public string LastLoginDevice { get; set; }

        /// <summary>
        /// 电子签名
        /// </summary>
        [MaxLength(512)]
        public string Signature { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public List<long> RoleIdList { get; set; }

        /// <summary>
        /// 扩展机构集合
        /// </summary>
        public List<SysUserExtOrg> ExtOrgIdList { get; set; } = new List<SysUserExtOrg>();
    }

    public class UpdateUserInput : AddUserInput
    {
    }

    public class DeleteUserInput : BaseIdInput
    {
        /// <summary>
        /// 机构Id
        /// </summary>
        public long OrgId { get; set; }
    }

    public class ResetPwdUserInput : BaseIdInput
    {
    }

    public class ChangePwdInput
    {
        /// <summary>
        /// 当前密码
        /// </summary>
        [Required(ErrorMessage = "当前密码不能为空")]
        public string PasswordOld { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不能为空")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "密码需要大于5个字符")]
        public string PasswordNew { get; set; }
    }
}
