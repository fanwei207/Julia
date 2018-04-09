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
using RD_WorkFlow;

public partial class RDW_DocView : System.Web.UI.Page
{
    RDW rdw = new RDW();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null)
        {
            ltlAlert.Text = "window.close();";
        }

        //定义参数
        string strDocID = Convert.ToString(Request.QueryString["id"]);

        SqlDataReader reader = rdw.SelectRDWDetailDocView(strDocID);
        reader.Read();

        Byte[] _byte = (byte[])reader["content"];
        String _filename = DateTime.Now.ToFileTime().ToString() + reader["filename"].ToString();
        FileStream _fs = new FileStream(Server.MapPath("/Excel/") + _filename, FileMode.OpenOrCreate, FileAccess.Write);
        _fs.Write(_byte, 0, _byte.Length);
        _fs.Close();

        ltlAlert.Text = "window.open('/Excel/" + _filename + "', '_blank'); window.close();";
        reader.Close();

        //Response.ContentType = Convert.ToString(reader["Type"]).Trim();
        //Response.BinaryWrite((byte[])reader["Content"]);
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(reader["FileName"].ToString()));
        //Response.Flush();
        //Response.Close();
        //reader.Close();
    }
}
