using CommClass;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_pur_restart : BasePage
{
    private NewWorkflow nwf = new NewWorkflow();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropNode.DataSource = nwf.GetFlowNode("EDAF9855-7C48-40DD-9683-F13872885185", Session["uID"].ToString());
            dropNode.DataBind();

            if (dropNode.Items.Count == 0)
            {
                btnSearch.Enabled = false;
                this.Alert("你没有查看节点的权限！");
                return;
            }

            BindData();
        }

    }

    protected void BindData()
    {
        string strSQL = "sp_pur_GetFailedPoApprove";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@uID", Session["uID"].ToString());
        parm[1] = new SqlParameter("@ponbr", txtNbr.Text.Trim());
        parm[2] = new SqlParameter("@vend", txtVend.Text.Trim());
        parm[3] = new SqlParameter("@node", dropNode.SelectedValue);

        DataTable dt = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.qadplan"), CommandType.StoredProcedure, strSQL, parm).Tables[0];
      
        gvList.DataSource = dt;
        gvList.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnRestart_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() == "")
        {
            this.Alert("请输入重启原因！");
            return;
        }
        foreach (GridViewRow row in gvList.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk.Checked)
            {
                string id = gvList.DataKeys[row.RowIndex].Values["ID"].ToString();
                nwf.RestartWorkflow(id, Session["uID"].ToString(), dropNode.SelectedValue, txtReason.Text.Trim());
            }
        }
        BindData();
    }

    protected void btnRestartAll_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() == "")
        {
            this.Alert("请输入重启原因！");
            return;
        }
        foreach (GridViewRow row in gvList.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk.Checked)
            {
                string id = gvList.DataKeys[row.RowIndex].Values["ID"].ToString();
                nwf.RestartWorkflowForAll(id, Session["uID"].ToString(), dropNode.SelectedValue, txtReason.Text.Trim());
            }
        }
        BindData();
    }
}