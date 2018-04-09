using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_Mold_StatisticalInfo :BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindGridView();
            lbl_Qty.Text = gv.Rows.Count.ToString();
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected override void BindGridView()
    {
        string sql = "sp_Mold_selectStatisticalInfo";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@vend", txt_vend.Text.Trim());
        param[1] = new SqlParameter("@itemCode", txt_itemCode.Text.Trim());
        param[2] = new SqlParameter("@status", Convert.ToInt32(chb_status.Checked));

        gv.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param).Tables[0];
        gv.DataBind();
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindGridView();
        lbl_Qty.Text = gv.Rows.Count.ToString();
    }
}