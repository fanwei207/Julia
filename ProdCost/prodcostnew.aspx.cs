using System;
using System.Data;
using System.Text;
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
using OEAppServer;

public partial class prodcostnew : BasePage
{
        AppServer appsv = new AppServer();
        String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn_rdw"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                rate.Value = appsv.getcurrate();
            }
        }

        protected void getpart()
        {
            if (project_code.Value.Trim() != String.Empty)
            {
                String strSQL = "SELECT TOP 1 prod_QAD FROM prod_mstr where prod_Code = '" + project_code.Value.Trim() + "' order by prod_id desc";
                part_code.Value = Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.Text, strSQL));
                /*为了安全应该改成stored procedure*/
            }
        }
        protected DataTable generatedata()
        {
            DataTable _Table = appsv.prodcost(part_code.Value.Trim(), Convert.ToDecimal(tax.Value.Trim()), Convert.ToDecimal(rate.Value.Trim()), Convert.ToDecimal(dif.Value.Trim()));

            DataColumn dc0 = new DataColumn();
            dc0.DataType = System.Type.GetType("System.Decimal");
            dc0.Caption = "strun";
            dc0.ColumnName = "strun";
            dc0.Expression = "psqty * psrun";
            _Table.Columns.Add(dc0);

            DataColumn dc1 = new DataColumn();
            dc1.DataType = System.Type.GetType("System.Decimal");
            dc1.Caption = "mtl";
            dc1.ColumnName = "mtl";
            dc1.Expression = "psqty * cur_mtl_tl";
            _Table.Columns.Add(dc1);

            DataColumn dc2 = new DataColumn();
            dc2.DataType = System.Type.GetType("System.Decimal");
            dc2.Caption = "lbr";
            dc2.ColumnName = "lbr";
            dc2.Expression = "psqty * psrun * pslbr";
            _Table.Columns.Add(dc2);

            DataColumn dc3 = new DataColumn();
            dc3.DataType = System.Type.GetType("System.Decimal");
            dc3.Caption = "bdn";
            dc3.ColumnName = "bdn";
            dc3.Expression = "psqty * psrun * psbdn";
            _Table.Columns.Add(dc3);

            /*_Table.Columns.Remove("");*/
            return _Table;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.getpart();
        }   /*取数据用AJAX代替*/

        protected void btnExport_Click(object sender, EventArgs e)
        {
            this.getpart();

            if (part_code.Value != String.Empty)
            {
            DataTable _Table = this.generatedata();

            string title = "50^<b>层级</b>~^80^<b>分类</b>~^100^<b>父零件</b>~^100^<b>子零件</b>~^250^<b>描述</b>~^50^<b>状态</b>~^50^<b>本层用量</b>~^50^<b>单位用量</b>~^50^<b>币种</b>~^50^<b>原币价格</b>~^50^<b>单位价格</b>~^" +
            "250^<b>供应商</b>~^100^<b>默认工艺流程</b>~^80^<b>工作中心</b>~^100^<b>描述</b>~^80^<b>保税材料</b>~^300^<b>警告</b>~^80^<b>单位工时</b>~^80^<b>标准工时</b>~^80^<b>材料</b>~^80^<b>人工</b>~^80^<b>费用</b>~^";

            if (_Table != null && _Table.Rows.Count > 0)
            {
                ExportExcel(title, _Table, false);
            }
            else
            {
                ltlAlert.Text = "alert('物料" + part_code.Value.Trim() + "不存在成本明细，请检查基础数据和结构维护是否完整！');";
                return;
            }
            }
        } /*EXCEL 导出*/
}
