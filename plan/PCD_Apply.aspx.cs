using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plan_PCD_Apply : System.Web.UI.Page
{
    private edi.PCDApply helper = new edi.PCDApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string poNbr = Request.QueryString["poNbr"];
            lblPoNbr.Text = poNbr;

            if (Request.QueryString["Id"] != null)
            {
                lblApplyId.Text = Request.QueryString["Id"];
            }
            else
            {
                btnBack.Visible = false;
            }
            BindApplyData();
            BindApproveData();
        }
    }


    private void BindApplyData()
    {
        string id = lblApplyId.Text;
        string poNbr = Request.QueryString["poNbr"];
        string poLine = Request.QueryString["poLine"];
        DataTable dt;
        DataTable detDt;
        if (id != "")
        {
            dt = helper.GetApply(id);
            detDt = helper.GetApplyDet(id);
        }
        else
        {
            dt = helper.GetApply(poNbr, poLine);
            detDt = helper.GetApplyDet(poNbr, poLine);
        }
        txt_approveID.Text = "";
        txt_ApproveEmail.Text = "";
        txtApproveName.Text = "";
        txtApprOpin.Text = "";
        gvDet.DataSource = detDt;
        gvDet.DataBind();
        if (dt.Rows.Count > 0)
        {
            gvDet.Columns[0].Visible = false;
            DataRow row = dt.Rows[0];
            txtPlanDate.Text = row["planDate"] == DBNull.Value ? "" : string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(row["planDate"]));
            lblApplyId.Text = row["ApplyId"].ToString();
            lblApplyer.Text = row["Applyer"].ToString();
            lblApplyerEmail.Text = row["ApplyerEmail"].ToString();
            lblAproveDate.Text = row["ApproveDate"] == DBNull.Value ? "" : string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(row["ApproveDate"]));
            txtApplyReason.Text = row["ApplyReason"].ToString();
            btn_ApplySubmit.Visible = false;
            txtApplyReason.Enabled = false;
            if (row["approveby"].ToString() == Session["uID"].ToString())
            {
                btn_approve.Visible = true;
                btn_diaApp.Visible = true;
                approveopinion.Visible = true;
            }
            else
            {
                tbApply.Visible = false;
            }
            string result = row["ApproveResult"].ToString().ToLower();
            if (result != "")
            {
                chk_isApproved.Checked = true;
                btnSetPCD.Enabled = false;
                if (result == "true")
                {
                    lbl_ApproveNote.Text = "审批通过";
                }
                else
                {
                    lbl_ApproveNote.Text = "审批未通过";
                }
            }

        }
        else
        {
            btn_ApplySubmit.Visible = true;
            txtApplyReason.Enabled = true;
            btn_approve.Visible = false;
            btn_diaApp.Visible = false;
        }
    }

    private void BindApproveData()
    {
        string applyId = lblApplyId.Text;
        gvApprove.DataSource = helper.GetApprove(applyId);
        gvApprove.DataBind();
    }
    protected void btn_Approver_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('../RDW/rdw_getProjQadApprover.aspx','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
    protected void btn_ApplySubmit_Click(object sender, EventArgs e)
    {
        if (txtApplyReason.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Apply Reason,Please!'); ";
            return;
        }
        if (txtApproveName.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Select one Approver,Please!'); ";
            return;
        }

        if (chkEmail.Checked)
        {
            if (txt_ApproveEmail.Text == string.Empty)
            {
                ltlAlert.Text = "alert('Enter the Approver Email if you want to send email,Please!'); ";
                return;
            }
        }
        string poNbr = lblPoNbr.Text;
        string approver = txt_approveID.Text;
        string applyReason = txtApplyReason.Text.Trim().ToString();
        string uID = Session["uID"].ToString();


        DataTable table = new DataTable("ApplyDet");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "poNbr";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "poLine";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "applyQty";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "planDate";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "applyPlanDate";
        table.Columns.Add(column);

        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            CheckBox chk = gvRow.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {               
                TextBox txtApplyPCD = gvRow.FindControl("txtApplyPCD") as TextBox;
                TextBox txtApplyQty = gvRow.FindControl("txtApplyQty") as TextBox;
                if (txtApplyPCD.Text.Trim() != "")
                {
                    row = table.NewRow();
                    row["poNbr"] = lblPoNbr.Text;
                    row["poLine"] = gvDet.DataKeys[gvRow.RowIndex].Values["poLine"].ToString();
                    string planDate = gvDet.DataKeys[gvRow.RowIndex].Values["planDate"].ToString();
                    string applyPlanDate = txtApplyPCD.Text.Trim();
                    string applyQty = txtApplyQty.Text.Trim();
                    if (!string.IsNullOrEmpty(planDate))
                    {
                        row["planDate"] = planDate;
                    }
                    if (!string.IsNullOrEmpty(applyPlanDate))
                    {
                        row["applyPlanDate"] = applyPlanDate;
                    }
                    if (!string.IsNullOrEmpty(applyQty))
                    {
                        row["applyQty"] = applyQty;
                    }
                    else
                    {
                        row["applyQty"] = gvDet.DataKeys[gvRow.RowIndex].Values["ordQty"].ToString();
                    }
                    table.Rows.Add(row);
                }
                else
                {
                    string poLine = gvDet.DataKeys[gvRow.RowIndex].Values["poLine"].ToString();
                    ltlAlert.Text = "alert('行号" + poLine + "的PCD为空，不能提交!'); ";
                    table.Dispose();
                    return;
                }
            }
        }
        int applyId = helper.AddApply(poNbr, approver, applyReason, uID, table);
        if (applyId > 0)
        {
            bool isSuccess = true;
            string message = "";
            lblApplyId.Text = applyId.ToString();

            if (chkEmail.Checked)
            {
                isSuccess = helper.SendMailForApprove(poNbr, txt_ApproveEmail.Text.Trim(), txtApproveName.Text.Trim(), Session["Email"].ToString(), Session["uName"].ToString(), applyReason, applyId.ToString(), out message);
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Apply successfully!');";

            }
            else
            {
                if (message != "")
                {
                    ltlAlert.Text = "alert('" + message + "');";
                }
            }
            BindApplyData();
            BindApproveData();
        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }


    protected void btn_approve_Click(object sender, EventArgs e)
    {
        if (txtApprOpin.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Approve Opinion,Please!'); ";
            return;
        }

        string applyId = lblApplyId.Text;
        string approver = txt_approveID.Text;
        string uID = Session["uID"].ToString();
        string approveOpoin = txtApprOpin.Text.Trim().ToString();
        DataTable table = new DataTable("ApplyDet");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "DetId";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "applyPlanDate";
        table.Columns.Add(column);
        foreach (GridViewRow gvRow in gvDet.Rows)
        {        
            TextBox txtApplyPCD = gvRow.FindControl("txtApplyPCD") as TextBox;
            if (txtApplyPCD.Text.Trim() != "")
            {
                row = table.NewRow();
                int detId = int.Parse(gvDet.DataKeys[gvRow.RowIndex].Values["DetId"].ToString());
                row["DetId"] = detId;
                row["applyPlanDate"] = txtApplyPCD.Text.Trim();
                table.Rows.Add(row);
            }
            else
            {
                string poLine = gvDet.DataKeys[gvRow.RowIndex].Values["poLine"].ToString();
                ltlAlert.Text = "alert('行号" + poLine + "的PCD为空，不能审批通过!'); ";
                table.Dispose();
                return;
            }
            
        }
        helper.UpdateApplyDet(table);
        if (helper.Approve(applyId, approver, uID, approveOpoin, true))
        {
            bool isSuccess = true;
            string message = "";
            if (txt_ApproveEmail.Text.Trim() != "" && chkEmail.Checked)
            {
                isSuccess = helper.SendMailForApprove(lblPoNbr.Text, txt_ApproveEmail.Text.Trim(), txtApproveName.Text.Trim(), Session["Email"].ToString(), Session["uName"].ToString(), txtApplyReason.Text.Trim(), applyId.ToString(), out message);
            }
            else if (txt_ApproveEmail.Text.Trim() == "")
            {
                isSuccess = helper.SendMailForPass(lblPoNbr.Text, lblApplyerEmail.Text.Trim(), lblApplyer.Text.Trim(), Session["Email"].ToString(), Session["uName"].ToString(), txtApplyReason.Text.Trim(), applyId.ToString(), out message);
            }
            if (isSuccess)
            {
                ltlAlert.Text = "alert('Successfully!');";

            }
            else
            {
                if (message != "")
                {
                    ltlAlert.Text = "alert('" + message + "');";
                }
            }
            BindApplyData();
            BindApproveData();
            return;
        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }
    protected void btn_diaApp_Click(object sender, EventArgs e)
    {
        if (txtApprOpin.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Enter the Approve Opinion,Please!'); ";
            return;
        }

        string applyId = lblApplyId.Text;
        string uID = Session["uID"].ToString();
        string approveOpoin = txtApprOpin.Text.Trim().ToString();
        string planDate = txtPlanDate.Text.Trim();
        if (helper.Approve(applyId, "", uID, approveOpoin, false))
        {
            bool isSuccess = true;
            string message = "";

            isSuccess = helper.SendMailForFailed(lblPoNbr.Text, lblApplyerEmail.Text.Trim(), lblApplyer.Text.Trim(), Session["Email"].ToString(), Session["uName"].ToString(), txtApprOpin.Text, applyId.ToString(), out message);

            if (isSuccess)
            {
                ltlAlert.Text = "alert('Successfully!');";

            }
            else
            {
                if (message != "")
                {
                    ltlAlert.Text = "alert('" + message + "');";
                }
            }
            BindApplyData();
            BindApproveData();
            return;
        }
        else
        {
            ltlAlert.Text = "alert('Database Operation Failed!');";
            return;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PCD_ApplyList.aspx", true);
    }

    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string poLine = Request.QueryString["poLine"];
            if (!string.IsNullOrEmpty(poLine))
            {
                if (gvDet.DataKeys[e.Row.RowIndex].Values["poLine"].ToString() == poLine)
                {
                    CheckBox chk = e.Row.FindControl("chk") as CheckBox;
                    if (chk != null)
                    {
                        chk.Checked = true;
                    }
                }
            }

            TextBox txtApplyPCD = e.Row.FindControl("txtApplyPCD") as TextBox;
            if (txtApplyPCD.Text.Trim() != "")
            {
                txtApplyPCD.Text = DateTime.Parse(txtApplyPCD.Text).ToString("yyyy-MM-dd");
            }
        }
    }
    protected void btnSetPCD_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            TextBox txtApplyPCD = gvRow.FindControl("txtApplyPCD") as TextBox;
            if (txtApplyPCD != null)
            {
                txtApplyPCD.Text = txtPlanDate.Text.Trim();
            }
        }
    }

}