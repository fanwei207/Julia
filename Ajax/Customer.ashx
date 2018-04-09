<%@ WebHandler Language="C#" Class="Supplier" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public class Supplier : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {

        if (context.Request.Params["req"] == null)
        {
            return;
        }
        
        context.Response.ContentType = "text/html";

        JavaScriptSerializer json = new JavaScriptSerializer();

        string _supp = context.Request.Params["req"].ToString();
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

        IList<dynamic> list = new List<dynamic>();

        string strSql = "Select Top 10 REPLACE(REPLACE(ad_addr,char(10),''),CHAR(13),'') as ad_addr, REPLACE(REPLACE(MAX(ad_name),char(10),''),CHAR(13),'') as ad_name, ad_line1 = Case When Len(MAX(ad_line1)) = 0 Then '&nbsp;' Else REPLACE(REPLACE(MAX(ad_line1),char(10),''),CHAR(13),'') End, ad_line2 = Case When Len(MAX(ad_line2)) = 0 Then '&nbsp;' Else REPLACE(REPLACE(MAX(ad_line2),char(10),''),CHAR(13),'') End";
        strSql += " From QAD_DATA..ad_mstr";
        strSql += " Where ad_type = 'customer'";
        strSql += "     And (ad_addr Like '" + _supp.Replace("*", "%") + "%' Or ad_name Like N'%" + _supp.Replace("*", "%") + "%') GROUP BY ad_addr";

        try
        {            
            DataTable table = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    list.Add(new {
                        Code = row["ad_addr"].ToString(),
                        Name = row["ad_name"].ToString(),
                        Addr1 = row["ad_line1"].ToString(),
                        Addr2 = row["ad_line2"].ToString(),
                    });
                }
            }
            else
            {
                list.Add(new
                {
                    Code = "",
                    Name = "",
                    Addr1 = "",
                    Addr2 = ""
                });
            }
        }
        catch
        {
            list.Add(new
            {
                Code = "",
                Name = "",
                Addr1 = "",
                Addr2 = ""
            });
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