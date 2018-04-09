<%@ WebHandler Language="C#" Class="prodcostb" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.Web.SessionState;
using System.Configuration;
using System.Collections.Generic;
using OEAppServer;

public class ProdCost
{
    public string lel
    {
        get;
        set;
    }
    public string ptype
    {
        get;
        set;
    }
    public string pspar
    {
        get;
        set;
    }
    public string pscomp
    {
        get;
        set;
    }
    public string pdesc2
    {
        get;
        set;
    }
    public string pstat
    {
        get;
        set;
    }
   /* public string uqty
    {
        get;
        set;
    }*/
    public string psqty
    {
        get;
        set;
    }
    public string pcurr
    {
        get;
        set;
    }
    public string porc
    {
        get;
        set;
    }
    public string cur_mtl_tl
    {
        get;
        set;
    }
    public string pcvend
    {
        get;
        set;
    }
    public string isbond
    {
        get;
        set;
    }
    public string mtl
    {
        get;
        set;
    }
    public string lbr
    {
        get;
        set;
    }
    public string bdn
    {
        get;
        set;
    }
}

 public class ResultData 
 {
    public object Data;
    public int TotalCount;
  }

public class prodcostb : IHttpHandler, IRequiresSessionState {
    AppServer appsv = new AppServer();
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];
    int rowct = 0;
    public void ProcessRequest (HttpContext context) {
    context.Response.ContentType = "application/json";
    /*context.Response.ContentType = "text/plain";*/
    /*context.Response.ContentType = "text/html";*/


    /*取得参数值*/
            string project = context.Request["project"];
            string part = context.Request["part"];
            string tax = context.Request["tax"];
            string rate = context.Request["rate"];
            string dif = context.Request["dif"];

            if (project != String.Empty)
            {
            string strSql = "SELECT TOP 1 prod_QAD FROM prod_mstr where prod_Code = '" + project + "' order by prod_id desc";
            part = Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.Text, strSql)); 
            }

          JavaScriptSerializer json = new JavaScriptSerializer();
          IList<ProdCost> list = new List<ProdCost>();

      if (part.Length >= 14)  /*限制长度*/
      {
       try
       {
       DataTable _Table = appsv.prodcost(part, Convert.ToDecimal(tax),Convert.ToDecimal(rate),Convert.ToDecimal(dif));

            DataColumn dc0 = new DataColumn();
            dc0.DataType = System.Type.GetType("System.Decimal");
            dc0.Caption = "strun";
            dc0.ColumnName = "strun";
            dc0.Expression = "psqty * psrun";
            _Table.Columns.Add(dc0);

            DataColumn dc1 = new DataColumn();
            dc1.DataType = System.Type.GetType("System.Decimal");
            dc1.Caption = "mtl";
            dc1.ColumnName = "mtl";
            dc1.Expression = "psqty * cur_mtl_tl";
            _Table.Columns.Add(dc1);

            DataColumn dc2 = new DataColumn();
            dc2.DataType = System.Type.GetType("System.Decimal");
            dc2.Caption = "lbr";
            dc2.ColumnName = "lbr";
            dc2.Expression = "psqty * psrun * pslbr";
            _Table.Columns.Add(dc2);

            DataColumn dc3 = new DataColumn();
            dc3.DataType = System.Type.GetType("System.Decimal");
            dc3.Caption = "bdn";
            dc3.ColumnName = "bdn";
            dc3.Expression = "psqty * psrun * psbdn";
            _Table.Columns.Add(dc3);

             /*从APPSERVER里取得DATATABLE*/
            if (_Table != null && _Table.Rows.Count > 0)
            {
                rowct = _Table.Rows.Count;
                foreach (DataRow row in _Table.Rows)
                {
                    ProdCost prod = new ProdCost();
                    prod.lel = row["lel"].ToString();
                    prod.ptype = row["ptype"].ToString();
                    prod.pspar = row["pspar"].ToString();
                    prod.pscomp = row["pscomp"].ToString();
                    prod.pdesc2 = row["pdesc2"].ToString();
                    prod.pstat = row["pstat"].ToString();
                    //prod.uqty = row["uqty"].ToString();
                    prod.psqty = row["psqty"].ToString();
                    prod.pcurr = row["pcurr"].ToString();
                    if (row["pcvend"].ToString() == String.Empty)
                    {
                    prod.porc = "";
                    prod.cur_mtl_tl = "";
                    prod.isbond = "";
                    }
                    else
                    {
                    prod.porc = row["porc"].ToString();
                    prod.cur_mtl_tl = row["cur_mtl_tl"].ToString();
                    prod.isbond = row["isbond"].ToString();
                    }
                    prod.pcvend = row["pcvend"].ToString();
                    prod.mtl = row["mtl"].ToString();
                    if (row["pstat"].ToString() != "V" && row["pstat"].ToString() != "SV")
                    {
                    prod.lbr = row["lbr"].ToString();
                    prod.bdn = row["bdn"].ToString();
                    }
                    else
                    {
                    prod.lbr = "";
                    prod.bdn = "";
                    }
                    list.Add(prod);
                }
            }
            else
            {
                ProdCost prod = new ProdCost();
                    prod.lel = "";
                    prod.ptype = "无数据";
                    prod.pspar = "";
                    prod.pscomp = "";
                    prod.pdesc2 = "";
                    prod.pstat = "";
                    //prod.uqty = "";
                    prod.psqty = "";
                    prod.pcurr = "";
                    prod.porc = "";
                    prod.cur_mtl_tl = "";
                    prod.pcvend = "";
                    prod.isbond = "";
                    prod.mtl = "";
                    prod.lbr = "";
                    prod.bdn = "";
                
                list.Add(prod);
            }
        }
        catch
        { 
            ProdCost prod = new ProdCost();
                    prod.lel = "";
                    prod.ptype = "无数据";
                    prod.pspar = "";
                    prod.pscomp = "";
                    prod.pdesc2 = "";
                    prod.pstat = "";
                    //prod.uqty = "";
                    prod.psqty = "";
                    prod.pcurr = "";
                    prod.porc = "";
                    prod.cur_mtl_tl = "";
                    prod.pcvend = "";
                    prod.isbond = "";
                    prod.mtl = "";
                    prod.lbr = "";
                    prod.bdn = "";

            list.Add(prod);
        }    
        }  
       
       /* context.Response.Write(json.Serialize(list));*/
       context.Response.Write(json.Serialize(new ResultData()
        {
            Data = list,
            TotalCount = rowct
        }));

         /*序列化成JSON*/

        context.Response.End();
         /*响应请求*/
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}

/*AJAX 查询产品成本*/
/*RGJSBSWLZBNJHWYY*/