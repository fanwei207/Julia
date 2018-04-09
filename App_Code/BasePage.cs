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
using System.Security.Principal;
using System.Collections.Generic;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// 定义一个Excel导出接口
/// </summary>
public interface IExcelExport
{
    string Data
    {
        get;
    }

    string Head
    {
        get;
    }
}

 public enum ExcelVersion 
{ 
    Excel2003,
    Excel2007
}

/// <summary>
/// 控件参数类。主要用于页面自动恢复时
/// </summary>
public class ControlParam
{
    private string _id;
    /// <summary>
    /// 控件的ID
    /// </summary>
    public string ID
    {
        get 
        {
            return this._id;
        }
        set
        {
            this._id = value;
        }
    }

    private string _value;
    /// <summary>
    /// 控件的值
    /// </summary>
    public string Value
    {
        get
        {
            return this._value;
        }
        set
        {
            this._value = value;
        }
    }

    public ControlParam(string id, string value)
    {
        this._id = id;
        this._value = value;
    }
}

public static class baseDomain
{
    public static readonly string[] Domain = { "tcpi.com.cn", "tcp-china.com" };

    public const string Network = "http://";

    public const string portal = "portal";

    public const string supplier = "supplier";

    /// <summary>
    /// 返回Portal网站地址
    /// </summary>
    /// <returns></returns>
    public static String getPortalWebsite()
    {
        return Network + portal + "." + Domain[0];
    }

    /// <summary>
    /// 返回Supplier网站地址
    /// </summary>
    /// <returns></returns>
    public static String getSupplierWebsite()
    {
        return Network + supplier + "." + Domain[0];
    }

