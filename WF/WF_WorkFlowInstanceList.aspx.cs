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

public partial class PM_HeaderList : BasePage
{    
    WorkFlow wf = new WorkFlow();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadWorkFlowTemplate();
            BindData();
        }
    }

    protected void loadWorkFlowTemplate()
    {
        ddlWorkFlow.DataSource = wf.GetWorkFlowTemplateByDomain(Convert.ToInt32(Session["plantCode"]));
        ddlWorkFlow.DataBind();
        ddlWorkFlow.Items.Insert(0, new ListItem("--","0"));
    }

    protected void BindData()
    {
        //定义参数
        string wfnNbr = txtNbr.Text.Trim();
        int flowID = Convert.ToInt32(ddlWorkFlow.SelectedValue);
        string reqDate1 = txtReqDate.Text.Trim();
        int uID = Convert.ToInt32(Session["uID"].ToString());
        int status = 1;
        if (radProcess.Checked) status = 1;
        if (radFinish.Checked) status = 2;
        if (radCancel.Checked) status = 3;

        gvWFI.DataSource = wf.GetWorkFlowInstanceCreatedBySelf(wfnNbr,flowID,reqDate1,uID,status);
        gvWFI.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWFI_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvWFI_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWFI.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (txtReqDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtReqDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('申请日期格式不正确！正确格式是:YYYY-MM-DD');";
                return;
            }
        }
        BindData();
    }

    protected void gvWFI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            ltlAlert.Text = "window.open('/WF/WF_FormEdit.aspx?nbr=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now;
            ltlAlert.Text += "','','menubar=no,scrollbars=no,resizable=no,width=900,height=530,top=0,left=0');";
        }
        if (e.CommandName == "Detail")
        {
            string status = "1";
            if (radProcess.Checked) status = "1";
            if (radFinish.Checked) status = "2";
            if (radCancel.Checked) status = "3"; 
            Response.Redirect("WF_WorkFlowInstanceListDetail.aspx?nbr=" + e.CommandArgument.ToString().Trim() + "&tp=" + status + "&rm=" + DateTime.Now);
        }
    }

    protected void gvWFI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
             
        }
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("WF_WorkFlowInstanceInsert.aspx?rm=" + DateTime.Now.ToString());
    }

    protected void radProcess_CheckedChanged(object sender, EventArgs e)
    {
        if (txtReqDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtReqDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('申请日期格式不正确！正确格式是:YYYY-MM-DD');";
                return;
            }
        }
        BindData();
    }

    protected void radFinish_CheckedChanged(object sender, EventArgs e)
    {
        if (txtReqDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtReqDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('申请日期格式不正确！正确格式是:YYYY-MM-DD');";
                return;
            }
        }
        BindData();
    }

    protected void radCancel_CheckedChanged(object sender, EventArgs e)
    {
        if (txtReqDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtReqDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('申请日期格式不正确！正确格式是:YYYY-MM-DD');";
                return;
            }
        }
        BindData();
    }
}
