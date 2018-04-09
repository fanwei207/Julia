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
using adamFuncs;
using QADSID;
using System.IO;

public partial class SID_SID_ShowPicture : System.Web.UI.Page
{
    SID sid = new SID();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           string sidid = Request.QueryString["sid"];
            BindData(sidid);
        }
    }
    protected void BindData(string sidid)
    {
        //定义参数
        
        DataSet ds = sid.SelectSIDListpictureship(sidid);
        if (ds.Tables[0].Rows.Count == 0)
        {
            Response.Redirect("SID_ShipPicture.aspx?from=new&shipid=" + lblshipid.Text + "&sid=" + lblsid.Text + "&rt=" + DateTime.Now.ToFileTime().ToString());
         
        }
        gvSID.DataSource = ds.Tables[0];
        gvSID.DataBind();
        gv.DataSource = ds.Tables[1];
        gv.DataBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //int index = e.Row.RowIndex;
            //lblshipid.Text = gv.DataKeys[index].Values["SID_ShipId"].ToString().Trim();
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "show")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            //int fsId = Convert.ToInt32(gv.DataKeys[index].Values["fst_id"].ToString());
            string filePath = gv.DataKeys[index].Values["SID_url"].ToString();
            try
            {
                filePath = Server.MapPath(filePath);
            }
            catch (Exception)
            {

                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            
            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";



           // BindMessagereply();
        }
    }
    protected void btnup_Click(object sender, EventArgs e)
    {
        Response.Redirect("SID_UpPicture.aspx?from=new&shipid=" + lblshipid.Text + "&sid=" + lblsid.Text+ "&rt=" + DateTime.Now.ToFileTime().ToString());
         
    }
    protected void gvSID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;
            lblsid.Text += gvSID.DataKeys[index].Values["SID"].ToString().Trim() + ",";
            lblshipid.Text = gvSID.DataKeys[index].Values["SID_ShipId"].ToString().Trim();
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("SID_ShipPicture.aspx?from=new&shipid=" + lblshipid.Text + "&sid=" + lblsid.Text + "&rt=" + DateTime.Now.ToFileTime().ToString());
         
    }
}