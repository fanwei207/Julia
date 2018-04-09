using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
using System.Data;


public partial class EDI_EDI_ChangePoMstr : BasePage
{

    poc_helper helper = new poc_helper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request["no"] != null)
            {
                txtPoNbr.Text = Request["no"].ToString();
            }
            if (Request["pocCode"] != null)
            {
                txtPocCode.Text = Request["pocCode"].ToString();
            }
            if (Request["statuValue"] != null)
            {
                ddlStatu.SelectedValue = Request["statuValue"].ToString();
            }
            if (Request["typeValue"] != null)
            {
                ddlType.SelectedValue = Request["typeValue"].ToString();
            }
            if (Request["dateBegin"] != null)
            {
                txtDateBegin.Text = Request["dateBegin"].ToString();
            }
            if (Request["dateEnd"] != null)
            {
                txtDateEnd.Text = Request["dateEnd"].ToString();
            }

            gvbind();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        gvbind();
    }

    private void gvbind()
    {
        string poNbr = txtPoNbr.Text.Trim();
        string pocCode = txtPocCode.Text.Trim();
        string pocStarDate = txtDateBegin.Text.Trim();
        string pocEndDate  = txtDateEnd.Text.Trim();
        string status = ddlStatu.SelectedValue.Trim();
        string waiting = ddlType.SelectedValue.Trim();
        string uID = Session["uID"].ToString();

        DataTable dt = helper.selectPOChangeMstr(poNbr, pocCode, pocStarDate, pocEndDate, status, waiting, uID);

        gvInfo.DataSource = dt;
        gvInfo.DataBind();
    }


    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        gvbind();
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string flag = gvInfo.DataKeys[e.Row.RowIndex].Values["poc_managerIsAgree"].ToString();

            //if (flag == "True")
            //{
            //    ((Label)e.Row.FindControl("lbNoticeAgree")).Text = "Y";
            //}
            //else if (flag == "False")
            //{
            //    ((Label)e.Row.FindControl("lbNoticeAgree")).Text = "N";
            //}
            //else
            //{
            //    ((Label)e.Row.FindControl("lbNoticeAgree")).Text = "";
            //}

            string status = gvInfo.DataKeys[e.Row.RowIndex].Values["poc_status"].ToString();

            if (status == "0")
            {
                ((Label)e.Row.FindControl("lbStatus")).Text = "normal";
            }
            else if (status == "10")//已经执行了
            {
                ((Label)e.Row.FindControl("lbStatus")).Text = "complete";
                ((LinkButton)e.Row.FindControl("lkbtnClose")).Text = "";
            }
            else if (status == "-10")
            {
                ((Label)e.Row.FindControl("lbStatus")).Text = "refuse"; //拒绝
            }
            else if (status == "-20")
            {
                ((Label)e.Row.FindControl("lbStatus")).Text = "close";
            }


            string commit = gvInfo.DataKeys[e.Row.RowIndex].Values["poc_commit"].ToString();
            if (commit.Equals(string.Empty) )
            {
                ((LinkButton)e.Row.FindControl("lkbtnDet")).Text = "";
                
            }

            if (flag == "True" || flag == "False" || status == "-20")
            {
                ((LinkButton)e.Row.FindControl("lkbtnClose")).Text = "";
            }

            if (gvInfo.DataKeys[e.Row.RowIndex].Values["poc_commit"].ToString() == "True")
            {
                ((LinkButton)e.Row.FindControl("lkbtnUpdate")).CommandName = "commit";
            }

            //开后门，有权限的，还在运行的状态的单子都有权限去关闭
            if (status == "0" && this.Security["10001210"].isValid)
            {
                ((LinkButton)e.Row.FindControl("lkbtnClose")).Text = "Close";
            }
        }
    }
    protected void gvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string poc_id = e.CommandArgument.ToString();
       
        
        if (e.CommandName == "lkbtnUpdate")
        {
            int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex; //此得出的值是表示那行被选中的索引值
            string status =  gvInfo.DataKeys[rowindex].Values["poc_status"].ToString();
            if (status != "0")
            {

                ltlAlert.Text = "alert('The Apply is commiting,already.Can't be changed');";

                return;
            }
            else
            {
                Response.Redirect("EDI_ChangePoNew.aspx?poc_id=" + poc_id);
            }
        }
        if (e.CommandName == "lkbtnDet")
        {
            int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex; //此得出的值是表示那行被选中的索引值
            string status = gvInfo.DataKeys[rowindex].Values["poc_status"].ToString();

            string no = txtPoNbr.Text.Trim();
            string pocCode = txtPocCode.Text.Trim();
            string statuValue = ddlStatu.SelectedValue.ToString();
            string typeValue = ddlType.SelectedValue.ToString();
            string dateBegin = txtDateBegin.Text.Trim();
            string dateEnd = txtDateEnd.Text.Trim();

            Response.Redirect("EDI_ChangePoDet.aspx?poc_id=" + poc_id + "&status=" + status + "&pocCode=" + pocCode
                                + "&no=" + no + "&statuValue=" + statuValue + "&typeValue=" + typeValue + "&dateBegin=" + dateBegin
                                + "&dateEnd=" + dateEnd);
        }
        if (e.CommandName == "lkbtnClose")
        {
            int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex; //此得出的值是表示那行被选中的索引值
            if (helper.ClosePoApply(poc_id,Session["uID"].ToString(),Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('Close Success!');";
                gvbind();
            }
            else
            {
                ltlAlert.Text = "alert('Close filed!');";
                
            }
        }
        if (e.CommandName == "commit")
        {
            int rowindex = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex; //此得出的值是表示那行被选中的索引值
            ltlAlert.Text = "alert('The Apply commit already!');";
        }
    }
    protected void btnToNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("EDI_ChangePoNew.aspx");
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {

        string poNbr = txtPoNbr.Text.Trim();
        string pocCode = txtPocCode.Text.Trim();
        string pocStarDate = txtDateBegin.Text.Trim();
        string pocEndDate = txtDateEnd.Text.Trim();
        string status = ddlStatu.SelectedValue.Trim();
        string waiting = ddlType.SelectedValue.Trim();
        string uID = Session["uID"].ToString();


        DataTable dt = helper.exportPOCMstr(poNbr, pocCode, pocStarDate, pocEndDate, status, waiting, uID);
        string title = "100^<b>POC CODE</b>~^100^<b>PO NUMBER</b>~^100^<b>LINE</b>~^100^<b>ISDO</b>~^";
         title += "100^<b>STATUS</b>~^100^<b>APPLY BY</b>~^100^<b>APPLY DATE</b>~^100^<b>IS DELETE</b>~^";
         title += "100^<b>NEW CUST PART</b>~^100^<b>OLD CUST PART</b>~^100^<b>NEW QAD</b>~^100^<b>OLD QAD</b>~^";
         title += "100^<b>NEW QTY</b>~^100^<b>OLD QTY</b>~^100^<b>NEW UM</b>~^100^<b>OLD UM</b>~^";
         title += "100^<b>NEW PRICE</b>~^100^<b>OLD PRICE</b>~^100^<b>NEW REQ DATE</b>~^100^<b>OLD REQ DATE</b>~^";
         title += "100^<b>NEW DUE DATE</b>~^100^<b>OLD DUE DATE</b>~^100^<b>NEW REMARK</b>~^100^<b>OLD REMARK</b>~^";
         title += "100^<b>NEW DESC</b>~^100^<b>OLD DESC</b>~^";
        if (dt != null && dt.Rows.Count > 0)
        {
            ExportExcel(title, dt, false);
        }
    }
}