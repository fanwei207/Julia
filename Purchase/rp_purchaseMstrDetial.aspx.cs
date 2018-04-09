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
using System.Web.UI.WebControls.Expressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
//using System.Web.Mail;
using System.Text;
using Microsoft.Web.UI.WebControls;
using System.IO;
using CommClass;
using System.Net.Mail;
using Purchase;


public partial class Purchase_rp_purchaseMstrDetial : BasePage
{
    adamClass adam = new adamClass();
    RPPurchase pur = new RPPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        hiduID.Value = Session["uID"].ToString();
        hiduName.Value = Session["uName"].ToString();
        hidEmail.Value = Session["email"].ToString();
        hidPlant.Value = Session["plantCode"].ToString();
        if (!IsPostBack)
        {
            hidID.Value = Request["ID"].ToString();
            BindBusDept();
            BindGV();
            BindPurchaseInfo();
        }
    }
    private void BindPurchaseInfo()
    {
        #region 绑定Mstr信息
        DataTable tbMstr = SelectCustCompMstrByID();
        foreach (DataRow rows in tbMstr.Rows)
        {
            hidNo.Value = rows["rp_No"].ToString();
            hidStatus.Value = rows["Status"].ToString();
            hidDeptStatus.Value = rows["deptStatus"].ToString();
            hidDeptStatusBy.Value = rows["deptStatusBy"].ToString();
            hidDeptStatusName.Value = rows["deptStatusName"].ToString();

            labDeptName.Text = rows["rp_deptName"].ToString();
            createDeptID.Value = rows["rp_deptID"].ToString();
            labCreate.Text = rows["createName"].ToString();
            hidID.Value = rows["ID"].ToString();
            labNo.Text = rows["rp_No"].ToString();
            ddlBusDept.SelectedValue = rows["rp_BusinessDept"].ToString();


            hidCreateBy.Value = rows["createBy"].ToString();
            hidCreateEmail.Value = SelectCreateEmail();

            hidCreatePlant.Value = rows["rp_plantCode"].ToString();

            hidBusDeptUserStatus.Value = rows["busDeptUserStatus"].ToString();
            hidBusDeptUserStatusBy.Value = rows["busDeptUserStatusBy"].ToString();
            hidBusDeptUserStatusName.Value = rows["busDeptUserStatusName"].ToString();

            hidBusDeptStatus.Value = rows["busDeptStatus"].ToString();
            hidBusDeptStatusBy.Value = rows["busDeptStatusBy"].ToString();
            hidBusDeptStatusName.Value = rows["busDeptStatusName"].ToString();

            hidEquipmentStatus.Value = rows["EquipmentStatus"].ToString();
            hidEquipmentStatusBy.Value = rows["EquipmentStatusBy"].ToString();
            hidEquipmentStatusName.Value = rows["EquipmentStatusName"].ToString();

            hidSupplierStatus.Value = rows["supplierStatus"].ToString();
            hidSupplierStatusBy.Value = rows["supplierStatusBy"].ToString();
            hidSupplierStatusName.Value = rows["supplierStatusName"].ToString();

            hidCEOStatus.Value = rows["CEOStatus"].ToString();
            hidCEOStatusBy.Value = rows["CEOStatusBy"].ToString();
            hidCEOStatusName.Value = rows["CEOStatusName"].ToString();


            hidHasQad.Value = "0";
            hasCEO.Value = "0";
        }
        tdFile.InnerHtml = GetPurchaseMagandFile(hidNo.Value.ToString(), "file").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        #endregion

        #region 绑定部门主管
        if (hidDeptStatus.Value == "2")
        {
            btnDeptYes.Visible = false;
            btnDeptBack.Text = "驳回 (" + hidDeptStatusName.Value.ToString() + ")";
            btnDeptBack.Enabled = false;
        }
        else if (hidDeptStatus.Value == "1")
        {
            btnDeptBack.Visible = false;
            btnDeptYes.Text = "同意 (" + hidDeptStatusName.Value.ToString() + ")";
            btnDeptYes.Enabled = false;
        }
        else
        {
            btnDeptYes.Visible = true;
            btnDeptBack.Visible = true;
            btnDeptYes.Enabled = true;
            btnDeptBack.Enabled = true;
            btnDeptYes.Text = "同意";
            btnDeptBack.Text = "驳回";
        }
        tdDept.InnerHtml = GetPurchaseMagandFile(hidNo.Value.ToString(), "dept").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        //labFinance.Text = GetProcUser("1", "username", labCustCompNo.Text).Replace(",", "<br /><br />");
        hidDeptID.Value = getDeptInfo("userid");
        #endregion

        #region 绑定业务部门
        if (hidBusDeptUserStatus.Value == "2")
        {
            btnBusDeptUserSubmit.Visible = false;
            btnBusDeptUserBack.Text = "驳回 (" + hidBusDeptUserStatusName.Value.ToString() + ")";
            btnBusDeptUserBack.Enabled = false;
        }
        else if (hidBusDeptUserStatus.Value == "1")
        {
            btnBusDeptUserBack.Visible = false;
            btnBusDeptUserSubmit.Text = "提交 (" + hidBusDeptUserStatusName.Value.ToString() + ")";
            btnBusDeptUserSubmit.Enabled = false;
        }
        else
        {
            btnBusDeptUserSubmit.Visible = true;
            btnBusDeptUserBack.Visible = true;
            btnBusDeptUserSubmit.Enabled = true;
            btnBusDeptUserBack.Enabled = true;
            btnBusDeptUserSubmit.Text = "提交";
            btnBusDeptUserBack.Text = "驳回";
        }
        tdBusDeptuser.InnerHtml = GetPurchaseMagandFile(hidNo.Value.ToString(), "busDeptUser").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        labBusDeptUser.Text = GetBusDeptUser(ddlBusDept.SelectedValue.ToString(), "username", "0").Replace(",", "<br /><br />");
        hidBusDeptUserID.Value = GetBusDeptUser(ddlBusDept.SelectedValue.ToString(), "userid", "0");
        hidBusDeptUserEmail.Value = GetBusDeptUser(ddlBusDept.SelectedValue.ToString(), "email", "0");
        #endregion

        #region 绑定业务主管
        if (hidBusDeptStatus.Value == "2")
        {
            btnBusDeptYes.Visible = false;
            btnBusDeptBack.Text = "驳回 (" + hidBusDeptStatusName.Value.ToString() + ")";
            btnBusDeptBack.Enabled = false;
        }
        else if (hidBusDeptStatus.Value == "1")
        {
            btnBusDeptBack.Visible = false;
            btnBusDeptYes.Text = "同意 (" + hidBusDeptStatusName.Value.ToString() + ")";
            btnBusDeptYes.Enabled = false;
        }
        else
        {
            btnBusDeptYes.Visible = true;
            btnBusDeptBack.Visible = true;
            btnBusDeptYes.Enabled = true;
            btnBusDeptBack.Enabled = true;
            btnBusDeptYes.Text = "同意";
            btnBusDeptBack.Text = "驳回";
        }
        tdBusDept.InnerHtml = GetPurchaseMagandFile(hidNo.Value.ToString(), "busDept").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        labBusDept.Text = GetBusDeptUser(ddlBusDept.SelectedValue.ToString(), "username", "1").Replace(",", "<br /><br />");
        hidBusDeptID.Value = GetBusDeptUser(ddlBusDept.SelectedValue.ToString(), "userid", "1");
        hidBusDeptEmail.Value = GetBusDeptUser(ddlBusDept.SelectedValue.ToString(), "email", "1");
        #endregion

        #region 绑定设备部门
        if (hidEquipmentStatus.Value == "2")
        {
            btnEquipmentYes.Visible = false;
            btnEquipmentBack.Text = "驳回 (" + hidEquipmentStatusName.Value.ToString() + ")";
            btnEquipmentBack.Enabled = false;
        }
        else if (hidEquipmentStatus.Value == "1")
        {
            btnEquipmentBack.Visible = false;
            btnEquipmentYes.Text = "同意 (" + hidEquipmentStatusName.Value.ToString() + ")";
            btnEquipmentYes.Enabled = false;
        }
        else
        {
            btnEquipmentYes.Visible = true;
            btnEquipmentBack.Visible = true;
            btnEquipmentYes.Enabled = true;
            btnEquipmentBack.Enabled = true;
            btnEquipmentYes.Text = "同意";
            btnEquipmentBack.Text = "驳回";
        }
        tdEquipment.InnerHtml = GetPurchaseMagandFile(hidNo.Value.ToString(), "equipment").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        labEquipment.Text = GetProcUser("2", "username").Replace(",", "<br /><br />");
        hidEquipmentID.Value = GetProcUser("2", "userid");
        hidEquipmentEmail.Value = GetProcUser("2", "email");
        #endregion

        #region 绑定供应商部门
        if (SelectIsExistsPrice())
        {
            btnSupplierBack.Visible = false;
            hidHasQad.Value = "1";
        }
        else
        {
            if (hidSupplierStatus.Value == "2")
            {
                btnSupplierBack.Text = "驳回 (" + hidSupplierStatusName.Value.ToString() + ")";
                btnSupplierBack.Enabled = false;
            }
            else if (hidSupplierStatus.Value == "1")
            {
                btnSupplierBack.Visible = false;
            }
            else
            {
                btnSupplierBack.Visible = true;
                btnSupplierBack.Enabled = true;
                btnSupplierBack.Text = "驳回";
            }
        }
        tdSupplier.InnerHtml = GetPurchaseMagandFile(hidNo.Value.ToString(), "supplier").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        labSupplier.Text = GetProcUser("3", "username").Replace(",", "<br /><br />");
        hidSupplierID.Value = GetProcUser("3", "userid");
        hidSupplierEmail.Value = GetProcUser("3", "email");
        #endregion

        #region 绑定副总
        if (SelectCEOStatus())
        {
            btnCEOYse.Visible = false;
            btnCEOBack.Visible = false;
            hasCEO.Value = "1";
        }
        else
        {
            if (hidCEOStatus.Value == "2")
            {
                btnCEOYse.Visible = false;
                btnCEOBack.Text = "驳回 (" + hidCEOStatusName.Value.ToString() + ")";
                btnCEOBack.Enabled = false;
            }
            else if (hidCEOStatus.Value == "1")
            {
                btnCEOBack.Visible = false;
                btnCEOYse.Text = "同意 (" + hidCEOStatusName.Value.ToString() + ")";
                btnCEOYse.Enabled = false;
            }
            else
            {
                btnCEOYse.Visible = true;
                btnCEOBack.Visible = true;
                btnCEOYse.Enabled = true;
                btnCEOBack.Enabled = true;
                btnCEOYse.Text = "同意";
                btnCEOBack.Text = "驳回";
            }
        }
        tdCEO.InnerHtml = GetPurchaseMagandFile(hidNo.Value.ToString(), "ceo").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        labCEO.Text = GetCEOUser("username").Replace(",", "<br /><br />").Replace(";", "");
        hidCEOID.Value = GetCEOUser("userid");
        hidCEOEmail.Value = GetCEOUser("email");
        #endregion
    }
    private string SelectCreateEmail()
    {
        string sql = "select email from users where userid = " + hidCreateBy.Value.ToString();
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql));
    }
    private string getDeptInfo(string type)
    {
        string str = "sp_rp_getDeptInfo";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@deptID", createDeptID.Value.ToString());
        param[1] = new SqlParameter("@type", type);
        param[2] = new SqlParameter("@plantcode", hidCreatePlant.Value.ToString());

        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 获取业务部门的人员--权限
    /// </summary>
    /// <param name="deptid"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GetBusDeptUser(string deptid, string type, string isboss)
    {
        string str = "sp_rp_GetBusDeptUser";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@deptID", deptid);
        param[1] = new SqlParameter("@type", type);
        param[2] = new SqlParameter("@isboss", isboss);
        param[3] = new SqlParameter("@plant", hidCreatePlant.Value.ToString());
        param[4] = new SqlParameter("@id", hidID.Value.ToString());
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 获取副总--权限
    /// </summary>
    /// <param name="deptid"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GetCEOUser(string type)
    {
        string str = "sp_rp_GetCEOUser";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@type", type);
        param[1] = new SqlParameter("@plant", hidCreatePlant.Value.ToString());
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }

    private string GetProcUser(string deptid, string type)
    {
        string str = "sp_rp_GetProcUser";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@type", type);
        param[1] = new SqlParameter("@deptid", deptid);
        param[2] = new SqlParameter("@plantcode", Session["PlantCode"].ToString());
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool SelectIsExistsPrice()
    {
        string str = "sp_rp_SelectIsExistsPrice";
        SqlParameter param = new SqlParameter("@id", hidID.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool SelectCEOStatus()
    {
        string str = "sp_rp_SelectCEOStatus";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@id", hidID.Value.ToString());
        param[1] = new SqlParameter("@plant", hidPlant.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 获取每个步骤的留言和文档
    /// </summary>
    /// <param name="no"></param>
    /// <param name="dept"></param>
    /// <returns></returns>
    private string GetPurchaseMagandFile(string no, string dept)
    {
        string str = "sp_rp_GetPurchaseMagandFile";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@dept", dept);
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private DataTable SelectCustCompMstrByID()
    {
        string str = "sp_rp_SelectPurchaseMstrByID";
        SqlParameter param = new SqlParameter("@id", hidID.Value.ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }

    private void BindBusDept()
    {
        DataTable dt = selectBusDept();
        ddlBusDept.Items.Clear();
        ddlBusDept.DataSource = dt;
        ddlBusDept.DataBind();
        ddlBusDept.Items.Insert(0, new ListItem("--业务部门--", "0"));

    }
    private DataTable selectBusDept()
    {
        string sql = "Select departmentid,departmentname From RP_department";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0]; ;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request["type"] != null)
        {
            if (Request["type"].ToString() == "listdet")
            {
                this.Redirect("../Purchase/rp_purchaseListDet.aspx?vend=" + Request["vend"].ToString() + "&type=" + Request["types"].ToString() + "&domain=" + Request["domain"].ToString());
            }
            else
            {
                this.Redirect("../Purchase/rp_purchaseMstrList.aspx");
            }
        }
        else
        {
            if (Request["vender"] != null)
            {
                this.Redirect("../price/PCF_madeInquiryDet.aspx?vender=" + Request["vender"].ToString() + "&venderName=" + Request["venderName"].ToString());
            }
            if (Request["PCF_inquiryID"] != null)
            {
                this.Redirect("../price/PCF_InquiryDet.aspx?PCF_inquiryID=" + Request["PCF_inquiryID"] + "&TVender=" + Request.QueryString["TVender"] + "&TVenderName=" + Request.QueryString["TVenderName"] + "&TQAD=" + Request.QueryString["TQAD"] + "&ddlStatus=" + Request.QueryString["ddlStatus"]);
            }
        }

        

        //else if (Request["type"].ToString() == "edit1")
        //{
        //    Response.Redirect("../price/PCF_madeInquiryDet.aspx?vender=" + Request["vender"].ToString() + "&venderName=" + Request["venderName"].ToString());
        //}
        //else if (Request["type"].ToString() == "edit2")
        //{
        //    Response.Redirect("../price/PCF_InquiryDet.aspx?PCF_inquiryID=" + Request["PCF_inquiryID"]);

        //} 

    }
    private void BindGV()
    {
        DataTable dt = pur.SelectPurchaseDet(Request["ID"].ToString());
        gv.DataSource = dt;
        gv.DataBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindGV();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindGV();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string ID = gv.DataKeys[e.RowIndex].Values["ID"].ToString().Trim();
        string qad = ((TextBox)gv.Rows[e.RowIndex].Cells[0].FindControl("txtQAD")).Text.ToString().Trim();
        string vend = ((TextBox)gv.Rows[e.RowIndex].Cells[1].FindControl("txtVend")).Text.ToString().Trim();
        string vendName = ((TextBox)gv.Rows[e.RowIndex].Cells[2].FindControl("txtVendName")).Text.ToString().Trim();
        string um = ((TextBox)gv.Rows[e.RowIndex].Cells[3].FindControl("txtUm")).Text.ToString().Trim();
        string price = ((TextBox)gv.Rows[e.RowIndex].Cells[5].FindControl("txtPrice")).Text.ToString().Trim();
        string qadDesc1 = ((TextBox)gv.Rows[e.RowIndex].Cells[6].FindControl("txtQADDesc1")).Text.ToString().Trim();
        string qadDesc2 = ((TextBox)gv.Rows[e.RowIndex].Cells[7].FindControl("txtQADDesc2")).Text.ToString().Trim();
        string format = ((TextBox)gv.Rows[e.RowIndex].Cells[10].FindControl("txtFormat")).Text.ToString().Trim();


        string str = "sp_rp_UpdatePurchaseDetByID";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@id", ID);
        param[1] = new SqlParameter("@qad", qad);
        param[2] = new SqlParameter("@vend", vend);
        param[3] = new SqlParameter("@vendName", vendName);
        param[4] = new SqlParameter("@um", um);
        param[5] = new SqlParameter("@price", price);
        param[6] = new SqlParameter("@qadDesc1", qadDesc1);
        param[7] = new SqlParameter("@qadDesc2", qadDesc2);
        param[8] = new SqlParameter("@format", format);

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, param);

        gv.EditIndex = -1;
        BindGV();
    }
    protected void btnDeptYes_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("dept", "1"))
        {
            ltlAlert.Text = "alert('部门主管操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidDeptStatus.Value = "1";
            BindPurchaseInfo();
            #region 部门主管同意后发送邮件给业务部门
            string Topical = "采购单：" + labNo.Text;
            string mto = hidBusDeptUserEmail.Value.ToString();
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   有一张新的采购单已通过部门主管审核，现需要您进行相应的操作，采购单号：" + labNo.Text + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(Session["email"].ToString(), mto, Topical, body);
            #endregion
        }
    }

    public static bool SendEmail222(string from, string to, string subject, string body)
    {
        try
        {
            MailAddress _mailFrom = new MailAddress(from);
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = _mailFrom;

            foreach (string _to in to.Split(';'))
            {
                if (!string.IsNullOrEmpty(_to))
                {
                    _mailMessage.To.Add(_to);
                }
            }


            _mailMessage.Subject = subject;
            _mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, Encoding.GetEncoding("GB2312"), "text/html");

            _mailMessage.AlternateViews.Add(htmlBody);
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
    /// 部门主管驳回
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDeptBack_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("dept", "2"))
        {
            ltlAlert.Text = "alert('部门主管操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidDeptStatus.Value = "2";
            BindPurchaseInfo();
            #region 部门主管驳回后发送邮件给申请人
            string Topical = "采购单：" + labNo.Text;
            string mto = hidCreateEmail.Value.ToString();
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   您申请的采购单已被部门主管驳回，采购单号：" + labNo.Text + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
            #endregion
        }
    }
    /// <summary>
    /// 更新各状态
    /// </summary>
    /// <param name="deptType"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    private bool UpdateStatus(string deptType, string status)
    {
        string str = "sp_rp_UpdateStatus";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@id", hidID.Value.ToString());
        param[1] = new SqlParameter("@deptType", deptType);
        param[2] = new SqlParameter("@status", status);
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@uName", Session["uName"].ToString());
        param[5] = new SqlParameter("@plant", Session["PlantCode"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnBusDeptUserSubmit_Click(object sender, EventArgs e)
    {
        if (selectIsExistsSupplier())
        {
            if (!selectIsExistsUm())
            {
                ltlAlert.Text = "alert('有物料没有填写采购单位，请填写后再提交'); ";
                return;
            }
            else
            {
                if (!UpdateStatus("busdeptuser", "1"))
                {
                    ltlAlert.Text = "alert('业务部门操作失败，请联系管理员'); ";
                    return;
                }
                else
                {
                    hidBusDeptUserStatus.Value = "1";
                    BindPurchaseInfo();
                    #region 业务部门提交后发送邮件给业务主管
                    string Topical = "采购单：" + labNo.Text;
                    string mto = hidBusDeptEmail.Value.ToString();
                    string body = "<html>";
                    body += "<body>";
                    body += "<form>";
                    body += "<br>";
                    body += "您好:" + "<br>";
                    body += "   有新的采购单需要你审批，采购单号：" + labNo.Text + "<br>";
                    body += "<br>";
                    body += "</body>";
                    body += "</form>";
                    body += "</html>";
                    SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
                    #endregion
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('有物料没有填写供应商，请填写后再提交'); ";
            return;
        }
    }
    protected void btnBusDeptUserBack_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("busdeptuser", "2"))
        {
            ltlAlert.Text = "alert('业务部门操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidBusDeptUserStatus.Value = "2";
            BindPurchaseInfo();
            #region 业务部门驳回后发送邮件给申请人
            string Topical = "采购单：" + labNo.Text;
            string mto = hidCreateEmail.Value.ToString();
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   您申请的采购单已被业务部门驳回，采购单号：" + labNo.Text + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
            #endregion
        }

    }
    private bool selectIsExistsSupplier()
    {
        string str = "sp_rp_selectIsExistsSupplier";
        SqlParameter param = new SqlParameter("@id",hidID.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool selectIsExistsUm()
    {
        string str = "sp_rp_selectIsExistsUm";
        SqlParameter param = new SqlParameter("@id", hidID.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnBusDeptYes_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("busdept", "1"))
        {
            ltlAlert.Text = "alert('业务主管操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidBusDeptStatus.Value = "1";
            BindPurchaseInfo();
            if (!selectIsExistsQAD())
            {
                #region 业务主管同意后发送邮件给设备部
                string Topical = "采购单：" + labNo.Text;
                string mto = hidEquipmentEmail.Value.ToString();
                string body = "<html>";
                body += "<body>";
                body += "<form>";
                body += "<br>";
                body += "您好:" + "<br>";
                body += "   有新的采购单业务主管已审批，请您进行审批，采购单号：" + labNo.Text + "<br>";
                body += "<br>";
                body += "</body>";
                body += "</form>";
                body += "</html>";
                SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
                #endregion 
            }
        }
    }
    protected void btnBusDeptBack_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("busdept", "2"))
        {
            ltlAlert.Text = "alert('业务主管操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidBusDeptStatus.Value = "2";
            BindPurchaseInfo();
            #region 业务主管驳回后发送邮件给业务部人员
            string Topical = "采购单：" + labNo.Text;
            string mto = hidBusDeptUserEmail.Value.ToString();
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   采购单已被主管驳回，请您在系统中进行相应操作，采购单号：" + labNo.Text + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
            #endregion
        }
    }
    protected void btnEquipmentYes_Click(object sender, EventArgs e)
    {
        if (selectIsExistsQAD())
        {
            if (!UpdateStatus("equipment", "1"))
            {
                ltlAlert.Text = "alert('设备部操作失败，请联系管理员'); ";
                return;
            }
            else
            {
                hidEquipmentStatus.Value = "1";
                BindPurchaseInfo();
                if (SelectIsExistsPrice())
                {
                    #region 设备部同意后如果价格全都都有发送邮件给副总
                    string Topical = "采购单：" + labNo.Text;
                    string mto = hidCEOEmail.Value.ToString();
                    string body = "<html>";
                    body += "<body>";
                    body += "<form>";
                    body += "<br>";
                    body += "您好:" + "<br>";
                    body += "   有新的采购单已被设备部同意，请您在系统中进行相应操作，采购单号：" + labNo.Text + "<br>";
                    body += "<br>";
                    body += "</body>";
                    body += "</form>";
                    body += "</html>";
                    SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
                    #endregion
                }
                else
                {
                    #region 设备部同意后发送邮件给供应商开发部
                    string Topical = "采购单：" + labNo.Text;
                    string mto = hidSupplierEmail.Value.ToString();
                    string body = "<html>";
                    body += "<body>";
                    body += "<form>";
                    body += "<br>";
                    body += "您好:" + "<br>";
                    body += "   有新的采购单已被设备部同意，请您在系统中进行相应操作，采购单号：" + labNo.Text + "<br>";
                    body += "<br>";
                    body += "</body>";
                    body += "</form>";
                    body += "</html>";
                    SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
                    #endregion
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('请填写完整QAD后再提交'); ";
            return;
        }
    }
    private bool selectIsExistsQAD()
    {
        string str = "sp_rp_selectIsExistsQAD";
        SqlParameter param = new SqlParameter("@id", hidID.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnEquipmentBack_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("equipment", "2"))
        {
            ltlAlert.Text = "alert('设备部操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidEquipmentStatus.Value = "2";
            BindPurchaseInfo();
            #region 业务主管驳回后发送邮件给业务部人员
            string Topical = "采购单：" + labNo.Text;
            string mto = hidBusDeptUserEmail.Value.ToString();
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   采购单已被设备部驳回，请您在系统中进行相应操作，采购单号：" + labNo.Text + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
            #endregion
        }

    }
    protected void btnSupplierBack_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("supplier", "2"))
        {
            ltlAlert.Text = "alert('供应商开发部操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidSupplierStatus.Value = "2";
            BindPurchaseInfo();
            #region 供应商开发部驳回后发送邮件给业务部人员
            string Topical = "采购单：" + labNo.Text;
            string mto = hidBusDeptUserEmail.Value.ToString();
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   采购单已被供应商开发部驳回，请您在系统中进行相应操作，采购单号：" + labNo.Text + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
            #endregion
        }
    }
    protected void btnCEOYse_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("ceo", "1"))
        {
            ltlAlert.Text = "alert('副总操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidCEOStatus.Value = "1";
            BindPurchaseInfo();
        }
    }
    protected void btnCEOBack_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("ceo", "2"))
        {
            ltlAlert.Text = "alert('副总操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidCEOStatus.Value = "2";
            BindPurchaseInfo();
            #region 副总驳回后发送邮件给业务部人员
            string Topical = "采购单：" + labNo.Text;
            string mto = hidBusDeptUserEmail.Value.ToString();
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   采购单已被副总驳回，请您在系统中进行相应操作，采购单号：" + labNo.Text + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(hidEmail.Value.ToString(), mto, Topical, body);
            #endregion
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[11].Visible = false;
        
        }
    }
}