using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using ENT = Websmith.Entity;

namespace Websmith.DataLayer
{
    public class UpStream
    {
        public static DataSet UpStreamData(string APIName, object objModel)
        {
            string responseFromServer = "";
            DataSet ds = new DataSet("JsonData");
            try
            {
                //WebRequest tRequest = WebRequest.Create("http://api.possoftwareindia.com/api/" + APIName);
                WebRequest tRequest = WebRequest.Create("http://blissapi.appsmith.co.in/api/" + APIName);
                tRequest.Method = "POST";
                tRequest.ContentType = "application/json";

                string jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objModel);

                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);

                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                responseFromServer = tReader.ReadToEnd();
                                XmlDocument xml = JsonConvert.DeserializeXmlNode(responseFromServer, "RootObject");
                                ENT.UpStreamResponse objResponse = JsonConvert.DeserializeObject<ENT.UpStreamResponse>(responseFromServer);
                                ds = new DataSet("JsonData");
                                XmlReader xr = new XmlNodeReader(xml);
                                ds.ReadXml(xr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ds = new DataSet();
                throw ex;
            }
            return ds;
        }
    }
}
