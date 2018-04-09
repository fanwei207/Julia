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
 

public partial class admin_ApproveInternet : BasePage
{   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             
        }
        BindData(); 
    }

    private void BindData()
    {
        Int32 iPlantCode = Convert.ToInt32(ddlPlantCode.SelectedValue);
        String strUserName = txtApplyName.Text.ToString();
        bool boolApproved = chkb_displayToApprove.Checked;

        DataTable dt = admin_AccessApply.getApplyOuterNetAccessInfo(iPlantCode, strUserName, boolApproved);

        gv.DataSource = dt;
        gv.DataBind(); 
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    { 
        BindData();
    }


    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            ((LinkButton)e.Row.FindControl("btnApprove")).Style.Add("font-weight", "normal");
            ((LinkButton)e.Row.FindControl("btnCancel")).Style.Add("font-weight", "normal");
        
            if (e.Row.Cells[8].Text.ToString() != "&nbsp;")
            {
                ((LinkButton)e.Row.FindControl("btnApprove")).Text = "已审批";

            }
            else
            {   // 信息部 IT 404
                if (Session["deptID"].ToString() == "404" && gv.DataKeys[e.Row.RowIndex][2].ToString() == Convert.ToString(Session["uID"]))
                {
                    ((LinkButton)e.Row.FindControl("btnApprove")).Enabled = true;
                    ((LinkButton)e.Row.FindControl("btnCancel")).Enabled = true;
                }
                else
                {
                    if (gv.DataKeys[e.Row.RowIndex][1].ToString() == Convert.ToString(Session["uID"]))
                    {
                        ((LinkButton)e.Row.FindControl("btnDelete")).Enabled = true;
                    }
                }
            }
        }
    }
 

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        this.BindData();
    } 

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String result = String.Empty;
        if (e.CommandName == "Approve")
        {
            result = "Yes";
            if (admin_AccessApply.UpdateApplyInternetAcc(e.CommandArgument.ToString(), result))
            {
                BindData();
                ltlAlert.Text = "alert('批准申请成功')";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('批准申请未成功，请重新审批')";
                return;
            }
        }
        if (e.CommandName == "myCancel")
        {
             result = "No";
             if (admin_AccessApply.UpdateApplyInternetAcc(e.CommandArgument.ToString(), result))
            {
                BindData();
                ltlAlert.Text = "alert('已拒绝外网访问申请')";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('审批未成功，请重新审批')";
                return;
            }
        }
        if (e.CommandName == "Delete")
        {
            admin_AccessApply.DeleteApplyInternetAcc(e.CommandArgument.ToString());
            BindData();
        }
    }

    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {      
        BindData();
    }

    protected void chkb_displayToApprove_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
}
