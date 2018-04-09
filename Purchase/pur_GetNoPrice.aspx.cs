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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class pur_GetNoPrice : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
        }
    }
    private void GridViewBind() 
    {
        try
        {
            gv.DataSource = GetTypeList(txt_vender.Text.Trim(), txt_part.Text.Trim());
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }

    public DataTable GetTypeList(string vender, string part)
    {
        try
        {
            string strName = "sp_pur_GetNoPrice";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@vender", vender);
            param[1] = new SqlParameter("@part", part);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //string typeid = gv.DataKeys[e.RowIndex].Values["pur_typeid"].ToString();
        //TextBox typename = (TextBox)gv.Rows[e.RowIndex].FindControl("txt_typename");

        gv.EditIndex = -1;
        GridViewBind();
    }
    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail")
        {
            if (gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('此行是空行！');";
                return;
            }
            string versionid = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionId"].ToString();
            string versionname = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionName"].ToString();
            string flag = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionFlag"].ToString();
            Response.Redirect("pur_ResultCheckStandard.aspx?id=" + versionid + "&name=" + versionname + "&flag=" + flag + "&rm=" + DateTime.Now);
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //BindTypes();
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        DataTable dt = GetTypeList(txt_vender.Text.Trim(), txt_part.Text.Trim());
        string title = "<b>客户ID</b>~^50^<b>域</b>~^120^<b>物料</b>~^50^<b>地点</b>~^120^<b>入库日期</b>~^";
        ExportExcel(title, dt, false);        
    }
}
