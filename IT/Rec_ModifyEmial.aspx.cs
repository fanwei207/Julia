using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class IT_Rec_ModifyEmial : System.Web.UI.Page
{
    private Rec_Emial recEml = new Rec_Emial();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
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

            int plant = int.Parse(ddl_Company.SelectedValue);
            //DataSet dst = recEml.SelectOriginalEml(Convert.ToInt32(Request.QueryString["id"]),Request.QueryString["type"]);

            //if (dst.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
            //    {

            //        txtUsers.Text += ";" + dst.Tables[0].Rows[i][0].ToString();
            //        txtUserEmls.Text += ";" + dst.Tables[0].Rows[i][1].ToString();
            //    }
            //}
            txtUsers.Text += ";";
            txtUserEmls.Text += ";";
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
        ddl_department.Items.Insert(0, new ListItem("--Select A Department--", "0"));
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

        DataSet dst = new DataSet();
        dst = recEml.SelectEmployees(plantid, departmentId, chkbLeave.Checked,userName);
        if (dst.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
            {
                ListItem item = new ListItem(dst.Tables[0].Rows[i].ItemArray[2].ToString().Trim() + "--" + dst.Tables[0].Rows[i].ItemArray[1].ToString().Trim() + "--" + dst.Tables[0].Rows[i].ItemArray[3].ToString().Trim(), dst.Tables[0].Rows[i].ItemArray[0].ToString().Trim());
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
        if (chkUser.Items.Count < 1)
            return;
        string strUsers = txtUsers.Text.Trim();
        string strUserEmls = txtUserEmls.Text.Trim();

        if (strUserEmls.Replace(";", "").Length == 0)
        {
            if (!recEml.DeleteAllEmails(Request.QueryString["id"], Request.QueryString["type"],Convert.ToInt32(Session["uID"])))
            {
                ltlAlert.Text = "alert('Database Operation Failed!');";
                return;
            }
            else
            {
                ltlAlert.Text = "window.close();window.opener.location='Rec_RecipientConfig.aspx?reportid=" + Request.QueryString["id"] + "';";
                //Response.Redirect("Rec_RecipientConfig.aspx");
            }
        }
        else
        {
            if (!recEml.UpdateEmialbyType(Request.QueryString["id"], Request.QueryString["type"], strUserEmls, Convert.ToInt32(Session["uID"])))
            {
                ltlAlert.Text = "alert('Database Operation Failed!');";
                return;
            }
            else
            {
                //Response.Redirect("Rec_RecipientConfig.aspx");
                ltlAlert.Text = "window.close();window.opener.location='Rec_RecipientConfig.aspx?reportid=" + Request.QueryString["id"] + "';";
            }
        }
    }
    protected void chkUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkUser.Items.Count == 0) return;
        string struser = txtUsers.Text.Trim().Replace(";;", ";");
        string struserEml = txtUserEmls.Text.Trim().Replace(";;", ";");
        char[] chartemp = { '~' };

        foreach (ListItem item in chkUser.Items)
        {
            string[] strTemp = item.Text.Trim().Split(chartemp);
            if (item.Selected)
            {
                string[] items = item.Text.Split(new string[] { "--" }, StringSplitOptions.None);
                string name = items[1];
                string email = items[2].Trim();
                if (email == "" || email=="N/A")
                {
                    item.Selected = false;
                    ltlAlert.Text = "alert('The user does not have email.Please contact hr!');";
                    return;
                }
                //if (!recEml.CheckStringChinese(name))
                //{
                //    struser = struser.Replace(";" + item.Value + ";", ";");
                //    struser += item.Value + ";";
                //}
                //else
                //{
                //    item.Selected = false;
                //    ltlAlert.Text = "alert('The user does not have english name.Please contact hr!');";
                //    return;
                //}
                //struserEml = struserEml.Replace(";" + strTemp[0] + ";", ";");
                //struserEml += strTemp[0]+";";

                struserEml = struserEml.Replace(";" + email + ";", ";");
                struserEml += email + ";";
            }
            else
            {
                struser = struser.Replace(";" + item.Value + ";", ";");
                struserEml = struserEml.Replace(";" + strTemp[0] + ";", ";");
            }
        }

        txtUsers.Text = struser.Replace(";;", ";");
        txtUserEmls.Text = struserEml.Replace(";;", ";");
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
    protected void btn_search_Click(object sender, EventArgs e)
    {
        loadUser();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.close();";
    }
}