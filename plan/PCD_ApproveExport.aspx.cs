using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class NWF_NWF_FlowExport : BasePage
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtEDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindGv();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSDate.Text.Trim().Length > 0)
            {
                DateTime dt1 = Convert.ToDateTime(txtSDate.Text);
                DateTime dt2 = Convert.ToDateTime(txtEDate.Text);
            }
        }
        catch
        {
            this.Alert("日期格式有误，请重新输入");
            return;
        }
        DataTable dt = GetPcdData(dropDomain.SelectedValue, txtSDate.Text, txtEDate.Text, txtQad.Text, txtNbr.Text, ddlType.SelectedValue, ddlStatus.SelectedValue);
        string EXTitle = "100^<b>加工单</b>~^<b>ID</b>~^150^<b>QAD</b>~^100^<b>下达日期</b>~^50^<b>订单数量</b>~^50^<b>域</b>~^50^<b>地点</b>~^80^<b>产线</b>~^120^<b>审批创建日期</b>~^200^<b>审批名称</b>~^200^<b>节点名称</b>~^120^<b>审批时间</b>~^120^<b>审批人</b>~^120^<b>审批状态</b>~^";
        string []keys={"wo_nbr","wo_lot"};
        this.ExportExcel(EXTitle, dt, true, 10, keys);
    }
    public  void BindGv()
    {
        try
        {
            if(txtSDate.Text.Trim().Length>0)
            {
                DateTime dt1 = Convert.ToDateTime(txtSDate.Text);
                DateTime dt2 = Convert.ToDateTime(txtEDate.Text);
            }
        }
        catch
        {
            this.Alert("日期格式有误，请重新输入");
            return;
        }
        gv.DataSource = GetPcdData(dropDomain.SelectedValue,txtSDate.Text,txtEDate.Text,txtQad.Text,txtNbr.Text,ddlType.SelectedValue,ddlStatus.SelectedValue);
        gv.DataBind();
    }
    public DataTable GetPcdData(string plant,string sDate, string eDate, string line, string nbr, string type,string status)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[4] = new SqlParameter("@plant", plant);
            param[0] = new SqlParameter("@nbr", nbr);
            param[1] = new SqlParameter("@type", type);
            param[2] = new SqlParameter("@startDate", sDate);
            param[3] = new SqlParameter("@endDate", eDate);
            param[5] = new SqlParameter("@qad", line);
            param[6] = new SqlParameter("@status", status);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_pcd_selectExpireData", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGv();
    }

}