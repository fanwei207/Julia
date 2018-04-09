<%@ WebHandler Language="C#" Class="CloseNotice" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;

public class CloseNotice : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";

        string _userID = context.Session["uID"].ToString();
        
        string strSql = "Update Users";
        strSql += " Set showNotice = 'none'";
        strSql += " Where userID = " + _userID;

        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSql);

            context.Session["showNotice"] = "none";
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