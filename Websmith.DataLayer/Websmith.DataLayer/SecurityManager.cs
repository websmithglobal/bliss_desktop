using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Websmith.DataLayer
{
    public class SecurityManager
    {
        public long GetSerial()
        {
            long sum = 0;
            try
            {
                ManagementClass mos = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mos.GetInstances();

                string mac = String.Empty;
                foreach (ManagementObject mo in moc)
                {
                    if (Convert.ToBoolean(mo["IPEnabled"])) //Skip objects without a MACAddress
                    {
                        mac = Convert.ToString(mo["MacAddress"]);
                    }
                }
                mos.Dispose();
               
                int index = 1;
                foreach (char ch in mac)
                {
                    if (char.IsDigit(ch))
                    {
                        sum += sum + Convert.ToInt32(ch) * (index * 2);
                    }
                    else if (char.IsLetter(ch))
                    {
                        switch (ch.ToString().ToUpper())
                        {
                            case "A":
                                sum += sum + 10 * (index * 2);
                                break;
                            case "B":
                                sum += sum + 11 * (index * 2);
                                break;
                            case "C":
                                sum += sum + 12 * (index * 2);
                                break;
                            case "D":
                                sum += sum + 13 * (index * 2);
                                break;
                            case "E":
                                sum += sum + 14 * (index * 2);
                                break;
                            case "F":
                                sum += sum + 15 * (index * 2);
                                break;
                        }
                    }
                    index += 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sum;
        }

        public bool CheckKey(long key)
        {
            long x = GetSerial();
            long y = x * x + 53 / x + 113 * (x / 4);
            return y == key;
        }

        public long GenerateKey(long serial)
        {
            long x = serial;
            return x * x + 53 / x + 113 * (x / 4);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}

