using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
using CommClass;
using System.Data;

public partial class ForgetPassword_Default : System.Web.UI.Page
{

    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            labTips.Text = "密码找回规则：<br />&nbsp;&nbsp;&nbsp;&nbsp;正确填写左侧全部信息之后，系统会发送邮件至你的邮箱；进入你的邮箱，点击里面的链接，重置你的密码。";

            labTips.Text += "<br />Password Recovery Rules: <br />&nbsp;&nbsp;&nbsp;&nbsp; Enter all of the infomation on the left;our system will send you a email with a link;click this link to reset your password.";

            BindPlants();
        }
        string aa = dropPlant.SelectedValue;
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

    protected void btnSumit_Click(object sender, EventArgs e)
    {
        string errg = "";
        if (dropPlant.SelectedIndex == 0)
        {
            errg = "\\n At First, You Must Select Your Company！\\n(你必须先选择一个公司！);";
        }
        if (txt_usercode.Text == string.Empty || txt_usercode.Text == "UserName/UserNo")
        {
            errg += "\\n Account can not be empty！\\n(账号不可为空！)";
        }
        if (txt_UserName.Text == string.Empty)
        {
            errg += "\\n Name can not be empty！\\n(姓名不可为空！)";
        }
        if (txt_ic.Text == string.Empty)
        {
            errg += "\\n IC can not be empty！\\n(身份证不可为空！)";
        }
        if (errg != string.Empty)
        {
            this.ltlAlert.Text = "alert('" + errg + "');";
            return;
        }
        Session["PlantCode"] = dropPlant.SelectedValue;
        if (Convert.ToInt16(Session["PlantCode"]) > 100)
        {
            Session["PlantCode"] = Convert.ToString(Convert.ToInt16(Session["PlantCode"]) - 100);
            Session["temp"] = 1;
        }
        else
        {
            Session["temp"] = 0;
        }
        String p_userno = txt_usercode.Text.Trim();
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@user", p_userno);
        parm[1] = new SqlParameter("@pwd", string.Empty);
        parm[2] = new SqlParameter("@plantCode", Session["PlantCode"]);
        String ipAddr = Request.UserHostAddress;
        DataSet ds;
        try
        {
            ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_adm_selectLoginInfo", parm);
            if (ds == null && ds.Tables[0].Rows.Count <= 0)
            {
                ltlAlert.Text = "alert('Account does not exist. \\n账号不存在.')";
                return;
            }
            else
            {
                if (!Convert.ToBoolean(ds.Tables[0].Rows[0]["isActive"]) || (Convert.ToString(ds.Tables[0].Rows[0]["leaveDate"]) != string.Empty && !Convert.ToBoolean(ds.Tables[0].Rows[0]["isok"])))
                {
                    ltlAlert.Text = "alert('User denied. 用户失效或已离职.');";
                    return;
                }
                else if (Convert.ToString(ds.Tables[0].Rows[0]["email"]) == string.Empty)
                {
                    ltlAlert.Text = "alert('Mail  does not exist. \\n 用户邮箱不存在.');";
                    return;
                }
                else if ((ipAddr.Substring(0, 3) != "10." && ipAddr != "::1" && ipAddr != "127.0.0.1" && ipAddr != "192.168.171.9") && !Convert.ToBoolean(ds.Tables[0].Rows[0]["accInternet"]))
                {
                    ltlAlert.Text = "alert('User denied! 没有从外网访问的权限！')";
                    return;
                }
                if (this.txt_usercode.Text.ToUpper() != Convert.ToString(ds.Tables[0].Rows[0]["loginName"].ToString().ToUpper()))
                {
                    errg += "\\n Account  does not exist! \\n(Account does not exist \\n 账号不存在!)";
                }
                if (txt_UserName.Text != Convert.ToString(ds.Tables[0].Rows[0]["username"]))
                {
                    errg += "\\n Name is not correct! \\n (姓名不正确!)";
                }
                if (this.txt_ic.Text != Convert.ToString(ds.Tables[0].Rows[0]["ic"]))
                {
                    errg += "\\n IC is not correct! \\n (身份证不正确!)";
                }
                if (errg != string.Empty)
                {
                    this.ltlAlert.Text = "alert('" + errg + "');";
                    return;
                }
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Get user information failed \n 获取用户信息失败！');Form1.usercode.focus();";
            return;
        }

        string strSql = " insert into forgetpassword (loginname,PlantCode) values ('" + p_userno + "','" + Session["PlantCode"] + "')";

        try
        {
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, strSql);
        }
        catch
        {
            ltlAlert.Text = "alert('Submission information verification failed \n 提交信息验证失败！');Form1.usercode.focus();";
            return;
        }
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_admin_mail_ForgetPasswordURL", parm);
        ltlAlert.Text = "alert('Success! \\n Please go to your mailbox:" + Convert.ToString(ds.Tables[0].Rows[0]["email"]) + "\t" + "confirm！ \\n 提交信息验证通过;\\n请到邮箱" + Convert.ToString(ds.Tables[0].Rows[0]["email"]) + "中确认！');";
        //Response.Redirect("../MasterPage.aspx?rt=" & DateTime.Now.ToFileTime());
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        this.txt_usercode.Text = "";
        this.txt_UserName.Text = "";
        this.txt_ic.Text = "";
    }

    protected void txt_usercode_changed(object sender, EventArgs e)
    {
        adamClass chk = new adamClass();
        if (dropPlant.SelectedIndex == 0)
        {
            this.ltlAlert.Text = "alert('At First, You Must Select Your Company！\\n(你必须先选择一个公司！)');";
            return;
        }
        Session["PlantCode"] = dropPlant.SelectedValue;

        if (Convert.ToInt16(Session["PlantCode"]) > 100)
        {
            Session["PlantCode"] = Convert.ToString(Convert.ToInt16(Session["PlantCode"]) - 100);
            Session["temp"] = 1;
        }

        else
        {
            Session["temp"] = 0;
        }
        if (txt_usercode.Text == string.Empty || txt_usercode.Text == "UserName/UserNo")
        {
            this.ltlAlert.Text = "alert('Account can not be empty！\\n(账号不可为空！)');";
            return;
        }

        #region delete

        /*
        String p_userno = txt_usercode.Text.Trim();
        SqlParameter[] parm = new SqlParameter[3];
        parm[0] = new SqlParameter("@user", p_userno);
        parm[1] = new SqlParameter("@pwd", string.Empty);
        parm[2] = new SqlParameter("@plantCode", Session["PlantCode"]);

        String ipAddr = Request.UserHostAddress;
        DataSet ds;
        try
        {
            ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_adm_selectLoginInfo", parm);
            if (ds != null && ds.Tables[0].Rows.Count <= 0)
            {
                ltlAlert.Text = "alert('Account does not exist. 账号不存在.')";
                return;
            }
            else
            {
                if (!Convert.ToBoolean(ds.Tables[0].Rows[0]["isActive"]) || (Convert.ToString(ds.Tables[0].Rows[0]["leaveDate"]) != string.Empty && !Convert.ToBoolean(ds.Tables[0].Rows[0]["isok"])))
                {
                    ltlAlert.Text = "alert('User denied. 用户失效或已离职.');";
                    return;
                }
                else if (Convert.ToString(ds.Tables[0].Rows[0]["email"]) == string.Empty)
                {
                    ltlAlert.Text = "alert('Mail  does not exist. 用户邮箱不存在.');";
                    return;
                }
                else if ((ipAddr.Substring(0, 3) != "10." && ipAddr != "::1" && ipAddr != "127.0.0.1" && ipAddr != "192.168.171.9") && !Convert.ToBoolean(ds.Tables[0].Rows[0]["accInternet"]))
                {
                    ltlAlert.Text = "alert('User denied. 没有从外网访问的权限！')";
                    return;
                }
                //hdnusercode.Value = ds.Tables[0].Rows[0]["loginName"].ToString();
                //hdnuserIC.Value = ds.Tables[0].Rows[0]["ic"].ToString();
            }
        }
        catch
        {
            ltlAlert.Text = "alert('获取用户信息失败！');Form1.usercode.focus();";
            return;
        }
        */

        #endregion
    }
}