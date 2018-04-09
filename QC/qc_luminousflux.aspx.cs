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


public partial class QC_luminousflux : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblPage.Text = Request.QueryString["page"].ToString().Trim();
            lblGroup.Text = Request.QueryString["group"].ToString().Trim();

            DataTable table = oqc.GetReportSnapByGroup(lblPage.Text.Trim(), lblGroup.Text.Trim());

            lblPart.Text = table.Rows[0][4].ToString().Trim();

            mRecv.Items.Add(new MenuItem(table.Rows[0][1].ToString().Trim(), table.Rows[0][1].ToString().Trim()));
            mRecv.Items[0].Selectable = false;

            mOrder.Items.Add(new MenuItem(table.Rows[0][2].ToString().Trim(), table.Rows[0][2].ToString().Trim()));
            mOrder.Items[0].Selectable = false;

            mLine.Items.Add(new MenuItem(table.Rows[0][3].ToString().Trim(), table.Rows[0][3].ToString().Trim()));
            mLine.Items[0].Selectable = false;

            decimal dd = Convert.ToDecimal(table.Rows[0][6].ToString().Trim());

            if (table.Rows.Count > 1)
            {
                dd -= Convert.ToDecimal(table.Rows[0][6].ToString().Trim());

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    mRecv.Items[0].ChildItems.Add(new MenuItem(table.Rows[i][1].ToString().Trim(), table.Rows[i][1].ToString().Trim()));
                    mRecv.Items[0].ChildItems[i].Selectable = false;

                    mOrder.Items[0].ChildItems.Add(new MenuItem(table.Rows[i][2].ToString().Trim(), table.Rows[i][2].ToString().Trim()));
                    mOrder.Items[0].ChildItems[i].Selectable = false;

                    mLine.Items[0].ChildItems.Add(new MenuItem(table.Rows[i][3].ToString().Trim(), table.Rows[i][3].ToString().Trim()));
                    mLine.Items[0].ChildItems[i].Selectable = false;

                    dd += Convert.ToDecimal(table.Rows[i][6].ToString().Trim());
                }
            }

            lblRcvd.Text = dd.ToString();

            table.Dispose();
        }
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
            if (mRecv.Items[0].ChildItems.Count == 0)
            {
                string line = mLine.Items[0].Text.Trim();
                string recv = mRecv.Items[0].Text.Trim();
                int hour = int.Parse(txtHour.Text.Trim());
                string way =dropWay.SelectedValue;
                string part = lblPart.Text.Trim();

                fop.ImportTxt(line, recv, hour, way, part, int.Parse(Session["uID"].ToString()), ref strMsg);
                if (strMsg != "")
                {
                    ltlAlert.Text = "alert('" + strMsg + "')"; ;
                    return;
                }
            }
            else
            {
                for (int i = 0; i < mRecv.Items[0].ChildItems.Count; i++)
                {
                    string line = mLine.Items[0].ChildItems[i].Text.Trim();
                    string recv = mRecv.Items[0].ChildItems[i].Text.Trim();
                    int hour = int.Parse(txtHour.Text.Trim());
                    string way = dropWay.SelectedValue;
                    string part = lblPart.Text.Trim();

                    fop.ImportTxt(line, recv, hour, way, part, int.Parse(Session["uID"].ToString()), ref strMsg);
                    if (strMsg != "成功写入数据库")
                    {
                        ltlAlert.Text = "alert('" + strMsg + "')";
                        return;
                    }
                    ltlAlert.Text = "alert('成功写入数据!')";
                }
            }
        }
    }
    protected void uploadBtnExcel_ServerClick(object sender, EventArgs e)
    {   
        FileOperate fop = new FileOperate(filenameExcel, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.XLS;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')"; ;
        }
        else
        {
            if (mRecv.Items[0].ChildItems.Count == 0)
            {
                string line = mLine.Items[0].Text.Trim();
                string recv = mRecv.Items[0].Text.Trim();
                int hour = 0;
                int way = 0;
                string part = lblPart.Text.Trim();

                fop.ImportExcel(line, recv, hour, way, part, int.Parse(Session["uID"].ToString()), ref strMsg);
                if (strMsg != "")
                {
                    ltlAlert.Text = "alert('" + strMsg + "')";
                    return;
                }
            }
            else
            {
                for (int i = 0; i < mRecv.Items[0].ChildItems.Count; i++)
                {
                    string line = mLine.Items[0].ChildItems[i].Text.Trim();
                    string recv = mRecv.Items[0].ChildItems[i].Text.Trim();
                    int hour = int.Parse(txtHour.Text.Trim());
                    int way = int.Parse(dropWay.SelectedValue);
                    string part = lblPart.Text.Trim();

                    fop.ImportExcel(line, recv, hour, way, part, int.Parse(Session["uID"].ToString()), ref strMsg);
                    if (strMsg != "")
                    {
                        ltlAlert.Text = "alert('" + strMsg + "')";
                        return;
                    }
                }
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (lblPage.Text.Trim() == "100103121")
            Response.Redirect("qc_report.aspx");
        else
            Response.Redirect("qc_report_undo.aspx");
    }
}
