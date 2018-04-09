using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Configuration;
/// <summary>
/// Summary description for PCF_helper
/// </summary>
public class PCF_helper
{
    adamClass adam = new adamClass();

    private string getGUID()
    {
        System.Guid guid = new Guid();
        guid = Guid.NewGuid();
        string str = guid.ToString();
        return str;
    }

	public PCF_helper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable selectMadeInquiryList(string QAD, string vender, string venderName)
    {
        string sqlstr = "sp_PCF_selectMadeInquiryList";

        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@QAD",QAD)
            , new SqlParameter("@vender",vender)
            , new SqlParameter("@venderName",venderName)

        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }

    public DataTable selectMadeInquiryDet(string vender, string venderName, string qty)
    {
        string sqlstr = "sp_PCF_selectMadeInquiryDet";

        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@qty",qty)
            , new SqlParameter("@vender",vender)
            , new SqlParameter("@venderName",venderName)

        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public int deletePCFDetByPCFID(string PCF_ID, string uID, string uName)
    {
        string sqlstr = "sp_PCF_deleteMadeInquiryDet";

        SqlParameter[] param = new SqlParameter[]
        {
            new SqlParameter("@uID",uID)
            , new SqlParameter("@uName",uName)
            , new SqlParameter("@PCF_ID",PCF_ID)

        };

        return Convert.ToInt32( SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public bool insertTOInquiry(DataTable TempTable, string uID, string uName, string vender, string venderName, out string PCF_inquiryID)
    {
        StringWriter writer = new StringWriter();
        TempTable.WriteXml(writer);
        string xmlDetail = writer.ToString();


        string sqlstr = "sp_pcf_insertTOInquiry";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@xmlDetail",xmlDetail)
        , new SqlParameter("@uID",uID)
        , new SqlParameter("@uName",uName)
        , new SqlParameter("@PCF_inquiryID",SqlDbType.UniqueIdentifier)
        , new SqlParameter("@vender",vender)
        , new SqlParameter("@venderName",venderName)
        };

        param[3].Direction = ParameterDirection.Output;

        bool flag =  Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));

        PCF_inquiryID = param[3].Value.ToString();

        return flag;

    }

