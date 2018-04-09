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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;

public partial class CustPart : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //能导入或新增的人，才拥有删除权限
            gvlist.Columns[8].Visible = this.Security["10000050"].isValid ? this.Security["10000060"].isValid : false;
            gvlist.Columns[9].Visible = this.Security["10000050"].isValid ? this.Security["10000060"].isValid : false;
            gvlist.Columns[8].Visible = true;
            gvlist.Columns[9].Visible = true;

            //自动回复
            if (Request["Param"] != null)
            {
                BindGridView();
            }
            if (Request["cust"] != null)
            {
                txtCust.Text = Request["cust"].ToString().Trim();
                txtPart.Text = Request["custPart"].ToString().Trim();
                txtQad.Text = Request["QAD"].ToString().Trim();
                BindGridView();
            }
        }
    }
    protected override void BindGridView()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@cust", txtCust.Text.Trim());
            param[1] = new SqlParameter("@custPart", txtPart.Text.Trim());
            param[2] = new SqlParameter("@qad", txtQad.Text.Trim());
            param[3] = new SqlParameter("@stdDate", txtStdDate.Text.Trim());
            param[4] = new SqlParameter("@endDate", txtEndDate.Text.Trim());
            param[5] = new SqlParameter("@domain", dropDomain.SelectedValue);
            DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectCustPart", param);
            gvlist.DataSource = ds;
            gvlist.DataBind();
        }
        catch (Exception ee)
        { ;}
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string qad = e.Row.Cells[2].Text;
            if (qad.Substring(0, 1) == "1" && qad.Substring(qad.Length - 1) == "0")
            {
                e.Row.Cells[9].Text = "";
            }
        }
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtStdDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtStdDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期格式不正确!正确格式如：2012-01-01');";
                return;
            }
        }
        if (txtEndDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtEndDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!正确格式如：2012-01-01');";
                return;
            }
        }
        BindGridView();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        //ltlAlert.Text = "window.open('CustPartExport.aspx?cust=" + txtCust.Text.Trim() + "&part=" + txtPart.Text.Trim() + "&qad=" + txtQad.Text.Trim() + "&stdDate=" + txtStdDate.Text.Trim() + "&endDate=" + txtEndDate.Text.Trim() + "&rt=" + DateTime.Now.ToString() + "', '_blank');";
        DataTable dt = GetExcelImport();
        string title = "120^<b>客户/货物发往</b>~^120^<b>客户物料</b>~^120^<b>物料号</b>~^80^<b>生效日期</b>~^80^<b>截止日期</b>~^180^<b>说明</b>~^100^<b>显示客户物料</b>~^100^<b>SKU</b>~^100^<b>UL信息</b>~^";
        ExportExcel(title, dt, false);
    }
    protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", gvlist.DataKeys[e.RowIndex].Values["cp_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            param[3] = new SqlParameter("@retValue", SqlDbType.NVarChar, 50);
            param[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_deleteCustPart", param);
            string error = param[3].Value.ToString();
            if (param[3].Value.ToString() != string.Empty)
            {
                ltlAlert.Text = "alert('" + param[3].Value.ToString() + "');";
            }
        }
        catch
        {
            ltlAlert.Text = "alert('删除失败！请刷新后重新操作一次！');";
        }

        BindGridView();
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string cpId = gvlist.DataKeys[index].Values["cp_id"].ToString();
            this.Redirect("CustPartEdit.aspx?id=" + cpId + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    public DataTable GetExcelImport()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@cust", txtCust.Text.Trim());
            param[1] = new SqlParameter("@custPart", txtPart.Text.Trim());
            param[2] = new SqlParameter("@qad", txtQad.Text.Trim());
            param[3] = new SqlParameter("@stdDate", txtStdDate.Text.Trim());
            param[4] = new SqlParameter("@endDate", txtEndDate.Text.Trim());
            param[5] = new SqlParameter("@domain", dropDomain.SelectedValue);
            DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectCustPartimport", param);
            return ds.Tables[0];
        }
        catch (Exception ee)
        {
            return null;
        }
    }
}
