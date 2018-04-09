using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class Purchase_RP_WebDETList : BasePage
{
    private adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Mid"] != null)
            {
                lbl_id.Text = Request.QueryString["Mid"].ToString();
                BindMstrData(lbl_id.Text);
            }
            else
            {
                txtReason.Enabled = false;
                trReview.Visible = false;
                trApply.Visible = true;
                lbn_url.Text = txturl.Text;
                txturl.Visible = true;
                lbn_url.Visible = false;
            }      
        }
    }

    private void BindMstrData(string Mid)
    {
        IDataReader reader = GetProductStruApplyMstr(lbl_id.Text);
        string status="";
        if (reader.Read())
        {
            txt_nbr.Text = reader["RP_code"].ToString();
            txt_status.Text = reader["RP_status"].ToString();
            //txt_prodCode.Text = reader["ProdCode"].ToString();
            txtReason.Text = reader["Reason"].ToString();
            txtRmks.Text = reader["RP_webNbr"].ToString();
            txturl.Text = reader["RP_URL"].ToString();
            lbl_Status.Text = reader["RP_status"].ToString();
            txtvend.Text = reader["RP_vend"].ToString();
            hid_CreatedBy.Value = reader["RP_createby"].ToString();
            status = reader["status"].ToString();
            //ddlZH.SelectedValue = reader["RP_ZH"].ToString();
            //ddldepartment.SelectedValue = reader["departmentid"].ToString();
            //BindDetailData();

        }
        reader.Close();
        if (status == "0")
        {
            txtReason.Enabled = false;
            trReview.Visible = false;
            trApply.Visible = true;
            lbn_url.Text = txturl.Text;
            txturl.Visible = true;
            lbn_url.Visible = false;
        }
        else if (status == "10" && this.Security["120005402"].isValid)
        {
            txtReason.Enabled = true;
            trReview.Visible = true;
            trApply.Visible = false;
            btn_OK.Visible = false;
            lbn_url.Text = txturl.Text;
            lbn_url.Visible = true;
            txturl.Visible = false;
            txtRmks.ReadOnly = true;
        }
        else if (status == "20" && this.Security["120005403"].isValid)
        {
            txtReason.Enabled = false;
            trReview.Visible = true;
            trApply.Visible = false;
            btn_Confirm.Visible = false;
            btn_Reject.Visible = false;
            lbn_url.Text = txturl.Text;
            lbn_url.Visible = true;
            txturl.Visible = false;
            txtRmks.ReadOnly = true;
            btn_OK.Visible = true;
        }
        else
        {
            txtReason.Enabled = false;
            trReview.Visible = true;
            trApply.Visible = false;
            btn_Confirm.Visible = false;
            btn_Reject.Visible = false;
            btn_OK.Visible = false;
            lbn_url.Text = txturl.Text;
            lbn_url.Visible = true;
            txturl.Visible = false;
            txtRmks.ReadOnly = true;
        }
        BindDetailData();
    }
    public SqlDataReader GetProductStruApplyMstr(string id)
    {
        try
        {
            string strName = "sp_RP_selectwebmstr";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
    }


    public DataTable GetProductStruApplyDetail(string id)
    {
        try
        {
            string strName = "sp_RP_selectwebDetail";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    private void BindDetailData()
    {
        gv_product.DataSource = GetProductStruApplyDetail(lbl_id.Text);
        gv_product.DataBind();
    }
    protected void gv_product_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_product.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gv_product_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gv_product_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gv_product_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {


        if (txturl.Text == "")
        {
            this.Alert("网址不能为空");
            return;
        }
        else if (txtRmks.Text == "")
        {
            this.Alert("订单号不能为空");
            return;
        }
        try
        {
            string strName = "sp_RP_Insertwebmstrtocaiwu";
            SqlParameter[] parm = new SqlParameter[6];

            parm[1] = new SqlParameter("@url", txturl.Text);
            parm[2] = new SqlParameter("@webnbr", txtRmks.Text);
            parm[3] = new SqlParameter("@id", lbl_id.Text);
          
             SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
             this.Alert("保存成功并提交");
             BindMstrData(lbl_id.Text);
        }
        catch
        {
            this.Alert("保存失败");
        }
    }

    public void UpdateProductStruApplyMstr(string id, string reason, string status, string userId, string userName)
    {
        string strName = "sp_RP_UpdatewebMstr";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@id", id);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);
        parm[6] = new SqlParameter("@userName", userName);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        UpdateProductStruApplyMstr(lbl_id.Text, txtReason.Text, "-10", Session["uID"].ToString(), Session["uName"].ToString());
        this.Alert("取消成功");
        BindMstrData(lbl_id.Text);
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        this.Redirect("RP_WebMstrList.aspx");
    }
    protected void btn_Confirm_Click(object sender, EventArgs e)
    {
        UpdateProductStruApplyMstr(lbl_id.Text, txtReason.Text, "20", Session["uID"].ToString(), Session["uName"].ToString());
        this.Alert("审批成功");
        BindMstrData(lbl_id.Text);
    }
    protected void btn_Reject_Click(object sender, EventArgs e)
    {
        if (txtReason.Text == "")
        {
            this.Alert("原因不能为空");
            return;
        }

        UpdateProductStruApplyMstr(lbl_id.Text, txtReason.Text, "10", Session["uID"].ToString(), Session["uName"].ToString());
        this.Alert("驳回成功");
        BindMstrData(lbl_id.Text);
    }
    protected void btn_OK_Click(object sender, EventArgs e)
    {
        UpdateProductStruApplyMstr(lbl_id.Text, txtReason.Text, "30", Session["uID"].ToString(), Session["uName"].ToString());
        this.Alert("确认成功");
        BindMstrData(lbl_id.Text);
    }
    protected void lbn_url_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('" + lbn_url.Text + "','网页',''); w.focus();";
    }
}