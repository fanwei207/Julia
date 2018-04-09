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
        ddl_SkillType.Items.Add("--请选择一个类别--");
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
                this.Alert("该技能已被引用不能做删除！");
                return;
            }

            if (hr_train.DeleteSkill(Convert.ToInt32(gv_skilklist.DataKeys[e.RowIndex].Values["train_id"].ToString())))
            {
                this.Alert("删除成功！");
            }
            else
            {
                this.Alert("删除失败！");
            }
        }
        catch
        {
            this.Alert("删除失败！请联系管理员！"); ;
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
            this.Alert("请选择培训类型！");
            return;
        }
        if (string.IsNullOrEmpty(tb_Skillname.Text))
        {
            this.Alert("培训技能不能为空！");
            return;
        }
        int status = hr_train.InsertSkillInfo(Convert.ToInt32(ddl_SkillType.SelectedValue), ddl_SkillType.SelectedItem.Text, tb_Skillname.Text
                                    , Convert.ToInt32(Session["uid"]), Session["uName"].ToString(), Convert.ToInt32(Session["plantcode"]));
        if (status == 1)
        {
            this.Alert("培训技能保存成功！");
            GridViewSkillBind();
        }
        else if (status == 0)
        {
            this.Alert("培训技能已存在，不允许重复添加！");
            return;
        }
        else
        {
            this.Alert("培训技能保存失败！");
            return;
        }
    }
}
