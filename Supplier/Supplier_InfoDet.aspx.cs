using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class Supplier_Supplier_InfoDet : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbsupplierNo.Text = Request["supplierNo"].ToString();
            hidSupplierNo.Value = Request["supplierNo"].ToString();
            hidSupplierID.Value = Request["supplierID"].ToString();
            bind();


        }
    }

    private void bindddlFileType()
    {
        ddlFileType.Items.Clear();
        ddlFileType.DataSource = selectFileType();
        ddlFileType.DataBind();
    }

    private DataTable selectFileType()
    {
        string sqlstr = "sp_supplier_selectFileType";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr).Tables[0];
    }

    private void bind()
    {
        hidOldNo.Value = "";
        btnAddLinkMan.Visible = true;
        btnModifyLinkMan.Visible = false;
        SqlDataReader sdr = selectInfoBySupplierNo(Request["supplierNo"].ToString(), Request["supplierID"].ToString());
        while (sdr.Read())
        {
            labPlant.Text = sdr["domain"].ToString();
            labDate.Text = sdr["applyDate"].ToString();
            labAPPUserName.Text = sdr["applyName"].ToString();
            labAPPDeptName.Text = sdr["applyDepartment"].ToString();
            TxTChineseSupplierName.Text = sdr["supplierName"].ToString();
            TxTEnglishSupplierName.Text = sdr["supplierEnglish"].ToString();
            txtChineseSupplierAddress.Text = sdr["suppChineseAddress"].ToString();
            txtEnglishSupplierAddress.Text = sdr["suppEnglishAddress"].ToString();
            txtBusinesstype.Text = sdr["SupplieType"].ToString();
            txtBroadHeading.Text = sdr["BroadHeading"].ToString();
            txtSubDivision.Text = sdr["SubDivision"].ToString();
            //txtSupplierfax.Text = sdr[""].ToString();
            labFactoryInspection.Text = sdr["FactoryInspection"].ToString();
            lbTerms.Text = sdr["vd_cr_terms"].ToString();
            lbCurr.Text = sdr["vd_curr"].ToString();
            lbTax.Text = sdr["ad_taxc"].ToString();
            txtRemark.Text = sdr["Remark"].ToString();
        }
        sdr.Dispose();
        sdr.Close();

        bindLinkManGV();
        bindFAgv();
        bindSignedGV();
        bindGvFormal();

        hidTabIndex.Value = "0";

    }

    private void bindSignedGV()
    {
        gvFile.DataSource = selectSignedInfoGVByNo();
        gvFile.DataBind();
        hidTabIndex.Value = "2";
    }

    private DataTable selectSignedInfoGVByNo()
    {
        string sqlstr = "sp_supplier_selectSignedInfoGVByNo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@uID",Session["uID"].ToString())
            ,new SqlParameter("@supplierNo",hidSupplierNo.Value)
            , new SqlParameter("@supplierID",hidSupplierID.Value)
        
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }

    private void bindFAgv()
    {
        bindddlFileType();

        FAgv.DataSource = selectsupplierInfoFQByNo();
        FAgv.DataBind();
        hidTabIndex.Value = "1";
    }

    private DataTable selectsupplierInfoFQByNo()
    {
        string sqlstr = "sp_supplier_selectSupplierInfoFAByNo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@uID",Session["uID"].ToString())
            ,new SqlParameter("@supplierNo",hidSupplierNo.Value)
            , new SqlParameter("@supplierID",hidSupplierID.Value)
        
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    private void bindLinkManGV()
    {
        gvBasisInfo.DataSource = bindLink(Request["supplierNo"].ToString(), Request["supplierID"].ToString());
        gvBasisInfo.DataBind();

        txtLinkNum.Text = string.Empty;
        txtLinkName.Text = string.Empty;
        txtRole.Text = string.Empty;
        txtMobilePhone.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        hidTabIndex.Value = "0";
    }

    private DataTable bindLink(string supplierNo, string supplierID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@supplierNo", supplierNo);
        param[1] = new SqlParameter("@supplierID", supplierID);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_supplier_selectInfoLinkNameByNo", param).Tables[0];
    }

    private int updateSupplierInfo()
    {

        string sqlstr = "sp_supplier_updateSupplierInfo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@supplierNo",Request["supplierNo"].ToString())
            , new SqlParameter("@supplierID",Request["supplierID"].ToString())
            , new SqlParameter("@remark",txtRemark.Text.ToString().Trim())
            , new SqlParameter("@chineseSupplierAddress",txtChineseSupplierAddress.Text.ToString().Trim())
            , new SqlParameter("@englishSupplierAddress",txtEnglishSupplierAddress.Text.ToString().Trim())
            , new SqlParameter("@businesstype",txtBusinesstype.Text.ToString().Trim())
            , new SqlParameter("@broadHeading",txtBroadHeading.Text.ToString().Trim())
            , new SqlParameter("@subDivision",txtSubDivision.Text.ToString().Trim())
            , new SqlParameter("@chineseSupplierName",TxTChineseSupplierName.Text.ToString().Trim())
            , new SqlParameter("@englishSupplierName",TxTEnglishSupplierName.Text.ToString().Trim())
        
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private SqlDataReader selectInfoBySupplierNo(string supplierNo, string supplierID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@supplierNo", supplierNo);
        param[1] = new SqlParameter("@supplierID", supplierID);

        return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_supplier_selectInfoByNo", param);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supplier_InfoList.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        savePageInfo();



    }

    private void savePageInfo()
    {
        int flag = updateSupplierInfo();

        if (flag == 1)
        {
            ltlAlert.Text = "alert('保存成功')";
            bind();
        }
        else
        {
            ltlAlert.Text = "alert('保存失败')";
        }
    }

    /// <summary>
    /// 添加联系人功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddLinkMan_Click(object sender, EventArgs e)
    {

        int test = 0;
        if (!int.TryParse(txtLinkNum.Text.ToString().Trim(), out test))
        {
            Alert("输入的排序号不是数字，请重新输入！");
            return;
        }

        int flag = btnAddLinkManByID();

        if (flag == 2)
        {
            Alert("输入的排序号重复了，请重新输入！");
            return;
        }
        else if (flag == 1)
        {
            Alert("添加成功！");
            bindLinkManGV();
            int flag1 = updateSupplierInfo();

            if (flag1 == 1)
            {
                ltlAlert.Text = "alert('保存成功')";
                bind();
            }
            else
            {
                ltlAlert.Text = "alert('保存失败')";
            }

        }
        else
        {
            Alert("添加失败！请联系管理员！");
            return;
        }
    }

    private int btnAddLinkManByID()
    {
        string sqlstr = "sp_supplier_AddLinkManByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@supplierNo",Request["supplierNo"].ToString())
            , new SqlParameter("@supplierID",Request["supplierID"].ToString())
            , new SqlParameter("@Num",txtLinkNum.Text.ToString().Trim())
            , new SqlParameter("@Name",txtLinkName.Text.ToString().Trim())
            , new SqlParameter("@Role",txtRole.Text.ToString().Trim())
            , new SqlParameter("@MobilePhone",txtMobilePhone.Text.ToString().Trim())
            , new SqlParameter("@Phone",txtPhone.Text.ToString().Trim())
            , new SqlParameter("@Email",txtEmail.Text.ToString().Trim())
            , new SqlParameter("@uID",Session["uID"].ToString())
            , new SqlParameter("@uName",Session["uName"].ToString())

        
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
    protected void btnModifyLinkMan_Click(object sender, EventArgs e)
    {
        int test = 0;
        if (!int.TryParse(txtLinkNum.Text.ToString().Trim(), out test))
        {
            Alert("输入的排序号不是数字，请重新输入！");
            return;
        }

        int flag = btnModifyLinkManByID();

        if (flag == 2)
        {
            Alert("输入的排序号存在重复，请重新输入！");
            return;
        }
        else if (flag == 1)
        {
            Alert("修改成功！");
            btnAddLinkMan.Visible = true;
            btnModifyLinkMan.Visible = false;
            bindLinkManGV();
            savePageInfo();


        }
        else
        {
            Alert("修改失败！请联系管理员！");
            return;


        }
    }


    /// <summary>
    /// 没写完，存储过程没有创建
    /// </summary>
    /// <returns></returns>
    private int btnModifyLinkManByID()
    {
        string sqlstr = "sp_supplier_ModifyLinkManByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@supplierNo",Request["supplierNo"].ToString())
            , new SqlParameter("@supplierID",Request["supplierID"].ToString())
            , new SqlParameter("@Num",txtLinkNum.Text.ToString().Trim())
            , new SqlParameter("@Name",txtLinkName.Text.ToString().Trim())
            , new SqlParameter("@Role",txtRole.Text.ToString().Trim())
            , new SqlParameter("@MobilePhone",txtMobilePhone.Text.ToString().Trim())
            , new SqlParameter("@Phone",txtPhone.Text.ToString().Trim())
            , new SqlParameter("@Email",txtEmail.Text.ToString().Trim())
            , new SqlParameter("@uID",Session["uID"].ToString())
            , new SqlParameter("@uName",Session["uName"].ToString())
            , new SqlParameter("@oldnum",hidOldNo.Value)
        
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
    protected void gvBasisInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnEdit")
        {
            int index = ((GridViewRow)((LinkButton)(e.CommandSource)).Parent.Parent).RowIndex;
            string no = gvBasisInfo.Rows[index].Cells[0].Text.ToString();
            string name = gvBasisInfo.Rows[index].Cells[1].Text.ToString();
            string role = gvBasisInfo.Rows[index].Cells[2].Text.ToString();
            string mobliePhone = gvBasisInfo.Rows[index].Cells[3].Text.ToString();
            string phone = gvBasisInfo.Rows[index].Cells[4].Text.ToString();
            string email = gvBasisInfo.Rows[index].Cells[5].Text.ToString();

            hidOldNo.Value = no;



            txtLinkNum.Text = no;
            txtLinkName.Text = name;
            txtRole.Text = role;
            txtMobilePhone.Text = mobliePhone;
            txtPhone.Text = phone;
            txtEmail.Text = email;

            btnAddLinkMan.Visible = false;
            btnModifyLinkMan.Visible = true;

            hidTabIndex.Value = "0";

        }
        else if (e.CommandName == "lkbtnDelete")
        {
            int index = ((GridViewRow)((LinkButton)(e.CommandSource)).Parent.Parent).RowIndex;
            string no = gvBasisInfo.Rows[index].Cells[0].Text.ToString();


            int flag = deleteLinkByNo(no);

            if (flag == 1)
            {
                Alert("删除成功！");
                bindLinkManGV();
                savePageInfo();
            }
            else
            {
                Alert("删除失败！");
            }

        }
    }

    private int deleteLinkByNo(string no)
    {
        string sqlstr = "sp_supplier_deleteLinkByNo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@supplierNo",Request["supplierNo"].ToString())
            , new SqlParameter("@supplierID",Request["supplierID"].ToString())
            , new SqlParameter("@Num",no)
           
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
    protected void FAgv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = FAgv.DataKeys[intRow].Values["supplier_FilePath"].ToString().Trim();
            string fileName = FAgv.DataKeys[intRow].Values["supplier_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        if (e.CommandName.ToString() == "DeleteFA")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string fileID = FAgv.DataKeys[intRow].Values["supplier_AssetFileID"].ToString().Trim();

            int flag = deleteFAFile(fileID);

            if (flag == 1)
            {
                ltlAlert.Text = "alert('删除成功')";
                bindFAgv();
                savePageInfo();
            }
            else
            {
                Alert("删除失败！");
            }

        }

    }

    private int deleteFAFile(string fileID)
    {
        string sqlstr = "sp_supplier_deleteFAFile";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@fileID",fileID)
            
        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
    protected void FAgv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (FAgv.DataKeys[e.Row.RowIndex].Values["supplier_FilePath"].ToString() == string.Empty)
            {
                e.Row.Cells[4].Text = "无文件";
                e.Row.Cells[4].Enabled = false;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }
            //此处允许查看
            if (FAgv.DataKeys[e.Row.RowIndex].Values["supplier_FileIsEffect"].ToString() == "0")//已过期
            {
                //e.Row.Cells[4].Text = "文件已过期";
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }
            if (FAgv.DataKeys[e.Row.RowIndex].Values["canDelete"].ToString() == "0")
            {
                e.Row.Cells[5].Text = "";
            }

        }
    }
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
                if (!UploadFile(ref _filePath, ref _fileName, FileUpload2))
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
                bindFAgv();
                return;
            }
            filetypename = txtFileTypeName.Text;
        }


        int flag = insertFileAssetDet(hidSupplierID.Value, hidSupplierNo.Value, ddlFileType.SelectedValue, ddlFileType.SelectedItem.ToString()
            , ddlNecessity.SelectedValue, ddlNecessity.SelectedItem.ToString(), _fileName, _filePath, filetypename, _fileEffDate);

        if (flag == 0)
        {
            ltlAlert.Text = "";
            return;
        }
        else
        {
            bind();
            savePageInfo();


        }
    }

    private int insertFileAssetDet(string supplierID, string supplierNo, string fileid, string filrtype,
            string necessityid, string necessity, string FileName, string FilePath, string filetypename, string fileEffDate)
    {
        string sqlstr = "sp_supplier_insertFileAssetInfoDet";


        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@supplierID", supplierID);
        param[1] = new SqlParameter("@supplierNo", supplierNo);
        param[2] = new SqlParameter("@supplier_FileID", fileid);
        param[3] = new SqlParameter("@supplier_FileType", filrtype);
        param[4] = new SqlParameter("@supplier_FileNecessityID", necessityid);
        param[5] = new SqlParameter("@supplier_FileNecessity", necessity);
        param[6] = new SqlParameter("@FileName", FileName);
        param[7] = new SqlParameter("@FilePath", FilePath);
        param[8] = new SqlParameter("@EffectDate", fileEffDate);
        param[9] = new SqlParameter("@FileTypeName", filetypename);
        param[10] = new SqlParameter("@uID", Session["uID"].ToString());
        param[11] = new SqlParameter("@uName", Session["uName"].ToString());



        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));


    }



    private bool UploadFile(ref string filePath, ref string fileName, FileUpload FileUpload)
    {
        string strUserFileName = string.Empty;

        strUserFileName = FileUpload.PostedFile.FileName;//获取上传文件的路径

        //string strUserFileName = FileUpload2.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");//取出最后一个\的位置
        string _fileName = strUserFileName.Substring(flag + 1);//取出文件名

        string path = @"/TecDocs/NewSupplier/" + hidSupplierNo.Value.ToString() + "/";
        string strCatFolder = Server.MapPath(path);//上传需要的虚拟路径
        string attachName = "";
        string attachExtension = "";

        //获取文件的名称和后缀

        attachName = Path.GetFileNameWithoutExtension(FileUpload.PostedFile.FileName);
        attachExtension = Path.GetExtension(FileUpload.PostedFile.FileName);



        //string SaveFileName = System.IO.Path.Combine(strCatFolder, _newFileName);//合并两个路径为上传到服务器上的全路径
        string SaveFileName = System.IO.Path.Combine(strCatFolder, DateTime.Now.ToFileTime().ToString() + attachExtension);//合并两个路径为上传到服务器上的全路径
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('fail to create directory！')";

                return false;
            }
        }

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

            FileUpload.PostedFile.SaveAs(SaveFileName);

        }
        catch
        {
            ltlAlert.Text = "alert('文件上传失败')";
            return false;
        }


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


        filePath = path + docid;
        fileName = _fileName;
        return true;
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
            if (gvFile.DataKeys[e.Row.RowIndex].Values["canDelete"].ToString() == "0")
            {
                e.Row.Cells[5].Text = "";
            }

        }
    }
    protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
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
            if (disApproveForSignedFileStatus(fileID))
            {
                this.Alert("作废失败！");
                return;
            }
            else
            {
                bindSignedGV();
            }
        }
        if (e.CommandName.ToString() == "DeleteFA")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string fileID = gvFile.DataKeys[intRow].Values["SignFile_FileID"].ToString().Trim();

            int flag = deleteSignedFile(fileID);

            if (flag == 1)
            {
                ltlAlert.Text = "alert('删除成功')";
                bindSignedGV();
                savePageInfo();
            }
            else
            {
                Alert("删除失败！");
            }

        }
    }

    private int deleteSignedFile(string fileID)
    {
        string sqlstr = "sp_supplier_deleteSignedFile";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@fileID",fileID)
            
        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private bool disApproveForSignedFileStatus(string fileID)
    {
        string sql = "Update supplier_InfoSignedFileDet Set SignFile_FileStatus = 2 Where SignFile_FileID = '" + fileID + "'";
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql));
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
                if (!UploadFile(ref _filePath, ref _fileName, FileUpload1))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }
                else
                {
                    if (insertSignedFileInfo(hidSupplierID.Value, hidSupplierNo.Value, _fileName, _filePath))
                    {
                        this.Alert("上传成功！");
                        bindSignedGV();
                    }
                    else
                    {


                        this.Alert("上传文件时失败！请联系管理员！");
                        return;
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!请联系管理员！')";
            }
        }
    }

    private bool insertSignedFileInfo(string supplierID, string supplierNo, string fileName, string filePath)
    {
        string sqlstr = "sp_supplier_insertSignedFileInfo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@supplierID", supplierID)
            , new SqlParameter("@supplierNo", supplierNo)
            , new SqlParameter("@fileName", fileName)
            , new SqlParameter("@filePath",filePath)
            , new SqlParameter("@uID",Session["uID"].ToString())
            , new SqlParameter("@uName",Session["uName"].ToString())
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));


    }

    protected void gvFormal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (gvFormal.DataKeys[e.Row.RowIndex].Values["canDelete"].ToString() == "0")
            {
                e.Row.Cells[4].Text = "";
            }

        }
    }
    protected void gvFormal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvFile.DataKeys[intRow].Values["SignFile_FilePath"].ToString().Trim();
            string fileName = gvFile.DataKeys[intRow].Values["SignFile_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        if (e.CommandName.ToString() == "DeleteGF")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string fileID = gvFile.DataKeys[intRow].Values["SignFile_FileID"].ToString().Trim();

            int flag = deleteFormalFile(fileID);

            if (flag == 1)
            {
                ltlAlert.Text = "alert('删除成功')";
                bindGvFormal();
                savePageInfo();
            }
            else
            {
                Alert("删除失败！");
            }

        }
    }

    private int deleteFormalFile(string fileID)
    {
        string sqlstr = "sp_supplier_deleteFormalFile";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@fileID",fileID)
            
        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private void bindGvFormal()
    {
        gvFormal.DataSource = selectFormalInfoGVByNo();
        gvFormal.DataBind();
        hidTabIndex.Value = "3";
    }

    private DataTable selectFormalInfoGVByNo()
    {
        string sqlstr = "sp_supplier_selectFormalInfoGVByNo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@uID",Session["uID"].ToString())
            ,new SqlParameter("@supplierNo",hidSupplierNo.Value)
            , new SqlParameter("@supplierID",hidSupplierID.Value)
        
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        hidTabIndex.Value = "2";
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
                if (!UploadFile(ref _filePath, ref _fileName, FileUpload3))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }
                else
                {
                    if (insertFormalFileInfo(hidSupplierID.Value, hidSupplierNo.Value, _fileName, _filePath))
                    {
                        this.Alert("上传成功！");
                        bindGvFormal();
                    }
                    else
                    {

                        this.Alert("上传文件时失败！请联系管理员！");
                        return;
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!请联系管理员！')";
            }
        }
    }

    private bool insertFormalFileInfo(string supplierID, string supplierNo, string fileName, string filePath)
    {
        string sqlstr = "sp_supplier_insertForFileInfo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@supplierID", supplierID)
            , new SqlParameter("@supplierNo", supplierNo)
            , new SqlParameter("@fileName", fileName)
            , new SqlParameter("@filePath",filePath)
            , new SqlParameter("@uID",Session["uID"].ToString())
            , new SqlParameter("@uName",Session["uName"].ToString())
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
}
