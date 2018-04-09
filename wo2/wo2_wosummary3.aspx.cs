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
using QCProgress;

public partial class wo2_wosummary3 : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToInt32(Session["PlantCode"].ToString()) == 1)
            {
                trZhengDeng.Visible = true;
                trMaoGuan.Visible = false;
                trPcb.Visible = false;
                trMingGuan.Visible = false;
                trZhiGuan.Visible = false;
            }
            else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 2)
            {
                trZhengDeng.Visible = true;
                trMaoGuan.Visible = true;
                trPcb.Visible = true;
                trMingGuan.Visible = false;
                trZhiGuan.Visible = false;
            }
            else if (Convert.ToInt32(Session["PlantCode"].ToString()) == 5)
            {
                trZhengDeng.Visible = true;
                trMaoGuan.Visible = true;
                trPcb.Visible = false;
                trMingGuan.Visible = true;
                trZhiGuan.Visible = true;
            }
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (txtDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('截止日期不能为空!')";
            return;
        }
        else
        {
            try
            {
                DateTime _dd = Convert.ToDateTime(txtDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!')";
                return;
            }
        }

        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("../docs/wo2_ZD.xls");
        string strImport = "wo2_ZD_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.ExportRepSummary("sp_wo2_rep_wosummary_ZD", txtDate.Text, uID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnExportMao_Click(object sender, EventArgs e)
    {
        if (txtDateMao.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('截止日期不能为空!')";
            return;
        }
        else
        {
            try
            {
                DateTime _dd = Convert.ToDateTime(txtDateMao.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!')";
                return;
            }
        }

        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("../docs/wo2_ZD.xls");
        string strImport = "wo2_MAO_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.ExportRepSummary("sp_wo2_rep_wosummary_MAO", txtDateMao.Text, uID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnExportPcb_Click(object sender, EventArgs e)
    {
        if (txtDatePcb.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('截止日期不能为空!')";
            return;
        }
        else
        {
            try
            {
                DateTime _dd = Convert.ToDateTime(txtDatePcb.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!')";
                return;
            }
        }

        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("../docs/wo2_ZD.xls");
        string strImport = "wo2_PCB_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.ExportRepSummary("sp_wo2_rep_wosummary_PCB", txtDatePcb.Text, uID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnExportMI_Click(object sender, EventArgs e)
    {
        if (txtDateMI.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('截止日期不能为空!')";
            return;
        }
        else
        {
            try
            {
                DateTime _dd = Convert.ToDateTime(txtDateMI.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!')";
                return;
            }
        }

        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("../docs/wo2_ZD.xls");
        string strImport = "wo2_MING_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.ExportRepSummary("sp_wo2_rep_wosummary_MING", txtDateMI.Text, uID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void btnExportZHI_Click(object sender, EventArgs e)
    {
        if (txtDateZhi.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('截止日期不能为空!')";
            return;
        }
        else
        {
            try
            {
                DateTime _dd = Convert.ToDateTime(txtDateZhi.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!')";
                return;
            }
        }

        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("../docs/wo2_ZD.xls");
        string strImport = "wo2_ZHI_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
        _qcexcel.ExportRepSummary("sp_wo2_rep_wosummary_ZHI", txtDateZhi.Text, uID);

        GC.Collect();

        ltlAlert.Text = "window.open('/Excel/" + strImport + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
}
