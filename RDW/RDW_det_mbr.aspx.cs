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
    public partial class RDW_det_mbr : BasePage
    {
        RD_Steps step = new RD_Steps();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int type = 0;
                int id = int.Parse(Request.QueryString["did"].ToString());

                if (Request.QueryString["mid"] != null && Request.QueryString["did"] != null)
                {
                    if (Request.QueryString["t"].ToString() == "m")//Det_mbr
                    {
                        type = 1;
                    }
                    else //Det_Ptr Maintenance
                    {
                        // "170005", USA
                        type = 3;
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

                DataSet dst = step.SelectReviewersAll(plant, int.Parse(Request.QueryString["did"].ToString()), type);

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
            ddl_department.Items.Insert(0, new ListItem("--Select A Department--", "0"));
        }

        protected void loadUser()
        {
            int type = 0;
            int id = int.Parse(Request.QueryString["did"].ToString());

            if (Request.QueryString["mid"] != null && Request.QueryString["did"] != null)
            {
                if (Request.QueryString["t"].ToString() == "m")//Det_mbr
                {
                    type = 1;
                }
                else //Det_Ptr Maintenance
                {
                    // "170005", USA
                    type = 3;
                }
            }

            if (chkUser.Items.Count > 0)
                chkUser.Items.Clear();

            int plantid = Convert.ToInt32(ddl_Company.SelectedValue);
            int departmentId = Convert.ToInt32(ddl_department.SelectedValue);
            string userName = txt_user.Text.Trim();
            string strUser = string.Empty;
            string strUserID = string.Empty;
            id = int.Parse(Request.QueryString["did"].ToString());


            DataSet dst = step.SelectReviewers(plantid, id, type, departmentId, userName,chkbLeave.Checked);
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

            int id = int.Parse(Request.QueryString["did"].ToString());
            int type = 0;
            string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);

            if (Request.QueryString["t"] == "m") //Det_mbr Maintenance
            {
                type = 1;
            }
            else //Det_Ptr Maintenance
            {
                type = 3;
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
                    ltlAlert.Text = "window.close();window.opener.location='/RDW/RDW_DetailList.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
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
                    ltlAlert.Text = "window.close();window.opener.location='/RDW/RDW_DetailList.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
                }
            }
        }

        protected void chkUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkUser.Items.Count == 0) return;

            string struser = txtUsers.Text.Trim().Replace(";;", ";");
            string strusername = txtUserNames.Text.Trim().Replace(";;", ";");
            char[] chartemp = { '~' };

            foreach (ListItem item in chkUser.Items)
            {
                string[] strTemp = item.Text.Trim().Split(chartemp);
                if (item.Selected)
                {
                    string[] items = item.Text.Split(new string[] { "--" }, StringSplitOptions.None);
                    string name = items[1];
                    string email = items[2].Trim();
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


                    strusername = strusername.Replace(";" + strTemp[0] + ";", ";");
                    strusername += strTemp[0] + ";";
                }
                else
                {
                    struser = struser.Replace(";" + item.Value + ";", ";");
                    strusername = strusername.Replace(";" + strTemp[0] + ";", ";");
                }
            }

            txtUsers.Text = struser.Replace(";;", ";");
            txtUserNames.Text = strusername.Replace(";;", ";");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            string strQuy = Request.QueryString["fr"] == null ? "" : Convert.ToString(Request.QueryString["fr"]);
            ltlAlert.Text = "window.location.href='/RDW/RDW_DetailList.aspx?mid=" + Request.QueryString["mid"].ToString() + "&fr=" + strQuy + "&rm=" + DateTime.Now.ToString() + "';";
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