<%@ WebHandler Language="C#" Class="custbondu" %>

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

public class custbondu : IHttpHandler, IRequiresSessionState {

    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    int rowct = 0;

    public void ProcessRequest (HttpContext context) {
    context.Response.ContentType = "application/json";

    string podid = context.Request["podid"];
	string podomain = context.Request["podomain"];
	string ponbr = context.Request["ponbr"];
	string povend = context.Request["povend"];
    string poline = context.Request["poline"];
	string posite = context.Request["posite"];
	string popart = context.Request["popart"];
    string poprice = context.Request["poprice"];
	string ctdnumber = context.Request["ctdnumber"];
    string ctdrmks = context.Request["ctdrmks"];
    string uid = context.Session["uID"].ToString();
    string uname = context.Session["uName"].ToString();
 
	  JavaScriptSerializer json = new JavaScriptSerializer();
	  IList<Podet> list = new List<Podet>();

      try
      {    
            SqlParameter[] param = new SqlParameter[12];

            param[0] = new SqlParameter("@podid", podid);
            param[1] = new SqlParameter("@podomain", podomain);
            param[2] = new SqlParameter("@ponbr", ponbr);
            param[3] = new SqlParameter("@povend", povend);
            param[4] = new SqlParameter("@poline", poline);
            param[5] = new SqlParameter("@posite", posite);
            param[6] = new SqlParameter("@popart", popart);
            param[7] = new SqlParameter("@poprice", poprice);
            param[8] = new SqlParameter("@ctdnumber", ctdnumber);
            param[9] = new SqlParameter("@ctdrmks", ctdrmks);
            param[10] = new SqlParameter("@uid", uid);
            param[11] = new SqlParameter("@uname", uname);

            DataTable _Table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_custbond_update", param).Tables[0];

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

/*AJAX 更新报关手册号*/