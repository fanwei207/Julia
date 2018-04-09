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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using QADSID;
using System.Data.SqlClient;


public partial class EDI_EDIPOMap : BasePage
{
    adamClass chk = new adamClass();
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn_edi"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["err"] == "y")
            {
                string title = "<b>TCP订单号</b>~^100^<b>序号1</b>~^30^<b>采购单编号</b>~^100^<b>序号2</b>~^30^<b>错误信息</b>~^";
                string sql = " Select EMP_ediNbr,EMP_ediLine,EMP_poNbr,EMP_poLine,EMP_errorMessage From EDI_DB.dbo.EDIPOMapTemp Where createBy=" + Session["uID"].ToString() ;
                DataTable dt = SqlHelper.ExecuteDataset(strConn,CommandType.Text,sql).Tables[0];
                ExportExcel(title, dt, false);                
            }

        }
    }
    protected void btn_import_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }
        //string sql = "Delete EDIPOMapError Where createBy=" + Session["uID"].ToString();
        //SqlHelper.ExecuteNonQuery(strConn, CommandType.Text, sql);

        string sql = "sp_epm_deleteEpmTemp";
        SqlParameter[] param1 = new SqlParameter[2];
        param1[0] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
        param1[1] = new SqlParameter("@retvalue", SqlDbType.Int);
        param1[1].Direction = ParameterDirection.Output;
        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param1);
        if (param1[1].Value.ToString() == "-1")
        {
            ltlAlert.Text = "alert('导入失败，删除临时表数据错误请联系管理员!');";
            return;
        }

        if (ImportExcelFile())
        {
            sql = "sp_epm_InsertOrUpdateEpm";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
            param[1] = new SqlParameter("@retvalue", SqlDbType.Int);
            param[1].Direction = ParameterDirection.Output;
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);

            if(param[1].Value.ToString() == "-1")
            {
                Response.Redirect(chk.urlRand("EDIPOMap.aspx?err=y"));
            }
            else if (param[1].Value.ToString() == "1")
            {
                ltlAlert.Text = "alert('导入成功!');";
            }
            else
            {
                ltlAlert.Text = "alert('导入失败，数据库错误请联系管理员!');";
            }

        }
    }

    public Boolean ImportExcelFile()
    {
        //DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

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
                    //ds = chk.getExcelContents(strFileName);
                    
                    dt = GetExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + e.ToString() + "'.');";
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
                    try
                    {
                        if (
                            dt.Columns[0].ColumnName != "日期" ||
                            dt.Columns[1].ColumnName != "客户代码" ||
                            dt.Columns[2].ColumnName != "客户名称" ||
                            dt.Columns[3].ColumnName != "港口" ||
                            dt.Columns[4].ColumnName != "TCP订单号" ||
                            dt.Columns[5].ColumnName != "客户订单号" ||
                            dt.Columns[6].ColumnName != "SW1" ||
                            dt.Columns[7].ColumnName != "SW2" ||
                            dt.Columns[8].ColumnName != "截止日期" ||
                            dt.Columns[9].ColumnName != "序号1" ||
                            dt.Columns[10].ColumnName != "销售单号" ||
                            dt.Columns[11].ColumnName != "QAD号编码" ||
                            dt.Columns[12].ColumnName != "产品型号" ||
                            dt.Columns[13].ColumnName != "订购数量(套)" ||
                            dt.Columns[14].ColumnName != "数量(只)" ||
                            dt.Columns[15].ColumnName != "裸灯QAD号" ||
                            dt.Columns[16].ColumnName != "采购单号" ||
                            dt.Columns[17].ColumnName != "序号2" ||
                            dt.Columns[18].ColumnName != "备注" ||
                            dt.Columns[19].ColumnName != "留样")
                        {
                            dt.Reset();
                            ltlAlert.Text += "alert('导入文件的模版不正确!');";
                            return false;
                        }
                    }
                    catch(Exception e)
                    {
                        ltlAlert.Text += "alert('导入文件的模版不正确"+e.Message+"!');";
                        return false;
                    }

                    String _ediNbr = "";
                    int _ediLine = 0;
                    String _poNbr = "";
                    int _poLine =0;
                    Guid _id ;

                    i = 0;

                    
                    DataTable table = new DataTable("temp");
                    DataColumn column;
                    DataRow row;

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Guid");
                    column.ColumnName = "EMP_ID";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "EMP_ediNbr";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "EMP_ediLine";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "EMP_poNbr";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "EMP_poLine";
                    table.Columns.Add(column);
                    
                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "EMP_plantCode";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "createName";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "createBy";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.DateTime");
                    column.ColumnName = "createDate";
                    table.Columns.Add(column);
                    int flag = 1;
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        _ediNbr = dt.Rows[i].ItemArray[4].ToString().Trim();
                        try
                        {
                            _ediLine = Convert.ToInt32(dt.Rows[i].ItemArray[9].ToString());
                        }
                        catch
                        {
                            _ediLine = 0;
                        }
                        _poNbr = dt.Rows[i].ItemArray[16].ToString().Trim();
                        try
                        {
                            _poLine = Convert.ToInt32(dt.Rows[i].ItemArray[17].ToString());
                        }
                        catch
                        {
                            _poLine = 0;
                        }                        
                        _id = Guid.NewGuid();
                        
                       row = table.NewRow();

                       row["EMP_ID"] = _id;
                       row["EMP_ediNbr"] = _ediNbr;
                       row["EMP_ediLine"] = _ediLine;
                       row["EMP_poNbr"] = _poNbr;
                       row["EMP_poLine"] = _poLine;
                       row["EMP_plantCode"] = Session["plantCode"].ToString();
                       row["createName"] = Session["uName"].ToString();
                       row["createBy"] = Convert.ToInt32(Session["uID"].ToString());
                       row["createDate"] = DateTime.Now;

                       table.Rows.Add(row);
                              
                    }

                    //table有数据的情况下
                    if (table != null && table.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(strConn, SqlBulkCopyOptions.UseInternalTransaction))
                        {
                            bulkCopy.DestinationTableName = "EDIPOMapTemp";

                            bulkCopy.ColumnMappings.Clear();

                            bulkCopy.ColumnMappings.Add("EMP_ID", "EMP_ID");
                            bulkCopy.ColumnMappings.Add("EMP_ediNbr", "EMP_ediNbr");
                            bulkCopy.ColumnMappings.Add("EMP_ediLine", "EMP_ediLine");
                            bulkCopy.ColumnMappings.Add("EMP_poNbr", "EMP_poNbr");
                            bulkCopy.ColumnMappings.Add("EMP_poLine", "EMP_poLine");
                            bulkCopy.ColumnMappings.Add("EMP_plantCode", "EMP_plantCode");
                            bulkCopy.ColumnMappings.Add("createName", "createName");
                            bulkCopy.ColumnMappings.Add("createBy", "createBy");
                            bulkCopy.ColumnMappings.Add("createDate", "createDate");

                            try
                            {
                                bulkCopy.WriteToServer(table);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('导入时出错，请联系系统管理员！');";
                                return false;
                            }
                            finally
                            {
                                table.Dispose();
                                bulkCopy.Close();
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

}