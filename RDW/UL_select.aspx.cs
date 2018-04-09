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
using RD_WorkFlow;
using System.Data.SqlClient;

public partial class RDW_UL_select : BasePage
{
    RDW rdw = new RDW();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            BindData();
        }
    }
    protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRDW.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        //string strID = Convert.ToString(Request.QueryString["mid"]);
        //if (rdw.insertUL(strID, Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"])))
        //{
        BindData();
        //}
        //else
        //{
        //    ltlAlert.Text = "alert('新建UL失败');";
        //}
    }
    public void BindData()
    {
        gvRDW.DataSource = rdw.selectULView(txtProject.Text, txtDate1.Text, txtDate2.Text, txtModel.Text);
        gvRDW.DataBind();
    }
    protected void btnEXCEL_Click(object sender, EventArgs e)
    {
        DataTable dt = rdw.selectULTemp(txtProject.Text, txtDate1.Text, txtDate2.Text, txtModel.Text);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("无所查询数据！");
            return;
        }

        string title = "300^<b>Product</b>~^110^<b>E号</b>~^70^<b>Section</b>~^120^<b>Driver JXL</b>~^120^<b>LED JXL</b>~^120^<b>Driver Lv</b>~^120^<b>LED Lv</b>~^200^<b>文档中查到的对应图片号</b>~^100^<b>NOTE</b>~^120^<b>QAD</b>~^";
        this.ExportExcel(title, dt, false);
    }
}