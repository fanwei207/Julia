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
using System.IO;
using System.Data.SqlClient;

public partial class WF_WorkFlowTemplateEdit : BasePage
{
    WorkFlow wf = new WorkFlow();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //�ж��û��Ƿ�߱��������õ�Ȩ��


            //�ж��û��Ƿ�߱�������ģ��ɾ���ĵ�Ȩ��
            if (!this.Security["13013"].isValid)
            {
                if (wf.JudgeWorkFlowIsCreatedBySelf(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Session["uID"])))
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

            SqlDataReader reader = wf.GetWorkFlowTemplateByID(Convert.ToInt32(Request.QueryString["id"]));
            if (reader.Read())
            {
                txtFlowName.Text = Convert.ToString(reader["Flow_Name"]);
            }
            reader.Close();
            databind();
        }
    }

    protected void databind()
    {
        DataTable dt = wf.GetFlowNode(Convert.ToInt32(Request.QueryString["id"]));
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.gvFN.DataSource = dt;
            this.gvFN.DataBind();
            int ColunmCount = gvFN.Rows[0].Cells.Count;
            gvFN.Rows[0].Cells.Clear();
            gvFN.Rows[0].Cells.Add(new TableCell());
            gvFN.Rows[0].Cells[0].ColumnSpan = ColunmCount;
            gvFN.Rows[0].Cells[0].Text = "���޲���";
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
            ltlAlert.Text = "alert('�������Ʋ���Ϊ��!');";
            return;
        }

        if (txtNodeSortValue.Text == string.Empty)
        {
            ltlAlert.Text = "alert('��Ų���Ϊ��!');";
            return;
        }

        if (btn_add.Text == "����")//add
        {
            int flowID = Convert.ToInt32(Request.QueryString["id"]);
            string name = txtNodeName.Text.Trim();
            string[] _sortorder = txtNodeSortValue.Text.Trim().Split(new char[] { ';' });
            int sortorder = Convert.ToInt32(_sortorder[0]);
            string sortname = _sortorder[1];
            string description = txtNodeDesc.Text.Trim();
            int createdBy = Convert.ToInt32(Session["uID"]);

            int nRet = wf.AddFlowNode(flowID, name, sortorder, sortname, description, createdBy);
            if (nRet == -1)
            {
                ltlAlert.Text = "alert('�������̲���ʧ�ܣ�����ϵ����Ա!');";
            }
            else if (nRet == 0)
            {
                ltlAlert.Text = "alert('�ò������ƻ�����Ѿ����ڣ�������������ƻ����!');";
            }
            else if (nRet == 2)
            {
                ltlAlert.Text = "alert('���̵ĵ�һ�������Ϊ10�����ˣ���ѡ�����10!');";
            }
            else if (nRet == 3)
            {
                ltlAlert.Text = "alert('���̲�����Ų�������������ѡ��!');";
            }
            else
            {
                txtNodeName.Text = string.Empty;
                txtNodeDesc.Text = string.Empty;
                txtNodeSort.Text = string.Empty;
                //ltlAlert.Text = "alert('�������̲���ɹ�!');";
            }
            databind();
        }
        else//edit
        {
            int id = Convert.ToInt32(lblid.Text.Trim());
            int flowID = Convert.ToInt32(Request.QueryString["id"]);
            string name = txtNodeName.Text.Trim();
            string[] _sortorder = txtNodeSortValue.Text.Trim().Split(new char[] { ';' });
            int sortorder = Convert.ToInt32(_sortorder[0]);
            string sortname = _sortorder[1];
            string description = txtNodeDesc.Text.Trim();
            int modifiedBy = Convert.ToInt32(Session["uID"]);

            int nRet = wf.UpdateFlowNode(id, flowID, name, sortorder, sortname, description, modifiedBy);
            if (nRet == -1)
            {
                ltlAlert.Text = "alert('�޸����̲���ʧ�ܣ�����ϵ����Ա!');";
            }
            else if (nRet == 0)
            {
                ltlAlert.Text = "alert('�ò��������Ѿ����ڣ��������������!');";
            }
            else
            {
                txtNodeName.Text = string.Empty;
                txtNodeDesc.Text = string.Empty;
                txtNodeSort.Text = string.Empty;
                btn_add.Text = "����";
              
                btn_select.Enabled = true;
                //ltlAlert.Text = "alert('�޸����̲���ɹ�!');";
            }
            databind();
        }
    }

    protected void btn_select_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "openNS('WF_NodeSortChoose.aspx');";
        databind();
    }

    protected void gvFN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
           
            //�ж��û��Ƿ�߱�������ģ��ɾ���ĵ�Ȩ��
            if (!this.Security["13013"].isValid)
            {
                if (wf.JudgeFlowNodeIsCreatedBySelf(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["uID"])))
                {
                    SqlDataReader reader = wf.GetFlowNodeByID(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                    if (reader.Read())
                    {
                        lblid.Text = Convert.ToString(reader["Node_ID"]);
                        txtNodeName.Text = Convert.ToString(reader["Node_Name"]);
                        txtNodeDesc.Text = Convert.ToString(reader["Node_Description"]);
                        txtNodeSort.Text = Convert.ToString(reader["Sort_Order"]) + ";" + Convert.ToString(reader["Sort_Name"]);
                        txtNodeSortValue.Text = Convert.ToString(reader["Sort_Order"]) + ";" + Convert.ToString(reader["Sort_Name"]);
                        btn_add.Text = "�޸�";
                        btn_select.Enabled = false;
                    }
                    reader.Close();
                }
                else
                {
                    ltlAlert.Text = "alert('�ù�����ģ�岽�費��������,����Ȩ�ޱ༭!');";
                }
            }
            else
            {
                SqlDataReader reader = wf.GetFlowNodeByID(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                if (reader.Read())
                {
                    lblid.Text = Convert.ToString(reader["Node_ID"]);
                    txtNodeName.Text = Convert.ToString(reader["Node_Name"]);
                    txtNodeDesc.Text = Convert.ToString(reader["Node_Description"]);
                    txtNodeSort.Text = Convert.ToString(reader["Sort_Order"]) + ";" + Convert.ToString(reader["Sort_Name"]);
                    txtNodeSortValue.Text = Convert.ToString(reader["Sort_Order"]) + ";" + Convert.ToString(reader["Sort_Name"]);
                    btn_add.Text = "�޸�";
                    btn_select.Enabled = false;
                }
            }
        }

        if (e.CommandName == "Reviewer")
        {
            //�ж��û��Ƿ�߱�������ģ��ɾ���ĵ�Ȩ��
            if (!this.Security["13013"].isValid)
            {
                if (wf.JudgeFlowNodeIsCreatedBySelf(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["uID"])))
                {
                    ltlAlert.Text = "window.open('/WF/WF_FlowNodePersonChoose.aspx?nid=" + e.CommandArgument.ToString().Trim() + "&fid=" + Request.QueryString["id"].ToString() + "&rm=" + DateTime.Now;
                    ltlAlert.Text += "','','menubar=no,scrollbars=no,resizable=no,width=700,height=500,top=0,left=0');";
                }
                else
                {
                    ltlAlert.Text = "alert('�ù�����ģ�岽�費��������,����Ȩ���޸Ĳ�����!');";
                }
            }
            else
            {
                ltlAlert.Text = "window.open('/WF/WF_FlowNodePersonChoose.aspx?nid=" + e.CommandArgument.ToString().Trim() + "&fid=" + Request.QueryString["id"].ToString() + "&rm=" + DateTime.Now;
                ltlAlert.Text += "','','menubar=no,scrollbars=no,resizable=no,width=700,height=500,top=0,left=0');";
            }
        }
    }

    protected void gvFN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
        //�ж��û��Ƿ�߱�������ģ��ɾ���ĵ�Ȩ��
        if (!this.Security["13013"].isValid)
        {
            if (wf.JudgeFlowNodeIsCreatedBySelf(Convert.ToInt32(gvFN.DataKeys[e.RowIndex].Value), Convert.ToInt32(Session["uID"])))
            {
                int nRet = wf.DeleteFlowNode(Convert.ToInt32(gvFN.DataKeys[e.RowIndex].Value), Convert.ToInt32(Request.QueryString["id"]));
                if (nRet == 0)
                {
                    ltlAlert.Text = "alert('ֻ�н���ű�����ļ�¼ȫ��ɾ��������ɾ��������¼!');";
                }
                else if (nRet == -1)
                {
                    ltlAlert.Text = "alert('ɾ��ʧ��,����ϵ����Ա!');";
                }
                else
                {

                }
                databind();
            }
            else
            {
                ltlAlert.Text = "alert('�ù�����ģ�岽�費��������,����Ȩ��ɾ��!');";
            }
        }
        else
        {
            int nRet = wf.DeleteFlowNode(Convert.ToInt32(gvFN.DataKeys[e.RowIndex].Value), Convert.ToInt32(Request.QueryString["id"]));
            if (nRet == 0)
            {
                ltlAlert.Text = "alert('ֻ�н���ű�����ļ�¼ȫ��ɾ��������ɾ��������¼!');";
            }
            else if (nRet == -1)
            {
                ltlAlert.Text = "alert('ɾ��ʧ��,����ϵ����Ա!');";
            }
            else
            {

            }
            databind();
        }
    }

    protected void gvFN_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFN.PageIndex = e.NewPageIndex;
        databind();
    }

    protected void gvFN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }

    protected void btn_return_Click(object sender, EventArgs e)
    { 
        Response.Redirect("WF_WorkFlowTemplateList.aspx", true);
    }
}
