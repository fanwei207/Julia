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
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;

public partial class WF_WorkFlowTemplateInsert : BasePage
{  
    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
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
        else
        {
            if (!Regex.IsMatch(txtFlowPre.Text.Trim(), "^[A-Z]+$"))
            {
                ltlAlert.Text = "alert('����ģ�����ֻ��Ϊ��д��ĸ,�Ҳ��ܳ��������ַ�!');";
                return;
            }
        }

        if (fileFlowFormTemplate.Value.Trim().Length <= 0)
        {
            //ltlAlert.Text = "alert('��ѡ����Ҫ�ϴ��ı�ģ��!');";
            //return;
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
        }

        string id = "";
        string name = txtFlowName.Text.Trim();
        string pre = txtFlowPre.Text.Trim();
        int domain = Convert.ToInt32(Session["PlantCode"]);
        string description = txtFlowDescription.Text.Trim();
        
        string formname = System.IO.Path.GetFileNameWithoutExtension(fileFlowFormTemplate.PostedFile.FileName);
        Stream formStream = fileFlowFormTemplate.PostedFile.InputStream;
        int formLength = fileFlowFormTemplate.PostedFile.ContentLength;
        string formType = fileFlowFormTemplate.PostedFile.ContentType;
        byte[] formByte = new byte[formLength];
        formStream.Read(formByte, 0, formLength);

        string remark = txtFlowRemark.Text.Trim();
        bool status = rbNormal.Checked == true ? true : false;
        int createdBy = Convert.ToInt32(Session["uID"]);

        int nRet = wf.AddWorkFlowTemplate(name,pre,domain,description,formname,formType,formByte,remark,status,createdBy);
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
            txtFlowName.Text = string.Empty;
            txtFlowPre.Text = string.Empty;
            txtFlowDescription.Text = string.Empty;
            txtFlowRemark.Text = string.Empty;
            SqlDataReader reader = wf.GetWorkFlowTemplateIDByName(name, domain);
            if(reader.Read())
            {
               id = Convert.ToString(reader["Flow_ID"]);
            }
            reader.Close();
            ltlAlert.Text = "alert('��������ģ��ɹ�!'); window.location.href = '/WF/WF_FlowNode.aspx?id=" + id + "&rm=" + DateTime.Now + "';"; 
        }
    }

    protected void btn_return_Click(object sender, EventArgs e)
    {
        Response.Redirect("WF_WorkFlowTemplateList.aspx?rm=" + DateTime.Now, true);
    }
}