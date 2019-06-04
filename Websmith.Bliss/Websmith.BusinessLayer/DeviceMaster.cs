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
                using (DAL.DeviceMaster objDAL = new DAL.DeviceMaster())
                {
                    _ = objDAL.InsertUpdateDeleteDeviceMaster(objENT: new ENT.DeviceMaster
                    {
                        DeviceIP = ipAddress,
                        DeviceStatus = status,
                        Mode = "STATUS"
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
