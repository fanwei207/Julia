using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class wo_actualReleaseZq : BasePage
{
    private wo.Wo_ActualRelease helper = new wo.Wo_ActualRelease();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtActDateFrom.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-2));

            BindData();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string woNbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string relDateFrom = txtDateFrom.Text.Trim();
        string relDateTo = txtDateTo.Text.Trim();
        string actDateFrom = txtActDateFrom.Text.Trim();
        string actDateTo = txtActDateTo.Text.Trim();
        string domain = ddlDomain.SelectedItem.Text;
        gvlist.DataSource = helper.GetWoActRelListZq(woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain, txtZq.Text);
        gvlist.DataBind();
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        string woNbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string relDateFrom = txtDateFrom.Text.Trim();
        string relDateTo = txtDateTo.Text.Trim();
        string actDateFrom = txtActDateFrom.Text.Trim();
        string actDateTo = txtActDateTo.Text.Trim();
        string domain = ddlDomain.SelectedItem.Text;
        DataTable dt = helper.GetWoActRelList(woNbr, part, relDateFrom, relDateTo, actDateFrom, actDateTo, domain);
        //添加“确认信息”列
        DataColumn dc = dt.Columns.Add("confirmInfo", typeof(System.String));
        dc.AllowDBNull = true;

        foreach(DataRow dr in dt.Rows)
        {
            domain = dr["wo_domain"].ToString();
            woNbr = dr["wo_nbr"].ToString();
            string lot = dr["wo_lot"].ToString();
            string bom = dr["wo_bom_code"].ToString();

            dr["confirmInfo"] = helper.GetWoMstrULInfo(domain, woNbr, lot, bom); 
        }

        string title = "100^<b>加工单</b>~^100^<b>ID</b>~^120^<b>QAD</b>~^100^<b>QAD下达日期</b>~^100^<b>计划日期</b>~^100^<b>评审日期</b>~^100^<b>上线日期</b>~^100^<b>地点</b>~^100^<b>生产线</b>~^100^<b>成本中心</b>~^100^<b>工厂</b>~^100^<b>周期章</b>~^100^<b>确认信息</b>~^";
        ExportExcel(title, dt, false);
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            string _domain = rowView["wo_domain"].ToString();
            string _nbr = rowView["wo_nbr"].ToString();
            string _lot = rowView["wo_lot"].ToString();
            string _bom = rowView["wo_bom_code"].ToString();

            e.Row.Cells[12].Text = helper.GetWoMstrULInfo(_domain, _nbr, _lot, _bom);
        }
    }
}