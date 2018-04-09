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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
//using Excel;
using ExcelHelper;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using System.IO;


public partial class price_F4311 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind();
        }
    }

    private DataSet SelectF4311()
    {
        String strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.JDE_DATE"];

        try
        {
            string strName = "sp_JDE_selectF4311";
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@order", txtOrder.Text.Trim());
            param[1] = new SqlParameter("@item", txtItemNumber.Text.Trim());
            param[2] = new SqlParameter("@reqdate1", txtReqDate1.Text.Trim());
            param[3] = new SqlParameter("@reqdate2", txtReqDate2.Text.Trim());
            param[4] = new SqlParameter("@orddate1", txtOrdDate1.Text.Trim());
            param[5] = new SqlParameter("@orddate2", txtOrdDate2.Text.Trim());
            param[6] = new SqlParameter("@upddate1", txtUpdDate1.Text.Trim());
            param[7] = new SqlParameter("@upddate2", txtUpdDate2.Text.Trim());
            param[8] = new SqlParameter("@ref", txtRef.Text.Trim());

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }
    }

    private void Bind()
    {
        System.Data.DataTable dt = SelectF4311().Tables[0];

        if (dt.Rows.Count == 0)
        {
            dt = dt.Clone();
            dt.Rows.Add(dt.NewRow());
        }

        gvF4311.DataSource = dt;
        gvF4311.DataBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        #region Authentication
        //if (txtItemNumber.Text.Trim() != String.Empty)
        //{
        //    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,}$");

        //    if (!regex.IsMatch(txtItemNumber.Text.Trim()))
        //    {
        //        ltlAlert.Text = "alert('ItemNumber must be a set of numbers');";
        //        return;
        //    }
        //}

        if (txtReqDate1.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtReqDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Requested Date\" format is incorrect');";
                return;

            }
        }

        if (txtReqDate2.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtReqDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Requested Date\" format is incorrect');";
                return;

            }
        }


        if (txtOrdDate1.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtOrdDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Order Date\" format is incorrect');";
                return;

            }
        }

        if (txtOrdDate2.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtOrdDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Order Date\" format is incorrect');";
                return;

            }
        }

        if (txtUpdDate1.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtUpdDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Updated Date\" format is incorrect');";
                return;

            }
        }

        if (txtUpdDate2.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtUpdDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Updated Date\" format is incorrect');";
                return;

            }
        }
        #endregion

        Bind();
    }
    protected void gvF4311_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvF4311.PageIndex = e.NewPageIndex;
        Bind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        #region Authentication
        if (txtItemNumber.Text.Trim() != String.Empty)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,}$");

            if (!regex.IsMatch(txtItemNumber.Text.Trim()))
            {
                ltlAlert.Text = "alert('ItemNumber must be a set of numbers');";
                return;
            }
        }

        if (txtReqDate1.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtReqDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Requested Date\" format is incorrect');";
                return;

            }
        }

        if (txtReqDate2.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtReqDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Requested Date\" format is incorrect');";
                return;

            }
        }


        if (txtOrdDate1.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtOrdDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Order Date\" format is incorrect');";
                return;

            }
        }

        if (txtOrdDate2.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtOrdDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Order Date\" format is incorrect');";
                return;

            }
        }

        if (txtUpdDate1.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtUpdDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Updated Date\" format is incorrect');";
                return;

            }
        }

        if (txtUpdDate2.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtUpdDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('\"Updated Date\" format is incorrect');";
                return;

            }
        }

       
        #endregion

        string title = "<b>Order</b>~^100^<b>LineNumber</b>~^100^<b>BusinessUnit</b>~^100^<b>ShipTo</b>~^100^<b>Requested</b>~^100^<b>OrderDate</b>~^"
         + "100^<b>OriginalPromised</b>~^100^<b>ActualShip</b>~^<b>CancelDate</b>~^<b>Reference2</b>~^100^<b>ItemNumber</b>~^"
         + "100^<b>2ndItemNumber</b>~^<b>Description</b>~^<b>LineType</b>~^<b>StatusCode-Next</b>~^<b>StatusCode-Last</b>~^<b>U/M</b>~^<b>Quantity</b>~^<b>QuantityOpen</b>~^<b>QuantityReceived</b>~^<b>UnitCost</b>~^<b>PrintMessage</b>~^"
         + "<b>FreightHandlingCode</b>~^<b>BuyerNumber</b>~^<b>TransactionOriginator</b>~^<b>UserID</b>~^100^<b>DateUpdated</b>~^";
        DataTable dts = SelectF4311().Tables[0];
        ExportExcel(title, dts, false);
        //string strFloder = Server.MapPath("../docs/qc_F4311.xls");
        //string strImport = "qc_F4311" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        //// 创建一个Application对象并使其可见 
        //Excel.Application app = new Excel.ApplicationClass();
        //app.Visible = false;

        //// 打开模板文件，得到WorkBook对象 
        //Excel.Workbook workBook = app.Workbooks.Open(strFloder, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        ////输出明细信息
        //DataSet _dataset = SelectF4311();

        //Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        //for (int col = 0; col < _dataset.Tables[0].Columns.Count; col++)
        //{
        //    workSheet.Cells[1, col + 1] = _dataset.Tables[0].Columns[col].ColumnName.ToString();
        //}

        //int nRows = 2;

        //foreach (DataRow row in _dataset.Tables[0].Rows)
        //{
        //    for (int col = 0; col < _dataset.Tables[0].Columns.Count; col++)
        //    {
        //        workSheet.Cells[nRows, col + 1] = row[col].ToString();
        //    }

        //    nRows += 1;
        //}

        //// 输出Excel文件并退出 
        //try
        //{
        //    //workBook.Protect(strRnd, true, false);
        //    workBook.SaveAs(Server.MapPath("../Excel/") + strImport, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
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
        //}
        //catch
        //{}
        //finally
        //{
        //    KillProcess("EXCEL");
        //}

        //GC.Collect();

        //ltlAlert.Text = "window.open('../Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    private void KillProcess(string processName)
    {
        System.Diagnostics.Process myproc = new System.Diagnostics.Process();
        //得到所有打开的进程
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

}
