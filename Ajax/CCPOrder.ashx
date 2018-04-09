<%@ WebHandler Language="C#" Class="CCPOrder" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;

public class CCPOrder : IHttpHandler, IRequiresSessionState
{


    adamClass adam = new adamClass();

    public void ProcessRequest(HttpContext context)
    {

        if (context.Request.Params["req"] == null)
        {
            return;
        }

        context.Response.ContentType = "text/html";

        StringBuilder strJson = new StringBuilder("[");

        string _order = context.Request.Params["req"].ToString();
        //string _plantCode = context.Session["PlantCode"].ToString();

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

        string strSql = "Select Top 10 Json = '{\"poNbr\":\"' + poNbr + '\"}' ";
        strSql += " From EDI_DB..EdiPoHrd";
        strSql += " Where poNbr like '" + _order + "%'";
        //strSql += "     And pt_part Like '" + _part + "%'";

        try
        {
            DataTable table = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];

            if (table != null && table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}