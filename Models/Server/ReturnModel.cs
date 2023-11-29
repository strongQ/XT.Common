using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace XT.Common.Models.Server
{
    [DataContract]
    public class ReturnModel<T>
    {
        [DataMember(Order = 1)]
        public T Data { get; set; }
        [DataMember(Order = 2)]
        public bool Flag { get; set; }
        [DataMember(Order = 3)]
        public bool IsSuccess { get { return Flag; } }
        /// <summary>
        /// 消息
        /// </summary>
        [DataMember(Order = 4)]
        public string Msg { get; set; }
        [DataMember(Order = 5)]
        public int Code { get; set; }

    }
}
