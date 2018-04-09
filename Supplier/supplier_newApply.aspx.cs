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
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using Microsoft.Web.UI.WebControls;
using RD_WorkFlow;
using System.Text.RegularExpressions;

public partial class Supplier_supplier_newApply : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 权限
            this.Security.Register("120000035", "供应商资质文件填写权限");
            this.Security.Register("120000037", "主管签字权限");
            this.Security.Register("120000038", "总经理签字权限");
            this.Security.Register("120000039", "签署文件-供应商意见权限");
            this.Security.Register("120000040", "签署文件-法务部意见权限");
            this.Security.Register("120000041", "签署文件-财务部意见权限");
            this.Security.Register("120000042", "正式合同文件-上传正式合同权限");
            this.Security.Register("120000043", "验厂意见权限");
            this.Security.Register("120000044", "签署文件-上传文档权限");
            this.Security.Register("120000045", "填写供应商代码权限");
            this.Security.Register("120000046", "供应商资质文件上传文档权限");
            //

            selectEmailAddress();

            if (this.Security["120000035"].isValid)
            {
                hidFileQualificationOpinionSecurity.Value = "1";
            }
            else
            {
                hidFileQualificationOpinionSecurity.Value = "0";
            }
            //主管签字权限
            if (this.Security["120000037"].isValid)
            {
                hidLeaderSecurity.Value = "1";
            }
            else
            {
                hidLeaderSecurity.Value = "0";
            }
            //总经理签字权限
            if (this.Security["120000038"].isValid)
            {
                hidManageSecurity.Value = "1";
            }
            else
            {
                hidManageSecurity.Value = "0";
            }
            //供应商意见及上传文档权限
            if (this.Security["120000039"].isValid)
            {
                hidSuppOpinionSecurity.Value = "1";
            }
            else
            {
                hidSuppOpinionSecurity.Value = "0";
            }
            //法务部意见权限
            if (this.Security["120000040"].isValid)
            {
                hidLawOpinionSecurity.Value = "1";
            }
            else
            {
                hidLawOpinionSecurity.Value = "0";
            }
            //财务部意见权限
            if (this.Security["120000041"].isValid)
            {
                hidFinanceOpinionSecurity.Value = "1";
            }
            else
            {
                hidFinanceOpinionSecurity.Value = "0";
            }
            //上传正式合同权限
            if (this.Security["120000042"].isValid)
            {
                hidFormalFileSecurity.Value = "1";
            }
            else
            {
                hidFormalFileSecurity.Value = "0";
            }
            //验厂意见权限
            if (this.Security["120000043"].isValid)
            {
                hidFISecurity.Value = "1";
            }
            else
            {
                hidFISecurity.Value = "0";
            }
            //签署文件权限
            if (this.Security["120000044"].isValid)
            {
                hidSignFileSecurity.Value = "1";
            }
            else
            {
                hidSignFileSecurity.Value = "0";
            }
            //填写供应商代码权限
            if (this.Security["120000045"].isValid)
            {
                hidSupplierNumSecurity.Value = "1";
            }
            else
            {
                hidSupplierNumSecurity.Value = "0";
            }
            //供应商资质文件上传文档权限
            if (this.Security["120000046"].isValid)
            {
                hidFileQualificationSecurity.Value = "1";
            }
            else
            {
                hidFileQualificationSecurity.Value = "0";
            }
            #endregion

            try
            {
                hidTabIndex.Value = Request.QueryString["index"];
                //hidTabIndex.Value = "0";
            }
            catch (Exception)
            {
            }

            hidUserName.Value = Session["uName"].ToString();
            hidDeptName.Value = GetUserDept(Convert.ToInt32(Session["uID"].ToString()), Convert.ToInt32(Session["PlantCode"].ToString()));

            #region 新增
            if (Request.QueryString["type"] == null || Request.QueryString["type"] == "add")
            {
                if (Session["plantCode"].ToString() == "1")
                {
                    labPlant.Text = "上海强凌电子有限公司 SZX";
                }
                else if (Session["plantCode"].ToString() == "2")
                {
                    labPlant.Text = "镇江强凌电子有限公司 ZQL";
                }
                else if (Session["plantCode"].ToString() == "5")
                {
                    labPlant.Text = "扬州强凌有限公司 YQL";
                }
                else if (Session["plantCode"].ToString() == "8")
                {
                    labPlant.Text = "淮安强陵照明有限公司  HQL";
                }
                labDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                labAPPUserName.Text = Session["uName"].ToString();
                labAPPDeptName.Text = GetUserDept(Convert.ToInt32(Session["uID"].ToString()), Convert.ToInt32(Session["PlantCode"].ToString()));
                //labDeptLeader.Text = GetDeptLeader();
                //BindBroadHeading();
                //BindSubDivision();
                //BindSubMaterial();
                //Label1.Text = selectSupplierNo();
                hidSupplierID.Value = selectSupplierNo();
                InsertFileQualificationDet(hidSupplierID.Value);
                BindFileQualification();
                BindFQgv();
                
            }
            #endregion

            #region 修改
            else if (Request.QueryString["type"] == "edit")
            {
                hidSupplierID.Value = Request["no"].ToString();
                DataTable dt = SelectSupplierList();

                #region 数据绑定
                labPlant.Text = dt.Rows[0]["supplier_AppCompanyName"].ToString();
                labDate.Text = dt.Rows[0]["supplier_AppDate"].ToString();
                labAPPUserName.Text = dt.Rows[0]["supplier_AppUserName"].ToString();
                labAPPDeptName.Text = dt.Rows[0]["supplier_AppDeptName"].ToString();
                //labDeptLeader.Text = dt.Rows[0]["supplier_AppLeaderName"].ToString();
                labChineseSupplierName.Text = dt.Rows[0]["supplier_SuppChineseName"].ToString();
                labEnglishSupplierName.Text = dt.Rows[0]["supplier_SuppEnglishName"].ToString();
                labChineseSupplierAddress.Text = dt.Rows[0]["supplier_SuppChineseAddress"].ToString();
                labEnglishSupplierAddress.Text = dt.Rows[0]["supplier_SuppEnglishAddress"].ToString();
                labSupplierUserName.Text = dt.Rows[0]["supplier_SuppContactName"].ToString();
                labSupplierRoleName.Text = dt.Rows[0]["supplier_SuppContactRoleName"].ToString();
                labSupplierNumber.Text = dt.Rows[0]["supplier_SuppContactNumber"].ToString();
                labSupplierPhone.Text = dt.Rows[0]["supplier_SuppContactPhone"].ToString();
                labSupplierFax.Text = dt.Rows[0]["supplier_SuppFax"].ToString();
                labSupplierEmail.Text = dt.Rows[0]["supplier_SuppEmail"].ToString();
                labSupplierWeb.Text = dt.Rows[0]["supplier_SuppCompanyWeb"].ToString();
                labSuppNewReason.Text = dt.Rows[0]["supplier_SuppNewReason"].ToString();
                labBusinesstypeID.Text = dt.Rows[0]["supplier_SuppBusinessTypeID"].ToString();
                labBusinesstype.Text = dt.Rows[0]["supplier_SuppBusinessType"].ToString();
                labBroadHeading.Text = dt.Rows[0]["BroadHeading"].ToString();
                labSubDivision.Text = dt.Rows[0]["SubDivision"].ToString();
                labSubMaterial.Text = dt.Rows[0]["SubMaterial"].ToString();
                labFactoryInspection.Text = dt.Rows[0]["FactoryInspection"].ToString();
                labBroadHeadingID.Text = dt.Rows[0]["supplier_BroadHeadingID"].ToString();
                labSubDivisionID.Text = dt.Rows[0]["supplier_SubDivisionID"].ToString();
                labSubMaterialID.Text = dt.Rows[0]["supplier_SubMaterialID"].ToString();
                labFactoryInspectionLevelID.Text = dt.Rows[0]["supplier_FactoryInspectionLevelID"].ToString();
                hidLevelID.Value = labFactoryInspection.Text;
                //
                labLeaderUsername.Text = dt.Rows[0]["supplier_AppLeaderName"].ToString();

                hidCreatedID.Value = dt.Rows[0]["createBy"].ToString();
                hidCreatedEmail.Value = dt.Rows[0]["email"].ToString();
                txtCurr.Text = dt.Rows[0]["supplier_SupplierCurr"].ToString();
                txtTax.Text = dt.Rows[0]["supplier_SupplierTaxc"].ToString();
                txtPaymentDays.Text = dt.Rows[0]["supplier_SupplierCrTerms"].ToString();
                #endregion
                //BindBroadHeading();
                //BindSubDivision();
                //BindSubMaterial();
                BindFileQualification();
                BindFQgv();
                BindgvSignFile();
                //绑定所有留言及附件
                BindOpinion();
                //绑定正式合同文件
                BindgvFormal();
                
            }
            #endregion
            #region 绑定
            //bind();
            #endregion
        }
    }

    private void selectEmailAddress()
    {
        string sqlstr = "sp_supplier_selectEmailAddress";

        SqlDataReader sdr = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, sqlstr);

        if (sdr.Read())
        {
            hidLawPersonEmail.Value = sdr["law"].ToString();
            hidFinPersonEmail.Value = sdr["fin"].ToString();
            hidManagerEmail.Value = sdr["manager"].ToString();
            hidSupplierEditEmail.Value = sdr["supplier"].ToString();
            hidSupplierPersonEmail.Value = sdr["supplierDep"].ToString();
        }

        sdr.Close();
        sdr.Dispose();

    }

    private void emailMassage(string to, string copy, string subject, string body)
    {

        #region 发送邮件
        string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();





        string bodyTemp = body;

        #region 写Body
        body = "<font style='font-size: 12px;'>" + bodyTemp + "</font><br />";
        body += "<br /><br />";
        body += "<font style='font-size: 12px;'>详情请登陆 "+baseDomain.getPortalWebsite()+" </font><br />";
        body += "<font style='font-size: 12px;'>For details please visit "+baseDomain.getPortalWebsite()+" </font>";
        #endregion





        if (!this.SendEmail(from, to, copy, subject, body))
        {
            this.ltlAlert.Text = "alert('Email sending failure');";
        }
        else
        {
            this.ltlAlert.Text = "alert('Email sending');";
        }
        #endregion
    }

    private DataTable SelectSupplierList()
    {
        string str = "sp_supplier_SelectNewSupplierListByNo";
        SqlParameter param = new SqlParameter("@no", hidSupplierID.Value.ToString());
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    /// <summary>
    /// 绑定大类区分
    /// </summary>
    //private void BindBroadHeading()
    //{
    //    DataTable dt = GetPartType("Heading", "DA4C29A2-C094-44DA-8FD8-0BAFE9520150", "A9F79BF6-335F-4EE1-B513-D14C62113A5F");
    //    ddlBroadHeading.Items.Clear();
    //    ddlBroadHeading.DataSource = dt;
    //    ddlBroadHeading.DataBind();
    //    ddlBroadHeading.Items.Insert(0, new ListItem("--大类区分--", "B30F90D4-0D39-4E8F-B2BB-2D245A7B0F6F"));
    //}
    /// <summary>
    /// 绑定细部区分
    /// </summary>
    //private void BindSubDivision()
    //{
    //    DataTable dt = GetPartType("Division", ddlBroadHeading.SelectedValue, "C4C58ADE-A9A0-4921-8D55-AB2038713761");
    //    ddlSubDivision.Items.Clear();
    //    ddlSubDivision.DataSource = dt;
    //    ddlSubDivision.DataBind();
    //    ddlSubDivision.Items.Insert(0, new ListItem("--细部区分--", "C68D9DD0-8F1B-4AEE-9D0B-BFD811D7711E"));
    //}
    /// <summary>
    /// 绑定子物料
    /// </summary>
    //private void BindSubMaterial()
    //{
    //    DataTable dt = GetPartType("Material", ddlBroadHeading.SelectedValue, ddlSubDivision.SelectedValue);
    //    ddlSubMaterial.Items.Clear();
    //    ddlSubMaterial.DataSource = dt;
    //    ddlSubMaterial.DataBind();
    //    ddlSubMaterial.Items.Insert(0, new ListItem("--子物料--", "C4C58ADE-A9A0-4921-8D55-AB2038713761"));
    //}
    /// <summary>
    /// 绑定供应商资质文件评估GridView
    /// </summary>
    private void BindFQgv()
    {
        DataTable dt = selectFileQualificationDet(hidSupplierID.Value);
        FQgv.DataSource = dt;
        FQgv.DataBind();
    }
    /// <summary>
    /// 绑定供应商资质文件评估文件类型DropDownList
    /// </summary>
    private void BindFileQualification()
    {
        DataTable dt = selectFileQualificationList(hidSupplierID.Value);
        ddlFileType.Items.Clear();
        ddlFileType.DataSource = dt;
        ddlFileType.DataBind();
        //ddlFileType.Items.Insert(0, new ListItem("--子物料--", "C4C58ADE-A9A0-4921-8D55-AB2038713761"));
    }
    /// <summary>
    /// 获取供应商资质文件评估明细GridView
    /// </summary>
    /// <param name="no"></param>
    /// <returns></returns>
    private DataTable selectFileQualificationDet(string no)
    {
        string str = "sp_supplier_selectFileQualificationDet";

        SqlParameter param = new SqlParameter("@no", no);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    /// <summary>
    /// 获取供应商资质文件评估文件类型DropDownList
    /// </summary>
    /// <param name="no"></param>
    /// <returns></returns>
    public DataTable selectFileQualificationList(string no)
    {
        string str = "sp_supplier_selectFileQualificationList";
        SqlParameter param = new SqlParameter("@no", no);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    /// <summary>
    /// 新增供应商资质文件评估明细GridView
    /// </summary>
    /// <param name="no"></param>
    private void InsertFileQualificationDet(string no)
    {
        string str = "sp_supplier_insertFileQualification_det";

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@uID", Session["uID"].ToString());
        param[2] = new SqlParameter("@uName", Session["uName"].ToString());
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, str, param);
    }
    /// <summary>
    /// 获取新供应商申请编号
    /// </summary>
    /// <returns></returns>
    private string selectSupplierNo()
    {
        string sql = "sp_supplier_selectSupplierNo";
        SqlParameter param = new SqlParameter("@uID", Session["uID"].ToString());
        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, sql, param).ToString();
    }
    /// <summary>
    /// 获取当前人所属部门
    /// </summary>
    private String GetUserDept(int uid, int plantCode)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@uid", uid);
        param[1] = new SqlParameter("@plantCode", plantCode);

        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_GetUserDept", param).ToString();
    }
    /// <summary>
    /// 获取当前部门领导信息
    /// </summary>
    /// <returns>姓名</returns>
    private String GetDeptLeader()
    {
        string sql = "select userName from users where roleid = 307 and plantCode = 1 and departmentID =" + Session["deptID"].ToString();

        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, sql).ToString();
    }
    /// <summary>
    /// 获取当前部门领导信息
    /// </summary>
    /// <returns>ID</returns>
    private String GetDeptLeaderID()
    {
        string sql = "select userID from users where roleid = 307 and plantCode = 1 and departmentID =" + Session["deptID"].ToString();

        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, sql).ToString();
    }
    /// <summary>
    /// 获取供应商物料类型
    /// </summary>
    /// <param name="type">物料分类</param>
    /// <param name="BroadHeadingID">大类区分Guid</param>
    /// <param name="SubDivisionID">细部区分Guid</param>
    /// <returns></returns>
    private DataTable GetPartType(string type, string BroadHeadingID, string SubDivisionID)
    {
        try
        {
            string str = "sp_supplier_selectPartType";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@type", type);
            param[1] = new SqlParameter("@BroadHeadingID", BroadHeadingID);
            param[2] = new SqlParameter("@SubDivisionID", SubDivisionID);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 根据大类区分确定细部区分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ddlBroadHeading_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindSubDivision();
    //    BindSubMaterial();
    //}
    /// <summary>
    /// 根据大类区分和细部区分确定子物料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindSubMaterial();
    //}
    /// <summary>
    /// 新增供应商按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Redirect("supplier_newApplyList.aspx");
        //string ChineseSupplierName = labChineseSupplierName.Text;
        //string EnglishSupplierName = labEnglishSupplierName.Text;
        //string ChineseSupplierAddress = labChineseSupplierAddress.Text;
        //string EnglishSupplierAddress = labEnglishSupplierAddress.Text;
        //string SupplierUserName = labSupplierUserName.Text;
        //string SupplierRoleName = labSupplierRoleName.Text;
        //string SupplierNumber = labSupplierNumber.Text;
        //string SupplierPhone = labSupplierPhone.Text;
        //string SupplierFax = labSupplierFax.Text;
        //string SupplierEmail = labSupplierEmail.Text;
        //string SupplierWeb = labSupplierWeb.Text;
        //string SuppNewReason = labSuppNewReason.Text;
        //if (Request.QueryString["type"] == null || Request.QueryString["type"] == "add")
        //{
        //    if (insertintoSupplierInfo(ChineseSupplierName, EnglishSupplierName, ChineseSupplierAddress, EnglishSupplierAddress, SupplierUserName
        //        , SupplierRoleName, SupplierNumber, SupplierPhone, SupplierFax, SupplierEmail, SupplierWeb, SuppNewReason))
        //    {
        //        ltlAlert.Text = "alert('新增供应商申请失败，请联系管理员！');";
        //        return;
        //    }
        //    else
        //    {
        //        hidSupplierID.Value = selectSupplierNo();
        //    }
        //}
        //else if (Request.QueryString["type"] == "edit")
        //{
        //    if (insertintoSupplierInfo(ChineseSupplierName, EnglishSupplierName, ChineseSupplierAddress, EnglishSupplierAddress, SupplierUserName
        //        , SupplierRoleName, SupplierNumber, SupplierPhone, SupplierFax, SupplierEmail, SupplierWeb, SuppNewReason))
        //    {
        //        ltlAlert.Text = "alert('新增供应商申请失败，请联系管理员！');";
        //        return;
        //    }
        //    else
        //    {
        //        hidSupplierID.Value = selectSupplierNo();
        //    }
        //}
    }
    /// <summary>
    /// 新增供应商
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private bool insertintoSupplierInfo(string ChineseSupplierName, string EnglishSupplierName, string ChineseSupplierAddress, string EnglishSupplierAddress, string SupplierUserName, string SupplierRoleName, string SupplierNumber, string SupplierPhone, string SupplierFax, string SupplierEmail, string SupplierWeb, string SuppNewReason)
    {
        string str = "sp_supplier_insertNewSupplier";
        SqlParameter[] param = new SqlParameter[28];
        //申请人信息
        param[0] = new SqlParameter("@Plant", labPlant.Text);
        param[1] = new SqlParameter("@APPDate", labDate.Text);
        param[2] = new SqlParameter("@APPUserID", Session["uID"].ToString());
        param[3] = new SqlParameter("@APPUserName", labAPPUserName.Text);
        param[4] = new SqlParameter("@APPDeptID", Session["deptID"].ToString());
        param[5] = new SqlParameter("@APPDeptName", labAPPDeptName.Text);
        param[6] = new SqlParameter("@DeptLeaderID", GetDeptLeaderID());
        param[7] = new SqlParameter("@DeptLeader", "");
        //param[7] = new SqlParameter("@DeptLeader", labDeptLeader.Text);
        //供应商信息
        param[8] = new SqlParameter("@ChineseSupplierName", ChineseSupplierName);
        param[9] = new SqlParameter("@EnglishSupplierName", EnglishSupplierName);
        param[10] = new SqlParameter("@ChineseSupplierAddress", ChineseSupplierAddress);
        param[11] = new SqlParameter("@EnglishSupplierAddress", EnglishSupplierAddress);
        param[12] = new SqlParameter("@SupplierUserName", SupplierUserName);
        param[13] = new SqlParameter("@SupplierRoleName", SupplierRoleName);
        param[14] = new SqlParameter("@SupplierNumber", SupplierNumber);
        param[15] = new SqlParameter("@SupplierPhone", SupplierPhone);
        param[16] = new SqlParameter("@SupplierFax", SupplierFax);
        param[17] = new SqlParameter("@SupplierEmail", SupplierEmail);
        param[18] = new SqlParameter("@SupplierWeb", SupplierWeb);
        param[19] = new SqlParameter("@SuppNewReason", SuppNewReason);
        param[20] = new SqlParameter("@No", hidSupplierID.Value);

        param[21] = new SqlParameter("@Businesstype", labBusinesstype.Text);
        param[22] = new SqlParameter("@BusinesstypeID", labBusinesstypeID.Text);
        param[23] = new SqlParameter("@BroadHeading", labBroadHeading.Text);
        param[24] = new SqlParameter("@SubDivision", labSubDivision.Text);
        param[25] = new SqlParameter("@SubMaterial", labSubMaterial.Text);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool updateSupplierInfo(string SuppChineseName, string SuppEnglishName, string SuppChineseAddress, string SuppEnglishAddress, string SuppContactName, string SuppContactRoleName, string SuppContactNumber, string SuppContactPhone, string SuppFax, string SuppEmail, string SuppCompanyWeb, string SuppNewReason)
    {
        string str = "";

        SqlParameter[] param = new SqlParameter[30];
        param[0] = new SqlParameter("@no", hidSupplierID.Value);
        param[1] = new SqlParameter("@SuppChineseName", SuppChineseName);
        param[2] = new SqlParameter("@SuppEnglishName", SuppEnglishName);
        param[3] = new SqlParameter("@SuppChineseAddress", SuppChineseAddress);
        param[4] = new SqlParameter("@SuppEnglishAddress", SuppEnglishAddress);
        param[5] = new SqlParameter("@SuppContactName", SuppContactName);
        param[6] = new SqlParameter("@SuppContactRoleName", SuppContactRoleName);
        param[7] = new SqlParameter("@SuppContactNumber", SuppContactNumber);
        param[8] = new SqlParameter("@SuppContactPhone", SuppContactPhone);
        param[9] = new SqlParameter("@SuppFax", SuppFax);
        param[10] = new SqlParameter("@SuppEmail", SuppEmail);
        param[11] = new SqlParameter("@SuppBusinessTypeID", labBusinesstypeID.Text);
        param[12] = new SqlParameter("@SuppBusinessType", labBusinesstype.Text);
        param[13] = new SqlParameter("@SuppCompanyWeb", SuppCompanyWeb);
        param[14] = new SqlParameter("@BroadHeadingID", labBroadHeading.Text);
        param[15] = new SqlParameter("@SubDivisionID", labSubDivision.Text);
        param[16] = new SqlParameter("@SubMaterialID", labSubMaterial.Text);
        param[17] = new SqlParameter("@SuppNewReason", SuppNewReason);
        param[18] = new SqlParameter("@uID", Session["uID"].ToString());
        param[19] = new SqlParameter("@uName", Session["uName"].ToString());
        return false;
    }
    /// <summary>
    /// RowDataBound事件
    /// 绑定供应商资质文件评估GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FQgv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (((LinkButton)e.Row.FindControl("lbldownload")).Text != "供应商预评估报告")
            {
                ((LinkButton)e.Row.FindControl("lbldownload")).Enabled = false;
                
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lbldownload")).Style.Value = "TEXT-DECORATION:solid";
                
            }

            //权限
            //if (!this.Security["120000035"].isValid)
            //{
            //    e.Row.Cells[5].Enabled = false;
            //}
            if (FQgv.DataKeys[e.Row.RowIndex].Values["supplier_FilePath"].ToString() == string.Empty)
            {
                
                e.Row.Cells[4].Text = "无文件";
                e.Row.Cells[4].Enabled = false;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }
            //此处允许查看
            if (FQgv.DataKeys[e.Row.RowIndex].Values["supplier_FileIsEffect"].ToString() == "2")//已过期
            {
                //e.Row.Cells[4].Text = "文件已过期";
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                
            }
        }
    }
    /// <summary>
    /// RowCommand事件
    /// 绑定供应商资质文件评估GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FQgv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = FQgv.DataKeys[intRow].Values["supplier_FilePath"].ToString().Trim();
            string fileName = FQgv.DataKeys[intRow].Values["supplier_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        if (e.CommandName.ToString() == "Download")
        {

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/docs/" + "SupplierPreassessmentReport.xlsx" + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");
        }
    }
    /// <summary>
    /// 更新供应商资质文件评估信息按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFileSubmit_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "1";
        string _filePath = "";
        string _fileName = "";
        string _fileEffDate = string.Empty;
        string filetypename = string.Empty;
        if (FileUpload2.FileName == string.Empty)
        {
            _filePath = "";
            _fileName = "";
            _fileEffDate = "";
        }
        else
        {
            if (txtEffectDate.Text == string.Empty)
            {
                this.Alert("请填入附件的有效时间！");
                return;
            }
            _fileEffDate = txtEffectDate.Text;
            try
            {
                if (!UploadFile(ref _filePath, ref _fileName, "FileUpload2"))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!请联系管理员！')";
            }
        }
        if (ddlFileType.SelectedItem.ToString() == "其他")
        {
            if (txtFileTypeName.Text == string.Empty)
            {
                this.Alert("请在其他文件类型后面填写文件的名称！");
                BindFQgv();
                return;
            }
            filetypename = txtFileTypeName.Text;
        }
        if (updateFileQualificationDet(hidSupplierID.Value, ddlFileType.SelectedValue, ddlFileType.SelectedItem.ToString()
            , ddlNecessity.SelectedValue, ddlNecessity.SelectedItem.ToString(), _fileName, _filePath, filetypename, _fileEffDate))
        {
            ltlAlert.Text = "";
            return;
        }
        else
        {
            BindFQgv();
            BindFileQualification();
        }
    }
    /// <summary>
    /// 更新供应商资质文件评估信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private bool updateFileQualificationDet(string no, string fileid, string filrtype, string necessityid, string necessity, string FileName, string FilePath, string filetypename, string fileEffDate)
    {
        string str = "sp_supplier_updateFileQualificationDet";

        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@supplier_FileID", fileid);
        param[2] = new SqlParameter("@supplier_FileType", filrtype);
        param[3] = new SqlParameter("@supplier_FileNecessityID", necessityid);
        param[4] = new SqlParameter("@supplier_FileNecessity", necessity);
        param[5] = new SqlParameter("@FileName", FileName);
        param[6] = new SqlParameter("@FilePath", FilePath);
        param[7] = new SqlParameter("@EffectDate", fileEffDate);
        param[8] = new SqlParameter("@FileTypeName", filetypename);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private bool UploadFile(ref string filePath, ref string fileName, string type)
    {
        string strUserFileName = string.Empty;
        if (type == "FileUpload1")
        {
            strUserFileName = FileUpload1.PostedFile.FileName;
        }
        else if (type == "FileUpload2")
        {
            strUserFileName = FileUpload2.PostedFile.FileName;
        }
        else if (type == "FileUpload3")
        {
            strUserFileName = FileUpload3.PostedFile.FileName;
        }
        //string strUserFileName = FileUpload2.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/NewSupplier/" + hidSupplierID.Value + "/";
        string strCatFolder = Server.MapPath(catPath);
        string attachName = "";
        string attachExtension = "";

        //获取文件的名称和后缀
        if (type == "FileUpload1")
        {
            attachName = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
            attachExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
        }
        else if (type == "FileUpload2")
        {
            attachName = Path.GetFileNameWithoutExtension(FileUpload2.PostedFile.FileName);
            attachExtension = Path.GetExtension(FileUpload2.PostedFile.FileName);
        }
        else if (type == "FileUpload3")
        {
            attachName = Path.GetFileNameWithoutExtension(FileUpload3.PostedFile.FileName);
            attachExtension = Path.GetExtension(FileUpload3.PostedFile.FileName);
        }


        //string SaveFileName = System.IO.Path.Combine(strCatFolder, _newFileName);//合并两个路径为上传到服务器上的全路径
        string SaveFileName = System.IO.Path.Combine(strCatFolder, DateTime.Now.ToFileTime().ToString() + attachExtension);//合并两个路径为上传到服务器上的全路径
        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('fail to delete folder！')";

                return false;
            }
        }
        if (!Directory.Exists(Server.MapPath(catPath)))
        {
            Directory.CreateDirectory(Server.MapPath(catPath));
        }
        try
        {
            if (type == "FileUpload1")
            {
                FileUpload1.PostedFile.SaveAs(SaveFileName);
            }
            else if (type == "FileUpload2")
            {
                FileUpload2.PostedFile.SaveAs(SaveFileName);
            }
            else if (type == "FileUpload3")
            {
                FileUpload3.PostedFile.SaveAs(SaveFileName);
            }
        }
        catch
        {
            ltlAlert.Text = "alert('文件上传失败')";
            return false;
        }
        string path = @"/TecDocs/NewSupplier/" + hidSupplierID.Value + "/";

        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        string docid = DateTime.Now.ToFileTime().ToString() + attachExtension;
        try
        {
            File.Move(SaveFileName, Server.MapPath(path + docid));
        }
        catch
        {
            ltlAlert.Text = "alert('fail to move file')";

            if (File.Exists(SaveFileName))
            {
                try
                {
                    File.Delete(SaveFileName);
                }
                catch
                {
                    ltlAlert.Text = "alert('fail to delete folder')";

                    return false;
                }
            }
            return false;
        }


        filePath = catPath + docid;
        fileName = _fileName;
        return true;
    }
    private void BindgvSignFile()
    {
        DataTable dt = SelectSignFile();
        gvFile.DataSource = dt;
        gvFile.DataBind();
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "2";
        string _filePath = "";
        string _fileName = "";

        if (FileUpload1.FileName == string.Empty)
        {
            this.Alert("请重新选择上传文件！");
            return;
        }
        else
        {
            try
            {
                if (!UploadFile(ref _filePath, ref _fileName, "FileUpload1"))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }
                else
                {
                    if (insertSignFileInfo(hidSupplierID.Value, _fileName, _filePath))
                    {
                        this.Alert("上传文件时失败！请联系管理员！");
                        return;
                    }
                    else
                    {
                        BindgvSignFile();
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!请联系管理员！')";
            }
        }
    }
    private bool insertSignFileInfo(string no, string fileName, string filePath)
    {
        string str = "sp_supplier_insertSignFileInfo";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@fileName", fileName);
        param[2] = new SqlParameter("@filePath", filePath);
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool insertFormalFileInfo(string no, string fileName, string filePath)
    {
        string str = "sp_supplier_insertFormalFileInfo";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@fileName", fileName);
        param[2] = new SqlParameter("@filePath", filePath);
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private DataTable SelectSignFile()
    {
        string sql = "select isnull(SignFile_FileStatus,0) SignFile_FileStatus,SignFile_FileID,SignFile_FileName,SignFile_FilePath,createBy,createName,createDate from supplier_SignFileDet where SignFile_SupplierNo ='" + hidSupplierID.Value + "'";

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    private void BindOpinion()
    {
        btnSave.Enabled = false;
        DataTable supplierMstr = GetSupplierMstrByNo();
        tdSuppDeptOpinion.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "SuppDept").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        tdSuppDeptOpinion1.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "SuppDept").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        tdLawDeptOpinion.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "LawDept").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        tdLawDeptOpinion1.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "LawDept").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        tdFinanceDeptOpinion.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "FinanceDept").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        tdFinanceDeptOpinion1.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "FinanceDept").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        tdLeaderOpinion.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "Leader").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        tdSignFileOpinion.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "SignFile").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        tdFLOpinion.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "FL").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");
        tdManageOpinion.InnerHtml = GetProcMagandFile(hidSupplierID.Value.ToString(), "Manage").Replace("\n", "<br />").Replace("/u", "<u>").Replace("u/", "<u/>");

        hidLeaderIsAgree.Value = SelectHidValue("Leader").ToString();
        hidSignFileIsAgree.Value = SelectHidValue("SignFile").ToString();
        hidFLIsAgree.Value = SelectHidValue("FL").ToString();
        hidSuppDept.Value = SelectHidValue("SuppDept").ToString();
        hidLawDept.Value = SelectHidValue("LawDept").ToString();
        hidFinanceDept.Value = SelectHidValue("FinanceDept").ToString();
        hidManageIsAgree.Value = SelectHidValue("Manage").ToString();
        hidIsFL.Value = SelectHidValue("ISFL").ToString();
        hidSupplierStatus.Value = SelectHidValue("SupplierStatus").ToString();
        labSupplierNum.Text = selectSupplierNum();
        hidFLStatus.Value = selectFIStatus();
        if (hidIsFL.Value == "1")
        {
            //hidFLStatus.Value = selectFIStatus();
            btnIsNotFI.Visible = false;
            btnIsFI.Enabled = false;
        }
        else if (hidIsFL.Value == "2")
        {
            btnIsFI.Visible = false;
            btnIsNotFI.Enabled = false;
        }
        if (hidFLStatus.Value != "2")
        {
            linkFIStatus.Visible = false;
        }
        else
        {
            linkFIStatus.Visible = false;
            tdFIlink.InnerHtml = "<u style='color:red;' id='linkFI'>查看验厂详情</u>";
        }
        #region 新供应商信息单按钮显示控制
        //领导签字
        if (hidLeaderIsAgree.Value == "1")
        {
            btnLeaderNo.Visible = false;
            btnLeaderYes.Enabled = false;
            btnSave.Enabled = true;
        }
        else if (hidLeaderIsAgree.Value == "2")
        {
            btnLeaderNo.Enabled = false;
            btnLeaderYes.Visible = false;
            btnSignFileYes.Visible = false;
            btnSave.Enabled = true;
        }
        //供应商资质文件评估
        if (hidSignFileIsAgree.Value == "1")
        {
            btnSignFileYes.Enabled = false;
            btnSignFileNo.Visible = false;
            btnSave.Enabled = true;
        }
        else if (hidSignFileIsAgree.Value == "2")
        {
            btnSignFileYes.Visible = false;
            btnSignFileNo.Enabled = false;
            btnFLYes.Visible = false;
            btnFLNo.Visible = false;
        }
        //验厂意见
        if (hidFLIsAgree.Value == "1")
        {
            btnFLYes.Enabled = false;
            btnFLNo.Visible = false;
            btnSave.Enabled = true;
        }
        else if (hidFLIsAgree.Value == "2")
        {
            btnFLYes.Visible = false;
            btnFLNo.Enabled = false;
        }
        //总经理意见
        if (hidManageIsAgree.Value == "1")
        {
            btnManageYes.Enabled = false;
            btnManageNo.Visible = false;
        }
        else if (hidManageIsAgree.Value == "2")
        {
            btnManageYes.Visible = false;
            btnManageNo.Enabled = false;
        }
        #endregion

        #region 签署文件按钮显示控制
        if (hidSuppDept.Value == "0")
        {
            Button10.Visible = true;
            btnSuppDeptYes.Enabled = true;
            btnSuppDeptNo.Visible = true;
            btnLawDeptYes.Visible = false;
            btnLawDeptNo.Visible = false;
            btnFinanceDeptYes.Visible = false;
            btnFinanceDeptNo.Visible = false;
            gvFile.Columns[4].Visible = true;
        }
        else if (hidSuppDept.Value == "1")
        {
            Button10.Visible = false;
            btnSuppDeptYes.Enabled = false;
            btnSuppDeptNo.Visible = false;
            btnLawDeptYes.Visible = false;
            btnLawDeptNo.Visible = false;
            btnFinanceDeptYes.Visible = false;
            btnFinanceDeptNo.Visible = false;
            gvFile.Columns[4].Visible = false;
            btnSave.Visible = false;
            if (hidLawDept.Value == "0")
            {
                btnLawDeptYes.Visible = true;
                btnLawDeptNo.Visible = true;
                btnFinanceDeptYes.Visible = false;
                btnFinanceDeptNo.Visible = false;
            }
            else if (hidLawDept.Value == "1")
            {
                btnLawDeptYes.Visible = true;
                btnLawDeptYes.Enabled = false;
                btnLawDeptNo.Visible = false;
                btnFinanceDeptYes.Visible = false;
                btnFinanceDeptNo.Visible = false;
                if (hidFinanceDept.Value == "0")
                {
                    btnFinanceDeptYes.Visible = true;
                    btnFinanceDeptNo.Visible = true;
                }
                else if (hidFinanceDept.Value == "1")
                {
                    btnFinanceDeptYes.Visible = true;
                    btnFinanceDeptYes.Enabled = false;
                    btnFinanceDeptNo.Visible = false;
                }
                else if (hidFinanceDept.Value == "2")
                {
                    Button10.Visible = true;
                    btnSuppDeptYes.Enabled = true;
                    btnSuppDeptNo.Visible = true;
                    btnLawDeptYes.Visible = false;
                    btnLawDeptNo.Visible = false;
                    btnFinanceDeptYes.Visible = false;
                    btnFinanceDeptNo.Visible = false;
                }
            }
            else if (hidLawDept.Value == "2")
            {
                Button10.Visible = true;
                btnSuppDeptYes.Enabled = true;
                btnSuppDeptNo.Visible = true;
                btnLawDeptYes.Visible = false;
                btnLawDeptNo.Visible = false;
                btnFinanceDeptYes.Visible = false;
                btnFinanceDeptNo.Visible = false;
                btnSave.Visible = true;
            }
        }
        else if (hidSuppDept.Value == "2")
        {
            Button10.Visible = false;
            btnSuppDeptYes.Visible = false;
            btnSuppDeptNo.Enabled = false;
            btnLawDeptYes.Visible = false;
            btnLawDeptNo.Visible = false;
            btnFinanceDeptYes.Visible = false;
            btnFinanceDeptNo.Visible = false;
            gvFile.Columns[4].Visible = false;
            btnFLYes.Visible = false;
        }

        if (hidFLStatus.Value == "0")
        {
            if (hidIsFL.Value == "1")
            {
                Button10.Visible = false;
                btnSuppDeptYes.Visible = false;
                btnSuppDeptNo.Visible = false;
                btnLawDeptYes.Visible = false;
                btnLawDeptNo.Visible = false;
                btnFinanceDeptYes.Visible = false;
                btnFinanceDeptNo.Visible = false;
                gvFile.Columns[4].Visible = true;
            }
        }


        #endregion

        #region 按钮操作人显示
        foreach (DataRow row in supplierMstr.Rows)
        {
            //主管签字
            if (!string.IsNullOrEmpty(row["supplier_LeaderIsAgreeBy"].ToString()))
            {
                if (row["supplier_LeaderIsAgree"].ToString() == "1")
                {
                    btnLeaderYes.Width = 100;
                    btnLeaderYes.Text = "同意（" + row["supplier_LeaderIsAgreeName"].ToString() + "）";
                }
                else if (row["supplier_LeaderIsAgree"].ToString() == "2")
                {
                    btnLeaderNo.Width = 100;
                    btnLeaderNo.Text = "拒绝（" + row["supplier_LeaderIsAgreeName"].ToString() + "）";
                }
            }
            //供应商资质文件评估
            if (!string.IsNullOrEmpty(row["supplier_SignFileIsAgreeBy"].ToString()))
            {
                if (row["supplier_SignFileIsAgree"].ToString() == "1")
                {
                    btnSignFileYes.Width = 100;
                    btnSignFileYes.Text = "同意（" + row["supplier_SignFileIsAgreeName"].ToString() + "）";
                }
                else if (row["supplier_SignFileIsAgree"].ToString() == "2")
                {
                    btnSignFileNo.Width = 100;
                    btnSignFileNo.Text = "拒绝（" + row["supplier_SignFileIsAgreeName"].ToString() + "）";
                }
            }
            //是否验厂
            if (!string.IsNullOrEmpty(row["supplier_IsFLBy"].ToString()))
            {
                if (row["supplier_IsFL"].ToString() == "1")
                {
                    btnIsFI.Width = 100;
                    btnIsFI.Text = "验厂（" + row["supplier_IsFLName"].ToString() + "）";
                }
                else if (row["supplier_IsFL"].ToString() == "2")
                {
                    btnIsNotFI.Width = 100;
                    btnIsNotFI.Text = "不验厂（" + row["supplier_IsFLName"].ToString() + "）";
                }
            }
            //验厂意见
            if (!string.IsNullOrEmpty(row["supplier_FLIsAgreeBy"].ToString()))
            {
                if (row["supplier_FLIsAgree"].ToString() == "1")
                {
                    btnFLYes.Width = 100;
                    btnFLYes.Text = "同意（" + row["supplier_FLIsAgreeName"].ToString() + "）";
                }
                else if (row["supplier_FLIsAgree"].ToString() == "2")
                {
                    btnFLNo.Width = 100;
                    btnFLNo.Text = "拒绝（" + row["supplier_FLIsAgreeName"].ToString() + "）";
                }
            }
            //总经理意见
            if (!string.IsNullOrEmpty(row["supplier_ManageIsAgreeBy"].ToString()))
            {
                if (row["supplier_ManageIsAgree"].ToString() == "1")
                {
                    btnManageYes.Width = 100;
                    btnManageYes.Text = "同意（" + row["supplier_ManageIsAgreeName"].ToString() + "）";
                }
                else if (row["supplier_ManageIsAgree"].ToString() == "2")
                {
                    btnManageNo.Width = 100;
                    btnManageNo.Text = "拒绝（" + row["supplier_ManageIsAgreeName"].ToString() + "）";
                }
            }
            //供应商提交
            if (!string.IsNullOrEmpty(row["supplier_SupplierStatusBy"].ToString()))
            {
                if (row["supplier_SupplierStatus"].ToString() == "2")
                {
                    btnSupplierNum.Enabled = false;
                    btnSupplierNum.Width = 100;
                    btnSupplierNum.Text = "同意（" + row["supplier_SupplierStatusName"].ToString() + "）";
                }
            }






            //供应商意见
            if (!string.IsNullOrEmpty(row["supplier_SuppDeptIsAgreeBy"].ToString()))
            {
                if (row["supplier_SuppDeptIsAgree"].ToString() == "1")
                {
                    btnLeaderYes.Width = 100;
                    btnLeaderYes.Text = "提交（" + row["supplier_SuppDeptIsAgreeName"].ToString() + "）";
                }
                else if (row["supplier_LeaderIsAgree"].ToString() == "2")
                {
                    btnLeaderNo.Width = 100;
                    btnLeaderNo.Text = "拒绝（" + row["supplier_SuppDeptIsAgreeName"].ToString() + "）";
                }
            }
            //法务部意见
            if (!string.IsNullOrEmpty(row["supplier_LawDeptIsAgreeBy"].ToString()))
            {
                if (row["supplier_LawDeptIsAgree"].ToString() == "1")
                {
                    btnLeaderYes.Width = 100;
                    btnLeaderYes.Text = "同意（" + row["supplier_LawDeptIsAgreeName"].ToString() + "）";
                }
                else if (row["supplier_LeaderIsAgree"].ToString() == "2")
                {
                    btnLeaderNo.Width = 100;
                    btnLeaderNo.Text = "驳回（" + row["supplier_LawDeptIsAgreeName"].ToString() + "）";
                }
            }
            //财务部意见
            if (!string.IsNullOrEmpty(row["supplier_FinanceDeptIsAgreeBy"].ToString()))
            {
                if (row["supplier_FinanceDeptIsAgree"].ToString() == "1")
                {
                    btnLeaderYes.Width = 100;
                    btnLeaderYes.Text = "同意（" + row["supplier_FinanceDeptIsAgreeName"].ToString() + "）";
                }
                else if (row["supplier_FinanceDeptIsAgree"].ToString() == "2")
                {
                    btnLeaderNo.Width = 100;
                    btnLeaderNo.Text = "拒绝（" + row["supplier_FinanceDeptIsAgreeName"].ToString() + "）";
                }
            }
        }
        #endregion
    }
    private DataTable GetSupplierMstrByNo()
    {
        string str = "sp_supplier_selecrSupplierMstr";
        SqlParameter param = new SqlParameter("@no", hidSupplierID.Value);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    private string selectSupplierNum()
    {
        string sql = "Select  supplier_supplierNum  From  supplier_mstr Where supplier_No = '" + hidSupplierID.Value + "'";
        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, sql).ToString();
    }
    private string selectFIStatus()
    {
        string str = "sp_supplier_selectFIstatus";

        SqlParameter param = new SqlParameter("@no", hidSupplierID.Value);
        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param).ToString();
    }

    private string GetProcMagandFile(string no, string ResponType)
    {
        string str = "sp_supplier_SelectResponOpinion";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@ResponType", ResponType);
        return Convert.ToString(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }

    #region btn事件
    protected void btnLeaderYes_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        if (btnIsAgree("Leader", "Yes"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();
        }
    }
    protected void btnLeaderNo_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
    
        if (btnIsAgree("Leader", "No"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();
        }
    }
    protected void btnSignFileYes_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        if (btnIsAgree("SignFile", "Yes"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            if (!txtCurr.Text.Equals(string.Empty) && !txtTax.Text.Equals(string.Empty) && !txtPaymentDays.Text.Equals(string.Empty))
            {
                saveInfo();
                BindOpinion();
            }
            else
                Alert("币种、税率、账期不能为空，请填写");
            
        }
    }
    protected void btnSignFileNo_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        if (btnIsAgree("SignFile", "No"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();
        }
    }
    protected void btnFLYes_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        if (btnIsAgree("FL", "Yes"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();
        }
    }
    protected void btnFLNo_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        if (btnIsAgree("FL", "No"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();
        }
    }
    protected void btnManageYes_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        if (btnIsAgree("Manage", "Yes"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();


            emailMassage(hidSupplierEditEmail.Value, string.Empty, "新增供应商有需要您操作的项目", "新增供应商有需要您操作的项目");
        }
    }
    protected void btnManageNo_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        if (btnIsAgree("Manage", "No"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();

            emailMassage(hidCreatedEmail.Value, string.Empty, "新增供应商您申请的项目被拒绝", "新增供应商您申请的项目被拒绝");
        }
    }
    protected void btnSuppDeptYes_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "2";
        if (btnIsAgree("SuppDept", "Yes"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();
            emailMassage(hidLawPersonEmail.Value, string.Empty, "新增供应商有需要您审批的项目", "新增供应商有需要您审批的项目");

        }
    }
    protected void btnSuppDeptNo_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "2";
        if (btnIsAgree("SuppDept", "No"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();
            emailMassage(hidCreatedEmail.Value, string.Empty, "新增供应商您申请的项目被拒绝", "新增供应商您申请的项目被拒绝");
        }
    }
    protected void btnLawDeptYes_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "2";
        if (btnIsAgree("LawDept", "Yes"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();

            emailMassage(hidFinPersonEmail.Value, string.Empty, "新增供应商有需要您审批的项目", "新增供应商有需要您审批的项目");
        }
    }
    protected void btnLawDeptNo_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "2";
        if (btnIsAgree("LawDept", "No"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();
            emailMassage(hidSupplierPersonEmail.Value, string.Empty, "新增供应商有需要您审批的项目", "新增供应商有需要您审批的项目");
        }
    }
    protected void btnFinanceDeptYes_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "2";
        if (btnIsAgree("FinanceDept", "Yes"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();

            emailMassage(hidManagerEmail.Value, string.Empty, "新增供应商有需要您审批的项目", "新增供应商有需要您审批的项目");
        }
    }
    protected void btnFinanceDeptNo_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "2";
        if (btnIsAgree("FinanceDept", "No"))
        {
            this.Alert("操作失败！");
            return;
        }
        else
        {
            BindOpinion();
            emailMassage(hidSupplierPersonEmail.Value, string.Empty, "新增供应商有需要您审批的项目", "新增供应商有需要您审批的项目");
        }
    }

    #endregion
    private bool btnIsAgree(string btnType, string btnTypeValue)
    {
        string str = "sp_supplier_btnIsAgree";

        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@btnType", btnType);
        param[1] = new SqlParameter("@btnTypeValue", btnTypeValue);
        param[2] = new SqlParameter("@uID", Session["uID"].ToString());
        param[3] = new SqlParameter("@uName", Session["uName"].ToString());
        param[4] = new SqlParameter("@no", hidSupplierID.Value);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private string SelectHidValue(string btnType)
    {
        try
        {
            string str = "sp_supplier_SelectHidValue";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@no", hidSupplierID.Value);
            param[1] = new SqlParameter("@btnType", btnType);

            return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param).ToString();
        }
        catch
        {
            return "0";
        }
    }
    private void BindgvFormal()
    {
        DataTable dt = SelectFormalFile();
        gvFormal.DataSource = dt;
        gvFormal.DataBind();
    }
    private DataTable SelectFormalFile()
    {
        string sql = "select FormalFile_FileName,FormalFile_FilePath,createBy,createName,createDate from supplier_FormalFileDet where FormalFile_SupplierNo ='" + hidSupplierID.Value + "'";

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "3";
        string _filePath = "";
        string _fileName = "";

        if (FileUpload3.FileName == string.Empty)
        {
            this.Alert("请重新选择上传文件！");
            return;
        }
        else
        {
            try
            {
                if (!UploadFile(ref _filePath, ref _fileName, "FileUpload3"))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }
                else
                {
                    if (insertFormalFileInfo(hidSupplierID.Value, _fileName, _filePath))
                    {
                        this.Alert("上传文件时失败！请联系管理员！");
                        return;
                    }
                    else
                    {
                        BindgvFormal();
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!请联系管理员！')";
            }
        }
    }
    protected void gvFormal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvFormal.DataKeys[intRow].Values["FormalFile_FilePath"].ToString().Trim();
            string fileName = gvFormal.DataKeys[intRow].Values["FormalFile_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }
    protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvFile.DataKeys[intRow].Values["SignFile_FilePath"].ToString().Trim();
            string fileName = gvFile.DataKeys[intRow].Values["SignFile_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        //作废
        if (e.CommandName.ToString() == "NotEff")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string fileID = gvFile.DataKeys[intRow].Values["SignFile_FileID"].ToString().Trim();
            if (disApproveForSignFileStatus(fileID))
            {
                this.Alert("作废失败！");
                return;
            }
            else
            {
                BindgvSignFile();
            }
        }
    }
    private bool disApproveForSignFileStatus(string fileID)
    {
        string sql = "Update supplier_SignFileDet Set SignFile_FileStatus = 2 Where SignFile_FileID = '" + fileID + "'";
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, sql));
    }
    protected void linkFIStatus_Click(object sender, EventArgs e)
    {
        Response.Redirect("FI_view.aspx?suppno=" + hidSupplierID.Value);
    }
    protected void btnSupplierNum_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        if (updateSupplierStatus())
        {
            ltlAlert.Text = "alert('提交失败，请联系管理员')";
            return;
        }
        else
        {
            BindOpinion();
        }
    }
    private bool updateSupplierStatus()
    {
        string str = "sp_supplier_UpdateSupplierStatus";

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@no", hidSupplierID.Value);
        param[1] = new SqlParameter("@uID", Session["uID"].ToString());
        param[2] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void gvFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gvFile.DataKeys[e.Row.RowIndex].Values["SignFile_FileStatus"].ToString() == "2")
            {
                e.Row.Cells[4].Text = "已作废";
                e.Row.Cells[4].Enabled = false;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnIsFI_Click(object sender, EventArgs e)
    {
        if (updateIsFI("Y"))
        {
            ltlAlert.Text = "alert('验厂按钮操作失败，请联系管理员')";
            return;
        }
        else
        {
            Response.Redirect("../supplier/Supp_FatocryInspection_New.aspx?suppno=" + hidSupplierID.Value + "&type=" + labFactoryInspection.Text
                             + "&name=" + labChineseSupplierName.Text + "&Address=" + labChineseSupplierAddress.Text
                             + "&Fax=" + labSupplierFax.Text + "&Telephone=" + labSupplierNumber.Text + "," + labSupplierPhone.Text);
        }
    }
    protected void btnIsNotFI_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "0";
        if (updateIsFI("N"))
        {
            ltlAlert.Text = "alert('不验厂按钮操作失败，请联系管理员')";
            return;
        }
        else
        {
            BindOpinion();
        }
    }
    private bool updateIsFI(string type)
    {
        string str = "sp_supplier_updateIsFI";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@uID", Session["uID"].ToString());
        param[1] = new SqlParameter("@uName", Session["uName"].ToString());
        param[2] = new SqlParameter("@no", hidSupplierID.Value);
        param[3] = new SqlParameter("@type", type);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int flag = saveInfo();

        if (flag == 1 && !txtCurr.Text.Equals(string.Empty) && !txtTax.Text.Equals(string.Empty) && !txtPaymentDays.Text.Equals(string.Empty))
        {
            if (!txtCurr.Text.ToLower().Equals("rmb") && !txtCurr.Text.ToLower().Equals("usd"))
            {
                Alert("币种，请输入rmb/usd");

            }
            if (!txtPaymentDays.Text.Equals("0") && !txtPaymentDays.Text.Equals("30") && !txtPaymentDays.Text.Equals("60") && !txtPaymentDays.Text.Equals("90"))
            {
                Alert("账期，请输入0/30/60/90");

            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtTax.Text.Trim(), "^\\d+$"))
            {
                Alert("税率，请输入数字");

            }
            Alert("保存成功");
        }
        else
            Alert("保存失败，币种、税率、账期不能为空，请填写");
    }

    private int saveInfo()
    {

        string str = "sp_supplier_Save";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@supplierNo", Request["no"].ToString());
        param[1] = new SqlParameter("@curr", txtCurr.Text.ToString().Trim());
        param[2] = new SqlParameter("@tax", txtTax.Text.ToString().Trim());
        param[3] = new SqlParameter("@cr_terms", txtPaymentDays.Text.ToString().Trim());



        return Convert.ToInt32(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));

    }


}