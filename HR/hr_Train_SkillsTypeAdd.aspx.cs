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
using adamFuncs;
using Wage;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class hr_Train_SkillsTypeAdd : BasePage
{
    HRTrain hr_train = new HRTrain();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewSkillTypeBind();
        }
    }
    private void GridViewSkillTypeBind()
    {
        gv.DataSource = hr_train.selectSkillType();
        gv.DataBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (!hr_train.DeleteSkillTypeCheckUse(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["train_id"].ToString())))
            {
                this.Alert("����ѵ����ѱ����ò�����ɾ����");
                return;
            }
            if (hr_train.DeleteSkillType(Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["train_id"].ToString())))
            {
                this.Alert("ɾ���ɹ���");
            }
            else
            {
                this.Alert("ɾ��ʧ�ܣ�");
            }
        }
        catch
        {
            this.Alert("ɾ��ʧ�ܣ�����ϵ����Ա��"); ;
        }

        GridViewSkillTypeBind();
    }
    protected void gv_skilklist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        this.Redirect("hr_Train_Skills.aspx?appno=" + hid_formid.Value + "&check=false");
    }
    protected void btn_skillTypeSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_skilltype.Text))
        {
            this.Alert("�������Ͳ���Ϊ�գ�");
            return;
        }
        int status =hr_train.InsertSkillType(txt_skilltype.Text, Convert.ToInt32(Session["uid"]), Session["uName"].ToString(), Convert.ToInt32(Session["plantcode"]));
        if (status==1)
        {
            GridViewSkillTypeBind();
            this.Alert("�������ͱ���ɹ���");
            return;
        }
        else if (status == 0)
        {
            this.Alert("���������Ѵ��ڣ��������ظ���ӣ�");
            return;
        }
        else
        {
            this.Alert("�������ͱ���ʧ�ܣ�");
            return;
        }
    }
}
