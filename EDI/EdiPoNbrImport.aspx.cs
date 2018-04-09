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
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using CommClass;

public partial class EDI_EdiPoNbrImport : BasePage
{
    adamClass chk = new adamClass();
    BasePage Bp = new BasePage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRadio();
            lblTips.Visible = false;
            btnSure.Visible = false;
        }
    }
    public void BindGv(string type)
    {
            gv.DataSource = GetPoNbr(type);
            gv.DataBind();
    }

    public void BindRadio()
    {
        radioType.Items.Add(new ListItem("Replace","replace"));
        radioType.Items.Add(new ListItem("Cancel", "cancel"));
    }
    #region 导入Excel数据
    public Boolean ImportExcelFile()
    {
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

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
                this.Alert("Fail to upload file.");
                return false;
            }

        }
        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            this.Alert("Please select a file.");
            return false;
        }

        strUserFileName = strFileName;

        strFileName = strCatFolder + "\\" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + strFileName;
        #endregion
        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                this.Alert("The maximum upload file is 8 MB.");
                return false;
            }
            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                this.Alert("alert('Failed to upload file");
                return false;
            }
            if (File.Exists(strFileName))
            {
                try
                {
                    dt = Bp.GetExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    this.Alert( "Import file must be in Excel format.");
                    return false;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
                if (radioType.SelectedValue == "replace")
                {
                    if (dt.Rows.Count > 0)
                    {
                        #region Excel的列名必须保持一致
                        if (dt.Columns.Count < 2)
                        {
                            dt.Reset();
                            this.Alert("Please Choose right Template！");
                            return false;
                        }
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            if (col == 0 && dt.Columns[col].ColumnName.Trim().ToLower() != "ponbr")
                            {
                                dt.Reset();
                                this.Alert("The " + col.ToString() + "th column name should be PoNbr！");
                                return false;
                            }

                            if (col == 1 && dt.Columns[col].ColumnName.Trim().ToLower() != "ponbr1")
                            {
                                dt.Reset();
                                this.Alert("The " + col.ToString() + "th column name should bePoNbr1!");
                                return false;
                            }

                        }
                        #endregion

                        //转换成模板格式
                        DataTable table = new DataTable("temp");
                        DataColumn column;
                        DataRow row;

                        #region 定义表列
                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "PoNbr";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "PoNbr1";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.Int32");
                        column.ColumnName = "crBy";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "crByName";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "crDate";
                        table.Columns.Add(column);

                        #endregion

                        int _uID = Convert.ToInt32(Session["uID"]);
                        string _uName = Session["uName"].ToString();
                        foreach (DataRow r in dt.Rows)
                        {
                            row = table.NewRow();
                            #region
                            row["PoNbr"] = r[0].ToString().Trim();
                            row["PoNbr1"] = r[1].ToString().Trim();
                            row["crDate"] = DateTime.Now.ToLocalTime();
                            row["crBy"] = _uID;
                            row["crByName"] = _uName;
                            table.Rows.Add(row);
                            #endregion
                        }

                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            ClearTbPoNbr(Convert.ToInt32(Session["uId"].ToString()),radioType.SelectedValue);
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_edi")))
                            {
                                bulkCopy.DestinationTableName = "dbo.edi_PoNbrReplaceTemp";
                                bulkCopy.ColumnMappings.Add("PoNbr", "po_Nbr");
                                bulkCopy.ColumnMappings.Add("PoNbr1", "po_Nbr1");
                                bulkCopy.ColumnMappings.Add("crBy", "po_createBy");
                                bulkCopy.ColumnMappings.Add("crDate", "po_createDate");
                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    this.Alert("Operation fails!Please try again!");

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
                //取消订单
                if (radioType.SelectedValue == "cancel")
                {
                    if (dt.Rows.Count > 0)
                    {
                        #region Excel的列名必须保持一致
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            if (col == 0 && dt.Columns[col].ColumnName.Trim().ToLower() != "ponbr")
                            {
                                dt.Reset();
                                this.Alert("Header Error");
                                return false;
                            }
                        }
                        #endregion

                        //转换成模板格式
                        DataTable table = new DataTable("temp");
                        DataColumn column;
                        DataRow row;

                        #region 定义表列
                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "PoNbr";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.Int32");
                        column.ColumnName = "crBy";
                        table.Columns.Add(column);

                        column = new DataColumn();
                        column.DataType = System.Type.GetType("System.String");
                        column.ColumnName = "crDate";
                        table.Columns.Add(column);

                        #endregion

                        int _uID = Convert.ToInt32(Session["uID"]);
                        string _uName = Session["uName"].ToString();
                        foreach (DataRow r in dt.Rows)
                        {
                            row = table.NewRow();
                            #region
                            row["PoNbr"] = r[0].ToString().Trim();
                            row["crDate"] = DateTime.Now.ToLocalTime();
                            row["crBy"] = _uID;
                            table.Rows.Add(row);
                            #endregion
                        }

                        //table有数据的情况下
                        if (table != null && table.Rows.Count > 0)
                        {
                            ClearTbPoNbr(Convert.ToInt32(Session["uId"].ToString()),radioType.SelectedValue);
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(admClass.getConnectString("SqlConn.Conn_edi")))
                            {
                                bulkCopy.DestinationTableName = "dbo.edi_PoNbrDeleteTemp";
                                bulkCopy.ColumnMappings.Add("PoNbr", "po_Nbr");
                                bulkCopy.ColumnMappings.Add("crBy", "po_createBy");
                                bulkCopy.ColumnMappings.Add("crDate", "po_createDate");
                                try
                                {
                                    bulkCopy.WriteToServer(table);
                                }
                                catch (Exception ex)
                                {
                                    this.Alert("Operation fails!Please try again!");

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
    #endregion
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (radioType.SelectedIndex < 0)
        {
            this.Alert("Please Choose import Type !");
            return;
        }
        DataTable err = new DataTable();
        if (ImportExcelFile())
        {
            if (radioType.SelectedValue == "replace")
            {
                err = GetErrMsg(Convert.ToInt32(Session["uId"].ToString()),"replace");
                if (err.Rows.Count > 0 && err != null)
                {
                    string title = "100^<b>Line</b>~^500^<b>Err Msg</b>~^";
                    ExportExcel(title, err, false);//将错误信息导出到excel
                }
                else
                {
                    gv.Visible = true;
                    BindGv(radioType.SelectedValue);
                    lblTips.Visible = true;
                    btnSure.Visible = true;
                }
            }
            if (radioType.SelectedValue == "cancel")
            {
                err = GetErrMsg(Convert.ToInt32(Session["uId"].ToString()), "cancel");
                if (err.Rows.Count > 0 && err != null)
                {
                    string title = "100^<b>Line</b>~^500^<b>Err Msg</b>~^";
                    ExportExcel(title, err, false);//将错误信息导出到excel
                }
                else
                {
                    gv.Visible = true;
                    BindGv(radioType.SelectedValue);
                    lblTips.Visible = true;
                    btnSure.Visible = true;
                    gv.Columns[1].Visible = false;
                }
            }
        }
        else
            this.Alert("Import Failed ,please try again!");
    }
    protected void btnSure_Click(object sender, EventArgs e)
    {
        if (radioType.SelectedValue == "replace")
        {
            ChangePoNbr();
            lblTips.Visible = false;
            btnSure.Visible = false;
            gv.Visible = false;
            ClearTbPoNbr(Convert.ToInt32(Session["uId"].ToString()), "replace");
        }
        else
        {
            CanclePoNbr();
            lblTips.Visible = false;
            btnSure.Visible = false;
            gv.Visible = false;
            ClearTbPoNbr(Convert.ToInt32(Session["uId"].ToString()), "cancel");
        }
    }
    public void ChangePoNbr()
    {
        string sp = "sp_edi_ChangePoNbr";
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, sp);
    }

    public void CanclePoNbr()
    {
        string sp = "sp_edi_updatePoHrdCancelAllPoNbr";
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, sp);
    }

    public DataTable GetPoNbr(string type)
    {
        SqlParameter parm = new SqlParameter("@type", type);
        string sp = "sp_edi_selectRePoNbr";
       return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, sp, parm).Tables[0];
    }
    public void ClearTbPoNbr(int id, string type) //当前用户每次上传前，清空以前自己上传的记录
    {
        if (type == "replace")
        {
            string sp = "sp_edi_deleteUploadedPoNbr";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@uId", id);
            parm[1] = new SqlParameter("@reValue", SqlDbType.Bit);
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, sp, parm);
        }
        else
        {
            string sp = "sp_edi_deleteCancelUploadPoNbr";
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@uId", id);
            parm[1] = new SqlParameter("@reValue", SqlDbType.Bit);
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, sp, parm);
        }
    }
    public DataTable GetErrMsg(int id, string type)
    {
        if (type == "replace")
        {
            SqlParameter parm = new SqlParameter("@uId", id);
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkAndInserPoNbrReplace");
            string sp = "sp_edi_selectReplaceErrMsg";
            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, sp, parm).Tables[0];
        }
        else if (type == "cancel")
        {
            SqlParameter parm = new SqlParameter("@uId", id);
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkAndInserPoNbrDelete");
            string sp = "sp_edi_selectPoNbrDeleteErrMsg";
            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, sp, parm).Tables[0];
        }
        else
            return null;
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGv(radioType.SelectedValue);
    }
}