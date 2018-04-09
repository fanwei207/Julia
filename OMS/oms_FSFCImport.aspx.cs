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
using QADSID;
using System.Collections.Generic;

public partial class oms_FSFCImport : BasePage
{
    adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCustCode.Text = Request.QueryString["custCode"].ToString();
            lblCust.Text = Request.QueryString["custName"].ToString();
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            BindGvForecast();
        }
    }

    protected void BindGvForecast()
    {
        gvForecast.DataSource = OMSHelper.GetForecast(lblCustCode.Text,"",Convert.ToDateTime("2000/1/1"));
        gvForecast.DataBind();
    }

    protected bool ImportExcelFile()
    {
        string strFileName = "";
        string strFilePath = "";
        int index = 0;
        strFilePath = filename2.PostedFile.FileName;
        index = strFilePath.LastIndexOf("\\");
        strFileName = strFilePath.Substring(index + 1);
        if (strFileName.Trim().Length <= 0)
        {
            this.Alert("file is required!");
            return false;
        }
        string extension = System.IO.Path.GetExtension(strFilePath);
        if (filename2.PostedFile != null)
        {
            if (filename2.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('the file uploading must less than 8 MB!');";
                return false;
            }
        }
        if (extension != ".xls")
        {
            this.Alert("the file name must be *.xls！");
            return false;
        }
        //创建临时文件夹用来存储上传的excel，便于读取其内容
        string catFolder = Server.MapPath("/OMS/temp/");
        if (!Directory.Exists(catFolder))
        {
            try
            {
                Directory.CreateDirectory(catFolder);
            }
            catch
            {
                this.Alert("fail to create a folder！");
                return false;
            }
        }
        strFilePath = catFolder + strFileName;
        if (File.Exists(strFilePath))
        {
            File.Delete(strFilePath);
            filename2.PostedFile.SaveAs(strFilePath);
        }
        else
        {
            filename2.PostedFile.SaveAs(strFilePath);
        }
        DataTable table = new DataTable();
        DataColumn column;
        #region   给table添加相应列

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fc_customer";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fc_list";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fc_pr";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fc_part";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "fc_date";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "fc_qty";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "fc_unit";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "fc_CreateBy";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.DateTime");
        column.ColumnName = "fc_CreateDate";
        table.Columns.Add(column);

        #endregion
        #region
        DataTable err = new DataTable();

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "list";
        err.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "err";
        err.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "err1";
        err.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "err2";
        err.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "err3";
        err.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "err4";
        err.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "err5";
        err.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "err6";
        err.Columns.Add(column);
        #endregion

        DataTable dt = ExcelToDS(strFilePath).Tables["table1"];
        DataRow row;
        DataRow erow;
        if (dt != null && dt.Rows.Count > 0)
        {
            //判断模板是否正确

            bool flag = false;
            if (dt.Rows[0][0].ToString().Trim() != "NO." || dt.Rows[0].ItemArray[1].ToString().Trim() != "Product"
                  || dt.Rows[0].ItemArray[2].ToString().Trim() != "Ship Date" || dt.Rows[0].ItemArray[3].ToString().Trim() != "Forcate Qty" ||
                   dt.Rows[0].ItemArray[4].ToString().Trim() != "Unit")
            {
                this.Alert("the excel uploading must be the same as template！");
                this.Alert(dt.Rows[0].ItemArray[0].ToString());
                return false;
            }
            int s = 0;
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                int j = i + 1;
                row = table.NewRow();
                erow = err.NewRow();

                erow["err"] = "第" + j + "行";
                #region 判断导入数据格式是否正确,并且写入table
                if (dt.Rows[i].ItemArray[0].ToString().Trim() != string.Empty)
                {
                    row["fc_list"] = dt.Rows[i].ItemArray[0].ToString().Trim();
                }
                else
                {

                    erow["err1"] = "No. is required";
                    flag = true;
                }
                if (dt.Rows[i].ItemArray[1].ToString().Trim() != string.Empty)
                {
                    row["fc_part"] = "";
                    row["fc_pr"] = dt.Rows[i].ItemArray[1].ToString().Trim();
                }
                else
                {

                    erow["err3"] = "Product is required";
                    flag = true;

                }
                if (dt.Rows[i].ItemArray[2].ToString().Trim() != string.Empty)
                {
                    try
                    {
                        DateTime time = Convert.ToDateTime(dt.Rows[i].ItemArray[2].ToString().Trim());
                        row["fc_date"] = time;
                    }
                    catch (FormatException fe)
                    {

                        erow["err4"] = "Forcaste Date is not a date";
                        flag = true;
                    }
                }
                else
                {
                    erow["err4"] = "Forcaste Date is required";
                    flag = true;
                }
                if (dt.Rows[i].ItemArray[3].ToString().Trim() != string.Empty)
                {
                    try
                    {
                        int qty = Convert.ToInt32(dt.Rows[i].ItemArray[3].ToString().Trim());
                        row["fc_qty"] = qty;
                    }
                    catch (FormatException fe)
                    {
                        erow["err5"] = "Forcaste Qty is not a number";
                        flag = true;
                    }
                }
                else
                {
                    erow["err5"] = "Forcaste Qty is required";
                    flag = true;
                }
                if (dt.Rows[i].ItemArray[4].ToString().Trim() != string.Empty)
                {
                    row["fc_unit"] = dt.Rows[i].ItemArray[4].ToString();
                }
                else
                {
                    erow["err6"] = "Unit is required";
                    flag = true;
                }
                #endregion
                row["fc_customer"] = lblCustCode.Text;
                row["fc_createBy"] = Session["uId"];
                DateTime time1 = DateTime.Now;
                row["fc_createDate"] = time1;
                table.Rows.Add(row);
                if (flag)
                {
                    erow["list"] = ++s;
                    err.Rows.Add(erow);
                }
                flag = false;
            }
        }
        string title = "100^<b>Err Msg</b>~^100^<b>Position</b>~^100^<b>1</b>~^100^<b>2</b>~^100^<b>3</b>~^100^<b>4</b>~^100^<b>5</b>~^100^<b>6</b>~^";

        if (err != null && err.Rows.Count > 0)
        {
            ExportExcel(title, err, false);//将错误信息导出到excel
            return false;
        }
        if (table != null && table.Rows.Count > 0)
        {
            //批量写入数据库中

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(adm.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
            {
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    bulkCopy.DestinationTableName = "tcpc0..OMS_FSForecast";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("fc_customer", "fc_customer");
                    bulkCopy.ColumnMappings.Add("fc_pr", "fc_product_nbr");
                    bulkCopy.ColumnMappings.Add("fc_part", "fc_part");
                    bulkCopy.ColumnMappings.Add("fc_date", "fc_date");
                    bulkCopy.ColumnMappings.Add("fc_qty", "fc_qty");
                    bulkCopy.ColumnMappings.Add("fc_unit", "fc_pr_unit");
                    bulkCopy.ColumnMappings.Add("fc_createBy", "fc_createBy");
                    bulkCopy.ColumnMappings.Add("fc_createDate", "fc_createDate");
                }
                try
                {
                    bulkCopy.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    this.Alert("fail to operate DB！");
                    return false;
                }
                finally
                {
                    table.Dispose();
                    bulkCopy.Close();
                }
            }
        }
        //最后删除临时文件和文件夹
        File.Delete(strFilePath);
        Directory.Delete(catFolder);
        return true;
    }

    /// <summary>
    /// 将excel数据导入datatable中
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns>
    public DataSet ExcelToDS(string Path)
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties='Excel 8.0; HDR=NO;IMEX=1';";
        OleDbConnection conn = new OleDbConnection(strConn);
        conn.Open();
        string strExcel = "";
        DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        string SheetName = dt.Rows[0]["TABLE_NAME"].ToString(); //这里就是取得表的名字了
        OleDbDataAdapter myCommand = null;
        DataSet ds = null;
        strExcel = "select * from [" + SheetName + "]";
        myCommand = new OleDbDataAdapter(strExcel, strConn);
        ds = new DataSet();
        myCommand.Fill(ds, "table1");
        conn.Close();
        dt.Dispose();
        return ds;
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        
        if (!ImportExcelFile())
        {
            //导入未成功，最后删除临时文件和文件夹
            string strFileName = "";
            string strFilePath = "";
            int index = 0;
            strFilePath = filename2.PostedFile.FileName;
            index = strFilePath.LastIndexOf("\\");
            strFileName = strFilePath.Substring(index + 1);
            string catFolder = Server.MapPath("/OMS/temp/");
            strFilePath = catFolder + strFileName;
            if (File.Exists(strFilePath))
            {
                File.Delete(strFilePath);
                Directory.Delete(catFolder);
            }
        }
        BindGvForecast();

    }

    protected void gvForecast_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Delete1")
        {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
            int fc_id = Convert.ToInt32(gvForecast.DataKeys[index].Values["id"].ToString());
            if (!OMSHelper.DeleteForecast(fc_id))
            {
                this.Alert("fail to delete!");
                return;
            }
        }
    }

    protected void gvForecast_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvForecast.PageIndex = e.NewPageIndex;
        BindGvForecast();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("oms_cust.aspx");
    }
}