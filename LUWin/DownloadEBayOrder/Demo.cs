using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;

namespace DownloadEBayOrder
{
    public partial class Demo : Form
    {
        static string _storePath = Application.StartupPath + "\\eBayOrders";
        public Demo()
        {
            InitializeComponent();
            this.Shown += new EventHandler(Demo_Shown);
        }

        void Demo_Shown(object sender, EventArgs e)
        {
            GeteEbayDetail();
            MessageBox.Show("OK");
        }


        string GeteEbayDetail()
        {
            #region settings
            string devID = Config.devID;
            string appID = Config.appID;
            string certID = Config.certID;

            //Get the Server to use (Sandbox or Production)
            string serverUrl = Config.serverUrl;

            //Get the User Token to Use
            string userToken = Config.userToken;

            //SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
            //SiteID Indicates the eBay site to associate the call with
            int siteID = Config.siteID;
            #endregion


            #region Load The XML Document Template and Set the Neccessary Values
            //Load the XML Document to Use for this Request
            XmlDocument xmlDoc = new XmlDocument();

            ////Get XML Document from Embedded  Resources
            //xmlDoc.Load((Server.MapPath("/q_admin/ebayMaster/Online/Xml/GeteBayOfficialTimeRequest.xml")));

            ////Set the various node values   attr1858_26443
            //xmlDoc["GeteBayOfficialTimeRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;

            string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<GetSellerTransactionsRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <IncludedContainingOrder>{1}</IncludedContainingOrder>
    <DetailLevel>ReturnAll</DetailLevel>
    <NumberOfDays>10</NumberOfDays>
</GetSellerTransactionsRequest>
", userToken, "True");

            xmlDoc.LoadXml(sendXml);
            //Get XML into a string for use in encoding
            string xmlText = xmlDoc.InnerXml;
            //eBay.Service.Call.AddItemCall aic = new eBay.Service.Call.AddItemCall();


            //Put the data into a UTF8 encoded  byte array
            UTF8Encoding encoding = new UTF8Encoding();
            int dataLen = encoding.GetByteCount(xmlText);
            byte[] utf8Bytes = new byte[dataLen];
            Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
            #endregion


            #region Setup The Request (inc. HTTP Headers
            //Create a new HttpWebRequest object for the ServerUrl
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

            //Set Request Method (POST) and Content Type (text/xml)
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = utf8Bytes.Length;

            //Add the Keys to the HTTP Headers
            request.Headers.Add("X-EBAY-API-DEV-NAME: " + devID);
            request.Headers.Add("X-EBAY-API-APP-NAME: " + appID);
            request.Headers.Add("X-EBAY-API-CERT-NAME: " + certID);

            //Add Compatability Level to HTTP Headers
            //Regulates versioning of the XML interface for the API
            request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 783");

            //Add function name, SiteID and Detail Level to HTTP Headers
            request.Headers.Add("X-EBAY-API-CALL-NAME:GetSellerTransactions");
            request.Headers.Add("X-EBAY-API-SITEID: " + siteID.ToString());

            //Time out = 15 seconds,  set to -1 for no timeout.
            //If times-out - throws a WebException with the
            //Status property set to WebExceptionStatus.Timeout.
            request.Timeout = 150000;

            #endregion


            #region Send The Request
            Stream str = null;
            try
            {
                //Set the request Stream
                str = request.GetRequestStream();
                //Write the equest to the Request Steam
                str.Write(utf8Bytes, 0, utf8Bytes.Length);
                str.Close();
                //Get response into stream
                WebResponse resp = request.GetResponse();
                str = resp.GetResponseStream();
            }
            catch (WebException wEx)
            {
                //Error has occured whilst requesting
                //Display error message and exit.
                if (wEx.Status == WebExceptionStatus.Timeout)
                    throw new Exception("Request Timed-Out.");
                else
                    throw new Exception(wEx.Message);

                //MessageBox.Show("Press Enter to Continue...\r\n" + wEx.Message);

            }
            #endregion

            #region Process Response
            // Get Response into String
            StreamReader sr = new StreamReader(str);
            string sssss = sr.ReadToEnd();
            xmlDoc.LoadXml(sssss);
            sr.Close();
            str.Close();

            if (!Directory.Exists(_storePath))
                Directory.CreateDirectory(_storePath);
            string filefullname = _storePath + "\\_getsellerTransaction.xml";
            if (File.Exists(filefullname))
                File.Delete(filefullname);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filefullname);
            sw.Write(sssss);
            sw.Close();
            sw.Dispose();
            return sssss;
            #endregion
        }
    }
}
