using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;

public partial class EDI_CustComplaint_NewSheet : BasePage
{
    adamClass adam = new adamClass();
    public string _fpath
    {
        get
        {
            return ViewState["fpath"].ToString();
        }
        set
        {
            ViewState["fpath"] = value;
        }
    }
    public string _fname
    {
        get
        {
            return ViewState["fname"].ToString();
        }
        set
        {
            ViewState["fname"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidType.Value = Request["type"];
            if (hidType.Value == null)
            {
            }
            else
            {
                if (hidType.Value == "new")
                {
                    hidCustCompNo.Value = selectCustCompNo();
                }
                if (hidType.Value == "edit")
                {
                    hidID.Value = Request["id"].ToString();
                    hidCustCompNo.Value = Request["no"].ToString();
                    txtCustomer.Text = Request["cust"].ToString();
                    txtOrder.Text = Request["order"].ToString();
                    txtOrder.Enabled = false;
                    ddlOrder.Enabled = false;
                    ddlOrder.SelectedValue = Request["idtype"].ToString();
                    //txtProblemContent.Enabled = false;
                    txtProblemContent.Text = Request["problemContent"].ToString();
                    txtDateCode.Text = Request["datecode"].ToString();
                    txtDueDate.Text = Request["duedate"].ToString();
                    btnEdit.Visible = true;
                    btnSubmit.Visible = false;
                    txtMoney.Text = SelectPaymentMoney().Replace("($)","");
                    if (txtMoney.Text != string.Empty)
                    {
                        chkMoney.Checked = true;
                        hidMoneyStatus.Value = "1";
                    }
                    //if (CheckExistsGoods())
                    //{
                    //    chkGoods.Checked = true;
                    //    hidGoodsStatus.Value = "1";
                    //}
                }
            }
            labCustComplaintNo.Text = hidCustCompNo.Value;
            Bind();
            BindPart();
            BindGoods();
        }
    }

    private bool CheckExistsGoods()
    {
        string str = "sp_CustComp_CheckExistsGoods";
        SqlParameter param = new SqlParameter("@no", hidCustCompNo.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private string SelectPaymentMoney()
    {
        string str = "sp_CustComp_SelectPaymentMoney";
        SqlParameter param = new SqlParameter("@no", hidCustCompNo.Value.ToString());

        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 获取新供应商申请编号
    /// </summary>
    /// <returns></returns>
    private string selectCustCompNo()
    {
        string sql = "sp_CustComp_selectCustCompNo";
        SqlParameter param = new SqlParameter("@uID", Session["uID"].ToString());
        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sql, param).ToString();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (hidType.Value == "new")
        {
            if (!InsertCustComplaintMstr())
            {
                ltlAlert.Text = "alert('提交失败，请联系管理员！')";
                return;
            }
        }
        if (chkMoney.Checked)
        {
            if (txtMoney.Text == string.Empty)
            {
                txtMoney.Text = "0.00000";
            }
            if (!InsertMoneyStatus())
            {
                ltlAlert.Text = "alert('提交失败（赔款），请联系管理员！')";
                return;
            }
        }
        /*
        if (chkGoods.Checked)
        {
            if (!InsertGoodsStatus())
            {
                ltlAlert.Text = "alert('提交失败（退换货），请联系管理员！')";
                return;
            }
        }*/
        Response.Redirect("../rmInspection/CustComplaint_SheetList.aspx");
    }
    private bool InsertMoneyStatus()
    {
        string str = "sp_CustComp_InsertMoneyStatus";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@no", labCustComplaintNo.Text);
        param[1] = new SqlParameter("@uID", Session["uID"].ToString());
        param[2] = new SqlParameter("@uName", Session["uName"].ToString());
        param[3] = new SqlParameter("@money", txtMoney.Text);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool InsertGoodsStatus()
    {
        string str = "sp_CustComp_InsertGoodsStatus";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@no", labCustComplaintNo.Text);
        param[1] = new SqlParameter("@uID", Session["uID"].ToString());
        param[2] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool InsertCustComplaintMstr()
    {
        string str = "sp_CustComp_InsertCustComplaintMstr";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@customer",txtCustomer.Text);
        param[1] = new SqlParameter("@order",txtOrder.Text);
        param[2] = new SqlParameter("@problemContent",txtProblemContent.Text);
        param[3] = new SqlParameter("@custComplaintNo",labCustComplaintNo.Text);
        param[4] = new SqlParameter("@uID",Session["uID"].ToString());
        param[5] = new SqlParameter("@uName", Session["uName"].ToString());
        param[6] = new SqlParameter("@DateCode", txtDateCode.Text);
        param[7] = new SqlParameter("@dueDate", txtDueDate.Text);
        param[8] = new SqlParameter("@idType", ddlOrder.SelectedValue.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure,str,param));
    }

    private bool UpdateCustComplaintMstr()
    {
        string str = "sp_CustComp_UpdateCustComplaintMstr";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@customer", txtCustomer.Text);
        param[1] = new SqlParameter("@order", txtOrder.Text);
        param[2] = new SqlParameter("@problemContent", txtProblemContent.Text);
        param[3] = new SqlParameter("@custComplaintNo", labCustComplaintNo.Text);
        param[4] = new SqlParameter("@uID", Session["uID"].ToString());
        param[5] = new SqlParameter("@uName", Session["uName"].ToString());
        param[6] = new SqlParameter("@DateCode", txtDateCode.Text);
        param[7] = new SqlParameter("@dueDate", txtDueDate.Text);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private void Bind()
    { 
        DataTable dt = getCustCompFileList();
        gv.DataSource = dt;
        gv.DataBind();
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
    private DataTable getCustCompFileList()
    {
        string sql = "Select * From CustComp_FileList Where CustComp_No = '" + labCustComplaintNo.Text + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text,sql).Tables[0];
    }
    private DataTable getPart()
    {
        string sql = "Select ID,CustComp_No,Payment_Type,Payment_Money,Payment_Line,Payment_Part,";
        sql += "Payment_Description,Payment_Qty,Payment_Price,Payment_Total,";
        sql += "Convert(varchar(10), Payment_DetReqDate, 120) Payment_DetReqDate,";
        sql += "Convert(varchar(10), Payment_DetDueDate, 120) Payment_DetDueDate,createBy,";
        sql += "createName,createDate,modifyBy,modifyName,modifyDate,poLine,SID_Site ";
        sql += "From CustComp_PaymentDet ";
        sql += "Where PayMent_Type = 2 And CustComp_No = '" + labCustComplaintNo.Text + "'";
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
        sql += "Where PayMent_Type = 3 And CustComp_No = '" + labCustComplaintNo.Text + "'";
        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (UpLoadFile(filename))
        {
            if (!InsertCustComplaintFile(labCustComplaintNo.Text, _fname, _fpath, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('保存文件信息失败，请联系管理员！')";
                return;
            }
            else
            {
                Bind();
            }
        }
        else
        {
            ltlAlert.Text = "alert('上传文件失败，请联系管理员！')";
            return;
        }
    }
    private bool InsertCustComplaintFile(string labCustComplaintNo, string _fname, string _fpath, string _uID, string _uName)
    {
        string str = "sp_CustComp_InsertCustComplaintFile";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@labCustComplaintNo", labCustComplaintNo);
        param[1] = new SqlParameter("@fname", _fname);
        param[2] = new SqlParameter("@fpath", _fpath);
        param[3] = new SqlParameter("@uID", _uID);
        param[4] = new SqlParameter("@uName", _uName);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 上传文件
    /// </summary>
    protected bool UpLoadFile(HtmlInputFile fileID)
    {
        string _uID = Convert.ToString(Session["uID"]);
        string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);

        string strUserFileName = fileID.PostedFile.FileName;//是获取文件的路径，即FileUpload控件文本框中的所有内容，
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string attachExtension = Path.GetExtension(fileID.PostedFile.FileName);
        string _newFileName = DateTime.Now.ToFileTime().ToString() + attachExtension;

        string catPath = @"/TecDocs/CustComp/";
        string strCatFolder = Server.MapPath(catPath);
        if (!Directory.Exists(strCatFolder))
        {
            Directory.CreateDirectory(strCatFolder);
        }

        string SaveFileName = System.IO.Path.Combine(strCatFolder, _newFileName);//合并两个路径为上传到服务器上的全路径
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
        try
        {
            fileID.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file')";

            return false;
        }

        _fpath = catPath + _newFileName;
        _fname = _fileName;
        return true;
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gv.DataKeys[intRow].Values["CustComp_FilePath"].ToString().Trim();
            string fileName = gv.DataKeys[intRow].Values["CustComp_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strDocID = gv.DataKeys[e.RowIndex].Values["ID"].ToString();
        string strPath = gv.DataKeys[e.RowIndex].Values["CustComp_FilePath"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", strDocID);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_CustComp_DeleteFile", sqlParam);
        try
        {
            File.Delete(strPath);
        }
        catch
        {
            ;
        }

        Bind();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../rmInspection/CustComplaint_SheetList.aspx");
    }
    /// <summary>
    /// 赔料
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (!CheckExistsPart())
        {
            ltlAlert.Text = "alert('物料'" + txtPart.Text + "'在订单" + labCustComplaintNo.Text + "中不存在')";
            return;
        }
        else
        {
            //if (CheckExistsPoLine(txtLine.Text))
            //{
                if (!InserPart())
                {
                    ltlAlert.Text = "alert('物料新增失败，请联系管理员')";
                    return;
                }
                else
                {
                    if (hidType.Value == "new")
                    {
                        if (!InsertCustComplaintMstr())
                        {
                            ltlAlert.Text = "alert('主表保存失败，请联系管理员！')";
                            return;
                        }
                    }
                    BindPart();
                    hidType.Value = "edit";
                    txtOrder.Enabled = false;
                    ddlOrder.Enabled = false;
                }
            //}
            //else
            //{
            //    ltlAlert.Text = "alert('原订单/销售单行号不正确')";
            //    return;
            //}
        }
    }

    /// <summary>
    /// 退换货
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNew1_Click(object sender, EventArgs e)
    {
        if (!CheckExistsPart())
        {
            ltlAlert.Text = "alert('物料'" + txtPart.Text + "'在订单" + labCustComplaintNo.Text + "中不存在')";
            return;
        }
        else
        {
            //if (CheckExistsPoLine(txtLine1.Text))
            //{
                if (!InserGoods())
                {
                    ltlAlert.Text = "alert('物料新增失败，请联系管理员')";
                    return;
                }
                else
                {
                    if (hidType.Value == "new")
                    {
                        if (!InsertCustComplaintMstr())
                        {
                            ltlAlert.Text = "alert('主表保存失败，请联系管理员！')";
                            return;
                        }
                    }
                    BindGoods();
                    hidType.Value = "edit";
                    txtOrder.Enabled = false;
                    ddlOrder.Enabled = false;
                }
            //}
            //else
            //{
            //    ltlAlert.Text = "alert('原订单/销售单行号不正确')";
            //    return;
            //}
        }
    }
    private bool CheckExistsPoLine(string line)
    {
        string str = "sp_CustComp_CheckPoLine"; ;

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@order", txtOrder.Text);
        param[1] = new SqlParameter("@poLine", line);
        if (ddlOrder.SelectedValue == "1")
        {
            param[2] = new SqlParameter("@type", "1");
        }
        else if (ddlOrder.SelectedValue == "2")
        {
            param[2] = new SqlParameter("@type", "2");
        }

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool CheckExistsPart()
    {
        string str = "";
        return true;
    }
    private bool InserGoods()
    {
        string str = "sp_CustComp_InsertGoods";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@no", labCustComplaintNo.Text);
        param[1] = new SqlParameter("@part", txtPart1.Text);
        param[2] = new SqlParameter("@qty", txtQty1.Text);
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@uName", Session["uName"].ToString());
        param[5] = new SqlParameter("@order", txtOrder.Text);
        param[6] = new SqlParameter("@poLine", txtLine1.Text);
        param[7] = new SqlParameter("@IDType", ddlOrder.SelectedValue.ToString());
        param[8] = new SqlParameter("@GoodsDateCode", txtGoodsDateCode.Text);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool InserPart()
    {
        string str = "sp_CustComp_InsertPart";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@no", labCustComplaintNo.Text);
        param[1] = new SqlParameter("@part", txtPart.Text);
        param[2] = new SqlParameter("@qty", txtQty.Text);
        param[3] = new SqlParameter("@ReqDate", txtDetReqDate.Text);
        param[4] = new SqlParameter("@DueDate", txtDetDueDate.Text);
        param[5] = new SqlParameter("@uID", Session["uID"].ToString());
        param[6] = new SqlParameter("@uName", Session["uName"].ToString());
        param[7] = new SqlParameter("@order", txtOrder.Text);
        param[8] = new SqlParameter("@poLine", txtLine.Text);
        param[9] = new SqlParameter("@IDType", ddlOrder.SelectedValue.ToString());
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void gvPart_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strDocID = gvPart.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", strDocID);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_CustComp_DeletePart", sqlParam);

        BindPart();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        if (!UpdateCustComplaintMstr())
        {
            ltlAlert.Text = "alert('修改失败，请联系管理员！')";
            return;
        }
        if (chkMoney.Checked)
        {
            if (hidMoneyStatus.Value.ToString() != "1")
            {
                if (txtMoney.Text == string.Empty)
                {
                    txtMoney.Text = "0.00000";
                }
                if (!InsertMoneyStatus())
                {
                    ltlAlert.Text = "alert('提交失败（赔款），请联系管理员！')";
                    return;
                }
            }
            else
            {
                if (txtMoney.Text != SelectPaymentMoney().Replace("($)", ""))
                {
                    if (!UpdateMoneyRecord())
                    {
                        ltlAlert.Text = "alert('修改失败（赔款），请联系管理员！')";
                        return;
                    }
                }
            }
        }
        else
        {
            if (hidMoneyStatus.Value.ToString() == "1")
            {
                if (!DeleteMoneyRecord())
                {
                    ltlAlert.Text = "alert('修改失败（赔款），请联系管理员！')";
                    return;
                }
            }
        }
        /*
        if (chkGoods.Checked)
        {
            if (hidGoodsStatus.Value.ToString() != "1")
            {
                if (!InsertGoodsStatus())
                {
                    ltlAlert.Text = "alert('提交失败（退换货），请联系管理员！')";
                    return;
                }
            }
        }
        else
        {
            if (hidGoodsStatus.Value.ToString() == "1")
            {
                if (!DeleteGoodsRecord())
                {
                    ltlAlert.Text = "alert('修改失败（退换货），请联系管理员！')";
                    return;
                }
            }
        }
        */

        Response.Redirect("../rmInspection/CustComplaint_SheetList.aspx");
    }
    private bool DeleteGoodsRecord()
    {
        string str = "sp_CustComp_DeleteGoodsRecord";
        SqlParameter param = new SqlParameter("@no", hidCustCompNo.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool DeleteMoneyRecord()
    {
        string str = "sp_CustComp_DeleteMoneyRecord";
        SqlParameter param = new SqlParameter("@no", hidCustCompNo.Value.ToString());
        
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool UpdateMoneyRecord()
    {
        string str = "sp_CustComp_UpdateMoneyRecord";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@no", hidCustCompNo.Value.ToString());
        param[1] = new SqlParameter("@money", txtMoney.Text);
        param[2] = new SqlParameter("@uID", Session["uID"].ToString());
        param[3] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void gvPart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["Payment_DetDueDate"]) == "1900-01-01")
            //{
            //    e.Row.Cells[7].Text = "";
            //}
        }
    }
    protected void ddlOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOrder.SelectedValue.ToString() == "1")
        {
            txtOrder.Text = string.Empty;
            txtOrder.CssClass = "CCPOrder";
        }
        else if (ddlOrder.SelectedValue.ToString() == "2")
        {
            txtOrder.Text = string.Empty;
            txtOrder.CssClass = "CCPSo";
        }
    }
    protected void gvGoods_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvGoods_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strDocID = gvGoods.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", strDocID);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_CustComp_DeleteGoods", sqlParam);

        BindGoods();
    }
}