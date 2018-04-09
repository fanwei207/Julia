<%@ WebHandler Language="C#" Class="Item" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;
using System.Collections.Generic;

/// <summary>
/// Item的定义
/// </summary>
public class ItemCell
{
    public string Code
    {
        get;
        set;
    }
    public string Desc
    {
        get;
        set;
    }
    public string OldCode
    {
        get;
        set;
    }
}

public class Item : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {

        if (context.Request.Params["req"] == null) {
            return;
        }
        
        context.Response.ContentType = "text/html";

        JavaScriptSerializer json = new JavaScriptSerializer();
        
        string _param = context.Request.Params["req"].ToString();

        string strSql = "Select Top 10 REPLACE(REPLACE(code,char(10),''),CHAR(13),''), description = REPLACE(REPLACE(Isnull(description, ''),char(10),''),CHAR(13),''), old_code = REPLACE(REPLACE(Isnull(old_code, ''),char(10),''),CHAR(13),'')";
        strSql += "     From Items ";
        strSql += "     Where code Like '" + _param + "%'";

        IList<ItemCell> list = new List<ItemCell>();
        
        try
        {            
            DataTable table = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    ItemCell item = new ItemCell();
                    item.Code = row["code"].ToString();
                    item.Desc = row["description"].ToString();
                    item.OldCode = row["old_code"].ToString();

                    list.Add(item);
                }
            }
            else
            {
                ItemCell item = new ItemCell();
                item.Code = "无数据";
                item.Desc = "";
                item.OldCode = "";
                
                list.Add(item);
            }
        }
        catch
        {
            ItemCell item = new ItemCell();
            item.Code = "无数据";
            item.Desc = "";
            item.OldCode = "";

            list.Add(item);
        }

        context.Response.Write(json.Serialize(list));
        context.Response.End();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}