    public DataTable selectInqyuryList(string vender, string venderName, string QAD, string IMID,string  status)
    {
        string sqlstr = "sp_pcf_selectInqyuryList";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@vender",vender)
            , new SqlParameter("@venderName",venderName)
            , new SqlParameter("@QAD",QAD)
            , new SqlParameter("@IMID",IMID)
            , new SqlParameter("@status",status)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public SqlDataReader selectInqyuryHead(string PCF_inquiryID)
    {
        string sqlstr = "sp_pcf_selectInqyuryHead";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCF_inquiryID",PCF_inquiryID)
           
        };

        return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param);
    }

    public DataTable selectInquiryDet(string PCF_inquiryID)
    {
        string sqlstr = "sp_pcf_selectInquiryDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCF_inquiryID",PCF_inquiryID)
           
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool CancelinquiryDetByID(string PCF_ID, string uID, string uName)
    {
        string sqlstr = "sp_pcf_CancelinquiryDetByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCF_ID",PCF_ID)
            , new SqlParameter("@uID",uID)
            , new SqlParameter("@uName",uName)
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));

    }

    public DataTable selectInquiryBasisByID(string PCF_inquiryID)
    {
        string sqlstr = "sp_pcf_selectInquiryBasisByID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCF_inquiryID",PCF_inquiryID)
           
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public bool deleteInquiryBasis(string PCF_InquiryImportID)
    {
        string sqlstr = "sp_pcf_deleteInquiryBasis";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCF_InquiryImportID",PCF_InquiryImportID)
           
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public bool insertBasis(string PCF_inquiryID, string uID, string uName, string fileName, string filePate)
    {
        string sqlstr = "sp_pcf_insertBasis";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCF_inquiryID",PCF_inquiryID)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@uName",uName)
            ,new SqlParameter("@fileName",fileName)
            ,new SqlParameter("@filePate",filePate)
           
        };

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public void createinquiry(string stroutFile, string IMID, string company, string vendor, string createdDate, string createByName, int uID, string curr)
    {
        System.Data.DataTable dt = generationInquiry(IMID, out company, out createdDate, out createByName, uID, curr);
        string excelname = System.DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "");
        string strFile = stroutFile;
        string filePath = System.Web.HttpContext.Current.Server.MapPath("../Excel/" + strFile);
        MemoryStream ms = RenderDataTableToExcel(dt, filePath, uID, IMID, company, vendor, createdDate, createByName) as MemoryStream;
        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        byte[] data = ms.ToArray();
        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();
        data = null;
        ms = null;
        fs = null;
    }

    private DataTable generationInquiry(string IMID, out string company, out string createdDate, out string createByName, int uID, string curr)
    {
        string sqlstr = "sp_pcf_generationInquiry";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@IMID",IMID)
        ,new SqlParameter("@company",SqlDbType.NVarChar,100)
        ,new SqlParameter("@createdDate" ,SqlDbType.VarChar,50)
        ,new SqlParameter("@createdByName" ,SqlDbType.NVarChar,24)
        , new SqlParameter("@uID",uID)
        ,new SqlParameter("@curr",curr)
        };

        param[1].Direction = ParameterDirection.Output;
        param[2].Direction = ParameterDirection.Output;
        param[3].Direction = ParameterDirection.Output;
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        company = param[1].Value.ToString();
        createdDate = param[2].Value.ToString();
        createByName = param[3].Value.ToString();
        return dt;
    }
    public Stream RenderDataTableToExcel(System.Data.DataTable SourceTable, string stroutFile, int uid, string IMID, string company, string vendor, string createdDate, string createByName)
    {
        MemoryStream ms = new MemoryStream();
        FileStream file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/pcf_excelinquiry.xls"), FileMode.Open, FileAccess.Read);
        NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");//workbook.CreateSheet();
        NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(17);//sheet.GetRow(0);
        file.Close();


        //输出头部信息
        #region 输出头部信息
        sheet.GetRow(2).GetCell(1).SetCellValue("询价单号：" + IMID);
        sheet.GetRow(3).GetCell(1).SetCellValue("公司：");
        sheet.GetRow(3).GetCell(2).SetCellValue(company);
        sheet.GetRow(3).GetCell(5).SetCellValue("询价日期：");
        sheet.GetRow(3).GetCell(7).SetCellValue(createdDate);
        sheet.GetRow(5).GetCell(2).SetCellValue(vendor);
        sheet.GetRow(7).GetCell(1).SetCellValue("联系人：");
        sheet.GetRow(7).GetCell(5).SetCellValue("联系电话：");
        sheet.GetRow(9).GetCell(1).SetCellValue("备注：");

        //输出明细


        //设置7列宽为100
        sheet.SetColumnWidth(9, 250);
        //sheet.SetColumnWidth(12, 100);
        //加标题
        sheet.GetRow(11).GetCell(1).SetCellValue("QAD");
        sheet.GetRow(11).GetCell(2).SetCellValue("规格");
        sheet.GetRow(11).GetCell(3).SetCellValue("单位");
        sheet.GetRow(11).GetCell(4).SetCellValue("描述");
        sheet.GetRow(11).GetCell(5).SetCellValue("描述1");
        sheet.GetRow(11).GetCell(6).SetCellValue("描述2");
        sheet.GetRow(11).GetCell(7).SetCellValue("价格");
        sheet.GetRow(11).GetCell(8).SetCellValue("币种");

        //明细起始行
        int rowIndex = 12;

        //ICellStyle style1 = hssfworkbook.CreateCellStyle();
        //style1.WrapText = true;
        ICellStyle style2 = hssfworkbook.CreateCellStyle();
        style2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        style2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thick;



        foreach (DataRow row in SourceTable.Rows)
        {


            //            Sub changeRows()
            //'
            //' changeRows 宏
            //'

            //'
            //    Range("E12:H100").Select
            //    With Selection
            //        .HorizontalAlignment = xlGeneral
            //        .VerticalAlignment = xlBottom
            //        .WrapText = True
            //        .Orientation = 0
            //        .AddIndent = False
            //        .IndentLevel = 0
            //        .ShrinkToFit = False
            //        .ReadingOrder = xlContext
            //        .MergeCells = False
            //    End With
            //End Sub




            NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);
            ICellStyle style = hssfworkbook.CreateCellStyle();
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;


            ICell cell = dataRow.CreateCell(1);
            cell.SetCellValue(row["PCF_part"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(2);
            cell.SetCellValue(row["PCF_format"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(3);
            cell.SetCellValue(row["PCF_um"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(4);
            cell.SetCellValue(row["PCF_desc"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(5);
            cell.SetCellValue(row["PCF_desc1"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(6);
            cell.SetCellValue(row["PCF_desc2"].ToString());
            cell.CellStyle = style;


            cell = dataRow.CreateCell(7);
            cell.CellStyle = style;
            cell = dataRow.CreateCell(8);
            cell.SetCellValue(row["Curr"].ToString());
            cell.CellStyle = style;


            rowIndex++;



        }

        for (int i = sheet.LastRowNum; i > SourceTable.Rows.Count + 12; i--)
        {
            sheet.ShiftRows(i, i + 1, -1);

        }

        #endregion

        hssfworkbook.Write(ms);
        //workbook.Write(ms);
        ms.Flush();
        ms.Position = 0;
        sheet = null;
        headerRow = null;
        //workbook = null;
        hssfworkbook = null;
        return ms;
    }

    public DataTable selectInquiryDetTemp(string PCF_inquiryID)
    {
        string sqlstr = "sp_pcf_selectInquiryDetTemp";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCF_inquiryID",PCF_inquiryID)
           
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public int updateInquiryDet(DataTable dt,string PCF_inquiryID, string uID, string uName)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();


        string sqlstr = "sp_pcf_updateInquiryDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@xmlDetail",xmlDetail)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@uName",uName)
          ,new SqlParameter("@PCF_inquiryID",PCF_inquiryID)
           
        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    public DataTable updateInquiryDetByExcel(DataTable dt, string PCF_inquiryID, string uID, string uName)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_pcf_updateInquiryDetByExcel";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@xmlDetail",xmlDetail)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@uName",uName)
          ,new SqlParameter("@PCF_inquiryID",PCF_inquiryID)
           
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable selectPassInquiryDet(string part, string vender, string venderName)
    {
        string sqlstr = "sp_pcf_selectPassInquiryDet";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@part",part)
            ,new SqlParameter("@vender",vender)
            ,new SqlParameter("@venderName",venderName)
          
           
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    public DataTable commitPassInquiryToCheck(DataTable dt, string uID, string uName , out string mailTo)
    {
        StringWriter writer = new StringWriter();
        dt.WriteXml(writer);
        string xmlDetail = writer.ToString();

        string sqlstr = "sp_pcf_commitPassInquiryToCheck";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@xmlDetail",xmlDetail)
            ,new SqlParameter("@uID",uID)
            ,new SqlParameter("@uName",uName)
          ,new SqlParameter("@mailTo" ,SqlDbType.NVarChar,500)
           
        };

        param[3].Direction = ParameterDirection.Output;
        DataTable retu = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

        mailTo = param[3].Value.ToString();
        return retu;
    }

    public string selectApplyMstrID(string PCF_sourceID)
    {
        string sqlstr = "sp_pcf_selectApplyMstrID";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@PCF_sourceID",PCF_sourceID)
            
        };

        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }
}