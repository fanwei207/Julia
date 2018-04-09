using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class rmInspection_GuestComplaint_NewSheet : BasePage
{
    string strconn = ConfigurationManager.AppSettings["SqlConn.rmInspection"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindSeverityLevel();
            bindddlGuestLevel();
            bindApproach();
            hidType.Value = Request["type"];
            if (hidType.Value == null)
            {
            }
            else
            {
                if (hidType.Value == "new")
                {
                    hidGuestCompNo.Value = selectGuestCompNo();
                    labGuestComplaintNo.Text = selectGuestCompNo();
                }
                if (hidType.Value == "edit")
                {
                    labGuestComplaintNo.Text = Request["no"].ToString();
                    txtGuestNo.Text = Request["guestNo"].ToString();
                    txtGuestName.Text = Request["cust"].ToString();

                    txtProblemContent.Text = Request["problemContent"].ToString();
                    txtReceivedDate.Text = Request["receivedate"].ToString();
                    ddlSeverity.SelectedValue = Request["severity"].ToString();
                    ddlGuestLevel.SelectedValue = Request["guestLevel"].ToString();
                    string checkedApproach = Request["checkedItems"].ToString();
                    //绑定选中的解决方式
                    DataTable selected = selectCheckedApproach(checkedApproach);
                    foreach (DataRow row in selected.Rows)
                    {
                        string approach = row["ApproachID"].ToString();
                        for (int i = 0; i < radApproach.Items.Count; i++)
                        {
                            if (approach == radApproach.Items[i].Value)
                                radApproach.Items[i].Selected = true;
                        }
                    }

                    //绑定上传过的文件
                    BindFiles();
                    //绑定导入的文件
                    BindImportFiles();

                    btnEdit.Visible = true;
                    btnSubmit.Visible = false;
                }
                
            }
        }
    }

    private void BindImportFiles()
    {
        DataTable dt = getGuestCompImportFiles();
        gvImport.DataSource = dt;
        gvImport.DataBind();
    }

    private DataTable getGuestCompImportFiles()
    {
        string sql = "Select * From comp_importDoc Where GuestComplaintNo = '" + labGuestComplaintNo.Text + "'";

        return SqlHelper.ExecuteDataset(strconn, CommandType.Text, sql).Tables[0];
    }

    private void BindFiles()
    {
        DataTable dt = getGuestCompFileList();
        gv.DataSource = dt;
        gv.DataBind();
    }

    private DataTable selectCheckedApproach(string checkedApproach)
    {
        string str = "sp_comp_selectMarketChecked";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@compNo", Request["no"].ToString());
        param[1] = new SqlParameter("@checkApproach", checkedApproach);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }

    private void bindApproach()
    {
        radApproach.Items.Clear();
        radApproach.DataSource = selectGuestComplainApproach();
        radApproach.DataBind();
    }

    private object selectGuestComplainApproach()
    {
        string sqlstr = "sp_selectGuestComplainApproach";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@approachname", "");

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    private void bindddlGuestLevel()
    {
        ddlGuestLevel.Items.Clear();
        ddlGuestLevel.DataSource = selectGuestLevel();
        ddlGuestLevel.DataBind();
    }

    private object selectGuestLevel()
    {
        string sqlstr = "sp_selectGuestLevel";
        SqlParameter param = new SqlParameter("@levelName", "");

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    private string selectGuestCompNo()
    {
        string sql = "sp_selectGuestCompNo";
        SqlParameter param = new SqlParameter("@uID", Session["uID"].ToString());
        return SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, sql, param).ToString();
    }
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
    private void bindSeverityLevel()
    {
        ddlSeverity.Items.Clear();
        ddlSeverity.DataSource = selectSeverityLevel();
        ddlSeverity.DataBind();
    }

    private DataTable selectSeverityLevel()
    {
        string sqlstr = "sp_selectSeverityLevel";
        SqlParameter param = new SqlParameter("@severityName", "");

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (hidType.Value == "new")
        {
            DataTable dt = getGuestCompImportFiles();
            if(dt.Rows.Count <= 0)
            {
                ltlAlert.Text = "alert('请导入文件')";
                return;
            }
            if(string.IsNullOrEmpty(txtProblemContent.Text))
            {
                ltlAlert.Text = "alert('请填写问题描述')";
                return;
            }
            if (!InsertGuestComplaintMstr())
            {
                ltlAlert.Text = "alert('提交失败，请联系管理员！')";
                DeleteAllUploadFiles();
                return;
            }
            else
            {
                ltlAlert.Text = "alter('提交成功')";
                Semail(hidGuestCompNo.Value.ToString());
                Response.Redirect("../rmInspection/GuestComplaint_SheetList.aspx");               
                return;
            }
        }

    }

    private void DeleteAllUploadFiles()
    {
        string str = "sp_comp_deleteAllUploadFiles";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@GuestComplaintNo", labGuestComplaintNo.Text);

        SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, str, param);
    }

    private bool InsertGuestComplaintMstr()
    {
        string checkedItem = getCheckedItem();
        string str = "sp_InsertGuestComplaintMstr";
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@GuestComplaintNo", labGuestComplaintNo.Text);
        param[1] = new SqlParameter("@guestNo", txtGuestNo.Text);
        param[2] = new SqlParameter("@guestName", txtGuestName.Text);
        param[3] = new SqlParameter("@receivedDate", txtReceivedDate.Text);
        param[4] = new SqlParameter("@severityName", ddlSeverity.SelectedValue.ToString());
        param[5] = new SqlParameter("@problemContent", txtProblemContent.Text);
        param[6] = new SqlParameter("@guestLevel", ddlGuestLevel.SelectedValue.ToString());
        param[7] = new SqlParameter("@approachNames", checkedItem);
        param[8] = new SqlParameter("@uID", Session["uID"].ToString());
        param[9] = new SqlParameter("@uName", Session["uName"].ToString());


        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, str, param));
    }

    private string getCheckedItem()
    {
        string checkedItem = "";
        for (int i = 0; i < radApproach.Items.Count; i++)
        {
            if (radApproach.Items[i].Selected)
            {
                insertApproachDeciding(radApproach.Items[i].Value, labGuestComplaintNo.Text);
                checkedItem += radApproach.Items[i] + ";";
            }
        }
        return checkedItem;
    }

    private void insertApproachDeciding(string approachId, string complaintNo)
    {
        string str = "sp_comp_InsertApproachDeciding";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@approachId", approachId);
        param[1] = new SqlParameter("@complaintNo", complaintNo);

        SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, str, param);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../rmInspection/GuestComplaint_SheetList.aspx");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        DataTable dt = getGuestCompImportFiles();
        if (dt.Rows.Count <= 0)
        {
            ltlAlert.Text = "alert('请导入文件')";
            return;
        }
        if (string.IsNullOrEmpty(txtProblemContent.Text))
        {
            ltlAlert.Text = "alert('请填写问题描述')";
            return;
        }
        if (!UpdateGuestComplaintMstr())
        {
            ltlAlert.Text = "alert('修改失败，请联系管理员！')";
            DeleteAllUploadFiles();
            return;
        }
        else
        {
            ltlAlert.Text = "alert('修改成功')";
            Response.Redirect("../rmInspection/GuestComplaint_SheetList.aspx");
            return;
        }
    }

    private bool UpdateGuestComplaintMstr()
    {
        string checkedItem = getCheckedItem();
        string str = "sp_UpdateGuestComplaintMstr";

        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@GuestComplaintNo", labGuestComplaintNo.Text);
        param[1] = new SqlParameter("@guestNo", txtGuestNo.Text);
        param[2] = new SqlParameter("@guestName", txtGuestName.Text);
        param[3] = new SqlParameter("@receivedDate", txtReceivedDate.Text);
        param[4] = new SqlParameter("@severityName", ddlSeverity.SelectedValue.ToString());
        param[5] = new SqlParameter("@problemContent", txtProblemContent.Text);
        param[6] = new SqlParameter("@guestLevel", ddlGuestLevel.SelectedValue.ToString());
        param[7] = new SqlParameter("@approachNames", checkedItem);
        param[8] = new SqlParameter("@uID", Session["uID"].ToString());
        param[9] = new SqlParameter("@uName", Session["uName"].ToString());


        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, str, param));

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (UpLoadFile(filename))
        {
            if (!InserGuestComplaintFile(labGuestComplaintNo.Text, _fname, _fpath, Session["uID"].ToString(), Session["uName"].ToString()))
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


    private void Bind()
    {
        DataTable dt = getGuestCompFileList();
        gv.DataSource = dt;
        gv.DataBind();
    }

    private DataTable getGuestCompFileList()
    {
        string sql = "Select * From GuestComp_FileList Where GuestComp_No = '" + labGuestComplaintNo.Text + "'";

        return SqlHelper.ExecuteDataset(strconn, CommandType.Text, sql).Tables[0];
    }


    private bool InserGuestComplaintFile(string labGuestComplaintNo, string _fname, string _fpath, string _uID, string _uName)
    {
        string str = "sp_InsertGuestComplaintFile";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@labGuestComplaintNo", labGuestComplaintNo);
        param[1] = new SqlParameter("@fname", _fname);
        param[2] = new SqlParameter("@fpath", _fpath);
        param[3] = new SqlParameter("@uID", _uID);
        param[4] = new SqlParameter("@uName", _uName);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strconn, CommandType.StoredProcedure, str, param));
    }
    private bool UpLoadFile(HtmlInputFile fileID)
    {
        string _uID = Convert.ToString(Session["uID"]);
        string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);

        string strUserFileName = fileID.PostedFile.FileName;//是获取文件的路径，即FileUpload控件文本框中的所有内容，
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string attachExtension = Path.GetExtension(fileID.PostedFile.FileName);
        string _newFileName = DateTime.Now.ToFileTime().ToString() + attachExtension;

        string catPath = @"/TecDocs/GuestComp/";
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
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gv.DataKeys[intRow].Values["GuestComp_FilePath"].ToString().Trim();
            string fileName = gv.DataKeys[intRow].Values["GuestComp_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strDocID = gv.DataKeys[e.RowIndex].Values["ID"].ToString();
        string strPath = gv.DataKeys[e.RowIndex].Values["GuestComp_FilePath"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", strDocID);
        SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_GuestComp_DeleteFile", sqlParam);
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
    protected void btnImport_Click(object sender, EventArgs e)
    {
        ImportExcelFile();
    }

    public void ImportExcelFile()
    {

        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }
        }

        strUserFileName = file1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }

        if (file1.PostedFile != null)
        {
            if (file1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                file1.PostedFile.SaveAs(strFileName);//上传 文件
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {


                    DataTable errDt = null;
                    DataTable dt = null;
                    bool success = false;
                    try
                    {
                        dt = GetExcelContents(strFileName);
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('导入文件必须是Excel格式');";
                        return;
                    }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }
                    }

                    string message = "";
                    try
                    {
                        success = importCompInfo(dt, out message, strUID, out errDt);//导入文件
                    }
                    catch { }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }

                    }
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                        }
                    }
                    else
                    {

                        string title = "100^<b>订单号</b>~^100^<b>行号</b>~^100^<b>物料号</b>~^100^<b>周期章</b>~^100^<b>单位</b>~^100^<b>QAD</b>~^100^<b>价格</b>~^100^<b>币种</b>~^100^<b>位置</b>~^100^<b>数量</b>~^";
                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title, errDt, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('出错');";
                    return;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }
    }

    public bool importCompInfo(DataTable dt, out string message, string uID, out DataTable errorDt)
    {
        message = "";
        errorDt = null;
        dt.TableName = "TempTable";
        bool success = true;

        if (success)
        {
            try
            {
                if (
                    dt.Columns[0].ColumnName != "订单号" ||
                    dt.Columns[1].ColumnName != "行号" ||
                    dt.Columns[2].ColumnName != "物料号" ||
                    dt.Columns[3].ColumnName != "周期章" ||
                    dt.Columns[4].ColumnName != "单位" ||
                    dt.Columns[5].ColumnName != "QAD" ||
                    dt.Columns[6].ColumnName != "价格" ||
                    dt.Columns[7].ColumnName != "币种" ||
                    dt.Columns[8].ColumnName != "位置" ||
                    dt.Columns[9].ColumnName != "数量" ||
                    dt.Columns[10].ColumnName != "备注"
                    )
                {
                    dt.Reset();
                    message = "导入文件的模版不正确，请更新模板再导入!";
                    success = false;
                }

                DataTable TempTable = new DataTable("TempTable");
                DataColumn TempColumn;
                DataRow TempRow;


                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "CustPo";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PoLine";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "CustPart";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "DateCode";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "UM";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Qad";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Price";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Currency";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Location";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "Qty";
                TempTable.Columns.Add(TempColumn);


                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        TempRow = TempTable.NewRow();//创建新的行

                        TempRow["CustPo"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                        TempRow["PoLine"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                        TempRow["CustPart"] = dt.Rows[i].ItemArray[2].ToString().Trim();
                        TempRow["DateCode"] = dt.Rows[i].ItemArray[3].ToString().Trim();
                        TempRow["UM"] = dt.Rows[i].ItemArray[4].ToString().Trim();
                        TempRow["Qad"] = dt.Rows[i].ItemArray[5].ToString().Trim();
                        TempRow["Price"] = dt.Rows[i].ItemArray[6].ToString().Trim();
                        TempRow["Currency"] = dt.Rows[i].ItemArray[7].ToString().Trim();
                        TempRow["Location"] = dt.Rows[i].ItemArray[8].ToString().Trim();
                        TempRow["Qty"] = dt.Rows[i].ItemArray[9].ToString().Trim();

                        TempTable.Rows.Add(TempRow);
                    }

                    StringWriter writer = new StringWriter();
                    TempTable.WriteXml(writer);
                    string xmlDetail = writer.ToString();
                    string GuestCompNo = labGuestComplaintNo.Text;
                    string sqlstr = "sp_comp_importCompInfo";

                    SqlParameter[] param = new SqlParameter[]{
                             new SqlParameter("@detail", xmlDetail)
                             , new SqlParameter("@uID",uID)
                             , new SqlParameter("@compNo",GuestCompNo)
                             , new SqlParameter("@custNo",txtGuestNo.Text)
                                 };

                    errorDt = SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, sqlstr, param).Tables[0];
                    if (errorDt.Rows.Count > 0)
                    {

                        if (errorDt.Rows[0][0].ToString().Equals("1"))
                        {
                            success = true;
                            message = "导入文件成功!";
                            BindImportFiles();
                        }
                        else
                        {
                            message = "导入文件失败!";
                            success = false;
                        }
                    }
                    else
                    {
                        message = "导入文件失败!";
                        success = false;
                    }
                }
            }
            catch
            {
                message = "导入文件失败!";
                success = false;
                throw new Exception();
            }

        }
        return success;

    }
    protected void lkbModle_Click(object sender, EventArgs e)
    {
        string title = "100^<b>订单号</b>~^100^<b>行号</b>~^100^<b>物料号</b>~^100^<b>周期章</b>~^100^<b>单位</b>~^100^<b>QAD</b>~^100^<b>价格</b>~^100^<b>币种</b>~^100^<b>位置</b>~^100^<b>数量</b>~^100^<b>备注</b>~^";

        string[] titleSub = title.Split(new char[] { '~' });

        DataTable dtExcel = new DataTable("temp");
        DataColumn col;

        foreach (string colName in titleSub)
        {
            col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = colName;
            dtExcel.Columns.Add(col);
        }

        ExportExcel(title, dtExcel, false);
    }
    protected void gvImport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ID = gvImport.DataKeys[e.RowIndex].Values["FileID"].ToString();
        
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", ID);

        SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, "sp_Comp_DeleteImportFiles", sqlParam);
        BindImportFiles();
    }
    protected void gvImport_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindImportFiles();
    }
    protected void gvImport_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvImport.EditIndex = -1;
        BindImportFiles();
    }
    protected void gvImport_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = gvImport.DataKeys[e.RowIndex].Values["FileID"].ToString();
        TextBox txtFob = (TextBox)gvImport.Rows[e.RowIndex].Cells[10].Controls[0];

        try
        {
            string strSql = "sp_comp_updateFob";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@fileid", id);
            sqlParam[1] = new SqlParameter("@fob", txtFob.Text.Trim().ToString());
            sqlParam[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strconn, CommandType.StoredProcedure, strSql, sqlParam);

            if (Convert.ToBoolean(sqlParam[2].Value))
            {
                ltlAlert.Text = "alert('更新成功!');";
                return;
            }
            BindImportFiles();
        }
        catch
        {
            throw new Exception("更新失败！");
        }

        gvImport.EditIndex = -1;
        BindImportFiles();
    }
    protected void gvImport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvImport.PageIndex = e.NewPageIndex;
        BindImportFiles();
    }

    private void Semail(string _no)
    {
        DataTable dt = GetEmail(_no, "applyList");
        string mailto = dt.Rows[0]["email"].ToString();
        StringBuilder sb = new StringBuilder();
        string mailSubject = "强凌 - " + dt.Rows[0]["GuestComplaintNo"].ToString() + "-客诉申请通知";
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<html>");
                sb.Append("<form>");
                sb.Append("<br>");
                sb.Append("ALL," + "<br>");
                sb.Append("    下列客诉单我已提交申请! 详细信息如下。" + "<br>");
                sb.Append("<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "客诉单号：" + _no + " ，" + "<br>");
                sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "内容描述：" + dt.Rows[0]["ProblemContent"].ToString() + " ，" + "<br>");                
                sb.Append("详情请登录100系统客诉模块查看，谢谢" + "<br>");
                sb.Append("</body>");
                sb.Append("</form>");
                sb.Append("</html>");
            }

        }
        string moduleName = "preResult";
        DataTable d = getHaveAuthority(moduleName);
        if (d.Rows.Count > 0)
        {
            for (int i = 0; i < d.Rows.Count; i++)
            {
                if (!this.SendEmail(mailto, d.Rows[i]["email"].ToString(), "", mailSubject, sb.ToString()))
                {
                    this.Alert("邮件发送失败！");
                    return;
                }
            }
        }
    }

    private DataTable getHaveAuthority(string moduleName)
    {
        string str = "sp_comp_selectModuleEmail";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@moduleName", moduleName);
        param[1] = new SqlParameter("@remark", "");

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }

    private DataTable GetEmail(string No, string moduleFrom)
    {
        string str = "sp_comp_selectComplaintInfo";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@compNo", No);
        param[1] = new SqlParameter("@uid", Session["uID"]);
        param[2] = new SqlParameter("@moduleName", moduleFrom);

        return SqlHelper.ExecuteDataset(strconn, CommandType.StoredProcedure, str, param).Tables[0];
    }
}