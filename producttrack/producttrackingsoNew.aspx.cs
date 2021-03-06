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
using QCProgress;
using CommClass;
using System.IO;


public partial class producttrackingso : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected override void BindGridView()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@domain", dropDomain.SelectedValue);
            param[1] = new SqlParameter("@part", txtQad.Text.Trim());
            param[2] = new SqlParameter("@nbr1", txtNbr1.Text.Trim());
            param[3] = new SqlParameter("@nbr2", txtNbr2.Text.Trim());
            param[4] = new SqlParameter("@dueDate1", txtDueDate1.Text.Trim());
            param[5] = new SqlParameter("@dueDate2", txtDueDate2.Text.Trim());

            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, "sp_selectProductTrackingSONew", param);

            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        { }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDueDate1.Text))
        {
            if (!this.IsDate(txtDueDate1.Text))
            {
                ltlAlert.Text = "alert('截至日期 的格式不正确!')";
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtDueDate2.Text))
        {
            if (!this.IsDate(txtDueDate2.Text))
            {
                ltlAlert.Text = "alert('截至日期 的格式不正确!')";
                return;
            }
        }

        BindGridView();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Link")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            this.Redirect("/QadDoc/qad_bomviewdoc.aspx?cmd=newso&part=" + gv.DataKeys[index].Values["sod_part"].ToString());
        }
    }
    protected void btnExportError_Click(object sender, EventArgs e)
    {
        string ExtTitle = "100^<b>整灯QAD</b>~^200^<b>整灯部件号</b>~^100^<b>子键QAD</b>~^200^<b>子键部件号</b>~^100^<b>实际文档数</b>~^100^<b>需要文档数</b>~^";
        string ExtSQL = @"select distinct product, pro_code, ps_comp, ps_code, Isnull(cnt_fact, 0), Isnull(cnt_need, 0)
                          from qaddoc..ProductTrackingNew
                          where exists(select * from qad_data..sod_det where sod_domain in ('szx', 'zqz') and sod_part = product)
                            And Isnull(cnt_fact, 0) < isnull(cnt_need, 0)
                            And isnull(cnt_need, 0) > 0";

        this.ExportExcel(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"]
            , ExtTitle
            , ExtSQL
            , false);
    }
}
