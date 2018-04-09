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
using System.Text.RegularExpressions;


public partial class changePassWord : System.Web.UI.Page
{

    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtLoginName.Text = "";
            txtPlantCode.Text = "";
            string newid_no = Request.Params["newid_no"];
            string strSql2 = " select top 1 tmp.loginname,us.userPWD,tmp.plantcode ";
            strSql2 += " from forgetpassword  tmp inner join Users us on tmp.loginname=us.loginname and tmp.plantcode=us.plantcode ";
            strSql2 += "where tmp.flag is null   and tmp.newid_no='" + newid_no + "' and (GETDATE()<CONVERT(varchar(100), createtime+0.04, 120))";

            DataSet ds2;
            try
            {
                ds2 = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, strSql2);
                if (ds2 != null && ds2.Tables[0].Rows.Count <= 0)
                {
                    ltlAlert.Text = "alert('Password format error;Expired,please application again！\\n 超过了有效期;请重新申请！')";
                    return;
                }
                else
                {
                    txtLoginName.Text = (Convert.ToString(ds2.Tables[0].Rows[0]["loginname"]));
                    txtPlantCode.Text = (Convert.ToString(ds2.Tables[0].Rows[0]["plantcode"]));
                }
            }
            catch
            {
                ltlAlert.Text = "alert('Submission information verification failed! \\n 提交信息验证失败！');Form1.usercode.focus();";
                return;
            }

            if (txtPlantCode.Text == "98" || txtPlantCode.Text == "99")
            {
                labTips.Text = "Password Rulse:<br />- Must contain numbers, letters, and special characters<br />- The password is valid for: 3 months<br />- The new password cannot be the same as the previous password";
            }
            else
            {
                labTips.Text = "密码规则如下： <br/> - 密码必须包含数字、大写字母、特殊字符 <br/> - 密码有效期为：3个月 <br/> - 密码最短：6位，最长：20位 <br/> - 密码重复次数：1 ，即新密码不能与前1个密码相同";
            }
        } 
    }

    private bool checkPasswordRules()
    {
        DataSet ds;
        SqlParameter[] param = new SqlParameter[0];
        //param[0] = new SqlParameter("@userID", dropUsers.SelectedValue);
        ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_hr_selectPasswordRules");
        if (ds != null && ds.Tables[0].Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('Get PasswordRules Err! \\n 获取密码规则失败!.')";
            return false;
        }
        else
        {
            string minLen = (Convert.ToString(ds.Tables[1].Rows[0]["minLen"]));
            string maxLen = (Convert.ToString(ds.Tables[1].Rows[0]["maxLen"]));
            string hasNumber = (Convert.ToString(ds.Tables[1].Rows[0]["hasNumber"]));
            string hasLowLetter = (Convert.ToString(ds.Tables[1].Rows[0]["hasLowLetter"]));
            string hasUpLetter = (Convert.ToString(ds.Tables[1].Rows[0]["hasUpLetter"]));
            string hasSpecial = (Convert.ToString(ds.Tables[1].Rows[0]["hasSpecial"]));
            string patternDesc = (Convert.ToString(ds.Tables[1].Rows[0]["structureDesc"]));
            this.labTips.Text = (Convert.ToString(ds.Tables[1].Rows[0]["structureDesc"]));
            string numberRegex = (Convert.ToString(ds.Tables[1].Rows[0]["numberRegex"]));
            string lowLetterRegex = (Convert.ToString(ds.Tables[1].Rows[0]["lowLetterRegex"]));
            string upLetterRegex = (Convert.ToString(ds.Tables[1].Rows[0]["upLetterRegex"]));
            string specialRegex = (Convert.ToString(ds.Tables[1].Rows[0]["specialRegex"]));
            if (hasNumber == string.Empty || numberRegex == string.Empty || hasLowLetter == string.Empty || lowLetterRegex == string.Empty || hasUpLetter == string.Empty || upLetterRegex == string.Empty || hasSpecial == string.Empty || specialRegex == string.Empty)
            {
                return true;
            }
            if (Convert.ToBoolean(hasNumber))
            {
                if (!Regex.IsMatch(txtConfirmPassword.Text.Trim(), numberRegex))
                {
                    return false;
                }
            }
            if (Convert.ToBoolean(hasLowLetter))
            {
                if (!Regex.IsMatch(txtConfirmPassword.Text.Trim(), lowLetterRegex))
                {
                    return false;
                }
            }
            if (Convert.ToBoolean(hasUpLetter))
            {
                if (!Regex.IsMatch(txtConfirmPassword.Text.Trim(), upLetterRegex))
                {
                    return false;
                }
            }
            if (Convert.ToBoolean(hasSpecial))
            {
                if (!Regex.IsMatch(txtConfirmPassword.Text.Trim(), specialRegex))
                {
                    return false;
                }
            }
        }
        return true;
    }

    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        txtNewPassword.Text = "";
        txtConfirmPassword.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtNewPassword.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('New Password could not be empty！\\n 新密码不可为空！');";
            return;
        }
        if (txtConfirmPassword.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Confirm Password could not be empty！\\n 确认密码不可为空！');";
            return;
        }
        if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
        {
            ltlAlert.Text = "alert('different input password！\\n 两次输入密码不同！');";
            return;
        }
        if (!checkPasswordRules())
        {
            ltlAlert.Text = "alert('New Password format error！\\n 新密码规则不正确！\\n');";
            return;
        }

        //更新用户密码并添加密码使用记录
        if (txtLoginName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('Get employee information error！\\n (获取用户信息失败！)')";
            return;
        }
        if (txtPlantCode.Text == string.Empty)
        {
            ltlAlert.Text = "alert('Get plantcode error！\\n (获取公司归属失败！)')";
            return;
        }
        string strSql = " select top 1 tmp.loginname,us.userPWD,tmp.plantcode ";
        strSql += " from forgetpassword  tmp inner join Users us on tmp.loginname=us.loginname and tmp.plantcode=us.plantcode ";
        strSql += "where tmp.flag is null and tmp.loginname='" + txtLoginName.Text + "' and tmp.plantcode='" + txtPlantCode.Text + "' and (GETDATE()<CONVERT(varchar(100), createtime+0.04, 120))";
        DataSet ds1;
        try
        {
            ds1 = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, strSql);
            if (ds1 != null && ds1.Tables[0].Rows.Count <= 0)
            {
                ltlAlert.Text = "alert('Password format error;Expired,please reapply！\\n 超过了有效期;请重新申请！')";
                return;

            }
            else
            {
                try
                {
                    string aa = chk.encryptPWD(txtConfirmPassword.Text.Trim());
                    string strSql1 = " update tcpc0.dbo.users set userPWD='" + chk.encryptPWD(txtConfirmPassword.Text.Trim()) + "'  where roleID<>1 and userno='" + txtLoginName.Text + "' and PlantCode='" + txtPlantCode.Text + "'";
                    SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, strSql1);

                    string strSql5 = " update tcpc0.dbo.forgetpassword set flag='1'  where loginname='" + txtLoginName.Text + "' and PlantCode='" + txtPlantCode.Text + "'";
                    SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.Text, strSql5);
                    ltlAlert.Text = "alert('Password reset succed! \\n 密码重置成功！');Form1.usercode.focus();";
                    return;
                }
                catch
                {
                    ltlAlert.Text = "alert('Password reset failed! \\n 有非法字符，密码重置失败！');Form1.usercode.focus();";
                    return;
                }
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Submission information verification failed! \\n 提交信息验证失败！');Form1.usercode.focus();";
            return;
        }
    }
}