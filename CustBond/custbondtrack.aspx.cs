using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Data;


public partial class CustBond_custbondtrack : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year - 1, 1, 1);
            date.Value = d1.ToString("yyyy-MM-dd");
            date1.Value = now.ToString("yyyy-MM-dd");
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
    }  /*数据由前台AJAX取*/

    protected void btnExport_Click(object sender, EventArgs e)
    {
        String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@number", number.Value);
        param[1] = new SqlParameter("@part", part.Value);
        param[2] = new SqlParameter("@date", date.Value);
        param[3] = new SqlParameter("@date1", date1.Value);
        param[4] = new SqlParameter("@bstatus", bstatus.Value);
        DataTable _Table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_custbond_trackexport", param).Tables[0];

        string title = "150^<b>海关手册号</b>~^180^<b>备注信息</b>~^80^<b>采购单</b>~^50^<b>行</b>~^50^<b>地点</b>~^100^<b>零件号</b>~^100^<b>订单数量</b>~^100^<b>收货数量</b>~^100^<b>出运核销数量</b>~^100^<b>单价(USD)</b>~^80^<b>订单日期</b>~^" +
        "80^<b>销售订单</b>~^50^<b>行</b>~^80^<b>销往</b>~^80^<b>运往</b>~^160^<b>名称</b>~^100^<b>出运核销数量</b>~^80^<b>出运日期</b>~^80^<b>计算日期</b>~^";

        if (_Table != null && _Table.Rows.Count > 0)
        {
            ExportExcel(title, _Table, false);
        }
        else
        {
            ltlAlert.Text = "alert('没有查询到可以导出的数据！');";
            return;
        }

    }
}