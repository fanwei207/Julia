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
using System.Data.SqlClient;
using System.IO;

public partial class WF_WorkFlowTemplateEdit : BasePage
{
   
    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fill();
        }
    }

    protected void Fill()
    {
        if (Request.QueryString["id"] != null)
        {
            SqlDataReader reader = wf.GetWorkFlowTemplateByID(Convert.ToInt32(Request.QueryString["id"]));
            if (reader.Read())
            {
                txtFlowName.Text = Convert.ToString(reader["Flow_Name"]);
                txtFlowPre.Text = Convert.ToString(reader["Flow_Req_Pre"]);
                txtFlowDescription.Text = Convert.ToString(reader["Flow_Description"]);
                hlFlowTemplateName.Text = Convert.ToString(reader["Flow_FormTemplateName"]);
                hlFlowTemplateName.NavigateUrl = "WF_ExcelEdit.aspx?id=" + Request.QueryString["id"] + "&rm=" + DateTime.Now;
                txtFlowRemark.Text = Convert.ToString(reader["Flow_Remark"]);
                if (Convert.ToBoolean(reader["Flow_Status"]))
                {
                    rbNormal.Checked = true;
                    rbAbandon.Checked = false;
                }
                else
                {
                    rbNormal.Checked = false;
                    rbAbandon.Checked = true;
                }
                if (Convert.ToBoolean(reader["Flow_StopFormTemplate"]))
                {
                    chkStopTemp.Checked = true;
                }
                else
                {
                    chkStopTemp.Checked = false;
                }
                txtCreatedBy.Text = Convert.ToString(reader["Flow_CreatedBy"]);
                txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", reader["Flow_CreatedDate"]);
            }
            reader.Close();
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        int nRet = 0;
        if (txtFlowName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('����ģ�����Ʋ���Ϊ��!');";
            return;
        }

        if (txtFlowPre.Text == string.Empty)
        {
            ltlAlert.Text = "alert('����ģ����벻��Ϊ��!');";
            return;
        }

        if (fileFlowFormTemplate.Value.Trim().Length <= 0)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string name = txtFlowName.Text.Trim();
            string pre = txtFlowPre.Text.Trim();
            string description = txtFlowDescription.Text.Trim();
            string formname = hlFlowTemplateName.Text.Trim();
            string remark = txtFlowRemark.Text.Trim().Replace("\n", "<br>");
            bool status = rbNormal.Checked == true ? true : false;
            int modifiedBy = Convert.ToInt32(Session["uID"]);
            bool stopTemp = chkStopTemp.Checked;
            nRet = wf.UpdateWorkFlowTemplateWithNoFile(id, name, pre, description, formname, remark, status, modifiedBy, stopTemp);
        }
        else
        {
            string tempname = Server.MapPath("/Excel/" + Path.GetFileName(fileFlowFormTemplate.PostedFile.FileName));
            fileFlowFormTemplate.PostedFile.SaveAs(tempname);
            string extension = wf.GetFileExtension(tempname);
            File.Delete(tempname);
            if (extension != "6063")
            {
                ltlAlert.Text = "alert('ģ����ʽֻ��Ϊxml���ļ�!');";
                return;
            }

            int id = Convert.ToInt32(Request.QueryString["id"]);
            string name = txtFlowName.Text.Trim();
            string pre = txtFlowPre.Text.Trim();
            string description = txtFlowDescription.Text.Trim();

            string formname = System.IO.Path.GetFileNameWithoutExtension(fileFlowFormTemplate.PostedFile.FileName);
            Stream formStream = fileFlowFormTemplate.PostedFile.InputStream;
            int formLength = fileFlowFormTemplate.PostedFile.ContentLength;
            string formType = fileFlowFormTemplate.PostedFile.ContentType;
            byte[] formByte = new byte[formLength];
            formStream.Read(formByte, 0, formLength);

            string remark = txtFlowRemark.Text.Trim().Replace("\n", "<br>");
            bool status = rbNormal.Checked == true ? true : false;
            int modifiedBy = Convert.ToInt32(Session["uID"]);
            bool stopTemp = chkStopTemp.Checked;
            nRet = wf.UpdateWorkFlowTemplateWithFile(id, name, pre, description, formname, formType, formByte, remark, status, modifiedBy, stopTemp);
        }

        if (nRet == -1)
        {
            ltlAlert.Text = "alert('��������ģ��ʧ�ܣ�����ϵ����Ա!');";
        }
        else if (nRet == 0)
        {
            ltlAlert.Text = "alert('������ģ�������Ѿ����ڣ����������ģ������!');";
        }
        else if (nRet == -2)
        {
            ltlAlert.Text = "alert('������ģ������Ѿ����ڣ����������ģ�����!');";
        }
        else
        {
            Response.Write("<script>alert('����ģ����Ϣ�޸ĳɹ�!');window.location.href ='/WF/WF_WorkFlowTemplateList.aspx'</script>");
        }
    }

    protected void btn_return_Click(object sender, EventArgs e)
    { 
        this.Redirect("WF_WorkFlowTemplateList.aspx?rm=" + DateTime.Now);
    }
}
