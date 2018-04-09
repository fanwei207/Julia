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

public partial class Supplier_supplier_newApply1 : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                    labPlant.Text = "淮安强陵照明有限公司 HQL";
                }
                labDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                labAPPUserName.Text = Session["uName"].ToString();
                labAPPDeptName.Text = GetUserDept(Convert.ToInt32(Session["uID"].ToString()), Convert.ToInt32(Session["PlantCode"].ToString()));
                //labDeptLeader.Text = GetDeptLeader();
                BindBusinesstype();
                BindBroadHeading();
                BindSubDivision();
                BindSubMaterial();
                BindFactoryInspectionLevel();
                //Label1.Text = selectSupplierNo();
                hidSupplierID.Value = selectSupplierNo();
            }
            #endregion

            #region 修改
            else if (Request.QueryString["type"] == "edit")
            {
                hidSupplierID.Value = Request["no"].ToString();
                DataTable dt = SelectSupplierList();
                txtChineseSupplierName.Enabled = false;
                ddlBusinesstype.Enabled = false;
                ddlBroadHeading.Enabled = false;
                ddlSubDivision.Enabled = false;
                ddlSubMaterial.Enabled = false;
                ddlFactoryInspectionLevel.Enabled = false;
                #region 数据绑定
                labPlant.Text = dt.Rows[0]["supplier_AppCompanyName"].ToString();
                labDate.Text = dt.Rows[0]["supplier_AppDate"].ToString();
                labAPPUserName.Text = dt.Rows[0]["supplier_AppUserName"].ToString();
                labAPPDeptName.Text = dt.Rows[0]["supplier_AppDeptName"].ToString();
                //labDeptLeader.Text = dt.Rows[0]["supplier_AppLeaderName"].ToString();
                txtChineseSupplierName.Text = dt.Rows[0]["supplier_SuppChineseName"].ToString();
                txtEnglishSupplierName.Text = dt.Rows[0]["supplier_SuppEnglishName"].ToString();
                txtChineseSupplierAddress.Text = dt.Rows[0]["supplier_SuppChineseAddress"].ToString();
                txtEnglishSupplierAddress.Text = dt.Rows[0]["supplier_SuppEnglishAddress"].ToString();
                txtSupplierUserName.Text = dt.Rows[0]["supplier_SuppContactName"].ToString();
                txtSupplierRoleName.Text = dt.Rows[0]["supplier_SuppContactRoleName"].ToString();
                txtSupplierNumber.Text = dt.Rows[0]["supplier_SuppContactNumber"].ToString();
                txtSupplierPhone.Text = dt.Rows[0]["supplier_SuppContactPhone"].ToString();
                txtSupplierFax.Text = dt.Rows[0]["supplier_SuppFax"].ToString();
                txtSupplierEmail.Text = dt.Rows[0]["supplier_SuppEmail"].ToString();
                txtSupplierWeb.Text = dt.Rows[0]["supplier_SuppCompanyWeb"].ToString();
                txtSuppNewReason.Text = dt.Rows[0]["supplier_SuppNewReason"].ToString();
                hidSupplierStatus.Value = dt.Rows[0]["supplier_SupplierStatus"].ToString();
                //txtBusinesstypeID.Text = dt.Rows[0]["supplier_SuppBusinessTypeID"].ToString();
                //txtBusinesstype.Text = dt.Rows[0]["supplier_SuppBusinessType"].ToString();
                //txtBroadHeading.Text = dt.Rows[0]["BroadHeading"].ToString();
                //labSubDivision.Text = dt.Rows[0]["SubDivision"].ToString();
                //labSubMaterial.Text = dt.Rows[0]["SubMaterial"].ToString();
                //labBroadHeadingID.Text = dt.Rows[0]["supplier_BroadHeadingID"].ToString();
                //labSubDivisionID.Text = dt.Rows[0]["supplier_SubDivisionID"].ToString();
                //labSubMaterialID.Text = dt.Rows[0]["supplier_SubMaterialID"].ToString();
                #endregion
                BindBusinesstype();
                BindBroadHeading();
                BindSubDivision();
                BindSubMaterial();
                BindFactoryInspectionLevel();
                ddlBusinesstype.SelectedValue = dt.Rows[0]["supplier_SuppBusinessTypeID"].ToString();
                ddlBroadHeading.SelectedValue = dt.Rows[0]["supplier_BroadHeadingID"].ToString();
                ddlSubDivision.SelectedValue = dt.Rows[0]["supplier_SubDivisionID"].ToString();
                ddlSubMaterial.SelectedValue = dt.Rows[0]["supplier_SubMaterialID"].ToString();
                ddlFactoryInspectionLevel.SelectedValue = dt.Rows[0]["supplier_FactoryInspectionLevelID"].ToString();
                //BindFileQualification();
                //BindFQgv();

                if (hidSupplierStatus.Value == "2")
                {
                    Button1.Enabled = true;
                }
                else
                {
                    Button1.Enabled = false;
                }
            }
            #endregion            
        }
    }
    private DataTable SelectSupplierList()
    {
        string str = "sp_supplier_SelectNewSupplierListByNo";
        SqlParameter param = new SqlParameter("@no", hidSupplierID.Value.ToString());
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

    private void BindBusinesstype()
    {
        ddlBusinesstype.Items.Clear();
        DataTable dt = GetPartType("SupplieType", "B30F90D4-0D39-4E8F-B2BB-2D245A7B0F6F","B30F90D4-0D39-4E8F-B2BB-2D245A7B0F6F", "C68D9DD0-8F1B-4AEE-9D0B-BFD811D7711E", "C4C58ADE-A9A0-4921-8D55-AB2038713761");
        //DataTable dt = GetPartType("Heading", ddlBroadHeading.SelectedValue, ddlSubDivision.SelectedValue, ddlSubMaterial.SelectedValue);
        ddlBusinesstype.DataSource = dt;
        ddlBusinesstype.DataBind();
        ddlBusinesstype.Items.Insert(0, new ListItem("--经营类型--", "B30F90D4-0D39-4E8F-B2BB-2D245A7B0F6F"));
    }
    /// <summary>
    /// 绑定大类区分
    /// </summary>
    private void BindBroadHeading()
    {
        ddlBroadHeading.Items.Clear();
        DataTable dt = GetPartType("Heading", ddlBusinesstype.SelectedValue, "B30F90D4-0D39-4E8F-B2BB-2D245A7B0F6F", "C68D9DD0-8F1B-4AEE-9D0B-BFD811D7711E", "C4C58ADE-A9A0-4921-8D55-AB2038713761");
        //DataTable dt = GetPartType("Heading", ddlBroadHeading.SelectedValue, ddlSubDivision.SelectedValue, ddlSubMaterial.SelectedValue);
        ddlBroadHeading.DataSource = dt;
        ddlBroadHeading.DataBind();
        ddlBroadHeading.Items.Insert(0, new ListItem("--大类区分--", "B30F90D4-0D39-4E8F-B2BB-2D245A7B0F6F"));
    }
    /// <summary>
    /// 绑定细部区分
    /// </summary>
    private void BindSubDivision()
    {
        ddlSubDivision.Items.Clear();
        DataTable dt = GetPartType("Division", ddlBusinesstype.SelectedValue, ddlBroadHeading.SelectedValue, "C68D9DD0-8F1B-4AEE-9D0B-BFD811D7711E", "C4C58ADE-A9A0-4921-8D55-AB2038713761");
        //DataTable dt = GetPartType("Division", ddlBroadHeading.SelectedValue, ddlSubDivision.SelectedValue, ddlSubMaterial.SelectedValue);
        ddlSubDivision.DataSource = dt;
        ddlSubDivision.DataBind();
        ddlSubDivision.Items.Insert(0, new ListItem("--细部区分--", "C68D9DD0-8F1B-4AEE-9D0B-BFD811D7711E"));
    }
    /// <summary>
    /// 绑定子物料
    /// </summary>
    private void BindSubMaterial()
    {
        ddlSubMaterial.Items.Clear();
        DataTable dt = GetPartType("Material", ddlBusinesstype.SelectedValue, ddlBroadHeading.SelectedValue, ddlSubDivision.SelectedValue, "C4C58ADE-A9A0-4921-8D55-AB2038713761");
        //DataTable dt = GetPartType("Material", ddlBroadHeading.SelectedValue, ddlSubDivision.SelectedValue, ddlSubMaterial.SelectedValue);
        ddlSubMaterial.DataSource = dt;
        ddlSubMaterial.DataBind();
        ddlSubMaterial.Items.Insert(0, new ListItem("--子物料--", "C4C58ADE-A9A0-4921-8D55-AB2038713761"));
    }
    private void BindFactoryInspectionLevel()
    {
        ddlFactoryInspectionLevel.Items.Clear();
        //DataTable dt = GetPartType("FactoryInspectionLevel", ddlBroadHeading.SelectedValue, ddlSubDivision.SelectedValue);
        DataTable dt = GetPartType("FactoryInspection", ddlBusinesstype.SelectedValue, ddlBroadHeading.SelectedValue, ddlSubDivision.SelectedValue, ddlSubMaterial.SelectedValue);
        ddlFactoryInspectionLevel.DataSource = dt;
        ddlFactoryInspectionLevel.DataBind();
        ddlFactoryInspectionLevel.Items.Insert(0, new ListItem("--等级--", "17EAFE13-0AA9-4C90-BD74-79EDE1C99082"));
    }

    /// <summary>
    /// 获取供应商物料类型
    /// </summary>
    /// <param name="type">物料分类</param>
    /// <param name="BroadHeadingID">大类区分Guid</param>
    /// <param name="SubDivisionID">细部区分Guid</param>
    /// <returns></returns>
    private DataTable GetPartType(string type, string SupplieTypeID, string BroadHeadingID, string SubDivisionID, string SubMaterialID)
    {
        try
        {
            string str = "sp_supplier_selectPartType";

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@type", type);
            param[1] = new SqlParameter("@BroadHeadingID", BroadHeadingID);
            param[2] = new SqlParameter("@SubDivisionID", SubDivisionID);
            param[3] = new SqlParameter("@SubMaterialID", SubMaterialID);
            param[4] = new SqlParameter("@SupplieTypeID", SupplieTypeID);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    private String GetFactoryInspectionLevel()
    {
        try
        {
            string str = "sp_supplier_selectFactoryInspectionLevel";

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@BroadHeadingID", ddlBroadHeading.SelectedValue);
            param[1] = new SqlParameter("@SubDivisionID", ddlSubDivision.SelectedValue);
            param[2] = new SqlParameter("@SubMaterialID", ddlSubMaterial.SelectedValue);
            param[3] = new SqlParameter("@SupplieTypeID", ddlBusinesstype.SelectedValue);

            return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param).ToString();
        }
        catch
        {
            return "17EAFE13-0AA9-4C90-BD74-79EDE1C99082";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string ChineseSupplierName = txtChineseSupplierName.Text;
        string EnglishSupplierName = txtEnglishSupplierName.Text;
        string ChineseSupplierAddress = txtChineseSupplierAddress.Text;
        string EnglishSupplierAddress = txtEnglishSupplierAddress.Text;
        string SupplierUserName = txtSupplierUserName.Text;
        string SupplierRoleName = txtSupplierRoleName.Text;
        string SupplierNumber = txtSupplierNumber.Text;
        string SupplierPhone = txtSupplierPhone.Text;
        string SupplierFax = txtSupplierFax.Text;
        string SupplierEmail = txtSupplierEmail.Text;
        string SupplierWeb = txtSupplierWeb.Text;
        string SuppNewReason = txtSuppNewReason.Text;
        if (Request.QueryString["type"] == null || Request.QueryString["type"] == "add")
        {
            hidSupplierID.Value = selectSupplierNo();
            if (insertintoSupplierInfo(ChineseSupplierName, EnglishSupplierName, ChineseSupplierAddress, EnglishSupplierAddress, SupplierUserName
                , SupplierRoleName, SupplierNumber, SupplierPhone, SupplierFax, SupplierEmail, SupplierWeb, SuppNewReason))
            {
                ltlAlert.Text = "alert('新增供应商申请失败，请联系管理员！');";
                return;
            }
            else
            {
                //hidSupplierID.Value = selectSupplierNo();
                //添加供应商资质文件明细
                InsertFileQualificationDet(hidSupplierID.Value);
                Response.Redirect("supplier_newApplyList.aspx");
            }
        }
        else if (Request.QueryString["type"] == "edit")
        {
            if (updateSupplierInfo(ChineseSupplierName, EnglishSupplierName, ChineseSupplierAddress, EnglishSupplierAddress, SupplierUserName
                , SupplierRoleName, SupplierNumber, SupplierPhone, SupplierFax, SupplierEmail, SupplierWeb, SuppNewReason))
            {
                ltlAlert.Text = "alert('新增供应商更新失败，请联系管理员！');";
                return;
            }
            else
            {
                Response.Redirect("supplier_newApplyList.aspx");
            }
        }
    }
    private bool updateSupplierInfo(string ChineseSupplierName, string EnglishSupplierName, string ChineseSupplierAddress
        , string EnglishSupplierAddress, string SupplierUserName, string SupplierRoleName, string SupplierNumber
        , string SupplierPhone, string SupplierFax, string SupplierEmail, string SupplierWeb, string SuppNewReason)
    {
        string str = "sp_supplier_updateNewSupplier";

        SqlParameter[] param = new SqlParameter[21];
        param[0] = new SqlParameter("@no", hidSupplierID.Value);

        param[1] = new SqlParameter("@ChineseSupplierName", ChineseSupplierName);
        param[2] = new SqlParameter("@EnglishSupplierName", EnglishSupplierName);
        param[3] = new SqlParameter("@ChineseSupplierAddress", ChineseSupplierAddress);
        param[4] = new SqlParameter("@EnglishSupplierAddress", EnglishSupplierAddress);
        param[5] = new SqlParameter("@SupplierUserName", SupplierUserName);
        param[6] = new SqlParameter("@SupplierRoleName", SupplierRoleName);
        param[7] = new SqlParameter("@SupplierNumber", SupplierNumber);
        param[8] = new SqlParameter("@SupplierPhone", SupplierPhone);
        param[9] = new SqlParameter("@SupplierFax", SupplierFax);
        param[10] = new SqlParameter("@SupplierEmail", SupplierEmail);
        param[11] = new SqlParameter("@SupplierWeb", SupplierWeb);
        param[12] = new SqlParameter("@SuppNewReason", SuppNewReason);

        param[13] = new SqlParameter("@Businesstype", ddlBusinesstype.SelectedItem.ToString());
        param[14] = new SqlParameter("@BusinesstypeID", ddlBusinesstype.SelectedValue);
        param[15] = new SqlParameter("@BroadHeading", ddlBroadHeading.SelectedValue);
        param[16] = new SqlParameter("@SubDivision", ddlSubDivision.SelectedValue);
        param[17] = new SqlParameter("@SubMaterial", ddlSubMaterial.SelectedValue);

        param[18] = new SqlParameter("@uID", Session["uID"].ToString());
        param[19] = new SqlParameter("@uName", Session["uName"].ToString());
        param[20] = new SqlParameter("@FactoryInspection", ddlFactoryInspectionLevel.SelectedValue);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }

    /// <summary>
    /// 新增供应商
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private bool insertintoSupplierInfo(string ChineseSupplierName, string EnglishSupplierName, string ChineseSupplierAddress
        , string EnglishSupplierAddress, string SupplierUserName, string SupplierRoleName, string SupplierNumber
        , string SupplierPhone, string SupplierFax, string SupplierEmail, string SupplierWeb, string SuppNewReason)
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
        //供应商信息
        param[6] = new SqlParameter("@ChineseSupplierName", ChineseSupplierName);
        param[7] = new SqlParameter("@EnglishSupplierName", EnglishSupplierName);
        param[8] = new SqlParameter("@ChineseSupplierAddress", ChineseSupplierAddress);
        param[9] = new SqlParameter("@EnglishSupplierAddress", EnglishSupplierAddress);
        param[10] = new SqlParameter("@SupplierUserName", SupplierUserName);
        param[11] = new SqlParameter("@SupplierRoleName", SupplierRoleName);
        param[12] = new SqlParameter("@SupplierNumber", SupplierNumber);
        param[13] = new SqlParameter("@SupplierPhone", SupplierPhone);
        param[14] = new SqlParameter("@SupplierFax", SupplierFax);
        param[15] = new SqlParameter("@SupplierEmail", SupplierEmail);
        param[16] = new SqlParameter("@SupplierWeb", SupplierWeb);
        param[17] = new SqlParameter("@SuppNewReason", SuppNewReason);
        param[18] = new SqlParameter("@No", hidSupplierID.Value);

        param[19] = new SqlParameter("@Businesstype", ddlBusinesstype.SelectedItem.ToString());
        param[20] = new SqlParameter("@BusinesstypeID", ddlBusinesstype.SelectedValue);
        param[21] = new SqlParameter("@BroadHeading", ddlBroadHeading.SelectedValue);
        param[22] = new SqlParameter("@SubDivision", ddlSubDivision.SelectedValue);
        param[23] = new SqlParameter("@SubMaterial", ddlSubMaterial.SelectedValue);
        param[24] = new SqlParameter("@FactoryInspection", ddlFactoryInspectionLevel.SelectedValue);
        
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, str, param));
    }

    /// <summary>
    /// 获取当前部门领导信息
    /// </summary>
    /// <returns>ID</returns>
    private string GetDeptLeaderID()
    {
        string sql = "select userID from users where roleid = 307 and plantCode = 1 and departmentID =" + Session["deptID"].ToString();

        return SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.Text, sql).ToString();
    }
    /// <summary>
    /// 根据大类区分确定细部区分
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlBroadHeading_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubDivision();
        BindSubMaterial();
        BindFactoryInspectionLevel();
        ddlFactoryInspectionLevel.SelectedValue = GetFactoryInspectionLevel();
    }
    /// <summary>
    /// 根据大类区分和细部区分确定子物料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubMaterial();
        BindFactoryInspectionLevel();
        ddlFactoryInspectionLevel.SelectedValue = GetFactoryInspectionLevel();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        this.Redirect("supplier_newApplyList.aspx");
    }
    protected void ddlSubMaterial_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFactoryInspectionLevel();
        ddlFactoryInspectionLevel.SelectedValue = GetFactoryInspectionLevel();
    }
    protected void ddlBusinesstype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBroadHeading();
        BindSubDivision();
        BindSubMaterial();
        BindFactoryInspectionLevel();
        ddlFactoryInspectionLevel.SelectedValue = GetFactoryInspectionLevel();
    }
}