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
using QADSID;

public partial class SID_ExportSIDDocumentInfo : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    string strFile = string.Empty;
    ExcelHelper.ExcelHelper excel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ExportExcel();
        }
    }

    protected void ExportExcel()
    {
        //定义参数
        string strSql = Convert.ToString(Session["EXSQL"]);

        strFile = "SID_DocumentInfo_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        excel = new ExcelHelper.ExcelHelper(Server.MapPath("/docs/SID_DocumentInfo.xls"), Server.MapPath("../Excel/") + strFile);

        excel.DocumentInfoToExcel("发票信息汇总", strSql);

        ltlAlert.Text = "window.open('/Excel/" + strFile + "', '_blank');";
    }
}
