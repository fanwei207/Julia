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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
using CommClass;

public partial class Login : System.Web.UI.Page
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            String redir = Request.Url.ToString();

            BindPlants();

            #region
            if (Request.UserHostAddress.Contains("10.3.0.") || Request.UserHostAddress.Contains("10.3.1.") || Request.UserHostAddress.Contains("10.3.2.") || Request.UserHostAddress.Contains("10.3.3.") || Request.UserHostAddress.Contains("10.3.4.") || Request.UserHostAddress.Contains("10.3.5.") || Request.UserHostAddress.Contains("10.3.97.") || Request.UserHostAddress.Contains("10.3.98.") || Request.UserHostAddress.Contains("10.3.99."))  
            {
                dropPlant.SelectedValue = "1";
            }
            else if (Request.UserHostAddress.Contains("10.3.10.") || Request.UserHostAddress.Contains("10.3.20."))
            {
                dropPlant.SelectedValue = "2";
            }
            else if (Request.UserHostAddress.Contains("10.3.30.") || Request.UserHostAddress.Contains("10.3.40."))
            {
                dropPlant.SelectedValue = "5";
            }
            else if (Request.UserHostAddress.Contains("10.3.50."))
            {
                dropPlant.SelectedValue = "8";
            }
            else if (Request.UserHostAddress.Contains("10.3.70."))
            {
                dropPlant.SelectedValue = "10";
            }
            else if (Request.UserHostAddress.Contains("10.1."))
            {
                dropPlant.SelectedValue = "99";
            }
            else if (Request.UserHostAddress.Contains("10.3.60."))
            {
                dropPlant.SelectedValue = "11";
            }
            else
            {
                dropPlant.SelectedValue = "0";
            }
            #endregion

            //自动登陆
            if (Request.QueryString["c"] != null)
            {
                try
                {
                    dropPlant.SelectedIndex = -1;
                    dropPlant.Items.FindByValue(Server.HtmlDecode(Request.QueryString["c"].ToString())).Selected = true;
                }
                catch
                {
                    ;
                }

                TextBoxUserID.Text = Server.HtmlDecode(Request.QueryString["u"].ToString());
                TextBoxPWD.Text = Server.HtmlDecode(Request.QueryString["p"].ToString());

                this.btnLogin_Click(this, new EventArgs());
            }

            String strLogin;
            strLogin = Request.Params["login"];
            if (strLogin != null)
            {
                if (strLogin.CompareTo("timeout") == 0)
                {
                    Session.Contents.RemoveAll();
                    Session.Abandon();
                    ltlAlert.Text = "alert('Session expired.已超时,请再登入.')";
                }
                else if (strLogin.CompareTo("logout") == 0)
                {
                    Session.Contents.RemoveAll();
                    Session.Abandon();
                }
                else if (strLogin.CompareTo("moretwo") == 0)
                {
                    Session.Contents.RemoveAll();
                    Session.Abandon();
                    ltlAlert.Text = "alert('Invalidate for repeating login.')";
                }
                else if (strLogin.CompareTo("cookie") == 0)
                {
                    ltlAlert.Text = "alert('Writing cookie error.')";
                }
                else if (strLogin.CompareTo("readCookie error") == 0)
                {
                    ltlAlert.Text = "alert('Reading cookie error.')";
                }
                else if (strLogin.CompareTo("expire.") == 0)
                {
                    ltlAlert.Text = "alert('Session expired. 已过期,请再登入.')";
                }
                else
                {
                    ltlAlert.Text = "alert('Try again. 请再登入. ')";
                }
            }

        }
    }

    /// <summary>
    /// 绑定公司
    /// </summary>
    protected void BindPlants()
    {
        String strSQL = string.Empty;

        try
        {
            if (Request.Params["login"] != null && Request.Params["login"].ToLower() == "admin")
            {
                strSQL = "SELECT plantID, description From Plants order by plantID";
            }
            else
            {
                strSQL = "SELECT plantID, description From Plants where isAdmin=0 order by plantID";
            }

            DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSQL);

            dropPlant.DataSource = ds;
            dropPlant.DataBind();

            dropPlant.Items.Insert(0, new ListItem("--Choose Your Company--", "0"));

            ds.Reset();
        }
         
        catch
        {
            ;
        }
    }

    protected void SuccessLogs()
    {
        try
        {
            HttpBrowserCapabilities bc = Request.Browser;
            String strSQL = "";
            strSQL = "INSERT INTO successlogs (userID, agent, addr, dtime, browser, version) VALUES(" + Convert.ToString(Session["uID"]) + ",'" + Request.UserHostName + "','" + Request.UserHostAddress + "',getdate(), '" + bc.Browser + "', '" + bc.Version + "')";
            SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.Text, strSQL);
        }
        catch
        { ;}
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (dropPlant.SelectedIndex == 0)
        {
            this.ltlAlert.Text = "alert('At First, You Must Select Your Company！\\n(你必须先选择一个公司！)');";
            return;
        }

        Session["PlantCode"] = dropPlant.SelectedValue;
        if (Convert.ToInt16(dropPlant.SelectedValue) > 100)
        {
            Session["PlantCode"] = Convert.ToString(Convert.ToInt16(Session["PlantCode"]) - 100);
            Session["temp"] = 1;
        }
        else
        {
            Session["temp"] = 0;
        }


        String us = TextBoxUserID.Text.Trim();
        String pwd = chk.encryptPWD(TextBoxPWD.Text.Trim(), "www.fengxinsoftware.com");

        //Access User Information
        DataSet ds;

        String ipAddr = Request.UserHostAddress;

        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@user", us);
        parm[1] = new SqlParameter("@pwd", pwd);
        parm[2] = new SqlParameter("@plantCode", Session["PlantCode"]);

        ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_adm_selectLoginInfo", parm);

        if (ds != null && ds.Tables[0].Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('USER or PWD is incorrect. 用户名或密码不对(1).')";
        }
        else
        {
            if (!Convert.ToBoolean(ds.Tables[0].Rows[0]["isActive"]) || (Convert.ToString(ds.Tables[0].Rows[0]["leaveDate"]) != string.Empty && !Convert.ToBoolean(ds.Tables[0].Rows[0]["isok"])))
            {
                ltlAlert.Text = "alert('User denied. 用户失效或已离职.');";
                return;
            }
            else if ((ipAddr.Substring(0, 3) != "10." && ipAddr != "::1" && ipAddr != "127.0.0.1" && ipAddr != "192.168.171.9" && ipAddr.Substring(0, 8) != "172.16.0") && !Convert.ToBoolean(ds.Tables[0].Rows[0]["accInternet"]))
            {
                ltlAlert.Text = "alert('User denied. 没有从外网访问的权限！')";
                return;
            }

            Session["uID"] = ds.Tables[0].Rows[0]["userID"].ToString();
            Session["loginName"] = ds.Tables[0].Rows[0]["loginName"].ToString();
            Session["uName"] = ds.Tables[0].Rows[0]["userName"].ToString();
            Session["eName"] = ds.Tables[0].Rows[0]["englishName"].ToString();
            Session["uRole"] = ds.Tables[0].Rows[0]["roleID"].ToString();
            Session["roleName"] = ds.Tables[0].Rows[0]["roleName"].ToString();
            Session["uGroupID"] = ds.Tables[0].Rows[0]["departmentID"].ToString();
            Session["orgID"] = ds.Tables[0].Rows[0]["organizationID"].ToString();
            Session["orgName"] = ds.Tables[0].Rows[0]["gname"].ToString();
            Session["homeID"] = ds.Tables[0].Rows[0]["homeID"].ToString();
            Session["deptID"] = ds.Tables[0].Rows[0]["departmentID"].ToString();
            Session["email"] = ds.Tables[0].Rows[0]["email"].ToString();
            Session["conceal"] = 0;
            Session["Coefficient"] = ds.Tables[0].Rows[0]["Coefficient"].ToString();

            //是否显示菜单
            Session["showMenu"] = ds.Tables[0].Rows[0]["showMenu"];
            //是否显示教程
            Session["showTutorials"] = ds.Tables[0].Rows[0]["showTutorials"];

            //密码控制
            Session["isInitPWD"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["isInitPWD"]);
            Session["changePWDNextTime"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["changePWDNextTime"]);
            Session["isPWDReset"] = Convert.ToBoolean(ds.Tables[0].Rows[0]["isPWDReset"]);

            //Destop报表指向的存储过程
            Session["ReportProc"] = ds.Tables[0].Rows[0]["ReportProc"];

            //写入Log
            SuccessLogs();

            System.Web.Security.FormsAuthentication.SetAuthCookie(TextBoxUserID.Text, true);

            HttpCookie cookie = new HttpCookie("KeyData");

            cookie["PlantCode"] = Session["PlantCode"].ToString();
            cookie["uID"] = Session["uID"].ToString();
            cookie["uName"] = HttpUtility.UrlEncode(Session["uName"].ToString());
            cookie["roleName"] = HttpUtility.UrlEncode(Session["roleName"].ToString());
            cookie["uRole"] = Session["uRole"].ToString();
            cookie["orgID"] = Session["orgID"].ToString();
            cookie["deptID"] = Session["deptID"].ToString();
            cookie["showTutorials"] = Session["showTutorials"].ToString();
            cookie["ReportProc"] = Session["ReportProc"].ToString();

            Response.Cookies.Add(cookie);

            Response.Redirect("MasterPage.aspx?rt=" + DateTime.Now.ToString());
        }

        ds.Reset();
    }
}
