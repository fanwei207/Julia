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
using adamFuncs;
using System.Text;

public partial class MasterPage : BasePage
{
    adamClass adam = new adamClass();
    public String WECOLMELABLE = "";
    public String LOGOUTLABLE = "ע��";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool _notChinese = false;
            if (Convert.ToInt32(Session["PlantCode"]) == 98 || Convert.ToInt32(Session["PlantCode"]) == 99) 
            {
                _notChinese = true;
            }

            if (Convert.ToBoolean(Session["isInitPWD"]))
            {
                if (_notChinese)
                {
                    ltlAlert.Text = "alert('For first log in, please change your password��');window.location.href='/admin/editpassword.aspx';";
                }
                else
                {
                    ltlAlert.Text = "alert('�״ε�½�����޸����룡');window.location.href='/admin/editpassword.aspx';";
                }
            }
            else if (Convert.ToBoolean(Session["changePWDNextTime"]))
            {
                if (_notChinese)
                {
                    ltlAlert.Text = "alert('Password rule has been changed, please change your password��');window.location.href='/admin/editpassword.aspx';";
                }
                else
                {
                    ltlAlert.Text = "alert('��������Ѹ��ģ����޸����룡');window.location.href='/admin/editpassword.aspx';";
                }
            }
            else if (Convert.ToBoolean(Session["isPWDReset"]))
            {
                if (_notChinese)
                {
                    ltlAlert.Text = "alert('Your password has been reset, please change your password��');window.location.href='/admin/editpassword.aspx';";
                }
                else
                {
                    ltlAlert.Text = "alert('���������ã����޸����룡');window.location.href='/admin/editpassword.aspx';";
                }
            }
            else if (!CheckPwdValidity())
            {
                Session["isPwdValid"] = false;

                if (_notChinese)
                {
                    ltlAlert.Text = "alert('Your password has expired, please change your password��');window.location.href='/admin/editpassword.aspx';";
                }
                else
                {
                    ltlAlert.Text = "alert('�����ѹ��ڣ����޸����룡');window.location.href='/admin/editpassword.aspx';";
                }
            }

            if (_notChinese)
            {
                this.WECOLMELABLE = "Welcome��&nbsp;&nbsp;" + Session["uName"].ToString();
                this.LOGOUTLABLE = "Logout";
            }
            else
            {
                this.WECOLMELABLE = "��ӭ����&nbsp;&nbsp;" + Session["uName"].ToString() + "&nbsp;" + String.Format("������{0:yyyy.MM.dd dddd}", DateTime.Now);
                this.LOGOUTLABLE = "ע��";
            }
        }
    }
    /// <summary>
    /// ��֤�������
    /// </summary>
    /// <returns></returns>
    public bool CheckPwdValidity()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@uID", Session["uID"]);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_hr_checkUserPwdValidity", param);
            return Convert.ToBoolean(param[1].Value);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// ��ȡ��Ȩ�Ĳ˵�
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="moduleID"></param>
    /// <returns></returns>
    public DataTable GetAuthorizedMenus(string userID, string module)
    {
        string strSql = "sp_admin_selectAuthorizedMenuList";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@userID", userID);
        param[1] = new SqlParameter("@module", module);

        try
        {
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// ��ȡ�˵�
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public string GetMenus(string userID, string moduleID)
    {
        string _menus = "";

        DataTable table = GetAuthorizedMenus(userID, moduleID);

        if (table != null || table.Rows.Count > 0)
        {
            _menus = BuildMenus(table, "0");
        }
        else
        {
            _menus = "�˵���ȡʧ��<br />����ϵ����Ա";
        }

        return _menus;
    }
    /// <summary>
    /// �ݹ�Menu��
    /// </summary>
    /// <param name="table"></param>
    /// <param name="parent"></param>
    /// <param name="depth">���</param>
    /// <returns></returns>
    public string BuildMenus(DataTable table, string parent)
    {
        StringBuilder _builder = new StringBuilder("");

        DataRow[] rowList = table.Select("parentID = '" + parent + "'", "sortOrder Asc");

        if (rowList.Length > 0)
        {
            _builder.Append("<ul>");

            foreach (DataRow row in rowList)
            {
                if (row["id"].ToString() == "170000")
                {
                    _builder.Append("<li><a id=\"" + row["id"].ToString() + "\" doc=\" " + row["helpDoc"].ToString() + "\" href=\"#" + row["url"].ToString() + "\">" + row["Name"].ToString() + "</a>");
                }
                else
                {
                    _builder.Append("<li><a id=\"" + row["id"].ToString() + "\" doc=\" " + row["helpDoc"].ToString() + "\" href=\"#" + row["url"].ToString() + "\">" + row["description"].ToString() + row["Name"].ToString() + "</a>");
                }

                _builder.Append(BuildMenus(table, row["id"].ToString()));

                _builder.Append("</li>");
            }

            _builder.Append("</ul>");
        }

        return _builder.ToString();
    }
    protected void linkSignOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");
    }

    /// <summary>
    /// ��ȡ��˾����
    /// </summary>
    /// <param name="plantCode"></param>
    protected string GetPlants(string plantCode)
    {
        try
        {
            string strSQL = "SELECT description From Plants Where plantID = " + plantCode;

            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, strSQL).ToString();
        }
        catch
        {
            return string.Empty;
        }
    }
}
