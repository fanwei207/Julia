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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for Cproducttrack
/// </summary>
public class Cproducttrack
{
    String strSQL = "";
    String strConn = System.Configuration.ConfigurationSettings.AppSettings["sqlConn.Conn9"];

    public Cproducttrack()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DataSet GetWO(String Lot, String strDate, String strDate1)
    {

        strSQL = "select tr_nbr,tr_lot,tr_part,tr_serial,tr_date,tr_qty_loc,tr_domain from PUB.tr_hist where tr_type = 'RCT-WO' ";
        strSQL += "and tr_serial ='" + Lot + "' and tr_date >= '" + strDate + "' and tr_date <= '" + strDate1 + "'  ";

        // strSQL = "select wo_nbr,wo_lot,wo_part,'',wo_rel_date,'',wo_domain from PUB.wo_mstr where wo_domain = 'szx'";
        //strSQL  += " and wo_nbr = '" + Lot + "' ";

        return OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);

    }

    public DataSet GetWOIss(String domain, String Nbr, String Lot)
    {
        strSQL = "select tr_nbr,tr_lot,tr_part,tr_date,tr_serial,0 - tr_qty_loc as qty_loc from PUB.tr_hist where tr_domain = '" + domain + "' and tr_type='iss-wo' and tr_nbr ='" + Nbr + "' and tr_lot = '" + Lot + "'";
        return OdbcHelper.ExecuteDataset(strConn, CommandType.Text, strSQL);

    }


}
