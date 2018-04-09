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
using Microsoft.ApplicationBlocks.Data;
using CommClass;
using System.Data.SqlClient;
using adamFuncs;

public partial class wo2_wouserquery : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            binddata();
        }
    }

    protected void binddata()
    {
        DataSet ds = getWorkOrder(txt_domain.Text.Trim(), txt_wo_nbr.Text.Trim(), txt_wo_lot.Text.Trim(), txt_wo_process.Text.Trim(), txt_Fingerprint.Text.Trim(), txt_userNo.Text.Trim(), txt_userName.Text.Trim(), txt_CreatedDate.Text.Trim());

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
        }
        if (ds.Tables[0].Rows.Count == 0)
        {
            int columnCount = gvWorkOrder.Rows[0].Cells.Count;
            gvWorkOrder.Rows[0].Cells.Clear();
            gvWorkOrder.Rows[0].Cells.Add(new TableCell());
            gvWorkOrder.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvWorkOrder.Rows[0].Cells[0].Text = "No Data";
            gvWorkOrder.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }

        gvWorkOrder.DataSource = ds;
        gvWorkOrder.DataBind();
    }

    public static DataSet getWorkOrder(string domain, string wo_nbr, string wo_lot, string wo_process, string fingerprint, string userNo, string userName, string createdDate)
    {
        SqlParameter[] param = new SqlParameter[8];
        param[0] = new SqlParameter("@domain", domain);
        param[1] = new SqlParameter("@wo_nbr", wo_nbr);
        param[2] = new SqlParameter("@wo_lot", wo_lot);
        param[3] = new SqlParameter("@wo_process", wo_process);
        param[4] = new SqlParameter("@fingerprint", fingerprint);
        param[5] = new SqlParameter("@userNo", userNo);
        param[6] = new SqlParameter("@userName", userName);
        param[7] = new SqlParameter("@createdDate", createdDate);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "BarCodeSys.dbo.sp_wo_selectWorkOrderInfo2", param);
        return ds;
    }


    protected void btn_queryClick(object sender, EventArgs e)
    {
        binddata();
    }

    protected void myPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWorkOrder.PageIndex = e.NewPageIndex;
        this.binddata();
    }

    protected void gvWorkOrder_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }
}
