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
using NPOI.SS.Util;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;


public partial class EDI_CustComplaint_SheetDetial : BasePage
{
    adamClass adam = new adamClass();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("19531000", "投诉单明细（部门主管审核权限）");
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hiduID.Value = Session["uID"].ToString();
            hiduName.Value = Session["uName"].ToString();
            #region 传递的参数
            hidID.Value = Request["ID"].ToString();
            /*
            hidID.Value = Request["ID"].ToString();
            hidNo.Value = Request["No"].ToString();
            hidDeptStatus.Value = Request["DeptStatus"].ToString();
            hidDeptStatusBy.Value = Request["DeptStatusBy"].ToString();
            hidDeptStatusName.Value = Request["DeptStatusName"].ToString();
            hidCustomer.Value = Request["Customer"].ToString();
            hidOrder.Value = Request["Order"].ToString();
            hidDescribe.Value = Request["Describe"].ToString();
            hidFinance.Value = Request["Finance"].ToString();
            hidFinanceBy.Value = Request["FinanceBy"].ToString();
            hidFinanceName.Value = Request["FinanceName"].ToString();
            hidAfterSaleService.Value = Request["AfterSaleService"].ToString();
            hidAfterSaleServiceBy.Value = Request["AfterSaleServiceBy"].ToString();
            hidAfterSaleServiceName.Value = Request["AfterSaleServiceName"].ToString();
            hidReqDate.Value = Request["ReqDate"].ToString();
            hidDueDate.Value = Request["DueDate"].ToString();
            if (!string.IsNullOrEmpty(Request["Factory"].ToString()))
            {
                hidFactory.Value = Request["Factory"].ToString();
            }
            else
            {
                hidFactory.Value = "0";
            }
            hidResponsiblePerson.Value = Request["ResponsiblePerson"].ToString();
            hidPayment.Value = Request["Payment"].ToString();
            hidStaus.Value = Request["Staus"].ToString();
            hidDetModeifyBy.Value = Request["DetModeifyBy"].ToString();
            hidDetModeifyName.Value = Request["DetModeifyName"].ToString();
            hidcreateBy.Value = Request["createBy"].ToString();
            hidcreateName.Value = Request["createName"].ToString();
            */
            

