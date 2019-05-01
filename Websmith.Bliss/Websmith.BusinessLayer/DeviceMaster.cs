using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENT = Websmith.Entity;
using DAL = Websmith.DataLayer;

namespace Websmith.BusinessLayer
{
    public class DeviceMaster
    {
        public DeviceMaster()
        { }

        public void ChangeStatus(string ipAddress, int status)
        {
            try
            {
                ENT.DeviceMaster objENT = new ENT.DeviceMaster();
                objENT.DeviceIP = ipAddress.Trim();
                objENT.DeviceStatus = status;
                objENT.Mode = "STATUS";
                using (DAL.DeviceMaster objDAL = new DAL.DeviceMaster())
                {
                    objDAL.InsertUpdateDeleteDeviceMaster(objENT);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
