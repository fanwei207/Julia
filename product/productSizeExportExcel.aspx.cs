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
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.DDF;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;

public partial class productSizeExportExcel : BasePage
{
    adamClass chk = new adamClass();
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            FileTypeDropDownList1.SelectedIndex = 0;
            ListItem item1;
            item1 = new ListItem("Excel (.xls) file");
            item1.Value = "0";
            FileTypeDropDownList1.Items.Add(item1);
        
        }
    }



    /// <summary>
    /// delete ImportError table
    /// </summary>
    public void DelImportError(Int32 uID)
    {
        string strSQL = " Delete From productSizeExport_temp Where prod_createdby='" + uID + "'";
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL);

    }
    protected DataTable getproductSizeList()
    {
        string strSQL = "sp_product_selectProductSizeExport";
        SqlParameter[] parms = new SqlParameter[1];
        parms[0] = new SqlParameter("@uID", Session["uID"]);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strSQL, parms).Tables[0];
    }

    protected void BtnShip_ServerClick(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            return;
        }

        DelImportError(Convert.ToInt32(Session["uID"]));

        int ErrorRecord = 0;

        if (!ImportExcelFile())
        {
            ErrorRecord += 1;
        }
        if (ErrorRecord == 0)
        {
            DataTable table = getproductSizeList();
            //  ExportData(table);
            string title = "<b>产品型号</b>~^<b>产品编码</b>~^120^<b>重量</b>~^<b>体积(m3)</b>~^<b>长度(cm)</b>~^<b>宽度(cm)</b>~^<b>深度(cm)</b>~^<b只数/套</b>~^<b>套数/箱</b>~^<b>箱数/货盘</b>~^";
            ExportExcel(title, table, false);
        }
        else
        {
            this.Alert("导入失败！");
        }

    }

    public Boolean ImportExcelFile()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        int ErrorRecord = 0;
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

        //Modified By Shanzm 2012-12-27：唯一字符串可以设定为“年月日时分秒毫秒”
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        strFileName = strCatFolder + "\\" + strKey + strUserFileName;

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
                    dt = GetExcelContents(strFileName);//this.GetExcelContents(strFileName, 1);
                }
                catch
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('导入文件必须是Excel格式或者模板及内容正确!');";
                    return false;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string code = dt.Rows[i]["部件号"].ToString();
                        string qad = dt.Rows[i]["QAD号"].ToString();
                        string strSQL = "sp_product_insertProductSizeExport";
                        SqlParameter[] parms = new SqlParameter[4];
                        parms[0] = new SqlParameter("@code", code);
                        parms[1] = new SqlParameter("@qad", qad);
                        parms[2] = new SqlParameter("@uID", Session["uID"]);
                        parms[3] = new SqlParameter("@uName", Session["uName"]);
                        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, strSQL, parms);
                    }
                    catch
                    {
                        return false;
                    }
                }
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }

            }
        }

        if (ErrorRecord <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    #region 采用NPOI读取Excel
    /// <summary>
    /// 采用NPOI读取Excel
    /// </summary>
    /// <param name="excelPath">要读取的Excel路径</param>
    /// <param name="header">不能为空！验证Excel表头。格式是：客户,物料号,价格</param>
    /// <returns></returns>
    public DataTable GetExcelContents(string excelPath, int Num)
    {
        string ext = Path.GetExtension(excelPath);
        DataTable dt = null;
        if (ext == ".xls")
        {
            dt = GetExcelContent2003(excelPath, Num);
        }
        else
        {
            dt = GetExcelContent2007(excelPath, Num);
        }
        return dt;
    }
    #endregion

    /// <summary>
    /// 采用NPOI读取Excel2003
    /// </summary>
    /// <param name="excelPath">要读取的Excel路径</param>
    /// <param name="header">不能为空！验证Excel表头。格式是：客户,物料号,价格</param>
    /// <returns></returns>
    public DataTable GetExcelContent2003(string excelPath, int count)
    {
        if (File.Exists(excelPath))
        {
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            FileStream fileStream = new FileStream(excelPath, FileMode.Open);
            IWorkbook workbook = new HSSFWorkbook(fileStream);

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheetAt(count);

            DataTable table = new DataTable();
            //获取sheet的首行
            IRow headerRow = sheet.GetRow(0);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum + 1;

            for (int i = (sheet.FirstRowNum + 1); i < rowCount; i++)
            {
                try
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            switch (cell.CellType)
                            {
                                case CellType.Blank:
                                    dataRow[j] = "";
                                    break;
                                case CellType.String:
                                    dataRow[j] = cell.StringCellValue;
                                    break;
                                case CellType.Numeric:
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = cell.DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = cell.NumericCellValue;
                                    }
                                    break;
                                case CellType.Formula:
                                    HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(workbook);
                                    dataRow[j] = e.Evaluate(cell).StringValue;
                                    break;
                                default:
                                    dataRow[j] = cell.ToString();
                                    break;
                            }
                        }
                    }

                    table.Rows.Add(dataRow);
                }
                catch
                {
                    continue;
                }
            }

            workbook = null;
            sheet = null;

            return table;
        }
        else
        {
            return null;
        }
    }

    /// <summary>  
    /// 采用NPOI读取Excel2007
    /// 将Excel文件中的数据读出到DataTable中(xlsx)  
    /// </summary>  
    /// <param name="file"></param>  
    /// <returns></returns>  
    public DataTable GetExcelContent2007(string file, int count)
    {
        DataTable dt = new DataTable();
        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
            ISheet sheet = xssfworkbook.GetSheetAt(count);

            //表头  
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            List<int> columns = new List<int>();
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueTypeForXLSX(header.GetCell(i) as XSSFCell);
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                    //continue;  
                }
                else
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                columns.Add(i);
            }
            //数据  
            for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                bool hasValue = false;
                foreach (int j in columns)
                {
                    dr[j] = GetValueTypeForXLSX(sheet.GetRow(i).GetCell(j) as XSSFCell);
                    if (dr[j] != null && dr[j].ToString() != string.Empty)
                    {
                        hasValue = true;
                    }
                }
                if (hasValue)
                {
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }

    /// <summary>  
    /// 获取单元格类型(xlsx)  
    /// </summary>  
    /// <param name="cell"></param>  
    /// <returns></returns>  
    private static object GetValueTypeForXLSX(XSSFCell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank:
                return null;
            case CellType.Boolean:
                return cell.BooleanCellValue;
            case CellType.Numeric:
                return cell.NumericCellValue;
            case CellType.String:
                return cell.StringCellValue;
            case CellType.Error:
                return cell.ErrorCellValue;
            case CellType.Formula:
            default:
                return "=" + cell.CellFormula;
        }
    }

}
