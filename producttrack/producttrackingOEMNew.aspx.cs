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
using QCProgress;
using CommClass;
using System.IO;

public partial class producttrack_producttrackingOEMNew : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBomDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

           
        }
    }

    protected override void BindGridView()
    {
        if (txtBOM.Text.Trim().Length == 0)
        {
            return;
        }

        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@bom", txtBOM.Text.Trim().Replace("*", "%"));
            param[1] = new SqlParameter("@date", txtBomDate.Text.Trim());

            DataTable table = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, "sp_selectProductTrackingNewOEM", param).Tables[0];

            gv.DataSource = table;
            gv.DataBind();
        }
        catch (Exception e)
        {
            this.Alert("BOM不存在！");
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txtBOM.Text.Length == 0)
        {
            ltlAlert.Text = "alert('BOM 不能为空!且长度是14位！')";
            return;
        }

        if (txtBomDate.Text.Length == 0)
        {
            txtBomDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
        }
        else
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtBomDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('BOM日期 的格式不正确!')";
                return;
            }
        }

        BindGridView();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    e.Row.Cells[3].Visible = false;
        //    e.Row.Cells[4].Visible = false;
        //    e.Row.Cells[5].Visible = false;

        //    for (int i = 6; i < e.Row.Cells.Count; i++)
        //    {
        //        e.Row.Cells[i].Width = Unit.Pixel(80);

        //        e.Row.Cells[i].Text = e.Row.Cells[i].Text.Substring(1 + e.Row.Cells[i].Text.IndexOf("="));
        //    }
        //}
        //else if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    if (e.Row.Cells[0].Text == "--")
        //    {
        //        e.Row.Cells[0].Text = string.Empty;
        //    }

        //    e.Row.Cells[3].Visible = false;
        //    e.Row.Cells[4].Visible = false;
        //    e.Row.Cells[5].Visible = false;

        //    for (int i = 6; i < e.Row.Cells.Count; i++)
        //    {
        //        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
        //        string amount = e.Row.Cells[i].Text;
        //        int a = Convert.ToInt32(amount.Substring(0, amount.IndexOf("/")));
        //        int b = Convert.ToInt32(amount.Substring(amount.IndexOf("/") + 1));
        //        if (a < b)
        //        {
        //            e.Row.Cells[i].Attributes.Add("style", "background-color:#FF6666");
        //        }
        //    }
        //}
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Link")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            this.Redirect("/QadDoc/qad_bomviewdoc.aspx?cmd=newtrackOEM&part=" + gv.DataKeys[index].Values["product"].ToString());
        }
    }
   
    protected void btnExportError_Click(object sender, EventArgs e)
    {
        string ExtTitle = "100^<b>部件号</b>~^100^<b>QAD</b>~^100^<b>OEM规格书QAD</b>~^100^<b>OEM检验标准</b>~^100^<b>丝印</b>~^100^<b>包装工艺</b>~^";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@bom", txtBOM.Text.Trim().Replace("*", "%"));
        param[1] = new SqlParameter("@date", txtBomDate.Text.Trim());

        DataTable table = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, "sp_selectProductTrackingNewOEM", param).Tables[0];


        this.ExportExcel( ExtTitle
            , table
            , false);
    }
}
