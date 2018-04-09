using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Performance_Monit_PIC : System.Web.UI.Page
{
    Monitor monit = new Monitor();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindCompany();
            ddl_Company.SelectedIndex = -1;
            try
            {
                ddl_Company.Items.FindByValue(Session["PlantCode"].ToString()).Selected = true;
            }
            catch
            {
                ddl_Company.SelectedIndex = -1;
            }
            txtUsers.Text += ";";
            txtUserNames.Text += ";";
            BindDepartment();
            loadUser();
        }
        
    }
    private void BindCompany()
    {
        ddl_Company.DataSource = admin_AccessApply.getCompanyInfo();
        ddl_Company.DataBind();
    }

    private void BindDepartment()
    {
        ddl_department.DataSource = admin_AccessApply.getDepartmentInfo(Convert.ToInt32(ddl_Company.SelectedValue.ToString()));
        ddl_department.DataBind();
    }
    protected void loadUser()
    {
        if (chkUser.Items.Count > 0)
            chkUser.Items.Clear();
        int plantid = Convert.ToInt32(ddl_Company.SelectedValue);
        int departmentId = Convert.ToInt32(ddl_department.SelectedValue);
        string userName = txt_user.Text.Trim();
        string strUser = string.Empty;
        string strUserID = string.Empty;

        DataTable dt = admin_AccessApply.getUserByDepart(departmentId, txt_user.Text.ToString(), ddl_Company.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                ListItem item = new ListItem(dt.Rows[i].ItemArray[2].ToString().Trim() + "~" + dt.Rows[i].ItemArray[1].ToString().Trim(), dt.Rows[i].ItemArray[0].ToString().Trim());
                chkUser.Items.Add(item);

                if (txtUsers.Text.Trim().IndexOf(";" + chkUser.Items[i].Value + ";") > -1)
                {
                    chkUser.Items[i].Selected = true;
                }
            }
        }

    }

    
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        monit.AddPIC(Convert.ToInt32(Request.QueryString["id"]), txtUserNames.Text, txtUsers.Text);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Monit_LogList.aspx");
    }
    protected void btn_search_Click1(object sender, EventArgs e)
    {
        loadUser();
    }
    protected void ddl_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartment();
    }
    protected void ddl_department_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_user.Text = string.Empty;
        loadUser();
    }
    protected void chkUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkUser.Items.Count == 0) return;

        string struser = txtUsers.Text.Trim().Replace(";;", ";");
        string strusername = txtUserNames.Text.Trim().Replace(";;", ";");

        foreach (ListItem item in chkUser.Items)
        {
            if (item.Selected)
            {
                string[] items = item.Text.Split(new string[] { "~" }, StringSplitOptions.None);
                string name = items[1];
                string email = items[2];
                if (email == "")
                {
                    item.Selected = false;
                    ltlAlert.Text = "alert('The user does not have email.Please contact hr!');";
                    return;
                }
                //struser = struser.Replace(";" + item.Value + ";", ";");
                //struser += item.Value + ";";
                struser = struser.Replace(";" + items[0] + ";", ";");
                struser += items[0] + ";";
                strusername = strusername.Replace(";" + name + ";", ";");
                strusername += name + ";";
                
            }
            else
            {
                struser = struser.Replace(";" + item.Value + ";", ";");
                //strusername = strusername.Replace(";" + items[1] + ";", ";"); ;
            }
        }

        txtUsers.Text = struser.Replace(";;", ";");
        txtUserNames.Text = strusername.Replace(";;", ";");
    }
}