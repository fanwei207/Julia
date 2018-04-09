using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;

public partial class EDIPoStatus : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DataSet _postion = SelectOverlayPosition("PoStatus");

            string strInnerHtml = string.Empty;

            if (_postion != null && _postion.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in _postion.Tables[0].Rows)
                {
                    strInnerHtml += "<div style=\"" + row["opst_style"].ToString() + "\" " + row["opst_action"].ToString() + "></div>";
                }
            }

            divOverlayHolder.InnerHtml = strInnerHtml + divOverlayHolder.InnerHtml;
        }
    }

    protected DataSet SelectOverlayPosition(string OverlayName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@overlayName", OverlayName);

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "proc_edi_selectOverlayPosition", param);
        }
        catch
        {
            return null;
        }
    }
}
