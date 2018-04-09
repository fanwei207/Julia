using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
 
public partial class PM_DocView : BasePage
{
   
    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null)
        {
            ltlAlert.Text = "window.close();";
        }

        SqlDataReader reader = wf.GetAttachDetail(Convert.ToInt32(Request.QueryString["id"]));
        if (reader.HasRows)
        {
            //reader.Read();
            //Response.ContentType = Convert.ToString(reader["Attach_Type"]).Trim();
            //Response.BinaryWrite((byte[])reader["Attach_Content"]);
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(reader["Attach_Name"].ToString()));
            //Response.Flush();
            //Response.Close();
            //reader.Close();

            reader.Read();
            FileStream fs = new FileStream(Server.MapPath("/Excel/") + reader["Attach_Name"].ToString(), FileMode.OpenOrCreate, FileAccess.Write);
            byte[] ct = (byte[])reader["Attach_Content"];
            fs.Write(ct, 0, ct.Length);
            fs.Close();
            ltlAlert.Text = "window.open('/Excel/" + reader["Attach_Name"].ToString() + "', '_blank'); window.close();";
            reader.Close();
            
        }
    }
}
