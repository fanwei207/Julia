<%@ WebHandler Language="C#" Class="CloseMenu" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;

public class CloseMenu : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";

        string _margin_left = context.Request.Params["req"].ToString(); 
        string _userID = context.Session["uID"].ToString();
        
        string strSql = "Update Users";
        strSql += " Set showMenu = '" + _margin_left + "'";
        strSql += " Where userID = " + _userID;

        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSql);

            context.Session["showMenu"] = _margin_left;
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