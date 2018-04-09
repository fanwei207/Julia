using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class NWF_NWF_FlowNode : BasePage
{
    private NewWorkflow helper = new NewWorkflow();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("13113", "工作流模板删、改的权限");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //判断用户是否具备步骤设置的权限
            //判断用户是否具备工作流模板删、改的权限
            if (!this.Security["13113"].isValid)
            {
                if (helper.JudgeWorkFlowIsCreatedBySelf(Request.QueryString["id"], Convert.ToInt32(Session["uID"])))
                {
                    btn_add.Enabled = true;
                }
                else
                {
                    btn_add.Enabled = false;
                }
            }
            else
            {
                btn_add.Enabled = true;
            }

            SqlDataReader reader = helper.GetWorkFlowTemplateByID(Request.QueryString["id"]);
            if (reader.Read())
            {
                txtFlowName.Text = Convert.ToString(reader["Flow_Name"]);
            }
            reader.Close();
            BindData();
        }
    }

    protected void BindData()
    {
        DataTable dt = helper.GetFlowNode(Request.QueryString["id"]);
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.gvFN.DataSource = dt;
            this.gvFN.DataBind();
            int ColunmCount = gvFN.Rows[0].Cells.Count;
            gvFN.Rows[0].Cells.Clear();
            gvFN.Rows[0].Cells.Add(new TableCell());
            gvFN.Rows[0].Cells[0].ColumnSpan = ColunmCount;
            gvFN.Rows[0].Cells[0].Text = "暂无步骤";
            gvFN.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            this.gvFN.DataSource = dt;
            this.gvFN.DataBind();
        }
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (txtNodeName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('步骤名称不能为空!');";
            return;
        }

        if (txtNodeSort.Text == string.Empty)
        {
            ltlAlert.Text = "alert('序号不能为空!');";
            return;
        }

        if (btn_add.Text == "增加")//add
        {
            string flowID = Request.QueryString["id"];
            string name = txtNodeName.Text.Trim(); ;
            int sortorder = Convert.ToInt32(txtNodeSort.Text.Trim());
            string type = ddlType.SelectedValue;
            string description = txtNodeDesc.Text.Trim();
            int createdBy = Convert.ToInt32(Session["uID"]);

            helper.AddFlowNode(flowID, name, sortorder, type, description, createdBy,ckb_Email.Checked);

            txtNodeName.Text = string.Empty;
            txtNodeDesc.Text = string.Empty;
            txtNodeSort.Text = string.Empty;
            //ltlAlert.Text = "alert('创建流程步骤成功!');";          
            BindData();
        }
        else//edit
        {
            string id = lblid.Text.Trim();
            string flowID =Request.QueryString["id"];
            string name = txtNodeName.Text.Trim();
            int sortorder = Convert.ToInt32(txtNodeSort.Text.Trim());
            string type = ddlType.SelectedValue;
            string description = txtNodeDesc.Text.Trim();
            int modifiedBy = Convert.ToInt32(Session["uID"]);

            helper.UpdateFlowNode(id, flowID, name, sortorder, type, description, modifiedBy,ckb_Email.Checked);

                txtNodeName.Text = string.Empty;
                txtNodeDesc.Text = string.Empty;
                txtNodeSort.Text = string.Empty;
                btn_add.Text = "增加";
                //ltlAlert.Text = "alert('修改流程步骤成功!');";
            BindData();
        }
    }

    protected void gvFN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {

            //判断用户是否具备工作流模板删、改的权限
            if (this.Security["13113"].isValid || helper.JudgeFlowNodeIsCreatedBySelf(e.CommandArgument.ToString(), Convert.ToInt32(Session["uID"])))
            {

                SqlDataReader reader = helper.GetFlowNodeByID(e.CommandArgument.ToString().Trim());
                if (reader.Read())
                {
                    lblid.Text = Convert.ToString(reader["Node_ID"]);
                    txtNodeName.Text = Convert.ToString(reader["Node_Name"]);
                    txtNodeDesc.Text = Convert.ToString(reader["Node_Desc"]);
                    txtNodeSort.Text = reader["Node_Sort"].ToString();
                    ddlType.SelectedValue = reader["Node_Type"].ToString();
                    ckb_Email.Checked = Convert.ToBoolean( reader["Node_Email"].ToString());
                    btn_add.Text = "修改";
                }

            }
            else
            {
                ltlAlert.Text = "alert('您没有编辑此工作流模板步骤的权限!');";
            }
        }

        if (e.CommandName == "Reviewer")
        {
            //判断用户是否具备工作流模板删、改的权限
            if (this.Security["13113"].isValid || helper.JudgeFlowNodeIsCreatedBySelf(e.CommandArgument.ToString(), Convert.ToInt32(Session["uID"])))
            {
                //ltlAlert.Text = "$.window('审批人设置',500,550,'../NWF/NWF_FlowNodePerson.aspx?nodeId=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now+"')";
                Response.Redirect("NWF_FlowNodePerson.aspx?nodeId=" + e.CommandArgument.ToString().Trim() + "&flowId=" + Request.QueryString["id"] + "&rm=" + DateTime.Now, true);


            }
            else
            {
                ltlAlert.Text = "alert('您没有编辑此工作流模板步骤的权限!');";
            }
        }

        if (e.CommandName == "design")
        {
            if (this.Security["13113"].isValid || helper.JudgeFlowNodeIsCreatedBySelf(e.CommandArgument.ToString(), Convert.ToInt32(Session["uID"])))
            {
                Response.Redirect("NWF_FormDesign.aspx?FlowId=" + Request.QueryString["id"] + "&NodeId=" + e.CommandArgument.ToString().Trim(), true);
            }
            else
            {
                ltlAlert.Text = "alert('您没有编辑此工作流模板步骤的权限!');";
            }
        }
    }

    protected void gvFN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //判断用户是否具备工作流模板删、改的权限
        if (!this.Security["13113"].isValid || helper.JudgeFlowNodeIsCreatedBySelf(gvFN.DataKeys[e.RowIndex].Value.ToString(), Convert.ToInt32(Session["uID"])))
        {

            int nRet = helper.DeleteFlowNode(gvFN.DataKeys[e.RowIndex].Value.ToString(), Convert.ToInt32(Session["uID"]));
            if (nRet == -1)
            {
                ltlAlert.Text = "alert('此步骤有前置或后置步骤，不能删除!');";
            }
            else if (nRet == 0)
            {
                ltlAlert.Text = "alert('删除失败!');";
            }
            BindData();

        }
        else
        {
            ltlAlert.Text = "alert('您没有删除此记录的权限!');";
        }

    }

    protected void gvFN_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFN.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btn_return_Click(object sender, EventArgs e)
    {
        Response.Redirect("NWF_WorkFlowTemplateList.aspx", true);
    }
    protected void btn_set_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "$.window('工作流流程配置',800,500,'../NWF/NWF_FlowNodeTransition.aspx?FlowId=" + Request.QueryString["id"] + "&FlowName=" + txtFlowName.Text + "');";
    }
    protected void btnFormDesign_Click(object sender, EventArgs e)
    {
        Response.Redirect("NWF_FormDesign.aspx?FlowId=" + Request.QueryString["id"], true);
    }
}