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
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
public partial class EDI_FIFOshowAllDet : BasePage
{   
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }
    protected void BindGridView()
    {
        try
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@CustomerPO", txtSOPO.Text.Trim());
        
            parm[1] = new SqlParameter("@Type", ddltype.SelectedValue.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_FIFO_selectFIFOALLDet", parm);
            gvlist.DataSource = ds;
            gvlist.DataBind();
        }
        catch (Exception ee)
        { ;}
    }
    protected void gvlist_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }   

    protected void btnquery_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void btnimport_Click(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            SqlParameter[] parm = new SqlParameter[2];
            parm[0] = new SqlParameter("@CustomerPO", txtSOPO.Text.Trim());
            parm[1] = new SqlParameter("@Type", ddltype.SelectedValue.ToString());
            DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_FIFO_selectFIFOALLDet", parm);
            dt = ds.Tables[0];
            string title = "<b>产品</b>~^<b>零件</b>~^<b>描述</b>~^<b>单位</b>~^<b>用量</b>~^200^<b>损耗率</b>~^200^<b>实用量</b>~^200^<b>标准本层</b>~^200^<b>标准低层</b>~^<b>GL单价</b>~^<b>GL金额</b>~^<b>当前本层</b>~^<b>当前低层</b>~^<b>CU单价</b>~^<b>CU金额</b>~^<b>差异</b>~^<b>层次</b>~^<b>采购价日期</b>~^<b>供应商</b>~^<b>域</b>~^<b>发放原则</b>~^<b>组件类型</b>~^<b>汇率</b>~^<b>工时</b>~^<b>工费(委外)</b>~^<b>工单</b>~^<b>下达日期</b>~^";
            this.ExportExcel(title, dt, true);
        }
        catch (Exception ee)
        { ;}
    }
}