using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Diagnostics;
using System.Configuration;
using Excel;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using ExcelHelper;
using System.Drawing;
using QCProgress;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Data.Common;

/// <summary>
/// Summary description for QCExcel
/// </summary>
public partial class QCExcel
{
    private ExcelHelper.ExcelHelper _excel;
    private string _templetFile;
    private string _outputFile;

    adamClass adam = new adamClass();

    public QCExcel(string templetFilePath, string outputFilePath)
    {
        _excel = new ExcelHelper.ExcelHelper(templetFilePath, outputFilePath);

        _templetFile = templetFilePath;//��
        _outputFile = outputFilePath;//��
    }

    public QCExcel()
    {
        _excel = null;

        _templetFile = string.Empty;
        _outputFile = string.Empty;
    }

    private void KillProcess(string processName)
    {
        System.Diagnostics.Process myproc = new System.Diagnostics.Process();
        //�õ����д򿪵Ľ���
        try
        {
            foreach (Process thisproc in Process.GetProcessesByName(processName))
            {
                if (!thisproc.CloseMainWindow())
                {
                    thisproc.Kill();
                }
            }
        }
        catch (Exception ex)
        {
            //throw ex
        }
    }

    /// <summary>
    /// ���ϼ����ձ���:����
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyIncoming(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Incoming";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch(Exception ex)
        {
            return null;
        }
    }
    /// <summary>
    /// ���ϼ����ձ���:дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportIncoming(int uID, string stddate, string enddate)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        ISheet detailSheet = workbook.GetSheetAt(1);

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyIncoming(uID, stddate, enddate);

        int nCols = _dataset.Tables[0].Columns.Count;
        int nRows = 3;

        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            iRow.CreateCell(0).SetCellValue(row[0]);
            iRow.CreateCell(1).SetCellValue(row[1]);
            iRow.CreateCell(2).SetCellValue(row[2]);
            iRow.CreateCell(3).SetCellValue(row[3]);
            iRow.CreateCell(5).SetCellValue(row[4]);
            iRow.CreateCell(6).SetCellValue(row[5]);
            iRow.CreateCell(7).SetCellValue(row[6]);
            iRow.CreateCell(8).SetCellValue(row[7]);
            iRow.CreateCell(9).SetCellValue(row[8]);
            iRow.CreateCell(10).SetCellValue(row[9]);
            iRow.CreateCell(11).SetCellValue(row[10]);
            iRow.CreateCell(12).SetCellValue(row[11]);
            iRow.CreateCell(13).SetCellValue(row[12]);
            iRow.CreateCell(16).SetCellValue(row[13]);
            iRow.CreateCell(17).SetCellValue(row[14]);
            iRow.CreateCell(18).SetCellValue(row[15]);

            DataRow[] rows = _dataset.Tables[0].Select("QAD�� = '" + row[1].ToString() + "' AND ��Ӧ�� = '" + row[2].ToString() + "' AND ������Ŀ = '" + row[3].ToString() + "' AND ���� = '" + row[4].ToString() + "' AND prh_group = '" + row[16].ToString() + "'");
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    iRow.CreateCell(14).SetCellValue(row[4]);
                    iRow.CreateCell(15).SetCellValue(row[5]);

