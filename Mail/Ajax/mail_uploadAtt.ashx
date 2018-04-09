<%@ WebHandler Language="C#" Class="mail_uploadAtt" %>
using System.Web.Script.Serialization;
using System;
using System.Web;

public class urlData
{
  //  public string error { get; set; }
    public string msg { get; set; }
    public string url { get; set; }
}
public class mail_uploadAtt : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        HttpFileCollection files = context.Request.Files;
        urlData ud = new urlData();
        string basePath = context.Request["basePath"].ToString();
        string msg = string.Empty;
       // string error = string.Empty;
        string url;
        string fileName;
        JavaScriptSerializer json = new JavaScriptSerializer();
        if (files.Count > 0)
        {
            fileName=DateTime.Now.ToString("yyyyMMddhhmmss")+DateTime.Now.Millisecond+"_"+System.IO.Path.GetFileName(files[0].FileName);
            files[0].SaveAs(basePath + fileName);
            msg = (files[0].ContentLength/1024).ToString();
            ud.msg = msg;
            url = basePath + fileName;
            ud.url = url;
            context.Response.Write(json.Serialize(ud));
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}