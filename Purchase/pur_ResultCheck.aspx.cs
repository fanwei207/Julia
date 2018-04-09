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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using System.Text;

public partial class pur_ResultCheck : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    PurResult result = new PurResult();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindVendor();
            BindVDate();
            GridViewBind();
            //this.gv.Columns[1].Visible = false;
            if (this.Security["120000057"].isValid)
            {
                btn_check.Visible = true;
            }
            else
            {
                btn_check.Visible = false;
            }
        }
    }

    private void BindVendor()
    {
        try
        {
            //string strName = "sp_purresult_selectPro";
            //SqlParameter[] param = new SqlParameter[1];
            //param[0] = new SqlParameter("@proid", ddl_checkpro.SelectedValue);

            //DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            //ddl_vendor.DataSource = result.GetVerdorList(ddl_vendor.SelectedValue, "");//ds;
            //ddl_vendor.DataBind();
            //ddl_vendor.Items.Insert(0, "--请选择--");
            //ddl_vendor.SelectedIndex = 0;
        }
        catch
        {
            this.Alert("获取");
        }
    }
    private void BindVDate()
    {
        ddl_year.Items.Clear();
        try
        {
            ddl_year.Items.Insert(0, DateTime.Now.AddYears(-1).Year.ToString());
            ddl_year.Items.Insert(1, DateTime.Now.Year.ToString());
            ddl_year.SelectedIndex = 1;
        }
        catch
        {
            this.Alert("获取");
        }
    }
    private void GridViewBind() 
    {
        string vendor = "";
        string year = "";
        string mounth = "";
        int pass = 0;
        //if (ddl_vendor.SelectedIndex == 0)
        //{
        //    vendor = "";
        //}
        //else
        //{
        //    vendor = ddl_vendor.SelectedValue;
        //}
        vendor = txt_vendor.Text;
        year = ddl_year.Text;
        mounth = ddl_mounth.SelectedValue;
        pass = Convert.ToInt32(ddl_pass.SelectedValue);

        try
        {
            gv.DataSource = result.GetVendorResultReportList(vendor, year, mounth, pass, Convert.ToInt32(Session["uID"].ToString()), Session["uName"].ToString()); ;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    private bool checkNoteIsExist(string type)
    {
        try
        {
            string strName = "sp_vc_checkVendCompCateIsExist";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@name", type);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strName = "sp_vc_deleteVendCompCate";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Values["vcc_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[3].Value))
            {
                this.Alert("删除成功！");
            }
            else
            {
                this.Alert("删除失败！");
            }
        }
        catch
        {
            this.Alert("删除失败！请联系管理员！"); ;
        }

        GridViewBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string _id = gv.DataKeys[e.RowIndex].Values["vcc_id"].ToString();
        TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].FindControl("txDesc");
        if (txDesc.Text.Length == 0)
        {
            this.Alert("罚款科目不能为空！");
            return;
        }

        if (checkNoteIsExist(txDesc.Text))
        {
            ltlAlert.Text = "alert('罚款科目不能重复!');";
            return;
        }

        try
        {
            string strName = "sp_vc_updateVendCompCate";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@id", _id);
            param[1] = new SqlParameter("@name", txDesc.Text);
            param[2] = new SqlParameter("@uID", Session["uID"].ToString());
            param[3] = new SqlParameter("@uName", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[4].Value))
            {
                this.Alert("更新成功！");
            }
            else
            {
                this.Alert("更新失败！");
            }
        }
        catch
        {
            this.Alert("更新失败！请联系管理员！"); ;
        }

        gv.EditIndex = -1;
        GridViewBind();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void ddl_vendor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            string strFile = string.Empty;
            strFile = "SID_Pur_Result_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            PackingExcel.PackingExcel excel = null;
            PurResult purresult = new PurResult();
            excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/pur_ResultReport.xls"), Server.MapPath("../Excel/") + strFile);
            purresult.PurResult1(Server.MapPath("/docs/pur_ResultReport.xls"), Server.MapPath("../Excel/") + strFile);

            //excel.ATLInvoiceToExcelNewByNPOI("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicLogo, PicSeal);
            purresult.ATLInvoiceToExcelNewByNPOI("报关单",txt_vendor.Text, ddl_year.SelectedValue, ddl_mounth.SelectedValue, Convert.ToInt32(ddl_pass.SelectedValue), Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]));

            ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }
        catch
        {
            ;
        }
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;

        foreach (GridViewRow row in gv.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");
            
            chkImport.Checked = chkAll.Checked;
        }
    }
    protected void btn_check_Click(object sender, EventArgs e)
    {
        StringBuilder ids = new StringBuilder();
        foreach (GridViewRow row in gv.Rows)
        {
            //CheckBox chk = row.FindControl("chk") as CheckBox;
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");
            if (chkImport.Checked)
            {
                string vend = gv.DataKeys[row.RowIndex].Values["prh_vend"].ToString();
                string year = gv.DataKeys[row.RowIndex].Values["pur_year"].ToString();
                string mounth = gv.DataKeys[row.RowIndex].Values["pur_mounth"].ToString();
                result.CheckVendPass(vend, year, mounth, true,Session["uID"].ToString());
            }
        }
        GridViewBind();
    }
    protected void btn_return_Click(object sender, EventArgs e)
    {
        StringBuilder ids = new StringBuilder();
        foreach (GridViewRow row in gv.Rows)
        {
            //CheckBox chk = row.FindControl("chk") as CheckBox;
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");
            if (chkImport.Checked)
            {
                string vend = gv.DataKeys[row.RowIndex].Values["prh_vend"].ToString();
                string year = gv.DataKeys[row.RowIndex].Values["pur_year"].ToString();
                string mounth = gv.DataKeys[row.RowIndex].Values["pur_mounth"].ToString();
                result.CheckVendPass(vend, year, mounth, false, Session["uID"].ToString());
            }
        }
        GridViewBind();
    }
}
