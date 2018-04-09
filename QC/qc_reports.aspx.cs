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

public partial class QC_qc_reports : BasePage
{
    QC _qc = new QC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!_qc.TcpLimited(Session["uID"].ToString()))
                this.tblTcp.Visible = false;
            else
                this.tblTcp.Visible = true;

            if (!_qc.OutLimited(Session["uID"].ToString()))
                this.tblOut.Visible = false;
            else
                this.tblOut.Visible = true;
        }
    }
    protected void btnDaily_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate.focus();";
            return;
        }

        if (txtEndDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtStdDate.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportIncoming(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnDaily_Product_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate_Product.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate_Product.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate_Product.focus();";
                return;
            }
        }
        else 
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate_Product.focus();";
            return;
        }

        if (txtEndDate_Product.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate_Product.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate_Product.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtStdDate_Product.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProduct(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox1.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox1.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox1.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox1.focus();";
            return;
        }


        if (TextBox2.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox2.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox2.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox1.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_cap" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductCap(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox3.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox3.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox3.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox3.focus();";
            return;
        }


        if (TextBox4.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox4.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox4.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox4.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_ballast" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductBallast(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnComponent_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox5.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox5.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox5.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox5.focus();";
            return;
        }


        if (TextBox6.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox6.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox6.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox6.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming_com.xls");
        string strImport = "qc_report_daily_incoming_com_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportIncomingCom(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnPackage_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox9.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox9.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox9.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox9.focus();";
            return;
        }


        if (TextBox10.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox10.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox10.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox10.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming_package.xls");
        string strImport = "qc_report_daily_incoming_package_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportIncomingPackage(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnPart_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox7.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox7.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox7.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox7.focus();";
            return;
        }


        if (TextBox8.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox8.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox8.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox8.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming_part.xls");
        string strImport = "qc_report_daily_incoming_part_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportIncomingPart(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnAcc_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox11.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox11.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox11.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox11.focus();";
            return;
        }


        if (TextBox12.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox12.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox12.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox12.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming_part.xls");
        string strImport = "qc_report_daily_incoming_part_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportIncomingAcc(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox13.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox13.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox13.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox13.focus();";
            return;
        }


        if (TextBox14.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox14.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox14.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox14.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_process.xls");
        string strImport = "qc_report_daily_process_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProcess(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox15.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox15.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox15.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox15.focus();";
            return;
        }

        if (TextBox16.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox16.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox16.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox16.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_process.xls");
        string strImport = "qc_report_daily_process_cap_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProcessCap(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox17.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox17.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox17.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox17.focus();";
            return;
        }

        if (TextBox18.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox18.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox18.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox18.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_process_ballast.xls");
        string strImport = "qc_report_daily_process_Ballast_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProcessBallast(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox19.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox19.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox19.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox19.focus();";
            return;
        }

        if (TextBox20.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox20.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox20.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox20.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_str_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductStr(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox21.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox21.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox21.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox21.focus();";
            return;
        }

        if (TextBox22.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox22.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox22.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox22.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_process.xls");
        string strImport = "qc_report_daily_process_str_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProcessStr(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnYear1_Click(object sender, EventArgs e)
    {
        string strFloder = Server.MapPath("/docs/qc_report_summary_incoming_year.xls");
        string strImport = "qc_report_summary_incoming_year_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.SummaryReportIncomingYear(dropType1.SelectedValue, dropType1.SelectedValue, dropYear1.SelectedValue);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnMonth1_Click(object sender, EventArgs e)
    {
        string strFloder = Server.MapPath("/docs/qc_report_summary_incoming_month.xls");
        string strImport = "qc_report_summary_incoming_month_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.SummaryReportIncomingMonth(dropType1.SelectedValue, dropType1.SelectedValue, dropYear1.SelectedValue, dropMonth1.SelectedValue);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox23.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox23.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox23.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox23.focus();";
            return;
        }

        if (TextBox24.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox24.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox24.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox24.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_tcp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportTcp(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox25.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox25.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox25.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox25.focus();";
            return;
        }

        if (TextBox26.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox26.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox26.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox26.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_tcp_cap_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportTcpCap(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox27.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox27.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox27.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox27.focus();";
            return;
        }

        if (TextBox28.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox28.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox28.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox28.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_tcp_ballast_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportTcpBallast(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox29.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox29.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox29.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox29.focus();";
            return;
        }

        if (TextBox30.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox30.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox30.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox30.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_process.xls");
        string strImport = "qc_report_daily_process_cap_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProcessCapZj(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox31.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox31.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox31.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox31.focus();";
            return;
        }

        if (TextBox32.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox32.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox32.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox32.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductMingG(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btnDaily_ProductOut_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox33.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox33.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox33.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox33.focus();";
            return;
        }

        if (TextBox34.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox34.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox34.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox34.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_out_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductOut(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void Button14_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox35.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox35.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox35.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox35.focus();";
            return;
        }


        if (TextBox36.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox36.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox36.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox36.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_cap_out_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductCapOut(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void Button15_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox37.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox37.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox37.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox37.focus();";
            return;
        }


        if (TextBox38.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox38.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox38.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox38.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_ballast_out_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductBallastOut(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void Button16_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox39.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox39.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox39.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox39.focus();";
            return;
        }

        if (TextBox40.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox40.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox40.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox40.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_out_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductMingGOut(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void Button17_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox41.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox41.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox41.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox41.focus();";
            return;
        }

        if (TextBox42.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox42.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox42.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox42.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_str_out_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductStrOut(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnDaily_lED_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate_LED.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate_LED.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate_LED.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate_LED.focus();";
            return;
        }

        if (txtEndDate_LED.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate_LED.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate_LED.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtStdDate_LED.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_LED_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportLED(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnDaily_lED_TCP_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate_LED_TCP.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate_LED_TCP.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate_LED_TCP.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate_LED_TCP.focus();";
            return;
        }

        if (txtEndDate_LED_TCP.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate_LED_TCP.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate_LED_TCP.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate_LED_TCP.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_led_tcp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportTcpLED(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnDaily_ProductOut_LED_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate_Out_LED.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate_Out_LED.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate_Out_LED.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate_Out_LED.focus();";
            return;
        }

        if (txtEndDate_Out_LED.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate_Out_LED.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate_Out_LED.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate_Out_LED.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_led_out_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductOutLED(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button18_Click(object sender, EventArgs e)
    {
        
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox43.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox43.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox13.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox13.focus();";
            return;
        }


        if (TextBox44.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox44.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox14.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox14.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_process.xls");
        string strImport = "qc_report_daily_process_LED_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProcessLED(uID, strStdDate, strEndDate);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button19_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox45.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox45.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox45.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox45.focus();";
            return;
        }

        if (TextBox46.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox46.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox46.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox46.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_SFP_out_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductSFPOut(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button20_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox47.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox47.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox47.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox47.focus();";
            return;
        }

        if (TextBox48.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox48.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox48.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox48.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductRework(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button21_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox49.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox49.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox49.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox49.focus();";
            return;
        }

        if (TextBox50.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox50.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox50.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox50.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_LED_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportLEDRework(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button22_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox51.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox51.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox51.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox51.focus();";
            return;
        }


        if (TextBox52.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox52.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox52.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox52.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_cap" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductCapRework(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button23_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox53.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox53.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox53.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox53.focus();";
            return;
        }


        if (TextBox54.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox54.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox54.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox54.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_ballast" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductBallastRework(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button24_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox55.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox55.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox55.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox55.focus();";
            return;
        }

        if (TextBox56.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox56.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox56.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox56.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductMingGRework(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button25_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox57.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox57.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox57.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox57.focus();";
            return;
        }

        if (TextBox58.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox58.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox58.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox58.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_str_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductStrRework(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button26_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox59.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox59.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox59.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox59.focus();";
            return;
        }

        if (TextBox60.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox60.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox60.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox60.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductSecond(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button27_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox61.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox61.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox61.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox61.focus();";
            return;
        }

        if (TextBox62.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox62.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox62.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox62.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_LED_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportLEDSecond(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button28_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox63.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox63.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox63.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox63.focus();";
            return;
        }


        if (TextBox64.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox64.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox64.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox64.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_cap" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductCapSecond(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button29_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox65.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox65.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox65.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox65.focus();";
            return;
        }


        if (TextBox66.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox66.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox66.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox66.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_ballast" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductBallastSecond(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button30_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox67.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox67.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox67.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox67.focus();";
            return;
        }

        if (TextBox68.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox68.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox68.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox68.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductMingGSecond(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button31_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox69.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox57.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox69.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox69.focus();";
            return;
        }

        if (TextBox70.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox70.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox70.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox70.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_product_str_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportProductStrSecond(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnDSD_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox71.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox71.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox71.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox71.focus();";
            return;
        }

        if (TextBox72.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox72.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox72.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox72.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_DSD_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportDSD(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void Button32_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox73.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox73.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox73.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox73.focus();";
            return;
        }

        if (TextBox74.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox74.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox74.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox74.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_dsd_tcp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportTcpDSD(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
 
    protected void Button34_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox77.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox77.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox77.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox77.focus();";
            return;
        }

        if (TextBox78.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox78.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox78.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox78.focus();";
            return;
        }

        int uID = int.Parse(Session["uID"].ToString());
        int nPlantCode = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_T8_tcp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportTcpT8(uID, strStdDate, strEndDate, nPlantCode);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void btnT8_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (TextBox75.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox75.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.TextBox75.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.TextBox75.focus();";
            return;
        }

        if (TextBox76.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(TextBox76.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.TextBox76.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.TextBox76.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());
        int nPlantID = int.Parse(Session["PlantCode"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_product.xls");
        string strImport = "qc_report_daily_T8_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.DailyReportT8(uID, strStdDate, strEndDate, nPlantID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
}
