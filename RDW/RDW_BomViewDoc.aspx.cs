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

public partial class RDW_RDW_BomViewDoc : BasePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            Confirm.Enabled = this.Security["170022"].isValid;
            BindData();
            if (Request.QueryString["ty"] == "export")
            {
                DataSet ds = CompareBomList(Convert.ToInt32(Request.QueryString["mid"]), Request.QueryString["part"], Request.QueryString["domain"], Request.QueryString["date"]);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
                    doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("BOM of Comparison");

                    #region 设置列宽
                    AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col1.ColumnIndexStart = 0;
                    col1.ColumnIndexEnd = 0;
                    col1.Width = 50 * 6000 / 164;
                    sheet.AddColumnInfo(col1);

                    AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col2.ColumnIndexStart = 1;
                    col2.ColumnIndexEnd = 1;
                    col2.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col2);

                    AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col3.ColumnIndexStart = 2;
                    col3.ColumnIndexEnd = 2;
                    col3.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col3);

                    AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col4.ColumnIndexStart = 3;
                    col4.ColumnIndexEnd = 3;
                    col4.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col4);

                    AppLibrary.WriteExcel.ColumnInfo col5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col5.ColumnIndexStart = 4;
                    col5.ColumnIndexEnd = 4;
                    col5.Width = 10 * 6000 / 164;
                    sheet.AddColumnInfo(col5);

                    AppLibrary.WriteExcel.ColumnInfo col6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col6.ColumnIndexStart = 5;
                    col6.ColumnIndexEnd = 5;
                    col6.Width = 50 * 6000 / 164;
                    sheet.AddColumnInfo(col6);

                    AppLibrary.WriteExcel.ColumnInfo col7 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col7.ColumnIndexStart = 6;
                    col7.ColumnIndexEnd = 6;
                    col7.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col7);

                    AppLibrary.WriteExcel.ColumnInfo col8 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col8.ColumnIndexStart = 7;
                    col8.ColumnIndexEnd = 7;
                    col8.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col8);

                    AppLibrary.WriteExcel.ColumnInfo col9 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
                    col9.ColumnIndexStart = 8;
                    col9.ColumnIndexEnd = 8;
                    col9.Width = 100 * 6000 / 164;
                    sheet.AddColumnInfo(col9);
                    #endregion

                    int rowIndex = 1;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        PrintExcel(doc, sheet, rowIndex, row["levef"], row["ps_par"], row["ps_comp"], row["cost"], row["olevef"], row["ops_par"], row["ops_comp"], row["ocost"], row["oDate"]);
                        if (rowIndex == 1)
                        {
                            rowIndex += 4;
                        }
                        else
                        {
                            rowIndex++;
                        }
                    }

                    doc.Save(Server.MapPath("/Excel/"), true);
                    ds.Dispose();

                    ltlAlert.Text = "window.open('/Excel/" + doc.FileName + "','_blank');";
                }
            }
        }
    }

    protected void PrintExcel(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex,Object levef, Object ps_par, Object ps_comp, Object cost, Object olevef, Object ops_par, Object ops_comp, Object ocost, Object oDate )
    {
        AppLibrary.WriteExcel.XF xf = doc.NewXF();
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        xf.Font.FontName = "宋体";
        xf.UseMisc = true;
        xf.Font.Bold = false;
        xf.Font.Height = 9 * 256 / 13;

        xf.LeftLineStyle = 1;
        xf.TopLineStyle = 1;
        xf.RightLineStyle = 1;
        xf.BottomLineStyle = 1;

        if (rowIndex == 1)
        {
            sheet.Cells.Merge(rowIndex, rowIndex, 1, 9);
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
            sheet.Cells.Add(rowIndex, 1, "BOM Code:" + Request.QueryString["part"], xf);
           
            rowIndex++;

            sheet.Cells.Merge(rowIndex, rowIndex, 1, 4);
            sheet.Cells.Add(rowIndex, 1, "BOM Date1:" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now), xf);

            sheet.Cells.Merge(rowIndex, rowIndex, 6, 9);
            sheet.Cells.Add(rowIndex, 6, "BOM Date2:" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", oDate), xf);
            

            rowIndex++;
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
            sheet.Cells.Add(rowIndex, 1, "Level", xf);
            sheet.Cells.Add(rowIndex, 2, "Parent", xf);
            sheet.Cells.Add(rowIndex, 3, "Component", xf);
            sheet.Cells.Add(rowIndex, 4, "Cost", xf);
            sheet.Cells.Add(rowIndex, 6, "Level", xf);
            sheet.Cells.Add(rowIndex, 7, "Parent", xf);
            sheet.Cells.Add(rowIndex, 8, "Component", xf);
            sheet.Cells.Add(rowIndex, 9, "Cost", xf);
            rowIndex++;
        }
        sheet.Cells.Add(rowIndex, 1, levef.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, ps_par.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, ps_comp.ToString(), xf);
        sheet.Cells.Add(rowIndex, 4, cost.ToString(), xf);
        sheet.Cells.Add(rowIndex, 6, olevef.ToString(), xf);
        sheet.Cells.Add(rowIndex, 7, ops_par.ToString(), xf);
        sheet.Cells.Add(rowIndex, 8, ops_comp.ToString(), xf);
        sheet.Cells.Add(rowIndex, 9, ocost.ToString(), xf);

    }

    protected void BindData()
    {
        if (Request.QueryString["part"] != null)
        {
            
            lbPart.Text = Request.QueryString["part"];
            gv.DataSource = SelectBomList(Request.QueryString["part"],dropdomain.SelectedItem.Text,txtDate.Text.Trim());
            gv.DataBind();
        }

    }

    protected static DataTable SelectBomList(string part, string domain,string bomDate)
    {
        try
        {
            DataTable dt = null;
            string strName = "sp_rdw_selectBomList";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@part", part);
            param[1] = new SqlParameter("@domain", domain);
            param[2] = new SqlParameter("@bomDate", bomDate);
            return dt = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param).Tables[0];

        }
        catch
        {
            return null;
        }

    }

    protected static DataSet CompareBomList(int mid,string part, string domain, string bomDate)
    {
        try
        {
            DataSet ds = null;
            string strName = "sp_rdw_CompareBomList";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@mid",mid);
            param[1] = new SqlParameter("@part", part);
            param[2] = new SqlParameter("@domain", domain);
            param[3] = new SqlParameter("@bomDate", bomDate);
            return ds = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);

        }
        catch
        {
            return null;
        }

    }


    protected static bool ConfirmBom(int mid,string part, int createBy, string domain, string bomDate)
    {
        try
        {
            string strName = "sp_rdw_updateBomList";
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@mid", mid);
            param[1] = new SqlParameter("@part", part);
            param[2] = new SqlParameter("@domain", domain);
            param[3] = new SqlParameter("@bomDate", bomDate);
            param[4] = new SqlParameter("@createBy", createBy);
            param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_rdw"], CommandType.StoredProcedure, strName, param);
            return Convert.ToBoolean(param[5].Value);
        }
        catch
        {
            return false;
        }
    
    }

    protected void Confirm_Click(object sender, EventArgs e)
    {
            if (gv.Rows.Count > 0)
            {
                if (ConfirmBom(Convert.ToInt32(Request.QueryString["mid"]),lbPart.Text.Trim(), Convert.ToInt32(Session["uID"]), dropdomain.SelectedItem.Text, txtDate.Text.Trim()))
                {
                    ltlAlert.Text = "alert('Confirm BOM successfully!'); ";
                }
                else
                {
                    ltlAlert.Text = "alert('Confirm failed!'); ";
                    return;
                }
            }
            else
            {
                ltlAlert.Text = "alert('No BOM needs to be confirmed!'); ";
                return;
            }
    }


    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#ffffdd'");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
        }
    }
    protected void compare_Click(object sender, EventArgs e)
    {
        Response.Redirect("/RDW/RDW_BomViewDoc.aspx?ty=export&mid=" + Request.QueryString["mid"] + "&part=" + lbPart.Text.Trim() + "&domain=" + dropdomain.SelectedItem.Text + "&date=" + txtDate.Text.Trim() + "&rt=" + DateTime.Now.ToString());
    }
}
