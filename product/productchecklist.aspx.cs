using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class product_productchecklist : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@ParentCode", txbParentCode.Text.Trim());
        param[1] = new SqlParameter("@ChildCode", txbChildCode.Text.Trim());
        param[2] = new SqlParameter("@isChecked", chkCheckedItems.Checked);

        DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "Item_selectCheckProducts", param);

        gvCheckList.DataSource = ds;
        gvCheckList.DataBind();

        lblProdCount.Text = ds.Tables[0].Rows.Count.ToString();

        ds.Dispose();
    }

    protected void gvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }

    protected void gvCheckList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvCheckList.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (lblProdCount.Text == "0")
        {
            ltlAlert.Text = "alert('没有可导出的内容！')";
        }
        else
        {
            ltlAlert.Text = "window.open('/product/productchecklist_export.aspx?parentcode=" + txbParentCode.Text.Trim() + "&childcode=" + txbChildCode.Text.Trim() + "&rm=" + DateTime.Now.ToString() + "', '_blank');";
        }
    }

    protected void chkCheckedItems_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
