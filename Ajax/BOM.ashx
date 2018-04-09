<%@ WebHandler Language="C#" Class="BOM" %>

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
/// 临时BOM的子零件类
/// </summary>
public class Child
{
    /// <summary>
    /// ps_comp
    /// </summary>
    public string Part
    {
        get;
        set;
    }
    /// <summary>
    /// ps_qty_per
    /// </summary>
    public double QtyPer
    {
        get;
        set;
    }
    /// <summary>
    /// ps_scrp_pct
    /// </summary>
    public double Scrp
    {
        get;
        set;
    }
    /// <summary>
    /// ps_start
    /// </summary>
    public string StartDate
    {
        get;
        set;
    }
    /// <summary>
    /// ps_end
    /// </summary>
    public string EndDate
    {
        get;
        set;
    }
}

public class BOM : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {

        if (context.Request.Params["req"] == null) {
            return;
        }
        
        context.Response.ContentType = "text/html";

        JavaScriptSerializer json = new JavaScriptSerializer();
        
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

        string strSql = "Select Top 10 ps_comp, ps_qty_per = Isnull(ps_qty_per, 0), ps_scrp_pct = Isnull(ps_scrp_pct, 0), ps_start, ps_end";
        strSql += " From QAD_DATA.dbo.ps_mstr";
        strSql += " Where ps_domain = '" + _plantCode + "'";
        strSql += "     And ps_par = '" + _part + "'";

        IList<Child> list = new List<Child>();
        
        try
        {            
            DataTable table = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                { 
                    Child item = new Child();
                    item.Part = row["ps_comp"].ToString();
                    item.QtyPer = Convert.ToDouble(row["ps_qty_per"]);
                    item.Scrp = Convert.ToDouble(row["ps_scrp_pct"]);
                    item.StartDate = row["ps_start"].ToString() == string.Empty ? string.Empty : string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(row["ps_start"]));
                    item.EndDate = row["ps_end"].ToString() == string.Empty ? string.Empty : string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(row["ps_end"]));

                    list.Add(item);
                }
            }
            else
            {
                Child item = new Child();
                item.Part = "无数据";
                item.QtyPer = 0F;
                item.Scrp = 0F;
                item.StartDate = "";
                item.EndDate = "";

                list.Add(item);
            }
        }
        catch
        {
            Child item = new Child();
            item.Part = "无数据";
            item.QtyPer = 0F;
            item.Scrp = 0F;
            item.StartDate = "";
            item.EndDate = "";

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