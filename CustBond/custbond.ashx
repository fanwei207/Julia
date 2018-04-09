<%@ WebHandler Language="C#" Class="custbond" %>

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

public class Podet
{
    public string podid
    {
        get;
        set;
    }
    public string podomain
    {
        get;
        set;
    }
    public string ponbr
    {
        get;
        set;
    }
    public string povend
    {
        get;
        set;
    }
    public string poline
    {
        get;
        set;
    }
    public string posite
    {
        get;
        set;
    }
    public string popart
    {
        get;
        set;
    }
    public string podesc
    {   get;
        set;
    }
    public string poord
    {
        get;
        set;
    }
    public string poqty
    {
        get;
        set;
    }
    public string podate
    {
        get;
        set;
    }
     public string poprice
    {
        get;
        set;
    }
    public string ctdnumber
    {
        get;
        set;
    }
    public string ctdrmks
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

public class custbond : IHttpHandler, IRequiresSessionState {

    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    int rowct = 0;

    public void ProcessRequest (HttpContext context) {
    context.Response.ContentType = "application/json";

	
    /*取得参数值*/
	string vend = context.Request["vend"];
	string date = context.Request["date"];
	string date1 = context.Request["date1"];
	string part = context.Request["part"];
    string bstatus = context.Request["bstatus"];
	
     if (date == "")
     date = "1900-01-01";
	 //low_date

     if (date1 == "")
     date1 = "3999-12-31";
	 //hi_date

	 
	  JavaScriptSerializer json = new JavaScriptSerializer();
	  IList<Podet> list = new List<Podet>();

      try
      {    
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@vend", vend);
            param[1] = new SqlParameter("@part", part);
            param[2] = new SqlParameter("@date", date);
            param[3] = new SqlParameter("@date1", date1);
            param[4] = new SqlParameter("@bstatus", bstatus);
            DataTable _Table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_custbond_select", param).Tables[0];

            if (_Table != null && _Table.Rows.Count > 0)
            {
                rowct = _Table.Rows.Count;
                foreach (DataRow row in _Table.Rows)
                {
                    Podet pod = new Podet();
                    pod.podid = row[0].ToString();
                    pod.podomain = row[1].ToString();
                    pod.ponbr = row[2].ToString();
                    pod.povend = row[3].ToString();
                    pod.poline = row[4].ToString();
                    pod.posite = row[5].ToString();
					pod.popart = row[6].ToString();
                    pod.podesc = row[7].ToString();
                    pod.poord = row[8].ToString();
					pod.poqty = row[9].ToString();
                    pod.podate = row[10].ToString();
                    pod.poprice = row[11].ToString();
                    pod.ctdnumber = row[12].ToString();
                    pod.ctdrmks = row[13].ToString();
                    list.Add(pod);
                }
            }
            /*else
            {
                    Podet pod = new Podet();
                    pod.podid = "";
                    pod.podomain = "";
                    pod.ponbr = "";
                    pod.povend = "";
                    pod.poline = "";
                    pod.posite = "";
					pod.popart = "无数据";
                    pod.podesc = "";
                    pod.poord = "";
					pod.poqty = "";
                    pod.podate = "";
                    pod.poprice = "";
                    pod.ctdnumber = "";
                    pod.ctdrmks = "";
                    list.Add(pod);
            }*/
	  }
        catch
        { 
                    Podet pod = new Podet();
                    pod.podid = "";
                    pod.podomain = "";
                    pod.ponbr = "";
                    pod.povend = "";
                    pod.poline = "";
                    pod.posite = "";
					pod.popart = "数据获取出错";
                    pod.podesc = "";
                    pod.poord = "";
					pod.poqty = "";
                    pod.podate = "";
                    pod.poprice = "";
                    pod.ctdnumber = "";
                    pod.ctdrmks = "";
                    list.Add(pod);
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

/*AJAX 查询采购收货用于匹配报关手册号*/