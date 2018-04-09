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

namespace TCP.Price
{
    /// <summary>
    /// Summary description for Price
    /// </summary>
    public class POPrice
    {
        String strConn = System.Configuration.ConfigurationSettings.AppSettings["sqlConn.Conn9"];

        public POPrice()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        /// <summary>
        /// 从pc_mstr中获取数据
        /// </summary>
        /// <param name="part">零件号</param>
        /// <param name="date">截止日期</param>
        /// <returns></returns>
        public DataSet GetPriceList(String part, String date)
        {
            //String strSQL = " SELECT pc_domain, pc_part, pc_list, pc_um, pc_amt, pc_start, pc_expire, Case When pc_expire Is Null Then '4000-1-1' Else pc_expire End pc_expire1, pc_curr FROM PUB.pc_mstr Where pc_part Like '" + part + "%' And pc_start <= '" + date + "' And (pc_expire >= '" + date + "' Or pc_expire Is Null) Order By pc_domain, pc_part, pc_list, pc_expire1 DESC, pc_start DESC";
            String domain = "'szx'";
            String strhd = " SELECT pc_domain, pc_part, pc_list, pc_um, pc_amt, pc_start, pc_expire, Case When pc_expire Is Null Then '4000-1-1' Else pc_expire End pc_expire1, pc_curr FROM PUB.pc_mstr Where pc_part Like '" + part + "%' And pc_start <= '" + date + "' And (pc_expire >= '" + date + "' Or pc_expire Is Null) and pc_domain = ";
            String strSQL;
            strSQL = strhd + domain;
            do
            {
                switch (domain)
                {
                    case "'szx'": domain = "'zql'";
                        break;
                    case "'zql'": domain = "'zqz'";
                        break;
                    case "'zqz'": domain = "'yql'";
                        break;
                    case "'yql'": domain = "'hql'";
                        break;
                    case "'hql'": domain = "'atl'";
                        break;
                    default: domain = "";
                        break;
                }
                if (domain != "")
                {
                    strSQL = strSQL + " union " + strhd + domain;
                }
            }
            while (domain != "");

            
            return OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);
        }
        /// <summary>
        /// 获取零件的产品类、描述信息
        /// </summary>
        /// <param name="part">零件号</param>
        /// <param name="domain">域</param>
        /// <returns></returns>
        public DataSet GetPartInfo(String part, String domain)
        {
            String strSQL = " SELECT pt_prod_line, pt_desc1, pt_buyer FROM PUB.pt_mstr Where pt_part = '" + part + "' And pt_domain = '" + domain + "'";

            return OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);
        }
        /// <summary>
        /// 获取供应商的名称
        /// </summary>
        /// <param name="vent">供应商代码</param>
        /// <param name="domain">域</param>
        /// <returns></returns>
        public String GetVendInfo(String vent, String domain)
        {
            String strSQL = " SELECT ad_name FROM PUB.ad_mstr Where ad_addr = '" + vent + "' And ad_domain = '" + domain + "'";

            DataSet _dataset = OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);

            if (_dataset != null && _dataset.Tables[0].Rows.Count > 0)
                return _dataset.Tables[0].Rows[0][0].ToString();
            else
                return vent;
        }

    }
}