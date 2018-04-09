<%@ WebHandler Language="C#" Class="Buyer" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;

public class Buyer : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/html";
        
        StringBuilder strJson = new StringBuilder("[");

        string _domain = context.Request.Params["domain"].ToString();

        string strSql = "Select Top 10 Json = '{\"Code\":\"' + REPLACE(REPLACE(code_value,char(10),''),CHAR(13),'') + '\",\"Name\":\"' + REPLACE(REPLACE(code_cmmt,char(10),''),CHAR(13),'') + '\",\"Domain\":\"' + REPLACE(REPLACE(code_domain,char(10),''),CHAR(13),'') + '\"}' ";
        strSql += " From QAD_DATA..code_mstr";
        strSql += " Where code_domain = '" + _domain + "'";
        strSql += "     And code_fldName = 'po_buyer'";
        strSql += "     And Len(code_cmmt) > 0";

        try
        {            
            DataTable table = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];

            if (table != null && table.Rows.Count > 0)
            {
                for (int i = 0; i <table.Rows.Count;i++)
                {
                    //最后一个不加逗号
                    if (i == table.Rows.Count - 1)
                    {
                        strJson.Append(table.Rows[i]["Json"].ToString().Replace("\\", ""));
                    }
                    else
                    {
                        strJson.Append(table.Rows[i]["Json"].ToString().Replace("\\", "") + ",");
                    }
                }
            }
            else
            {
                strJson.Append("{\"P\":\"\",\"D\":\"\",\"E\":\"\"}");
            }
        }
        catch
        {
            strJson.Append("{\"P\":\"\",\"D\":\"\",\"E\":\"\"}");
        }
        
        strJson.Append("]");

        context.Response.Write(strJson.ToString());
        context.Response.End();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}