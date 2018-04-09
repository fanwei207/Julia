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

        string _detId = context.Request.Params["detId"].ToString();

        string strSql = "Select dmd_html From WorkFlow.dbo.Demo_det Where dmd_id = " + _detId;

        try
        {
            string _html = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSql).ToString();

            context.Response.Write(_html);
        }
        catch
        {
            context.Response.Write("获取内容时出错！请联系管理员");
        }

        context.Response.End();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}