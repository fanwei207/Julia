<%@ WebHandler Language="C#" Class="QCtest" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;

public class QCtest : IHttpHandler {

    adamClass adam = new adamClass();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        string nbr = context.Request.Params["nbr"].ToString();
        string lot = context.Request.Params["lot"].ToString();

        string sql = string.Empty;
        string result;

        sql += " Select top 1 isnull('part:' + isnull(wo_part,'') + ';domain:' + isnull(wo_domain,'') + ';site:' + isnull(wo_site,1000) + ';Desc:' + isnull(pt_desc1,'') + ',' + isnull(pt_desc2,''),'')";
        sql += " from qad_data.dbo.wo_mstr wo ";
        sql += " left join qad_data.dbo.pt_mstr pt on wo.wo_part = pt.pt_part ";
        sql += " where wo_nbr = '" + nbr + "' ";
        sql += "     And wo_lot = " + lot + " ";
        result = Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql));

        if (result != string.Empty)
        {
            context.Response.Write(result.ToString());
        }
        else
        {
            context.Response.Write("part:;domain:;site:;Desc:;");
        }
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}