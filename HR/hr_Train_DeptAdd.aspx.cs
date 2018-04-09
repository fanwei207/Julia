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

public partial class hr_Train_DeptAdd : BasePage
{
    HR hr_salary = new HR();
    HRTrain hr_train = new HRTrain();

    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtYear.Text = DateTime.Now.Year.ToString();
            //dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            hid_formid.Value = Request.QueryString["appno"].Replace(" ", "");
            BindDeptByPerson();
            BindByAttedPersonInfo(hid_formid.Value, Convert.ToBoolean(Request.QueryString["check"].ToString()));
            if (Convert.ToBoolean(Request.QueryString["check"].ToString()))
            {
                this.btn_save.Visible = false;
            }
            else
            {
                this.btn_save.Visible = true;
            }
            
        }
    }
    private void BindDeptByPerson()
    {
        ddl_deptbyperson.Items.Clear();
        ddl_deptbyperson.Items.Add("--请选择一个部门--");
        try
        {
            SqlDataReader reader = hr_train.SelectDeptMentList(Convert.ToString(Session["plantcode"]));
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddl_deptbyperson.Items.Add(new ListItem(reader["name"].ToString(), reader["departmentID"].ToString()));
                }
                reader.Close();
            }
        }
        catch
        {
            ;
        }
    }
    private void BindByAttedPersonInfo(string formid, bool checkstatus )
    {
        try
        {
            dgv_Right.DataSource = hr_train.AttedPersonInfo(formid, false, Convert.ToString(Session["plantcode"]));
            dgv_Right.DataBind();
            int countTotal = 0;
            countTotal = Convert.ToInt32(this.dgv_Right.Rows.Count);
            for (int i = 0; i < countTotal; i++)
            {
                CheckBox cbox = (CheckBox)this.dgv_Right.Rows[i].Cells[0].Controls[1];
                cbox.Checked = true;
            }
        }
        catch
        {
            ;
        }
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        this.Redirect("hr_Train_DeptReport.aspx?appno=" + hid_formid.Value + "&check=false");
    }
    private void CheckAttedPerson(string formid, bool checkstatus)
    {
        try
        {
            DataTable dt = hr_train.AttedPersonInfo(formid, false, Convert.ToString(Session["plantcode"]));
            int countTotal = 0;
            countTotal = Convert.ToInt32(this.dgv_Right.Rows.Count);
            for (int i = 0; i < countTotal; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dgv_Right.Rows[i].Cells[1].Text == dt.Rows[j]["loginname"].ToString())
                    {
                        CheckBox cbox = (CheckBox)this.dgv_Right.Rows[i].Cells[0].Controls[1];
                        cbox.Checked = true;
                    }
                }
            }
        }
        catch
        {
            ;
        }
    }
    protected void BingPersonsInfo()
    {
        if (ddl_deptbyperson.SelectedIndex == 0)
        {
            this.Alert("请选择部门！");
            return;
        }

        try
        {
            dgv_Right.DataSource = hr_train.DeptPersonsInfo(Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(ddl_deptbyperson.SelectedValue), tb_EmpNo.Text);
            dgv_Right.DataBind();
            CheckAttedPerson(hid_formid.Value,false);
        }
        catch
        {
            ;
        }
    }
    protected void btn_Query_Click(object sender, EventArgs e)
    {
        BingPersonsInfo();
    }
    protected bool sendemailtoAttenPersons(string formid, int domain)
    {
        DataTable dt = hr_train.GetAttendEmailInfo(formid, Convert.ToString(Session["plantcode"]));
        string email = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            email += email + ";" + dt.Rows[i]["email"].ToString();
           
        }
        if (dt.Rows.Count <= 0)
        {
            email = "ishelp@" + baseDomain.Domain[0];
        }
        DataTable dt1 = hr_train.GetTrainInfo(formid, Convert.ToString(Session["plantcode"]));

        StringBuilder sb = new StringBuilder();
        string mailto = email;
        string mailSubject = "强凌 - " + "参加培训" + "通知";
        sb.Append("<html>");
        sb.Append("<form>");
        sb.Append("<br>");
        sb.Append("各位同仁" + "<br>");
        sb.Append("    您好:" + "<br>");
        sb.Append("<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "您也被邀请参加以下培训!" + " ，" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "培训时间：" + dt1.Rows[0]["train_dTime"].ToString() + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "培训部门：" + dt1.Rows[0]["train_DeptName"].ToString() + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "培训讲师：" + dt1.Rows[0]["train_Teacher"].ToString() + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "培训时数 ：" + dt1.Rows[0]["train_TrainTime"].ToString() + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "联系电话：" + dt1.Rows[0]["train_Phone"].ToString() + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "培训主题：" + dt1.Rows[0]["train_Subject"].ToString() + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "培训內容：" + dt1.Rows[0]["train_Content"].ToString() + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "培训地点：" + dt1.Rows[0]["train_Place"].ToString() + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "培训对象：" + dt1.Rows[0]["Train_Object"].ToString() + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "请到--100库(人事管理-->培训-->培训查询)页面查看具体内容！ " + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "100库系统："+baseDomain.getPortalWebsite()+" " + "<br>");

        sb.Append("</body>");
        sb.Append("</form>");
        sb.Append("</html>");

        if (!this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), mailto, "", mailSubject, sb.ToString()))
        {
            this.Alert("邮件发送失败！");
            return false;
        }
        return true;
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        bool flag1 = false;
        bool flag2 = false;
        int countTotal = 0;
        countTotal = Convert.ToInt32(this.dgv_Right.Rows.Count);
        DataTable dt = null;
        try
        {
            dt = hr_train.AttedPersonInfo(Request.QueryString["appno"].Replace(" ", ""), false, Convert.ToString(Session["plantcode"]));
        }
        catch
        {
            ;
        }
        for (int i = 0; i < countTotal; i++)
        {
            CheckBox cbox = (CheckBox)this.dgv_Right.Rows[i].Cells[0].Controls[1];
            if (cbox.Checked == true)
            {
                GridViewRow gvr = this.dgv_Right.Rows[i];
                string dep = gvr.Cells[4].Text;
                string EmployeeNo = gvr.Cells[1].Text;
                string EmployeeName = gvr.Cells[2].Text;
                string DeptMentName = gvr.Cells[3].Text;
                string DeptMentNo = gvr.Cells[4].Text;
                flag2 = hr_train.insertIntoEmpForRecord(EmployeeNo, EmployeeName, DeptMentNo, DeptMentName, Convert.ToInt32(Session["plantcode"].ToString()), this.hid_formid.Value, Session["uName"].ToString());
            }
            else
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dgv_Right.Rows[i].Cells[1].Text == dt.Rows[j]["loginname"].ToString())
                    {
                        flag1 = hr_train.DeleteEmployeeForRecord(this.hid_formid.Value, dt.Rows[j]["loginname"].ToString(), Convert.ToInt32(Session["plantcode"].ToString()));
                        if (!flag1)
                        {
                            this.Alert("培训提交失败！请联系管理员！");
                            return;
                        }
                    }
                }
            }
        }
        if (!sendemailtoAttenPersons(this.hid_formid.Value, Convert.ToInt32(Session["plantcode"].ToString())))
        {
            this.Alert("发送邮件失败");
            return;
        }
        if (flag2)
        {
            this.Alert("保存成功");
        }
        else
        {
            this.Alert("培训提交失败！请联系管理员！");
            return;
        }
    }
    protected void cb_ChoiceAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChoiceAll = sender as CheckBox;
        for (int i = 0; i <= this.dgv_Right.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)this.dgv_Right.Rows[i].Cells[0].Controls[1];
            if (ChoiceAll.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }
    protected void ddl_deptbyperson_SelectedIndexChanged(object sender, EventArgs e)
    {
        BingPersonsInfo();
    }
}
