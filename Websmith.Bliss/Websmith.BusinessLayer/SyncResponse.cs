using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Websmith.BusinessLayer
{
    public class SyncResponse
    {
        public static bool sendMessageAcknowledgement(string message, string ip)
        {
            bool response = false;
            try
            {
                response = true;
            }
            catch (Exception)
            {
                response = false;
            }
            return response;
        }
    }
}
