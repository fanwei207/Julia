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
using System.IO;
using QCProgress;

public partial class QC_qc_product_lum : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            lblNbr.Text = Request.QueryString["nbr"].ToString();
            lblLot.Text = Request.QueryString["lot"].ToString();
            lblRcvd.Text = Request.QueryString["rcvd"].ToString();
            lblPart.Text = Request.QueryString["part"].ToString();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("qc_product.aspx");
    }
    protected void uploadBtn_ServerClick(object sender, EventArgs e)
    {
        if (txtHour.Text.Trim() == "")
        {
            ltlAlert.Text = "alert('测试时间不能为空')";
            return;
        }

        if (dropWay.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择测试方式')";
            return;
        }
        try
        {
            int n = int.Parse(txtHour.Text.Trim());
        }
        catch
        {
            ltlAlert.Text = "alert('测试时间必须为整数')";
            return;
        }

        FileOperate fop = new FileOperate(filename, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.TXT;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {
            string line = lblNbr.Text.Trim();
            string recv = lblLot.Text.Trim();
            int hour = int.Parse(txtHour.Text.Trim());
            string way = dropWay.SelectedValue;
            string part = lblPart.Text.Trim();

            fop.ProductImportTxt(line, recv, hour, way, part, int.Parse(Session["uID"].ToString()), Convert.ToBoolean(Request.QueryString["tcp"].ToString()),ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }
            ltlAlert.Text = "alert('导入数据成功!')";
        }
    }
    protected void uploadBtnExcel_ServerClick(object sender, EventArgs e)
    {
        FileOperate fop = new FileOperate(filenameExcel, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.XLS;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')"; 
            return;
        }
        else
        {
            string line = lblNbr.Text.Trim();
            string recv = lblLot.Text.Trim();
            int hour = 0;
            int way = 0;
            string part = lblPart.Text.Trim();

            fop.ProductImportExcel(line, recv, hour, way, part, int.Parse(Session["uID"].ToString()), Convert.ToBoolean(Request.QueryString["tcp"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }
            ltlAlert.Text = "alert('导入数据成功!')";
        }
    }
}
