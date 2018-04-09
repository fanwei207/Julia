using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class plan_wo_actualReleasePrint : BasePage
{
    private wo.Wo_ActualRelease helper = new wo.Wo_ActualRelease();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            gvlist.Columns[14].Visible = false;
            txtDateFrom.Text = System.DateTime.Now.AddDays(-6).ToString("yyyy/MM/dd");
            BindData();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    private void BindData()
    {
        string woNbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string relDateFrom = txtDateFrom.Text.Trim();
        string relDateTo = txtDateTo.Text.Trim();
        gvlist.DataSource = helper.GetWoActRelPrintList(woNbr, part, relDateFrom, relDateTo);
        gvlist.DataBind();
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            string woNbr = txtNbr.Text.Trim();
            string part = txtQAD.Text.Trim();
            string relDateFrom = txtDateFrom.Text.Trim();
            string relDateTo = txtDateTo.Text.Trim();
            string strFile = string.Empty;
            SamplePrint.SamplePrintExecl excel = null;
            strFile = "Sample" + "_ATL_Packing" + ".xls";//DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            excel = new SamplePrint.SamplePrintExecl(Server.MapPath("/docs/plan_Sample.xls"), Server.MapPath("../Excel/") + strFile);
            //excel.SamplePrintToExcel("送样单", Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), woNbr, part, relDateFrom, relDateTo);
            //ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
        }
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {

        string woNbr = txtNbr.Text.Trim();
        string part = txtQAD.Text.Trim();
        string relDateFrom = txtDateFrom.Text.Trim();
        string relDateTo = txtDateTo.Text.Trim();
        DataTable dt = helper.GetWoActRelPrintList(woNbr, part, relDateFrom, relDateTo);
        string title = "100^<b>加工单</b>~^100^<b>ID</b>~^120^<b>QAD</b>~^100^<b>QAD下达日期</b>~^100^<b>计划日期</b>~^100^<b>评审日期</b>~^100^<b>上线日期</b>~^100^<b>工单数量</b>~^100^<b>地点</b>~^100^<b>生产线</b>~^100^<b>成本中心</b>~^100^<b>工厂</b>~^100^<b>周期章</b>~^";
        ExportExcel(title, dt, false);
    }

    protected string chkSelect()
    {
        //定义参数
        string strSelect = "";
        int j = 0;

        //判断是否有选择
        for (int i = 0; i < gvlist.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvlist.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gvlist.DataKeys[i].Value.ToString() + ",";
                j += 1;
            }
            if (j > 20)
            {
                strSelect = "20";
                return strSelect;
            }
        }
        return strSelect;
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gvlist.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gvlist.Rows[i].FindControl("chk_Select");
            if (chkAll.Checked)
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
        }
    }
    protected void btn_print_Click(object sender, EventArgs e)
    {
        string strRet = chkSelect();
        string struID = Convert.ToString(Session["uID"]);
        bool Ret = true;
        if (strRet.ToString() == "20")
        {
            ltlAlert.Text = "alert('最多只能选择二十个送样单打印！');";
            return;
        }

        if (strRet.Length != 0)
        {
            //strRet = strRet.Substring(0, strRet.Length - 1);
            //Ret = pack.InsertPrintPackingTemp(strRet, struID);

            if (Ret)
            {
                string woNbr = strRet;// txtNbr.Text.Trim();
                string part = "";//txtQAD.Text.Trim();
                string relDateFrom = "";//txtDateFrom.Text.Trim();
                string relDateTo = "";//txtDateTo.Text.Trim();
                string strFile = string.Empty;
                SamplePrint.SamplePrintExecl excel = null;
                strFile = "Sample" + "_ATL_Packing" + ".xls";//DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                excel = new SamplePrint.SamplePrintExecl(Server.MapPath("/docs/plan_Sample.xls"), Server.MapPath("../Excel/") + strFile);
                excel.SamplePrintToExcel("送样单", Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), strRet, woNbr, part, relDateFrom, relDateTo);
                ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }
            else
            {
                ltlAlert.Text = "alert('选择要打印的单据有问题，请联系管理员！');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('没有选择要打印的单据！');";
        }
    }
}