using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Models.Modbus
{
    /// <summary>
    /// Modbus地址
    /// </summary>
    public class ModbusAddress
    {
        /// <summary>
        /// 开始位置
        /// </summary>
        public string StartNumber { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public ushort StepNumber { get; set; }
        /// <summary>
        /// BIT位置
        /// </summary>
        public int BitLocation { get; set; }

        public override string ToString()
        {
            return string.Format("开始位置:{0},长度:{1},BIT位置:{2}", StartNumber, StepNumber, BitLocation);
        }

    }
}
