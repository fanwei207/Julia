<%@ WebHandler Language="C#" Class="Part" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;

public class Part : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {

        if (context.Request.Params["req"] == null)
        {
            return;
        }
        
        context.Response.ContentType = "text/html";
        
        StringBuilder strJson = new StringBuilder("[");

        string _part = context.Request.Params["req"].ToString();
        string _plantCode = context.Session["PlantCode"].ToString();

        if (_plantCode == "1")
            _plantCode = "SZX";
        else if (_plantCode == "2")
            _plantCode = "ZQL";
        else if (_plantCode == "5")
            _plantCode = "YQL";
        else if (_plantCode == "8")
            _plantCode = "HQL";
        else
            _plantCode = "";

        string strSql = "Select Top 10 Json = '{\"Part\":\"' + REPLACE(REPLACE(pt_part,char(10),''),CHAR(13),'') + '\",\"Desc1\":\"' + REPLACE(REPLACE(REPLACE(pt_desc1,'\"','' ),char(10),''),CHAR(13),'') + '\",\"Desc2\":\"' + REPLACE(REPLACE(REPLACE(pt_desc2,'\"','' ),char(10),''),CHAR(13),'')  + '\"}' ";
        strSql += " From QAD_DATA..pt_mstr";
        strSql += " Where pt_domain = '" + _plantCode + "'";
        strSql += "     And pt_part Like '" + _part + "%'";

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