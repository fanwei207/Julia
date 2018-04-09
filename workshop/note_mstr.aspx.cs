using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class workshop_note_mstr : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.BarCodeSys"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            BindGridView();
        }
    }
    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_note_selectNoteReport";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@plantCode", dropDomain.SelectedValue);
            param[1] = new SqlParameter("@date", txtDate.Text);
            param[2] = new SqlParameter("@user", txtUser.Text);
            param[3] = new SqlParameter("@dept", txtDept.Text);
            param[4] = new SqlParameter("@line", txtLine.Text);
            param[5] = new SqlParameter("@nbr", txtNbr.Text);

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDate.Text))
        {
            this.Alert("日期不能为空！");
            return;
        }
        else if (!this.IsDate(txtDate.Text))
        {
            this.Alert("日期 格式不正确！");
            return;
        }

        BindGridView();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _id = gv.DataKeys[index].Values["note_id"].ToString();
            this.Redirect("note_det.aspx?id=" + _id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        try
        {
            string strName = "sp_note_selectWorkLogReportInfo1";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@plantCode", dropDomain.SelectedValue);
            param[1] = new SqlParameter("@date", txtDate.Text);
            param[2] = new SqlParameter("@user", txtUser.Text);
            param[3] = new SqlParameter("@dept", txtDept.Text);
            param[4] = new SqlParameter("@line", txtLine.Text);
            param[5] = new SqlParameter("@nbr", txtNbr.Text);

            DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];

            string EXTitle = "40^<b>域</b>~^<b>车间</b>~^<b>产线</b>~^70^<b>日期</b>~^50^<b>线长</b>~^150^<b>工单</b>~^100^<b>事项</b>~^600^<b>内容</b>~^120^<b>完成时间</b>~^";
            this.ExportExcel(EXTitle, table, true, 7, "note_id", "ntp_type");
        }
        catch
        {
            this.Alert("导出失败！请联系管理员！");
        }

        
    }
}