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
using CommClass;

public partial class plan_tcpInventoryInfo : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected static DataSet selectLiitmLimcu(string liitm, string lilocn)
    {
        try
        {
            string strName = "sp_plan_selectTcpInventoryInfo";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@liitm", liitm);
            param[1] = new SqlParameter("@lilocn", lilocn);
            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.JDE_DATE"), CommandType.StoredProcedure, strName, param);
        }
        catch
        {
            return null;
        }


    }

    protected void BindData()
    {
        DataSet dst = selectLiitmLimcu(txtLiitm.Text.Trim(), txtLocation.Text.Trim());
        gvFix.DataSource = dst;
        gvFix.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvFix_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFix.PageIndex = e.NewPageIndex;
        gvFix.SelectedIndex = -1;
        BindData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = selectLiitmLimcu(txtLiitm.Text.Trim(), txtLocation.Text.Trim()).Tables[0];
        string title = "80^<b>liitm</b>~^100^<b>Location</b>~^200^<b>item</b>~^120^<b>QAD</b>~^60^<b>BU</b>~^150^<b>Lot</b>~^40^<b>P/S</b>~^100^<b>Last Receipt</b>~^80^<b>On Hand</b>~^80^<b>On Transit</b>~^80^<b>UnitPrice</b>~^400^<b>QAD Desc</b>~^";
        this.ExportExcel(title,dt,false);
    }
}
