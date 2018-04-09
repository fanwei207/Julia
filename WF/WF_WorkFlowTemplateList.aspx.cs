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

public partial class WF_WorkFlowTemplate : BasePage
{
  
    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //�ж��û��Ƿ�߱�������ģ��鿴��Ȩ��

            //�ж��û��Ƿ�߱�������ģ����ӵ�Ȩ��
            if (this.Security["13001"].isValid)
            {
                BtnAdd.Visible = true;
            }
            else
            {
             
                BtnAdd.Visible = false;
            }

            //�ж��û��Ƿ�߱��������õ�Ȩ��
           if (this.Security["13009"].isValid)
            {
                gvWF.Columns[9].Visible = true;
            }
            else
            {
                gvWF.Columns[9].Visible = false;
            }
        }
    }

    protected override void BindGridView()
    {
        DataTable dt = wf.GetWorkFlowTemplate(txtFlowName.Text.Trim(), txtCreatedBy.Text.Trim(), txtCreatedDate1.Text.Trim(), txtCreatedDate2.Text.Trim(), chkall.Checked, Convert.ToInt32(Session["PlantCode"]));
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.gvWF.DataSource = dt;
            this.gvWF.DataBind();
            int ColunmCount = gvWF.Rows[0].Cells.Count;
            gvWF.Rows[0].Cells.Clear();
            gvWF.Rows[0].Cells.Add(new TableCell());
            gvWF.Rows[0].Cells[0].ColumnSpan = ColunmCount;
            gvWF.Rows[0].Cells[0].Text = "û������";
            gvWF.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            this.gvWF.DataSource = dt;
            this.gvWF.DataBind();
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (txtCreatedDate1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtCreatedDate1.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('���ڸ�ʽ����ȷ����ȷ��ʽ��:YYYY-MM-DD');";
                return;
            }
        }

        if (txtCreatedDate2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtCreatedDate2.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('���ڸ�ʽ����ȷ����ȷ��ʽ��:YYYY-MM-DD');";
                return;
            }
        }
        BindGridView();
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    { 
        Response.Redirect("WF_WorkFlowTemplateInsert.aspx?rm="+ DateTime.Now);
    }

    protected void gvWF_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWF.PageIndex = e.NewPageIndex;
        this.BindGridView();
    }

    protected void gvWF_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //�ж��û��Ƿ�߱�������ģ��ɾ���ĵ�Ȩ��
         if (!this.Security["13013"].isValid)
        {
            if (wf.JudgeWorkFlowIsCreatedBySelf(Convert.ToInt32(gvWF.DataKeys[e.RowIndex].Value), Convert.ToInt32(Session["uID"])))
            {
                int nRet = 0;
                nRet = wf.DeleteWorkFlowTemplate(Convert.ToInt32(gvWF.DataKeys[e.RowIndex].Value));
                if (nRet == 0)
                {
                    ltlAlert.Text = "alert('������ģ���Ѿ���ʹ��,���޷�ɾ��!');";
                }
                else if (nRet < 0)
                {
                    ltlAlert.Text = "alert('ɾ������ģ��ʧ��,����ϵ����Ա!');";
                }
                BindGridView();
            }
            else
            {
                ltlAlert.Text = "alert('�ù�����ģ�岻��������,����Ȩ��ɾ��!');";
            }
        }
        else
        {
            int nRet = 0;
            nRet = wf.DeleteWorkFlowTemplate(Convert.ToInt32(gvWF.DataKeys[e.RowIndex].Value));
            if (nRet == 0)
            {
                ltlAlert.Text = "alert('������ģ���Ѿ���ʹ��,���޷�ɾ��!');";
            }
            else if (nRet < 0)
            {
                ltlAlert.Text = "alert('ɾ������ģ��ʧ��,����ϵ����Ա!');";
            }
            BindGridView();
        }
    }

    protected void gvWF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
        }
    }

    protected void gvWF_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            //ltlAlert.Text = "window.open('WF_ExcelEdit.aspx?id=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now + "', '_blank');";
            ltlAlert.Text = "var w = window.open('/WF/WF_ExcelEdit.aspx?id=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now;
            ltlAlert.Text += "','','menubar=no,scrollbars=no,resizable=no,width=900,height=530,top=0,left=0'); w.focus();";
        }
        if (e.CommandName == "myEdit")
        {
            //�ж��û��Ƿ�߱�������ģ��ɾ���ĵ�Ȩ��
             if (!this.Security["13013"].isValid)
            {
                if (wf.JudgeWorkFlowIsCreatedBySelf(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(Session["uID"])))
                {
                    //ltlAlert.Text = "window.open('WF_WorkFlowTemplateEdit.aspx?id=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now + "', '_blank');";
                 //Response.Redirect("WF_WorkFlowTemplateEdit.aspx?id=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now, true);
                    this.Redirect("WF_WorkFlowTemplateEdit.aspx?id=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now);
                }
                else
                {
                    ltlAlert.Text = "alert('�ù�����ģ�岻��������,����Ȩ�ޱ༭!');";
                }
            }
            else
            {   
                Response.Redirect("WF_WorkFlowTemplateEdit.aspx?id=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now, true); 
            }
        }
        if (e.CommandName == "step")
        {  
            Response.Redirect("WF_FlowNode.aspx?id=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now, true);   
        }
        if (e.CommandName == "formDesign")
        {
            Response.Redirect("WF_FormDesign.aspx?FlowId=" + e.CommandArgument.ToString().Trim() + "&rm=" + DateTime.Now, true);
        }
    }
}
