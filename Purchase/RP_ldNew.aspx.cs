using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using CommClass;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;

public partial class Purchase_RP_ldNew : BasePage
{
    private adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            getType();
            if (Request.QueryString["id"] != null)
            {
                lbl_id.Text = Request.QueryString["id"];

                BindMstrData(Request.QueryString["supper"]);
            }
            else
            {
                txt_status.Text = "新增";
                this.trReview.Visible = false;
                this.trApply.Visible = true;
                this.txtReason.ReadOnly = true;
                //btn_Save.Visible = true;
                btn_Submit.Visible = false;
                btn_Cancel.Visible = false;
                trReason.Visible = false;
                trUpload.Visible = false;
                trUpload1.Visible = false;
                //this.txt_prodCode.ReadOnly = false;
                this.txtRmks.ReadOnly = false;
                this.trUpload2.Visible = false;
                this.trUpload3.Visible = false;
                this.trUpload4.Visible = false;
                this.trUpload5.Visible = false;
                getsite();
            }
        }


    }
    public void getsite()
    {
        if (Session["PlantCode"].ToString() == "1")
        {
            txtsite.Text = "1000";
        }
        else if (Session["PlantCode"].ToString() == "2")
        {
            txtsite.Text = "2000";
        }
        else if (Session["PlantCode"].ToString() == "5")
        {
            txtsite.Text = "4000";
        }
        else if (Session["PlantCode"].ToString() == "8")
        {
            txtsite.Text = "5000";
        }
    }
    public void getType()
    {
        try
        {
            

            string id  = string.Empty;

            string sqlstr = "sp_rp_getDepartmentByDomainOrCreateBy";

            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
            }

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter ("@id",id)
                ,new SqlParameter("@plantCode",Session["PlantCode"].ToString())
            
            };


            ddldepartment.DataSource = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr,param);

            ddldepartment.DataBind();
         

          

        }
        catch (Exception ex)
        {

        }
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {

        //DeleteProductStruApplyDetail(lbl_id.Text);
        //DeleteProductStruApply_NewProduct(lbl_id.Text);
        //ImportExcelFile();

        if (ImportExcelFile())
        {
            if (checkTemp())
            {
                if (InsertBatchTemp(Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    ltlAlert.Text = "alert('导入成功!');";
                    btn_Submit.Enabled = true;
                }
                else
                {
                    ltlAlert.Text = "alert('导入失败!');";
                }
            }

        }
        BindMstrData(issupper(lbl_id.Text));
        //BindMstrData();
        //this.Alert("导入成功！");
        //btn_Submit.Enabled = true;
    }
    private bool InsertBatchTemp(string uID, string uName)
    {
        try
        {
            SqlParameter[] sqlParam = new SqlParameter[4];
            sqlParam[0] = new SqlParameter("@uID", uID);
            sqlParam[1] = new SqlParameter("@uName", uName);
            sqlParam[3] = new SqlParameter("@mstr_id", lbl_id.Text);
            sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_insertlddet", sqlParam);

            return Convert.ToBoolean(sqlParam[2].Value);
        }
        catch
        {
            return false;
        }
    }
    private bool checkTemp()
    {
        SqlParameter[] parm = new SqlParameter[2];
        parm[0] = new SqlParameter("@uID", Session["uID"].ToString());
        parm[1] = new SqlParameter("@mstrid", lbl_id.Text);
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_Checkldtemp", parm);

        string strSql2 = "select * from RP_ld_temp where isnull( errMsg ,'') <>''";
        DataSet ds2;
        try
        {
            ds2 = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql2);
            if (ds2 != null && ds2.Tables[0].Rows.Count > 0)
            {
                string title = "100^<b>行号</b>~^100^<b>QAD</b>~^100^<b>数量</b>~^100^<b>errMsg</b>~^";

                string sql = " select line,[QAD],[num],[errMsg] from RP_ld_temp where createby = " + Session["uID"].ToString();

                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
                //ltlAlert.Text = "alert('导入失败!');";
                ExportExcel(title, dt, false);
                return false;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Submission information verification failed! \\n 提交信息验证失败！');Form1.usercode.focus();";
            return false;
        }
        return true;
    }
    public Boolean ImportExcelFile()
    {
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int line = 0;
        #region 上传文档例行处理
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
                return false;
            }

        }

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return false;
        }

        strUserFileName = strFileName;

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
        #endregion

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return false;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return false;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    dt = this.GetExcelContents(strFileName);
                    //ds = chk.getExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('导入文件必须是Excel格式!');";
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    /*
                     *  导入的Excel文件必须满足：
                     *      1、至少应该有五列
                     *      2、从第五列开始即视为工序
                     *      3、工序名称必须在wo2_mop中存在
                    */



                    #region Excel的列名必须保持一致
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        if (col == 0 && dt.Columns[col].ColumnName.Trim() != "行号")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 行号！');";
                            return false;
                        }

                        if (col == 1 && dt.Columns[col].ColumnName.Trim() != "QAD")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 QAD！');";
                            return false;
                        }
                        else if (col == 2 && dt.Columns[col].ColumnName.Trim() != "数量")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 数量！');";
                            return false;
                        }
                        else if (col == 3 && dt.Columns[col].ColumnName.Trim() != "地点")
                        {
                            dt.Reset();
                            ltlAlert.Text = "alert('该文件的第" + col.ToString() + "列必须是 地点！');";
                            return false;
                        }


                    }
                    #endregion
                    //构建ImportError
                    DataColumn column;


                    //DataRow rowError;//错误表的行

                    //转换成模板格式
                    DataTable table = new DataTable("RP_ld_temp");

                    DataRow row;

                    #region 定义表列
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "mstr_id";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "line";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "QAD";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "num";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "site";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createby";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "CreateName";
                    table.Columns.Add(column);


                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errMsg";
                    table.Columns.Add(column);
                    #endregion

                    int _uID = Convert.ToInt32(Session["uID"]);
                    string strerror = "";

                    if (DeleteProductStruApplyDetail(lbl_id.Text))
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            line = line + 1;
                            row = table.NewRow();

                            #region 赋值、长度判定
                            if (r["行号"].ToString().Trim().Length == 0)
                            {
                                strerror += "行号不能为空.";
                            }

                          
                                if (r["QAD"].ToString().Trim().Length == 0)
                                {
                                    strerror += "QAD不能为空.";
                                }
                                else
                                {
                                    string strSql2 = " SELECT * FROM QAD_Data..ld_det WHERE ld_part ='" + r["QAD"].ToString().Trim() + "'" + "and ld_site = " + r["地点"].ToString().Trim();
                                    DataSet ds2;
                                    ds2 = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql2);
                                    if (!(ds2 != null && ds2.Tables[0].Rows.Count > 0))
                                    {
                                        strerror += "库存QAD不存在.";
                                    }
                                }

                                if (r["数量"].ToString().Trim().Length == 0)
                            {
                                strerror += "数量不能为空.";

                            }
                                if (r["地点"].ToString().Trim().Length == 0)
                                {
                                    strerror += "地点不能为空.";

                                }
                                row["line"] = r["行号"].ToString().Trim();
                                row["num"] = r["数量"].ToString().Trim();
                                row["site"] = r["地点"].ToString().Trim();
                                row["QAD"] = r["QAD"].ToString().Trim();

                            row["createby"] = Session["uID"].ToString();
                            row["CreateName"] = Session["uName"].ToString();
                            row["mstr_id"] = lbl_id.Text;

                            #endregion

                            if (strerror != "")
                            {
                                row["errMsg"] = strerror;
                            }
                            else
                            {
                                row["errMsg"] = string.Empty;
                            }
                            table.Rows.Add(row);
                            strerror = "";
                        }


                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adam.dsn0()))
                            {
                                bulkCopy.DestinationTableName = "dbo.RP_ld_temp";
                                //bulkCopy.ColumnMappings.Add("domain", "cpt_domain");
                                bulkCopy.ColumnMappings.Add("mstr_id", "mstr_id");
                                bulkCopy.ColumnMappings.Add("line", "line");
                                bulkCopy.ColumnMappings.Add("QAD", "QAD");
                                bulkCopy.ColumnMappings.Add("num", "num");
                                bulkCopy.ColumnMappings.Add("site", "site");
                                bulkCopy.ColumnMappings.Add("createby", "createby");
                                bulkCopy.ColumnMappings.Add("CreateName", "CreateName");
                             
                                bulkCopy.ColumnMappings.Add("errMsg", "errMsg");
                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    ltlAlert.Text = "alert('导入临时表时出错，请联系系统管理员！');";
                                    return false;
                                }
                                finally
                                {
                                    table.Dispose();
                                }
                            }
                        }
                    }
                }

                dt.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        return true;
    }
    public bool DeleteProductStruApplyDetail(string id)
    {
        try
        {
            string strSQL = " DELETE FROM dbo.RP_ld_temp where mstr_id='" + id + "'";

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);

            strSQL = " DELETE FROM dbo.RP_ld_det where mstr_id='" + id + "'";

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);


            strSQL = " DELETE FROM dbo.RP_ld_temp where createby='" + Session["uID"] + "'";

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.Text, strSQL);
            return true;
        }
        catch (Exception)
        {

            return false;
        }
     
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        
        if (lbl_id.Text == "")
        {
            string id = AddProductStruApplyMstr( txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            if (id != "0")
            {
                lbl_id.Text = id;
                BindMstrData(issupper(lbl_id.Text));
            }
            else
            {
                this.Alert("申请保存失败");
                return;
            }
        }
        else
        {
            UpdateProductStruApplyMstr(lbl_id.Text, ddldepartment.SelectedValue.ToString(), txtRmks.Text.Trim(), txtReason.Text.Trim(), lbl_Status.Text, Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData(issupper(lbl_id.Text));
        }
    }
    public string  UpdateProductStruApplyMstr(string id, string ddldepartment, string desc, string reason, string status, string userId, string userName)
    {
        string strName = "sp_RP_UpdateldMstr";
        SqlParameter[] parm = new SqlParameter[8];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@depart", ddldepartment);
        parm[2] = new SqlParameter("@desc", desc);
        parm[3] = new SqlParameter("@reason", reason);
        parm[4] = new SqlParameter("@status", status);
        parm[5] = new SqlParameter("@userId", userId);                                                                                                              
        parm[6] = new SqlParameter("@userName", userName);
        parm[7] = new SqlParameter("@costCode", ddlCost.SelectedValue);

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
    }

    public string AddProductStruApplyMstr( string desc, string userId, string userName)
    {
        try
        {
            string strName = "sp_RP_Insertldmstr";
            SqlParameter[] parm = new SqlParameter[7];
          
            parm[1] = new SqlParameter("@desc", desc);
            parm[2] = new SqlParameter("@userId", userId);
            parm[3] = new SqlParameter("@userName", userName);
            parm[4] = new SqlParameter("@deparmentid", ddldepartment.SelectedValue.ToString());
            parm[5] = new SqlParameter("@plantcode", Session["PlantCode"]);
            parm[6] = new SqlParameter("@costCode", ddlCost.SelectedValue);

            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        }
        catch
        {
            return "0";
        }
    }
    public SqlDataReader GetProductStruApplyMstr(string id)
    {
        try
        {
            string strName = "sp_RP_selectldmstr";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            return null;
        }
    }
    private void BindDetailData()
    {
        gv_product.DataSource = GetProductStruApplyDetail(lbl_id.Text);
        gv_product.DataBind();
    }
    public DataTable GetProductStruApplyDetail(string id)
    {
        try
        {
            string strName = "sp_RP_selectldDetail";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    private void BindMstrData(string supper)
    {
        IDataReader reader = GetProductStruApplyMstr(lbl_id.Text);
        if (reader.Read())
        {
            txt_nbr.Text = reader["RP_no"].ToString();
            txt_status.Text = reader["ldstatus"].ToString();
            //txt_prodCode.Text = reader["ProdCode"].ToString();
            txtReason.Text = reader["Reason"].ToString();
            txtRmks.Text = reader["mark"].ToString();
            lbl_Status.Text = reader["status"].ToString();
            hid_CreatedBy.Value = reader["createby"].ToString();
            ddlZH.SelectedValue = reader["RP_ZH"].ToString();
            ddldepartment.SelectedValue = reader["departmentid"].ToString();
            txtcreate.Text = reader["createName"].ToString();
            txtcreatedep.Text = reader["createbyDepartment"].ToString();
            lbPlandCode.Text = reader["plantcode"].ToString();

            BindCost(reader["plantcode"].ToString());

            ddlCost.SelectedValue = reader["RP_costCode"].ToString();

            BindDetailData();
          
        }
        reader.Close();
        if (this.Security["45301012"].isValid && lbl_Status.Text == "20")
        {
            this.trReview.Visible = true;
            this.trReason.Visible = true;
            this.txtReason.ReadOnly = false;
            this.trApply.Visible = false;
            this.trUpload.Visible = false;
            this.trUpload1.Visible = false;
            this.trUpload2.Visible = false;
            this.trUpload3.Visible = false;
            this.trUpload4.Visible = false;
            this.trUpload5.Visible = false;
            //this.txt_prodCode.ReadOnly = true;
            this.txtRmks.ReadOnly = true;
        }
        else
        {
            this.trReview.Visible = false;
            this.trApply.Visible = true;
            this.txtReason.ReadOnly = true;
           // if (this.Security["45301011"].isValid)
            if (true)
            {
                if (lbl_Status.Text == "0" && Session["uID"].ToString() == hid_CreatedBy.Value)
                {
                    btn_Save.Visible = true;
                    btn_Submit.Visible = true;
                    btn_Cancel.Visible = true;
                    trReason.Visible = false;
                    trUpload.Visible = true;
                    trUpload1.Visible = true;
                    //this.txt_prodCode.ReadOnly = false;
                    this.txtRmks.ReadOnly = false;
                }
                else if (lbl_Status.Text == "10" )
                {
                    btn_Save.Visible = true;
                    btn_Submit.Visible = true;
                    btn_Cancel.Visible = true;
                    trReason.Visible = true;
                    trUpload.Visible = false;
                    trUpload1.Visible = false;
                    trReview.Visible = true;
                    trApply.Visible = false;
                    //this.txt_prodCode.ReadOnly = false;
                    txtReason.ReadOnly = false;
                    this.txtRmks.ReadOnly = false;
                   
                }
                else  if (lbl_Status.Text == "20")
                {
                    btn_Save.Visible = true;
                    btn_Submit.Visible = true;
                    btn_Cancel.Visible = true;
                    trReason.Visible = true;
                    trUpload.Visible = false;
                    trUpload1.Visible = false;
                    trReview.Visible = true;
                    trApply.Visible = false;
                    txtReason.ReadOnly = false;
                    //this.txt_prodCode.ReadOnly = false;
                    this.txtRmks.ReadOnly = false;
                }
                else if (lbl_Status.Text == "-20" && Session["uID"].ToString() == hid_CreatedBy.Value)
                {
                    btn_Save.Visible = true;
                    btn_Submit.Visible = true;
                    btn_Cancel.Visible = true;
                    trReason.Visible = true;
                    trUpload.Visible = true;
                    trUpload1.Visible = true;
                    ddlCost.Enabled = false;
                    //this.txt_prodCode.ReadOnly = false;
                    this.txtRmks.ReadOnly = false;
                }
                else if (lbl_Status.Text == "30")
                {
                    this.Security.Register("120003012", "领料单审批-财务权限");
                    if (this.Security["120003012"].isValid)
                    {
                        btn_Save.Visible = true;
                        btn_Submit.Visible = true;
                        btn_Cancel.Visible = true;
                    }
                    else
                    {
                        btn_Save.Visible = false;
                        btn_Submit.Visible = false;
                        btn_Cancel.Visible = false;
                    }
                   
                    trReason.Visible = true;
                    trUpload.Visible = false;
                    trUpload1.Visible = false;
                    trReview.Visible = true;
                    trApply.Visible = false;
                    txtReason.ReadOnly = false;
                    ddlZH.Enabled = true;

                    //this.txt_prodCode.ReadOnly = false;
                    this.txtRmks.ReadOnly = false;
                }
                else if (lbl_Status.Text == "40")
                {
                    btn_Save.Visible = false;
                    btn_Submit.Visible = false;
                    btn_Cancel.Visible = false;
                    trReason.Visible = false;
                    trUpload.Visible = false;
                    trUpload1.Visible = false;
                    ddlCost.Enabled = false;
                    //this.txt_prodCode.ReadOnly = true;
                    this.txtRmks.ReadOnly = true;
                    btnget.Visible = true;
                }

                else if (lbl_Status.Text == "-10")
                {
                    btn_Save.Visible = false;
                    btn_Submit.Visible = false;
                    btn_Cancel.Visible = false;
                    trReason.Visible = false;
                    trUpload.Visible = false;
                    trUpload1.Visible = false;
                    //this.txt_prodCode.ReadOnly = true;    
                    this.txtRmks.ReadOnly = true;
                   // btnget.Visible = true;
                }
                trUpload2.Visible = trUpload1.Visible;
                trUpload3.Visible = trUpload1.Visible;
                trUpload4.Visible = trUpload1.Visible;
                trUpload5.Visible = trUpload1.Visible;
            }
            else
            {
                btn_Save.Visible = false;
                btn_Submit.Visible = false;
                btn_Cancel.Visible = false;
                trReason.Visible = true;
                trUpload.Visible = false;
                trUpload1.Visible = false;
               // this.txt_prodCode.ReadOnly = true;
                this.txtRmks.ReadOnly = true;
            }
        }
        if (supper == "0")
        {
            btnsaveline.Visible = false;
            //trApply.Visible = false;
            btn_Save.Visible = false;
            btn_Submit.Visible = false;
            btn_Cancel.Visible = false;
            trReason.Visible = false;
            trUpload.Visible = false;
            trUpload1.Visible = false;
            btn_Confirm.Visible = false;
            btn_Reject.Visible = false;
            //this.txt_prodCode.ReadOnly = true;
            this.txtRmks.ReadOnly = true;
            btnget.Visible = false;
        }
        getsite();
    }

    private void BindCost(string plandCode)
    {

        ddlCost.Items.Clear();

        string strName = "sp_RP_selectCostDDR";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@plandCode", plandCode);

      
        //SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strName, parm).Tables[0];
        ddlCost.DataSource = dt;
        ddlCost.DataBind();


    }
    public int SubmitProductStruApplyMstr(string id, string userId, string userName)
    {
        string strName = "sp_RP_submitld";
        SqlParameter[] parm = new SqlParameter[6];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@rmks", txtRmks.Text.Trim());
        parm[2] = new SqlParameter("@userId", userId);
        parm[3] = new SqlParameter("@userName", userName);
        parm[4] = new SqlParameter("@costCode", ddlCost.SelectedValue);
        parm[5] = new SqlParameter("@departmentID", ddldepartment.SelectedValue);
        //SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strName, parm).ToString();
        int result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        //if (result == 0)
        //{
        //    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique");
        //}
        return result;
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (gv_product.Rows.Count > 0)
        {
            int res = SubmitProductStruApplyMstr(lbl_id.Text, Session["uID"].ToString(), Session["uName"].ToString());
            if (res == 0)
            {
                this.Alert("提交成功！");
                BindMstrData(issupper(lbl_id.Text));
                this.Redirect("RP_ldList.aspx");
            }
           
            else
            {
                this.Alert("失败！");
            }
        }
        else
        {
            this.Alert("请导入Excel！");
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        //UpdateProductStruApplyMstr(lbl_id.Text, txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-10", Session["uID"].ToString(), Session["uName"].ToString());
        UpdateProductStruApplyMstr(lbl_id.Text, ddldepartment.SelectedValue.ToString(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-10", Session["uID"].ToString(), Session["uName"].ToString());
        BindMstrData(issupper(lbl_id.Text));
        this.Alert("已取消！");
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
            this.Redirect("RP_ldList.aspx");
    }
    protected void btn_Confirm_Click(object sender, EventArgs e)
    {
        if (lbl_Status.Text == "30")
        {
            if (ddlZH.SelectedValue == "--")
            {
                this.Alert("请选择账户！");
                return;
            }
        }
        int result = ProductStruImport(lbl_id.Text, Session["uID"].ToString(), Session["plantCode"].ToString(), Session["uName"].ToString(),ddlZH.SelectedValue.ToString().Trim());
        if (result == 0)
        {
            this.Alert("成功！");
            BindMstrData(issupper(lbl_id.Text));
            if (lbl_Status.Text == "30")
            {
                string plantCode = "0";
                switch (lbPlandCode.Text.ToLower())
                { 
                    case "szx":
                        plantCode = "1";
                        break;
                    case "zql":
                        plantCode = "2";
                        break;
                    case "yql":
                        plantCode = "5";
                        break;
                    case "hql":
                        plantCode = "8";
                        break;
                }
                string sql = "SELECT  email FROM  tcpc0..AccessRule acc " +
                  " LEFT JOIN tcpc0..users us ON acc.userID = us.userID " +
                  " WHERE moduleID  = '120003012' and us.plantcode=" + plantCode;
                   DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    setEmailpass(dt.Rows[row]["email"].ToString().Trim());
                }
            }
            else if (lbl_Status.Text == "20")
            {
                string sql = "SELECT email FROM  dbo.RP_departmentperson rp "  +
                    " LEFT  JOIN users us ON us.userID = rp.userID " +
                    " WHERE isboss = 1 AND rp.departmentid = " + ddldepartment.SelectedValue.ToString();
                DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    setEmailpass(dt.Rows[row]["email"].ToString().Trim());
                }
            }
            else if (lbl_Status.Text == "40")
            {
                string sql = "SELECT email FROM  users where userid= " + hid_CreatedBy.Value;
                string email = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql).ToString();
                try
                {
                    if (email != "")
                    {
                        string body = "你的领料单" + txt_nbr.Text + "已审批通过,请打印领料单去仓库领用<br>" +
                        "详情请登陆 "+baseDomain.getPortalWebsite()+"/Purchase/RP_ldList.aspx ";
                        this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), email, "", "领料单审批通过", body);
                    }
                }
                catch
                {
                    this.Alert("邮件发送失败！");
                }
            }
           // this.Redirect("RP_ldList.aspx");
        }
        else
        {
            this.Alert("失败！");
        }
    }
    public bool setEmailpass(string email)
    {
        try
        {
           
               
                if (email != string.Empty)
                {

                    string body = "详情请登陆 "+baseDomain.getPortalWebsite()+"/Purchase/RP_ldList.aspx ";

                    this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString();, email, "", "有新的领料申请单需要你确认", body);
                
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    public int ProductStruImport(string id, string userId, string plantCode, string username,string zh)
    {

        string strName = "sp_RP_ldConfirm";
        SqlParameter[] parm = new SqlParameter[6];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@userId", userId);
        parm[2] = new SqlParameter("@plantCode", plantCode);
        parm[3] = new SqlParameter("@username", username);
        parm[4] = new SqlParameter("@zh", zh);
        parm[5] = new SqlParameter("@costCode", ddlCost.SelectedValue);
        int result = (int)SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, strName, parm);
        //if (result == 0)
        //{
        //    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "Item_CheckUnique");
        //}
        return result;
    }
    protected void btn_Reject_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() == "")
        {
            this.Alert("请填写驳回意见！");
        }
        else
        {
            //UpdateProductStruApplyMstr(lbl_id.Text,  txtRmks.Text.Trim(), txtReason.Text.Trim(), "-20", Session["uID"].ToString(), Session["uName"].ToString());
            string flag = UpdateProductStruApplyMstr(lbl_id.Text, ddldepartment.SelectedValue.ToString(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-20", Session["uID"].ToString(), Session["uName"].ToString());
            //0 数据库异常
            //-1 通过
            //其他为邮件地址
            if (flag == "0")
            {
                this.Alert("出现异常，请联系管理员");
            }
            else 
            {
                string body = "你的领料单" + txt_nbr.Text + "已被驳回<br>" +
                        "详情请登陆 "+baseDomain.getPortalWebsite()+"/Purchase/RP_ldList.aspx ";

                if (this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), flag, "", "领料单审批被驳回", body))
                {
                    this.Alert("已驳回");
                }
                else
                {
                    this.Alert("已驳回,但邮件发送失败");
                }
            }


            
            
            BindMstrData(issupper(lbl_id.Text));
            
            this.Redirect("RP_ldList.aspx");


        }
    }
    protected void gv_product_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {

    }
    protected void likbtn_Click(object sender, EventArgs e)
    {

    }
   
    
    protected void btnget_Click(object sender, EventArgs e)
    {
        //this.Redirect("RP_ldExcel.aspx?id=" + lbl_id.Text.Trim() + "&rt=" + DateTime.Now.ToFileTime().ToString());
        //检查订单是否需要重新确认



        
            
        
        #region 新的PDF导出法
        string id = lbl_id.Text.Trim();
        //string rcvd = lblDelivery.Text.Trim();
        int page_size = 10;

       // System.Data.DataTable poMstr = GetProductStruApplyMstrptf(id);

        string RP_no = "";
        string mark = "";
        string createname = "";
        string RP_date = "";
        string caiwuName = "";
        string caiwuDate = "";
        string leaderdate = "";
        string leadername = "";
        string departname = "";
        string departcode = "";
        string yewuname = "";
        string yewudate = "";
        string zh = "";
        string costCode = "";
        string costCodeName = "";
        IDataReader reader = GetProductStruApplyMstr(id);
        if (reader.Read())
        {
            RP_no = reader["RP_no"].ToString();

            mark = reader["mark"].ToString();

            createname = reader["createname"].ToString();
            RP_date = reader["RP_date"].ToString();
            caiwuName = reader["caiwuName"].ToString();
            caiwuDate = reader["caiwuDate"].ToString();
            leaderdate = reader["leaderdate"].ToString();
            leadername = reader["leadername"].ToString();
            departname = reader["departname"].ToString();
            departcode = reader["departcode"].ToString();
            costCode = reader["RP_costCode"].ToString();
            costCodeName = reader["rp_costName"].ToString();

            yewuname = reader["yewuname"].ToString();
            yewudate = reader["yewudate"].ToString();
            zh = reader["RP_ZH"].ToString();
        }
        reader.Close();

        //string vend_name = poMstr.Rows[0]["vend"].ToString().Trim();
        //string company_name = poMstr.Rows[0]["company"].ToString().Trim();
        //string company_address = poMstr.Rows[0]["address"].ToString().Trim();
        //string order_date = poMstr.Rows[0]["orderdate"].ToString().Trim();
        //string po_consignment = Convert.ToBoolean(poMstr.Rows[0]["po_consignment"]) ? "寄售单:" : "采购单:";

        string path = Server.MapPath("/Temp/" + RP_no + ".pdf");
        string imgBar = Server.MapPath("/Temp/" + RP_no + ".Jpeg");

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            this.Alert("领料单正在被使用！");
            BindMstrData(issupper(lbl_id.Text));
            return;
        }

       // 画条形码
        try
        {
            MemoryStream ms = new MemoryStream();
            System.Drawing.Image myimg = BarCodeHelper.MakeBarcodeImage(RP_no, 1, true);

            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Position = 0;
            byte[] data = null;
            data = new byte[ms.Length];
            ms.Read(data, 0, Convert.ToInt32(ms.Length));
            ms.Flush();
            File.WriteAllBytes(imgBar, data);
        }
        catch
        {
            this.Alert("条形码创建失败！");
            BindMstrData(issupper(lbl_id.Text));
            return;
        }

        try
        {
            //创建PDF文档
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
            iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(path, System.IO.FileMode.Create));
            document.Open();

            System.Data.DataTable poDet = GetProductStruApplyDetail(id);

            string strDelivery = string.Empty;
            int pages = 1 + poDet.Rows.Count / page_size;
            int current = 1;

            System.Data.DataTable table = new System.Data.DataTable();
            table = poDet.Clone();

            for (int row = 0; row < poDet.Rows.Count; row++)
            {
                if (row < current * page_size)
                {
                    table.ImportRow(poDet.Rows[row]);
                }

                if (row == current * page_size - 1 || row == poDet.Rows.Count - 1)
                {

                    Templete(document, imgBar, page_size, pages, current, table, mark, createname, RP_date, caiwuName, caiwuDate, leaderdate, leadername, departname, departcode, yewuname, yewudate, zh, costCodeName);

                    current += 1;
                    table.Rows.Clear();

                    document.NewPage();
                }
            }

            document.Close();
            BindMstrData(issupper(lbl_id.Text));
            OpenWindow("/Temp/" + RP_no + ".pdf?rt=" + DateTime.Now.Millisecond.ToString() + "','PDF','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0");
        }
        catch
        {
            ;
        }
        #endregion
    }
    public void Templete(iTextSharp.text.Document document, string imgName, int page_size, int pages, int current, System.Data.DataTable rows, string mark, string createname, string RP_date, string caiwuName, string caiwuDate, string leaderdate, string leadername, string departname, string departcode, string yewuname, string yewudate, string zh, string costCodeName)
    {
        BaseFont bfSong = BaseFont.CreateFont("C:/WINDOWS/Fonts/STSong.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//宋体

        iTextSharp.text.Font fontHei = new iTextSharp.text.Font(bfSong, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//送货单
        iTextSharp.text.Font fontSong = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//其他

        //header
        float[] h_widths = { 0.36f, 0.36f, 0.36f };
        PdfPTable header = new PdfPTable(h_widths);
        header.WidthPercentage = 100f;

        PdfPCell cell = new PdfPCell(new Phrase("", fontHei));
        cell.BorderWidth = 0f;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("领料单", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgName);
        img.ScalePercent(70);
        cell = new PdfPCell(img, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("领料人:" + createname + "  "  + "部门:" + departname + "  " + RP_date, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("成本中心:" + costCodeName + "  " + "账户:" + zh, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("页: " + current.ToString() + " / " + pages.ToString(), fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("部门领导: " + leadername+"  "+leaderdate, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("业务审批: " + yewuname + "  " + yewudate, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("财务审批: " + caiwuName + "  " + caiwuDate, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        //items
        float[] i_widths = { 0.05f, 0.1f, 0.4f, 0.05f, 0.05f,  };
        PdfPTable item = new PdfPTable(i_widths);
        item.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("行号", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("QAD", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("描述", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("单位", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("数量", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        
     

        //cell = new PdfPCell(new Phrase("整箱", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("零箱", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        for (int i = 0; i < page_size; i++)
        {
            if (i < rows.Rows.Count)
            {
                cell = new PdfPCell(new Phrase(rows.Rows[i]["line"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["QAD"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["description"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["UM"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);
                //cell = new PdfPCell(new Phrase("EA", fontSong));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                //item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["num"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                //cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_qty_short"].ToString(), fontSong));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                //item.AddCell(cell);

                //cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_qty_dev"].ToString(), fontSong));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
               // item.AddCell(cell);

                //cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_box_ent"].ToString(), fontSong));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                //item.AddCell(cell);

                //cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_box_sca"].ToString(), fontSong));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                //item.AddCell(cell);
            }
            else
            {
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                //item.AddCell(new Phrase(" ", fontSong));
                //item.AddCell(new Phrase(" ", fontSong));
                //item.AddCell(new Phrase(" ", fontSong));
                //item.AddCell(new Phrase(" ", fontSong));
            }
        }
        //footer
        float[] f_widths = { 0.33f, 0.33f, 0.33f };
        PdfPTable footer = new PdfPTable(f_widths);
        footer.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("领料人:", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("仓库保管员:", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("记账员:", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);


        //写入一个段落, Paragraph
        document.Add(header);
        document.Add(item);
        document.Add(footer);

    }

    public void OpenWindow(string url)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OpenWindow", "window.open('" + url + "', '_blank','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=500,top=0,left=0');", true);
    }
    protected void btnsaveline_Click(object sender, EventArgs e)
    {
        if (txtqad.Text.Trim().Length == 0)
        {
            this.Alert("QAD不能为空");
            return;
        }
        else
        {
            //string strSql2 = " SELECT * FROM QAD_Data..ld_det WHERE ld_part ='" + txtqad.Text.Trim() + "'" + "and ld_site = " + txtsite.Text.Trim();
            //DataSet ds2;
            //ds2 = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql2);
            //if (!(ds2 != null && ds2.Tables[0].Rows.Count > 0))
            //{
            //    this.Alert("库存QAD不存在.");
            //    return;
            //}
        }
        if (txtline.Text.Trim() == string.Empty)
        {
            txtline.Text = "1";
        }
        if (txtnum.Text.Trim() == string.Empty)
        {
            this.Alert("数量不能为空.");
            return;
        }
        else
        {
            int _line;
            try
            {
                _line = Convert.ToInt32(txtnum.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('数量必须是数字!');";
              
                return;
            }
            if (_line <= 0)
            {
                ltlAlert.Text = "alert('数量必须大于零!');";
            }
        }
            SqlParameter[] sqlParam = new SqlParameter[9];
            sqlParam[0] = new SqlParameter("@uID", Session["uID"].ToString());
            sqlParam[1] = new SqlParameter("@uName", Session["uName"].ToString());
            sqlParam[3] = new SqlParameter("@mstr_id", lbl_id.Text);
            sqlParam[4] = new SqlParameter("@line", txtline.Text.Trim());
            sqlParam[5] = new SqlParameter("@qad", txtqad.Text.Trim());
            sqlParam[6] = new SqlParameter("@num", txtnum.Text.Trim());
            sqlParam[7] = new SqlParameter("@site", txtsite.Text.Trim());
            sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
            sqlParam[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_insertlddetline", sqlParam);

            BindMstrData(issupper(lbl_id.Text));
       
    }
    protected void gv_product_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id", gv_product.DataKeys[e.RowIndex].Values["id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_deletelddet", param);
        }
        catch
        {
            ltlAlert.Text = "alert('删除失败!');";
            BindMstrData(issupper(lbl_id.Text));
        }

        BindMstrData(issupper(lbl_id.Text));
    }
    protected void gv_product_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (lbl_Status.Text != "0" && lbl_Status.Text != "-20")
            {
              
                    e.Row.Cells[5].Text = string.Empty;
                   

                
            }
        }
    }
    protected void btnld_Click(object sender, EventArgs e)
    {
        OpenWindow("RP_ld_search.aspx?qad=" + txtqad.Text + "&site=" + txtsite.Text);
    }
    protected void gv_product_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString().ToLower().CompareTo("det") == 0)
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            string prd = param[0].ToString().Trim();
            string po = param[1].ToString().Trim();


            OpenWindow("RP_ld_search.aspx?qad=" + prd + "&site=" + po);
        }
    }

    public string issupper(string id)
    {
        SqlParameter[] sqlParam = new SqlParameter[4];
        sqlParam[0] = new SqlParameter("@uID", Session["uID"].ToString());

        sqlParam[3] = new SqlParameter("@id", id);
        sqlParam[2] = new SqlParameter("@retValue", DbType.Boolean);
        sqlParam[2].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_checkuspper", sqlParam);

        return sqlParam[2].Value.ToString();
    }
}