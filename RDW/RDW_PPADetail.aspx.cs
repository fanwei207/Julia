using Microsoft.ApplicationBlocks.Data;
using RD_WorkFlow;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;

public partial class RDW_RDW_PPADetail : BasePage
{
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_rdw"];
    public string FileUrl;
    RDW rdw = new RDW();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("171011", "PPA 保存、上传权限");
        }

        base.OnInit(e);
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {

            if (Request.QueryString["mstrID"] != null || Request.QueryString["ppa_mstrID"] != null)
            {
                hidMstrId.Value = Request.QueryString["mstrID"] != null ? Request.QueryString["mstrID"] : Request.QueryString["ppa_mstrID"];
            }

            if (Request.QueryString["isView"] != null)
            {
                hidView.Value = "1";
            }
            else
            {
                hidView.Value = "0";
            }
            hidAppv.Value = "0";
            //BindAllGV
            BindAllGV();


            if (Request.QueryString["mstrID"] != null || Request.QueryString["ppa_mstrID"] != null)
            {

                #region Initial ppa_mstr Info
                string mstrID = hidMstrId.Value;
                string isView = hidView.Value;

                string sql = "sp_RDW_SelectPPAMstrInfo";
                SqlParameter[] parm = new SqlParameter[2];
                parm[0] = new SqlParameter("@mstrID", mstrID);
                parm[1] = new SqlParameter("@isView", isView);

                DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, parm).Tables[0];
                if (dt.Rows.Count <= 0)
                {
                    
                    Response.Redirect("/RDW/RDW_PPAList.aspx?error=1");
                    return;
                }
                hidMstrId.Value = dt.Rows[0]["ppa_mstrId"].ToString();
                txt_projID.Text = dt.Rows[0]["ppa_projIdentifier"].ToString();
                txt_clssification.Text = dt.Rows[0]["ppa_Classification"].ToString();
                txt_replacement.Text = dt.Rows[0]["ppa_ReplaStrategy"].ToString();
                txt_description.Text = dt.Rows[0]["ppa_description"].ToString();
                txt_prodCost.Text = dt.Rows[0]["ppa_prodCost"].ToString();
                txt_region.Text = dt.Rows[0]["ppa_region"].ToString();
                txt_forecastFY.Text = dt.Rows[0]["ppa_forecastFY"].ToString();
                txt_keyCustomer.Text = dt.Rows[0]["ppa_keyCustomer"].ToString();
                txt_rpaDate.Text = dt.Rows[0]["ppa_rpaDate"].ToString();
                txt_start.Text = dt.Rows[0]["ppa_energyStar"].ToString();
                txt_competitorInfo.Text = dt.Rows[0]["ppa_competitorSpec"].ToString();
                lbl_imgName.Text = dt.Rows[0]["ppa_competitorPicName"].ToString();
                lbl_imgPath.Text = dt.Rows[0]["ppa_competitorPicPath"].ToString();
                image.ImageUrl = dt.Rows[0]["ppa_competitorPicPath"].ToString();
                txt_PMSignature.Text = dt.Rows[0]["ppa_requestorPMName"].ToString();
                txt_PMDate.Text = dt.Rows[0]["ppa_requestorPMDate"].ToString();
                txt_RDSignature.Text = dt.Rows[0]["ppa_requestorR&DName"].ToString();
                txt_RDDate.Text = dt.Rows[0]["ppa_requestorR&DDate"].ToString();
                txt_DPMSignature.Text = dt.Rows[0]["ppa_reviewDPMName"].ToString();
                txt_DPMDate.Text = dt.Rows[0]["ppa_reviewDPMDate"].ToString();
                txt_EMSignature.Text = dt.Rows[0]["ppa_reviewEMName"].ToString();
                txt_EMDate.Text = dt.Rows[0]["ppa_reviewEMDate"].ToString();
                txt_ReviewComments.Text = dt.Rows[0]["ppa_reviewComments"].ToString();
                txt_ReviewCommentsDate.Text = dt.Rows[0]["ppa_reviewCommentsDate"].ToString();
                txt_IPASignature.Text = dt.Rows[0]["ppa_approverName"].ToString();
                txt_IPASigDate.Text = dt.Rows[0]["ppa_approverDate"].ToString();
                txt_IPADate.Text = dt.Rows[0]["ppa_approverDate"].ToString();
                txt_FPASignature.Text = dt.Rows[0]["ppa_approverCEOName"].ToString();
                txt_FPASigDate.Text = dt.Rows[0]["ppa_approverCEODate"].ToString();
                txt_FPADate.Text = dt.Rows[0]["ppa_approverCEODate"].ToString();
                hidAppv.Value = dt.Rows[0]["inAppv"].ToString().Equals("True")?"1":"0"; 
                FileUrl = dt.Rows[0]["ppa_FileNamePath"].ToString();

                if (dt.Rows[0]["ppa_approverResult"].ToString() == "1")
                    rb_IPAApprove.Checked = true;
                else if (dt.Rows[0]["ppa_approverResult"].ToString() == "2")
                    rb_IPAReject.Checked = true;
                else if (dt.Rows[0]["ppa_approverResult"].ToString() == "3")
                    rb_IPAHold.Checked = true;

                if (dt.Rows[0]["ppa_approverCEOResult"].ToString() == "1")
                    rb_FPAApprove.Checked = true;
                else if (dt.Rows[0]["ppa_approverCEOResult"].ToString() == "2")
                    rb_FPAReject.Checked = true;

                #endregion
                checkISView(dt.Rows[0]["ppa_createBy"].ToString());
                BindUpload();




            }
            else
            {
                checkISView(Session["uID"].ToString());
            }

             
            
        }
    }

    private void checkISView(string uID)
    {
        if (!hidMstrId.Value.Equals(string.Empty))
        {
            //if (!this.Security["171011"].isValid && uID.Equals(Session["uID"].ToString()))
            if (!this.Security["171011"].isValid )
            {
                btn_save.Enabled = false;
                btn_upLoad.Enabled = false;
                btn_Submit.Enabled = false;
                txt_UpLoadPPA.Enabled = false;
                gvUpload.Columns[4].Visible = false;
            }
            if (Request.QueryString["appv"] == null)
            {
                btn_back2.Visible = false;
                btn_back.Visible = false;
                btn_save.Enabled = false;
                btn_Submit.Enabled = false;
                btn_upLoad.Enabled = false;
                txt_UpLoadPPA.Enabled = false;
                gvUpload.Columns[4].Visible = false;
            }
            else if (hidView.Value.Equals("1") || hidAppv.Value.Equals("1"))
            {
                btn_back2.Visible = true;
                btn_back.Visible = true;
                btn_save.Enabled = false;
                btn_Submit.Enabled = false;
                btn_upLoad.Enabled = false;
                txt_UpLoadPPA.Enabled = false;
                gvUpload.Columns[4].Visible = false;
            }
        }
        

    }
    protected void BindInfo()
    {

        #region Initial ppa_mstr Info
        string mstrID = hidMstrId.Value;
        string isView = hidView.Value;
        string sql = "sp_RDW_SelectPPAMstrInfo";
        SqlParameter[] parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@mstrID", mstrID);
        parm[1] = new SqlParameter("@isView", isView);
        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, parm).Tables[0];
        hidMstrId.Value = dt.Rows[0]["ppa_mstrId"].ToString();
        txt_projID.Text = dt.Rows[0]["ppa_projIdentifier"].ToString();
        txt_clssification.Text = dt.Rows[0]["ppa_Classification"].ToString();
        txt_replacement.Text = dt.Rows[0]["ppa_ReplaStrategy"].ToString();
        txt_description.Text = dt.Rows[0]["ppa_description"].ToString();
        txt_prodCost.Text = dt.Rows[0]["ppa_prodCost"].ToString();
        txt_region.Text = dt.Rows[0]["ppa_region"].ToString();
        txt_forecastFY.Text = dt.Rows[0]["ppa_forecastFY"].ToString();
        txt_keyCustomer.Text = dt.Rows[0]["ppa_keyCustomer"].ToString();
        txt_rpaDate.Text = dt.Rows[0]["ppa_rpaDate"].ToString();
        txt_start.Text = dt.Rows[0]["ppa_energyStar"].ToString();
        txt_competitorInfo.Text = dt.Rows[0]["ppa_competitorSpec"].ToString();
        lbl_imgName.Text = dt.Rows[0]["ppa_competitorPicName"].ToString();
        lbl_imgPath.Text = dt.Rows[0]["ppa_competitorPicPath"].ToString();
        image.ImageUrl = dt.Rows[0]["ppa_competitorPicPath"].ToString();
        txt_PMSignature.Text = dt.Rows[0]["ppa_requestorPMName"].ToString();
        txt_PMDate.Text = dt.Rows[0]["ppa_requestorPMDate"].ToString();
        txt_RDSignature.Text = dt.Rows[0]["ppa_requestorR&DName"].ToString();
        txt_RDDate.Text = dt.Rows[0]["ppa_requestorR&DDate"].ToString();
        txt_DPMSignature.Text = dt.Rows[0]["ppa_reviewDPMName"].ToString();
        txt_DPMDate.Text = dt.Rows[0]["ppa_reviewDPMDate"].ToString();
        txt_EMSignature.Text = dt.Rows[0]["ppa_reviewEMName"].ToString();
        txt_EMDate.Text = dt.Rows[0]["ppa_reviewEMDate"].ToString();
        txt_ReviewComments.Text = dt.Rows[0]["ppa_reviewComments"].ToString();
        txt_ReviewCommentsDate.Text = dt.Rows[0]["ppa_reviewCommentsDate"].ToString();
        txt_IPASignature.Text = dt.Rows[0]["ppa_approverName"].ToString();
        txt_IPASigDate.Text = dt.Rows[0]["ppa_approverDate"].ToString();
        txt_IPADate.Text = dt.Rows[0]["ppa_approverDate"].ToString();
        txt_FPASignature.Text = dt.Rows[0]["ppa_approverCEOName"].ToString();
        txt_FPASigDate.Text = dt.Rows[0]["ppa_approverCEODate"].ToString();
        txt_FPADate.Text = dt.Rows[0]["ppa_approverCEODate"].ToString();
        FileUrl = dt.Rows[0]["ppa_FileNamePath"].ToString();

        if (dt.Rows[0]["ppa_approverResult"].ToString() == "1")
            rb_IPAApprove.Checked = true;
        else if (dt.Rows[0]["ppa_approverResult"].ToString() == "2")
            rb_IPAReject.Checked = true;
        else if (dt.Rows[0]["ppa_approverResult"].ToString() == "3")
            rb_IPAHold.Checked = true;

        if (dt.Rows[0]["ppa_approverCEOResult"].ToString() == "1")
            rb_FPAApprove.Checked = true;
        else if (dt.Rows[0]["ppa_approverCEOResult"].ToString() == "2")
            rb_FPAReject.Checked = true;

        #endregion
    }
    protected void BindAllGV()
    {
        //需要一个标志位设定是编辑还是查看
        #region BindAllGV
        string mstrID = hidMstrId.Value;
        string sql = "sp_RDW_selectPPAMetric";
        SqlParameter[] param = new SqlParameter[3];
        string isView = hidView.Value;
        if (Request["from"] == null)
        {
            param[0] = new SqlParameter("@mstrID", mstrID);
        }
        else
        {
            param[0] = new SqlParameter("@mstrID", mstrID);
        }

        param[2] = new SqlParameter("@isView", isView);

        param[1] = new SqlParameter("@type", "Photometric Requirements 光学指标");
        gv_pr.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gv_pr.DataBind();

        param[1] = new SqlParameter("@type", "Life & Lumen Maintenance 寿命及流明稳定性");
        gv_llm.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gv_llm.DataBind();

        param[1] = new SqlParameter("@type", "Electrical Performance Requirements 电气特性指标");
        gv_epr.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gv_epr.DataBind();

        param[1] = new SqlParameter("@type", "Mechanical Requirements 机械结构指标");
        gv_mr.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gv_mr.DataBind();

        param[1] = new SqlParameter("@type", "Dimming Requirements 调光性能指标");
        gv_dr.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gv_dr.DataBind();

        param[1] = new SqlParameter("@type", "Miscellaneous System Requirements 其它的参数要求");
        gv_msr.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gv_msr.DataBind();
        #endregion
    }
    #region upLoad
    protected void btn_upLoad_Click(object sender, EventArgs e)
    {
        if (FileUpload2.Value.Trim() != string.Empty)
        {
            try
            {
                string _filePath = "";
                string _fileName = "";
                if (!UploadFile(ref _filePath, ref _fileName, "image"))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }
                lbl_imgName.Text = _fileName;
                lbl_imgPath.Text = _filePath;
                image.ImageUrl = _filePath;
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!')";
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择上传的图片文件!')";
        }
        BindInfo();

        BindAllGV();
    }
    private bool UploadFile(ref string filePath, ref string fileName,string type)
    {
        string strUserFileName;
        if(type == "image")
            strUserFileName = FileUpload2.PostedFile.FileName;
        else
            strUserFileName = UpLoadFile.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string catPath = @"/TecDocs/RDW/";
        string strCatFolder = Server.MapPath(catPath);
        string attachName="";
        string attachExtension="";

        if (type == "image")
        {
            attachName = Path.GetFileNameWithoutExtension(FileUpload2.PostedFile.FileName);
            attachExtension = Path.GetExtension(FileUpload2.PostedFile.FileName);
        }
        else
        {
            attachName = Path.GetFileNameWithoutExtension(UpLoadFile.PostedFile.FileName);
            attachExtension = Path.GetExtension(UpLoadFile.PostedFile.FileName);
        }

        if (type == "image" && attachExtension.ToUpper() != ".JPG" && attachExtension.ToUpper() != ".GIF"  && attachExtension.ToUpper() != ".PNG" && attachExtension.ToUpper() != ".JPEG" && attachExtension.ToUpper() != ".BMP")
        {
            ltlAlert.Text = "alert('图片格式不正确！')";

            return false;
        }
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../images/"), DateTime.Now.ToFileTime().ToString() + attachExtension);//合并两个路径为上传到服务器上的全路径
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
            if (type == "image")
            {
                FileUpload2.PostedFile.SaveAs(SaveFileName);
            }
            else
            {

                UpLoadFile.PostedFile.SaveAs(SaveFileName);
                FileInfo file = new FileInfo(strUserFileName);
                string ext = file.Extension.ToLower();

                UpLoadFile.PostedFile.SaveAs(SaveFileName);
                if (ext == ".xls" || ext == ".xlsx")
                {
                    GetExcelContents(SaveFileName);
                }
                //GetExcelContent2003s(SaveFileName);
                
            }
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file')";
            BindAllGV();
            BindInfo();
            return false;
        }
        string path = @"/TecDocs/RDW/";

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

    public void GetExcelContents(string excelPath)
    {
        string ext = Path.GetExtension(excelPath);
        if (ext == ".xls")
        {
            GetExcelContent2003(excelPath);
        }
        else
        {
            GetExcelContent2007(excelPath);
        }
        return;
    }


    public void GetExcelContent2003(string excelPath)
    {
        /* typeint 
         * 0:无关信息
         * 1:主表信息
         * 2:标准
         */
        int typeint = 0;
        string[] str = { "", "", "", "", "", "" };
        if (File.Exists(excelPath))
        {
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            FileStream fileStream = new FileStream(excelPath, FileMode.Open);
            IWorkbook workbook = new HSSFWorkbook(fileStream);

            //StreamWriter sw = new StreamWriter("D:\\2.txt");

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheetAt(0);
            StringBuilder content = new StringBuilder();

            //获取sheet的首行
            //IRow headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            //int cellCount = headerRow.LastCellNum;

            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum + 1;

            for (int i = sheet.FirstRowNum; i < rowCount; i++)
            {
                if (typeint != 0)
                {
                    UpdateInfo(str[1], str[2], str[3], str[4], str[5], typeint);
                }
                str[0] = str[1] = str[2] = str[3] = str[4] = str[5] = string.Empty;
                //sw.WriteLine();
                try
                {
                    IRow row = sheet.GetRow(i);

                    for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {
                        //sw.Write("    ");
                        ICell cell = row.GetCell(j);
                        //if (cell != null)
                        //{
                        //    str[j] = cell.StringCellValue;
                        //}
                        //else
                        //{
                        //    str[j] = string.Empty;
                        //}
                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    str[j] = string.Empty;
                                    //sw.Write("");
                                    break;
                                case CellType.String:
                                    str[j] = cell.StringCellValue;
                                    //sw.Write(cell.StringCellValue);
                                    break;
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                                    {
                                        str[j] = cell.DateCellValue.ToString();
                                        //sw.Write(cell.DateCellValue);
                                    }
                                    else
                                    {
                                        str[j] = cell.NumericCellValue.ToString();
                                        //sw.Write(cell.NumericCellValue);
                                    }
                                    break;
                                case CellType.Formula:
                                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                                    //sw.Write(e.Evaluate(cell).StringValue);
                                    break;
                                default:
                                    //sw.Write(cell.ToString());
                                    break;
                            }
                            if (str[j] == "Version 版本")
                            {
                                typeint = 0;
                            }
                            else if (str[j] == "Project Identifier 项目号:" || str[j] == "Program Request 项目申请人" || str[j] == "Responsible Individuals 负责人")
                            {
                                typeint = 1;
                            }
                            else if (str[j] == "Photometric Requirements 光学要求")
                            {
                                typeint = 2;
                            }
                        }
                    }

                }
                catch
                {
                    continue;
                }
            }

            workbook = null;
            sheet = null;

            //sw.Close();
        }
        else
        {

        }
    }
    public void GetExcelContent2007(string file)
    {
        /* typeint 
         * 0:无关信息
         * 1:主表信息
         * 2:标准
         */
        int typeint = 0;
        string[] str = { "", "", "", "", "", "" };
        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {

            XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);

            //StreamWriter sw = new StreamWriter("D:\\8.txt");


            //获取excel的第一个sheet
            ISheet sheet = xssfworkbook.GetSheetAt(0);
            StringBuilder content = new StringBuilder();

            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum + 1;

            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                if (typeint != 0)
                {
                    UpdateInfo(str[1], str[2], str[3], str[4], str[5], typeint);
                }
                str[0] = string.Empty;
                str[1] = string.Empty;
                str[2] = string.Empty;
                str[3] = string.Empty;
                str[4] = string.Empty;
                str[5] = string.Empty;
                //sw.WriteLine();
                try
                {
                    IRow row = sheet.GetRow(i);

                    for (int j = row.FirstCellNum; j < row.LastCellNum; j++)
                    {
                        //sw.Write("    ");
                        ICell cell = row.GetCell(j);
                        //if (cell != null)
                        //{
                        //    str[j] = cell.StringCellValue;
                        //}
                        //else
                        //{
                        //    str[j] = string.Empty;
                        //}
                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    str[j] = string.Empty;
                                    //sw.Write("");
                                    break;
                                case CellType.String:
                                    str[j] = cell.StringCellValue;
                                    //sw.Write(cell.StringCellValue);
                                    break;
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                                    {
                                        str[j] = cell.DateCellValue.ToString();
                                        //sw.Write(cell.DateCellValue);
                                    }
                                    else
                                    {
                                        str[j] = cell.NumericCellValue.ToString();
                                        //sw.Write(cell.NumericCellValue);
                                    }
                                    break;
                                case CellType.Formula:
                                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(xssfworkbook);
                                    //sw.Write(e.Evaluate(cell).StringValue);
                                    break;
                                default:
                                    //sw.Write(cell.ToString());
                                    break;
                            }
                            if (str[j] == "Version 版本")
                            {
                                typeint = 0;
                            }
                            else if (str[j] == "Project Identifier 项目号:" || str[j] == "Program Request 项目申请人" || str[j] == "Responsible Individuals 负责人")
                            {
                                typeint = 1;
                            }
                            else if (str[j] == "Photometric Requirements 光学要求")
                            {
                                typeint = 2;
                            }
                        }
                    }
                }
                catch 
                {
                    continue;  
                }
            }
            xssfworkbook = null;
            sheet = null;

            //sw.Close();
        
        }



        
    }

    public void UpdateInfo(string str1, string str2, string str3, string str4, string str5, int typeint)
    {
        string sql = string.Empty;
        SqlParameter[] param = new SqlParameter[6];
        if (typeint == 1)
        {
            sql = "sp_RDW_updateInfo";
            param[0] = new SqlParameter("@str1", str1);
            param[1] = new SqlParameter("@str2", str2.Trim());
            param[2] = new SqlParameter("@str3", str3.Trim());
            param[3] = new SqlParameter("@str4", str4.Trim());
            param[4] = new SqlParameter("@str5", str5.Trim());
            param[5] = new SqlParameter("@mstrID", hidMstrId.Value);
        }
        else
        {
            sql = "sp_RDW_updateppaInfo";
            param[0] = new SqlParameter("@str1", str1);
            param[1] = new SqlParameter("@str2", str2);
            param[2] = new SqlParameter("@str3", str3);
            param[3] = new SqlParameter("@str4", str4);
            param[4] = new SqlParameter("@str5", str5);
            param[5] = new SqlParameter("@mstrID", hidMstrId.Value);
        }
        try
        {
            //SqlParameter[] param = new SqlParameter[6];
            //param[0] = new SqlParameter("@str1", str1);
            //param[1] = new SqlParameter("@str2", str2);
            //param[2] = new SqlParameter("@str3", str3);
            //param[3] = new SqlParameter("@str4", str4);
            //param[4] = new SqlParameter("@str5", str5);
            //param[5] = new SqlParameter("@mstrID", hidMstrId.Value);

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);
        }
        catch
        {
            return;
        }
            //return;
    }
    #endregion

    protected void btn_back_Click(object sender, EventArgs e)
    {
        string from = Request.QueryString["from"];
        if (from == null || from == "")
        {
            Response.Redirect("/RDW/RDW_PPAList.aspx");
        }
        if (from == "HL")
            Response.Redirect("RDW_HeaderList.aspx");
        else if (from == "ppalist")
        {
            Response.Redirect("/RDW/RDW_PPAList.aspx");
        }
        else
        {
            Response.Redirect("/RDW/RDW_DetailList.aspx?mid=" + Request.QueryString["RDW_mstrID"], true);
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        saveFunction();
    }

    /// <summary>
    /// 将保存的方法放到这里
    /// </summary>
    private void saveFunction()
    {
        #region ppa_mstr Info
        string projID = txt_projID.Text.Trim();
        string classification = txt_clssification.Text.Trim();
        string replacement = txt_replacement.Text.Trim();
        string description = txt_description.Text.Trim();
        string prodCost = txt_prodCost.Text.Trim();
        string region = txt_region.Text.Trim();
        string forecastFY = txt_forecastFY.Text.Trim();
        string keyCustomer = txt_keyCustomer.Text.Trim();
        string rpaDate = txt_rpaDate.Text.Trim();
        string start = txt_start.Text.Trim();
        string competitorInfo = txt_competitorInfo.Text.Trim();

        string PMSignature = txt_PMSignature.Text.Trim();
        string PMDate = txt_PMDate.Text.Trim();
        string RDSignature = txt_RDSignature.Text.Trim();
        string RDDate = txt_RDDate.Text.Trim();
        string DPMSignature = txt_DPMSignature.Text.Trim();
        string DPMDate = txt_DPMDate.Text.Trim();
        string EMSignature = txt_EMSignature.Text.Trim();
        string EMDate = txt_EMDate.Text.Trim();
        string ReviewComments = txt_ReviewComments.Text.Trim();
        string ReviewCommentsDate = txt_ReviewCommentsDate.Text.Trim();
        string IPASignature = txt_IPASignature.Text.Trim();
        string IPASigDate = txt_IPASigDate.Text.Trim();
        string FPASignature = txt_FPASignature.Text.Trim();
        string FPASigDate = txt_FPASigDate.Text.Trim();
        int IPAresult = 0;
        int FPAresult = 0;
        if (rb_IPAApprove.Checked) IPAresult = 1;
        else if (rb_IPAReject.Checked) IPAresult = 2;
        else if (rb_IPAHold.Checked) IPAresult = 3;
        if (rb_FPAApprove.Checked) FPAresult = 1;
        else if (rb_FPAReject.Checked) FPAresult = 2;
        #endregion

        if (string.IsNullOrEmpty(hidMstrId.Value))
        {
            #region NEW PPA
            //if (projID == "")
            //{
            //    ltlAlert.Text = "alert('The Project Identifier can not be empty'); ";
            //    return;
            //}
            //else if (!rdw.CheckExistsPPA(projID))
            //{
            //    ltlAlert.Text = "alert('The Project Identifier already exists'); ";
            //    return;
            //}
            string sql = "sp_RDW_InsertPPAMstr";
            try
            {
                int mid;
                string msid;
                if (hidMstrId.Value == "")
                {
                    mid = 0;
                    msid = Guid.NewGuid().ToString();
                }
                else
                {
                    mid = 1;
                    msid = hidMstrId.Value;
                }
                SqlParameter[] parm = new SqlParameter[7];
                parm[0] = new SqlParameter("@projID", projID);
                parm[1] = new SqlParameter("@retValue", DbType.Int32);
                parm[1].Direction = ParameterDirection.Output;
                parm[2] = new SqlParameter("@mID", mid);
                parm[3] = new SqlParameter("@mstrID", msid);
                parm[4] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
                parm[5] = new SqlParameter("@uName", Session["uName"].ToString());
                parm[6] = new SqlParameter("@retMstrID", SqlDbType.UniqueIdentifier);
                parm[6].Direction = ParameterDirection.Output;

                string qq = hidMstrId.Value;

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, parm);
                hidMstrId.Value = parm[6].Value.ToString();

                if (parm[1].Value.ToString() == "0")
                {
                    ltlAlert.Text = "alert('Save Data Error'); ";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('Save Data Error'); ";
                return;
            }
            #endregion

            #region Save ppa_det Info
            int i = 0;
            sql = "";

            for (i = 0; i < gv_pr.Rows.Count; i++)
            {
                string mstrid = hidMstrId.Value.ToString();
                string detid = gv_pr.DataKeys[i]["ppa_detID"].ToString();
                string type = gv_pr.DataKeys[i]["ppa_type"].ToString();
                string metric = gv_pr.DataKeys[i]["ppa_metric"].ToString();
                string metricID = gv_pr.DataKeys[i]["ppa_ID"].ToString();
                string sort = gv_pr.DataKeys[i]["ppa_sort"].ToString();

                if (InsertPPADet(mstrid, detid, type, metric, metricID, sort) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_llm.Rows.Count; i++)
            {
                string mstrid = hidMstrId.Value.ToString();
                string detid = gv_llm.DataKeys[i]["ppa_detID"].ToString();
                string type = gv_llm.DataKeys[i]["ppa_type"].ToString();
                string metric = gv_llm.DataKeys[i]["ppa_metric"].ToString();
                string metricID = gv_llm.DataKeys[i]["ppa_ID"].ToString();
                string sort = gv_llm.DataKeys[i]["ppa_sort"].ToString();

                if (InsertPPADet(mstrid, detid, type, metric, metricID, sort) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_epr.Rows.Count; i++)
            {
                string mstrid = hidMstrId.Value.ToString();
                string detid = gv_epr.DataKeys[i]["ppa_detID"].ToString();
                string type = gv_epr.DataKeys[i]["ppa_type"].ToString();
                string metric = gv_epr.DataKeys[i]["ppa_metric"].ToString();
                string metricID = gv_epr.DataKeys[i]["ppa_ID"].ToString();
                string sort = gv_epr.DataKeys[i]["ppa_sort"].ToString();

                if (InsertPPADet(mstrid, detid, type, metric, metricID, sort) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_mr.Rows.Count; i++)
            {
                string mstrid = hidMstrId.Value.ToString();
                string detid = gv_mr.DataKeys[i]["ppa_detID"].ToString();
                string type = gv_mr.DataKeys[i]["ppa_type"].ToString();
                string metric = gv_mr.DataKeys[i]["ppa_metric"].ToString();
                string metricID = gv_mr.DataKeys[i]["ppa_ID"].ToString();
                string sort = gv_mr.DataKeys[i]["ppa_sort"].ToString();

                if (InsertPPADet(mstrid, detid, type, metric, metricID, sort) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_dr.Rows.Count; i++)
            {
                string mstrid = hidMstrId.Value.ToString();
                string detid = gv_dr.DataKeys[i]["ppa_detID"].ToString();
                string type = gv_dr.DataKeys[i]["ppa_type"].ToString();
                string metric = gv_dr.DataKeys[i]["ppa_metric"].ToString();
                string metricID = gv_dr.DataKeys[i]["ppa_ID"].ToString();
                string sort = gv_dr.DataKeys[i]["ppa_sort"].ToString();

                if (InsertPPADet(mstrid, detid, type, metric, metricID, sort) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_msr.Rows.Count; i++)
            {
                string mstrid = hidMstrId.Value.ToString();
                string detid = gv_msr.DataKeys[i]["ppa_detID"].ToString();
                string type = gv_msr.DataKeys[i]["ppa_type"].ToString();
                string metric = gv_msr.DataKeys[i]["ppa_metric"].ToString();
                string metricID = gv_msr.DataKeys[i]["ppa_ID"].ToString();
                string sort = gv_msr.DataKeys[i]["ppa_sort"].ToString();

                if (InsertPPADet(mstrid, detid, type, metric, metricID, sort) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            #endregion
        }
        else
        {
            #region Save ppa_mstr Info
            string sql = "sp_RDW_UpdatePPAMstr";
            try
            {
                SqlParameter[] parm = new SqlParameter[33];
                parm[0] = new SqlParameter("@projID", projID);
                parm[1] = new SqlParameter("@classification", classification);
                parm[2] = new SqlParameter("@replacement", replacement);
                parm[3] = new SqlParameter("@description", description);
                parm[4] = new SqlParameter("@prodCost", prodCost);
                parm[5] = new SqlParameter("@region", region);
                parm[6] = new SqlParameter("@forecastFY", forecastFY);
                parm[7] = new SqlParameter("@keyCustomer", keyCustomer);
                parm[8] = new SqlParameter("@rpaDate", rpaDate);
                parm[9] = new SqlParameter("@start", start);
                parm[10] = new SqlParameter("@competitorInfo", competitorInfo);
                parm[11] = new SqlParameter("@PMSignature", PMSignature);
                parm[12] = new SqlParameter("@PMDate", PMDate);
                parm[13] = new SqlParameter("@RDSignature", RDSignature);
                parm[14] = new SqlParameter("@RDDate", RDDate);
                parm[15] = new SqlParameter("@DPMSignature", DPMSignature);
                parm[16] = new SqlParameter("@DPMDate", DPMDate);
                parm[17] = new SqlParameter("@EMSignature", EMSignature);
                parm[18] = new SqlParameter("@EMDate", EMDate);
                parm[19] = new SqlParameter("@ReviewComments", ReviewComments);
                parm[20] = new SqlParameter("@ReviewCommentsDate", ReviewCommentsDate);
                parm[21] = new SqlParameter("@IPASignature", IPASignature);
                parm[22] = new SqlParameter("@IPASigDate", IPASigDate);
                parm[23] = new SqlParameter("@FPASignature", FPASignature);
                parm[24] = new SqlParameter("@FPASigDate", FPASigDate);
                parm[25] = new SqlParameter("@IPAresult", Convert.ToInt32(IPAresult));
                parm[26] = new SqlParameter("@FPAresult", Convert.ToInt32(FPAresult));
                parm[27] = new SqlParameter("@retValue", DbType.Int32);
                parm[27].Direction = ParameterDirection.Output;
                //parm[28] = new SqlParameter("@mstrID", Request.QueryString["mstrID"]);
                parm[28] = new SqlParameter("@mstrID", hidMstrId.Value.ToString());
                parm[29] = new SqlParameter("@imgName", lbl_imgName.Text.Trim());
                parm[30] = new SqlParameter("@imgPath", lbl_imgPath.Text.Trim());
                parm[31] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
                parm[32] = new SqlParameter("@uName", Session["uName"].ToString());

                SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, parm);

                if (parm[27].Value.ToString() == "0")
                {
                    ltlAlert.Text = "alert('Save Data Error'); ";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('Save Data Error'); ";
                return;
            }



            #endregion

            #region Save ppa_det Info
            int i = 0;
            sql = "";

            for (i = 0; i < gv_pr.Rows.Count; i++)
            {
                string target = ((TextBox)gv_pr.Rows[i].FindControl("txt_prTarget")).Text.Trim();
                string actual = (gv_pr.Rows[i].FindControl("txt_prActual") as TextBox).Text.Trim();
                string comments = (gv_pr.Rows[i].FindControl("txt_prComments") as TextBox).Text.Trim();
                string isPass = (gv_pr.Rows[i].FindControl("chk_prIsPass") as CheckBox).Checked.ToString();
                string detID = gv_pr.DataKeys[i]["ppa_detID"].ToString();
                if (UpDatePPADet(actual, target, comments, isPass, detID) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_llm.Rows.Count; i++)
            {
                string target = (gv_llm.Rows[i].FindControl("txt_llmTarget") as TextBox).Text.Trim();
                string actual = (gv_llm.Rows[i].FindControl("txt_llmActual") as TextBox).Text.Trim();
                string comments = (gv_llm.Rows[i].FindControl("txt_llmComments") as TextBox).Text.Trim();
                string isPass = (gv_llm.Rows[i].FindControl("chk_llmIsPass") as CheckBox).Checked.ToString();
                string detID = gv_llm.DataKeys[i]["ppa_detID"].ToString();
                if (UpDatePPADet(actual, target, comments, isPass, detID) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_epr.Rows.Count; i++)
            {
                string target = (gv_epr.Rows[i].FindControl("txt_eprTarget") as TextBox).Text.Trim();
                string actual = (gv_epr.Rows[i].FindControl("txt_eprActual") as TextBox).Text.Trim();
                string comments = (gv_epr.Rows[i].FindControl("txt_eprComments") as TextBox).Text.Trim();
                string isPass = (gv_epr.Rows[i].FindControl("chk_eprIsPass") as CheckBox).Checked.ToString();
                string detID = gv_epr.DataKeys[i]["ppa_detID"].ToString();
                if (UpDatePPADet(actual, target, comments, isPass, detID) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_mr.Rows.Count; i++)
            {
                string target = (gv_mr.Rows[i].FindControl("txt_mrTarget") as TextBox).Text.Trim();
                string actual = (gv_mr.Rows[i].FindControl("txt_mrActual") as TextBox).Text.Trim();
                string comments = (gv_mr.Rows[i].FindControl("txt_mrComments") as TextBox).Text.Trim();
                string isPass = (gv_mr.Rows[i].FindControl("chk_mrIsPass") as CheckBox).Checked.ToString();
                string detID = gv_mr.DataKeys[i]["ppa_detID"].ToString();
                if (UpDatePPADet(actual, target, comments, isPass, detID) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_dr.Rows.Count; i++)
            {
                string target = (gv_dr.Rows[i].FindControl("txt_drTarget") as TextBox).Text.Trim();
                string actual = (gv_dr.Rows[i].FindControl("txt_drActual") as TextBox).Text.Trim();
                string comments = (gv_dr.Rows[i].FindControl("txt_drComments") as TextBox).Text.Trim();
                string isPass = (gv_dr.Rows[i].FindControl("chk_drIsPass") as CheckBox).Checked.ToString();
                string detID = gv_dr.DataKeys[i]["ppa_detID"].ToString();
                if (UpDatePPADet(actual, target, comments, isPass, detID) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }

            for (i = 0; i < gv_msr.Rows.Count; i++)
            {
                string target = (gv_msr.Rows[i].FindControl("txt_msrTarget") as TextBox).Text.Trim();
                string actual = (gv_msr.Rows[i].FindControl("txt_msrActual") as TextBox).Text.Trim();
                string comments = (gv_msr.Rows[i].FindControl("txt_msrComments") as TextBox).Text.Trim();
                string isPass = (gv_msr.Rows[i].FindControl("chk_msrIsPass") as CheckBox).Checked.ToString();
                string detID = gv_msr.DataKeys[i]["ppa_detID"].ToString();
                if (UpDatePPADet(actual, target, comments, isPass, detID) != "1")
                {
                    ltlAlert.Text = "alert('Save Detail Data Error'); ";
                    return;
                }
            }
            string ee = hidMstrId.Value;
            #endregion
        }

    }

    protected string InsertPPADet(string mstrid, string detid, string type, string metric, string metricID, string sort)
    {
        string sql = "sp_RDW_insertPPADet";
        try
        {
            SqlParameter[] parm = new SqlParameter[9];
            parm[0] = new SqlParameter("@mstrid", mstrid);
            parm[1] = new SqlParameter("@detid", detid);
            parm[2] = new SqlParameter("@type", type);
            parm[3] = new SqlParameter("@metric", metric);

            parm[4] = new SqlParameter("@metricID", metricID);
            parm[5] = new SqlParameter("@sort", sort);

            parm[6] = new SqlParameter("@retValue", DbType.Int32);
            parm[6].Direction = ParameterDirection.Output;
            parm[7] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
            parm[8] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, parm);

            return parm[6].Value.ToString();
        }
        catch
        {
            return "0";
        }

    }
    protected string UpDatePPADet( string actual,string target,string comments,string isPass,string detID)
    {
        string sql = "sp_RDW_UpdatePPADet";
        try
        {
            SqlParameter[] parm = new SqlParameter[8];
            parm[0] = new SqlParameter("@actual", actual);
            parm[1] = new SqlParameter("@target", target);
            parm[2] = new SqlParameter("@comments", comments);
            parm[3] = new SqlParameter("@isPass", isPass);
            parm[4] = new SqlParameter("@detID", detID);
            parm[5] = new SqlParameter("@retValue", DbType.Int32);
            parm[5].Direction = ParameterDirection.Output;
            parm[6] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
            parm[7] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, parm);

            return parm[5].Value.ToString();
        }
        catch
        {
            return "0";
        }

    }


    //UpLoad PPA File
    protected void txt_UpLoadPPA_Click(object sender, EventArgs e)
    {
        if (UpLoadFile.Value.Trim() != string.Empty)
        {
            try
            {
                string _filePath = "";
                string _fileName = "";
                btn_save_Click(sender, e);
                if (!UploadFile(ref _filePath, ref _fileName, "file"))
                {
                    this.Alert("上传文件时失败！请联系管理员！");
                    return;
                }

                //FileUrl = _filePath;
                string _uID = Convert.ToString(Session["uID"]);
                string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
                if (hidMstrId.Value == "")
                {
                    hidMstrId.Value = Guid.NewGuid().ToString();
                }
                //if (txt_projID.Text == string.Empty)
                //{
                //    txt_projID.Text = _fileName.Substring(0, _fileName.LastIndexOf("."));
                //}
                UploadPPADoc(hidMstrId.Value, _fileName, _filePath, _uName, _uID);
                BindUpload();
                BindAllGV();
                BindInfo();
                
            }
            catch
            {
                ltlAlert.Text = "alert('上传失败!')";
            }
        }
        else
        {
            ltlAlert.Text = "alert('上传失败!!')";
        }
    }

    private void UploadPPADoc(string mstrId, string fileName, string path, string uName, string uID)
    {

        SqlParameter[] sqlParam = new SqlParameter[5];
        sqlParam[0] = new SqlParameter("@MstrId", mstrId);
        sqlParam[1] = new SqlParameter("@Name", fileName);
        sqlParam[2] = new SqlParameter("@Path", path);
        sqlParam[3] = new SqlParameter("@Uploader", uName);
        sqlParam[4] = new SqlParameter("@Uid", uID);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_ppa_insertDocs", sqlParam);
    }

    private void BindUpload()
    {
        string isView = hidView.Value;
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@mstrId", hidMstrId.Value);
        param[1] = new SqlParameter("@isView", isView);

        DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_ppa_selectDocs", param);

        gvUpload.DataSource = ds;
        gvUpload.DataBind();
    }

    protected void gvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvUpload.DataKeys[intRow].Values["ppa_path"].ToString().Trim();
            string fileName = gvUpload.DataKeys[intRow].Values["ppa_fileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }

    protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            if (gvUpload.DataKeys[e.Row.RowIndex].Values["ppa_createdBy"].ToString() != Session["uID"].ToString())
            {
                btnDelete.Visible = false;
            }
        }
    }

    protected void gvUpload_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strDocID = gvUpload.DataKeys[e.RowIndex].Values["ppa_docId"].ToString();
        string strPath = gvUpload.DataKeys[e.RowIndex].Values["ppa_path"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@docId", strDocID);
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_ppa_deleteDoc", sqlParam);
        //try
        //{
        //    File.Delete(strPath);
        //}
        //catch
        //{
        //    ;
        //}

        BindUpload();
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        saveFunction();

        //提交审批流
        string mstrID = hidMstrId.Value;

        string sqlstr = "sp_ppa_submitPPAtoAppv";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            ,new SqlParameter("@uID",Session["uID"].ToString())
            ,new SqlParameter("@uName",Session["uName"].ToString()) 
        };

        int flag = Convert.ToInt32(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param));

        if (flag == 1)
        {
            ltlAlert.Text = "alert('提交成功!Submit successfully')";
            btn_save.Enabled = false;
            btn_Submit.Enabled = false;
            btn_upLoad.Enabled = false;
            txt_UpLoadPPA.Enabled = false;
            gvUpload.Columns[4].Visible = false;
        }
        else if (flag == 2)
        {
            ltlAlert.Text = "alert('审批未完成。请勿重复提交！Approval not completed. Please do not repeat!')";
        }
        else if (flag == 3)
        {
            ltlAlert.Text = "alert('审批已通过。请勿重复提交!Approval has been passed. Please do not repeat!')";
        }
        else if (flag == 4)
        {
            ltlAlert.Text = "alert('申请提交的项目必须有项目名！The project submitted must have the name of the project!')";
        }
        else if (flag == 5)
        {
            ltlAlert.Text = "alert('您申请的项目名存在正在审批或者已经通过的PPA！The name of the project you are applying for has been approved or has been approved by the PPA!')";
        }
        else
        {
            ltlAlert.Text = "alert('提交失败!请联系管理员。Submission failed! Please contact administrator.')";
        }

    }
}