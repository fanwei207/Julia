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

public partial class hr_Train_app : BasePage
{
    HRTrain hr_train = new HRTrain();
    adamClass adam = new adamClass();
    public int repeateColumn = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDept();
            BindTime();
            BindData();
            GetCourseName();
        }
    }
    protected void BindData()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["appno"]))
        {
            tb_FormID.Text = Request.QueryString["appno"].Replace(" ","");
            hid_formid.Value = Request.QueryString["appno"].Replace(" ", "");
            DataTable dt = hr_train.selectTrainDet(Request.QueryString["appno"]);
            if (dt.Rows.Count > 0)
            {
                tb_FormID.Text = dt.Rows[0]["train_AppNo"].ToString();
                tb_DeptName.Text = dt.Rows[0]["train_DeptName"].ToString();
                tb_Code.Text = dt.Rows[0]["train_code"].ToString();
                tb_AgentDate.Text = dt.Rows[0]["train_AgentDate"].ToString();
                tb_AppEno.Text = dt.Rows[0]["train_AppEno"].ToString();
                tb_AppName.Text = dt.Rows[0]["train_AppName"].ToString();
                lb_CompanyCodeName.Text = dt.Rows[0]["train_Companye"].ToString();
                tb_Date.Text = dt.Rows[0]["train_date"].ToString();
                string hour = dt.Rows[0]["train_hours"].ToString();
                string hours = hour.ToString().PadLeft(2, '0');
                ddl_Hours.Items.FindByValue(hours).Selected = true;
                string Minuter = dt.Rows[0]["train_Minuters"].ToString();
                string Minuters = Minuter.ToString().PadLeft(2, '0');
                ddl_Minuter.Items.FindByValue(Minuters).Selected = true;
                //ddl_Dep.Items.FindByValue(dt.Rows[0]["train_Dep"].ToString()).Selected = true;
                txt_dep.Text = dt.Rows[0]["train_DepName"].ToString();
                tb_Teacher.Value = dt.Rows[0]["train_Teacher"].ToString();
                tb_Subject.Text = dt.Rows[0]["train_Subject"].ToString();
                tb_Phone.Value = dt.Rows[0]["train_Phone"].ToString();
                //tb_Count.Text = dt.Rows[0]["train_EntriesNumber"].ToString();
                tb_Time.Text = dt.Rows[0]["train_TrainTime"].ToString();
                tb_Content.Text = dt.Rows[0]["train_Content"].ToString();
                tb_Place.Text = dt.Rows[0]["train_Place"].ToString();
                txt_Object.Text = dt.Rows[0]["Train_Object"].ToString();
                if (!string.IsNullOrEmpty(dt.Rows[0]["train_otherskill"].ToString()))
                {
                    tb_coursename.Text = dt.Rows[0]["train_otherskill"].ToString();
                    cb_Other.Checked = true;
                }
                else
                {
                    tb_coursename.Visible = false;
                    cb_Other.Visible = false;
                }
                btn_Application.Visible = false;
                DataTable dt1 = hr_train.SelectDomain(Request.QueryString["appno"]);//BindDomainInfo(Request.QueryString["appno"]);
                for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= ck_domain.Items.Count - 1; j++)
                    {
                        if (ck_domain.Items[j].Value == dt1.Rows[i]["train_Domain"].ToString())
                        {
                            ck_domain.Items[j].Selected = true;
                        }
                    }
                }
                tb_Date.ReadOnly = true;
                ddl_Hours.Enabled = false;
                ddl_Minuter.Enabled = false;
                ddl_Dep.Enabled = false;
                tb_Time.ReadOnly = true;
                tb_Subject.ReadOnly = true;
                tb_Content.ReadOnly = true;
                tb_Place.ReadOnly = true;
                btn_Application.Visible = false;
                ck_domain.Enabled = false;
                txt_Object.ReadOnly = true;
                trun.Visible = true;
                skillselect.Visible = false;
                ddl_Dep.Visible = false;
                txt_dep.Visible = true;
                txt_dep.ReadOnly = true;
            }
            else
            {
                DataTable dt1 = hr_train.selectDeptList(Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(Session["deptID"].ToString()));
                tb_DeptName.Text = dt1.Rows[0]["name"].ToString();
                tb_Code.Text = dt1.Rows[0]["code"].ToString();//Session["deptID"].ToString();
                tb_AgentDate.Text = Convert.ToString(System.DateTime.Now);
                tb_AppEno.Text = Session["loginName"].ToString();
                tb_AppName.Text = Session["uName"].ToString();
                if (Convert.ToString(Session["plantcode"]) == "1")
                {
                    lb_CompanyCodeName.Text = "SZX";
                }
                else if (Convert.ToString(Session["plantcode"]) == "2")
                {
                    lb_CompanyCodeName.Text = "ZQL";
                }
                else if (Convert.ToString(Session["plantcode"]) == "5")
                {
                    lb_CompanyCodeName.Text = "YQL";
                }
                else if (Convert.ToString(Session["plantcode"]) == "8")
                {
                    lb_CompanyCodeName.Text = "HQL";
                }
                btn_back.Visible = false;
                trun.Visible = false;
            }
        }
        else
        {
            DataTable dt = hr_train.selectDeptList(Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(Session["deptID"].ToString()));
            tb_DeptName.Text = dt.Rows[0]["name"].ToString();

            tb_Code.Text = dt.Rows[0]["code"].ToString();//Session["deptID"].ToString();
            tb_AgentDate.Text = Convert.ToString(System.DateTime.Now);
            tb_AppEno.Text = Session["loginName"].ToString();
            tb_AppName.Text = Session["uName"].ToString();
            if (Convert.ToString(Session["plantcode"]) == "1")
            {
                lb_CompanyCodeName.Text = "SZX";
            }
            else if (Convert.ToString(Session["plantcode"]) == "2")
            {
                lb_CompanyCodeName.Text = "ZQL";
            }
            else if (Convert.ToString(Session["plantcode"]) == "5")
            {
                lb_CompanyCodeName.Text = "YQL";
            }
            else if (Convert.ToString(Session["plantcode"]) == "8")
            {
                lb_CompanyCodeName.Text = "HQL";
            }
            trun.Visible = false;
            this.btn_back.Visible = false;
            CreateAppNo();
        }
    }

    /// <summary>
    /// ���ɕr�g������
    /// </summary>
    public void BindTime()
    {
        int num = 24;
        if (!string.IsNullOrEmpty(Request.QueryString["appno"]))
        {

        }
        else
        {
            num = 24;//Convert.ToInt32(ConfigurationManager.AppSettings["foretime"].ToString());
        }
        for (int k = 1; k < num + 1; k++)
        {
            this.ddl_Hours.Items.Add(DateTime.Now.AddHours(k).ToString("HH"));
        }
        int min = Convert.ToInt32(DateTime.Now.Minute.ToString());
        for (int i = 0; i <= 59; i++)
        {
            if(i%10 == 0)
            {
                string hour = i.ToString().PadLeft(2, '0');
                this.ddl_Minuter.Items.Add(hour);
            }
        }
    }
    private void BindDept()
    {
        ddl_Dep.Items.Clear();
        ddl_Dep.Items.Add("--��ѡ��һ������--");
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@plantID", Convert.ToString(Session["plantcode"]));
            param[1] = new SqlParameter("@DeptNo", 999999);
            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_train_selectDept", param);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddl_Dep.Items.Add(new ListItem(reader["name"].ToString(), reader["departmentID"].ToString()));
                }
                reader.Close();
            }
        }
        catch 
        {
            ;
        }

    }
    private void CreateAppNo()
    {
        try
        {
            hid_formid.Value = hr_train.CreateAppNo(Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString(), Convert.ToInt32(Session["plantcode"]));//param[5].Value.ToString();
            tb_FormID.Text = hid_formid.Value;
        }
        catch
        {
            ;
        }

    }

    /// <summary>
    /// ��ȡ������Ϣ
    /// </summary>
    /// <param name="type"></param>
    public void GetCourseName()
    {
        DataTable dt = null;
        if (string.IsNullOrEmpty(hid_formid.Value))
        {
            dt = hr_train.selectSilleDet(false, Request.QueryString["appno"], 0, Convert.ToInt32(Session["plantcode"]));
            this.CBList.DataSource = dt;
        }
        else
        {
            dt = hr_train.selectSilleDet(false, hid_formid.Value, 0, Convert.ToInt32(Session["plantcode"]));
            this.CBList.DataSource = dt;
        }

        this.CBList.DataTextField = "train_SkillName";
        this.CBList.DataValueField = "train_skillid";
        if (dt.Rows.Count <= 0)
        {
            cb_Other.Visible = false;
            tb_coursename.Visible = false;
        }
        else
        {
            cb_Other.Visible = true;
            tb_coursename.Visible = true;

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

        for (int k = 0; k < CBList.Items.Count; k++)
        {
                 CBList.Items[k].Selected = true;
                //if (dt.Rows[k]["train_skillid"].ToString() == "99999")
                //{
                //    cb_Other.Checked = true;
                //    tb_coursename.Text = dt.Rows[k]["train_skillname"].ToString();
                //}
        }
    }
    protected string CheckIsOk()
    {
        string error = "";
        if (string.IsNullOrEmpty(tb_Date.Text))
        {
            error += "�������ڲ���Ϊ��;";
        }
        if (ddl_Dep.SelectedIndex == 0)
        {
            error += "��ѡ��������ѵ��λ;";
        }
        if (string.IsNullOrEmpty(tb_Teacher.Value))
        {
            error += "��ѵ��ʦ����Ϊ��;";
        }
        if (string.IsNullOrEmpty(tb_Phone.Value))
        {
            error += "��ϵ�绰����Ϊ��;";
        }
        //if (string.IsNullOrEmpty(tb_Count.Text))
        //{
        //    error += "������������Ϊ��;";
        //}
        if (string.IsNullOrEmpty(tb_Time.Text))
        {
            error += "��ѵʱ������Ϊ��;";
        }
        if (ck_domain.SelectedIndex < 0)
        {
            error += "��ѡ����Ҫ��ѵ����;";
        }
        if (string.IsNullOrEmpty(tb_Subject.Text))
        {
            error += "��ѵ���ⲻ��Ϊ��;";
        }
        if (tb_Subject.Text.Length>50)
        {
            error += "��ѵ������50�ַ�����;";
        }
        if (string.IsNullOrEmpty(tb_Content.Text))
        {
            error += "��ѵ���ݲ���Ϊ��;";
        }
        if (tb_Content.Text.Length > 500)
        {
            error += "��ѵ������500�ַ�����;";
        }
        if (string.IsNullOrEmpty(tb_Place.Text))
        {
            error += "��ѵ�ص㲻��Ϊ��;";
        }
        if (tb_Place.Text.Length > 50)
        {
            error += "��ѵ�ص���50�ַ�����;";
        }
        if (string.IsNullOrEmpty(txt_Object.Text))
        {
            error += "��ѵ������Ϊ��;";
        }
        if (txt_Object.Text.Length > 50)
        {
            error += "��ѵ������50�ַ�����;";
        }
        if (ddl_Dep.SelectedIndex == 0)
        {
            error += "��ѡ��һ��������ѵ��λ;";
        }

        return error;
    }
    protected bool sendemailtoHR(string formid, DateTime dTime)
    {
        DataTable dt = hr_train.SelectHrEmail(formid, Convert.ToInt32(Session["plantcode"]));
        string email = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            email += email + ";" + dt.Rows[i]["email"].ToString();
        }
        if (dt.Rows.Count <= 0)
        {
            email = "ishelp@" + baseDomain.Domain[0];
        }
        StringBuilder sb = new StringBuilder();
        string mailto = email;
        string mailSubject = "ǿ�� - " + "��ѵ�ᱨ" + "֪ͨ";
        sb.Append("<html>");
        sb.Append("<form>");
        sb.Append("<br>");
        sb.Append("HR" + "<br>");
        sb.Append("    ����:" + "<br>");
        sb.Append("<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + tb_DeptName.Text + ":" + tb_AppName.Text + "�ύ��һ����ѵ�ƻ����ȴ�����ȷ��" + " ��" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "�뵽--100��(���¹���-->��ѵ-->��ѵ��ѯ)ҳ��鿴�������ݣ� " + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "100��ϵͳ��"+baseDomain.getPortalWebsite()+" " + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��ѵʱ�䣺" + dTime + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��ѵ���ţ�" + ddl_Dep.SelectedItem.Text + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��ѵ��ʦ��" + tb_Teacher.Value + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��ѵʱ�� ��" + tb_Time.Text + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��ϵ�绰��" + tb_Phone.Value + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��ѵ���⣺" + tb_Subject.Text + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��ѵ���ݣ�" + tb_Content.Text + "<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��ѵ�ص㣺" + tb_Place.Text +"<br>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "��ѵ����" + txt_Object.Text + "<br>");

        sb.Append("</body>");
        sb.Append("</form>");
        sb.Append("</html>");

        if (!this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), mailto, "", mailSubject, sb.ToString()))
        {
            this.Alert("�ʼ�����ʧ�ܣ�");
            return false;
        }
        return true;
    }
    protected void btn_Application_Click(object sender, EventArgs e)
    {
        bool flag2 =false;
        string d = tb_Date.Text;
        string error = CheckIsOk();
        if (error != "")
        {
            this.Alert(error);
            return;
        }
        DateTime dTime = Convert.ToDateTime(this.tb_Date.Text + " " + this.ddl_Hours.Text + ":" + this.ddl_Minuter.Text);
        try
        {
            hr_train.insertTainMstr(Convert.ToInt32(Session["deptID"].ToString()), tb_DeptName.Text, tb_Code.Text, Convert.ToDateTime(tb_AgentDate.Text), tb_AppEno.Text, tb_AppName.Text
                                    , lb_CompanyCodeName.Text, dTime, Convert.ToInt32(ddl_Dep.SelectedValue), ddl_Dep.SelectedItem.Text, tb_Teacher.Value, tb_Subject.Text
                                    , Convert.ToInt32(tb_Phone.Value), 0, Convert.ToInt32(tb_Time.Text), tb_Content.Text, tb_Place.Text, txt_Object.Text
                                    , Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString(), hid_formid.Value);
        }
        catch
        {
            if (!hr_train.AppErrDeleteMstr(this.hid_formid.Value, Convert.ToString(Session["plantcode"])))
            {
                this.Alert("��ѵ�ύʧ�ܣ�����ϵ����Ա��");
                return;
            }
            this.Alert("��ѵ�ύʧ�ܣ�����ϵ����Ա��");
            return;
        }
        for (int i = 0; i < ck_domain.Items.Count; i++)
        {
            CheckBoxList ck = new CheckBoxList();
            if (ck_domain.Items[i].Selected)
            {
                flag2 = hr_train.insertIntoDomainInfo(Convert.ToInt32(ck_domain.Items[i].Value), this.hid_formid.Value, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString());
                if (!flag2)
                {
                    if (!hr_train.AppErrDeleteMstr(this.hid_formid.Value, Convert.ToString(Session["plantcode"])))
                    {
                        this.Alert("��ѵ�ύʧ�ܣ�����ϵ����Ա��");
                        return;
                    }
                }
            }
        }
        if (!this.sendemailtoHR(hid_formid.Value, dTime))
        {
            if (!hr_train.AppErrDeleteMstr(this.hid_formid.Value, Convert.ToString(Session["plantcode"])))
            {
                this.Alert("ɾ����¼ʧ�ܣ�");
                return;
            }
            this.Alert("�����ʼ�ʧ�ܣ�");
            return;
        }
        this.Alert("��ѵ�ύ�ɹ���"+ "���뵥��Ϊ��" + hid_formid.Value);
        CreateAppNo();

    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        if (Convert.ToBoolean(Request.QueryString["per"].ToString()))
        {
            this.Redirect("hr_Train_ReportForPer.aspx?appno=" + tb_FormID.Text);
        }
        else
        {
            this.Redirect("hr_Train_Report.aspx?");
        }
    }
    protected void btn_UpRecord_Click(object sender, EventArgs e)
    {
        this.Redirect("hr_Train_DeptReport.aspx?appno=" + tb_FormID.Text + "&check=false");
    }
    protected void btn_selectskill_Click(object sender, EventArgs e)
    {
        this.Redirect("hr_Train_Skills.aspx?appno=" + tb_FormID.Text);
        //"../HR/hr_Train_Skills.aspx?appno=" + _formid + "&check=false"
    }
}
