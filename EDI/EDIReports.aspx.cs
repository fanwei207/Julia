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

public partial class EDI_EDIReports : BasePage
{
    public string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_edi"];

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnExport1_Click(object sender, EventArgs e)
    {
        DataTable table = null;
        DateTime _stdDate = DateTime.Now;
        DateTime _endDate = DateTime.Now;

        if (string.IsNullOrEmpty(txtPoRecDate1.Text))
        {
            this.Alert("日期1 不能为空！");
            return;
        }
        else if (!this.IsDate(txtPoRecDate1.Text))
        {
            this.Alert("日期1 格式不正确！");
            return;
        }

        _stdDate = Convert.ToDateTime(txtPoRecDate1.Text);

        if (!string.IsNullOrEmpty(txtPoRecDate2.Text))
        {
            if (!this.IsDate(txtPoRecDate2.Text))
            {
                this.Alert("日期2 格式不正确！");
                return;
            }

            _endDate = Convert.ToDateTime(txtPoRecDate2.Text);
        }

        try
        {
            string strSql = "Select * From EDI_Rep_PoNotInQad Where poRecDate >= '" + string.Format("{0:yyyy-MM-dd}", _stdDate) + "' And poRecDate < '" + string.Format("{0:yyyy-MM-dd}", _endDate) + "'";

            table = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql).Tables[0];

            if (table.Rows.Count > 0)
            {
                string EXTitle = "<b>订单</b>~^<b>FOB</b>~^<b>客户</b>~^<b>发货至</b>~^<b>销售单</b>~^<b>接收日期</b>~^<b>导入日期</b>~^<b>域</b>~^";

                this.ExportExcel(EXTitle, table, true);
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable table = null;
        DateTime _stdDate = DateTime.Now;
        DateTime _endDate = DateTime.Now;

        if (string.IsNullOrEmpty(txtOrdDate1.Text))
        {
            this.Alert("日期1 不能为空！");
            return;
        }
        else if (!this.IsDate(txtOrdDate1.Text))
        {
            this.Alert("日期1 格式不正确！");
            return;
        }

        _stdDate = Convert.ToDateTime(txtOrdDate1.Text);

        if (!string.IsNullOrEmpty(txtOrdDate2.Text))
        {
            if (!this.IsDate(txtOrdDate2.Text))
            {
                this.Alert("日期2 格式不正确！");
                return;
            }

            _endDate = Convert.ToDateTime(txtOrdDate2.Text);
        }

        try
        {
            string strSql = "sp_edi_rep_selectQadSoNotInEDI";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@stdDate", _stdDate);
            param[1] = new SqlParameter("@endDate", _endDate);

            table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];

            if (table.Rows.Count > 0)
            {
                string EXTitle = "<b>销售单</b>~^<b>客户订单</b>~^<b>客户</b>~^<b>发货至</b>~^<b>订单日期</b>~^<b>截止日期</b>~^<b>域</b>~^<b>在EDI中</b>~^<b>EDI销售单</b>~^<b>EDI备份销售单</b>~^";

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
    protected void Button2_Click(object sender, EventArgs e)
    { 
        try
        {
            string strSql = "sp_edi_rep_selectJdeNotInEDI";

            DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql).Tables[0];

            if (table.Rows.Count > 0)
            {
                string EXTitle = "<b>订单号</b>~^<b>订单日期</b>~^<b>ShipVia</b>~^<b>订单行</b>~^<b>销售订单</b>~^<b>行</b>~^<b>客户订单号</b>~^";

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
    protected void Button3_Click(object sender, EventArgs e)
    {
        DataTable table = null;
        DateTime _stdDate = DateTime.Now;
        DateTime _endDate = DateTime.Now;

        if (string.IsNullOrEmpty(TextBox3.Text))
        {
            this.Alert("日期1 不能为空！");
            return;
        }
        else if (!this.IsDate(TextBox3.Text))
        {
            this.Alert("日期1 格式不正确！");
            return;
        }

        _stdDate = Convert.ToDateTime(TextBox3.Text);

        if (!string.IsNullOrEmpty(TextBox4.Text))
        {
            if (!this.IsDate(TextBox4.Text))
            {
                this.Alert("日期2 格式不正确！");
                return;
            }

            _endDate = Convert.ToDateTime(TextBox4.Text);
        }

        try
        {
            string strSql = "sp_edi_rep_selectEDINotInHIST";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@stdDate", _stdDate);
            param[1] = new SqlParameter("@endDate", _endDate);

            table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];

            if (table.Rows.Count > 0)
            {
                string EXTitle = "<b>订单号</b>~^<b>客户</b>~^<b>到达日期</b>~^";

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