    /// <summary>
    /// 检测邮箱是否写对的方法
    /// </summary>
    /// <param name="email">写入的邮箱地址</param>
    /// <returns></returns>
    public static bool checkDomainOR(String email)
    {
        bool flag = false;
        foreach(string check in Domain)
        {
            if (email.IndexOf(check) != -1)
            {
                flag = true;
            }
        }
        return flag;
    }
}

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    adamClass adam = new adamClass();
    private SecurityHelper _securityHelper = new SecurityHelper(new Hashtable());

    /// <summary>
    /// 授权权限（控件级）
    /// </summary>
    public SecurityHelper Security
    {
        get
        {
            return this._securityHelper;
        }
    }

    private IList<ControlParam> _paramList = new List<ControlParam>();
    /// <summary>
    /// 需要恢复的参数列表
    /// </summary>
    public IList<ControlParam> Params
    {
        get
        {
            return this._paramList;
        }
        set
        {
            this._paramList = value;
        }
    }

    /// <summary>
    /// 创建索引。方便以Security[ID]进行访问
    /// </summary>
    /// <param name="menuID"></param>
    /// <returns></returns>
    public ControlParam this[string ctrlID]
    {
        get
        {
            ControlParam item = null;
            //把得到的控件权限列表循环，如果循环出来的ID其中有和传入的ID相等，证明有此权限
            foreach (ControlParam ctrl in Params)
            {
                if (ctrl.ID == ctrlID)
                {
                    item = new ControlParam(ctrl.ID, ctrl.Value);
                }
            }

            return item;
        }
    }
    /// <summary>
    /// 这个参数只能是私有的。如果在引用时不是用If Else句型，此时使用得到。因为This.Redirect没有立即返回的功能
    /// </summary>
    private bool _hasRecoveryForm = false;

	public BasePage() : base()
	{
        
    }

    protected override void OnPreLoad(EventArgs e)
    {
        //注意，cookie一旦过期，这里是取不到的，即为null
        HttpCookie cookie = Request.Cookies["KeyData"];

        if (!IsPostBack)
        {
            Session["EXHeader"] = "";
            Session["EXTitle"] = null;
            Session["EXSQL"] = null;

            Session["EXHeader1"] = "";
            Session["EXTitle1"] = null;
            Session["EXSQL1"] = null;

            /*
             * Session丢失有两种情况：
             *  1、打开新页面时，原Session就已丢失，导致页面直接无法打开。这个问题通过调用此页面可得到解决
             *  2、刷新已打开页面时，原Session丢失。这个问题可以通过BasePage基础类将关键信息存入打开页面得到解决
             *  
             *  上述两种问题的解决之道是一致的
             */
        }
        else
        {
            adamClass chk = new adamClass();

            if (cookie != null)
            {
                if (Session["PlantCode"] == null && cookie.Expires >= DateTime.Now)
                {
                    Session["PlantCode"] = cookie["PlantCode"];
                }

                if (Session["uID"] == null && cookie.Expires >= DateTime.Now)
                {
                    Session["uID"] = cookie["uID"];
                }

                if (Session["uName"] == null && cookie.Expires >= DateTime.Now)
                {
                    Session["uName"] = HttpUtility.HtmlDecode(cookie["uName"]);
                }

                if (Session["uRole"] == null && cookie.Expires >= DateTime.Now)
                {
                    Session["uRole"] = cookie["uRole"];
                }

                if (Session["roleName"] == null && cookie.Expires >= DateTime.Now)
                {
                    Session["roleName"] = HttpUtility.HtmlDecode(cookie["roleName"]);
                }

                if (Session["orgID"] == null && cookie.Expires >= DateTime.Now)
                {
                    Session["orgID"] = cookie["orgID"];
                }

                if (Session["deptID"] == null && cookie.Expires >= DateTime.Now)
                {
                    Session["deptID"] = cookie["deptID"];
                }

                if (Session["showTutorials"] == null && cookie.Expires >= DateTime.Now)
                {
                    Session["showTutorials"] = cookie["showTutorials"];
                }

                if (Session["ReportProc"] == null && cookie.Expires >= DateTime.Now)
                {
                    Session["ReportProc"] = cookie["ReportProc"];
                }
            }
            else
            {
                Response.Redirect("../Login.aspx?error=sessionout");
            }
        }

        base.OnPreLoad(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["HeightPixel"] != null)
            {
                Session["HeightPixel"] = Request.QueryString["HeightPixel"];
            }

            //存储关键变量
            ViewState["Security"] = null;
            ViewState["PageSize"] = 0;

            //功能块：权限认证
            string strPath = Request.FilePath.ToLower();
            if (strPath != "/login.aspx" && strPath != "/masterpage.aspx" && strPath != "/desktop.aspx")
            {
                HttpCookie cookie = Request.Cookies["KeyData"];

                DataTable tbl = null;
                string ModuleID = string.Empty;
                
                if (Session["HeightPixel"] == null)
                {
                    throw new Exception("Session[HeightPixel]丢失");
                }

                if (Session["PlantCode"] == null)
                {
                    throw new Exception("Session[PlantCode]丢失");
                }

                int nPageSize = AuthorizeMenus(Session["uID"].ToString(), strPath, this._securityHelper.ToString(), 0, Convert.ToInt32(Session["HeightPixel"]), Convert.ToInt32(Session["PlantCode"]), ref tbl, ref ModuleID);

                if (nPageSize < 0)
                {
                    Response.Redirect("../public/denied.htm");
                }
                else
                {
                    Session["ModuleID"] = ModuleID;

                    if (tbl == null)
                    {
                        Response.Redirect("../Login.aspx?login=nullchildmenu");
                    }
                    else
                    {
                        foreach (DataRow row in tbl.Rows)
                        {
                            this._securityHelper.Hashtable.Add(row["id"].ToString(), row["name"].ToString());
                        }
                    }

                    ViewState["PageSize"] = nPageSize;
                }
            }
            //权限存入Session中
            ViewState["Security"] = this._securityHelper;
        }
        else
        {
            /*
             * 前一次的调整值已经存在hidAutoPageSize中了，在自动调整后，该值会有所调整，则此时应该恢复进ViewState中去；特别是多变少的情况；
             * 因为该种情况不会自动刷新，而是去修改hidAutoPageSize的值
             */
            ViewState["PageSize"] = Request.Form["hidAutoPageSize"] == null ? 0 : Convert.ToInt32(Request.Form["hidAutoPageSize"]);

            //功能块：权限认证
            //this._securityHelper是存在内存中的，如果丢失了，再次从ViewState中恢复
            if (this._securityHelper.Hashtable.Count == 0)
            {
                try
                {
                    this._securityHelper = (SecurityHelper)ViewState["Security"];
                }
                catch
                {
                    this._securityHelper = null;
                }
            }
        }

        //功能块：自动调整GridView行数。
        //PageSize写入控件
        foreach (Control ctrl in this.Form.Controls)
        {
            if (ctrl.GetType() == typeof(GridView))
            {
                GridView gv = (GridView)ctrl;
                if (gv.CssClass.Contains("AutoPageSize"))
                {
                    //如果已经存在对应的记录了，则设置；否则，不设置，直接取默认的
                    if (Convert.ToInt32(ViewState["PageSize"]) > 0)
                    {
                        gv.PageSize = Convert.ToInt32(ViewState["PageSize"]);
                    } 
                }
                //功能块：GridView重构
                if (gv.CssClass.Contains("GridViewRebuild"))
                {
                    if (!string.IsNullOrEmpty(Request.Form["GridViewPageCount"]))
                    {
                        gv.PageSize = Convert.ToInt32(Request.Form["GridViewPageCount"]);
                    }
                    else
                    {
                        gv.PageSize = 30;
                    }

                    gv.Attributes.Add("PageSize", gv.PageSize.ToString());
                }
            }
            else if (ctrl.GetType() == typeof(DataGrid))
            {
                DataGrid dg = (DataGrid)ctrl;
                if (dg.CssClass.Contains("AutoPageSize"))
                {
                    if (Convert.ToInt32(ViewState["PageSize"]) > 0)
                    {
                        dg.PageSize = Convert.ToInt32(ViewState["PageSize"]);
                    }
                }
                //功能块：GridView重构
                if (dg.CssClass.Contains("GridViewRebuild"))
                {
                    if (!string.IsNullOrEmpty(Request.Form["GridViewPageCount"]))
                    {
                        dg.PageSize = Convert.ToInt32(Request.Form["GridViewPageCount"]);
                    }
                    else 
                    {
                        dg.PageSize = 30;
                    }
                }

                dg.Attributes.Add("PageSize", dg.PageSize.ToString());
            }
        }
        base.OnLoad(e);
    }
    /// <summary>
    /// 虚函数，供自动恢复参数时调用。页面必须实现此方法
    /// </summary>
    protected virtual void BindGridView() { }

    protected override void OnLoadComplete(EventArgs e)
    {
        if (!IsPostBack)
        {
            //功能块：参数恢复
            //本方法支持多级传送，故需要将非本页参数存入ViewState[RecoveryParams]中
            if (!String.IsNullOrEmpty(Request.Form["recoveryParams"]) && Request.Form["recoveryParams"].Contains("[FORM]") && Request.Form["recoveryParams"].Contains("[PAGE]") && Request.Form["recoveryParams"].Contains("[PARAM]"))
            {
                string _viewParams = string.Empty;

                foreach (string form in Request.Form["recoveryParams"].Split(new string[] { "[FORM]" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] QueryStringArray = form.Split(new string[] { "[PAGE]" }, StringSplitOptions.RemoveEmptyEntries);
                    if (QueryStringArray[0] == Request.FilePath.ToLower())
                    {
                        string[] _params = QueryStringArray[1].Split(new string[] { "[PARAM]" }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string p in _params)
                        {
                            if (!string.IsNullOrEmpty(p.Split(':')[1]))
                            {
                                this.Params.Add(new ControlParam(p.Split(':')[0], p.Split(':')[1]));
                            }
                        }
                    }
                    else
                    {
                        _viewParams += form + "[FORM]";
                    }
                }

                ViewState["RecoveryParams"] = _viewParams;


                foreach (Control ctrl in this.Form.Controls)
                {
                    if (ctrl.GetType() == typeof(TextBox))
                    {
                        TextBox txt = ctrl as TextBox;
                        if (txt.CssClass.Contains("Param") && this.Params.Count > 0 && this[txt.ID] != null)
                        {
                            txt.Text = Server.UrlDecode(this[txt.ID].Value);
                        }
                    }
                    else if (ctrl.GetType() == typeof(CheckBox))
                    {
                        CheckBox chk = ctrl as CheckBox;
                        if (chk.CssClass.Contains("Param") && this.Params.Count > 0 && this[chk.ID] != null)
                        {
                            chk.Checked = this[chk.ID].Value.ToLower() == "false" ? false : true;
                        }
                    }
                    else if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                    {
                        HtmlInputCheckBox inputChk = ctrl as HtmlInputCheckBox;
                        if (inputChk.Attributes["Class"] != null && inputChk.Attributes["Class"].Contains("Param") && this.Params.Count > 0 && this[inputChk.ID] != null)
                        {
                            inputChk.Checked = this[inputChk.ID].Value.ToLower() == "false" ? false : true;
                        }
                    }
                    else if (ctrl.GetType() == typeof(RadioButton))
                    {
                        RadioButton radio = ctrl as RadioButton;
                        if (radio.CssClass.Contains("Param") && this.Params.Count > 0 && this[radio.ID] != null)
                        {
                            radio.Checked = this[radio.ID].Value.ToLower() == "false" ? false : true;
                        }
                    }
                    else if (ctrl.GetType() == typeof(HtmlInputRadioButton))
                    {
                        HtmlInputRadioButton inputRadio = ctrl as HtmlInputRadioButton;
                        if (inputRadio.Attributes["Class"] != null && inputRadio.Attributes["Class"].Contains("Param") && this.Params.Count > 0 && this[inputRadio.ID] != null)
                        {
                            inputRadio.Checked = this[inputRadio.ID].Value.ToLower() == "false" ? false : true;
                        }
                    }
                    else if (ctrl.GetType() == typeof(DropDownList))
                    {
                        DropDownList drop = ctrl as DropDownList;
                        if (drop.CssClass.Contains("Param") && this.Params.Count > 0 && this[drop.ID] != null)
                        {
                            try
                            {
                                drop.SelectedIndex = -1;

                                drop.SelectedIndex = Convert.ToInt32(this[drop.ID].Value);
                            }
                            catch
                            {
                                ;
                            }
                        }
                    }
                    else if (ctrl.GetType() == typeof(GridView))
                    {
                        GridView gv = (GridView)ctrl;
                        if (gv.CssClass.Contains("Param") && this.Params.Count > 0 && this[gv.ID] != null)
                        {
                            gv.PageIndex = Convert.ToInt32(this[gv.ID].Value);
                        }
                    }
                    else if (ctrl.GetType() == typeof(DataGrid))
                    {
                        DataGrid dg = (DataGrid)ctrl;
                        if (dg.CssClass.Contains("Param") && this.Params.Count > 0 && this[dg.ID] != null)
                        {
                            dg.CurrentPageIndex = Convert.ToInt32(this[dg.ID].Value);
                        }
                    }
                }
            }
            //放在此处，那以后页面中的Page_Load函数中，就无需再次调用绑定类函数了
            BasePage bp = this as BasePage;
            bp.BindGridView();
        }

        base.OnLoadComplete(e);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        //回传页标识
        HtmlInputHidden hidIsPostBack = new HtmlInputHidden();
        hidIsPostBack.ID = "hidIsPostBack";
        hidIsPostBack.Value = IsPostBack.ToString();
        this.Controls.Add(hidIsPostBack);

        //是否有已设置了页数，即在AutoPageSize中是否已存在记录了
        HtmlInputHidden hidAutoPageSize = new HtmlInputHidden();
        hidAutoPageSize.ID = "hidAutoPageSize";
        hidAutoPageSize.Value = ViewState["PageSize"].ToString();
        this.Controls.Add(hidAutoPageSize);

        //控制ExportExcel显示与否
        HtmlInputHidden hidExportExcel = new HtmlInputHidden();
        hidExportExcel.ID = "hidExportExcel";
        hidExportExcel.Value = (Session["EXSQL"] == null || Session["EXSQL"].ToString().Trim().Length == 0) ? "0" : "1";
        this.Controls.Add(hidExportExcel);
    }

    protected override void OnError(EventArgs e)
    {
        base.OnError(e);

        #region Session丢失时，直接转出
        if (Session["uID"] == null || Session["uName"] == null || Session["PlantCode"] == null)
        {
            Response.Redirect("/Login.aspx?error=sessionout");
        }

        #endregion
    }

    /// <summary>
    /// 菜单（权限）认证加（要考虑入分辨率）
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="module"></param>
    /// <param name="regists">注册菜单</param>
    /// <param name="ClientWidthPixel"></param>
    /// <param name="ClientHeightPixel"></param>
    /// <returns>GridView显示记录数。-1表示失败；大于0均表示正常</returns>
    public int AuthorizeMenus(string userID, string module, string registerMenus, int ClientWidthPixel, int ClientHeightPixel, int plantCode, ref DataTable authMenus, ref string ModuleID)
    {
        string strSql = "sp_admin_selectAuthorizedMenu";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@userID", userID);
        param[1] = new SqlParameter("@module", module);
        param[2] = new SqlParameter("@registerMenus", registerMenus);
        param[3] = new SqlParameter("@ClientWidthPixel", ClientWidthPixel);
        param[4] = new SqlParameter("@ClientHeightPixel", ClientHeightPixel);
        param[5] = new SqlParameter("@plantCode", plantCode);
        param[6] = new SqlParameter("@pagesize", SqlDbType.Int);
        param[6].Direction = ParameterDirection.Output;

        try
        {
            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, param);

            authMenus = ds.Tables[0];

            ModuleID = "0";

            foreach (DataRow row in ds.Tables[1].Rows)
            {
                ModuleID = row["ModuleID"].ToString();
            }

            return Convert.ToInt32(param[6].Value);
        }
        catch
        {
            return -2;
        }
    }
    /// <summary>
    /// 验证指定的权限，是否合法
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public bool AccessAuthorizeMenuByID(string userID, string module)
    {
        string strSql = "sp_admin_checkAuthorizedMenuByID";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@userID", userID);
        param[1] = new SqlParameter("@module", module);
        param[2] = new SqlParameter("@retValue", SqlDbType.Int);
        param[2].Direction = ParameterDirection.Output;

        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, param);

            return Convert.ToBoolean(param[2].Value);
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 获取授权菜单的子权限
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    public DataTable GetAuthorizedChildMenus(string userID, string module, string registerMenus)
    {
        string strSql = "sp_admin_selectAuthorizedChildMenu";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@userID", userID);
        param[1] = new SqlParameter("@module", module);
        param[2] = new SqlParameter("@registerMenus", registerMenus);

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
    /// 获取快捷菜单列表
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public DataTable GetUserHomePages(string userID)
    {
        string strSql = "sp_adm_selectHomePageList";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@userID", userID);

        try
        {
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    #region 公共方法
    /// <summary>
    /// 是否是浮点数（可包含正负符号、小数点）
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool IsNumber(string str)
    {
        if (Regex.IsMatch(str, @"^[+-]?\d*[.]?\d*$"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 是否是日期格式：2013-11-12、11/12/2013等，也可包含时间
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool IsDate(string str)
    {
        string[] val = str.Split('-');

        if (val.Length != 3)
        {
            val = str.Split('.');

            if (val.Length != 3)
            {
                val = str.Split('/');

                if (val.Length != 3)
                {
                    return false;
                }
            }
        }

        try
        {
            DateTime _dt = Convert.ToDateTime(str);
        }
        catch
        {
            try
            {
                //处理：月日年 的情况
                DateTime _dt = Convert.ToDateTime(str, new System.Globalization.CultureInfo("fr-FR"));
            }
            catch
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// 判断是否含中文字符
    /// </summary>
    /// <param name="input"></param>
    /// <returns>True含中文字符 False不含</returns>
    public bool CheckStringChinese(string input)
    {
        foreach (var c in input.ToCharArray())
        {
            if (c >= 0x4e00 && c <= 0x9fbb)
            {
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Alert弹出框
    /// </summary>
    /// <param name="msg">所要弹出的信息</param>
    public void Alert(string msg)
    {
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "Alert", "<script language=\"JavaScript\" type=\"text/javascript\">alert('" + msg + "');</script>");
    }
    /// <summary>
    /// 替代Response.Redirect方法，以实现返回恢复页面功能
    /// </summary>
    /// <param name="redirectUrl">重定向Url</param>
    public void Redirect(string redirectUrl)
    {
        if (!this._hasRecoveryForm)
        {
            string paramArray = Request.FilePath.ToLower() + "[PAGE]";
            foreach (Control ctrl in this.Form.Controls)
            {
                if (ctrl.GetType() == typeof(TextBox))
                {
                    TextBox txt = ctrl as TextBox;
                    if (txt.CssClass.Contains("Param") && txt.Text.Trim().Length > 0)
                    {
                        paramArray += txt.ID + ":" + Server.UrlEncode(txt.Text) + "[PARAM]";
                    }
                }
                else if (ctrl.GetType() == typeof(CheckBox))
                {
                    CheckBox chk = ctrl as CheckBox;
                    if (chk.CssClass.Contains("Param"))
                    {
                        paramArray += chk.ID + ":" + chk.Checked.ToString() + "[PARAM]";
                    }
                }
                else if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                {
                    HtmlInputCheckBox inputChk = ctrl as HtmlInputCheckBox;
                    if (inputChk.Attributes["Class"].Contains("Param"))
                    {
                        paramArray += inputChk.ID + ":" + inputChk.Checked.ToString() + "[PARAM]";
                    }
                }
                else if (ctrl.GetType() == typeof(RadioButton))
                {
                    RadioButton radio = ctrl as RadioButton;
                    if (radio.CssClass.Contains("Param"))
                    {
                        paramArray += radio.ID + ":" + radio.Checked.ToString() + "[PARAM]";
                    }
                }
                else if (ctrl.GetType() == typeof(HtmlInputRadioButton))
                {
                    HtmlInputRadioButton inputRadio = ctrl as HtmlInputRadioButton;
                    if (inputRadio.Attributes["Class"].Contains("Param"))
                    {
                        paramArray += inputRadio.ID + ":" + inputRadio.Checked.ToString() + "[PARAM]";
                    }
                }
                else if (ctrl.GetType() == typeof(DropDownList))
                {
                    DropDownList drop = ctrl as DropDownList;
                    if (drop.CssClass.Contains("Param"))
                    {
                        paramArray += drop.ID + ":" + drop.SelectedIndex.ToString() + "[PARAM]";
                    }
                }
                else if (ctrl.GetType() == typeof(GridView))
                {
                    GridView gv = (GridView)ctrl;
                    if (gv.CssClass.Contains("Param"))
                    {
                        paramArray += gv.ID + ":" + gv.PageIndex.ToString() + "[PARAM]";
                    }
                }
                else if (ctrl.GetType() == typeof(DataGrid))
                {
                    DataGrid dg = (DataGrid)ctrl;
                    if (dg.CssClass.Contains("Param"))
                    {
                        paramArray += dg.ID + ":" + dg.CurrentPageIndex.ToString() + "[PARAM]";
                    }
                }
            }

            //如果没有参数就不继续操作了
            if (paramArray.Contains("[PARAM]"))
            {
                paramArray += "[FORM]";
            }
            else
            {
                paramArray = string.Empty;
            }

            //为支持多级传送，最后要加上ViewState["RecoveryParams"]
            paramArray += ViewState["RecoveryParams"] == null ? string.Empty : ViewState["RecoveryParams"].ToString();

            LiteralControl lc = new LiteralControl();
            lc.Text = "<form id=\"autoRecoveryForm\" method=\"post\" action=\"" + redirectUrl.Replace("\"", "&quot") + "\">";
            lc.Text += "<input name=\"recoveryParams\" type=\"hidden\" value=\"" + paramArray + "\" />";
            lc.Text += "</form>";
            lc.Text += "<script>";
            lc.Text += "autoRecoveryForm.submit();";
            lc.Text += "</script>";
            this.Page.Controls.Add(lc);
        }

        this._hasRecoveryForm = true;
    }
    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="from">发件人</param>
    /// <param name="to">收件人</param>
    /// <param name="copy">抄送人</param>
    /// <param name="subject">主题</param>
    /// <param name="body">内容</param>
    /// <returns></returns>
    public bool SendEmail(string from, string to, string copy, string subject, string body)
    {
        try
        {
            MailAddress _mailFrom = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
            MailMessage _mailMessage = new MailMessage();

            _mailMessage.From = _mailFrom;

            foreach (string _to in to.Split(';'))
            {
                if (!string.IsNullOrEmpty(_to))
                {
                    _mailMessage.To.Add(_to);
                }
            }

            if (!string.IsNullOrEmpty(copy))
            {
                foreach (string _cc in copy.Split(';'))
                {
                    if (!string.IsNullOrEmpty(_cc))
                    {
                        MailAddress _mailCopy = new MailAddress(_cc);
                        _mailMessage.CC.Add(_mailCopy);
                    }
                }
            }

            _mailMessage.Subject = subject + " from:" + from;
            _mailMessage.Body = Convert.ToString(body);
            _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(_mailMessage);

            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }
    /// <summary>
    /// 允许非继承条用
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="copy"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public static bool SSendEmail(string from, string to, string copy, string subject, string body)
    {
        try
        {
            MailAddress _mailFrom = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
            MailAddress _mailTo = new MailAddress(to);
            MailMessage _mailMessage = new MailMessage();

            _mailMessage.From = _mailFrom;
            _mailMessage.To.Add(_mailTo);

            if (!string.IsNullOrEmpty(copy))
            {
                foreach (string _cc in copy.Split(';'))
                {
                    if (!string.IsNullOrEmpty(_cc))
                    {
                        MailAddress _mailCopy = new MailAddress(_cc);
                        _mailMessage.CC.Add(_mailCopy);
                    }
                }
            }

            _mailMessage.Subject = subject + " from:" + from;
            _mailMessage.Body = Convert.ToString(body);
            _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(_mailMessage);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="from">发件人</param>
    /// <param name="to">收件人</param>
    /// <param name="copy">抄送人</param>
    /// <param name="subject">主题</param>
    /// <param name="body">内容</param>
    /// /// <param name="filepath">附件路径</param>
    /// <returns></returns>
    public bool SendEmail(string from, string to, string copy, string subject, string body, string path, string filepath)
    {
        try
        {
            MailAddress _mailFrom = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"].ToString());
            MailMessage _mailMessage = new MailMessage();

            _mailMessage.From = _mailFrom;

            foreach (string _to in to.Split(';'))
            {
                if (!string.IsNullOrEmpty(_to))
                {
                    _mailMessage.To.Add(_to);
                }
            }

            if (!string.IsNullOrEmpty(copy))
            {
                foreach (string _cc in copy.Split(';'))
                {
                    if (!string.IsNullOrEmpty(_cc))
                    {
                        MailAddress _mailCopy = new MailAddress(_cc);
                        _mailMessage.CC.Add(_mailCopy);
                    }
                }
            }

            _mailMessage.Subject = subject + " from:" + from;
            _mailMessage.Body = Convert.ToString(body);
            _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Priority = MailPriority.Normal;
            //_mailMessage.Attachments.Add(new Attachment("F:\\TCP160607.xls"));
            _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            //附件 

            if (filepath.IndexOf(";") > 0)
            {
                string[] sArray = filepath.Split(';');
                foreach (string i in sArray)
                {
                    if (!string.IsNullOrEmpty(i.ToString().Trim()))
                    {
                        string strFilePath = path + i.ToString().Trim();//filepath;//@"F:\\TCP160607.xls";
                        System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(strFilePath);//添加附件 
                        attachment1.Name = System.IO.Path.GetFileName(strFilePath);
                        attachment1.NameEncoding = System.Text.Encoding.GetEncoding("gb2312");
                        attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                        attachment1.ContentDisposition.Inline = true;
                        attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                        string cid = attachment1.ContentId;//关键性的地方，这里得到一个id数值 
                        _mailMessage.Attachments.Add(attachment1);
                    }
                }
            }
            else
            {
                string strFilePath = filepath;//@"F:\\TCP160607.xls";
                System.Net.Mail.Attachment attachment1 = new System.Net.Mail.Attachment(strFilePath);//添加附件 
                attachment1.Name = System.IO.Path.GetFileName(strFilePath);
                attachment1.NameEncoding = System.Text.Encoding.GetEncoding("gb2312");
                attachment1.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                attachment1.ContentDisposition.Inline = true;
                attachment1.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                string cid = attachment1.ContentId;//关键性的地方，这里得到一个id数值 
                _mailMessage.Attachments.Add(attachment1);
            }

            SmtpClient client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"].ToString(), ConfigurationManager.AppSettings["AdminEmailPwd"].ToString());
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(_mailMessage);
            _mailMessage.Dispose();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }



    /// <summary>
    /// 获取文件MD5值
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public string GetMD5HashFromFile(string fileName)
    {
        try
        {
            FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
    }  
    #endregion

    #region 导出公共方法
    /// <summary>
    /// Excel标题定义：250^<b>产品型号</b>~^200^<b>产品简称</b>~^
    /// </summary>
    private class ExcelTitle
    {
        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        private int _width;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        public ExcelTitle(string name, int width)
        {
            this.Name = name;
            this.Width = width;
        }

        public ExcelTitle()
        {

        }
    }
    /// <summary>
    /// 分解表头
    /// </summary>
    /// <param name="str">格式：250^<b>产品型号</b>~^200^<b>产品简称</b>~^</param>
    /// <returns></returns>
    private string GetString(string str)
    {
        if (str.IndexOf("<b>") > -1)
        {
            string str1 = "<b>";
            string str2 = "</b>";

            int n1 = str.IndexOf(str1, 0) + str1.Length;
            int n2 = str.IndexOf(str2, n1);
            if (n2 < 0)
            {
                throw new Exception("对不起，系统发生错误！请通知管理员。错误提示：标题未闭合。" + Request.UrlReferrer.ToString());
            }
            return str.Substring(n1, n2 - n1);
        }
        else
        {
            return str;
        }
    }
    /// <summary>
    /// 确定Excel单元格内容格式
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private string GetFormat(string str)
    {
        if (str != "")
        {
            if (IsDate(str))
            {
                return "DATE";
            }
            else if (IsNumber(str))
            {
                return "NUMBER";
            }
            else
            {
                return "NORMAL";
            }
        }
        else
        {
            return "NORMAL";
        }
    }

    private void SetDetailsValue(ISheet sheet, int total, DataTable dt, int startRowIndex, bool fullDateFormat = false)
    {
        if (dt.Columns.Count >= total)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + startRowIndex);
                
                for (int j = 1; j <= total; j++)
                {
                    ICell cell = row.CreateCell(j - 1);
                    int _col1 = j - 1 + (dt.Columns.Count - total);
                    cell.SetCellValue(dt.Rows[i][_col1], fullDateFormat);
                }
            }
        }
    }

    private void SetColumnTitleAndStyle(IWorkbook workbook, ISheet sheet, IList<ExcelTitle> ItemList, DataTable dt, ICellStyle styleHeader, IRow rowHeader, bool fullDateFormat)
    {
        int total = ItemList.Count;
        foreach (ExcelTitle item in ItemList)
        {
            int titleIndex = ItemList.IndexOf(item);
            sheet.SetColumnWidth(titleIndex, item.Width);

            ICell cell = rowHeader.CreateCell(titleIndex);
            cell.CellStyle = styleHeader;
            cell.SetCellValue(item.Name);

            int dtCol = 0;

            if (dt.Columns.Count == total)
            {
                dtCol = titleIndex;
            }
            else
            {
                dtCol = titleIndex + (dt.Columns.Count - total);
            }

            ICellStyle columnStyle = SetColumnStyleByDataType(workbook, dt.Columns[dtCol].DataType.ToString(), fullDateFormat);
            sheet.SetDefaultColumnStyle(titleIndex, columnStyle);
        }
    }

    private ICellStyle SetColumnStyleByDataType(IWorkbook workbook, string dataType, bool fullDateFormat)
    {
        ICellStyle style = workbook.CreateCellStyle();
        IFont font = workbook.CreateFont();
        style.VerticalAlignment = VerticalAlignment.Center;
        IDataFormat dataFormat = workbook.CreateDataFormat();
        short formatIndex;
        if (fullDateFormat)
        {
            formatIndex = dataFormat.GetFormat("yyyy-MM-dd hh:mm:ss");
        }
        else
        {
            formatIndex = dataFormat.GetFormat("yyyy-MM-dd");
        }
        switch (dataType)
        {
            case "System.DateTime":
                style.Alignment = HorizontalAlignment.Center;
                style.DataFormat = formatIndex;
                break;
            case "System.Int16":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int32":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int64":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Decimal":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Double":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Boolean":
                style.Alignment = HorizontalAlignment.Center;
                break;
            case "System.String":
                style.Alignment = HorizontalAlignment.Left;
                style.WrapText = true;
                break;
        }
        style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        font.FontHeightInPoints = 9;
        style.SetFont(font);
        return style;
    }

    private ICellStyle SetHeaderStyle(IWorkbook workbook)
    {
        ICellStyle styleHeader = workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//居中对齐

        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillPattern = FillPattern.SolidForeground;

        styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        IFont fontHeader = workbook.CreateFont();
        fontHeader.FontHeightInPoints = 10;
        fontHeader.Boldweight = 600;
        styleHeader.SetFont(fontHeader);

        return styleHeader;
    }

    private IList<ExcelTitle> GetExcelTitles(string EXTitle)
    {
        var ItemList = new List<ExcelTitle>();

        string str = EXTitle;
        int total = 0;
        int ind = 0;
        while (str.Length > 0)
        {
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                total = total + 1;
                str = "";
                break;
            }
            total = total + 1;
            str = str.Substring(ind + 2);
        }

        str = EXTitle;

        for (int i = 0; i <= total - 1; i++)
        {
            ExcelTitle item = new ExcelTitle();
            int width = 100 * 6000 / 164;
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                ind = str.IndexOf("L~");
                if (ind > -1)
                {
                    str = str.Substring(2);
                }

                ind = str.IndexOf("^");
                if (ind == -1)
                {
                    item.Name = str.Substring(2);
                    item.Width = width;
                }
                else
                {
                    item.Name = str.Substring(ind + 1);
                    item.Width = Convert.ToInt32(str.Substring(0, ind)) * 6000 / 164;
                }
                str = "";
                break;
            }
            else
            {
                item.Name = str.Substring(0, ind);
                item.Width = width;
                str = str.Substring(ind + 2);

                ind = item.Name.IndexOf("L~");
                if (ind > -1)
                {
                    item.Name = item.Name.Substring(2);
                }

                ind = item.Name.IndexOf("^");
                if (ind > -1)
                {
                    item.Width = Convert.ToInt32(item.Name.Substring(0, ind)) * 6000 / 164;
                    item.Name = item.Name.Substring(ind + 1);
                }
            }

            item.Name = item.Name.Replace("<b>", "").Replace("</b>", "");
            ItemList.Add(item);
        }
        return ItemList;
    }
    /// <summary>
    /// 拼接SQL语句的导出Excel通用方法(NPOI方法)
    /// </summary>
    /// <param name="dsnx">chk.dsn0()或chk.dsnx()</param>
    /// <param name="EXTitle">格式如：<b>工号</b>~^250^<b>姓名</b>~^</param>
    /// <param name="EXSQL"></param>
    /// <param name="fullDateFormat">（将被取消）日期格式：yyyy-MM-dd HH:mm:ss还是yyyy-MM-dd</param>
    public void ExportExcel(string dsnx, string EXTitle, string EXSQL, bool fullDateFormat)
    {
        adamClass chk = new adamClass();
        string sqlStr = EXSQL;
        DataTable dt = SqlHelper.ExecuteDataset(dsnx, CommandType.Text, sqlStr).Tables[0];
        ExportExcel(EXTitle, dt, fullDateFormat);
    }
    /// <summary>
    /// 拼接SQL语句的导出Excel通用方法(NPOI方法)
    /// </summary>
    /// <param name="dsnx">chk.dsn0()或chk.dsnx()</param>
    /// <param name="EXHeader">Header。通常位于第一行</param>
    /// <param name="EXTitle">格式如：<b>工号</b>~^250^<b>姓名</b>~^</param>
    /// <param name="EXSQL">SQL语句</param>
    /// <param name="fullDateFormat">日期格式：yyyy-MM-dd HH:mm:ss还是yyyy-MM-dd</param>
    public void ExportExcel(string dsnx, string EXHeader, string EXTitle, string EXSQL, bool fullDateFormat)
    {
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");

        IList<ExcelTitle> ItemList = GetExcelTitles(EXTitle);
        int total = ItemList.Count;

        adamClass chk = new adamClass();
        string sqlStr = EXSQL;

        DataTable dt = SqlHelper.ExecuteDataset(dsnx, CommandType.Text, sqlStr).Tables[0];

        //头栏样式
        ICellStyle styleHeader = SetHeaderStyle(workbook);

        //写主题栏
        IRow rowTitle = sheet.CreateRow(0);
        ICell cellTitle = rowTitle.CreateCell(0);
        cellTitle.SetCellValue(EXHeader);

        //写标题栏
        IRow rowHeader = sheet.CreateRow(1);
        SetColumnTitleAndStyle(workbook, sheet, ItemList, dt, styleHeader, rowHeader, fullDateFormat);

        //写入明细数据
        SetDetailsValue(sheet, total, dt, 2);

        dt.Reset();

        string _localFileName = string.Format("{0}.xls", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);

            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.Length);
            localFile.Dispose();
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "','_blank', 'width=800,height=600,top=0,left=0');</script>");
    }

    /// <summary>
    /// 导出Excel通用方法（数据源是DataTable）ExData的列数一定要大于或等于ExTitle里的标题列数。如果等于则从ExData从第一列开始写入Excel,如果大于则取后半部分和标题列数相等的部分写入Excel。
    /// </summary>
    /// <param name="dsnx">chk.dsn0()或chk.dsnx()</param>
    /// <param name="EXTitle">格式如：<b>工号</b>~^250^<b>姓名</b>~^</param>
    /// <param name="EXSQL"></param>
    /// <param name="fullDateFormat">日期格式：yyyy-MM-dd HH:mm:ss还是yyyy-MM-dd</param>
    public void ExportExcel(string EXTitle, DataTable EXData, bool fullDateFormat, ExcelVersion Version = ExcelVersion.Excel2007)
    {
        if (Version == ExcelVersion.Excel2007)
        {
            ExportExcel2007(EXTitle, EXData, fullDateFormat);
        }
        else
        {
            ExportExcel2003(EXTitle, EXData, fullDateFormat);
        }
    }
    
    /// <summary>
    /// 导出Excel通用方法（数据源是DataTable）ExData的列数一定要大于或等于ExTitle里的标题列数。如果等于则从ExData从第一列开始写入Excel,如果大于则取后半部分和标题列数相等的部分写入Excel。
    /// 可以按主从结构，把主数据合并单元格
    /// </summary>
    /// <param name="EXTitle">标题</param>
    /// <param name="EXData">导出数据源</param>
    /// <param name="fullDateFormat">日期格式：yyyy-MM-dd HH:mm:ss还是yyyy-MM-dd</param>
    /// <param name="mainColCount">主数据列数（需要合并单元格）</param>
    /// <param name="keyFlieds">主数据的关键字段（按关键字段的值来判断是否要合并）</param>
    public void ExportExcel(string EXTitle, DataTable EXData, bool fullDateFormat,int mainColCount,params string[] keyFlieds)
    {
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");

        IList<ExcelTitle> ItemList = GetExcelTitles(EXTitle);
        int total = ItemList.Count;

        DataTable dt = EXData;

        //头栏样式
        ICellStyle styleHeader = SetHeaderStyle(workbook);

        //写标题栏
        IRow rowHeader = sheet.CreateRow(0);
        SetColumnTitleAndStyle(workbook, sheet, ItemList, dt, styleHeader, rowHeader, fullDateFormat);

        SetDetailsValueMerged(sheet, total, dt, 1, mainColCount, fullDateFormat, keyFlieds);

        dt.Reset();
        string _localFileName = string.Format("{0}.xls", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);

            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.Length);
            localFile.Dispose();
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");
    }
    /// <summary>
    /// 按模板格式导出（模板头栏定义好，只写数据）
    /// </summary>
    /// <param name="startIndex">从第几行开始写</param>
    /// <param name="colCount">总共多少列</param>
    /// <param name="tempFile">模板的据对路径</param>
    /// <param name="EXData"></param>
    /// <param name="fullDateFormat"></param>
    public void ExportExcel(int startIndex, int colCount, string tempFile, DataTable EXData, bool fullDateFormat)
    {
        if (!File.Exists(tempFile))
        {
            throw new Exception("模板 " + tempFile + " 不存在");
        }

        FileStream templetFile = new FileStream(tempFile, FileMode.Open, FileAccess.Read);
        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet sheet = workbook.GetSheetAt(1);

        DataTable dt = EXData;


        //写明细数据
        SetDetailsValue(sheet, colCount, dt, startIndex, fullDateFormat);

        dt.Reset();

        string _localFileName = string.Format("{0}.xls", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);

            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.Length);
            localFile.Dispose();
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");
    }
    private void SetDetailsValueMerged(ISheet sheet, int total, DataTable dt,int startRowIndex,int mainColCount,bool fullDateFormat,params string[] keyFields)
    {
        bool mainIsNew = false;
        string[] keyValue = new string[keyFields.Length];
        int mergeStartRowIndex = startRowIndex;
        int n = startRowIndex;
        if (dt.Columns.Count >= total)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + startRowIndex);

                //string[] rowKeyValue = new string[keyFields.Length];
                for (int j = 0; j <= keyFields.Length - 1;j++ )
                {
                    if (keyValue[j] != dt.Rows[i][keyFields[j]].ToString().Trim())
                    {
                        mainIsNew = true;
                        keyValue[j] = dt.Rows[i][keyFields[j]].ToString().Trim();
                    }
                }
                if (mainIsNew)
                {
                    if (i > 0 && mergeStartRowIndex != n - 1)
                    {
                        for (int j = 0; j < mainColCount; j++)
                        {
                            sheet.AddMergedRegion(new CellRangeAddress(mergeStartRowIndex, n - 1, j, j));
                        }
                    }
                    mergeStartRowIndex = n;
                    mainIsNew = false;
                }
                for (int j = 1; j <= total; j++)
                {
                    ICell cell = row.CreateCell(j - 1);
                    int _col1 = j - 1 + (dt.Columns.Count - total);
                    cell.SetCellValue(dt.Rows[i][_col1], fullDateFormat);
                }
                n++;
            }

            if (mergeStartRowIndex != n - 1)
            {
                for (int j = 0; j < mainColCount; j++)
                {
                    sheet.AddMergedRegion(new CellRangeAddress(mergeStartRowIndex, n - 1, j, j));
                }
            }
        }
    }
    #endregion

    #region

     /// <summary>  
    /// 将DataTable数据导出到Excel文件中(xlsx)2003
    /// </summary>  
    /// <param name="dt"></param>  
    /// <param name="file"></param>  
    public void ExportExcel2003(string EXTitle, DataTable EXData, bool fullDateFormat)
    {
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");

        IList<ExcelTitle> ItemList = GetExcelTitles(EXTitle);
        int total = ItemList.Count;

        DataTable dt = EXData;

        //头栏样式
        ICellStyle styleHeader = SetHeaderStyle(workbook);

        //写标题栏
        IRow rowHeader = sheet.CreateRow(0);
        SetColumnTitleAndStyle(workbook, sheet, ItemList, dt, styleHeader, rowHeader, fullDateFormat);

        //写明细数据
        SetDetailsValue(sheet, total, dt, 1, fullDateFormat);

        dt.Reset();

        string _localFileName = string.Format("{0}.xls", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);

            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.Length);
            localFile.Dispose();
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");

    }

    private ICellStyle SetHeaderStyle2007(IWorkbook workbook)
    {
        ICellStyle styleHeader = workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//居中对齐

        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillPattern = FillPattern.SolidForeground;

        styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        IFont fontHeader = workbook.CreateFont();
        fontHeader.FontHeightInPoints = 10;
        fontHeader.Boldweight = 600;
        fontHeader.FontName = "Arial";
        fontHeader.Boldweight = short.MaxValue;
        styleHeader.SetFont(fontHeader);

        return styleHeader;
    }
    private void SetColumnTitleAndStyle2007(IWorkbook workbook, ISheet sheet, IList<ExcelTitle> ItemList, DataTable dt, ICellStyle styleHeader, IRow rowHeader, bool fullDateFormat)
    {
        int total = ItemList.Count;
        foreach (ExcelTitle item in ItemList)
        {
            int titleIndex = ItemList.IndexOf(item);
            sheet.SetColumnWidth(titleIndex, item.Width);

            ICell cell = rowHeader.CreateCell(titleIndex);
            cell.CellStyle = styleHeader;
            cell.SetCellValue(item.Name);

            int dtCol = 0;

            if (dt.Columns.Count == total)
            {
                dtCol = titleIndex;
            }
            else
            {
                dtCol = titleIndex + (dt.Columns.Count - total);
            }

            ICellStyle columnStyle = SetColumnStyleByDataType(workbook, dt.Columns[dtCol].DataType.ToString(), fullDateFormat);
            sheet.SetDefaultColumnStyle(titleIndex, columnStyle);
        }
    }

    private ICellStyle SetColumnStyleByDataType2007(IWorkbook workbook, string dataType)
    {
        ICellStyle style = workbook.CreateCellStyle();
        IFont font = workbook.CreateFont();
        style.VerticalAlignment = VerticalAlignment.Center;
        switch (dataType)
        {
            case "System.DateTime":
                style.Alignment = HorizontalAlignment.Center;
                break;
            case "System.Int16":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int32":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int64":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Decimal":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Double":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Boolean":
                style.Alignment = HorizontalAlignment.Center;
                break;
            case "System.String":
                style.Alignment = HorizontalAlignment.Left;
                style.WrapText = true;
                break;
        }
        style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        font.FontHeightInPoints = 9;
        font.FontName = "Arial";
        style.SetFont(font);
        return style;
    }

    private void SetDetailsValue2007(IWorkbook workbook, ISheet sheet, int total, DataTable dt, int startRowIndex,ICellStyle styleHeader, bool fullDateFormat = false)
    {
        if (dt.Columns.Count >= total)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + startRowIndex);
               
                for (int j = 1; j <= total; j++)
                {
                    ICell cell = row.CreateCell(j - 1);
                    int _col1 = j - 1 + (dt.Columns.Count - total);
                    cell.SetCellValue(dt.Rows[i][_col1], fullDateFormat);
                    //sheet.AutoSizeColumn(j);  //自适应宽度
                    cell.CellStyle = styleHeader;
                    //cell.CellStyle = SetColumnStyleByDataType2007(workbook, dt.Rows[i][_col1].ToString());
                }
            }
        }
    }
    private ICellStyle SetDetStyle2007(IWorkbook workbook)
    {
        ICellStyle styleDet = workbook.CreateCellStyle();
        //styleDet.Alignment = HorizontalAlignment.Center;//居中对齐
        
        styleDet.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.White.Index;
        styleDet.FillPattern = FillPattern.SolidForeground;

        styleDet.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleDet.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleDet.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleDet.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleDet.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleDet.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleDet.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleDet.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        IFont fontDet = workbook.CreateFont();
        fontDet.FontHeightInPoints = 9;
        fontDet.Boldweight = 600;
        fontDet.FontName = "Arial";
        fontDet.Boldweight = short.MaxValue;
        styleDet.SetFont(fontDet);

        return styleDet;
    }

    /// <summary>  
    /// 将DataTable数据导出到Excel文件中(xlsx)2007  
    /// </summary>  
    /// <param name="dt"></param>  
    /// <param name="file"></param>  
    public void ExportExcel2007(string EXTitle, DataTable EXData, bool fullDateFormat)  
    {  
       XSSFWorkbook workbook = new XSSFWorkbook();
       ISheet sheet = workbook.CreateSheet("excel");

       IList<ExcelTitle> ItemList = GetExcelTitles(EXTitle);
       int total = ItemList.Count;

       DataTable dt = EXData;

       //头栏样式
       ICellStyle styleHeader = SetHeaderStyle2007(workbook);

       //写标题栏
       IRow rowHeader = sheet.CreateRow(0);
       SetColumnTitleAndStyle2007(workbook, sheet, ItemList, dt, styleHeader, rowHeader, fullDateFormat);

       //明细样式
       ICellStyle styleDet = SetDetStyle2007(workbook);

       //写明细数据
       SetDetailsValue2007(workbook, sheet, total, dt, 1, styleDet, fullDateFormat);

       dt.Reset();

       string _localFileName = string.Format("{0}.xlsx", DateTime.Now.ToFileTime().ToString());

       using (MemoryStream ms = new MemoryStream())
       {
           workbook.Write(ms);

           Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
           localFile.Write(ms.ToArray(), 0, (int)ms.ToArray().Length);
           localFile.Dispose();
           ms.Flush();
           //ms.Position = 0;
           sheet = null;
           workbook = null;
       }

       Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");

    }  

    #endregion

    #region 采用NPOI读取Excel
    /// <summary>
    /// 采用NPOI读取Excel
    /// </summary>
    /// <param name="excelPath">要读取的Excel路径</param>
    /// <param name="header">不能为空！验证Excel表头。格式是：客户,物料号,价格</param>
    /// <returns></returns>
    public DataTable GetExcelContents(string excelPath)
    {
        string ext = Path.GetExtension(excelPath).ToLower();
        DataTable dt = null;
        if (ext == ".xls")
        {
            dt = GetExcelContent2003(excelPath);
        }
        else
        {
            dt = GetExcelContent2007(excelPath);
        }
        return dt;
    }
    #endregion

    #region 采用NPOI读取Excel
    /// <summary>
    /// 采用NPOI读取Excel2003
    /// </summary>
    /// <param name="excelPath">要读取的Excel路径</param>
    /// <param name="header">不能为空！验证Excel表头。格式是：客户,物料号,价格</param>
    /// <returns></returns>
    public DataTable GetExcelContent2003(string excelPath)
    {
        if (File.Exists(excelPath))
        {
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            FileStream fileStream = new FileStream(excelPath, FileMode.Open);
            IWorkbook workbook = new HSSFWorkbook(fileStream);

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheetAt(0);

            DataTable table = new DataTable();
            //获取sheet的首行
            IRow headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum + 1;

            for (int i = (sheet.FirstRowNum + 1); i < rowCount; i++)
            {
                try
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();
                    
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    dataRow[j] = "";
                                    break;
                                case CellType.String:
                                    dataRow[j] = cell.StringCellValue.Trim();
                                    break;
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = cell.DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = cell.NumericCellValue;
                                    }
                                    break;
                                case CellType.Formula:
                                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                                    dataRow[j] = e.Evaluate(cell).StringValue;
                                    break;
                                default:
                                    dataRow[j] = cell.ToString();
                                    break;
                            }
                        }
                    }

                    table.Rows.Add(dataRow);
                }
                catch
                {
                    continue;
                }
            }

            workbook = null;
            sheet = null;

            return table;
        }
        else
        {
            return null;
        }
    }


    /// <summary>  
    /// 采用NPOI读取Excel2007
    /// 将Excel文件中的数据读出到DataTable中(xlsx)  
    /// </summary>  
    /// <param name="file"></param>  
    /// <returns></returns>  
    public DataTable GetExcelContent2007(string file)
    {
        DataTable dt = new DataTable();
        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
            ISheet sheet = xssfworkbook.GetSheetAt(0);

            //表头  
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            List<int> columns = new List<int>();
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    //continue;  
                }
                else
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                columns.Add(i);
            }
            //数据  
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int j in columns)
                {
                    dr[j] = GetValueTypeForXLSX(sheet.GetRow(i).GetCell(j) as XSSFCell);
                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }
    /// <summary>  
    /// 获取单元格类型(xlsx)  
    /// </summary>  
    /// <param name="cell"></param>  
    /// <returns></returns>  
    private static object GetValueTypeForXLSX(XSSFCell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank:
                return null;
            case CellType.Boolean:
                return cell.BooleanCellValue;
            case CellType.Numeric:
                if (HSSFDateUtil.IsCellDateFormatted(cell))
                {
                    return cell.DateCellValue;
                }
                else
                {
                    return cell.NumericCellValue;
                }
            case CellType.String:
                return cell.StringCellValue.Trim();
            case CellType.Error:
                return cell.ErrorCellValue;
            case CellType.Formula:
            default:
                return "=" + cell.CellFormula;
        }
    }

    #endregion

}
