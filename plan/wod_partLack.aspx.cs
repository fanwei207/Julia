using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;

public partial class plan_wod_partLack : BasePage
{
    private wo.WodPartLack helper = new wo.WodPartLack();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string nbr = txtNbr.Text.Trim();
        string dateFrom = txtDateFrom.Text.Trim();
        string dateTo = txtDateTo.Text.Trim();
        string qad = txtQAD.Text.Trim();
        string lack = ddlLack.SelectedValue;
        string site = ddlSite.SelectedItem.Text;
        string domain = ddlDomain.SelectedItem.Text;
        gvlist.DataSource = helper.GetPartLack(nbr, dateFrom, dateTo, qad, lack, site, domain);
        gvlist.DataBind();
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string nbr = txtNbr.Text.Trim();
        string dateFrom = txtDateFrom.Text.Trim();
        string dateTo = txtDateTo.Text.Trim();
        string qad = txtQAD.Text.Trim();
        string lack = ddlLack.SelectedValue;
        string site = ddlSite.SelectedItem.Text;
        string domain = ddlDomain.SelectedItem.Text;
        string title = "<b>加工单</b>~^<b>ID</b>~^120^<b>QAD</b>~^<b>下达日期</b>~^<b>截止日期</b>~^120^<b>需求物料</b>~^<b>是否缺料</b>~^<b>缺料量</b>~^<b>工单缺料量</b>~^<b>地点</b>~^<b>域</b>~^<b>类型</b>~^<b>订单号/Loc Status</b>~^<b>Line</b>~^<b>供应商</b>~^<b>供应商是否确认</b>~^<b>订单截止日期</b>~^<b>在途数量/库存数量</b>~^<b>分配数量</b>~^<b>剩余数量</b>~^";
        DataTable wodData = helper.GetPartLackReport(nbr, dateFrom, dateTo, qad, lack, site, domain);
        this.ExportExcel(title, wodData, false, 11, "wod_id");
    }

    //public void ExportExcel(DataTable wodData)
    //{
        

    //    IWorkbook workbook = new HSSFWorkbook();
    //    ISheet sheet = workbook.CreateSheet("excel");


    //    wodData.DefaultView.Sort = "wod_createddate asc";
    //    DataTable dt = wodData.DefaultView.ToTable();

    //    DataTable podData = helper.GetPodLoc(dt.Rows[0]["wod_id"].ToString());

    //    //头栏样式
    //    ICellStyle styleHeader = SetHeaderStyle(workbook);

    //    //写标题栏
    //    IRow rowHeader = sheet.CreateRow(0);

    //    SetColumnTitleAndStyle(workbook, sheet, dt, podData, styleHeader, rowHeader);

    //    //写明细数据
    //    SetDetailsValue(sheet, dt, 1);

    //    wodData.Reset();

    //    string _localFileName = string.Format("{0}.xls", DateTime.Now.ToFileTime().ToString());

    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        workbook.Write(ms);

    //        Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
    //        localFile.Write(ms.ToArray(), 0, (int)ms.Length);
    //        localFile.Dispose();
    //        ms.Flush();
    //        ms.Position = 0;
    //        sheet = null;
    //        workbook = null;
    //    }

    //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");
    //}

    //private void SetDetailsValue(ISheet sheet, DataTable dt, int startRowIndex, bool fullDateFormat = false)
    //{
    //    string part = "";
    //    string site = "";
    //    string wodPart = "";
    //    string wodSite = "";
    //    int rowIndex = 0;
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        wodPart = dt.Rows[i]["wod_part"].ToString();
    //        wodSite = dt.Rows[i]["wod_site"].ToString();
    //        IRow row;
    //        if (part != wodPart || site != wodSite)
    //        {
    //            DataTable surplusData = helper.GetPadLocSurplus(part, site);
    //            for (int pi = 0; pi < surplusData.Rows.Count; pi++)
    //            {
    //                row = sheet.CreateRow(rowIndex + startRowIndex);
    //                for (int j = 0; j < surplusData.Columns.Count; j++)
    //                {
    //                    ICell cell;
    //                    if (j == surplusData.Columns.Count - 1)
    //                    {
    //                        cell = row.CreateCell(j + dt.Columns.Count - 1);
    //                    }
    //                    else
    //                    {
    //                        cell = row.CreateCell(j + dt.Columns.Count - 2);
    //                    }
    //                    int _col1 = j;
    //                    cell.SetCellValue(surplusData.Rows[pi][_col1], fullDateFormat);
    //                }
    //                rowIndex++;
    //            }
    //            part = wodPart;
    //            site = wodSite;
    //        }
    //        row = sheet.CreateRow(rowIndex + startRowIndex);
    //        for (int j = 1; j < dt.Columns.Count - 1; j++)
    //        {
    //            ICell cell = row.CreateCell(j - 1);
    //            int _col1 = j ;
    //            cell.SetCellValue(dt.Rows[i][_col1], fullDateFormat);
    //        }

    //        DataTable podData = helper.GetPodLoc(dt.Rows[i]["wod_id"].ToString());

    //        if (podData.Rows.Count == 0)
    //        {
    //            rowIndex++;
    //        }
    //        else
    //        {
    //            for (int pi = 0; pi < podData.Rows.Count; pi++)
    //            {
    //                if (pi != 0)
    //                {
    //                    row = sheet.CreateRow(rowIndex + startRowIndex);
    //                }
    //                for (int j = 0; j < podData.Columns.Count; j++)
    //                {
    //                    ICell cell = row.CreateCell(j + dt.Columns.Count - 2);
    //                    int _col1 = j;
    //                    cell.SetCellValue(podData.Rows[pi][_col1], fullDateFormat);
    //                }
    //                rowIndex++;
    //            }
    //        }


    //    }

    //}

    //private void SetColumnTitleAndStyle(IWorkbook workbook, ISheet sheet,  DataTable dtWod, DataTable dtPod,ICellStyle styleHeader, IRow rowHeader)
    //{
    //    Dictionary<string, string> wodDic = new Dictionary<string, string>();
    //    wodDic.Add("wod_nbr", "加工单");
    //    wodDic.Add("wod_lot", "ID");
    //    wodDic.Add("wod_wopart", "QAD");
    //    wodDic.Add("wod_rel_date", "下达日期");
    //    wodDic.Add("wod_due_date", "截止日期");
    //    wodDic.Add("wod_part", "需求物料");
    //    wodDic.Add("wod_lack", "是否缺料");
    //    wodDic.Add("wod_qty_need", "短缺量");
    //    wodDic.Add("wod_qty_lack", "工单缺料量");
    //    wodDic.Add("wod_site", "地点");
    //    wodDic.Add("wod_domain", "域");
    //    wodDic.Add("wod_potype", "类型");
    //    wodDic.Add("wod_ponbr", "订单号/Loc Status");
    //    wodDic.Add("wod_podline", "line");
    //    wodDic.Add("wod_povend", "供应商");
    //    wodDic.Add("wod_podduedate", "订单截止日期");
    //    wodDic.Add("wod_podqty", "在途数量/库存数量");
    //    wodDic.Add("wod_podqty_iss", "分配数量");
    //    wodDic.Add("wod_poconf", "供应商是否确认");

    //    int colIndex = 0;
    //    for (int index = 1; index < dtWod.Columns.Count -1 ; index++)
    //    {
    //        sheet.SetColumnWidth(colIndex, 120*6000 / 164);
    //        ICell cell = rowHeader.CreateCell(colIndex);
    //        cell.CellStyle = styleHeader;
    //        cell.SetCellValue(wodDic[dtWod.Columns[index].ColumnName]);

    //        ICellStyle columnStyle = SetColumnStyleByDataType(workbook, dtWod.Columns[index].DataType.ToString());
    //        sheet.SetDefaultColumnStyle(colIndex, columnStyle);
    //        colIndex++;
    //    }
    //    for (int index = 0; index < dtPod.Columns.Count; index++)
    //    {
    //        sheet.SetColumnWidth(colIndex, 120 * 6000 / 164);
    //        ICell cell = rowHeader.CreateCell(colIndex);
    //        cell.CellStyle = styleHeader;
    //        cell.SetCellValue(wodDic[dtPod.Columns[index].ColumnName]);

    //        ICellStyle columnStyle = SetColumnStyleByDataType(workbook, dtPod.Columns[index].DataType.ToString());
    //        sheet.SetDefaultColumnStyle(colIndex, columnStyle);
    //        colIndex++;
    //    }
    //    sheet.SetColumnWidth(colIndex, 120 * 6000 / 164);
    //    ICell cell1 = rowHeader.CreateCell(colIndex);
    //    cell1.CellStyle = styleHeader;
    //    cell1.SetCellValue("剩余数量");
    //    ICellStyle columnStyle1 = SetColumnStyleByDataType(workbook, "System.Float");
    //    sheet.SetDefaultColumnStyle(colIndex, columnStyle1);
    //}

    //private ICellStyle SetHeaderStyle(IWorkbook workbook)
    //{
    //    ICellStyle styleHeader = workbook.CreateCellStyle();
    //    styleHeader.Alignment = HorizontalAlignment.Center;//居中对齐

    //    styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
    //    styleHeader.FillPattern = FillPattern.SolidForeground;

    //    styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
    //    styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
    //    styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
    //    styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
    //    styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
    //    styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
    //    styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
    //    styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

    //    IFont fontHeader = workbook.CreateFont();
    //    fontHeader.FontHeightInPoints = 10;
    //    fontHeader.Boldweight = 600;
    //    styleHeader.SetFont(fontHeader);

    //    return styleHeader;
    //}

    //private ICellStyle SetColumnStyleByDataType(IWorkbook workbook, string dataType)
    //{
    //    ICellStyle style = workbook.CreateCellStyle();
    //    IFont font = workbook.CreateFont();

    //    switch (dataType)
    //    {
    //        case "System.DateTime":
    //            style.Alignment = HorizontalAlignment.Center;
    //            break;
    //        case "System.Int16":
    //            style.Alignment = HorizontalAlignment.Right;
    //            break;
    //        case "System.Int32":
    //            style.Alignment = HorizontalAlignment.Right;
    //            break;
    //        case "System.Int64":
    //            style.Alignment = HorizontalAlignment.Right;
    //            break;
    //        case "System.Decimal":
    //            style.Alignment = HorizontalAlignment.Right;
    //            break;
    //        case "System.Float":
    //            style.Alignment = HorizontalAlignment.Right;
    //            break;
    //        case "System.Double":
    //            style.Alignment = HorizontalAlignment.Right;
    //            break;
    //        case "System.Boolean":
    //            style.Alignment = HorizontalAlignment.Center;
    //            break;
    //        case "System.String":
    //            style.Alignment = HorizontalAlignment.Left;
    //            style.WrapText = true;
    //            break;
    //    }
    //    style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
    //    style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
    //    style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
    //    style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
    //    style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
    //    style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
    //    style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
    //    style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

    //    font.FontHeightInPoints = 9;
    //    style.SetFont(font);
    //    return style;
    //}
}