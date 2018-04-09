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

public partial class Purchase_Mold_AllLists : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            gv.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");//正常换行          

            gv.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");//自动换行

            txt_startDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            txt_endDate.Text = DateTime.Now.AddDays(1).ToShortDateString();
            ddl_detailsStatus.SelectedValue = "-1";
            BindGridView();
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected override void BindGridView()
    {
        string startDate;
        string endDate;
        //if (txt_startDate.Text.Trim().Length == 0)
        //    startDate = DateTime.Now.AddMonths(-1).ToShortDateString();
        //else
            startDate = txt_startDate.Text.Trim();
        //if (txt_endDate.Text.Trim().Length == 0)
        //    endDate = DateTime.Now.AddDays(1).ToShortDateString();
        //else
            endDate = txt_endDate.Text.Trim();

        string sql = "sp_mold_selectAllMoldInfo";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@vend", txt_vend.Text.Trim());
        param[1] = new SqlParameter("@moldNbr", txt_MoldNbr.Text.Trim());
        param[2] = new SqlParameter("@detailNbr", txt_DetailNbr.Text.Trim());
        param[3] = new SqlParameter("@status", Convert.ToInt32(ddl_detailsStatus.SelectedValue));
        param[4] = new SqlParameter("@startDate", startDate);
        param[5] = new SqlParameter("@endDate ", endDate);
        param[6] = new SqlParameter("@QAD ", txt_QAD.Text.Trim());


        gv.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param).Tables[0];
        gv.DataBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         int intRow = Convert.ToInt32(e.CommandArgument.ToString());
         string detID = gv.DataKeys[intRow].Values["detID"].ToString();
         string IntStatus = gv.DataKeys[intRow].Values["IntStatus"].ToString();

        if (e.CommandName.ToString() == "Change")
        {
            Response.Redirect("Mold_ChangeStatus.aspx?detID=" + detID + "&IntStatus=" + IntStatus);
        }
       
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql = "sp_mold_selectAllMoldInfoExcel";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@vend", txt_vend.Text.Trim());
        param[1] = new SqlParameter("@moldNbr", txt_MoldNbr.Text.Trim());
        param[2] = new SqlParameter("@detailNbr", txt_DetailNbr.Text.Trim());
        param[3] = new SqlParameter("@status", Convert.ToInt32(ddl_detailsStatus.SelectedValue));
        param[4] = new SqlParameter("@startDate", txt_startDate.Text.Trim());
        param[5] = new SqlParameter("@endDate ", txt_endDate.Text.Trim());
        param[6] = new SqlParameter("@QAD ", txt_QAD.Text.Trim());

        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param).Tables[0];
        string title = "120^<b>供应商编号</b>~^150^<b>模具类编号</b>~^150^<b>模具编号</b>~^<b>开模周期</b>~^<b>状态</b>~^200^<b>产能</b>~^200^<b>生产寿命</b>~^<b>生产类型</b>~^100^<b>材料类型</b>~^120^<b>材料品牌</b>~^<b>启用时间</b>~^<b>数量</b>~^<b>一模几穴</b>~^200^<b>穴编号</b>~^200^<b>备注</b>~^";
        ExportExcel(title, dt, false);


        
    }


   
}