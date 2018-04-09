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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using TCPC.WebChart;
using System.Drawing;
using System.Drawing.Drawing2D;
using Excel;
using System.Reflection;
using System.Diagnostics;

public partial class wo2_capacitycompared2 : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["uID"].ToString() == "56110")
                btnExport.Visible = true;

            txtEndDate.Text = DateTime.Now.AddDays(15).ToShortDateString().Replace("/", "-");
            txtStdDate.Text = DateTime.Now.ToShortDateString().Replace("/", "-"); 

            dropDomain_SelectedIndexChanged(this, new EventArgs());

            btnChart_Click(this, new EventArgs()); 
        }
    }

    protected DataSet GetCapcityCompared()
    {
        try
        {
            string strSql = "sp_wo2_capcityCompared2";

            SqlParameter[] parmArray = new SqlParameter[6];
            parmArray[0] = new SqlParameter("@domain", dropDomain.SelectedValue);
            parmArray[1] = new SqlParameter("@type", dropType.SelectedValue);
            parmArray[2] = new SqlParameter("@line", dropLine.SelectedValue);
            parmArray[3] = new SqlParameter("@stddate", txtStdDate.Text.Trim());
            parmArray[4] = new SqlParameter("@enddate", txtEndDate.Text.Trim());
            parmArray[5] = new SqlParameter("@uID", Session["uID"].ToString());

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
        }
        catch 
        {
            return null;
        }
    }
    protected void dropDomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropLine.Items.Clear();

        try
        {
            string strSql = "sp_wo2_selectLine";

            SqlParameter[] parmArray = new SqlParameter[1];
            parmArray[0] = new SqlParameter("@domain", dropDomain.SelectedValue);

            DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

            dropLine.DataSource = ds.Tables[0];
            dropLine.DataBind();
        }
        catch { }

        dropLine.Items.Insert(0, new ListItem("--", "--"));
        dropLine.SelectedIndex = 0;

        btnChart_Click(this, new EventArgs());
    }

    protected void btnChart_Click(object sender, EventArgs e)
    {
        if (txtStdDate.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtStdDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('日期格式不对');";

                return;
            }
        }

        if (txtEndDate.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtEndDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('日期格式不对');";

                return;
            }
        }

        DataSet ds = GetCapcityCompared();

        if (ds.Tables[0].Rows.Count < 1)
        {
            imgChart.Src = String.Empty;
            return;
        }

        TCPC.WebChart.Chart chart = new TCPC.WebChart.Chart(980, 440, ds.Tables[0]);
        chart.Adjust();

        chart.Path = Server.MapPath("../Excel/" + Session["uID"].ToString());

        //chart.SeriesGroup.Group[3].ChartType = ChartType.Line;

        chart.Paint();

        Pen myPen = new Pen(Brushes.Black);
        myPen.DashStyle = DashStyle.Dash;

        chart.GridLine.Pen = myPen;

        chart.yGridLine();

        Random rd = new Random();

        imgChart.Src = "../Excel/" + Session["uID"].ToString().Trim() + ".bmp?" + rd.Next().ToString();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        string strFloder = Server.MapPath("../docs/wo2_capacitycompared.xls");
        string strImport = "wo2_capacitycompared_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        SummaryReportIncomingYear(strFloder, Server.MapPath("../Excel/") + strImport);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    public void SummaryReportIncomingYear(string strFloder, string strImport)
    {
        // 创建一个Application对象并使其可见 
        Excel.Application app = new Excel.ApplicationClass();
        app.Visible = false;

        // 打开模板文件，得到WorkBook对象 
        Excel.Workbook workBook = app.Workbooks.Open(strFloder, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

        //输出明细信息
        DataSet _dataset = GetCapcityCompared();

        // 得到WorkSheet对象 
        Excel.Worksheet workSheet = (Excel.Worksheet)workBook.Sheets.get_Item(1);

        int nRows = 2;

        for (int col = 0; col < _dataset.Tables[0].Columns.Count; col++)
        {
            workSheet.Cells[1, col + 1] = _dataset.Tables[0].Columns[col].ColumnName;
        }

        foreach (DataRow row in _dataset.Tables[0].Rows)
        {
            for (int col = 0; col < _dataset.Tables[0].Columns.Count; col++)
            {
                workSheet.Cells[nRows, col + 1] = row[col].ToString();
            }

            nRows += 1;
        }

        //总数不良数合集
        Excel.Range xlColRange = workSheet.get_Range("A1:D" + Convert.ToString(_dataset.Tables[0].Rows.Count + 1), Missing.Value);
        Excel.Chart xlColChart = (Excel.Chart)workBook.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
       
        xlColChart.Name = "工单产能比较";

        //xlColChart.ChartArea.Height = 300;

        String strTitle = "工单产能比较(域:" + dropDomain.SelectedValue;

        if (dropType.SelectedIndex != 0)
        {
            strTitle += " 类别:" + dropType.SelectedValue;
        }

        if (dropLine.SelectedIndex != 0)
        {
            strTitle += " 生产线:" + dropLine.SelectedValue;
        }

        strTitle += ")";

        xlColChart.ChartWizard(xlColRange, Excel.XlChartType.xlColumnStacked, Type.Missing, Excel.XlRowCol.xlColumns, Type.Missing, Type.Missing, true,
                                strTitle, Type.Missing, "数量", "ExtraTitle");
        //Excel.Series xlColSeries = (Excel.Series)xlColChart.SeriesCollection(4);
        //xlColSeries.XValues = workSheet.get_Range("A2", "A" + workSheet.Rows.Count.ToString());
        //xlColSeries.ChartType = XlChartType.xlLine;

        Excel.ChartGroup xlGroup = (Excel.ChartGroup)xlColChart.ColumnGroups(1);
        xlGroup.GapWidth = 0;

        // 输出Excel文件并退出 
        try
        {
            //workBook.Protect(strRnd, true, false);
            workBook.SaveAs(strImport, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlExclusive, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
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
        catch
        {
           
        }
    }
    protected void dropLine_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnChart_Click(this, new EventArgs()); 
    }
    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnChart_Click(this, new EventArgs());
    }
}
