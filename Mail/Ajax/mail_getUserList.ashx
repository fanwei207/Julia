<%@ WebHandler Language="C#" Class="mail_getUserList" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;
using System.Collections.Generic;

public class usersMail {
    public string keyName
    {
        set;
        get;
    }
    public string valueName
    {
        set;
        get;
    }
}
public class mail_getUserList : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string key;
        JavaScriptSerializer json = new JavaScriptSerializer();
        key = context.Request["key"].ToString();
      //  key = "liu";
        IList<usersMail> list = new List<usersMail>();
        try {
            DataTable dt=MailHelper.getUsersMail(key); 
            if(dt!=null&&dt.Rows.Count>0)
            {
                 foreach (DataRow row in dt.Rows)
                 {
                     usersMail uMail=new usersMail();
                     uMail.keyName = row["keyName"].ToString();
                     uMail.valueName = row["valueName"].ToString();
                     list.Add(uMail);
                 }
            }
        }
        catch(Exception e){
            usersMail uMail = new usersMail();
            uMail.keyName = "";
            uMail.valueName = "";
            list.Add(uMail);
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