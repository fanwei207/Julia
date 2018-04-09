<%@ WebHandler Language="C#" Class="custbondh" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.Web.SessionState;
using System.Configuration;
using System.Collections.Generic;

public class Sodet
{
    public string sodid
    {
        get;
        set;
    }
    public string sonbr
    {
        get;
        set;
    }
    public string soline
    {
        get;
        set;
    }
    public string cust
    {
        get;
        set;
    }
    public string shipto
    {
        get;
        set;
    }
    public string name
    {   get;
        set;
    }
    public string shipqty
    {
        get;
        set;
    }
    public string shipdate
    {
        get;
        set;
    }
    public string createdate
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

public class custbondh : IHttpHandler, IRequiresSessionState {

    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    int rowct = 0;

    public void ProcessRequest (HttpContext context) {
    context.Response.ContentType = "application/json";

	
    /*取得参数值*/
	string hid = context.Request["hid"];
	 
	  JavaScriptSerializer json = new JavaScriptSerializer();
	  IList<Sodet> list = new List<Sodet>();

      try
      {    
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@hid", hid);
            DataTable _Table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_custbond_hist", param).Tables[0];

            if (_Table != null && _Table.Rows.Count > 0)
            {
                rowct = _Table.Rows.Count;
                foreach (DataRow row in _Table.Rows)
                {
                    Sodet sod = new Sodet();
                    sod.sodid = row[0].ToString();
                    sod.sonbr = row[1].ToString();
                    sod.soline = row[2].ToString();
                    sod.cust = row[3].ToString();
					sod.shipto = row[4].ToString();
                    sod.name = row[5].ToString();
					sod.shipqty = row[6].ToString();
                    sod.shipdate = row[7].ToString();
                    sod.createdate = row[8].ToString();
                    list.Add(sod);
                }
            }
            /*else
            {
                    Sodet sod = new Sodet();
                    sod.sodid = "";
                    sod.sonbr = "";
                    sod.soline = "";
                    sod.cust = "";
					sod.shipto = "";
                    sod.name = "无数据";
					sod.shipqty = "";
                    sod.shipdate = "";
                    sod.createdate = "";
                    list.Add(sod);
            }*/
	  }
        catch
        { 
                  Sodet sod = new Sodet();
                    sod.sodid = "";
                    sod.sonbr = "";
                    sod.soline = "";
                    sod.cust = "";
					sod.shipto = "";
                    sod.name = "数据获取出错";
					sod.shipqty = "";
                    sod.shipdate = "";
                    sod.createdate = "";
                    list.Add(sod);
        }    
       
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

/*明细*/