                    nRows++;
                }
            }
            else
            {
                nRows++;
            }
        }

        //��ϸ��
        nRows = 2;
        foreach (DataRow row in _dataset.Tables[1].Rows)
        {
            IRow iDeitalRow = detailSheet.CreateRow(nRows);

            iDeitalRow.CreateCell(0).SetCellValue(row[0]);
            iDeitalRow.CreateCell(1).SetCellValue(row[1]);
            iDeitalRow.CreateCell(2).SetCellValue(row[2]);
            iDeitalRow.CreateCell(3).SetCellValue(row[3]);
            iDeitalRow.CreateCell(4).SetCellValue(row[4]);
            iDeitalRow.CreateCell(5).SetCellValue(row[5]);
            iDeitalRow.CreateCell(6).SetCellValue(row[6]);
            iDeitalRow.CreateCell(7).SetCellValue(row[7]);
            iDeitalRow.CreateCell(8).SetCellValue(row[8]);

            nRows++;
        }
        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                detailSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ���ϼ����ձ���:Ԫ����
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyIncomingCom(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Incoming_Com";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ���ϼ����ձ���:Ԫ����
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportIncomingCom(int uID, string stddate, string enddate)
    {   
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyIncomingCom(uID, stddate, enddate);

        int nCols = _dataset.Tables[0].Columns.Count;
        int nRows = 3;

        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            iRow.CreateCell(0).SetCellValue(row[0]);
            iRow.CreateCell(1).SetCellValue(row[1]);
            iRow.CreateCell(2).SetCellValue(row[2]);
            iRow.CreateCell(3).SetCellValue(row[3]);
            iRow.CreateCell(5).SetCellValue(row[4]);
            iRow.CreateCell(6).SetCellValue(row[5]);
            iRow.CreateCell(7).SetCellValue(row[6]);
            iRow.CreateCell(8).SetCellValue(row[7]);
            iRow.CreateCell(9).SetCellValue(row[8]);
            iRow.CreateCell(10).SetCellValue(row[9]);
            iRow.CreateCell(11).SetCellValue(row[10]);
            iRow.CreateCell(14).SetCellValue(row[11]);
            iRow.CreateCell(15).SetCellValue(row[12]);
            iRow.CreateCell(16).SetCellValue(row[13]);

            DataRow[] rows = _dataset.Tables[1].Select("QAD�� = '" + row[1].ToString() + "' AND ��Ӧ�� = '" + row[2].ToString() + "' AND ������Ŀ = '" + row[3].ToString() + "' AND ���� = '" + row[4].ToString() + "' AND prh_group = '" + row[14].ToString() + "'");

            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    iRow.CreateCell(12).SetCellValue(row[4]);
                    iRow.CreateCell(13).SetCellValue(row[5]);

                    nRows++;
                }
            }
            else
            {
                nRows++;
            }
        }

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ���ϼ����ձ���:�㲿��
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyIncomingPart(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Incoming_Part";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ���ϼ����ձ���:�㲿��
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportIncomingPart(int uID, string stddate, string enddate)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyIncomingPart(uID, stddate, enddate);

        int nCols = _dataset.Tables[0].Columns.Count;
        int nRows = 3;

        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            iRow.CreateCell(0).SetCellValue(row[0]);
            iRow.CreateCell(1).SetCellValue(row[1]);
            iRow.CreateCell(2).SetCellValue(row[2]);
            iRow.CreateCell(3).SetCellValue(row[3]);
            iRow.CreateCell(5).SetCellValue(row[4]);
            iRow.CreateCell(6).SetCellValue(row[5]);
            iRow.CreateCell(7).SetCellValue(row[6]);
            iRow.CreateCell(8).SetCellValue(row[7]);
            iRow.CreateCell(9).SetCellValue(row[8]);
            iRow.CreateCell(10).SetCellValue(row[9]);
            iRow.CreateCell(11).SetCellValue(row[10]);
            iRow.CreateCell(12).SetCellValue(row[11]);
            iRow.CreateCell(13).SetCellValue(row[12]);
            iRow.CreateCell(14).SetCellValue(row[13]);
            iRow.CreateCell(17).SetCellValue(row[14]);
            iRow.CreateCell(18).SetCellValue(row[15]);
            iRow.CreateCell(19).SetCellValue(row[16]);

            DataRow[] rows = _dataset.Tables[1].Select("QAD�� = '" + row[1].ToString() + "' AND ��Ӧ�� = '" + row[2].ToString() + "' AND ������Ŀ = '" + row[3].ToString() + "' AND ���� = '" + row[4].ToString() + "' AND prh_group = '" + row[17].ToString() + "'");
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    iRow.CreateCell(15).SetCellValue(row[4]);
                    iRow.CreateCell(16).SetCellValue(row[5]);

                    nRows++;
                }
            }
            else
            {
                nRows++;
            }
        }

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ���ϼ����ձ���:��װ
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyIncomingPackage(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Incoming_Package";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ���ϼ����ձ���:��װ
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportIncomingPackage(int uID, string stddate, string enddate)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyIncomingPackage(uID, stddate, enddate);

        int nCols = _dataset.Tables[0].Columns.Count;
        int nRows = 3;

        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            iRow.CreateCell(0).SetCellValue(row[0]);
            iRow.CreateCell(1).SetCellValue(row[1]);
            iRow.CreateCell(2).SetCellValue(row[2]);
            iRow.CreateCell(3).SetCellValue(row[3]);
            iRow.CreateCell(5).SetCellValue(row[4]);
            iRow.CreateCell(6).SetCellValue(row[5]);
            iRow.CreateCell(7).SetCellValue(row[6]);
            iRow.CreateCell(8).SetCellValue(row[7]);
            iRow.CreateCell(9).SetCellValue(row[8]);
            iRow.CreateCell(10).SetCellValue(row[9]);
            iRow.CreateCell(13).SetCellValue(row[10]);
            iRow.CreateCell(14).SetCellValue(row[11]);
            iRow.CreateCell(15).SetCellValue(row[12]);

            DataRow[] rows = _dataset.Tables[1].Select("QAD�� = '" + row[1].ToString() + "' AND ��Ӧ�� = '" + row[2].ToString() + "' AND ������Ŀ = '" + row[3].ToString() + "' AND ���� = '" + row[4].ToString() + "' AND prh_group = '" + row[13].ToString() + "'");
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    iRow.CreateCell(11).SetCellValue(row[4]);
                    iRow.CreateCell(12).SetCellValue(row[5]);

                    nRows++;
                }
            }
            else
            {
                nRows++;
            }
        }

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ���ϼ����ձ���:����
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyIncomingAcc(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Incoming_Acc";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ���ϼ����ձ���:����
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportIncomingAcc(int uID, string stddate, string enddate)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyIncomingAcc(uID, stddate, enddate);

        int nCols = _dataset.Tables[0].Columns.Count;
        int nRows = 3;

        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            iRow.CreateCell(0).SetCellValue(row[0]);
            iRow.CreateCell(1).SetCellValue(row[1]);
            iRow.CreateCell(2).SetCellValue(row[2]);
            iRow.CreateCell(3).SetCellValue(row[3]);
            iRow.CreateCell(5).SetCellValue(row[4]);
            iRow.CreateCell(6).SetCellValue(row[5]);
            iRow.CreateCell(7).SetCellValue(row[6]);
            iRow.CreateCell(8).SetCellValue(row[7]);
            iRow.CreateCell(9).SetCellValue(row[8]);
            iRow.CreateCell(10).SetCellValue(row[9]);
            iRow.CreateCell(11).SetCellValue(row[10]);
            iRow.CreateCell(14).SetCellValue(row[11]);
            iRow.CreateCell(15).SetCellValue(row[12]);
            iRow.CreateCell(16).SetCellValue(row[13]);

            DataRow[] rows = _dataset.Tables[1].Select("QAD�� = '" + row[1].ToString() + "' AND ��Ӧ�� = '" + row[2].ToString() + "' AND ������Ŀ = '" + row[3].ToString() + "' AND ���� = '" + row[4].ToString() + "' AND prh_group = '" + row[14].ToString() + "'");
            if (rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    iRow.CreateCell(12).SetCellValue(row[4]);
                    iRow.CreateCell(13).SetCellValue(row[5]);

                    nRows++;
                }
            }
            else
            {
                nRows++;
            }
        }

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ����DataTable�������ͻ�ȡָ����ʽ�����С�����ȣ�
    /// </summary>
    /// <param name="workbook"></param>
    /// <param name="dataType"></param>
    /// <returns></returns>
    private ICellStyle GetColumnStyleByDataType(IWorkbook workbook, string dataType)
    {
        ICellStyle style = workbook.CreateCellStyle();

        switch (dataType)
        {
            case "System.DateTime":
                style.Alignment = HorizontalAlignment.Center;
                break;
            case "System.Int16":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int32":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int64":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Decimal":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Double":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Boolean":
                style.Alignment = HorizontalAlignment.Center;
                break;
            case "System.String":
                style.Alignment = HorizontalAlignment.Left;
                break;
        }

        return style;
    }
    /// <summary>
    /// д�ձ�����
    /// </summary>
    /// <param name="workSheet"></param>
    /// <param name="dataTable"></param>
    private void fnProductCore(ISheet workSheet, System.Data.DataTable dataTable)
    {
        /*
         * д������ ��"��ע"ǰ�������кϲ�
         */
        int nRet = 0;//��"��ע"���к�
        //��һ����prdID
        for (int nCol = 1; nCol <= dataTable.Columns.Count; nCol++)
        {
            if (dataTable.Columns[nCol].ColumnName.Trim() == "��ע")
            {
                nRet = nCol;
                break;
            }
        }

        //��"��ע"ǰ���й̶�

        ICellStyle styleHeader = workSheet.Workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//���ж���
        styleHeader.VerticalAlignment = VerticalAlignment.Top;

        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillPattern = FillPattern.SolidForeground;

        styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        NPOI.SS.UserModel.IFont fontHeader = workSheet.Workbook.CreateFont();
        fontHeader.FontHeightInPoints = 10;
        fontHeader.Boldweight = 600;
        styleHeader.SetFont(fontHeader);

        //��ΪҪ�ϲ���ͷ��������1��2ͬʱ��ֵ
        IRow headerRow1 = workSheet.CreateRow(0);
        IRow headerRow2 = workSheet.CreateRow(1);

        for (int nCol = 0; nCol < nRet; nCol++)
        {
            ICell cell1 = headerRow1.CreateCell(nCol);
            ICell cell2 = headerRow2.CreateCell(nCol);

            workSheet.AddMergedRegion(new CellRangeAddress(0, 1, nCol, nCol));

            cell1.CellStyle = styleHeader;
            cell2.CellStyle = styleHeader;
 
            cell1.SetCellValue(dataTable.Columns[nCol + 1].ColumnName.Trim());

            ICellStyle columnStyle1 = GetColumnStyleByDataType(workSheet.Workbook, dataTable.Columns[nCol].DataType.ToString());
            workSheet.SetDefaultColumnStyle(nCol, columnStyle1);
        }

        ////������ж�̬����
        int nStart = nRet;
        int nEnd = nStart;
        string strStateType = String.Empty;

        for (int nCol = nRet; nCol < dataTable.Columns.Count - 1; nCol++)
        {
            nEnd = nCol;

            ICell cell3 = headerRow1.CreateCell(nCol);
            ICell cell4 = headerRow2.CreateCell(nCol);

            cell3.CellStyle = styleHeader;
            cell4.CellStyle = styleHeader;

            if (strStateType != dataTable.Columns[nCol + 1].ColumnName.Trim().Split('|')[0])
            {
                if (strStateType != string.Empty)
                {
                    workSheet.AddMergedRegion(new CellRangeAddress(0, 0, nStart, nEnd - 1));
                }

                nStart = nCol;

                strStateType = dataTable.Columns[nCol + 1].ColumnName.Trim().Split('|')[0];
                cell3.SetCellValue(strStateType);
            }

            cell4.SetCellValue(dataTable.Columns[nCol + 1].ColumnName.Trim().Split('|')[1]);

            ICellStyle columnStyle2 = GetColumnStyleByDataType(workSheet.Workbook, dataTable.Columns[nCol + 1].DataType.ToString());
            workSheet.SetDefaultColumnStyle(nCol, columnStyle2);
        }
        //�ϲ����һ��
        if (nStart != nEnd)
        {
            workSheet.AddMergedRegion(new CellRangeAddress(0, 0, nStart, nEnd));
        }

        /*
        * д����
        */
        int nRows = 2;

        foreach (DataRow row in dataTable.Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            
            //��"��ע"ǰ���й̶� ���У�prdID
            for (int nCol = 1; nCol <= nRet; nCol++)
            {
                double d;
                iRow.CreateCell(nCol - 1).SetCellValue(row[nCol]);

            }
            //������ж�̬����
            for (int nCol = nRet + 1; nCol < dataTable.Columns.Count; nCol++)
            {
                double d;
                iRow.CreateCell(nCol - 1).SetCellValue(row[nCol]);
            }

            nRows++;
        }
    }

    /// <summary>
    /// ���Ƴ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProduct(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ���Ƴ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyLED(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_LED";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch(Exception ex)
        {
            return null;
        }
    }
    /// <summary>
    /// ���Ƴ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyDSD(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_DSD";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    /// <summary>
    /// CFL���Ƴ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProduct(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "CFL���Ƴ�Ʒ�����ձ���");
        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProduct(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// LED���Ƴ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportLED(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "LED���Ƴ�Ʒ�����ձ���");
        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyLED(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ��˿�Ƴ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportDSD(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "��˿�Ƽ����ձ���");
        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyDSD(uID, stddate, enddate, nPlantID);
        if (_dataset == null)
        {
            return;
        }
        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ���ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductMingG(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_MingG";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ���ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductMingG(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "���ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductMingG(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ë�ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductCap(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Cap";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ë�ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductCap(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "ë�ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductCap(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ��������Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductBallast(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Ballast";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ��������Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductBallast(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "��������Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductBallast(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ֱ�ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductStr(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Str";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ֱ�ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductStr(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "���ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductStr(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// ������Tcpë�ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyTcpCap(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Tcp_Cap";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// Tcpë�ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportTcpCap(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("ë�ܳ�Ʒ�����ձ���(ȫ��)");
        ISheet workSheet2 = workbook.CreateSheet("ë�ܳ�Ʒ�����ձ���(ɸѡ)");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyTcpCap(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet1, _dataset.Tables[0]);
        fnProductCore(workSheet2, _dataset.Tables[1]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// ������Tcp CFL��Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyTcp(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Tcp";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ������Tcp LED��Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyTcpLED(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Tcp_LED";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ������Tcp LED��Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyTcpDSD(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Tcp_DSD";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// Tcp��Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportTcp(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("CFL���Ƴ�Ʒ�����ձ���(ȫ��)");
        ISheet workSheet2 = workbook.CreateSheet("CFL���Ƴ�Ʒ�����ձ���(ɸѡ)");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyTcp(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet1, _dataset.Tables[0]);
        fnProductCore(workSheet2, _dataset.Tables[1]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// Tcp��Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportTcpLED(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("���Ƴ�Ʒ�����ձ���(ȫ��)");
        ISheet workSheet2 = workbook.CreateSheet("���Ƴ�Ʒ�����ձ���(ɸѡ)");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyTcpLED(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet1, _dataset.Tables[0]);
        fnProductCore(workSheet2, _dataset.Tables[1]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ��˿���ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportTcpDSD(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("��˿�Ƽ����ձ���(ȫ��)");
        ISheet workSheet2 = workbook.CreateSheet("��˿�Ƽ����ձ���(ɸѡ)");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyTcpDSD(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet1, _dataset.Tables[0]);
        fnProductCore(workSheet2, _dataset.Tables[1]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ������Tcp��������Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyTcpBallast(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Tcp_Ballast";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// Tcp��������Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportTcpBallast(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("��������Ʒ�����ձ���(ȫ��)");
        ISheet workSheet2 = workbook.CreateSheet("��������Ʒ�����ձ���(ɸѡ)");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyTcpBallast(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet1, _dataset.Tables[0]);
        fnProductCore(workSheet2, _dataset.Tables[1]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// д�ձ�����
    /// </summary>
    /// <param name="workSheet"></param>
    /// <param name="dataTable"></param>
    private void fnProcessCore1(ISheet workSheet, System.Data.DataTable dataTable)
    {
        /*
         * д������ ǰ15�е�ǰ���кϲ�
         */
        //���ñ߿򡢶��뷽ʽ
        ICellStyle styleHeader = workSheet.Workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//���ж���
        styleHeader.VerticalAlignment = VerticalAlignment.Center;

        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillPattern = FillPattern.SolidForeground;

        styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        NPOI.SS.UserModel.IFont fontHeader = workSheet.Workbook.CreateFont();
        fontHeader.FontHeightInPoints = 10;
        fontHeader.Boldweight = 600;
        styleHeader.SetFont(fontHeader);

        //��ΪҪ�ϲ���ͷ��������1��2ͬʱ��ֵ
        IRow headerRow1 = workSheet.CreateRow(0);
        IRow headerRow2 = workSheet.CreateRow(1);

        //ǰ15�й̶�
        int dynStartColIndex = 15;
        for (int nCol = 0; nCol < dynStartColIndex; nCol++)
        {
            ICell cell1 = headerRow1.CreateCell(nCol);
            ICell cell2 = headerRow2.CreateCell(nCol);

            workSheet.AddMergedRegion(new CellRangeAddress(0, 1, nCol, nCol));

            cell1.CellStyle = styleHeader;
            cell2.CellStyle = styleHeader;
 
            cell1.SetCellValue(dataTable.Columns[nCol + 1].ColumnName.Trim());

            ICellStyle columnStyle1 = GetColumnStyleByDataType(workSheet.Workbook, dataTable.Columns[nCol+1].DataType.ToString());
            workSheet.SetDefaultColumnStyle(nCol, columnStyle1);
        }
        //��ǰ13�зֱ�����������ʽ
        IDataFormat format =workSheet.Workbook.CreateDataFormat();
        workSheet.GetColumnStyle(3).DataFormat=format.GetFormat("#");
        workSheet.GetColumnStyle(13).DataFormat=HSSFDataFormat.GetBuiltinFormat("0.00%"); 
        
        //������ж�̬����
        for (int nCol = 0; nCol < dataTable.Columns.Count - dynStartColIndex - 1; nCol++)
        {
            
            ICell cell3 = headerRow1.CreateCell(dynStartColIndex + nCol * 3);
            ICell cell4 = headerRow1.CreateCell(dynStartColIndex + nCol * 3 + 1);
            ICell cell5 = headerRow1.CreateCell(dynStartColIndex + nCol * 3 + 2);
            workSheet.AddMergedRegion(new CellRangeAddress(0, 0, dynStartColIndex + nCol * 3, dynStartColIndex + nCol * 3 + 2));
            cell3.SetCellValue(dataTable.Columns[dynStartColIndex + 1 + nCol].ColumnName.Trim());

            cell3.CellStyle = styleHeader;
            cell4.CellStyle = styleHeader;
            cell5.CellStyle = styleHeader;

            ICell cell6 = headerRow2.CreateCell(dynStartColIndex + nCol * 3);
            ICell cell7 = headerRow2.CreateCell(dynStartColIndex + nCol * 3 + 1);
            ICell cell8 = headerRow2.CreateCell(dynStartColIndex + nCol * 3 + 2);
            cell6.SetCellValue("������");
            cell7.SetCellValue("������");
            cell8.SetCellValue("������ƷPPM");

            cell6.CellStyle = styleHeader;
            cell7.CellStyle = styleHeader;
            cell8.CellStyle = styleHeader;

            ICellStyle columnStyle1 = GetColumnStyleByDataType(workSheet.Workbook, dataTable.Columns[dynStartColIndex + 1 + nCol].DataType.ToString());
            workSheet.SetDefaultColumnStyle(dynStartColIndex + nCol * 3, columnStyle1);
            workSheet.SetDefaultColumnStyle(dynStartColIndex + nCol * 3 + 1, columnStyle1);
            workSheet.SetDefaultColumnStyle(dynStartColIndex + nCol * 3 + 2, columnStyle1);
        }

        /*
        * д����
        */
        ICellStyle cellStyle = workSheet.Workbook.CreateCellStyle();
        cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%"); 
        int nRows = 2;
        foreach (DataRow row in dataTable.Rows)
        {
            IRow iRow=workSheet.CreateRow(nRows);
            //ǰ52�й̶�
            for (int nCol = 0; nCol < dynStartColIndex; nCol++)
            {
                iRow.CreateCell(nCol).SetCellValue(row[nCol+1]);
            }
            //������ж�̬����
            for (int nCol = 0; nCol < dataTable.Columns.Count - dynStartColIndex - 1; nCol++)
            {
                iRow.CreateCell(dynStartColIndex + nCol * 3).SetCellValue(row[dynStartColIndex + 1 + nCol]);

                if (row[dynStartColIndex + 1 + nCol].ToString() != String.Empty)
                {
                    ICell cell = iRow.CreateCell(dynStartColIndex + nCol * 3 + 1);
                   
                    cell.SetCellValue(Convert.ToDouble(row[dynStartColIndex + 1 + nCol].ToString()) / Convert.ToDouble(row[11].ToString()));
                    cell.CellStyle = cellStyle;
                    iRow.CreateCell(dynStartColIndex + nCol * 3 + 2).SetCellValue(Convert.ToDouble(row[dynStartColIndex + 1 + nCol].ToString()) / Convert.ToDouble(row[11].ToString()) * 1000000);
                }
            }

            nRows++;
        }
    }
    /// <summary>
    /// д��������ĺ���
    /// </summary>
    /// <param name="workSheet"></param>
    /// <param name="dataTable"></param>
    private void fnProcessCore2(ISheet workSheet, System.Data.DataTable dataTable)
    {
        //���ñ߿򡢶��뷽ʽ
        ICellStyle styleHeader = workSheet.Workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//���ж���
        styleHeader.VerticalAlignment = VerticalAlignment.Center;

        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillPattern = FillPattern.SolidForeground;

        styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        NPOI.SS.UserModel.IFont fontHeader = workSheet.Workbook.CreateFont();
        fontHeader.FontHeightInPoints = 10;
        fontHeader.Boldweight = 600;
        styleHeader.SetFont(fontHeader);

        IRow headerRow1 = workSheet.CreateRow(0);
        IRow headerRow2 = workSheet.CreateRow(1);
        IRow headerRow3 = workSheet.CreateRow(2);
        int dynStartColIndex = 16;
        //ǰ14�й̶�
        for (int nCol = 0; nCol < dynStartColIndex; nCol++)
        {
            ICell cell1 = headerRow1.CreateCell(nCol);
            ICell cell2 = headerRow2.CreateCell(nCol);
            ICell cell3 = headerRow3.CreateCell(nCol);

            workSheet.AddMergedRegion(new CellRangeAddress(0, 2, nCol, nCol));
            cell1.SetCellValue(dataTable.Columns[nCol + 1].ColumnName.Trim());

            cell1.CellStyle = styleHeader;
            cell2.CellStyle = styleHeader;
            cell3.CellStyle = styleHeader;

            ICellStyle columnStyle1 = GetColumnStyleByDataType(workSheet.Workbook, dataTable.Columns[nCol + 1].DataType.ToString());
            workSheet.SetDefaultColumnStyle(nCol, columnStyle1);
        }
        //��ǰ14�� �ֱ�����������ʽ
        IDataFormat format = workSheet.Workbook.CreateDataFormat();
        workSheet.GetColumnStyle(3).DataFormat = format.GetFormat("#");//��4����QAD��
        workSheet.GetColumnStyle(13).DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%"); //��12�� �ϸ��� ��ʾ�ٷֱ�

        //������ж�̬����

        string strStateType = String.Empty;
        int nStart = dynStartColIndex;
        int nEnd = dynStartColIndex;

        for (int nCol = 0; nCol < dataTable.Columns.Count - dynStartColIndex-1; nCol++)
        {
            nEnd = dynStartColIndex + nCol * 2 + 1;

            ICell cell4 = headerRow1.CreateCell(nStart);
            ICell cell5 = headerRow1.CreateCell(nEnd);

            cell4.CellStyle = styleHeader;
            cell5.CellStyle = styleHeader;
            if (strStateType != dataTable.Columns[dynStartColIndex + 1 + nCol].ColumnName.Trim().Split('|')[0])
            {
                strStateType = dataTable.Columns[dynStartColIndex + 1 + nCol].ColumnName.Trim().Split('|')[0];
                nStart = dynStartColIndex + nCol * 2;

            }
            workSheet.AddMergedRegion(new CellRangeAddress(0, 0, nStart, nEnd));
            cell4.SetCellValue(strStateType + "ȱ�ݼ�������");

            ICell cell6 = headerRow2.CreateCell(dynStartColIndex + nCol * 2);
            ICell cell7 = headerRow2.CreateCell(dynStartColIndex + nCol * 2+1);
            workSheet.AddMergedRegion(new CellRangeAddress(1, 1, dynStartColIndex + nCol * 2, dynStartColIndex + nCol * 2 + 1));
            cell6.SetCellValue(dataTable.Columns[dynStartColIndex + 1 + nCol].ColumnName.Trim().Split('|')[1]);
            cell6.CellStyle = styleHeader;
            cell7.CellStyle = styleHeader;

            ICell cell8 = headerRow3.CreateCell(dynStartColIndex + nCol * 2);
            cell8.SetCellValue("����");
            cell8.CellStyle = styleHeader;
            ICell cell9 = headerRow3.CreateCell(dynStartColIndex + nCol * 2 + 1);
            cell9.SetCellValue("ȱ�ݱ���");
            cell9.CellStyle = styleHeader;

            //xlRange = (Excel.Range)workSheet.Columns[14 + nCol * 2 + 1, Type.Missing];//������ ��ʾ�ٷֱ�
            //xlRange.EntireColumn.NumberFormatLocal = "0.00%";
        }

        /*
        * д����
        */
        ICellStyle cellStyle = workSheet.Workbook.CreateCellStyle();
        cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%"); 
        int nRows = 3;

        foreach (DataRow row in dataTable.Rows)
        {
            IRow iRow = workSheet.CreateRow(nRows);
            //ǰ15�й̶�
            for (int nCol = 0; nCol < dynStartColIndex; nCol++)
            {
                iRow.CreateCell(nCol).SetCellValue(row[nCol + 1]);
            }
            //������ж�̬����
            for (int nCol = 0; nCol < dataTable.Columns.Count - dynStartColIndex - 1; nCol++)
            {
                iRow.CreateCell(dynStartColIndex + nCol * 2).SetCellValue(row[dynStartColIndex + 1 + nCol]);

                if (row[dynStartColIndex + 1 + nCol].ToString() != String.Empty)
                {
                    ICell cell = iRow.CreateCell(dynStartColIndex + nCol * 2 + 1);
                    cell.SetCellValue(Convert.ToDouble(row[dynStartColIndex + 1 + nCol].ToString()) / Convert.ToDouble(row[11].ToString()));
                    cell.CellStyle = cellStyle;
                }
            }

            nRows++;
        }
    }
    /// <summary>
    /// ���ƹ����ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProcess(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Process";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch(Exception ex)
        {
            return null;
        }
    }
    /// <summary>
    /// ���ƹ��̼����ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProcess(int uID, string stddate, string enddate)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("����׼���ձ���");
        ISheet workSheet2 = workbook.CreateSheet("����׼������");
        ISheet workSheet3 = workbook.CreateSheet("������װ�ձ���");
        ISheet workSheet4 = workbook.CreateSheet("���Ʋ��Թ���");
        ISheet workSheet5 = workbook.CreateSheet("�����ϻ�����");
        ISheet workSheet6 = workbook.CreateSheet("���Ƴ�Ʒ����");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProcess(uID, stddate, enddate);

 
        fnProcessCore1(workSheet1, _dataset.Tables[0]);
        fnProcessCore2(workSheet2, _dataset.Tables[1]);

        fnProcessCore1(workSheet3, _dataset.Tables[2]);
        fnProcessCore2(workSheet4, _dataset.Tables[3]);
        fnProcessCore2(workSheet5, _dataset.Tables[4]);
        fnProcessCore2(workSheet6, _dataset.Tables[5]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workSheet3 = null;
                workSheet4 = null;
                workSheet5 = null;
                workSheet6 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

        // ���Excel�ļ����˳� 


    }

    /// <summary>
    /// LED�����ձ���
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProcessLED(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Process_LED";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// LED���̼����ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProcessLED(int uID, string stddate, string enddate)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("LED��װ�ձ���");
        ISheet workSheet2 = workbook.CreateSheet("LED��ѹ���Թ���");
        ISheet workSheet3 = workbook.CreateSheet("LED��ѹ���Թ���");
        ISheet workSheet4 = workbook.CreateSheet("LED���⡢���ʲ��Թ���");
        ISheet workSheet5 = workbook.CreateSheet("LED�ϻ�����");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProcessLED(uID, stddate, enddate);


        fnProcessCore1(workSheet1, _dataset.Tables[0]);
        fnProcessCore2(workSheet2, _dataset.Tables[1]);
        fnProcessCore2(workSheet3, _dataset.Tables[2]);
        fnProcessCore2(workSheet4, _dataset.Tables[3]);
        fnProcessCore2(workSheet5, _dataset.Tables[4]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workSheet3 = null;
                workSheet4 = null;
                workSheet5 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

        // ���Excel�ļ����˳� 


    }

    /// <summary>
    /// ë�ܹ����ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProcessCap(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Process_Cap";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ë�ܹ��̼����ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProcessCap(int uID, string stddate, string enddate)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("��˿�ձ���");
        ISheet workSheet2 = workbook.CreateSheet("��˿����");
        ISheet workSheet3 = workbook.CreateSheet("�������ձ���");
        ISheet workSheet4 = workbook.CreateSheet("�����ܹ���");
        ISheet workSheet5 = workbook.CreateSheet("���������ձ���");
        ISheet workSheet6 = workbook.CreateSheet("ͿĤͿ�۹���");
        ISheet workSheet7 = workbook.CreateSheet("���ܲ���У�⹤��");
        ISheet workSheet8 = workbook.CreateSheet("��ڹ���");
        ISheet workSheet9 = workbook.CreateSheet("��������");
        ISheet workSheet10 = workbook.CreateSheet("��������");
        ISheet workSheet11 = workbook.CreateSheet("ɨ�蹤��");


        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProcessCap(uID, stddate, enddate);

        fnProcessCore1(workSheet1, _dataset.Tables[0]);
        fnProcessCore2(workSheet2, _dataset.Tables[1]);

        fnProcessCore1(workSheet3, _dataset.Tables[2]);
        fnProcessCore2(workSheet4, _dataset.Tables[3]);

        fnProcessCore1(workSheet5, _dataset.Tables[4]);
        fnProcessCore2(workSheet6, _dataset.Tables[5]);
        fnProcessCore2(workSheet7, _dataset.Tables[6]);
        fnProcessCore2(workSheet8, _dataset.Tables[7]);
        fnProcessCore2(workSheet9, _dataset.Tables[8]);
        fnProcessCore2(workSheet10, _dataset.Tables[9]);
        fnProcessCore2(workSheet11, _dataset.Tables[10]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workSheet3 = null;
                workSheet4 = null;
                workSheet5 = null;
                workSheet6 = null;
                workSheet7 = null;
                workSheet8 = null;
                workSheet9 = null;
                workSheet10 = null;
                workSheet11 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

    }
    /// <summary>
    /// ��ë���ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProcessCapZj(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Process_CapZj";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ��ë�ܼ����ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProcessCapZj(int uID, string stddate, string enddate)
    {
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("��ë���ձ���");
        ISheet workSheet2 = workbook.CreateSheet("ǰ������");
        ISheet workSheet3 = workbook.CreateSheet("�������");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProcessCapZj(uID, stddate, enddate);

        fnProcessCore1(workSheet1, _dataset.Tables[0]);
        fnProcessCore2(workSheet2, _dataset.Tables[1]);
        fnProcessCore2(workSheet3, _dataset.Tables[2]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workSheet3 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        
    }
    /// <summary>
    /// �����������ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProcessBallast(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Process_Ballast";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// �����������ձ���:дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProcessBallast(int uID, string stddate, string enddate)
    {
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProcessBallast(uID, stddate, enddate);

        ISheet workSheet1 = workbook.GetSheetAt(0);

        int nCols = _dataset.Tables[0].Columns.Count;
        int nRows = 2;

        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            int t1 = 0;
            int t2 = 0;
            int t3 = 0;
            int t4 = 0;
            int t5 = 0;
            int t6 = 0;
            int t7 = 0;
            int t8 = 0;
            int t9 = 0;
            int t10 = 0;
            int t11 = 0;

            IRow iRow = workSheet1.CreateRow(nRows);
            iRow.CreateCell(0).SetCellValue(row[1]);
            iRow.CreateCell(1).SetCellValue(row[2]);
            iRow.CreateCell(2).SetCellValue(row[3]);
            iRow.CreateCell(3).SetCellValue(row[4]);
            iRow.CreateCell(4).SetCellValue(row[5]);
            iRow.CreateCell(5).SetCellValue(row[6]);
            iRow.CreateCell(6).SetCellValue(row[7]);
            iRow.CreateCell(7).SetCellValue(row[8]);
            iRow.CreateCell(8).SetCellValue(row[9]);
            iRow.CreateCell(9).SetCellValue(row[10]);
            iRow.CreateCell(10).SetCellValue(row[11]);

            DataRow[] rows = _dataset.Tables[1].Select("prcID = " + row[0].ToString());

            if (rows.Length > 0)
            {
                for (int colIndex = 0; colIndex <= 10; colIndex++)
                {
                    workSheet1.AddMergedRegion(new CellRangeAddress(nRows, nRows + rows.Length - 1, colIndex, colIndex));
                }
            }

            for (int i = 0; i < rows.Length; i++)
            {
                iRow.CreateCell(11).SetCellValue(rows[i][1]);
                iRow.CreateCell(12).SetCellValue(rows[i][2]);
                iRow.CreateCell(13).SetCellValue(rows[i][3]);
                iRow.CreateCell(14).SetCellValue(rows[i][4]);
                iRow.CreateCell(15).SetCellValue(rows[i][5]);
                iRow.CreateCell(16).SetCellValue(rows[i][6]);
                iRow.CreateCell(17).SetCellValue(rows[i][7]);
                iRow.CreateCell(18).SetCellValue(rows[i][8]);
                iRow.CreateCell(19).SetCellValue(rows[i][9]);
                iRow.CreateCell(20).SetCellValue(rows[i][10]);
                iRow.CreateCell(21).SetCellValue(rows[i][11]);
                iRow.CreateCell(22).SetCellValue(rows[i][12]);

                if (rows[i][2].ToString() != string.Empty)
                {
                    t1 += Convert.ToInt32(rows[i][2].ToString());
                }

                if (rows[i][3].ToString() != string.Empty)
                {
                    t2 += Convert.ToInt32(rows[i][3].ToString());
                }

                if (rows[i][4].ToString() != string.Empty)
                {
                    t3 += Convert.ToInt32(rows[i][4].ToString());
                }

                if (rows[i][5].ToString() != string.Empty)
                {
                    t4 += Convert.ToInt32(rows[i][5].ToString());
                }

                if (rows[i][6].ToString() != string.Empty)
                {
                    t5 += Convert.ToInt32(rows[i][6].ToString());
                }

                if (rows[i][7].ToString() != string.Empty)
                {
                    t6 += Convert.ToInt32(rows[i][7].ToString());
                }

                if (rows[i][8].ToString() != string.Empty)
                {
                    t7 += Convert.ToInt32(rows[i][8].ToString());
                }

                if (rows[i][9].ToString() != string.Empty)
                {
                    t8 += Convert.ToInt32(rows[i][9].ToString());
                }

                if (rows[i][10].ToString() != string.Empty)
                {
                    t9 += Convert.ToInt32(rows[i][10].ToString());
                }

                if (rows[i][11].ToString() != string.Empty)
                {
                    t10 += Convert.ToInt32(rows[i][11].ToString());
                }

                if (rows[i][12].ToString() != string.Empty)
                {
                    t11 += Convert.ToInt32(rows[i][12].ToString());
                }

                nRows++;
                iRow = workSheet1.CreateRow(nRows);
            }

            iRow.CreateCell(0).SetCellValue("�ܼ�");
            workSheet1.AddMergedRegion(new CellRangeAddress(nRows, nRows, 0, 10));
            iRow.CreateCell(12).SetCellValue(t1);
            iRow.CreateCell(13).SetCellValue(t2);
            iRow.CreateCell(14).SetCellValue(t3);
            iRow.CreateCell(15).SetCellValue(t4);
            iRow.CreateCell(16).SetCellValue(t5);
            iRow.CreateCell(17).SetCellValue(t6);
            iRow.CreateCell(18).SetCellValue(t7);
            iRow.CreateCell(19).SetCellValue(t8);
            iRow.CreateCell(20).SetCellValue(t9);
            iRow.CreateCell(21).SetCellValue(t10);
            iRow.CreateCell(22).SetCellValue(t11);
            nRows += 2;
        }

        ISheet workSheet2 = workbook.CreateSheet("����������ձ���");
        

        fnProcessCore1(workSheet2, _dataset.Tables[2]);

        workbook.SetSheetOrder("����������ձ���", 0);
        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        //// ����һ��Application����ʹ��ɼ� 
        //Excel.Application app = new Excel.ApplicationClass();
        //app.Visible = false;

        //// ��ģ���ļ����õ�WorkBook���� 
        //Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        ////�����ϸ��Ϣ
        //DataSet _dataset = SelectDailyProcessBallast(uID, stddate, enddate);

        //#region ������Ԫ����
        //// �õ�WorkSheet���� 
        //Excel.Worksheet workSheet1 = (Excel.Worksheet)workBook.Sheets.get_Item(1);
        //workSheet1.Name = "������Ԫ�����ձ���";

        //int nCols = _dataset.Tables[0].Columns.Count;
        //int nRows = 3;

        //foreach (DataRow row in _dataset.Tables[0].Rows)
        //{
        //    int t1 = 0;
        //    int t2 = 0;
        //    int t3 = 0;
        //    int t4 = 0;
        //    int t5 = 0;
        //    int t6 = 0;
        //    int t7 = 0;
        //    int t8 = 0;
        //    int t9 = 0;
        //    int t10 = 0;
        //    int t11 = 0;

        //    workSheet1.Cells[nRows, 1] = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(row[1].ToString()));
        //    workSheet1.Cells[nRows, 2] = row[2].ToString();
        //    workSheet1.Cells[nRows, 3] = row[3].ToString();
        //    workSheet1.Cells[nRows, 4] = row[4].ToString();
        //    workSheet1.Cells[nRows, 5] = row[5].ToString();
        //    workSheet1.Cells[nRows, 6] = row[6].ToString();
        //    workSheet1.Cells[nRows, 7] = row[7].ToString();
        //    workSheet1.Cells[nRows, 8] = row[8].ToString();
        //    workSheet1.Cells[nRows, 9] = row[9].ToString();
        //    workSheet1.Cells[nRows, 10] = row[10].ToString();
        //    workSheet1.Cells[nRows, 11] = row[11].ToString();

        //    DataRow[] rows = _dataset.Tables[1].Select("prcID = " + row[0].ToString());

        //    if (rows.Length > 0)
        //    {
        //        workSheet1.get_Range("A" + nRows.ToString() + ":A" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("B" + nRows.ToString() + ":B" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("C" + nRows.ToString() + ":C" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("D" + nRows.ToString() + ":D" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("E" + nRows.ToString() + ":E" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("F" + nRows.ToString() + ":F" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("G" + nRows.ToString() + ":G" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("H" + nRows.ToString() + ":H" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("I" + nRows.ToString() + ":I" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("J" + nRows.ToString() + ":J" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //        workSheet1.get_Range("K" + nRows.ToString() + ":K" + Convert.ToString(nRows + rows.Length - 1), Type.Missing).Merge(0);
        //    }

        //    for (int i = 0; i < rows.Length; i++)
        //    {
        //        workSheet1.Cells[nRows, 12] = rows[i][1].ToString();
        //        workSheet1.Cells[nRows, 13] = rows[i][2].ToString();
        //        workSheet1.Cells[nRows, 14] = rows[i][3].ToString();
        //        workSheet1.Cells[nRows, 15] = rows[i][4].ToString();
        //        workSheet1.Cells[nRows, 16] = rows[i][5].ToString();
        //        workSheet1.Cells[nRows, 17] = rows[i][6].ToString();
        //        workSheet1.Cells[nRows, 18] = rows[i][7].ToString();
        //        workSheet1.Cells[nRows, 19] = rows[i][8].ToString();
        //        workSheet1.Cells[nRows, 20] = rows[i][9].ToString();
        //        workSheet1.Cells[nRows, 21] = rows[i][10].ToString();
        //        workSheet1.Cells[nRows, 22] = rows[i][11].ToString();
        //        workSheet1.Cells[nRows, 23] = rows[i][12].ToString();

        //        if (rows[i][2].ToString() != string.Empty)
        //        {
        //            t1 += Convert.ToInt32(rows[i][2].ToString());
        //        }

        //        if (rows[i][3].ToString() != string.Empty)
        //        {
        //            t2 += Convert.ToInt32(rows[i][3].ToString());
        //        }

        //        if (rows[i][4].ToString() != string.Empty)
        //        {
        //            t3 += Convert.ToInt32(rows[i][4].ToString());
        //        }

        //        if (rows[i][5].ToString() != string.Empty)
        //        {
        //            t4 += Convert.ToInt32(rows[i][5].ToString());
        //        }

        //        if (rows[i][6].ToString() != string.Empty)
        //        {
        //            t5 += Convert.ToInt32(rows[i][6].ToString());
        //        }

        //        if (rows[i][7].ToString() != string.Empty)
        //        {
        //            t6 += Convert.ToInt32(rows[i][7].ToString());
        //        }

        //        if (rows[i][8].ToString() != string.Empty)
        //        {
        //            t7 += Convert.ToInt32(rows[i][8].ToString());
        //        }

        //        if (rows[i][9].ToString() != string.Empty)
        //        {
        //            t8 += Convert.ToInt32(rows[i][9].ToString());
        //        }

        //        if (rows[i][10].ToString() != string.Empty)
        //        {
        //            t9 += Convert.ToInt32(rows[i][10].ToString());
        //        }

        //        if (rows[i][11].ToString() != string.Empty)
        //        {
        //            t10 += Convert.ToInt32(rows[i][11].ToString());
        //        }

        //        if (rows[i][12].ToString() != string.Empty)
        //        {
        //            t11 += Convert.ToInt32(rows[i][12].ToString());
        //        }

        //        nRows++;
        //    }

        //    workSheet1.Cells[nRows, 1] = "�ܼ�";
        //    workSheet1.get_Range("A" + nRows.ToString() + ":K" + nRows.ToString(), Type.Missing).Merge(0);

        //    workSheet1.Cells[nRows, 13] = t1.ToString();
        //    workSheet1.Cells[nRows, 14] = t2.ToString();
        //    workSheet1.Cells[nRows, 15] = t3.ToString();
        //    workSheet1.Cells[nRows, 16] = t4.ToString();
        //    workSheet1.Cells[nRows, 17] = t5.ToString();
        //    workSheet1.Cells[nRows, 18] = t6.ToString();
        //    workSheet1.Cells[nRows, 19] = t7.ToString();
        //    workSheet1.Cells[nRows, 20] = t8.ToString();
        //    workSheet1.Cells[nRows, 21] = t9.ToString();
        //    workSheet1.Cells[nRows, 22] = t10.ToString();
        //    workSheet1.Cells[nRows, 23] = t11.ToString();

        //    nRows += 2;
        //}
        //#endregion

        //#region ���������
        //// �õ�WorkSheet���� 
        //Excel.Worksheet workSheet2 = (Excel.Worksheet)workBook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //workSheet2.Name = "����������ձ���";

        //fnProcessCore1(workSheet2, _dataset.Tables[2]);

        //#endregion

        //// ���Excel�ļ����˳�
        //try
        //{
        //    //workBook.Protect(strRnd, true, false);
        //    workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        //    workBook.Close(null, null, null);
        //    app.Workbooks.Close();
        //    app.Application.Quit();
        //    app.Quit();

        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet1);
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet2);
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

        //    workSheet1 = null;
        //    workSheet2 = null;
        //    workBook = null;
        //    app = null;

        //    GC.Collect();
        //}
        //catch (Exception e)
        //{
        //    throw e;
        //}
        //finally
        //{
        //    KillProcess("EXCEL");
        //}

        }
    /// <summary>
    /// ֱ�ܹ����ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProcessStr(int uID, string stddate, string enddate)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Process_Str";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ֱ�ܼ����ձ���:дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProcessStr(int uID, string stddate, string enddate)
    {
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("ֱ��׼���ձ���");
        ISheet workSheet2 = workbook.CreateSheet("ֱ�ܱ�˿����");
        ISheet workSheet3 = workbook.CreateSheet("ֱ�������ձ���");
        ISheet workSheet4 = workbook.CreateSheet("ˮͿ�۹���");
        ISheet workSheet5 = workbook.CreateSheet("���Ź���");
        ISheet workSheet6 = workbook.CreateSheet("װͷ�������鹤��");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProcessStr(uID, stddate, enddate);

        fnProcessCore1(workSheet1, _dataset.Tables[0]);
        fnProcessCore2(workSheet2, _dataset.Tables[1]);

        fnProcessCore1(workSheet3, _dataset.Tables[2]);
        fnProcessCore2(workSheet4, _dataset.Tables[3]);
        fnProcessCore2(workSheet5, _dataset.Tables[4]);
        fnProcessCore2(workSheet6, _dataset.Tables[5]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workSheet3 = null;
                workSheet4 = null;
                workSheet5 = null;
                workSheet6 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
        
    }


    /// <summary>
    /// ���ϼ����걨��
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    public DataSet SelectSummaryIncomingYear(string type, string year)
    {
        try
        {
            string strName = "sp_QC_Report_Summary_Incoming_Year";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@type", type);
            param[1] = new SqlParameter("@year", year);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ���ϼ����걨��
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void SummaryReportIncomingYear(string title, string type, string year)
    {
        // ����һ��Application����ʹ��ɼ� 
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = false;

        // ��ģ���ļ����õ�WorkBook���� 
        Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //�����ϸ��Ϣ
        DataSet _dataset = SelectSummaryIncomingYear(type, year);

        // �õ�WorkSheet���� 
        Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        int nRows = 2;

        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            workSheet.Cells[nRows, 1] = row[0].ToString();
            workSheet.Cells[nRows, 2] = row[1].ToString();
            workSheet.Cells[nRows, 3] = row[2].ToString();
            workSheet.Cells[nRows, 4] = row[3].ToString();
            workSheet.Cells[nRows, 5] = row[4].ToString();
            workSheet.Cells[nRows, 6] = row[5].ToString();
            workSheet.Cells[nRows, 7] = row[6].ToString();

            nRows += 1;
        }

        //�����������ϼ�
        Excel.Range xlColRange = workSheet.get_Range("C1:C" + workSheet.Rows.Count.ToString() + ", F1:F" + workSheet.Rows.Count.ToString(), Missing.Value);
        Excel.Chart xlColChart = (Excel.Chart)workBook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        xlColChart.Name = title + "����������Chart";

        xlColChart.ChartWizard(xlColRange, Excel.XlChartType.xl3DColumn, Type.Missing, Excel.XlRowCol.xlColumns, Type.Missing, Type.Missing, true,
                                "���ϼ����걨��", Type.Missing, "�ջ�������", "ExtraTitle");
        Excel.Series xlColSeries = (Excel.Series)xlColChart.SeriesCollection(1);
        xlColSeries.XValues = workSheet.get_Range("B2", "B" + workSheet.Rows.Count.ToString());
        xlColSeries.HasDataLabels = true;
        xlColSeries.Name = "����";

        Excel.Series xlCols2 = (Excel.Series)xlColChart.SeriesCollection(2);
        xlCols2.XValues = workSheet.get_Range("B2", "B" + workSheet.Rows.Count.ToString());
        xlCols2.HasDataLabels = true;
        xlCols2.Name = "���ϸ���";

        //�ϸ���
        Excel.Range xlLineRange = workSheet.get_Range("E1:E" + workSheet.Rows.Count.ToString(), Type.Missing);
        Excel.Chart xlLineChart = (Excel.Chart)workBook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        xlLineChart.Name = title + "�ϸ���Chart";

        xlLineChart.ChartWizard(xlLineRange, Excel.XlChartType.xlLine, Type.Missing, Excel.XlRowCol.xlColumns, Type.Missing, Type.Missing, true,
                                  "���ϼ����걨��", Type.Missing, "�ϸ���", "ExtraTitle");

        //ָ��Y��̶�
        Excel.Axis valueAxis = (Excel.Axis)xlLineChart.Axes(XlAxisType.xlValue, XlAxisGroup.xlPrimary);
        valueAxis.MaximumScale = 1.05;
        valueAxis.MinimumScale = 0.6;
        valueAxis.CrossesAt = 0.6;
        valueAxis.MajorUnit = 0.05;
        valueAxis.HasMajorGridlines = true;

        Excel.Series xlLineSeries = (Excel.Series)xlLineChart.SeriesCollection(1);
        xlLineSeries.XValues = workSheet.get_Range("B2", "B" + workSheet.Rows.Count.ToString());

        xlLineSeries.HasDataLabels = true;
        xlLineSeries.Name = "�ϸ���";

        // ���Excel�ļ����˳� 
        try
        {
            //workBook.Protect(strRnd, true, false);
            workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            workSheet = null;
            workBook = null;
            app = null;

            GC.Collect();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            KillProcess("EXCEL");
        }

    }
    /// <summary>
    /// ���ϼ����ձ���ͼ
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="dtype"></param>
    /// <returns></returns>
    public DataSet SelectSummaryIncomingMonth(string type, string year, string month)
    {
        try
        {
            string strName = "sp_QC_Report_Summary_Incoming_Month";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@year", year);
            param[1] = new SqlParameter("@month", month);
            param[2] = new SqlParameter("@dtype", type);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// ���ϼ��鱨�����
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void SummaryReportIncomingMonth(string title, string type, string year, string month)
    {
        // ����һ��Application����ʹ��ɼ� 
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = false;

        // ��ģ���ļ����õ�WorkBook���� 
        Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //�����ϸ��Ϣ
        DataSet _dataset = SelectSummaryIncomingMonth(type, year, month);

        // �õ�WorkSheet���� 
        Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        int nRows = 2;

        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            workSheet.Cells[nRows, 1] = row[0].ToString();
            workSheet.Cells[nRows, 2] = row[1].ToString();
            workSheet.Cells[nRows, 3] = row[2].ToString();
            workSheet.Cells[nRows, 4] = row[3].ToString();
            workSheet.Cells[nRows, 5] = row[4].ToString();
            workSheet.Cells[nRows, 6] = row[5].ToString();

            nRows += 1;
        }

        //�����������ϼ�
        Excel.Range xlColRange = workSheet.get_Range("B1:B" + workSheet.Rows.Count.ToString() + ", E1:E" + workSheet.Rows.Count.ToString(), Missing.Value);
        Excel.Chart xlColChart = (Excel.Chart)workBook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        xlColChart.Name = title + "����������Chart";

        xlColChart.ChartWizard(xlColRange, Excel.XlChartType.xl3DColumn, Type.Missing, Excel.XlRowCol.xlColumns, Type.Missing, Type.Missing, true,
                                "���ϼ����±���", Type.Missing, "�ջ�������", "ExtraTitle");

        //ָ��X��
        Excel.Series xlColSeries = (Excel.Series)xlColChart.SeriesCollection(1);
        xlColSeries.XValues = workSheet.get_Range("A2", "A" + workSheet.Rows.Count.ToString());
        xlColSeries.HasDataLabels = true;
        xlColSeries.Name = "����";

        Excel.Series xlCols2 = (Excel.Series)xlColChart.SeriesCollection(2);
        xlCols2.XValues = workSheet.get_Range("A2", "A" + workSheet.Rows.Count.ToString());
        xlCols2.HasDataLabels = true;
        xlCols2.Name = "���ϸ���";

        //�ϸ���
        Excel.Range xlLineRange = workSheet.get_Range("D1:D" + workSheet.Rows.Count.ToString(), Type.Missing);
        Excel.Chart xlLineChart = (Excel.Chart)workBook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        xlLineChart.Name = title + "�ϸ���Chart";

        xlLineChart.ChartWizard(xlLineRange, Excel.XlChartType.xlLine, Type.Missing, Excel.XlRowCol.xlColumns, Type.Missing, Type.Missing, true,
                                  "���ϼ����±���", Type.Missing, "�ϸ���", "ExtraTitle");

        Excel.Series xlLineSeries = (Excel.Series)xlLineChart.SeriesCollection(1);
        xlLineSeries.XValues = workSheet.get_Range("A2", "A" + workSheet.Rows.Count.ToString());

        xlLineSeries.HasDataLabels = true;
        xlCols2.Name = "�ϸ���";

        // ���Excel�ļ����˳� 
        try
        {
            //workBook.Protect(strRnd, true, false);
            workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            workSheet = null;
            workBook = null;
            app = null;

            GC.Collect();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            KillProcess("EXCEL");
        }
    }


    public DataSet SelectPassRate()
    {
        try
        {
            string strName = "sp_QC_Rep_IncomingPassRate";

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// ���ؽ��ϼ���ĺϸ���
    /// </summary>
    public void PassRate()
    {
        // ����һ��Application����ʹ��ɼ� 
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = false;

        // ��ģ���ļ����õ�WorkBook���� 
        Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //�����ϸ��Ϣ
        DataSet _dataset = SelectPassRate();

        // �õ�WorkSheet���� 
        Excel.Worksheet workSheet1 = (Excel.Worksheet)workBook.Sheets.get_Item(1);
        Excel.Worksheet workSheet2 = (Excel.Worksheet)workBook.Sheets.get_Item(2);
        Excel.Worksheet workSheet3 = (Excel.Worksheet)workBook.Sheets.get_Item(3);

        int nRows = 2;

        //Shang Hai
        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            workSheet1.Cells[nRows, 1] = row[0].ToString();
            workSheet1.Cells[nRows, 2] = row[1].ToString();
            workSheet1.Cells[nRows, 3] = row[2].ToString();
            workSheet1.Cells[nRows, 4] = row[3].ToString();
            workSheet1.Cells[nRows, 5] = row[4].ToString();

            nRows += 1;
        }

        //Zhen Jiang 
        nRows = 2;

        foreach (DataRow row in _dataset.Tables[1].Rows)
        {
            workSheet2.Cells[nRows, 1] = row[0].ToString();
            workSheet2.Cells[nRows, 2] = row[1].ToString();
            workSheet2.Cells[nRows, 3] = row[2].ToString();
            workSheet2.Cells[nRows, 4] = row[3].ToString();
            workSheet2.Cells[nRows, 5] = row[4].ToString();

            nRows += 1;
        }

        //Yang Zhou 
        nRows = 2;

        foreach (DataRow row in _dataset.Tables[2].Rows)
        {
            workSheet3.Cells[nRows, 1] = row[0].ToString();
            workSheet3.Cells[nRows, 2] = row[1].ToString();
            workSheet3.Cells[nRows, 3] = row[2].ToString();
            workSheet3.Cells[nRows, 4] = row[3].ToString();
            workSheet3.Cells[nRows, 5] = row[4].ToString();

            nRows += 1;
        }

        // ���Excel�ļ����˳� 
        try
        {
            //workBook.Protect(strRnd, true, false);
            workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet2);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet3);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            workSheet1 = null;
            workSheet2 = null;
            workSheet3 = null;
            workBook = null;
            app = null;

            GC.Collect();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            KillProcess("EXCEL");
        }
    }

    /// <summary>
    /// ���ϼ�����ɫ����
    /// </summary>
    public void IncongmingFluxExport(String Line, String Recv)
    {
        QC oqc = new QC();

        // ����һ��Application����ʹ��ɼ� 
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = false;

        // ��ģ���ļ����õ�WorkBook���� 
        Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //�����ϸ��Ϣ
        System.Data.DataTable table = oqc.GetLumFlax(Line, Recv);

        // �õ�WorkSheet���� 
        Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        int nRows = 2;

        for (int row = 0; row < table.Rows.Count; row++)
        {
            workSheet.Cells[nRows, 1] = table.Rows[row]["createdate"].ToString();
            workSheet.Cells[nRows, 2] = table.Rows[row]["ProductType"].ToString();
            workSheet.Cells[nRows, 3] = table.Rows[row]["Testtype"].ToString();
            workSheet.Cells[nRows, 4] = table.Rows[row]["Err"].ToString();
            workSheet.Cells[nRows, 5] = table.Rows[row]["I1"].ToString();
            workSheet.Cells[nRows, 6] = table.Rows[row]["P1"].ToString();
            workSheet.Cells[nRows, 7] = table.Rows[row]["PF1"].ToString();
            workSheet.Cells[nRows, 8] = table.Rows[row]["Flux"].ToString();
            workSheet.Cells[nRows, 9] = table.Rows[row]["Efficiency"].ToString();
            workSheet.Cells[nRows, 10] = table.Rows[row]["Ra"].ToString();
            workSheet.Cells[nRows, 11] = table.Rows[row]["TC"].ToString();
            workSheet.Cells[nRows, 12] = table.Rows[row]["x/y"].ToString();
            workSheet.Cells[nRows, 13] = table.Rows[row]["Temperature"].ToString();

            nRows += 1;
        }

        // ���Excel�ļ����˳� 
        try
        {
            //workBook.Protect(strRnd, true, false);
            workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            workSheet = null;
            workBook = null;
            app = null;

            GC.Collect();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            KillProcess("EXCEL");
        }
    }

    /// <summary>
    /// ��Ʒ������ɫ����
    /// </summary>
    public void ProductFluxExport(String nbr, String lot, Boolean tcp)
    {
        QC oqc = new QC();

        // ����һ��Application����ʹ��ɼ� 
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = false;

        // ��ģ���ļ����õ�WorkBook���� 
        Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //�����ϸ��Ϣ
        System.Data.DataTable table = oqc.GetProductLumFlax(nbr, lot, tcp);

        // �õ�WorkSheet���� 
        Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        int nRows = 2;

        for (int row = 0; row < table.Rows.Count; row++)
        {
            workSheet.Cells[nRows, 1] = table.Rows[row]["wo_nbr"].ToString();
            workSheet.Cells[nRows, 2] = table.Rows[row]["wo_lot"].ToString();
            workSheet.Cells[nRows, 3] = table.Rows[row]["Line"].ToString();
            workSheet.Cells[nRows, 4] = table.Rows[row]["ProductType"].ToString();
            workSheet.Cells[nRows, 5] = table.Rows[row]["TestDate"].ToString();
            workSheet.Cells[nRows, 6] = table.Rows[row]["Testtype"].ToString();
            workSheet.Cells[nRows, 7] = table.Rows[row]["Err"].ToString();
            workSheet.Cells[nRows, 8] = table.Rows[row]["I1"].ToString();
            workSheet.Cells[nRows, 9] = table.Rows[row]["P1"].ToString();
            workSheet.Cells[nRows, 10] = table.Rows[row]["PF1"].ToString();
            workSheet.Cells[nRows, 11] = table.Rows[row]["Flux"].ToString();
            workSheet.Cells[nRows, 12] = table.Rows[row]["Efficiency"].ToString();
            workSheet.Cells[nRows, 13] = table.Rows[row]["Ra"].ToString();
            workSheet.Cells[nRows, 14] = table.Rows[row]["TC"].ToString();
            workSheet.Cells[nRows, 15] = table.Rows[row]["x/y"].ToString();
            workSheet.Cells[nRows, 16] = table.Rows[row]["Temperature"].ToString();

            nRows += 1;
        }

        // ���Excel�ļ����˳� 
        try
        {
            //workBook.Protect(strRnd, true, false);
            workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            workSheet = null;
            workBook = null;
            app = null;

            GC.Collect();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            KillProcess("EXCEL");
        }
    }

    /// <summary>
    /// ��Ʒ���ɫ����
    /// </summary>
    public void SampleFluxExport(Int32 ID)
    {
        QC oqc = new QC();

        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);
        IWorkbook workbook = new HSSFWorkbook(templetFile);
        System.Data.DataTable dt = oqc.SelectFluxDetail(ID);


        if (dt != null)
        {
            if (dt.Rows.Count >= 1)
            {
                ISheet workSheet = workbook.GetSheetAt(0);
                int nRows = 1;
                foreach (DataRow row in dt.Rows)
                {
                    IRow iRow = workSheet.CreateRow(nRows);

                    iRow.CreateCell(0).SetCellValue(row["fl_wo"]);
                    iRow.CreateCell(1).SetCellValue(row["ProductType"]);
                    iRow.CreateCell(2).SetCellValue(row["createdate"]);
                    iRow.CreateCell(3).SetCellValue(row["Testtype"]);
                    iRow.CreateCell(4).SetCellValue(row["Err"]);
                    iRow.CreateCell(5).SetCellValue(row["I1"]);
                    iRow.CreateCell(6).SetCellValue(row["P1"]);
                    iRow.CreateCell(7).SetCellValue(row["PF1"]);
                    iRow.CreateCell(8).SetCellValue(row["Flux"]);
                    iRow.CreateCell(9).SetCellValue(row["Efficiency"]);
                    iRow.CreateCell(10).SetCellValue(row["Ra"]);
                    iRow.CreateCell(11).SetCellValue(row["R9"]);
                    iRow.CreateCell(12).SetCellValue(row["TC"]);
                    iRow.CreateCell(13).SetCellValue(row["x/y"]);
                    iRow.CreateCell(14).SetCellValue(row["Temperature"]);
                    iRow.CreateCell(15).SetCellValue(row["DUV"]);
                    nRows++;
                }
            }
           
            
        }

        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

        //// ����һ��Application����ʹ��ɼ� 
        //Excel.Application app = new Excel.ApplicationClass();
        //app.Visible = false;

        //// ��ģ���ļ����õ�WorkBook���� 
        //Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        ////�����ϸ��Ϣ
        // System.Data.DataTable table = oqc.SelectFluxDetail(ID);

        //// �õ�WorkSheet���� 
        //Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        //int nRows = 2;

        //for (int row = 0; row < table.Rows.Count; row++)
        //{
        //    workSheet.Cells[nRows, 1] = table.Rows[row]["createdate"].ToString();
        //    workSheet.Cells[nRows, 2] = table.Rows[row]["ProductType"].ToString();
        //    workSheet.Cells[nRows, 3] = table.Rows[row]["Testtype"].ToString();
        //    workSheet.Cells[nRows, 4] = table.Rows[row]["Err"].ToString();
        //    workSheet.Cells[nRows, 5] = table.Rows[row]["I1"].ToString();
        //    workSheet.Cells[nRows, 6] = table.Rows[row]["P1"].ToString();
        //    workSheet.Cells[nRows, 7] = table.Rows[row]["PF1"].ToString();
        //    workSheet.Cells[nRows, 8] = table.Rows[row]["Flux"].ToString();
        //    workSheet.Cells[nRows, 9] = table.Rows[row]["Efficiency"].ToString();
        //    workSheet.Cells[nRows, 10] = table.Rows[row]["Ra"].ToString();
        //    workSheet.Cells[nRows, 11] = table.Rows[row]["TC"].ToString();
        //    workSheet.Cells[nRows, 12] = table.Rows[row]["x/y"].ToString();
        //    workSheet.Cells[nRows, 13] = table.Rows[row]["Temperature"].ToString();

        //    nRows += 1;
        //}

        //// ���Excel�ļ����˳� 
        //try
        //{
        //    //workBook.Protect(strRnd, true, false);
        //    workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        //    workBook.Close(null, null, null);
        //    app.Workbooks.Close();
        //    app.Application.Quit();
        //    app.Quit();

        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

        //    workSheet = null;
        //    workBook = null;
        //    app = null;

        //    GC.Collect();
        //}
        //catch (Exception e)
        //{
        //    throw e;
        //}
        //finally
        //{
        //    KillProcess("EXCEL");
        //}
    }

    /// <summary>
    /// ��Ʒ���ɫ����
    /// </summary>
    public void RoutingExport(DataSet ds, String Header)
    {
        // ����һ��Application����ʹ��ɼ� 
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = false;

        // ��ģ���ļ����õ�WorkBook���� 
        Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //�����ϸ��Ϣ
        System.Data.DataTable table = ds.Tables[0];

        // �õ�WorkSheet���� 
        Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        int nRows = 2;

        int nSheets = table.Rows.Count / 40000 + 1;

        for (int i = 0; i < nSheets; i++)
        {
            nRows = 2;
            workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(i + 1);

            //д��ͷ
            String[] strHeader = Header.Split(';');

            for (int j = 0; j < strHeader.Length; j++)
            {
                workSheet.Cells[1, j + 2] = strHeader[j];
            }

            int nStart = i * 40000;
            int nEnd = table.Rows.Count < 40000 * (i + 1) ? table.Rows.Count : 40000 * (i + 1);

            for (int row = nStart; row < nEnd; row++)
            {
                workSheet.Cells[nRows, 1] = table.Rows[row]["wo2_ro_routing"].ToString();
                workSheet.Cells[nRows, 2] = table.Rows[row]["C1"].ToString();
                workSheet.Cells[nRows, 3] = table.Rows[row]["C2"].ToString();
                workSheet.Cells[nRows, 4] = table.Rows[row]["C3"].ToString();
                workSheet.Cells[nRows, 5] = table.Rows[row]["C4"].ToString();
                workSheet.Cells[nRows, 6] = table.Rows[row]["C5"].ToString();
                workSheet.Cells[nRows, 7] = table.Rows[row]["C6"].ToString();
                workSheet.Cells[nRows, 8] = table.Rows[row]["C7"].ToString();
                workSheet.Cells[nRows, 9] = table.Rows[row]["C8"].ToString();
                workSheet.Cells[nRows, 10] = table.Rows[row]["Total"].ToString();
                workSheet.Cells[nRows, 11] = table.Rows[row]["QAD"].ToString();
                workSheet.Cells[nRows, 12] = table.Rows[row]["Diff"].ToString();

                nRows += 1;
            }  
        }

        // ���Excel�ļ����˳� 
        try
        {
            //workBook.Protect(strRnd, true, false);
            workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            workSheet = null;
            workBook = null;
            app = null;

            GC.Collect();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            KillProcess("EXCEL");
        }
    }
    /// <summary>
    /// ���ɫ����
    /// </summary>
    /// <param name="date1"></param>
    /// <param name="date2"></param>
    /// <param name="nbr1"></param>
    /// <param name="nbr2"></param>
    /// <param name="lot1"></param>
    /// <param name="lot2"></param>
    /// <param name="part1"></param>
    /// <param name="part2"></param>
    public void ProductFluxExport(string date1, string date2, string nbr1, string nbr2, string lot1, string lot2, string part1, string part2)
    {
        QC oqc = new QC();

        // ����һ��Application����ʹ��ɼ� 
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = false;

        // ��ģ���ļ����õ�WorkBook���� 
        Excel.Workbook workBook = app.Workbooks.Open(_templetFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //�����ϸ��Ϣ
        System.Data.DataSet ds = oqc.GetLumFlaxProduct(date1, date2, nbr1, nbr2, lot1, lot2, part1, part2);

        // �õ�WorkSheet���� 
        Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        int nRows = 2;

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            workSheet.Cells[nRows, 1] = row["wo_nbr"].ToString();
            workSheet.Cells[nRows, 2] = row["wo_lot"].ToString();
            workSheet.Cells[nRows, 3] = row["Line"].ToString();
            workSheet.Cells[nRows, 4] = row["ProductType"].ToString();
            workSheet.Cells[nRows, 5] = row["TestDate"].ToString();
            workSheet.Cells[nRows, 6] = row["Testtype"].ToString();
            workSheet.Cells[nRows, 7] = row["Err"].ToString();
            workSheet.Cells[nRows, 8] = row["I1"].ToString();
            workSheet.Cells[nRows, 9] = row["P1"].ToString();
            workSheet.Cells[nRows, 10] = row["PF1"].ToString();
            workSheet.Cells[nRows, 11] = row["Flux"].ToString();
            workSheet.Cells[nRows, 12] = row["Efficiency"].ToString();
            workSheet.Cells[nRows, 13] = row["Ra"].ToString();
            workSheet.Cells[nRows, 14] = row["TC"].ToString();
            workSheet.Cells[nRows, 15] = row["x/y"].ToString();
            workSheet.Cells[nRows, 16] = row["Temperature"].ToString();

            nRows++;
        }


        // ���Excel�ļ����˳� 
        try
        {
            //workBook.Protect(strRnd, true, false);
            workBook.SaveAs(_outputFile, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            workBook.Close(null, null, null);
            app.Workbooks.Close();
            app.Application.Quit();
            app.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

            workSheet = null;
            workBook = null;
            app = null;

            GC.Collect();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            KillProcess("EXCEL");
        }
    }


    /// <summary>
    /// �⳧CFL���Ƴ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductOut(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Out";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// �⳧LED���Ƴ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductOutLED(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Out_LED";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// �⳧CFL���Ƴ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductOut(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "�⳧CFL���Ƴ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductOut(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// �⳧LED���Ƴ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductOutLED(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "�⳧LED���Ƴ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductOutLED(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// �⳧ë�ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductCapOut(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Cap_Out";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// �⳧ë�ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductCapOut(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "�⳧ë�ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductCapOut(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// �⳧��������Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductBallastOut(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Ballast_Out";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// �⳧��������Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductBallastOut(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "�⳧��������Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductBallastOut(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// �⳧���ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductMingGOut(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_MingG_Out";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// �⳧���ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductMingGOut(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "�⳧���ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductMingGOut(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// �⳧ֱ�ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductStrOut(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Str_Out";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// �⳧ֱ�ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductStrOut(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "�⳧���ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductStrOut(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// �⳧���Ʒ��Ƭ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductSFPOut(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_SFP_Out";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// �⳧���Ʒ��Ƭ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductSFPOut(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "�⳧���Ʒ��Ƭ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductSFPOut(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }


    /// <summary>
    /// ���䷵��CFL���Ƴ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductRework(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "CFL���Ƴ�Ʒ�����ձ���");
        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductRework(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }


    /// <summary>
    /// ���䷵�����Ƴ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductRework(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Rework";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }



    /// <summary>
    /// ���䷵��LED���Ƴ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportLEDRework(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "LED���Ƴ�Ʒ�����ձ���");
        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyLEDRework(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// ���Ƴ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyLEDRework(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_LED_Rework";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// ���䷵��ë�ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductCapRework(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "ë�ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductCapRework(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// ���䷵��ë�ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductCapRework(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Cap_Rework";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }


    /// <summary>
    /// ���䷵����������Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductBallastRework(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "��������Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductBallastRework(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ���䷵����������Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductBallastRework(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Ballast_Rework";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// ���䷵�����ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductMingGRework(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "���ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductMingGRework(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// ���䷵�����ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductMingGRework(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_MingG_Rework";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }


    /// <summary>
    /// ���䷵��ֱ�ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductStrRework(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "���ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductStrRework(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// ���䷵��ֱ�ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductStrRework(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Str_Rework";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }


    /// <summary>
    /// QC�ڶ��μ���CFL���Ƴ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "CFL���Ƴ�Ʒ�����ձ���");
        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductSecond(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }


    /// <summary>
    /// QC�ڶ��μ������Ƴ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Second";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }




    /// <summary>
    /// C�ڶ��μ���LED���Ƴ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportLEDSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "LED���Ƴ�Ʒ�����ձ���");
        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyLEDSecond(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// QC�ڶ��μ������Ƴ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyLEDSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_LED_Second";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// QC�ڶ��μ���ë�ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductCapSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "ë�ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductCapSecond(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// QC�ڶ��μ���ë�ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductCapSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Cap_Second";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// QC�ڶ�����������Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductBallastSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "��������Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductBallastSecond(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    /// QC�ڶ�����������Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductBallastSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Ballast_Second";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    ///  QC�ڶ������ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductMingGSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "���ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductMingGSecond(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    /// <summary>
    ///  QC�ڶ������ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductMingGSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_MingG_Second";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// QC�ڶ���ֱ�ܳ�Ʒ�ձ���дExcel
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    public void DailyReportProductStrSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "���ܳ�Ʒ�����ձ���");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyProductStrSecond(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    /// <summary>
    /// QC�ڶ���ֱ�ܳ�Ʒ�ձ�������
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    private DataSet SelectDailyProductStrSecond(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_Str_Second";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// ���ϼ����ձ���:����
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    public DataSet SelectLineSamplingIncoming(int uID, string stddate, string enddate, string typeName, string plantcode)
    {
        try
        {
            string strName = "sp_QC_Report_LineSampling_Incoming";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@typeName", typeName);
            param[4] = new SqlParameter("@plantcode", plantcode);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// ���ϼ����ձ���:����
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    public DataSet SelectLampSamplingIncoming(int uID, string stddate, string enddate, string typeName,bool tcp, string plantcode)
    {           
        try
        {                                                    
            string strName = "sp_QC_Report_LampSampling_Incoming";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@typeName", typeName);
            param[4] = new SqlParameter("@tcp", tcp);
            param[5] = new SqlParameter("@plantcode", plantcode);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// ���ϼ����ձ���:����
    /// </summary>
    /// <param name="uID"></param>
    /// <param name="stddate"></param>
    /// <param name="enddate"></param>
    /// <returns></returns>
    public DataSet SelectCFLLampSamplingIncoming(int uID, string stddate, string enddate, string typeName, bool tcp, string plantcode)
    {
        try
        {
            string strName = "sp_QC_Report_CFLLampSampling_Incoming";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@typeName", typeName);
            param[4] = new SqlParameter("@tcp", tcp);
            param[5] = new SqlParameter("@plantid", plantcode);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

        public void DailyReportT8(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        ISheet workSheet = workbook.GetSheetAt(0);
        workbook.SetSheetName(0, "LED T8ֱ�ܵƼ����ձ���");
        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyT8(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet, _dataset.Tables[0]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

  private DataSet SelectDailyT8(int uID, string stddate, string enddate, int nPlantID)
    {
        try
        {
            string strName = "sp_QC_Report_Daily_Product_T8";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch(Exception ex)
        {
            return null;
        }
    }



      public void DailyReportTcpT8(int uID, string stddate, string enddate, int nPlantID)
    {
        //��ȡģ��·��
        FileStream templetFile = new FileStream(this._templetFile, FileMode.Open, FileAccess.Read);

        IWorkbook workbook = new HSSFWorkbook(templetFile);
        workbook.RemoveSheetAt(0);
        ISheet workSheet1 = workbook.CreateSheet("LED T8ֱ�ܵƼ����ձ���(ȫ��)");
        ISheet workSheet2 = workbook.CreateSheet("LED T8ֱ�ܵƼ����ձ���(ɸѡ)");

        //�����ϸ��Ϣ
        DataSet _dataset = SelectDailyTcpT8(uID, stddate, enddate, nPlantID);

        fnProductCore(workSheet1, _dataset.Tables[0]);
        fnProductCore(workSheet2, _dataset.Tables[1]);

        // ���Excel�ļ����˳� 
        try
        {
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);

                Stream localFile = new FileStream(this._outputFile, FileMode.OpenOrCreate);

                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
                localFile.Dispose();

                ms.Flush();
                ms.Position = 0;

                workSheet1 = null;
                workSheet2 = null;
                workbook = null;
            }
        }
        catch (Exception e)
        {
            throw e;
        }
    }

          private DataSet SelectDailyTcpT8(int uID, string stddate, string enddate, int nPlantID)
      {
        try
        {
            string strName = "sp_QC_Report_Daily_Tcp_T8";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@plantid", nPlantID);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }



  public DataSet SelectDSD(int uID, string stddate, string enddate, string typeName,bool tcp, string plantcode)
    {
        try
        {
            string strName = "sp_QC_Report_DSD";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@typeName", typeName);
            param[4] = new SqlParameter("@tcp", tcp);
            param[5] = new SqlParameter("@plantcode", plantcode);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch (Exception ex)
        {
            return null;
        }
    }


     public DataSet SelectT8(int uID, string stddate, string enddate, string typeName,bool tcp, string plantcode)
    {
        try
        {
            string strName = "sp_QC_Report_T8";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@uID", uID);
            param[1] = new SqlParameter("@stddate", stddate);
            param[2] = new SqlParameter("@enddate", enddate);
            param[3] = new SqlParameter("@typeName", typeName);
            param[4] = new SqlParameter("@tcp", tcp);
            param[5] = new SqlParameter("@plantcode", plantcode);

            return SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strName, param);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
  
}
