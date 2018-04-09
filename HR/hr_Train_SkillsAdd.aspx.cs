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

public partial class hr_Train_SkillsAdd : BasePage
{
    HRTrain hr_train = new HRTrain();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSkillList();
            GridViewSkillBind();
        }
    }
    private void BindSkillList()
    {
        ddl_SkillType.Items.Clear();
        ddl_SkillType.Items.Add("--��ѡ��һ�����--");
        try
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_train_selectSkillTypeList");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddl_SkillType.Items.Add(new ListItem(reader["train_SkillTypes"].ToString(), reader["train_id"].ToString()));
                }
                reader.Close();
            }
        }
        catch
        {
            ;
        }
    }
    private void GridViewSkillBind()
    {
        int skilltypeid = 0;
        if (ddl_SkillType.SelectedIndex == 0)
        {
            skilltypeid = 0;
        }
        else
        {
            skilltypeid = Convert.ToInt32(ddl_SkillType.SelectedValue);
        }
        gv_skilklist.DataSource = hr_train.selectSkillList(skilltypeid);
        gv_skilklist.DataBind();
    }
    protected void gv_skilklist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_skilklist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //string str = gv.DataKeys[e.RowIndex].Values["train_id"].ToString();
            if (!hr_train.DeleteCheckUseSkill(Convert.ToInt32(gv_skilklist.DataKeys[e.RowIndex].Values["train_id"].ToString())))
            {
                this.Alert("�ü����ѱ����ò�����ɾ����");
                return;
            }

            if (hr_train.DeleteSkill(Convert.ToInt32(gv_skilklist.DataKeys[e.RowIndex].Values["train_id"].ToString())))
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
        BindSkillList();
        GridViewSkillBind();
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        this.Redirect("hr_Train_Skills.aspx?appno=" + hid_formid.Value + "&check=false");
    }
    protected void btn_SkillSave_Click(object sender, EventArgs e)
    {
        if (ddl_SkillType.SelectedIndex == 0)
        {
            this.Alert("��ѡ����ѵ���ͣ�");
            return;
        }
        if (string.IsNullOrEmpty(tb_Skillname.Text))
        {
            this.Alert("��ѵ���ܲ���Ϊ�գ�");
            return;
        }
        int status = hr_train.InsertSkillInfo(Convert.ToInt32(ddl_SkillType.SelectedValue), ddl_SkillType.SelectedItem.Text, tb_Skillname.Text
                                    , Convert.ToInt32(Session["uid"]), Session["uName"].ToString(), Convert.ToInt32(Session["plantcode"]));
        if (status == 1)
        {
            this.Alert("��ѵ���ܱ���ɹ���");
            GridViewSkillBind();
        }
        else if (status == 0)
        {
            this.Alert("��ѵ�����Ѵ��ڣ��������ظ���ӣ�");
            return;
        }
        else
        {
            this.Alert("��ѵ���ܱ���ʧ�ܣ�");
            return;
        }
    }
}
