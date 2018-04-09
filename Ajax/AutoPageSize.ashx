<%@ WebHandler Language="C#" Class="AutoPageSize" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;

public class AutoPageSize : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";

        string _clientheight = context.Request.Params["clientheight"].ToString().ToLower();
        string _menu = context.Request.Params["menu"].ToString().ToLower();
        string _userID = context.Session["uID"].ToString();
        string _pagesize = context.Request.Params["pagesize"].ToString();
        
        string strSql = "Insert Into AutoPageSize(aps_clientheight, aps_menu, aps_userid, aps_clientwidth, aps_pagesize, aps_createDate)";
        strSql += " Select " + _clientheight + ", id, " + _userID + ", Null, " + _pagesize + ", GetDate()";
        strSql += " From Menu";
        strSql += " Where url = '" + _menu + "'";

        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSql);

            context.Response.Write(true);
        }
        catch
        {
            context.Response.Write(false);
        }

        context.Response.End();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}