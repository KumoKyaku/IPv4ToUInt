using System;
using System.Text.RegularExpressions;

namespace ConvertAnIPv4Address
{
    public static class ConvertUtility
    {
        public static uint IPv4ToUInt(string address)
        {
            Regex regex = new Regex(@"^\b(( *\d{1,3} *(?!\d))(\.|$)){4}\b");
            if (!regex.IsMatch(address))
            {
                throw new FormatException();
            }

            ///do not use system string function.
            ///only iterate the string once.
            byte[] parts = new byte[4];
            int index = 0;
            for (int i = 0; i < address.Length; i++)
            {
                char c = address[i];
                switch (c)
                {
                    case ' ':
                        continue;
                    case '.':
                        index++;
                        break;
                    default:
                        int value = parts[index] * 10 + (c - '0');
                        if (value > byte.MaxValue)
                        {
                            throw new FormatException();
                        }
                        parts[index] = (byte)value;
                        break;
                }

                if (index > 3)
                {
                    throw new FormatException();
                }
            }

            ///convert into uint
            uint result;
            unsafe
            {
                byte* ptr = (byte*)&result;
                if (BitConverter.IsLittleEndian)
                {
                    ptr[0] = parts[3];
                    ptr[1] = parts[2];
                    ptr[2] = parts[1];
                    ptr[3] = parts[0];
                }
                else
                {
                    ptr[0] = parts[0];
                    ptr[1] = parts[1];
                    ptr[2] = parts[2];
                    ptr[3] = parts[3];
                }
            }

            return result;
        }
    }
}
