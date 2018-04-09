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
            string title = "<b>��Ʒ�ͺ�</b>~^<b>��Ʒ����</b>~^120^<b>����</b>~^<b>���(m3)</b>~^<b>����(cm)</b>~^<b>���(cm)</b>~^<b>���(cm)</b>~^<bֻ��/��</b>~^<b>����/��</b>~^<b>����/����</b>~^";
            ExportExcel(title, table, false);
        }
        else
        {
            this.Alert("����ʧ�ܣ�");
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
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return false;
            }

        }
        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('��ѡ�����ļ�.');";
            return false;
        }
        strUserFileName = strFileName;

        //Modified By Shanzm 2012-12-27��Ψһ�ַ��������趨Ϊ��������ʱ������롱
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        strFileName = strCatFolder + "\\" + strKey + strUserFileName;

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('�ϴ����ļ����Ϊ 8 MB!');";
                return false;
            }
            try
            {
                filename1.PostedFile.SaveAs(strFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
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

                    ltlAlert.Text = "alert('�����ļ�������Excel��ʽ����ģ�弰������ȷ!');";
                    return false;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        string code = dt.Rows[i]["������"].ToString();
                        string qad = dt.Rows[i]["QAD��"].ToString();
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

    #region ����NPOI��ȡExcel
    /// <summary>
    /// ����NPOI��ȡExcel
    /// </summary>
    /// <param name="excelPath">Ҫ��ȡ��Excel·��</param>
    /// <param name="header">����Ϊ�գ���֤Excel��ͷ����ʽ�ǣ��ͻ�,���Ϻ�,�۸�</param>
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
    /// ����NPOI��ȡExcel2003
    /// </summary>
    /// <param name="excelPath">Ҫ��ȡ��Excel·��</param>
    /// <param name="header">����Ϊ�գ���֤Excel��ͷ����ʽ�ǣ��ͻ�,���Ϻ�,�۸�</param>
    /// <returns></returns>
    public DataTable GetExcelContent2003(string excelPath, int count)
    {
        if (File.Exists(excelPath))
        {
            //����·��ͨ���Ѵ��ڵ�excel������HSSFWorkbook��������excel�ĵ�
            FileStream fileStream = new FileStream(excelPath, FileMode.Open);
            IWorkbook workbook = new HSSFWorkbook(fileStream);

            //��ȡexcel�ĵ�һ��sheet
            ISheet sheet = workbook.GetSheetAt(count);

            DataTable table = new DataTable();
            //��ȡsheet������
            IRow headerRow = sheet.GetRow(0);

            //һ�����һ������ı�� ���ܵ�����
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            //���һ�еı��  ���ܵ�����
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
    /// ����NPOI��ȡExcel2007
    /// ��Excel�ļ��е����ݶ�����DataTable��(xlsx)  
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

            //��ͷ  
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
            //����  
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
    /// ��ȡ��Ԫ������(xlsx)  
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