            #endregion
            BindInfo();
        }
    }
    private DataTable SelectCustCompMstrByID()
    {
        string str = "sp_CustComp_SelectCustCompMstrByID";
        SqlParameter param = new SqlParameter("@id", hidID.Value.ToString());

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    private void BindInfo()
    { 
        #region 绑定Mstr信息
        DataTable tbMstr = SelectCustCompMstrByID();
        foreach (DataRow rows in tbMstr.Rows)
        {
            hidNo.Value = rows["CustComp_No"].ToString();
            hidDeptStatus.Value = rows["CustComp_DeptStatus"].ToString();
            hidDeptStatusBy.Value = rows["CustComp_DeptStatusBy"].ToString();
            hidDeptStatusName.Value = rows["CustComp_DeptStatusName"].ToString();
            hidCustomer.Value = rows["CustComp_Customer"].ToString();
            hidCustomerName.Value = rows["CustComp_CustomerName"].ToString();
            hidOrder.Value = rows["CustComp_OrderID"].ToString();
            hidDescribe.Value = rows["CustComp_Describe"].ToString();
            hidFinance.Value = rows["CustComp_Finance"].ToString();
            hidFinanceBy.Value = rows["CustComp_FinanceBy"].ToString();
            hidFinanceName.Value = rows["CustComp_FinanceName"].ToString();
            hidAfterSaleService.Value = rows["CustComp_AfterSaleService"].ToString();
            hidAfterSaleServiceBy.Value = rows["CustComp_AfterSaleServiceBy"].ToString();
            hidAfterSaleServiceName.Value = rows["CustComp_AfterSaleServiceName"].ToString();
            hidDateCode.Value = rows["CustComp_DateCode"].ToString();
            hidDueDate.Value = rows["CustComp_DueDate"].ToString();
            hidFactory.Value = rows["CustComp_Factory"].ToString();
            hidResponsiblePerson.Value = rows["CustComp_ResponsiblePerson"].ToString();
            hidPayment.Value = rows["CustComp_Payment"].ToString();
            hidStaus.Value = rows["CustComp_Staus"].ToString();
            hidDetModeifyBy.Value = rows["CustComp_DetModeifyBy"].ToString();
            hidDetModeifyName.Value = rows["CustComp_DetModeifyName"].ToString();
            hidcreateBy.Value = rows["createBy"].ToString();
            hidcreateName.Value = rows["createName"].ToString();

            txtSidMoney.Text = rows["SID_Money"].ToString();
        }
        #endregion

        #region 绑定投诉单基本信息
        BindPart();
        BindGoods();
        BindSID();
        labCustCompNo.Text = hidNo.Value.ToString();
        labCust.Text = hidCustomer.Value.ToString();
        labCustName.Text = hidCustomerName.Value.ToString();
        labOrder.Text = hidOrder.Value.ToString();
        labDescribe.Text = hidDescribe.Value.ToString();
        labMoney.Text = SelectPaymentMoney();// +" ($)";
        labReqDate.Text = hidDateCode.Value.ToString();
        labDueDate.Text = hidDueDate.Value.ToString();
        //if (CheckExistsGoods())
        //{
        //    chkGoods.Checked = true;
        //}
        tdFile.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "file").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        labPaymentTotal.Text = SelectPaymentTotal() + "($)";
        if (this.Security["19531000"].isValid)
        {
            hidDeptSecurity.Value = "1";
        }
        else
        {
            hidDeptSecurity.Value = "2";
        }
        if (hidDeptStatus.Value.ToString() == "1")
        {
            btnDeptNo.Visible = false;
            btnDeptYes.Text = "同意 (" + hidDeptStatusName.Value.ToString() + ")";
            btnDeptYes.Enabled = false;
        }
        else if (hidDeptStatus.Value.ToString() == "2")
        {
            btnDeptYes.Visible = false;
            btnDeptNo.Text = "驳回 (" + hidDeptStatusName.Value.ToString() + ")";
            btnDeptNo.Enabled = false;
        }
        else
        {
            btnDeptYes.Visible = true;
            btnDeptNo.Visible = true;
            btnDeptYes.Enabled = true;
            btnDeptNo.Enabled = true;
            btnDeptYes.Text = "同意";
            btnDeptNo.Text = "驳回";
        }
        tdDept.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "dept").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        #endregion

        #region 绑定财务
        if (hidFinance.Value == "2")
        {
            btnFinYes.Visible = false;
            btnFinNo.Text = "驳回 (" + hidFinanceName.Value.ToString() + ")";
            btnFinNo.Enabled = false;
        }
        else if (hidFinance.Value == "1")
        {
            btnFinNo.Visible = false;
            btnFinYes.Text = "同意 (" + hidFinanceName.Value.ToString() + ")";
            btnFinYes.Enabled = false;
        }
        else
        {
            btnFinYes.Visible = true;
            btnFinNo.Visible = true;
            btnFinYes.Enabled = true;
            btnFinNo.Enabled = true;
            btnFinYes.Text = "同意";
            btnFinNo.Text = "驳回";
        }
        tdFinance.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "finance").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        labFinance.Text = GetProcUser("1", "username",labCustCompNo.Text).Replace(",", "<br /><br />");
        hidFinanceID.Value = GetProcUser("1", "userid", labCustCompNo.Text);
        #endregion

        #region 绑定售后服务
        if (hidAfterSaleService.Value == "2")
        {
            btnAfterSaleServiceYes.Visible = false;
            btnAfterSaleServiceNo.Text = "驳回 (" + hidAfterSaleServiceName.Value.ToString() + ")";
            btnAfterSaleServiceNo.Enabled = false;
        }
        else if (hidAfterSaleService.Value == "1")
        {
            btnAfterSaleServiceNo.Visible = false;
            btnAfterSaleServiceYes.Text = "同意 (" + hidAfterSaleServiceName.Value.ToString() + ")";
            btnAfterSaleServiceYes.Enabled = false;
        }
        else
        {
            btnAfterSaleServiceYes.Visible = true;
            btnAfterSaleServiceNo.Visible = true;
            btnAfterSaleServiceYes.Enabled = true;
            btnAfterSaleServiceNo.Enabled = true;
            btnAfterSaleServiceYes.Text = "同意";
            btnAfterSaleServiceNo.Text = "驳回";
        }
        //绑定工厂
        ddlFactory.SelectedValue = hidFactory.Value;
        tdAfterSaleService.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "afterSaleService").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        labAfterSaleService.Text = GetProcUser("2", "username", labCustCompNo.Text).Replace(",", "<br /><br />");
        hidAfterSaleServiceID.Value = GetProcUser("2", "userid", labCustCompNo.Text);
        #endregion

        #region 绑定讨论区
        tdTalk.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "talk").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        labTalk.Text = GetProcUser(ddlFactory.SelectedValue + "0", "username", labCustCompNo.Text).Replace(",", "<br /><br />");
        hidTalkID.Value = GetProcUser(ddlFactory.SelectedValue.ToString() + "0", "userid", labCustCompNo.Text);
        #endregion

        #region 绑定处理方案

        tdOpinion.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "opinion").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        labOpinion.Text = GetProcUser(ddlFactory.SelectedValue + "0", "username", labCustCompNo.Text).Replace(",", "<br /><br />");
        hidOpinionID.Value = GetProcUser(ddlFactory.SelectedValue + "0", "userid", labCustCompNo.Text);
        #endregion

        #region 绑定最终结论
        if (hidStaus.Value.ToString() == "1")
        {
            btnFinsh.Text = "完结（" + hidDetModeifyName.Value.ToString() +")";
            btnFinsh.Enabled = false;
        }
        txtResponsiblePerson.Text = hidResponsiblePerson.Value.ToString();
        //txtPayment.Text = hidPayment.Value.ToString();
        labPayment.Text = hidPayment.Value.ToString();

        tdFinalOpinion.InnerHtml = GetProcMagandFile(hidNo.Value.ToString(), "finalOpinion").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        labFinalOpin.Text = GetProcUser("4", "username", labCustCompNo.Text).Replace(",", "<br /><br />");
        hidFinalOpinID.Value = GetProcUser("4", "userid", labCustCompNo.Text);
        #endregion
    }
    private string SelectPaymentTotal()
    {
        string str = "sp_CustComp_SelectPaymentTotal";
        SqlParameter param = new SqlParameter("@no", hidNo.Value.ToString());

        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private string SelectPaymentMoney()
    {
        string str = "sp_CustComp_SelectPaymentMoney";
        SqlParameter param = new SqlParameter("@no", hidNo.Value.ToString());

        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool CheckExistsGoods()
    {
        string str = "sp_CustComp_CheckExistsGoods";
        SqlParameter param = new SqlParameter("@no", hidNo.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0() ,CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 获取每个步骤的人员--权限
    /// </summary>
    /// <param name="deptid"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private string GetProcUser(string deptid, string type, string no)
    {
        switch (deptid)
        {
            case "10": 
                deptid = "50";
                break;
            case "20":
                deptid = "60";
                break;
            case "50":
                deptid = "70";
                break;
            case "80":
                deptid = "80";
                break;
        }
        string str = "sp_CustComp_GetTestUser";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@type", type);
        param[1] = new SqlParameter("@deptid", deptid);
        param[2] = new SqlParameter("@no", no);
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 获取每个步骤的留言和文档
    /// </summary>
    /// <param name="no"></param>
    /// <param name="dept"></param>
    /// <returns></returns>
    private string GetProcMagandFile(string no, string dept)
    {
        string str = "sp_CustComp_GetTestMagandFile";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@dept", dept);
        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../rmInspection/CustComplaint_SheetList.aspx");
    }
    protected void btnFinYes_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("Finance", "1"))
        {
            ltlAlert.Text = "alert('财务操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidFinance.Value = "1";
            hidFinanceBy.Value = Session["uID"].ToString();
            hidFinanceName.Value = Session["uName"].ToString();
            if (hidAfterSaleService.Value == "1")
            {
                InsertPayment();
            }
            BindInfo();
        }
    }
    private bool UpdateStatus(string deptType, string status)
    {
        string str = "sp_CustComp_UpdateStatus";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@id", hidID.Value.ToString());
        param[1] = new SqlParameter("@deptType", deptType);
        param[2] = new SqlParameter("@status", status);
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@uName", Session["uName"].ToString());
        param[5] = new SqlParameter("@factory", ddlFactory.SelectedValue.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnFinNo_Click(object sender, EventArgs e)
    {
        if(!UpdateStatus("Finance","2"))
        {
            ltlAlert.Text = "alert('财务操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidFinance.Value = "2";
            hidFinanceBy.Value = Session["uID"].ToString();
            hidFinanceName.Value = Session["uName"].ToString();
            BindInfo();
            #region 驳回后向创建者发送邮件
            string Topical = "客户投诉单：" + labCustCompNo.Text;
            string mto = GetUserEmailTo("1", "create", labCustCompNo.Text);
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   财务已对客户投诉单号：" + labCustCompNo.Text + "进行驳回，请重新修改此客户投诉单<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(Session["email"].ToString(), mto, Topical, body);
            #endregion
        }
    }
    protected void btnAfterSaleServiceYes_Click(object sender, EventArgs e)
    {
        if(!UpdateStatus("AfterSaleService","1"))
        {
            ltlAlert.Text = "alert('售后服务操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidAfterSaleService.Value = "1";
            hidAfterSaleServiceBy.Value = Session["uID"].ToString();
            hidAfterSaleServiceName.Value = Session["uName"].ToString();
            if (hidFinance.Value == "1")
            {
                InsertPayment();
            }
            BindInfo();
        }
    }
    private void InsertPayment()
    {
        if (!InsertEdiHrdByPayment())
        {
            ltlAlert.Text = "alert('EDI(头栏)操作失败，请联系管理员'); ";
            return;
        }
        if (!InsertEdiDetByPayment())
        {
            ltlAlert.Text = "alert('EDI(明细)操作失败，请联系管理员'); ";
            return;
        }
    }
    private bool InsertEdiHrdByPayment()
    {
        string str = "sp_CustComp_InsertEdiPdHrdByPayment";
        SqlParameter param = new SqlParameter("@no" , hidNo.Value.ToString());
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool InsertEdiDetByPayment()
    {
        string str = "sp_CustComp_InsertEdiPdDetByPayment";
        SqlParameter param = new SqlParameter("@no", hidNo.Value.ToString());
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnAfterSaleServiceNo_Click(object sender, EventArgs e)
    {
        if(!UpdateStatus("AfterSaleService","2"))
        {
            ltlAlert.Text = "alert('售后服务操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidAfterSaleService.Value = "2";
            hidAfterSaleServiceBy.Value = Session["uID"].ToString();
            hidAfterSaleServiceName.Value = Session["uName"].ToString();
            BindInfo();
            #region 驳回后向创建者发送邮件
            string Topical = "客户投诉单：" + labCustCompNo.Text;
            string mto = GetUserEmailTo("1", "create", labCustCompNo.Text);
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   质量判定已对客户投诉单号：" + labCustCompNo.Text + "进行驳回，请重新修改此客户投诉单<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(Session["email"].ToString(), mto, Topical, body);
            #endregion
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        txtResponsiblePerson.Enabled = true;
        btnSave.Visible = true;
        btnEdit.Visible = false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        txtResponsiblePerson.Enabled = false;
        btnSave.Visible = false;
        btnEdit.Visible = true;
        if (!string.IsNullOrEmpty(txtResponsiblePerson.Text))
        {
            if (!UpdateRePerAndMoney())
            {
                ltlAlert.Text = "alert('责任人操作失败，请联系管理员'); ";
                return;
            }
        }
    }
    private bool UpdateRePerAndMoney()
    {
        string str = "sp_CustComp_UpdateRePerAndMoney";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@id", hidID.Value.ToString());
        param[1] = new SqlParameter("@responsiblePerson", txtResponsiblePerson.Text.Trim());
        param[2] = new SqlParameter("@uID", Session["uID"].ToString());
        param[3] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnFinsh_Click(object sender, EventArgs e)
    {
        if (!UpdateFinshStatus())
        {
            ltlAlert.Text = "alert('完结操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            BindInfo();
            #region 发送邮件给客户投诉单所有相关联的人
            string Topical = "客户投诉单：" + labCustCompNo.Text;
            string mto = GetUserEmailTo("0", "all", labCustCompNo.Text);
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   客户投诉单：" + hidNo.Value.ToString() + ",已完结" + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(Session["email"].ToString(), mto, Topical, body);
            #endregion
        }
    }
    private bool UpdateFinshStatus()
    {
        string str = "sp_CustComp_UpdateFinshStatus";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@id", hidID.Value.ToString());
        param[1] = new SqlParameter("@uID", Session["uID"].ToString());
        param[2] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }

    private void BindPart()
    {
        DataTable dtPart = getPart();
        gvPart.DataSource = dtPart;
        gvPart.DataBind();
    }
    private void BindGoods()
    {
        DataTable dtGoods = getGoods();
        gvGoods.DataSource = dtGoods;
        gvGoods.DataBind();
    }
    private void BindSID()
    {
        DataTable dtPart = getSID();
        gvSID.DataSource = dtPart;
        gvSID.DataBind();
    }
    private DataTable getSID()
    {
        string sql = "Select SID_nbr,sid_po,sid_so_line,sid_qad,SID_qty_set,smstr.SID_shipdate";
        sql += " From CustComp_PaymentDet cd";
        sql += " left join SID_det sdet on sdet.sid_po = cd.CustComp_No And sdet.sid_qad = cd.Payment_Part";
        sql += " left join SID_mstr smstr on smstr.SID_id = sdet.SID_id";
        sql += " Where cd.CustComp_No = '" + hidNo.Value.ToString() + "'";
        sql += "    And cd.Payment_Type = 2";
        sql += "    And smstr.SID_shipdate < getdate()";
        sql += "    And smstr.SID_shipdate is not null";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }

    private DataTable getSIDExcel()
    {
        string sql = "Select SID_nbr,sid_so_line,sid_qad,SID_qty_set,smstr.SID_shipdate";
        sql += " From CustComp_PaymentDet cd";
        sql += " left join SID_det sdet on sdet.sid_po = cd.CustComp_No And sdet.sid_qad = cd.Payment_Part";
        sql += " left join SID_mstr smstr on smstr.SID_id = sdet.SID_id";
        sql += " Where cd.CustComp_No = '" + hidNo.Value.ToString() + "'";
        sql += "    And cd.Payment_Type = 2";
        sql += "    And smstr.SID_shipdate < getdate()";
        sql += "    And smstr.SID_shipdate is not null";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }
    private DataTable getPart()
    {
        string sql = "Select ID,CustComp_No,Payment_Type,Payment_Money,Payment_Line,Payment_Part,";
        sql += "Payment_Description,Payment_Qty,Payment_Price,Payment_Total,";
        sql += "Convert(varchar(10), Payment_DetReqDate, 120) Payment_DetReqDate,";
        sql += "Convert(varchar(10), Payment_DetDueDate, 120) Payment_DetDueDate,createBy,";
        sql += "createName,createDate,modifyBy,modifyName,modifyDate,poLine,SID_Site ";
        sql += "From CustComp_PaymentDet ";
        sql += "Where PayMent_Type = 2 And CustComp_No = '" + hidNo.Value.ToString() + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }

    private DataTable getParteExcel()
    {
        string sql = "Select poLine,SID_Site,Payment_Part,Payment_Description,Payment_Qty,";
        sql += "Payment_Price,Payment_Total,";
        sql += "Convert(varchar(10), Payment_DetReqDate, 120) Payment_DetReqDate,";
        sql += "Convert(varchar(10), Payment_DetDueDate, 120) Payment_DetDueDate ";
        sql += "From CustComp_PaymentDet ";
        sql += "Where PayMent_Type = 2 And CustComp_No = '" + hidNo.Value.ToString() + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }

    private DataTable getGoods()
    {
        string sql = "Select ID,CustComp_No,Payment_Type,Payment_Money,Payment_Line,Payment_Part,";
        sql += "Payment_Description,Payment_Qty,Payment_Price,Payment_Total,";
        sql += "Convert(varchar(10), Payment_DetReqDate, 120) Payment_DetReqDate,";
        sql += "Convert(varchar(10), Payment_DetDueDate, 120) Payment_DetDueDate,createBy,";
        sql += "createName,createDate,modifyBy,modifyName,modifyDate,poLine,SID_Site,Payment_DateCode ";
        sql += "From CustComp_PaymentDet ";
        sql += "Where PayMent_Type = 3 And CustComp_No = '" + hidNo.Value.ToString() + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }

    private DataTable getGoodsExcel()
    {
        string sql = "Select poLine,SID_Site,Payment_Part, ";
        sql += "Payment_Description,Payment_Qty,Payment_Price,Payment_Total,";
        sql += "Payment_DateCode ";
        sql += "From CustComp_PaymentDet ";
        sql += "Where PayMent_Type = 3 And CustComp_No = '" + hidNo.Value.ToString() + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }

   
    protected void btnEdit1_Click(object sender, EventArgs e)
    {
        ddlFactory.Enabled = true;
        btnEdit1.Visible = false;
        btnSave1.Visible = true;
    }
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        ddlFactory.Enabled = false;
        btnEdit1.Visible = true;
        btnSave1.Visible = false;
        if (!UpdateFactory())
        {
            ltlAlert.Text = "alert('工厂操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidFactory.Value = ddlFactory.SelectedValue.ToString();

            #region 发送邮件给相关工厂负责人
            string Topical = "客户投诉单：" + labCustCompNo.Text;
            string mto = GetUserEmailTo(hidFactory.Value.ToString(), "single", labCustCompNo.Text);
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   投诉单：" + labCustCompNo.Text + ",经过审核已确认是您工厂责任，请进行相应的操作" + "<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(Session["email"].ToString(), mto, Topical, body);
            #endregion
        }
        BindInfo();
    }
    private bool UpdateFactory()
    {
        string str = "sp_CustComp_UpdateFactory";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@id", hidID.Value.ToString());
        param[1] = new SqlParameter("@factory", ddlFactory.SelectedValue.ToString());
        param[2] = new SqlParameter("@uID", Session["uID"].ToString());
        param[3] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnDeptYes_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("DeptStatus", "1"))
        {
            ltlAlert.Text = "alert('部门主管操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidDeptStatus.Value = "1";
            hidDeptStatusBy.Value = Session["uID"].ToString();
            hidDeptStatusName.Value = Session["uName"].ToString();
            BindInfo();
        }
        #region 部门主管同意后发送邮件给财务和质量判定
        string Topical = "客户投诉单：" + labCustCompNo.Text;
        string mto = GetUserEmailTo("1", "single", labCustCompNo.Text);
        mto += GetUserEmailTo("2", "single", labCustCompNo.Text);
        string body = "<html>";
        body += "<body>";
        body += "<form>";
        body += "<br>";
        body += "您好:" + "<br>";
        body += "   有一张新的客户投诉单已通过部门主管审核，现需要您进行相应的操作，客户投诉单号：" + labCustCompNo.Text + "<br>";
        body += "<br>";
        body += "</body>";
        body += "</form>";
        body += "</html>";
        SendEmail222(Session["email"].ToString(), mto, Topical, body);
        #endregion
    }
    private string GetUserEmailTo(string procid,string type, string no)
    {
        string str = "sp_CustComp_SelectEmailTo";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@procid", procid);
        param[1] = new SqlParameter("@type", type);
        param[2] = new SqlParameter("@no", no);

        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    public static bool SendEmail222(string from, string to, string subject, string body)
    {
        try
        {
            //string[] uploadAtts = uploadAtt.Split(';');
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

            //if (!string.IsNullOrEmpty(copy))
            //{
            //    foreach (string _cc in copy.Split(';'))
            //    {
            //        if (!string.IsNullOrEmpty(_cc))
            //        {
            //            MailAddress _mailCopy = new MailAddress(_cc);
            //            _mailMessage.CC.Add(_mailCopy);
            //        }
            //    }
            //}
            _mailMessage.Subject = subject;
            _mailMessage.BodyEncoding = Encoding.GetEncoding("GB2312");
            _mailMessage.Priority = MailPriority.Normal;
            _mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            //string extention = null;
            //Dictionary<string, string> imgUrl = MailHelper.GetImgSrcAndCid(body);
            //if (imgUrl.Count > 0 && imgUrl != null)
            //{
            //    foreach (KeyValuePair<string, string> kp in imgUrl)
            //    {
            //        body = body.Replace(kp.Key, "cid:" + kp.Value);
            //    }
            //}
            AlternateView htmlBody = AlternateView.CreateAlternateViewFromString(body, Encoding.GetEncoding("GB2312"), "text/html");
            //if (imgUrl.Count > 0 && imgUrl != null)
            //{
            //    foreach (KeyValuePair<string, string> kp in imgUrl)
            //    {
            //        int j = kp.Key.LastIndexOf(".");
            //        extention = kp.Key.Substring(j + 1);
            //        LinkedResource lrImage;
            //        try
            //        {
            //            lrImage = new LinkedResource(imgPath.Replace("\\", "/") + kp.Key, "image/" + extention);
            //        }
            //        catch (Exception e)
            //        {
            //            try
            //            {
            //                lrImage = new LinkedResource(kp.Key);
            //            }
            //            catch
            //            {
            //                string _imgPath = @"D:\tcpcnew\";
            //                lrImage = new LinkedResource(_imgPath + "JULIA/" + kp.Key);
            //            }
            //        }
            //        lrImage.ContentId = kp.Value;
            //        htmlBody.LinkedResources.Add(lrImage);
            //    }
            //}
            _mailMessage.AlternateViews.Add(htmlBody);
            //foreach (string attss in uploadAtts)
            //{
            //    if (attss.Trim() != string.Empty)
            //    {
            //        int index = attss.Trim().LastIndexOf("/");
            //        Attachment newAtt = new Attachment(attss.Trim());
            //        newAtt.Name = attss.Trim().Substring(index + 1);
            //        _mailMessage.Attachments.Add(newAtt);
            //    }
            //}
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
    protected void btnDeptNo_Click(object sender, EventArgs e)
    {
        if (!UpdateStatus("DeptStatus", "2"))
        {
            ltlAlert.Text = "alert('部门主管操作失败，请联系管理员'); ";
            return;
        }
        else
        {
            hidDeptStatus.Value = "2";
            hidDeptStatusBy.Value = Session["uID"].ToString();
            hidDeptStatusName.Value = Session["uName"].ToString();
            BindInfo();
            #region 驳回后向创建者发送邮件
            string Topical = "客户投诉单：" + labCustCompNo.Text;
            string mto = GetUserEmailTo("1", "create", labCustCompNo.Text);
            string body = "<html>";
            body += "<body>";
            body += "<form>";
            body += "<br>";
            body += "您好:" + "<br>";
            body += "   部门主管已对客户投诉单号：" + labCustCompNo.Text + "进行驳回，请重新修改此客户投诉单<br>";
            body += "<br>";
            body += "</body>";
            body += "</form>";
            body += "</html>";
            SendEmail222(Session["email"].ToString(), mto, Topical, body);
            #endregion
        }
    }
    protected void btnSidEdit_Click(object sender, EventArgs e)
    {
        txtSidMoney.Enabled = true;
        btnSidEdit.Visible = false;
        btnSidSave.Visible = true;
    }
    protected void btnSidSave_Click(object sender, EventArgs e)
    {
        txtSidMoney.Enabled = false;
        btnSidEdit.Visible = true;
        btnSidSave.Visible = false;
        if (!UpdateSidMoney())
        {
            ltlAlert.Text = "alert('出运费用保存失败，请联系管理员'); ";
            return;
        }
        else
        {
            BindInfo();
        }
    }
    private bool UpdateSidMoney()
    {
        string str = "sp_CustComp_UpdateSidMoney";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@no", hidNo.Value.ToString());
        param[1] = new SqlParameter("@money", txtSidMoney.Text);
        param[2] = new SqlParameter("@payMoney", labPaymentTotal.Text.Substring(0, labPaymentTotal.Text.IndexOf("(")));

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }

    /// <summary>
    /// <br/> 变char（10）
    /// nbsp;变为空格
    /// </summary>
    /// <param name="south"></param>
    /// <returns></returns>
    private string replaceAllBrOrnbsp(string south)
    {
        south = south.Replace("<br />", Environment.NewLine);
        south = south.Replace("&nbsp;"," ");

        return south;
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {

        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");



        for (int i = 0; i <= 12; i++)
        {
            sheet.SetColumnWidth(i, 20 * 256);
        }

        int editRow = 0;
        int editCell = 0;

        int endRowLeft0 = 0;
        int endRowLeft1 = 0;
        int endRowLeft2 = 0;
        int beginRowLeft0 = 0;
        int beginRowLeft1 = 0;
        int beginRowLeft2 = 0;

        ICellStyle cellstyleLeft = workbook.CreateCellStyle();

        cellstyleLeft.WrapText = true;

        cellstyleLeft.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        cellstyleLeft.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
        
        cellstyleLeft.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        //cellstyleLeft.FillPattern = FillPattern.SolidForeground;

        cellstyleLeft.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        cellstyleLeft.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        cellstyleLeft.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        cellstyleLeft.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        cellstyleLeft.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        cellstyleLeft.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        cellstyleLeft.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        cellstyleLeft.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;


        IFont fontHeader = workbook.CreateFont();
        fontHeader.FontHeightInPoints = (short)(sheet.DefaultRowHeight /20);
        cellstyleLeft.SetFont(fontHeader);

        ICellStyle cellstyle2 = workbook.CreateCellStyle();

        cellstyle2.WrapText = true;

        cellstyle2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
        cellstyle2.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

        cellstyle2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        //cellstyleLeft.FillPattern = FillPattern.SolidForeground;

        cellstyle2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        cellstyle2.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        cellstyle2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        cellstyle2.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        cellstyle2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        cellstyle2.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        cellstyle2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        cellstyle2.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        cellstyle2.SetFont(fontHeader);

        ICell cellNull; 

        #region 第一行
        IRow row0 = sheet.CreateRow(editRow);
        ICell cell00 = row0.CreateCell(0);

        cell00.CellStyle = cellstyleLeft;
        cell00.SetCellValue("投诉单信息");

        beginRowLeft0 = 0;


        ICell cell01 = row0.CreateCell(1);

        cell01.CellStyle = cellstyleLeft;
        cell01.SetCellValue("投诉单号");

        ICell cell02 = row0.CreateCell(2);

        cell02.CellStyle = cellstyleLeft;
        cell02.SetCellValue(labCustCompNo.Text);

        ICell cell03 = row0.CreateCell(3);

        cell03.CellStyle = cellstyleLeft;
        cell03.SetCellValue("客户代码");

        ICell cell04 = row0.CreateCell(4);

        cell04.CellStyle = cellstyleLeft;
        cell04.SetCellValue(labCust.Text);

        ICell cell05 = row0.CreateCell(5);

        cell05.CellStyle = cellstyleLeft;
        cell05.SetCellValue("客户名称");

        ICell cell06 = row0.CreateCell(6);

        cell06.CellStyle = cellstyleLeft;
        cell06.SetCellValue(labCustName.Text);

        for (int i = 7; i <= 11; i++)
        {
            cellNull = row0.CreateCell(i);
            cellNull.CellStyle = cellstyleLeft;
        }

        endRowLeft0 += 1;
        editRow += 1;
        #endregion 

        #region 第二行
        IRow row1 = sheet.CreateRow(1);

        cellNull = row1.CreateCell(0);
        cellNull.CellStyle = cellstyleLeft;



        ICell cell11 = row1.CreateCell(1);

        cell11.CellStyle = cellstyleLeft;
        cell11.SetCellValue("原订单");

        ICell cell12 = row1.CreateCell(2);

        cell12.CellStyle = cellstyleLeft;
        cell12.SetCellValue(labOrder.Text);

        ICell cell13 = row1.CreateCell(3);

        cell13.CellStyle = cellstyleLeft;
        cell13.SetCellValue("Due Date");

        ICell cell14 = row1.CreateCell(4);

        cell14.CellStyle = cellstyleLeft;
        cell14.SetCellValue(labDueDate.Text);

        ICell cell15 = row1.CreateCell(5);

        cell15.CellStyle = cellstyleLeft;
        cell15.SetCellValue("Date Code");

        ICell cell16 = row1.CreateCell(6);

        cell16.CellStyle = cellstyleLeft;
        cell16.SetCellValue(labReqDate.Text);

        for (int i = 7; i <= 11; i++)
        {
            cellNull = row1.CreateCell(i);
            cellNull.CellStyle = cellstyleLeft;
        }

        endRowLeft0 += 1;
        editRow += 1;
        #endregion

        #region 第三行
        IRow row3 = sheet.CreateRow(2);

        cellNull = row3.CreateCell(0);
        cellNull.CellStyle = cellstyleLeft;

        ICell cell21 = row3.CreateCell(1);

        cell21.CellStyle = cellstyleLeft;
        cell21.SetCellValue("问题描述");



        sheet.AddMergedRegion(new CellRangeAddress(2, 2, 2, 6));

        ICell cell22 = row3.CreateCell(2);

        cell22.CellStyle = cellstyleLeft;
        cell22.SetCellValue(labDescribe.Text);

        

        for (int i = 3; i <= 11; i++)
        {
            ICell cell23 = row3.CreateCell(i);
            cell23.CellStyle = cellstyleLeft;
        }

        int length = Encoding.UTF8.GetBytes(cell22.ToString()).Length;
        row3.HeightInPoints = 20 * (length / 60 + 1);  



        endRowLeft0 += 1;
        editRow += 1;
        #endregion

        //#region 第四行附件
        //IRow row4 = sheet.CreateRow(3);
        //ICell cell31 = row4.CreateCell(1);

        //cell31.CellStyle = cellstyleLeft;
        //cell31.SetCellValue("附件");

        //sheet.AddMergedRegion(new CellRangeAddress(3, 3, 2, 6));

        //ICell cell32 = row4.CreateCell(2);

        //cell32.CellStyle = cellstyleLeft;
        //cell32.SetCellValue(tdFile.InnerText);


        

        //endRowLeft0 += 1;
        //#endregion

        #region 第四行
        IRow row4 = sheet.CreateRow(3);


        cellNull = row4.CreateCell(0);
        cellNull.CellStyle = cellstyleLeft;
        cellNull = row4.CreateCell(1);
        cellNull.CellStyle = cellstyleLeft;

        beginRowLeft1 = 3;
        ICell cell31 = row4.CreateCell(1);

        cell31.CellStyle = cellstyleLeft;
        cell31.SetCellValue("赔付明细");


        ICell cell32 = row4.CreateCell(2);

        cell32.CellStyle = cellstyleLeft;
        cell32.SetCellValue("1、赔款");


        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 3, 8));


        ICell cell33 = row4.CreateCell(3);

        cell33.CellStyle = cellstyleLeft;
        cell33.SetCellValue(labMoney.Text);


        for (int i = 3; i <= 11; i++)
        {
            cellNull = row4.CreateCell(i);
            cellNull.CellStyle = cellstyleLeft;
        }

        endRowLeft0 += 1;
        endRowLeft1 =  beginRowLeft1 ;
        editRow += 1;
        #endregion

        #region 第五行 表头行

        beginRowLeft2 = 4;
        IRow row5 = sheet.CreateRow(4);

        cellNull = row5.CreateCell(0);
        cellNull.CellStyle = cellstyleLeft;
        cellNull = row5.CreateCell(1);
        cellNull.CellStyle = cellstyleLeft;
        cellNull = row5.CreateCell(11);
        cellNull.CellStyle = cellstyleLeft;

        ICell cell42 = row5.CreateCell(2);

        cell42.CellStyle = cellstyleLeft;
        cell42.SetCellValue("2、赔料");

        ICell cell43 = row5.CreateCell(3);

        cell43.CellStyle = cellstyleLeft;
        cell43.SetCellValue("原订单行号");

        ICell cell44 = row5.CreateCell(4);

        cell44.CellStyle = cellstyleLeft;
        cell44.SetCellValue("原出运地");

        ICell cell45 = row5.CreateCell(5);

        cell45.CellStyle = cellstyleLeft;
        cell45.SetCellValue("物料号");

        ICell cell46 = row5.CreateCell(6);

        cell46.CellStyle = cellstyleLeft;
        cell46.SetCellValue("描述");

        ICell cell47 = row5.CreateCell(7);

        cell47.CellStyle = cellstyleLeft;
        cell47.SetCellValue("数量");

        ICell cell48 = row5.CreateCell(8);

        cell48.CellStyle = cellstyleLeft;
        cell48.SetCellValue("单价");

        ICell cell49 = row5.CreateCell(9);

        cell49.CellStyle = cellstyleLeft;
        cell49.SetCellValue("共计");

        ICell cell410 = row5.CreateCell(10);

        cell410.CellStyle = cellstyleLeft;
        cell410.SetCellValue("Req Date");

        ICell cell411 = row5.CreateCell(11);

        cell411.CellStyle = cellstyleLeft;
        cell411.SetCellValue("Due Date");


        endRowLeft0 += 1;
        endRowLeft1 += 1;
        endRowLeft2 = beginRowLeft2;
        editRow += 1;
        #endregion

        #region 第六行
        
        //从第三格开始
        DataTable dtPart = getParteExcel();

        for (int i = 0; i < dtPart.Rows.Count; i++)//row
        { 
            IRow row6 = sheet.CreateRow(5+i);

            cellNull = row6.CreateCell(0);
            cellNull.CellStyle = cellstyleLeft;
            cellNull = row6.CreateCell(1);
            cellNull.CellStyle = cellstyleLeft;
            cellNull = row6.CreateCell(2);
            cellNull.CellStyle = cellstyleLeft;
            cellNull = row6.CreateCell(11);
            cellNull.CellStyle = cellstyleLeft;
            for(int j = 0;j<dtPart.Columns.Count;j++)
            {
                
                ICell cell = row6.CreateCell(j+3);

                cell.CellStyle = cellstyleLeft;
                cell.SetCellValue(dtPart.Rows[i][j].ToString());
                  
            }
            endRowLeft0 += 1;
            endRowLeft1 += 1;
            endRowLeft2 += 1;
            editRow += 1;
        
        }


        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft2, endRowLeft2, 2, 2));

        beginRowLeft2 = 0;
        endRowLeft2 = 0;
        #endregion

        #region 第七行 表头
        IRow row7 = sheet.CreateRow(editRow);

        cellNull = row7.CreateCell(0);
        cellNull.CellStyle = cellstyleLeft;
        cellNull = row7.CreateCell(1);
        cellNull.CellStyle = cellstyleLeft;
        cellNull = row7.CreateCell(10);
        cellNull.CellStyle = cellstyleLeft;
        cellNull = row7.CreateCell(11);
        cellNull.CellStyle = cellstyleLeft;

        editCell = 2;

        ICell cell72 = row7.CreateCell(editCell);
        cell72.CellStyle = cellstyleLeft;
        cell72.SetCellValue("3、退换货");
        editCell += 1;


        ICell cell73 = row7.CreateCell(editCell);
        cell73.CellStyle = cellstyleLeft;
        cell73.SetCellValue("原订单行号");
        editCell += 1;

        ICell cell74 = row7.CreateCell(editCell);
        cell74.CellStyle = cellstyleLeft;
        cell74.SetCellValue("原出运地");
        editCell += 1;

        ICell cell75 = row7.CreateCell(editCell);
        cell75.CellStyle = cellstyleLeft;
        cell75.SetCellValue("物料号");
        editCell += 1;

        ICell cell76 = row7.CreateCell(editCell);
        cell76.CellStyle = cellstyleLeft;
        cell76.SetCellValue("描述");
        editCell += 1;

        ICell cell77 = row7.CreateCell(editCell);
        cell77.CellStyle = cellstyleLeft;
        cell77.SetCellValue("数量");
        editCell += 1;

        ICell cell78 = row7.CreateCell(editCell);
        cell78.CellStyle = cellstyleLeft;
        cell78.SetCellValue("单价");
        editCell += 1;

        ICell cell79 = row7.CreateCell(editCell);
        cell79.CellStyle = cellstyleLeft;
        cell79.SetCellValue("共计");
        editCell += 1;

        


        endRowLeft0 += 1;
        endRowLeft1 += 1;
        beginRowLeft2 = editRow;
        endRowLeft2 = beginRowLeft2;
        editRow += 1;
        editCell = 0;

        #endregion
        #region 第八行 表数据

        //从第三格开始
        DataTable dtGoods = getGoodsExcel();

        for (int i = 0; i < dtGoods.Rows.Count; i++)//row
        {
            IRow row8 = sheet.CreateRow(editRow + i);

            cellNull = row8.CreateCell(0);
            cellNull.CellStyle = cellstyleLeft;
            cellNull = row8.CreateCell(1);
            cellNull.CellStyle = cellstyleLeft;
            cellNull = row8.CreateCell(10);
            cellNull.CellStyle = cellstyleLeft;
            cellNull = row8.CreateCell(11);
            cellNull.CellStyle = cellstyleLeft;

            for (int j = 0; j < dtGoods.Columns.Count; j++)
            {

                ICell cell = row8.CreateCell(j + 3);

                cell.CellStyle = cellstyleLeft;
                cell.SetCellValue(dtGoods.Rows[i][j].ToString());

            }
            endRowLeft0 += 1;
            endRowLeft1 += 1;
            endRowLeft2 += 1;
            editRow += 1;

        }


        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft2, endRowLeft2, 2, 2));

        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft1, endRowLeft1, 1, 1));

        beginRowLeft2 = 0;
        endRowLeft2 = 0;
        beginRowLeft1 = 0;
        endRowLeft1 = 0;
        #endregion

        #region 第九行 赔付统计
        IRow row9 = sheet.CreateRow(editRow);

        cellNull = row9.CreateCell(0);
        cellNull.CellStyle = cellstyleLeft;

        

            editCell = 1;

        ICell cell91 = row9.CreateCell(editCell);
        cell91.CellStyle = cellstyleLeft;
        cell91.SetCellValue("赔付统计");
        editCell += 1;


        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 2, 6));

        ICellStyle cellRed = workbook.CreateCellStyle();
        cellRed.CloneStyleFrom(cellstyleLeft);

        IFont font = workbook.CreateFont();
        font.FontHeightInPoints = (short)(sheet.DefaultRowHeight /20);
        font.Color=NPOI.HSSF.Util.HSSFColor.Red.Index;
        cellRed.SetFont(font);

        ICell cell92 = row9.CreateCell(editCell);

        cell92.CellStyle = cellRed;
        cell92.SetCellValue(labPaymentTotal.Text);
        editCell++;

        for (int i = editCell; i <= 11; i++)
        {
            cellNull = row9.CreateCell(i);
            cellNull.CellStyle = cellRed;
        }

        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 2, 6));

        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft0, endRowLeft0, 0, 0));//第一格和最后一格合并

        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft0, endRowLeft0, 11, 11));

        editRow += 1;
        beginRowLeft0 = 0;
        endRowLeft0 = 0;
        #endregion

        #region 第十行 部门主管
      

        IRow row10 = sheet.CreateRow(editRow);
        editCell = 0;

        ICell cell101 = row10.CreateCell(editCell);
        cell101.CellStyle = cellstyleLeft;
        cell101.SetCellValue("部门主管");
        editCell += 1;

        string Deptmassage = tdDept.InnerHtml;

        if(hidDeptStatus.Value == "1")
        {
            Deptmassage += "----同意(" + hidDeptStatusName.Value.ToString() + ")";
        }
        else if(hidDeptStatus.Value == "2")
        {
            Deptmassage += "----拒绝(" + hidDeptStatusName.Value.ToString() + ")";
        }

     

        ICell cell102 = row10.CreateCell(editCell);
        cell102.CellStyle = cellstyle2;
        cell102.SetCellValue(replaceAllBrOrnbsp(Deptmassage));
        editCell++;
        


        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 1, 10));

        for (; editCell <= 10; editCell++)
        {
            cellNull = row10.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;

        }

        ICell cell1011 = row10.CreateCell(11);
        cell1011.CellStyle = cellstyleLeft;
        cell1011.SetCellValue("");
        editCell += 1;


        length = Encoding.UTF8.GetBytes(cell102.ToString()).Length;
        row10.HeightInPoints = 20 * (length / 60 + 1);  

        editRow += 1;
        #endregion

        #region 第十一行 讨论
        IRow row11 = sheet.CreateRow(editRow);
        editCell = 0;

        ICell cell111 = row11.CreateCell(editCell);
        cell111.CellStyle = cellstyleLeft;
        cell111.SetCellValue("讨论");
        editCell += 1;

        ICell cell112 = row11.CreateCell(editCell);
        cell112.CellStyle = cellstyle2;
        cell112.SetCellValue(replaceAllBrOrnbsp(tdTalk.InnerHtml));
        editCell += 1;

        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 1, 10));

        for (; editCell <= 10; editCell++)
        {
            cellNull = row11.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;

        }


        ICell cell1111 = row11.CreateCell(editCell);
        cell1111.CellStyle = cellstyleLeft;
        cell1111.SetCellValue(replaceAllBrOrnbsp(labTalk.Text));


        length = Encoding.UTF8.GetBytes(cell112.ToString()).Length;
        row11.HeightInPoints = 20 * (length / 60 + 1);  
        editRow += 1;
        
        #endregion

        #region 第十二行 财务

        IRow row12 = sheet.CreateRow(editRow);
        editCell = 0;

        ICell cell121 = row12.CreateCell(editCell);
        cell121.CellStyle = cellstyleLeft;
        cell121.SetCellValue("财务");
        editCell += 1;



        string Financemassage = tdFinance.InnerHtml;

        if (hidFinance.Value == "1")
        {
            Financemassage += "----同意(" + hidFinanceName.Value.ToString() + ")";
        }
        else if (hidFinance.Value == "2")
        {
            Financemassage += "----拒绝(" + hidFinanceName.Value.ToString() + ")";
        }


        ICell cell122 = row12.CreateCell(editCell);
        cell122.CellStyle = cellstyle2;
        cell122.SetCellValue(replaceAllBrOrnbsp(Financemassage));
        editCell++;

        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 1, 10));


        

        for (; editCell <= 10; editCell++)
        {
            cellNull = row12.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;

        }

        ICell cell1211 = row12.CreateCell(11);
        cell1211.CellStyle = cellstyleLeft;
        cell1211.SetCellValue(replaceAllBrOrnbsp(labFinance.Text));
        editCell += 1;

        length = Encoding.UTF8.GetBytes(cell122.ToString()).Length;
        row12.HeightInPoints = 20 * (length / 60 + 1);  

        editRow += 1;
        #endregion

        #region 第十三行 质量判定
        IRow row13 = sheet.CreateRow(editRow);
        editCell = 0;

        beginRowLeft0 = editRow;

        ICell cell131 = row13.CreateCell(editCell);
        cell131.CellStyle = cellstyleLeft;
        cell131.SetCellValue("质量判定");
        editCell += 1;

        ICell cell132 = row13.CreateCell(editCell);
        cell132.CellStyle = cellstyleLeft;
        cell132.SetCellValue("工厂：  " + ddlFactory.SelectedItem.Text);
        editCell += 1;

        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 1, 10));

        for (; editCell <= 10; editCell++)
        {
            cellNull = row13.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;

        }

        ICell cell1311 = row13.CreateCell(11);
        cell1311.CellStyle = cellstyleLeft;
        cell1311.SetCellValue(replaceAllBrOrnbsp(labAfterSaleService.Text));
        editCell += 1;

       


        editRow += 1;
        endRowLeft0 = beginRowLeft0 +1;
        #endregion



        #region 第十四行 质量判定 详情
        IRow row14 = sheet.CreateRow(editRow);
        editCell = 1;
        cellNull = row14.CreateCell(0);
        cellNull.CellStyle = cellstyleLeft;

        string AfterSaleServicemassage = tdAfterSaleService.InnerHtml;

        if (hidAfterSaleService.Value == "1")
        {
            AfterSaleServicemassage += "----同意(" + hidAfterSaleServiceName.Value.ToString() + ")";
        }
        else if (hidAfterSaleService.Value == "2")
        {
            AfterSaleServicemassage += "----拒绝(" + hidAfterSaleServiceName.Value.ToString() + ")";
        }

        ICell cell142 = row14.CreateCell(editCell);
        cell142.CellStyle = cellstyle2;
        cell142.SetCellValue(replaceAllBrOrnbsp(AfterSaleServicemassage));
        editCell++;

        for (; editCell <= 11; editCell++)
        {
            cellNull = row14.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;

        }

        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 1, 10));

        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft0, endRowLeft0, 0, 0));
        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft0, endRowLeft0, 11, 11));


        beginRowLeft0 = 0;
        endRowLeft0 = 0;
        editRow += 1;
        #endregion

        

        #region 第十五行 处理方案
        IRow row15 = sheet.CreateRow(editRow);
        editCell = 0;

        ICell cell151 = row15.CreateCell(editCell);
        cell151.CellStyle = cellstyleLeft;
        cell151.SetCellValue("处理方案");
        editCell += 1;

        ICell cell152 = row15.CreateCell(editCell);
        cell152.CellStyle = cellstyle2;
        cell152.SetCellValue(replaceAllBrOrnbsp(tdOpinion.InnerHtml));
        editCell += 1;

        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 1, 10));

        for (; editCell <= 10; editCell++)
        {
            cellNull = row15.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;

        }


        ICell cell1511 = row15.CreateCell(11);
        cell1511.CellStyle = cellstyleLeft;
        cell1511.SetCellValue(replaceAllBrOrnbsp(labOpinion.Text));


        length = Encoding.UTF8.GetBytes(cell152.ToString()).Length;
        row15.HeightInPoints = 20 * (length / 60 + 1);  

        editRow += 1;

        #endregion

        #region 第十六行 出运明细

        IRow row16 = sheet.CreateRow(editRow);
        editCell = 0;


        ICell cell161 = row16.CreateCell(editCell);
        cell161.CellStyle = cellstyleLeft;
        cell161.SetCellValue("出运明细");
        editCell += 1;

        ICell cell162 = row16.CreateCell(editCell);
        cell162.CellStyle = cellstyleLeft;
        cell162.SetCellValue("出运费用");
        editCell += 1;

        ICell cell163 = row16.CreateCell(editCell);
        cell163.CellStyle = cellstyleLeft;
        cell163.SetCellValue(txtSidMoney.Text);
        editCell += 1;

        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 3, 10));

        for (; editCell <= 10; editCell++)
        {
            cellNull = row16.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;

        }


        ICell cell1611 = row16.CreateCell(11);
        cell1611.CellStyle = cellstyleLeft;
        cell1611.SetCellValue("");

        beginRowLeft0 = editRow;
        endRowLeft0 = beginRowLeft0;
        editRow += 1;

        #endregion

        #region 第十七行 出运明细 表头
        IRow row17 = sheet.CreateRow(editRow);
        editCell = 1;

        cellNull = row17.CreateCell(0);
        cellNull.CellStyle = cellstyleLeft;

        ICell cell171 = row17.CreateCell(editCell);
        cell171.CellStyle = cellstyleLeft;
        cell171.SetCellValue("出运单号");
        editCell += 1;

        ICell cell172 = row17.CreateCell(editCell);
        cell172.CellStyle = cellstyleLeft;
        cell172.SetCellValue("行号");
        editCell += 1;

        ICell cell173 = row17.CreateCell(editCell);
        cell173.CellStyle = cellstyleLeft;
        cell173.SetCellValue("物料号");
        editCell += 1;

        ICell cell174 = row17.CreateCell(editCell);
        cell174.CellStyle = cellstyleLeft;
        cell174.SetCellValue("出运数量");
        editCell += 1;

        ICell cell175 = row17.CreateCell(editCell);
        cell175.CellStyle = cellstyleLeft;
        cell175.SetCellValue("出运日期");
        editCell += 1;


        for (; editCell <= 11; editCell++)
        {
            cellNull = row17.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;

        }

        endRowLeft0 += 1;
        editRow += 1;

        #endregion

        #region 第十八行 出运明细 明细
        
        editCell = 1;

        DataTable dtSID = getSIDExcel();

        for (int i = 0; i < dtSID.Rows.Count; i++)//row
        {
            IRow row18 = sheet.CreateRow(editRow + i);

            cellNull = row18.CreateCell(0);
            cellNull.CellStyle = cellstyleLeft;

            for (int t = 6; t <= 11; t++)
            {
                cellNull = row18.CreateCell(t);
                cellNull.CellStyle = cellstyleLeft;

            }
            for (int j = 0; j < dtSID.Columns.Count; j++)
            {

                ICell cell = row18.CreateCell(j + 1);

                cell.CellStyle = cellstyleLeft;
                cell.SetCellValue(dtSID.Rows[i][j].ToString());

            }
            endRowLeft0 += 1;
            editRow += 1;

        }

        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft0, endRowLeft0, 0, 0));
        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft0, endRowLeft0, 11, 11));


        #endregion

        #region 第十九行 结案意见
        editCell = 0;
        IRow row19 = sheet.CreateRow(editRow);

        beginRowLeft0 = editRow;

        ICell cell191 = row19.CreateCell(editCell);
        cell191.CellStyle = cellstyleLeft;
        cell191.SetCellValue("结案意见");
        editCell += 1;

        ICell cell192 = row19.CreateCell(editCell);
        cell192.CellStyle = cellstyleLeft;
        cell192.SetCellValue("责任方");
        editCell += 1;

        ICell cell193 = row19.CreateCell(editCell);
        cell193.CellStyle = cellstyleLeft;
        cell193.SetCellValue(txtResponsiblePerson.Text);
        editCell += 1;

        ICell cell194 = row19.CreateCell(editCell);
        cell194.CellStyle = cellstyleLeft;
        cell194.SetCellValue("赔付金额");
        editCell += 1;

        ICell cell195 = row19.CreateCell(editCell);
        cell195.CellStyle = cellstyleLeft;
        cell195.SetCellValue(labPayment.Text);
        editCell += 1;

        for (; editCell <= 10; editCell++)
        {
            cellNull = row19.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;
        }

        ICell cell1911 = row19.CreateCell(11);
        cell1911.CellStyle = cellstyleLeft;
        cell1911.SetCellValue(replaceAllBrOrnbsp(labFinalOpin.Text));

        editRow += 1;

        endRowLeft0 = beginRowLeft0 + 1;

        #endregion

        #region 第十九行 结案意见
        editCell = 1;
        IRow row20 = sheet.CreateRow(editRow);
        cellNull = row20.CreateCell(0);
        cellNull.CellStyle = cellstyleLeft;

        string FinalOpinionmassage = tdFinalOpinion.InnerHtml;

        if (hidStaus.Value.ToString() == "1")
        {
            FinalOpinionmassage += "----完结(" + hidDetModeifyName.Value.ToString() + ")";
        }
       



        ICell cell202 = row20.CreateCell(editCell);
        cell202.CellStyle = cellstyle2;
        cell202.SetCellValue(replaceAllBrOrnbsp(FinalOpinionmassage));
        editCell++;
        for (; editCell <= 11; editCell++)
        {
            cellNull = row20.CreateCell(editCell);
            cellNull.CellStyle = cellstyleLeft;
        }

        sheet.AddMergedRegion(new CellRangeAddress(editRow, editRow, 1, 10));
        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft0, endRowLeft0, 0, 0));
        sheet.AddMergedRegion(new CellRangeAddress(beginRowLeft0, endRowLeft0, 11, 11));

        #endregion


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
}