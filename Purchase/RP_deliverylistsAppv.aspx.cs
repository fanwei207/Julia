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
using System.Reflection;
using System.IO;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;


public partial class Purchase_RP_deliverylistsAppv : BasePage
{
    public adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            Session["statusPostBack"] = 1;



            txtGenDate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);

            BindData();
        }
    }

    public void BindData()
    {
        String strVend = String.Empty;



        DataTable dt = GetDeliveryList(txtprd.Text,
                                                        txbpo.Text,
                                                        strVend,
                                                        ddlsite.SelectedItem.Text,
                                                        ddldomain.SelectedItem.Text,
                                                        txbord.Text,
                                                        txbdue.Text,
                                                        txtGenDate.Text,
                                                        dropStat.SelectedValue,
                                                        "");

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            dtgList.DataSource = dt;
            dtgList.DataBind();
            int columnCount = dtgList.Rows[0].Cells.Count;
            dtgList.Rows[0].Cells.Clear();
            dtgList.Rows[0].Cells.Add(new TableCell());
            dtgList.Rows[0].Cells[0].ColumnSpan = columnCount;
            dtgList.Rows[0].Cells[0].Text = "没有记录";
            dtgList.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            dtgList.DataSource = dt;
            dtgList.DataBind();
        }
    }
    public DataTable GetDeliveryList(string prdnbr, string pono, string povend, string poship, string podomain, string poorddate, string poduedate, string gendate, string stat, string buyer)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@prdnbr", prdnbr);
            param[1] = new SqlParameter("@pono", pono);
            param[2] = new SqlParameter("@povend", povend);
            param[3] = new SqlParameter("@poship", poship);
            param[4] = new SqlParameter("@podomain", podomain);
            param[5] = new SqlParameter("@poorddate", poorddate);
            param[6] = new SqlParameter("@poduedate", poduedate);
            param[7] = new SqlParameter("@gendate", gendate);
            param[8] = new SqlParameter("@postat", stat);
            param[9] = new SqlParameter("@buyer", buyer);
            param[10] = new SqlParameter("@uid", Session["uID"].ToString());
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_selectDeliveryListappv", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txbord.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txbord.Text.Trim());
            }
            catch
            {
                this.Alert("订货日期格式不对");
                return;
            }
        }

        if (txbdue.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txbdue.Text.Trim());
            }
            catch
            {
                this.Alert("截止日期格式不对");
                return;
            }
        }

        if (txtGenDate.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txtGenDate.Text.Trim());
            }
            catch
            {
                this.Alert("生成日期格式不对");
                return;
            }
        }
        BindData();
    }

    protected void PageChange(object sender, GridViewPageEventArgs e)
    {
        dtgList.PageIndex = e.NewPageIndex;
        dtgList.SelectedIndex = -1;
        BindData();
    }
    public bool DeleteDeliveryList(string prdnbr, string pono, int createBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@prdnbr", prdnbr);
            param[1] = new SqlParameter("@prd_po_nbr", pono);
            param[2] = new SqlParameter("@createBy", createBy);
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_deleteDeliveryList", param);
            return Convert.ToBoolean(param[3].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void dtgList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().ToLower().CompareTo("del") == 0)
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string prd = param[0].ToString().Trim();
            string po = param[1].ToString().Trim();
            if (DeleteDeliveryList(prd, po, Convert.ToInt32(Session["uID"])))
            {
                BindData();
            }
            else
            {
                this.Alert("删除失败，请联系管理员！");
                return;
            }
        }
        else if (e.CommandName.ToString().ToLower().CompareTo("det") == 0)
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string prd = param[0].ToString().Trim();
            string po = param[1].ToString().Trim();
            string domain = param[2].ToString().Trim();

            Response.Redirect("RP_editDeliverysAppv.aspx?prd=" + prd + "&po=" + po + "&domain=" + domain + "&type=" + "edit" + "&rm=" + DateTime.Now.ToString());
        }
    }
    public bool CheckCanMondify(string prdnbr, string domain)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@prdnbr", prdnbr);
            param[1] = new SqlParameter("@domain", domain);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_checkCanMondify", param);
            return Convert.ToBoolean(param[2].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void dtgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {



            if (!CheckCanMondify(e.Row.Cells[0].Text.Trim(), e.Row.Cells[4].Text.Trim()))
            {
                e.Row.Cells[9].Text = string.Empty;
            }
        }
    }
}