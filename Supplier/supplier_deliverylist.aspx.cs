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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using CommClass;

public partial class supplier_deliverylist : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtInspDateStart.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
            txtInspDateEnd.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(txtInspDateStart.Text).AddMonths(1));
        }
    }

    protected DataSet GetData()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@nbr", txtNbr.Text.Trim());
            param[1] = new SqlParameter("@delivery", txtDelivery.Text.Trim());
            param[2] = new SqlParameter("@InspDateStart", txtInspDateStart.Text.Trim());
            param[3] = new SqlParameter("@InspDateEnd", txtInspDateEnd.Text.Trim());
            param[4] = new SqlParameter("@qcDateStart", txtQcDateStart.Text.Trim());
            param[5] = new SqlParameter("@qcDateEnd", txtQcDateEnd.Text.Trim());
            param[6] = new SqlParameter("@RcpDateStart", txtRcpDateStart.Text.Trim());
            param[7] = new SqlParameter("@RcpDateEnd", txtRcpDateEnd.Text.Trim());
            param[8] = new SqlParameter("@vend", txtVend.Text.Trim());

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.TCPC_Supplier"), CommandType.StoredProcedure, "dbo.sp_supp_selectDelivery", param);
        }
        catch
        {
            return null;
        }
    }

    protected void BindData()
    {
        dgLocation.DataSource = this.GetData();
        dgLocation.DataBind();
    }
    protected void dgLocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void dgLocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgLocation.PageIndex = e.NewPageIndex;

        BindData();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable table = this.GetData().Tables[0];

        if (table == null || table.Rows.Count <= 0)
        {
            this.Alert("û�����ݿɹ�������");
        }
        else
        {
            string _EXTitle = "<b>��Ӧ��</b>~^<b>�ͻ���</b>~^<b>ԭʼ����</b>~^<b>�ɹ���</b>~^<b>�볧����</b>~^<b>����������</b>~^<b>������</b>~^<b>�ʼ�����</b>~^<b>���Ʒ������</b>~^<b>��</b>~^<b>�ص�</b>~^<b>������</b>~^";
            this.ExportExcel(_EXTitle, table, false);
        }
    }
    protected DataSet SelectNoInSp()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RcpDateStart", txtRcpDateStart.Text.Trim());
            param[1] = new SqlParameter("@RcpDateEnd", txtRcpDateEnd.Text.Trim());

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.TCPC_Supplier"), CommandType.StoredProcedure, "sp_supp_selectNoInSpDelivery", param);
        }
        catch
        {
            return null;
        }

    }

    protected void btn_exportNoInSp_Click(object sender, EventArgs e)
    {
        DataTable table = this.SelectNoInSp().Tables[0];

        if (table == null || table.Rows.Count <= 0)
        {
            this.Alert("û�����ݿɹ�������");
        }
        else
        {
            string _EXTitle = "<b>�ͻ���</b>~^<b>�ɹ���</b>~^<b>��</b>~^<b>�ص�</b>~^<b>ɨ����</b>~^<b>��������</b>~^";
            this.ExportExcel(_EXTitle, table, true);
        }


    }
}
