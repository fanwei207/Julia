using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TCPCHINA.ODBCHelper;
using System.IO;
using System.Globalization;
using System.Net;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

namespace TCP.Atl
{
    /// <summary>
    /// Summary description for AtlSo
    /// </summary>
    public class AtlSo
    {
        String strQadConn = ConfigurationSettings.AppSettings["SqlConn.QAD_DATA"];
        String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn9"];

        public AtlSo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet GetSO(String PO1, String PO2, String SO1, String SO2, String OrdDate1, String OrdDate2, String DueDate1, String DueDate2, Int32 uID)
        {
            try
            {
                string strName = "sp_atl_selectso";
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@PO1", PO1);
                param[1] = new SqlParameter("@PO2", PO2);
                param[2] = new SqlParameter("@SO1", SO1);
                param[3] = new SqlParameter("@SO2", SO2);
                param[4] = new SqlParameter("@OrdDate1", OrdDate1);
                param[5] = new SqlParameter("@OrdDate2", OrdDate2);
                param[6] = new SqlParameter("@DueDate1", DueDate1);
                param[7] = new SqlParameter("@DueDate2", DueDate2);
                param[8] = new SqlParameter("@uID", uID);

                return SqlHelper.ExecuteDataset(strQadConn, CommandType.StoredProcedure, strName, param);
            }
            catch
            {
                return null;
            }
        }

        public Decimal GetPricePercent(String start, String expire)
        {
            String strSQL = " SELECT pi_list_price FROM PUB.pi_mstr Where pi_cs_code = 'C0000006' And pi_part_code = '99999999999999' And pi_start <= '" + start + "' And pi_expire <= '" + expire + "' And pi_domain = 'ATL'";

            Object obj = OdbcHelper.ExecuteScalar(strConn, CommandType.Text, strSQL);

            if (obj == null || obj.ToString() == String.Empty)
                return 1;
            else
                return Convert.ToDecimal(obj.ToString());
        }

        public DataSet GetDomain()
        {
            String strSQL = " SELECT dom_domain FROM PUB.dom_mstr Order By dom_domain";

            return OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);
        }

        public DataSet GetSite(String domain)
        {
            String strSQL = " SELECT si_site FROM PUB.si_mstr Where si_domain = '" + domain + "' Order By si_site";

            return OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);
        }

        public DataSet GetSupplier(String domain)
        {
            String strSQL = " SELECT vd_addr FROM PUB.vd_mstr Where vd_domain = '" + domain + "' Order By vd_addr";

            return OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);
        }

        public Boolean InsertAtlSo(String Str)
        {
            try
            {
                int nRet = SqlHelper.ExecuteNonQuery(strQadConn, CommandType.Text, Str);

                if (nRet > -1)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public void Export(String[] List)
        {
            String strFileName = DateTime.Now.ToString("yyMMddHHmmss");

            String strPath = @"D:\" + strFileName;

            StreamWriter streamw = File.CreateText(strPath);

            foreach (String str in List)
            {
                if (str != String.Empty)
                {
                    streamw.WriteLine("@@batchload popomt.p");

                    foreach (String sub in str.Split(','))
                    {
                        streamw.WriteLine(sub);
                    }

                    streamw.WriteLine("@@end");
                }
            }

            //上传至链接服务器上
            if (streamw != null)
            {
                streamw.Close();
                FtpStatusCode status = this.UploadFun(strPath, "ftp://10.3.0.43//ATL//" + strFileName);
                if (status == FtpStatusCode.ClosingData)
                {
                    if (File.Exists(strPath))
                    {
                        //删除文件
                        File.Delete(strPath);
                    }
                }
            }

            streamw.Close();
        }

        private FtpStatusCode UploadFun(string fileName, string uploadUrl)
        {
            Stream requestStream = null;
            FileStream fileStream = null;
            FtpWebResponse uploadResponse = null;

            try
            {
                FtpWebRequest uploadRequest =
                (FtpWebRequest)WebRequest.Create(uploadUrl);
                uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;

                uploadRequest.Proxy = null;
                NetworkCredential nc = new NetworkCredential();
                nc.UserName = "administrator";
                nc.Password = "TcpQad43";

                uploadRequest.Credentials = nc;


                requestStream = uploadRequest.GetRequestStream();
                fileStream = File.Open(fileName, FileMode.Open);

                byte[] buffer = new byte[1024];
                int bytesRead;
                while (true)
                {
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    requestStream.Write(buffer, 0, bytesRead);
                }
                requestStream.Close();
                fileStream.Close();
                uploadResponse = (FtpWebResponse)uploadRequest.GetResponse();
                return uploadResponse.StatusCode;

            }
            catch (UriFormatException ex)
            {
            }
            catch (IOException ex)
            {
            }
            catch (WebException ex)
            {
            }
            finally
            {
                if (uploadResponse != null)
                    uploadResponse.Close();
                if (fileStream != null)
                    fileStream.Close();
                if (requestStream != null)
                    requestStream.Close();
            }

            return FtpStatusCode.Undefined;
        }

    }
}