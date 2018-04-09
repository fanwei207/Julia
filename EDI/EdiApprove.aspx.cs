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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using QADSID;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Data;
using System.Text;
using System.Text;
using System.Data.SqlClient;
using adamFuncs;
using CommClass;

public partial class EdiApprove : BasePage
{
    //SID sid = new SID();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            dropNode.DataSource = new NewWorkflow().GetFlowNode("8C930BDD-4C65-45E6-A9D7-3EDD873005A3", Session["uID"].ToString());
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
        DataTable dt = GetShip();
        gvShip.DataSource = dt;
        gvShip.DataBind();
    }

    public DataTable GetShip()
    {
        string ponbr = txtnbr.Text.Trim();
        string poline = txtpolin.Text.Trim();
        string cuscode = txt_cuscode.Text.Trim();
        string part = txt_part.Text.Trim();
        string cuspart = txt_cuspart.Text.Trim();

        string strSQL = "sp_edi_GetApproveData";
        SqlParameter[] parm = new SqlParameter[7];
        parm[0] = new SqlParameter("@uID", Session["uID"].ToString());
        parm[1] = new SqlParameter("@ponbr", ponbr);
        parm[2] = new SqlParameter("@poline", poline);
        parm[3] = new SqlParameter("@cuscode", cuscode);
        parm[4] = new SqlParameter("@part", part);
        parm[5] = new SqlParameter("@cuspart", cuspart);
        parm[6] = new SqlParameter("@node", dropNode.SelectedValue);

        return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSQL, parm).Tables[0];
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Confirm3")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string id = gvShip.DataKeys[index].Values["ID"].ToString();
            string wfi_id = "8C930BDD-4C65-45E6-A9D7-3EDD873005A3";
            ConfirmShipInfo(wfi_id, id, Session["uID"].ToString());
            BindData();
        }

    }
    public void ConfirmShipInfo(String wfi_id, string id, string uID)
    {
        string strSQL = "sp_edi_restartWorkflow";
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@SourceId", id);
        parm[1] = new SqlParameter("@nodeId", dropNode.SelectedValue);
        parm[2] = new SqlParameter("@UserId", uID);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSQL, parm);
    }

    public void RestartWorkflowForAll(string id)
    {
        string strSQL = "sp_edi_restartWorkflowForAll";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@SourceId", id);

        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strSQL, parm);
    }


    protected void gvShip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShip.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvShip_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }

    protected void btnRestart_Click(object sender, EventArgs e)
    {
        string wfi_id = "8C930BDD-4C65-45E6-A9D7-3EDD873005A3";
        foreach (GridViewRow row in gvShip.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk.Checked)
            {
                string id = gvShip.DataKeys[row.RowIndex].Values["ID"].ToString();
                ConfirmShipInfo(wfi_id, id, Session["uID"].ToString());
            }
        }
        BindData();
    }
    protected void btnRestartAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvShip.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk.Checked)
            {
                string id = gvShip.DataKeys[row.RowIndex].Values["ID"].ToString();
                RestartWorkflowForAll(id);
            }
        }
        BindData();
    }
}