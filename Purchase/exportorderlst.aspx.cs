using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adamFuncs;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Purchase_exportorderlst : System.Web.UI.Page
{
    public adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = GetPurchaseOrder();

            if (dt != null && dt.Rows.Count > 0)
            {
                AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                doc.FileName = "orderlist-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("采购单列表");

                int rowIndex = 2;

                AppLibrary.WriteExcel.XF xf = doc.NewXF();
                xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
                xf.Font.FontName = "宋体";
                xf.UseMisc = true;
                xf.Font.Bold = true;
                xf.Font.Height = 9 * 256 / 13; //对应size = 13
                xf.LeftLineStyle = 1;
                xf.TopLineStyle = 1;
                xf.RightLineStyle = 1;
                xf.BottomLineStyle = 1;
                xf.TextWrapRight = true;

                sheet.Cells.Add(1, 1, "采购单", xf);
                sheet.Cells.Add(1, 2, "供应商", xf);
                sheet.Cells.Add(1, 3, "地点", xf);
                sheet.Cells.Add(1, 4, "域", xf);
                sheet.Cells.Add(1, 5, "订货日期", xf);
                sheet.Cells.Add(1, 6, "截至日期", xf);
              
                //采购单列
                AppLibrary.WriteExcel.ColumnInfo orderno = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                orderno.ColumnIndexStart = 0;
                orderno.ColumnIndexEnd = 0;
                orderno.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(orderno);
                //供应商列
                AppLibrary.WriteExcel.ColumnInfo supplyNo = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                supplyNo.ColumnIndexStart = 1;
                supplyNo.ColumnIndexEnd = 1;
                supplyNo.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(supplyNo);
                //地点列
                AppLibrary.WriteExcel.ColumnInfo adress = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                adress.ColumnIndexStart = 2;
                adress.ColumnIndexEnd = 2;
                adress.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(adress);
                //域列
                AppLibrary.WriteExcel.ColumnInfo domain = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                domain.ColumnIndexStart = 3;
                domain.ColumnIndexEnd = 3;
                domain.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(domain);
                //订货日期列
                AppLibrary.WriteExcel.ColumnInfo orderdate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                orderdate.ColumnIndexStart = 4;
                orderdate.ColumnIndexEnd = 4;
                orderdate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(orderdate);
                //截至日期列
                AppLibrary.WriteExcel.ColumnInfo enddate = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                enddate.ColumnIndexStart = 5;
                enddate.ColumnIndexEnd = 5;
                enddate.Width = 100 * 6000 / 164;
                sheet.AddColumnInfo(enddate);

                foreach (DataRow row in dt.Rows)
                {
                    
                    PrintExcel(doc, sheet, rowIndex
                                , row["po_nbr"], row["po_vend"], row["po_ship"], row["po_domain"], row["po_ord_date"]
                                , row["po_due_date"]);
                    rowIndex++;
                }

                doc.Send();

                Response.Flush();
                Response.End();
                dt.Dispose();
            }
            else
            {
                Response.Redirect("RP_polist.aspx");
            }




        }
    }


    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex
                                , Object po_nbr, Object po_vend, Object po_ship, Object po_domain, Object po_ord_date
                                , Object po_due_date)
    {
        AppLibrary.WriteExcel.XF xf = doc.NewXF();
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        xf.Font.FontName = "宋体";
        xf.UseMisc = true;
        xf.Font.Bold = false;
        xf.Font.Height = 9 * 256 / 13; //对应size = 13
        xf.LeftLineStyle = 1;
        xf.TopLineStyle = 1;
        xf.RightLineStyle = 1;
        xf.BottomLineStyle = 1;
        xf.TextWrapRight = true;

        sheet.Cells.Add(rowIndex, 1, po_nbr.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, po_vend.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, po_ship.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, po_domain.ToString(), xf);
        sheet.Cells.Add(rowIndex, 5, po_ord_date.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, po_due_date.ToString(), xf);
      
    }


    public DataTable GetPurchaseOrder()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@pono", Request.QueryString["txbpo"]);
            param[1] = new SqlParameter("@povend", Request.QueryString["strvend"]);
            param[2] = new SqlParameter("@poship", Request.QueryString["ddlsite"]);
            param[3] = new SqlParameter("@podomain", Request.QueryString["ddldomain"]);
            param[4] = new SqlParameter("@poorddate", Request.QueryString["txbord"]);
            param[5] = new SqlParameter("@poduedate", Request.QueryString["txbdue"]);
            param[6] = new SqlParameter("@pocon", Request.QueryString["txbconp"]);
            param[7] = new SqlParameter("@condate", Request.QueryString["txtDate"]);
            param[8] = new SqlParameter("@postat", Request.QueryString["dropStat"]);
            param[9] = new SqlParameter("@buyer", "");

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_selectPurchaseOrder", param);
            return ds.Tables[0];
        }
        catch
        {
            return null;
        }
    }
}