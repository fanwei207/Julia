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


public partial class producttrackingNew : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBomDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            btnUpdate.Attributes.Add("click", "return confirm('更新可能会花费较长时间，请确认是否继续？');");
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
            param[0] = new SqlParameter("@bom", txtBOM.Text.Trim());
            param[1] = new SqlParameter("@date", txtBomDate.Text.Trim());

            DataTable table = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, "sp_selectProductTrackingNew", param).Tables[0];

            gv.DataSource = table;
            gv.DataBind();
        }
        catch(Exception e)
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
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;

            for (int i = 6; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Width = Unit.Pixel(80);

                e.Row.Cells[i].Text = e.Row.Cells[i].Text.Substring(1 + e.Row.Cells[i].Text.IndexOf("="));
            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "--")
            {
                e.Row.Cells[0].Text = string.Empty;
            }

            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;

            for (int i = 6; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                string amount = e.Row.Cells[i].Text;
                int a = Convert.ToInt32(amount.Substring(0, amount.IndexOf("/")));
                int b = Convert.ToInt32(amount.Substring(amount.IndexOf("/") + 1));
                if (a < b)
                {
                    e.Row.Cells[i].Attributes.Add("style", "background-color:#FF6666");
                }
            }
        }
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

            this.Redirect("/QadDoc/qad_bomviewdoc.aspx?cmd=newtrack&part=" + gv.DataKeys[index].Values["product"].ToString());
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtBOM.Text.Trim())) 
        {
            this.Alert("请填写 BOM！可以使用*号匹配多项！");
            return;
        }

        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@domain", "SZX");
            param[1] = new SqlParameter("@bom", txtBOM.Text.Trim());

            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, "sp_jobProductDocTrackingNew", param);

            BindGridView();
        }
        catch
        { }
    }
    protected void btnExportError_Click(object sender, EventArgs e)
    {
        string ExtTitle = "100^<b>整灯QAD</b>~^200^<b>整灯部件号</b>~^100^<b>子键QAD</b>~^200^<b>子键部件号</b>~^100^<b>类别</b>~^100^<b>实际文档数</b>~^100^<b>需要文档数</b>~^";
        string ExtSQL = @"select distinct product, pro_code, ps_comp
                            , ps_code, ptt_detail
                            , Isnull(cnt_fact, 0)
                            , Isnull(cnt_need, 0)
                          from qaddoc..ProductTrackingNew p
                          where isnull(cnt_need, 0) > 0
                            and Isnull(cnt_fact, 0) < isnull(cnt_need, 0)";

        this.ExportExcel(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"]
            , ExtTitle
            , ExtSQL
            , false);
    }
}
