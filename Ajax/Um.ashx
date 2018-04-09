<%@ WebHandler Language="C#" Class="Um" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;
public class Um : IHttpHandler, IRequiresSessionState
{

    adamClass adam = new adamClass();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";


        StringBuilder strJson = new StringBuilder("[");

        string _qad = context.Request.Params["qad"].ToString();
        string _vend = context.Request.Params["vend"].ToString();

        string strSql = "Select distinct Top 10 Json = N'{\"UM\":\"' + REPLACE(REPLACE(pc_um,char(10),''),CHAR(13),'') + '\"}' ";
        strSql += " from tcpc0..pc_mstr where isnull(pc_start,'1900-1-1') <= GETDATE() and dateadd(day,1,isnull(pc_expire,'2999-1-1')) > getdate() ";
        //string p = "select distinct pc_um,pc_um from tcpc0..pc_mstr where isnull(pc_start,'1900-1-1') <= GETDATE() and dateadd(day,1,isnull(pc_expire,'2999-1-1')) > getdate()";
        if (_qad != string.Empty)
        {
            strSql += " And pc_part = '" + _qad + "'";
        }
        if (_vend != string.Empty)
        {
            strSql += " And pc_list = '" + _vend + "'";
        }
        strSql += " union select N'{\"UM\":\"其他\"}'";
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
                strJson.Append("{\"C\":\"\",\"N\":\"\",\"L1\":\"\",\"L2\":\"\"}");
            }
        }
        catch
        {
            strJson.Append("{\"C\":\"\",\"N\":\"\",\"L1\":\"\",\"L2\":\"\"}");
        }
        //strJson.ToString().Replace("\"", "");
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