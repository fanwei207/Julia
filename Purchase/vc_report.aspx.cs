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
using QCProgress;
using System.Data.SqlClient;
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
using System.IO;


public partial class vc_report : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCompanyGroup();
            GridViewBind();
        }
    }

    private void BindCompanyGroup()
    {
        //工厂
        ddl_Domain.Items.Insert(0, new ListItem("---", "0"));
        ddl_Domain.Items.Insert(1, new ListItem("SZX", "1"));
        ddl_Domain.Items.Insert(2, new ListItem("ZQL", "2"));
        ddl_Domain.Items.Insert(3, new ListItem("YQL", "5"));
        ddl_Domain.Items.Insert(4, new ListItem("HQL", "8"));
        ddl_Domain.SelectedIndex = 0;
    }

    private void GridViewBind() 
    {
        DateTime DateStart;
        DateTime DateEnd;
        if (string.IsNullOrEmpty(txt_datestart.Text.Trim()))
        {
            DateStart = Convert.ToDateTime(System.DateTime.Now.AddMonths(-1));
        }
        else
        {
            try
            {

                DateStart = Convert.ToDateTime(txt_datestart.Text);
            }
            catch
            {
                this.Alert("起始日输入的不是日期格式！");
                return;
            }
        }

        if (string.IsNullOrEmpty(txt_dateend.Text.Trim()))
        {
            DateEnd = Convert.ToDateTime(System.DateTime.Now);
        }
        else
        {
            try
            {

                DateEnd = Convert.ToDateTime(txt_dateend.Text.Trim());
            }
            catch
            {
                this.Alert("起始日输入的不是日期格式！");
                return;
            }
        }
        try
        {
            string strName = "sp_vc_selectVcReport";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Domain", Convert.ToInt32(ddl_Domain.SelectedValue));
            param[1] = new SqlParameter("@DateStart", DateStart);
            param[2] = new SqlParameter("@DateEnd", DateEnd);
            param[3] = new SqlParameter("@CompanyCode", txt_Custer.Text.Trim());

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            this.Alert("绑定数据失败！");
            return;
        }
    }

    protected void btn_Query_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        GridViewBind();
    }

    protected void btn_Export_Click(object sender, EventArgs e)
    {

        DateTime DateStart;
        DateTime DateEnd;
        if (string.IsNullOrEmpty(txt_datestart.Text.Trim()))
        {
            DateStart = Convert.ToDateTime(System.DateTime.Now.AddMonths(-1));
        }
        else
        {
            try
            {

                DateStart = Convert.ToDateTime(txt_datestart.Text);
            }
            catch
            {
                this.Alert("起始日输入的不是日期格式！");
                return;
            }
        }

        if (string.IsNullOrEmpty(txt_dateend.Text.Trim()))
        {
            DateEnd = Convert.ToDateTime(System.DateTime.Now);
        }
        else
        {
            try
            {

                DateEnd = Convert.ToDateTime(txt_dateend.Text.Trim());
            }
            catch
            {
                this.Alert("起始日输入的不是日期格式！");
                return;
            }
        }
        DataTable dt;
        try
        {
            string strName = "sp_vc_selectVcReport";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Domain", Convert.ToInt32(ddl_Domain.SelectedValue));
            param[1] = new SqlParameter("@DateStart", DateStart);
            param[2] = new SqlParameter("@DateEnd", DateEnd);
            param[3] = new SqlParameter("@CompanyCode", txt_Custer.Text.Trim());

            dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch
        {
            this.Alert("绑定数据失败！");
            return;
        }
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string createByName = string.Empty;//创建询价单人姓名
        string stroutFile = "vc_repoart_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        createinquiry(stroutFile, dt, createByName, Convert.ToInt32(Session["uID"]));
        ltlAlert.Text = "window.open('/Excel/" + stroutFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

    }

    public void createinquiry(string stroutFile, DataTable dts, string createByName, int uid)
    {
        System.Data.DataTable dt = dts;
        string excelname = System.DateTime.Now.ToString().Replace(":", "").Replace("-", "").Replace(" ", "");
        string strFile = stroutFile;
        string filePath = System.Web.HttpContext.Current.Server.MapPath("../Excel/" + strFile);
        MemoryStream ms = RenderDataTableToExcel(dt, filePath, uid, createByName) as MemoryStream;
        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        byte[] data = ms.ToArray();
        fs.Write(data, 0, data.Length);
        fs.Flush();
        fs.Close();
        data = null;
        ms = null;
        fs = null;
    }

    public Stream RenderDataTableToExcel(System.Data.DataTable SourceTable, string stroutFile, int uid, string createByName)
    {
        MemoryStream ms = new MemoryStream();
        FileStream file = new System.IO.FileStream(System.Web.HttpContext.Current.Server.MapPath("../docs/pur_compensation.xls"), FileMode.Open, FileAccess.Read);
        NPOI.HSSF.UserModel.HSSFWorkbook hssfworkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(file);
        NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheet("Sheet1");
        NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(17);
        file.Close();


        //输出头部信息
        #region 输出头部信息
        //sheet.GetRow(2).GetCell(0).SetCellValue("日期");
        //sheet.GetRow(2).GetCell(1).SetCellValue("供应商");
        //sheet.GetRow(2).GetCell(2).SetCellValue("名称");
        //sheet.GetRow(2).GetCell(3).SetCellValue("订单确认罚款");
        //sheet.GetRow(2).GetCell(4).SetCellValue("订单到货罚款");
        //sheet.GetRow(2).GetCell(5).SetCellValue("进货检验罚款");
        //sheet.GetRow(2).GetCell(6).SetCellValue("车间赔付");
        //sheet.GetRow(2).GetCell(7).SetCellValue("客户赔付索赔");
        //sheet.GetRow(2).GetCell(8).SetCellValue("其他");

        //输出明细


        //设置7列宽为100
        sheet.SetColumnWidth(13, 250);
        //sheet.SetColumnWidth(12, 100);
        //加标题
        //sheet.GetRow(11).GetCell(1).SetCellValue("QAD");
        //sheet.GetRow(11).GetCell(2).SetCellValue("部件号");
        //sheet.GetRow(11).GetCell(3).SetCellValue("单位");
        //sheet.GetRow(11).GetCell(4).SetCellValue("需求规格");
        //sheet.GetRow(11).GetCell(5).SetCellValue("详细描述");
        //sheet.GetRow(11).GetCell(6).SetCellValue("描述1");
        //sheet.GetRow(11).GetCell(7).SetCellValue("描述2");
        //sheet.GetRow(11).GetCell(6).SetCellValue("价格");
        //sheet.GetRow(11).GetCell(7).SetCellValue("币种");

        //明细起始行
        int rowIndex = 2;

        ICellStyle style2 = hssfworkbook.CreateCellStyle();
        style2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        style2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thick;



        foreach (DataRow row in SourceTable.Rows)
        {
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


            ICell cell = dataRow.CreateCell(0);
            cell.SetCellValue(row["vc_domain"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(1);
            cell.SetCellValue(row["vc_date"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(2);
            cell.SetCellValue(row["vc_vend"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(3);
            cell.SetCellValue(row["usr_companyName"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(4);
            cell.SetCellValue(row["checkrate"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(5);
            cell.SetCellValue(row["checkmount"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(6);
            cell.SetCellValue(row["arriverate"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(7);
            cell.SetCellValue(row["arrivemount"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(8);
            cell.SetCellValue(row["qcrate"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(9);
            cell.SetCellValue(row["qcmount"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(10);
            cell.SetCellValue(row["homemount"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(11);
            cell.SetCellValue(row["cusmount"].ToString());
            cell.CellStyle = style;

            cell = dataRow.CreateCell(12);
            cell.SetCellValue(row["otherrate"].ToString());
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

    protected void ddl_company_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewBind();
    }
}
