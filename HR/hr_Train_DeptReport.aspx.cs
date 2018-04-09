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

public partial class hr_Train_DeptReport : BasePage
{
    HRTrain hr_train = new HRTrain();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hid_formid.Value = Request.QueryString["appno"].Replace(" ","");
            BindByAttedPersonInfo(hid_formid.Value, Convert.ToBoolean(Request.QueryString["check"].ToString()));
            CheckAttedPerson(hid_formid.Value, false);
            if (Convert.ToBoolean(Request.QueryString["check"].ToString()))
            {
                this.btn_save.Visible = false;
                btn_check.Visible = true;
            }
            else
            {
                this.btn_save.Visible = true;
                btn_check.Visible = true;
            }
            if (this.Security["6090500"].isValid)
            {
                btn_save.Enabled = true;
                btn_check.Enabled = true;
            }
            else
            {
                btn_save.Enabled = false;
                btn_check.Enabled = false;
            }
        }
    }
    private void CheckAttedPerson(string formid, bool checkstatus)
    {
        try
        {
            DataTable dt = hr_train.AttedPersonInfo(formid, false, Convert.ToString(Session["plantcode"]));
            int countTotal = 0;
            countTotal = Convert.ToInt32(this.dgv_Left.Rows.Count);
            for (int i = 0; i < countTotal; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dgv_Left.Rows[i].Cells[1].Text == dt.Rows[j]["loginname"].ToString())
                    {
                        if (Convert.ToBoolean(dt.Rows[j]["train_CheckStatus"]))
                        {
                            CheckBox cbox = (CheckBox)this.dgv_Left.Rows[i].Cells[0].Controls[1];
                            cbox.Checked = true;
                        }
                    }
                }
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
            DataTable dt = hr_train.AttedPersonInfo(formid, false, Convert.ToString(Session["plantcode"])); 
            dgv_Left.DataSource = dt;
            dgv_Left.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        this.Redirect("hr_Train_app.aspx?appno=" + hid_formid.Value.Replace(" ",""));
    }
    protected void cb_ALL_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb_All = sender as CheckBox;
        for (int i = 0; i <= dgv_Left.Rows.Count - 1; i++)
        {
            CheckBox cbox = (CheckBox)this.dgv_Left.Rows[i].FindControl("CheckBox1");
            if (cb_All.Checked == true)
            {
                cbox.Checked = true;
            }
            else
            {
                cbox.Checked = false;
            }
        }
    }
    protected void btn_check_Click(object sender, EventArgs e)
    {
        bool flag2 = false;
        int countTotal = 0;
        countTotal = Convert.ToInt32(this.dgv_Left.Rows.Count);
        for (int i = 0; i < countTotal; i++)
        {
            CheckBox cbox = (CheckBox)this.dgv_Left.Rows[i].Cells[0].Controls[1];
            if (cbox.Checked == true)
            {
                GridViewRow gvr = this.dgv_Left.Rows[i];
                string dep = gvr.Cells[4].Text;
                string EmployeeNo = gvr.Cells[1].Text;
                string EmployeeName = gvr.Cells[2].Text;
                string DeptMentName = gvr.Cells[3].Text;
                string DeptMentNo = gvr.Cells[4].Text;
                flag2 = hr_train.insertCheckEmpForRecord(EmployeeNo, EmployeeName, DeptMentNo, DeptMentName, Convert.ToInt32(Session["plantcode"].ToString()), this.hid_formid.Value, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString());
            }
        }
        if (flag2)
        {
            this.Alert("实到人员确认成功");
            this.btn_check.Enabled = false;
            this.btn_save.Enabled = false;
        }

        else
        {
            this.Alert("培训提交失败！请联系管理员！");
            return;
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        this.Redirect("hr_Train_DeptAdd.aspx?appno=" + hid_formid.Value + "&check=false");
    }
}
