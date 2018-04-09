using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommClass;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class Sample_DocView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Request.QueryString["filepath"].ToString() == string.Empty)
            {
                Response.Redirect("无文档内容");
            }
            loaddoc();
        }

    }
 
    /// <summary>
    /// Request.QueryString["filepath"]当传历史文档时,传过来的参数据documentdetail的filepath 字段值而不是id值
    /// </summary>
    private void loaddoc()
    {
        string strFilePath = Request.QueryString["filepath"].ToString(); 
        String strSql = @" Select dd.content,dd.type, d.filename  From qaddoc.dbo.documentdetail dd Left join qaddoc.dbo.documents d on d.filepath=dd.id 
                           Where dd.id = " + strFilePath;

        SqlDataReader reader = SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, strSql);
     
        reader.Read();
        Response.ContentType = Convert.ToString(reader["Type"]).Trim();
        Response.BinaryWrite((byte[])reader["content"]);
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(reader["filename"].ToString()));
        Response.Flush();
        Response.Close();
        reader.Close(); 
    } 
}