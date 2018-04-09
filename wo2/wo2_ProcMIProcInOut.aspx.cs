using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class wo2_MIProcInOut : BasePage
{
    adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEffDate1.Text = string.Format("{0:yyyy-MM-1}", DateTime.Now);
        }
    }

    protected DataTable GetMgProcInOutData()
    {
        DateTime _stdDate = DateTime.Now;
        DateTime _endDate = DateTime.Now;

        if (string.IsNullOrEmpty(txtEffDate1.Text))
        {
            this.Alert("结算日期1 不能为空！");
            return null;
        }
        else if (!this.IsDate(txtEffDate1.Text))
        {
            this.Alert("结算日期1 格式不正确！");
            return null;
        }

        _stdDate = Convert.ToDateTime(txtEffDate1.Text);

        if (!string.IsNullOrEmpty(txtEffDate2.Text))
        {
            if (!this.IsDate(txtEffDate2.Text))
            {
                this.Alert("结算日期2 格式不正确！");
                return null;
            }

            _endDate = Convert.ToDateTime(txtEffDate2.Text);
        }

        try
        {
            string strSql = "sp_wo2_rep_selectMiProcInOut";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@effDate1", string.Format("{0:yyyy-MM-dd}", _stdDate));
            param[1] = new SqlParameter("effDate2", string.Format("{0:yyyy-MM-dd}", _endDate));

            return SqlHelper.ExecuteDataset(adm.dsnx(), CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            this.Alert("获取数据失败！请联系管理员！");
            return null;
        }
    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        DataTable table = GetMgProcInOutData();
        if (table.Rows.Count > 0)
        {
            string EXTitle = "<b>域</b>~^<b>地点</b>~^<b>工单</b>~^<b>ID号</b>~^<b>QAD</b>~^<b>生产线</b>~^<b>工单数量</b>~^<b>入库数量</b>~^<b>结算日期</b>~^<b>工序</b>~^<b>名称</b>~^<b>投入</b>~^<b>投入来源</b>~^<b>产出</b>~^<b>提示信息</b>~^";

            this.ExportExcel(EXTitle, table, true);
        }
        else
        {
            this.Alert("没有数据需要导出的！");
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        gv.DataSource = GetMgProcInOutData();
        gv.DataBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        gv.DataSource = GetMgProcInOutData();
        gv.DataBind();
    }
}