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
using System.IO;
using QADSID;
using System.Data.SqlClient;

public partial class SID_SID_ImportLadingNum : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }
        if (ImportExcelFile())
        {
            //Int32 Ierr = 0;

            //Ierr = sid.ImportShipInvData(Convert.ToInt32(Session["uID"]));
            //if (Ierr < 0)
            //{
            //    Response.Redirect(chk.urlRand("/SID/SID_Invimport.aspx?err=y"));
            //}
            //else
            //{
            ltlAlert.Text = "alert('导入成功!');";
            //}

        }
        //else
        //{
        //    ltlAlert.Text = "alert('表中已存在相同的出运单号或发票号!');";
        //}
    }



    public Boolean ImportExcelFile()
    {
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
                    dt = this.GetExcelContents(strFileName);
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
                    if (dt.Columns[0].ColumnName != "出运单号" ||
                        dt.Columns[1].ColumnName != "发票号" ||
                        dt.Columns[2].ColumnName != "提单日期")
                    {
                        dt.Reset();
                        ltlAlert.Text += "alert('导入文件的模版不正确!');";
                        return false;
                    }

                    String _shipname = "";
                    String _receipt = "";
                    String _laddate = "";

                    i = 0;

                    //转换成SID_InvTemp格式
                    DataTable table = new DataTable("temp");
                    DataColumn column;
                    DataRow row;

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "ShipNum";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "Receipt";
                    table.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.DateTime");
                    column.ColumnName = "LadDate";
                    table.Columns.Add(column);



                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        _shipname = "";
                        _receipt = "";
                        _laddate = "";



                        _shipname = dt.Rows[i].ItemArray[0].ToString().Trim();
                        _receipt = dt.Rows[i].ItemArray[1].ToString().Trim();
                        _laddate = dt.Rows[i].ItemArray[2].ToString().Trim();

                        if (JudgeLadingList(_shipname,_receipt))
                        {
                            ltlAlert.Text = "alert('表中已存在相同的出运单号或发票号!')";
                            return false;
                        }

                        row = table.NewRow();

                        row["ShipNum"] = _shipname;
                        row["Receipt"] = _receipt;
                        row["LadDate"] = _laddate;

                        table.Rows.Add(row);

                        if (_shipname.Trim() == "")
                        {
                            break; ;
                        }
                    }

                    //table有数据的情况下
                    if (table != null && table.Rows.Count > 0)
                    {

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                        {
                            bulkCopy.DestinationTableName = "SID_LadingNum";

                            bulkCopy.ColumnMappings.Clear();

                            bulkCopy.ColumnMappings.Add("ShipNum", "SID_ShipNum");
                            bulkCopy.ColumnMappings.Add("Receipt", "SID_Receipt");
                            bulkCopy.ColumnMappings.Add("LadDate", "SID_LadDate");

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
                } //dt.Rows.Count > 0                           

                dt.Reset();

                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
            }
        }
        return true;
    }

    private bool JudgeLadingList(string shipnum, string receipt)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@shipnum", shipnum);
        param[1] = new SqlParameter("@receipt", receipt);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_sid_JudgeLadingList", param));
    }
}