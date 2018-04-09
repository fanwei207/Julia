<%@ WebHandler Language="C#" Class="Supplier" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;

public class Supplier : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {

        if (context.Request.Params["req"] == null)
        {
            return;
        }
        
        context.Response.ContentType = "text/html";
        
        StringBuilder strJson = new StringBuilder("[");

        string _supp = context.Request.Params["req"].ToString();
        string _plantCode = context.Session["PlantCode"].ToString();

        //if (_plantCode == "1")
        //    _plantCode = "SZX";
        //else if (_plantCode == "2")
        //    _plantCode = "ZQL";
        //else if (_plantCode == "5")
        //    _plantCode = "YQL";
        //else if (_plantCode == "8")
        //    _plantCode = "HQL";
        //else
        //    _plantCode = "";

        //string strSql = "Select Top 10 Json = '{\"Code\":\"' + ad_addr + '\",\"Name\":\"' + ad_name + '\",\"Addr1\":\"' + Case When Len(ad_line1) = 0 Then '&nbsp;' Else ad_line1 End + '\",\"Addr2\":\"' + Case When Len(ad_line2) = 0 Then '&nbsp;' Else ad_line2 End + '\"}' ";
        //strSql += " From QAD_DATA..ad_mstr";
        //strSql += " Where ad_domain = '" + _plantCode + "'";
        //strSql += "     And ad_type = 'supplier'";
        //strSql += "     And (ad_addr Like '" + _supp + "%'";
        //strSql += "     Or ad_name Like '" + _supp + "%')";

        string strSql = "Select Top 10 Json = '{\"Code\":\"' + REPLACE(REPLACE(ad_addr,char(10),''),CHAR(13),'')  + '\",\"Name\":\"' + REPLACE(REPLACE(max(ad_name),char(10),''),CHAR(13),'') + '\",\"Addr1\":\"' + Case When Len(max(ad_line1)) = 0 Then '&nbsp;' Else REPLACE(REPLACE(max(ad_line1),char(10),''),CHAR(13),'') End + '\",\"Addr2\":\"' + Case When Len(max(ad_line2)) = 0 Then '&nbsp;' Else REPLACE(REPLACE(max(ad_line2),char(10),''),CHAR(13),'') End + '\"}' ";
        strSql += " From QAD_DATA..ad_mstr";
        strSql += " Where ad_type = 'supplier'";
        strSql += "     And (ad_addr Like '" + _supp + "%'";
        strSql += "     Or ad_name Like N'%" + _supp + "%')";
        strSql += " Group by ad_addr";

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
                strJson.Append("{\"C\":\"\",\"N\":\"\",\"L1\":\"\",\"L2\":\"\"}");
            }
        }
        catch
        {
            strJson.Append("{\"C\":\"\",\"N\":\"\",\"L1\":\"\",\"L2\":\"\"}");
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