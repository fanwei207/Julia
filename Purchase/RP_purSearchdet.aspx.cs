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
using System.Web.UI.WebControls.Expressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
//using System.Web.Mail;
using System.Text;
using Microsoft.Web.UI.WebControls;
using System.IO;
using CommClass;
using System.Net.Mail;


public partial class Purchase_RP_purSearchdet : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //BindGV();
            if (Request["type"].ToString() == "edit")
            {
                if (Request["pur_Nbr"] != null)
                {
                    BindData(Request["pur_Nbr"].ToString(), Request["pur_Line"].ToString());
                }
            }
            else
            { 
             BindGV(Request["pur_Nbr"].ToString(), Request["pur_Line"].ToString());
            }

        }
    }
    private void BindGV(string nbr ,string line)
    {
        DataTable dt = SelectPurchaseDet(nbr,line);
        gv.DataSource = dt;
        gv.DataBind();
    }
    public DataTable SelectPurchaseDet(string nbr,string line)
    {
        string str = "sp_rp_SelectPursearchDet";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@nbr", nbr);
        param[1] = new SqlParameter("@line", line);
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewEdit")
        {
            //int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            int intRow = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string pur_Nbr = gv.DataKeys[intRow].Values["pur_Nbr"].ToString();
              string pur_Line = gv.DataKeys[intRow].Values["pur_Line"].ToString();
            /*
            string _leaderIsAgree = gv.DataKeys[index].Values["supplier_LeaderIsAgree"].ToString();
            //有签名则不能编辑
            if (_leaderIsAgree != "0")
            {
                //ltlAlert.Text = "alert('主管已签字不能修改！');";
                //return;
                Response.Redirect("supplier_newApply1.aspx?type=edit&no=" + _no);
            }
            else
            {
                Response.Redirect("supplier_newApply1.aspx?type=edit&no=" + _no);
            }*/
            BindData( pur_Nbr, pur_Line);
            //this.Redirect("../Purchase/rp_purchaseMstrDetial.aspx?type=edit&ID=" + gv.DataKeys[intRow].Values["ID"].ToString());
        }
    }

    public void BindData(string nbr,string line)
    {
        String strVend = String.Empty;



        DataTable dt = GetDeliveryList(nbr,line);

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
        gvdet.Visible = false;
    }
    public DataTable GetDeliveryList(string nbr, string line)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ponbr", nbr);
            param[1] = new SqlParameter("@poline", line);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_selectDeliverysearchList", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void PageChange(object sender, GridViewPageEventArgs e)
    {

    }
    protected void dtgList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().ToLower().CompareTo("det") == 0)
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string prd = param[0].ToString().Trim();
            string po = param[1].ToString().Trim();
            string domain = param[2].ToString().Trim();
            gvdet.Visible = true;
            BindDatadet(prd, po, domain);
            //Response.Redirect("RP_editDeliverys.aspx?prd=" + prd + "&po=" + po + "&domain=" + domain + "&type=" + "edit" + "&rm=" + DateTime.Now.ToString());
        }
    }
    protected void dtgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    public void BindDatadet(string prdnbr, string ponbr, string domain)
    {
        System.Data.DataTable dt = GetDeliveryDetail(prdnbr, ponbr, domain);

        if (dt.Rows.Count == 0)
        {
            
            dt.Rows.Add(dt.NewRow());
            gvdet.DataSource = dt;
            gvdet.DataBind();
            int columnCount = gvdet.Rows[0].Cells.Count;
            gvdet.Rows[0].Cells.Clear();
            gvdet.Rows[0].Cells.Add(new TableCell());
            gvdet.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvdet.Rows[0].Cells[0].Text = "没有记录";
            gvdet.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {

            gvdet.DataSource = dt;
            gvdet.DataBind();

        }
    }
    public DataTable GetDeliveryDetail(string prdnbr, string ponbr, string domain)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@prdnbr", prdnbr);
            param[1] = new SqlParameter("@ponbr", ponbr);
            param[2] = new SqlParameter("@domain", domain);
           
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_selectDeliveryDetail", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    protected void dtgList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        this.Redirect("../Purchase/RP_purSearchList.aspx");
    }
}