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
using System.IO;

public partial class Purchase_PO_CofirmExport : BasePage
{
    adamFuncs.adamClass adam = new adamFuncs.adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddl_po_domainLoad();
            txt_po_ord_date1.Text = System.DateTime.Today.AddDays(1 - System.DateTime.Today.Day).ToString("yyyy-MM-dd"); 
            BindData();
        }
       
    }
    private void ddl_po_domainLoad()
    {
        String strSql = " select plantID,plantCode,description from tcpc0.dbo.Plants where isAdmin = 0 ";
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];

        ddl_po_domain.DataSource = dt;
        ddl_po_domain.DataBind();

        ListItem item = new ListItem("--", "");
        ddl_po_domain.Items.Insert(0, item);

        ddl_po_domain.SelectedValue = Session["PlantCode"].ToString();

    }
    private void BindData()
    {
        string strDomain = ddl_po_domain.SelectedItem.Text.ToString(); 
        string strVend = txt_po_vend.Text.ToString().Trim();
        string strPoNbr1 = txt_po_nbr1.Text.ToString().Trim();
        string strPoNbr2 = txt_po_nbr2.Text.ToString().Trim();
        string strPo_ord_date1 = txt_po_ord_date1.Text.ToString().Trim();
        string strPo_ord_date2 = txt_po_ord_date2.Text.ToString().Trim();
        string strPo_con_date1 = txt_po_con_date1.Text.ToString().Trim();
        string strPo_con_date2 = txt_po_con_date2.Text.ToString().Trim();
        bool bool_overNotConfirm = chk_overNotConfirm.Checked;//过期未确认
        bool bool_overConfirm = chk_overConfirm.Checked;//确认过期了

        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@domain", strDomain);
        param[1] = new SqlParameter("@vend", strVend);
        param[2] = new SqlParameter("@nbr1", strPoNbr1);
        param[3] = new SqlParameter("@nbr2", strPoNbr2);
        param[4] = new SqlParameter("@ord_date1", strPo_ord_date1);
        param[5] = new SqlParameter("@ord_date2", strPo_ord_date2);
        param[6] = new SqlParameter("@con_date1", strPo_con_date1);
        param[7] = new SqlParameter("@con_date2", strPo_con_date2);
        param[8] = new SqlParameter("@overNotConfirm", bool_overNotConfirm);
        param[9] = new SqlParameter("@overConfirm", bool_overConfirm);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_pur_selectSuppConfirmedPo", param).Tables[0];
        lbl_count.Text = "总共：" + dt.Rows.Count.ToString() + "条";

        gv.DataSource = dt;
        gv.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {
        if (lbl_count.Text == "0")
        {
            ltlAlert.Text = "alert('没有可导出的内容！')";
        }
        else
        {
            ltlAlert.Text = "window.open('/Purchase/PO_CofirmExport_export.aspx?domain=" + ddl_po_domain.SelectedItem.Text.ToString() +
                "&vend=" + txt_po_vend.Text.ToString().Trim() + "&nbr1=" + txt_po_nbr1.Text.ToString().Trim() + "&nbr2=" + txt_po_nbr2.Text.ToString().Trim() +
                "&ordDate1=" + txt_po_ord_date1.Text.ToString().Trim() + "&ordDate2=" + txt_po_ord_date2.Text.ToString().Trim() +
                "&conDate1=" + txt_po_con_date1.Text.ToString().Trim() + "&conDate2=" + txt_po_con_date2.Text.ToString().Trim() +
                "&overNotConfirm=" + chk_overNotConfirm.Checked.ToString() + "&overConfirm=" + chk_overConfirm.Checked.ToString() + "&rt=" + DateTime.Now.ToString() + "', '_blank');";
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);

    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        this.BindData();
    }

    protected void gv_PreRender(object sender, EventArgs e)
    {
        GridView gv1 = (GridView)sender;
        GridViewRow gv1r = (GridViewRow)gv1.BottomPagerRow;
        if (gv1r != null)
        {
            gv1r.Visible = true;
        }
    }
}
