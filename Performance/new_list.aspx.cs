using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;


public partial class Performance_new_list : BasePage
{
    adamClass adam = new adamClass();

    protected override void OnInit(EventArgs e)
    {
        this.Security.Register("89500410", "考核维护（拥有考核维护权限的人，才可以编辑）");
        
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //考核日期默认
            txtStdDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-2));
            txtEndDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1));
            //绑定类型
            dropType.DataSource = this.GetPerformanceType();
            dropType.DataBind();
            dropType.Items.Insert(0, new ListItem("--全部--", "0"));

            BindData();
        }
    }

    protected DataTable GetPerformanceType()
    {
        try
        {
            string strSql = "sp_perf_selectPerformanceType";

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected DataTable GetPerformance()
    {
        try
        {
            string strSql = "sp_perf_selectPerformance";
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@domain", dropPlant.SelectedIndex == 0 ? "All" : dropPlant.SelectedItem.Text);
            parms[1] = new SqlParameter("@stdDate", txtStdDate.Text.Trim());
            parms[2] = new SqlParameter("@enddate", txtEndDate.Text.Trim());
            parms[3] = new SqlParameter("@type", dropType.SelectedIndex == 0 ? "全部" : dropType.SelectedValue);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parms).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void BindData()
    {
        gv.DataSource = this.GetPerformance();
        gv.DataBind();
    }
    protected void btnQeury_Click(object sender, EventArgs e)
    {
        if (!this.IsDate(txtStdDate.Text.Trim()))
        {
            this.Alert("日期格式不正确！");
            return;
        }

        if (!this.IsDate(txtEndDate.Text.Trim()))
        {
            this.Alert("日期格式不正确！");
            return;
        }

        BindData();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Process")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string _id = gv.DataKeys[index].Values["perf_id"].ToString();
            string _domain = gv.Rows[index].Cells[1].Text;

            Response.Redirect("new_solution.aspx?id=" + _id + "&domain=" + _domain + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
        else if (e.CommandName == "Maint")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string _id = gv.DataKeys[index].Values["perf_id"].ToString();

            Response.Redirect("new_app.aspx?id=" + _id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!this.Security["89500430"].isValid)
            {
                LinkButton linkSolution = (LinkButton)e.Row.FindControl("linkSolution");
                linkSolution.Enabled = false;
            }

            if (!this.Security["89500410"].isValid)
            {
                LinkButton linkMaint = (LinkButton)e.Row.FindControl("linkMaint");
                linkMaint.Enabled = false;
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string EXTitle = "<b>日期</b>~^<b>地区</b>~^<b>考核类型</b>~^<b>部门</b>~^<b>责任人</b>~^<b>职位</b>~^<b>处理规定</b>~^<b>扣分考核</b>~^<b>记过考核</b>~^<b>人事考核</b>~^<b>备注</b>~^";
        EXTitle += "<b>实际负责人</b>~^<b>原因</b>~^<b>整改方案</b>~^";

        DataTable table = this.GetPerformance();

        if (table == null || table.Rows.Count == 0)
        {
            this.Alert("无数据可供导出！");
        }
        else
        {
            this.ExportExcel(EXTitle, table, false);
        }
    }
}