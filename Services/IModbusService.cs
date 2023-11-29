using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XT.Common.Services
{
    public interface IModbusService
    {
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <param name="station"></param>
        /// <returns></returns>
        Task<bool> Connect(string ip, int port = 502, int station = 1);

        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        Task<bool> Stop();

        /// <summary>
        /// 写modbus数据
        /// </summary>
        /// <param name="selectType">类型Uint16、String</param>
        /// <param name="writeAddr">地址</param>
        /// <param name="writeStep">步长</param>
        /// <param name="selectBit">位</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        Task<bool> WriteData(string selectType, int writeAddr, int writeStep, int selectBit, string data);
        /// <summary>
        /// 读modbus数据
        /// </summary>
        /// <param name="selectType">类型Uint16、String</param>
        /// <param name="writeAddr">地址</param>
        /// <param name="writeStep">步长</param>
        /// <param name="selectBit">位</param>
        /// <param name="selectDataType">数据格式 二进制、十进制</param>
        /// <returns></returns>
        Task<string> ReadData(string selectType, int writeAddr, int writeStep, int selectBit, string selectDataType);
    }
}
