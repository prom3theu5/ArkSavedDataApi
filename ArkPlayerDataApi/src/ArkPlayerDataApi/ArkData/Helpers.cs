using System;
using System.Text;

namespace ArkData
{
    internal class Helpers
    {
        public static int GetInt(byte[] data, string name)
        {
            byte[] bytes1 = Encoding.UTF8.GetBytes(name);
            byte[] bytes2 = Encoding.UTF8.GetBytes("IntProperty");

            int offset = data.LocateFirst(bytes1, 0);
            int num = data.LocateFirst(bytes2, offset);

            if (num > -1)
                return BitConverter.ToInt32(data, num + bytes2.Length + 9);
            return -1;
        }

        public static ushort GetUInt16(byte[] data, string name)
        {
            byte[] bytes1 = Encoding.UTF8.GetBytes(name);
            byte[] bytes2 = Encoding.UTF8.GetBytes("UInt16Property");

            int offset = data.LocateFirst(bytes1, 0);
            int num = data.LocateFirst(bytes2, offset);

            if (num >= 0)
                return BitConverter.ToUInt16(data, num + bytes2.Length + 9);
            return 0;
        }

        public static string GetString(byte[] data, string name)
        {
            byte[] bytes1 = Encoding.UTF8.GetBytes(name);
            byte[] bytes2 = Encoding.UTF8.GetBytes("StrProperty");
            int offset = data.LocateFirst(bytes1, 0);
            int num = data.LocateFirst(bytes2, offset);

            if (num < 0)
                return string.Empty;

            byte[] numArray = new byte[1];

            Array.Copy(data, num + bytes2.Length + 1, numArray, 0, 1);
 
            int length = numArray[0] - (data[num + bytes2.Length + 12] == byte.MaxValue ? 6 : 5);

            byte[] bytes3 = new byte[length];
            Array.Copy(data, num + bytes2.Length + 13, bytes3, 0, length);

            if (data[num + bytes2.Length + 12] == byte.MaxValue)
                return Encoding.Unicode.GetString(bytes3);

            return Encoding.UTF8.GetString(bytes3);
        }
    }
}
