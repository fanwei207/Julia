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

public partial class hr_Train_ReportForPer : BasePage
{
    HRTrain hr_train = new HRTrain();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDept();
            BindData();
        }
    }
    protected void BindData()
    {
        if (this.Security["6090900"].isValid)
        {
            dropDomain.Items.FindByValue(Convert.ToString(Session["plantcode"])).Selected = true;
            dropDomain.Enabled = false;
            ddl_Dep.Enabled = true;
            txt_User.ReadOnly = false;
        }
        else
        {
            dropDomain.Items.FindByValue(Convert.ToString(Session["plantcode"])).Selected = true;
            ddl_Dep.Items.FindByValue(Convert.ToString(Session["deptID"])).Selected = true;
            txt_User.Text = Convert.ToString(Session["loginname"]);
            dropDomain.Enabled = false;
            ddl_Dep.Enabled = false;
            txt_User.ReadOnly = true;
        }

        string deptno = "";
        if (ddl_Dep.SelectedIndex == 0)
        {
            deptno = "";
        }
        else
        {
            deptno =  ddl_Dep.SelectedValue;
        }
        try
        {
            DataTable dt = hr_train.SelectTrainListByPer(dropDomain.SelectedValue, txt_StartDate.Text, txt_EndDate.Text, txt_User.Text, deptno);
            gv_TrainReport.DataSource = dt;
            gv_TrainReport.DataBind();
        }
        catch
        {
            ;
        }

    }
    private void BindDept()
    {
        ddl_Dep.Items.Clear();
        ddl_Dep.Items.Add("--��ѡ��һ������--");
        try
        {
            SqlDataReader reader = hr_train.SelectDeptMentList(Convert.ToString(Session["plantcode"]));
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddl_Dep.Items.Add(new ListItem(reader["name"].ToString(), reader["departmentID"].ToString()));
                }
                reader.Close();
            }
        }
        catch { }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_TrainReport.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _appid = gv_TrainReport.DataKeys[index].Values["train_AppNo"].ToString();
            if (string.IsNullOrEmpty(_appid))
            {
                _appid = "HR-TRN-99999";
            }
            this.Redirect("hr_Train_app.aspx?appno=" + _appid + "&per=true");
            //Convert.ToBoolean(Request.QueryString["check"].ToString())
            //this.Redirect("hr_Train_DeptAdd.aspx?appno=" + hid_formid.Value + "&check=false");
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string deptno = "";
        if (ddl_Dep.SelectedIndex == 0)
        {
            deptno = "";
        }
        else
        {
            deptno = ddl_Dep.SelectedValue;
        }
        try
        {
            DataTable dt = hr_train.SelectTrainListByPer(dropDomain.SelectedValue, txt_StartDate.Text, txt_EndDate.Text, txt_User.Text, deptno);
            if (dt.Rows.Count <= 0)
            {
                this.Alert("����Ҫ��ѯ���ݣ�");
                return;
            }
            string EXTitle = "40^<b>���</b>~^160^<b>���뵥��</b>~^50^<b>��˾</b>~^100^<b>��ѵ��Ա</b>~^100^<b>��ѵ��Ա����</b>~^160^<b>��ѵ����</b>~^100^<b>��ϵ��ʽ</b>~^100^<b>����ʱ��</b>~^60^<b>��ѵ��λ</b>~^80^<b>��ʦ</b>~^70^<b>��������</b>~^70^<b>��ѵʱ��</b>~^220^<b>��ѵ����</b>~^100^<b>��ѵ�ص�</b>~^<b>����</b>~^70^<b>������</b>~^50^<b>����</b>~^";
            this.ExportExcel(EXTitle, dt, true, 1, "train_index", "train_Companye");
        }
        catch
        {
            this.Alert("����ʧ�ܣ�����ϵ����Ա��");
        }
    }
}
