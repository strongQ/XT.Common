using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Common.Extensions
{
    public static class ByteExtension
    {
        /// <summary>
        /// ushort[] 转 byte[]
        /// </summary>
        /// <param name="src"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        public static byte[] Ushorts2Bytes(this ushort[] src, bool reverse = false)
        {

            int count = src.Length;
            byte[] dest = new byte[count << 1];
            if (reverse)
            {
                for (int i = 0; i < count; i++)
                {
                    dest[i * 2] = (byte)(src[i] >> 8);
                    dest[i * 2 + 1] = (byte)(src[i] >> 0);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    dest[i * 2] = (byte)(src[i] >> 0);
                    dest[i * 2 + 1] = (byte)(src[i] >> 8);
                }
            }
            return dest;
        }
        /// <summary>
        /// byte[] 转 ushort[]
        /// </summary>
        /// <param name="src"></param>
        /// <param name="reverse"></param>
        /// <returns></returns>
        public static ushort[] Bytes2Ushorts(this byte[] src, bool reverse = false)
        {
            int len = src.Length;

            byte[] srcPlus = new byte[len + 1];
            src.CopyTo(srcPlus, 0);
            int count = len >> 1;

            if (len % 2 != 0)
            {
                count += 1;
            }

            ushort[] dest = new ushort[count];
            if (reverse)
            {
                for (int i = 0; i < count; i++)
                {
                    dest[i] = (ushort)(srcPlus[i * 2] << 8 | srcPlus[2 * i + 1] & 0xff);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    dest[i] = (ushort)(srcPlus[i * 2] & 0xff | srcPlus[2 * i + 1] << 8);
                }
            }

            return dest;
        }


        /// <summary>
        /// 设置某个偏移位置的BIT值
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static byte[] SetBit(this byte[] bytes, int offset, bool flag)
        {
            if (bytes.Length != 2)
            {
                return bytes;
            }
            if (offset <= 8)
            {
                bytes[0] = bytes[0].SetBoolByIndex(offset, flag);
            }
            else
            {
                bytes[1] = bytes[1].SetBoolByIndex(offset - 8, flag);
            }
            return bytes;
        }

        public static byte SetBoolByIndex(this byte data, int index, bool flag)
        {
            if (index > 8 || index < 1) throw new ArgumentOutOfRangeException();
            int v = index < 2 ? index : 2 << index - 2;
            return flag ? (byte)(data | v) : (byte)(data & ~v);
        }

        /// <summary>
        /// 根据偏移获取字节数组 偏移位置的BIT值
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static bool GetBit(this byte[] bytes, int offset)
        {
            if (bytes.Length != 2)
            {
                return false;
            }

            if (offset <= 8)
            {
                return bytes[0].GetBoolByIndex(offset);
            }
            else
            {
                return bytes[1].GetBoolByIndex(offset - 8);
            }
        }
        public static bool GetBoolByIndex(this byte data, int index)
        {
            byte x = 1;
            switch (index)
            {
                case 1: { x = 0x01; } break;
                case 2: { x = 0x02; } break;
                case 3: { x = 0x04; } break;
                case 4: { x = 0x08; } break;
                case 5: { x = 0x10; } break;
                case 6: { x = 0x20; } break;
                case 7: { x = 0x40; } break;
                case 8: { x = 0x80; } break;
                default: { return false; }
            }
            return (data & x) == x ? true : false;
        }


        /// <summary>
        /// 查询字符串出现的字数
        /// </summary>
        /// <param name="source"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static int GetCount(this string source, string search)
        {
            int count = 0; //计数器
            for (int i = 0; i <=source.Length - search.Length; i++)
            {
                if (source.Substring(i, search.Length) == search)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 将byte[]转成二进制字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetBitString(this byte[] data)
        {
            StringBuilder result = new StringBuilder(data.Length * 8);
            foreach (byte b in data)
            {
                result.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return result.ToString();
        }

        /// <summary>
        /// 字符串转成byte,指定byte长度
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this string data, int len)
        {
            byte[] lst = new byte[len];
            byte[] bytes = Encoding.ASCII.GetBytes(data);
            if (bytes.Length >= len)
            {
                for (int i = 0; i < len; i++)
                {
                    lst[i] = bytes[i];
                }
            }
            else
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    lst[i] = bytes[i];
                }
            }
            return lst;
        }

        /// <summary>
        /// 将byte[]转成字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetString(this byte[] data)
        {
            return Encoding.ASCII.GetString(data);
        }
    }
}
