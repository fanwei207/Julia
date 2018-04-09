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

public partial class hr_Train_Skills : BasePage
{
    HRTrain hr_train = new HRTrain();
    adamClass adam = new adamClass();
    public int repeateColumn = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            BindSkillTypeList();
            hid_formid.Value = Request.QueryString["appno"].Replace(" ","");
            int skilltypeid = 0;
            if (ddl_SkillType.SelectedIndex == 0)
            {
                skilltypeid = 0;
            }
            else
            {
                skilltypeid = Convert.ToInt32(ddl_SkillType.SelectedValue);
            }
            GetCourseName(false, hid_formid.Value, skilltypeid);
        }
    }
    protected void BindData()
    {
        DataTable dt = hr_train.selectTrainDet(Request.QueryString["appno"]);
        if (dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dt.Rows[0]["train_otherskill"].ToString()))
            {
                tb_coursename.Text = dt.Rows[0]["train_otherskill"].ToString();
                cb_Other.Checked = true;
            }
        }
    }

    private void BindSkillTypeList()
    {
        ddl_SkillType.Items.Clear();
        ddl_SkillType.Items.Add("--请选择一个部门--");
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

    /// <summary>
    /// 获取技能信息
    /// </summary>
    /// <param name="type"></param>
    public void GetCourseName(bool typeORformid, string formid, int skilltypeid)
    {
        DataTable dt = hr_train.selectSilleDet(typeORformid, formid, skilltypeid, Convert.ToInt32(Session["plantcode"]));
        this.CBList.DataSource = dt;
        if (typeORformid)
        {
            this.CBList.DataTextField = "train_SkillName";
            this.CBList.DataValueField = "train_id";
        }
        else
        {
            this.CBList.DataTextField = "train_SkillName";
            this.CBList.DataValueField = "train_skillid";
        }
        this.CBList.DataBind();
        if (dt.Rows != null && dt.Rows.Count >= 6)
        {
            repeateColumn = 3;
        }
        else
        {
            repeateColumn = Convert.ToInt32(dt.Rows.Count.ToString());
        }
        this.CBList.RepeatColumns = repeateColumn;

        DataTable dt1 = hr_train.selectSilleDet(false, formid, skilltypeid, Convert.ToInt32(Session["plantcode"]));
        for (int k = 0; k < CBList.Items.Count; k++)
        {
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                if (CBList.Items[k].Value == dt1.Rows[j]["train_skillid"].ToString())
                {
                    CBList.Items[k].Selected = true;
                }
                if (dt1.Rows[j]["train_skillid"].ToString() == "99999")
                {
                    cb_Other.Checked = true;
                    tb_coursename.Text = dt1.Rows[j]["train_skillname"].ToString();
                }
            }
        }
    }

    protected bool CourseNameSave(string formid, int plantcode)
    {
        if (ddl_SkillType.SelectedIndex != 0)
        {
            for (int k = 0; k < CBList.Items.Count; k++)
            {
                if (CBList.Items[k].Selected)
                {
                    try
                    {
                        hr_train.SaveSkills(formid, false, Convert.ToInt32(ddl_SkillType.SelectedValue), ddl_SkillType.SelectedItem.Text, Convert.ToInt32(CBList.Items[k].Value), CBList.Items[k].Text, plantcode, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString());
                    }
                    catch
                    {
                        this.Alert("培训提交失败！请联系管理员！");
                        return false;
                    }
                }
                else
                {
                    DataTable dt = hr_train.selectSilleDet(false, formid, Convert.ToInt32(ddl_SkillType.SelectedValue), Convert.ToInt32(Session["plantcode"]));
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (CBList.Items[k].Value == dt.Rows[j]["train_skillid"].ToString())
                            {
                                hr_train.DeleteSelectSkill(formid, Convert.ToInt32(dt.Rows[j]["train_skillid"].ToString()));
                            }
                        }
                    }
                }
            }
        }
        if (cb_Other.Checked)
        {
            for (int k = 0; k < CBList.Items.Count; k++)
            {
                if (!CBList.Items[k].Selected)
                {
                    try
                    {
                        DataTable dt = hr_train.selectSilleDet(false, formid, 0, Convert.ToInt32(Session["plantcode"]));
                        if (CBList.Items[k].Value == dt.Rows[k]["train_skillid"].ToString())
                        {
                            hr_train.DeleteSelectSkill(formid, Convert.ToInt32(dt.Rows[k]["train_skillid"].ToString()));
                        }
                    }
                    catch
                    {
                        this.Alert("培训提交失败！请联系管理员！");
                        return false;
                    }
                }
            }
            try
            {
                hr_train.SaveSkills(formid, true, 0, ddl_SkillType.SelectedItem.Text, 99999, tb_coursename.Text, plantcode, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString());
            }
            catch
            {
                this.Alert("培训提交失败！请联系管理员！");
                return false;
            }
        }
        return true;
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        this.Redirect("hr_Train_app.aspx?appno=" + hid_formid.Value +"&per=ture");
    }
    protected void btn_find_Click(object sender, EventArgs e)
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
        GetCourseName(true, hid_formid.Value, skilltypeid);
    }
    protected void btn_Check_Click(object sender, EventArgs e)
    {
        if (!cb_Other.Checked)
        {
            if (ddl_SkillType.SelectedIndex == 0)
            {
                this.Alert("请选择培训类型！");
                return;
            }
        }

        if (!CourseNameSave(this.hid_formid.Value, Convert.ToInt32(Session["plantcode"])))
        {
            if (!hr_train.AppErrDeleteMstr(this.hid_formid.Value, Convert.ToString(Session["plantcode"])))
            {
                this.Alert("培训技能保存失败！");
                return;
            }
        }
        this.Alert("培训技能保存成功！");
    }
    protected void ddl_SkillType_SelectedIndexChanged(object sender, EventArgs e)
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
        GetCourseName(true, hid_formid.Value, skilltypeid);
    }
}
