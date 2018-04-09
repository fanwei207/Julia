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

public partial class wo2_MgMiInOut : BasePage
{
    adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEffDate1.Text = string.Format("{0:yyyy-MM-1}", DateTime.Now);
        }
    }

    protected DataTable GetProcInOutData()
    {
        DateTime _stdDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", DateTime.Now));
        DateTime _endDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(1)));

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
            string strSql = "sp_wo2_t_selectProcInOut";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@type", dropType.SelectedValue);
            param[1] = new SqlParameter("@domain", dropDomain.SelectedValue);
            param[2] = new SqlParameter("@workshop", txtWorkShop.Text);
            param[3] = new SqlParameter("@date1", string.Format("{0:yyyy-MM-dd}", _stdDate));
            param[4] = new SqlParameter("@date2", string.Format("{0:yyyy-MM-dd}", _endDate));

            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            this.Alert("获取数据失败！请联系管理员！");
            return null;
        }
    }

    protected DataTable GetInOutExportProc(string type)
    { 
        try
        {
            string strSql = "sp_wo2_t_selectInOutProc";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@type", dropType.SelectedValue);
            param[1] = new SqlParameter("@domain", dropDomain.SelectedValue);

            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            this.Alert("获取数据失败！请联系管理员！");
            return null;
        }
    }

    protected bool CheckInOutProc(string type, string domain)
    {
        try
        {
            string strSql = "sp_wo2_t_checkPCBInOutProc";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@type", type);
            param[1] = new SqlParameter("@domain", domain);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adm.dsn0(), CommandType.StoredProcedure, strSql, param);

            return Convert.ToBoolean(param[2].Value);
        }
        catch
        {
            return false;
        }
    }

    protected void btnExport1_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex == 0)
        {
            this.Alert("请选择一个类别！");
            return;
        }

        if (dropDomain.SelectedIndex == 0)
        {
            this.Alert("请选择一个公司！");
            return;
        }

        DataTable table = GetProcInOutData();
        if (table.Rows.Count > 0)
        {
            string EXTitle = "<b>日期</b>~^<b>公司</b>~^<b>车间</b>~^<b>工单</b>~^<b>ID</b>~^150^<b>物料</b>~^<b>原工序</b>~^<b>工序</b>~^<b>工段长</b>~^<b>生产线</b>~^<b>线长</b>~^<b>工单数</b>~^<b>入库数</b>~^<b>投入量</b>~^<b>产出量</b>~^<b>消耗物料</b>~^<b>消耗数量</b>~^<b>缺陷原因</b>~^<b>责任人工号</b>~^<b>责任人姓名</b>~^<b>供应商代码</b>~^";

            this.ExportExcel(EXTitle, table, true);
        }
        else
        {
            this.Alert("没有数据需要导出的！");
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex == 0)
        {
            this.Alert("请选择一个类别！");
            return;
        }

        if (dropDomain.SelectedIndex == 0)
        {
            this.Alert("请选择一个公司！");
            return;
        }

        gv.DataSource = GetProcInOutData();
        gv.DataBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        gv.DataSource = GetProcInOutData();
        gv.DataBind();
    }
    protected void btnExport2_Click(object sender, EventArgs e)
    {
        if (!CheckInOutProc(dropType.SelectedValue, dropDomain.SelectedValue))
        {
            this.Alert("尚未维护工序，或没有设置首道、末道、退次工序！");
            return;
        }

        if (dropType.SelectedIndex == 0)
        {
            this.Alert("请选择一个类别！");
            return;
        }

        if (dropDomain.SelectedIndex == 0)
        {
            this.Alert("请选择一个公司！");
            return;
        }

        if (string.IsNullOrEmpty(txtWorkShop.Text))
        {
            this.Alert("车间不能为空！");
            return;
        }

        DateTime _stdDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", DateTime.Now));
        DateTime _endDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(1)));

        if (string.IsNullOrEmpty(txtEffDate1.Text))
        {
            this.Alert("结算日期1 不能为空！");
            return;
        }
        else if (!this.IsDate(txtEffDate1.Text))
        {
            this.Alert("结算日期1 格式不正确！");
            return;
        }

        _stdDate = Convert.ToDateTime(txtEffDate1.Text);

        if (!string.IsNullOrEmpty(txtEffDate2.Text))
        {
            if (!this.IsDate(txtEffDate2.Text))
            {
                this.Alert("结算日期2 格式不正确！");
                return;
            }

            _endDate = Convert.ToDateTime(txtEffDate2.Text);
        }

        try
        {
            string strSql = "sp_wo2_t_selectProcPCBInOutAnysis";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@type", dropType.SelectedValue);
            param[1] = new SqlParameter("@domain", dropDomain.SelectedValue);
            param[2] = new SqlParameter("@workshop", txtWorkShop.Text);
            param[3] = new SqlParameter("@date1", string.Format("{0:yyyy-MM-dd}", _stdDate));
            param[4] = new SqlParameter("@date2", string.Format("{0:yyyy-MM-dd}", _endDate));

            DataTable table = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param).Tables[0];

            if (table.Rows.Count > 0)
            {
                DataTable procTable = GetInOutExportProc(dropType.SelectedValue);

                if (procTable == null || procTable.Rows.Count <= 0)
                {
                    this.Alert("请先维护好工序！");
                }
                else
                {
                    string EXTitle = "<b>生产线</b>~^150^<b>QAD</b>~^<b>工单</b>~^<b>ID号</b>~^<b>工单数</b>~^<b>入库数</b>~^<b>结算日期</b>~^<b>系统发料</b>~^";
                    foreach (DataRow row in procTable.Rows)
                    {
                        if (row["p_proc"].ToString() == "退次")
                        {
                            EXTitle += "<b>退次</b>~^";
                        }
                        else
                        {
                            EXTitle += "<b>" + row["p_proc"].ToString() + "（入）</b>~^<b>" + row["p_proc"].ToString() + "（出）</b>~^<b>" + row["p_proc"].ToString() + "（合格率）</b>~^";
                        }
                    }
                    EXTitle += "<b>总合格率</b>~^";

                    this.ExportExcel(EXTitle, table, false);
                }
            }
            else
            {
                this.Alert("没有数据需要导出的！");
            }
        }
        catch
        {
            this.Alert("获取数据失败！请联系管理员！");
        }
    }
    protected void btnExport3_Click(object sender, EventArgs e)
    {
        if (!CheckInOutProc(dropType.SelectedValue, dropDomain.SelectedValue))
        {
            this.Alert("尚未维护工序，或没有设置首道、末道、退次工序！");
            return;
        }

        if (dropType.SelectedIndex == 0)
        {
            this.Alert("请选择一个类别！");
            return;
        }

        if (dropDomain.SelectedIndex == 0)
        {
            this.Alert("请选择一个公司！");
            return;
        }

        if (string.IsNullOrEmpty(txtWorkShop.Text))
        {
            this.Alert("车间不能为空！");
            return;
        }

        DateTime _stdDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", DateTime.Now));
        DateTime _endDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(1)));

        if (!string.IsNullOrEmpty(txtEffDate1.Text))
        {
            if (!this.IsDate(txtEffDate1.Text))
            {
                this.Alert("结算日期1 格式不正确！");
                return;
            }

            _stdDate = Convert.ToDateTime(txtEffDate1.Text);
        }

        if (!string.IsNullOrEmpty(txtEffDate2.Text))
        {
            if (!this.IsDate(txtEffDate2.Text))
            {
                this.Alert("结算日期2 格式不正确！");
                return;
            }

            _endDate = Convert.ToDateTime(txtEffDate2.Text);
        }

        if (_stdDate >= _endDate)
        {
            this.Alert("结算日期2 不能早于 结算日期1！");
            return;
        }

        try
        {
            string strSql = "sp_wo2_t_selectProcPCBInOutQX";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@type", dropType.SelectedValue);
            param[1] = new SqlParameter("@domain", dropDomain.SelectedValue);
            param[2] = new SqlParameter("@workshop", txtWorkShop.Text);
            param[3] = new SqlParameter("@date1", string.Format("{0:yyyy-MM-dd}", _stdDate));
            param[4] = new SqlParameter("@date2", string.Format("{0:yyyy-MM-dd}", _endDate));

            DataTable table = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql, param).Tables[0];

            if (table.Rows.Count > 0)
            {
                string EXTitle = "<b>车间</b>~^150^<b>工号</b>~^<b>姓名</b>~^<b>缺陷种类</b>~^<b>缺陷总数</b>~^";

                _stdDate = Convert.ToDateTime(string.Format("{0:yyyy-MM-01}", _stdDate));
                _endDate = _stdDate.AddMonths(1);

                while (_stdDate < _endDate)
                {
                    EXTitle += "<b>" + string.Format("{0:yyyy-MM-dd}", _stdDate) + "</b>~^";

                    _stdDate = _stdDate.AddDays(1);
                }

                this.ExportExcel(EXTitle, table, false);
            }
            else
            {
                this.Alert("没有数据需要导出的！");
            }
        }
        catch
        {
            this.Alert("获取数据失败！请联系管理员！");
        }
    }
}