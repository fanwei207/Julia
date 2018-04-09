using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
using CommClass;
using System.Data;

public partial class EDI_EDIPOExceptionList : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDDLExceptionCode();
            bind();
        }
    }

    private void bindDDLExceptionCode()
    {
        DataTable dt = getExceptionCodeList();
        ddlExceptionCode.DataSource = dt;
        ddlExceptionCode.DataBind();
        ddlExceptionCode.Items.Insert(0, new ListItem("--", "-1"));

    }

    private DataTable getExceptionCodeList()
    {
        string sqlstr = "SELECT code+'---'+description AS DESCRIPTION , code FROM dbo.EDIPOErrorConfigure";

        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.Text, sqlstr).Tables[0];
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }

    private void bind()
    {



        DataTable dt = getExceptionList();
        gv.DataSource = dt;
        gv.DataBind();
    }

    private DataTable getExceptionList()
    {
        string sqlstr = "sp_EDI_EDIPOSelectExceptionList";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PoNbr",txtPoNbr.Text.Trim()),
            new SqlParameter("@custNo",txtCustNo.Text.Trim()),
            new SqlParameter("@OwnName",txtOwnName.Text.Trim()),
            new SqlParameter("@CustName",txtCustName.Text.Trim()),
            new SqlParameter("@exceptionCode",ddlExceptionCode.SelectedValue),
          
        };

        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable errDt = getExceptionList();//输出错误信息
        string title = "100^<b>Order NO</b>~^100^<b>Order Date</b>~^100^<b>Customer Name</b>~^100^<b>Area</b>~^100^<b>Line</b>~^100^<b>Item</b>~^100^<b>QTY</b>~^100^<b>unit Price</b>~^100^<b>Total Cost</b>~^100^<b>Order Requst Date</b>~^100^<b>Owner</b>~^100^<b>Reason Code</b>~^100^<b>Start</b>~^";
        if (errDt != null && errDt.Rows.Count > 0)
        {
            ExportExcel(title, errDt, false);
        }
    }

}