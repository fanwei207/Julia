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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using RD_WorkFlow;
using System.Text;

namespace RD_WorkFlow
{
    public partial class rdw_step_mbr : BasePage
    {
        RDW_Template template = new RDW_Template();
        RD_Steps step = new RD_Steps();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int type = 0;
                int id = 0;
                int flag=Convert.ToInt32(Request.QueryString["flag"]);
                if (flag==1)
                {
                    if (Request.QueryString["mid"] != null) //Mstr Maintenance
                    {
                        id = int.Parse(Request.QueryString["mid"].ToString());
                        if (Request.QueryString["t"].ToString() == "pm") // Leader
                        {
                            type = 7;
                        }
                        //Product Manager
                        else if (Request.QueryString["t"].ToString() == "mgr")
                        {
                            type = 9;
                        }
                        else  // Viewer
                        {
                            type = 2;
                        }
                        btnBack.Visible = true;
                        btnClose.Visible = false;
                    }
                    else
                    {
                        Response.Redirect("/public/denied.htm");
                    }
                }

                
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
                DataSet dst;
                if (flag==1)
                {
                    dst = step.SelectReviewersAll(plant, int.Parse(Request.QueryString["mid"].ToString()), type);
                }
                else
                {
                    string t=Request.QueryString["t"];
                    string StepID = Request.QueryString["sid"];
                    if (t=="m")
                    {
                        dst = step.SelectStep_Mbr(StepID);
                    }
                    else
                    {
                        dst = step.SelectStep_Ptr(StepID);
                    }
                }

                if (dst.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dst.Tables[0].Rows.Count - 1; i++)
                    {

                        txtUsers.Text += ";" + dst.Tables[0].Rows[i][0].ToString();
                        txtUserNames.Text += ";" + dst.Tables[0].Rows[i][1].ToString();
                    }
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
            int type = 0;
            int id = 0;
            if (Request.QueryString["mid"] != null) //Mstr Maintenance
            {
                id = int.Parse(Request.QueryString["mid"].ToString());
                if (Request.QueryString["t"].ToString() == "m") // Leader
                {
                    type = 7;
                }
                else if (Request.QueryString["t"].ToString() == "mgr")
                {
                    type = 9;
                }
                else  // Viewer
                {
                    type = 2;
                }
                btnBack.Visible = true;
                btnClose.Visible = false;
            }
            else
            {
                Response.Redirect("/public/denied.htm");
            }

            if (chkUser.Items.Count > 0)
                chkUser.Items.Clear();
            int plantid = Convert.ToInt32(ddl_Company.SelectedValue);
            int departmentId = Convert.ToInt32(ddl_department.SelectedValue);
            string userName = txt_user.Text.Trim();
            string strUser = string.Empty;
            string strUserID = string.Empty;

            id = int.Parse(Request.QueryString["mid"].ToString());

            DataSet dst = step.SelectReviewers(plantid, id, type, departmentId, userName, chkbLeave.Checked);
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

            int id=-1;
            int type = 0;
            string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
            int flag = Convert.ToInt32(Request.QueryString["flag"]);
#region DetailList
            if (flag==1)
            {
                if (Request.QueryString["det"] != null) //Det_mbr Maintenance
                {
                    id = int.Parse(Request.QueryString["det"].ToString());
                    if (Request.QueryString["t"].ToString() == "pm")//Det_mbr & 
                        type = 1;
                    else//Det_Ptr Maintenance
                        type = 3;
                }
                else if (Request.QueryString["mid"] != null) //Mstr Maintenance
                {
                    id = int.Parse(Request.QueryString["mid"].ToString());
                    type = 2;
                    if (Request.QueryString["t"].ToString() == "pm")
                        type = 7;
                    else if (Request.QueryString["t"].ToString() == "mgr")
                    {
                        type = 9;
                    }
                }
                else //Step_mbr Maintenance
                {
                    id = int.Parse(Request.QueryString["sid"].ToString());
                    type = 0;

                }
            }
            
#endregion
            else if (flag==2)
            {
                if (Request.QueryString["t"] == "m")
                {
                    type = 8;
                }
                else
                    type = 0;

                id = int.Parse(Request.QueryString["sid"].ToString());
            }
            
           
            
            int uID = int.Parse(Session["uID"].ToString());
            string strReviewers = txtUsers.Text.Trim();
            string strReviewerNames = txtUserNames.Text.Trim();

            if (strReviewers.Replace(";", "").Length == 0)
            {
                if (!step.DeleteReviewers(id, type, uID))
                {
                    ltlAlert.Text = "alert('Database Operation Failed!');";
                    return;
                }
                else
                {
                    if (flag==1)
                    {
                        switch (type)
                        {
                            case 0: ltlAlert.Text = "window.close();window.opener.location='/RDW/rdw_step.aspx?fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 1: ltlAlert.Text = "window.close();window.opener.location='/RDW/rdw_step.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 2: ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + id.ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 3: ltlAlert.Text = "window.close();window.opener.location='/RDW/rdw_step.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 7: ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + id.ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 9: ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + id.ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                        }
                    }
                    
                    else if (flag==2)
                    {
                        ltlAlert.Text = "window.close();window.opener.location='/RDW/rdw_step.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
                    }
                    
                }
            }
            else
            {
                if (!step.UpdateReviewers(id, type, strReviewers, strReviewerNames, uID))
                {
                    ltlAlert.Text = "alert('Database Operation Failed!');";
                    return;
                }
                else
                {
                    if (flag==1)
                    {
                        switch (type)
                        {
                            case 0: ltlAlert.Text = "window.close();window.opener.location='/RDW/rdw_step.aspx?fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 1: ltlAlert.Text = "window.close();window.opener.location='/RDW/rdw_step.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 2: ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + id.ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 3: ltlAlert.Text = "window.close();window.opener.location='/RDW/rdw_step.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 7: ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + id.ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                            case 9: ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + id.ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';"; break;
                        }
                    }
                    
                    else if (flag==2)
                    {
                        ltlAlert.Text = "window.close();window.opener.location='/RDW/rdw_step.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
                    }
                    
                }
            }
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
                    string[] items = item.Text.Split(new string[] { "--" }, StringSplitOptions.None);
                    string name = items[1];
                    string email = items[2];
                    if (email == "")
                    {
                        item.Selected = false;
                        ltlAlert.Text = "alert('The user does not have email.Please contact hr!');";
                        return;
                    }
                    if (!CheckStringChinese(name))
                    {
                        struser = struser.Replace(";" + item.Value + ";", ";");
                        struser += item.Value + ";";
                    }
                    else
                    {
                        item.Selected = false;
                        ltlAlert.Text = "alert('The user does not have english name.Please contact hr!');";
                        return;
                    }

                }
                else
                {
                    struser = struser.Replace(";" + item.Value + ";", ";");
                }
            }

            txtUsers.Text = struser.Replace(";;", ";");
            txtUserNames.Text = strusername.Replace(";;", ";");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
            int flag = Convert.ToInt32(Request.QueryString["flag"]);
            if (flag==1)
            {
                ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
            }
            //ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
            else if (flag==2)
            {
                ltlAlert.Text = "window.location.href='/RDW/RDW_Step.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&@__pn=" + Request.QueryString["@__pn"] + "&@__pc=" + Request.QueryString["@__pc"] + "&@__sd=" + Request.QueryString["@__sd"] + "&@__st=" + Request.QueryString["@__st"] + "&@__sk=" + Request.QueryString["@__sk"] + "&@__pg=" + Request.QueryString["@__pg"] + "&rm=" + DateTime.Now.ToString() + "';";
            }
            
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
      
}
}