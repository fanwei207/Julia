<%@ WebHandler Language="C#" Class="Mail" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;
using System.Collections.Generic;

public class mailChild {
    public string m_uid
    {
        get;
        set;
    }
    public string m_subject
    {
        get;
        set;
    }
    public string m_fromName
    {
        get;
        set;
    }
    public string m_isDisposed
    {
        get;
        set;
    }
    public string m_receiveDate
    {
        get;
        set;
    }
    public string m_newMailNum
    {
        get;
        set;
    }
}

public class Mail : IHttpHandler {
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        int disposed;
        string eDate;
        string bDate;
        string bodyPath;
        string attachPath;
        string uId;
        JavaScriptSerializer json = new JavaScriptSerializer();
        disposed = Convert.ToInt32(context.Request.Params["isDisposed"].ToString());
        bDate = context.Request.Params["bDate"].ToString();
        eDate = context.Request.Params["eDate"].ToString();
        uId = context.Request.Params["uId"].ToString();
        bodyPath = context.Request.Params["bodyPath"].ToString();
        attachPath=context.Request.Params["attachPath"].ToString();
        IList<mailChild> list = new List<mailChild>();
        int mailNum=0;
      //  string jsonMail = "{mailNum:"+mailNum+"}";
        MailHelper.ReceiveMail(uId,out mailNum ,bodyPath ,attachPath);
        mailChild item1 = new mailChild();
        item1.m_newMailNum = mailNum.ToString();
        list.Add(item1);
        try {
            DataTable table = MailHelper.GetMalSubject(disposed, bDate, eDate, uId);
            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    mailChild item = new mailChild();
                    item.m_uid = row["m_UID"].ToString();
                    item.m_subject = row["msubject"].ToString();
                    item.m_fromName = row["mfromName"].ToString();
                    item.m_isDisposed = row["mDisposed"].ToString();
                    item.m_receiveDate = row["mdate"].ToString();
                    list.Add(item);
                }
            }
        }
        catch(Exception e)
        {
            mailChild item = new mailChild();
            item.m_uid = "";
            item.m_subject = "";
            item.m_fromName = "";
            item.m_isDisposed = "";
            item.m_receiveDate = "";
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