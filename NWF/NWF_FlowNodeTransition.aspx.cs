using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;

public partial class NWF_FlowNodeTransition : BasePage
{
    NewWorkflow newWF = new NewWorkflow();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            lblWorkFlowId.Text = Request.QueryString["FlowId"];
            SqlDataReader reader = newWF.GetWorkFlowTemplateByID(lblWorkFlowId.Text);
            if(reader.Read())
            {
                lblWorkFlowName.Text = reader["Flow_Name"].ToString();
            }
            reader.Close();
            dropPreNode.DataSource = newWF.GetFlowNode(lblWorkFlowId.Text);
            dropPreNode.DataBind();
            dropPreNode.Items.Insert(0, new ListItem("起始节点", "00000000-0000-0000-0000-000000000001"));
            dropPreNode.Items.Insert(0, new ListItem("--", "0"));

            dropNextNode.DataSource = newWF.GetFlowNode(lblWorkFlowId.Text);
            dropNextNode.DataBind();
            dropNextNode.Items.Insert(0, new ListItem("结束节点", "00000000-0000-0000-0000-000000000002"));
            dropNextNode.Items.Insert(0, new ListItem("--", "0"));

            BindData();
        }
    }
    protected void BindData()
    {
        String strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
        string strSql = "sp_nwf_selectFlowNodeTransition";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@FlowId", lblWorkFlowId.Text);
        DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];

        gv.DataSource = table;
        gv.DataBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
 
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (dropPreNode.SelectedIndex == 0)
        {
            this.Alert("请先选择一个前置节点！");
            return;
        }

        if (dropNextNode.SelectedIndex == 0)
        {
            this.Alert("请先选择一个后置节点！");
            return;
        }

        String strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
        string strSql = "sp_nwf_insertFlowNodeTransition";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@FlowId", lblWorkFlowId.Text);
        param[1] = new SqlParameter("@PrevNode", dropPreNode.SelectedValue);
        param[2] = new SqlParameter("@NextNode", dropNextNode.SelectedValue);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param);

        if (!Convert.ToBoolean(param[3].Value))
        {
            this.Alert("保存失败，请联系管理员！");
            return;
        }

        BindData();
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string PrevNode = gv.DataKeys[e.RowIndex].Values["PrevNode"].ToString();
        string NextNode = gv.DataKeys[e.RowIndex].Values["NextNode"].ToString();

        String strConn = ConfigurationManager.AppSettings["SqlConn.Conn_WF"];
        string strSql = "sp_nwf_deleteFlowNodeTransition";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@FlowId", lblWorkFlowId.Text);
        param[1] = new SqlParameter("@PrevNode", PrevNode);
        param[2] = new SqlParameter("@NextNode", NextNode);
        param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        param[3].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param);

        if (!Convert.ToBoolean(param[3].Value))
        {
            this.Alert("删除失败，请联系管理员！");
            return;
        }

        BindData();
    }
}