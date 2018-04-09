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


public partial class producttracking : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProductTrackingType();

            txtBomDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            btnUpdate.Attributes.Add("click", "return confirm('更新可能会花费较长时间，请确认是否继续？');");
        } 
    }

    protected void BindProductTrackingType()
    {
        string strSql = " Select Distinct ptt_type From qaddoc.dbo.ProductTrackingType ";

        DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.Text, strSql);

        dropTackingType.DataSource = ds;
        dropTackingType.DataBind();
    }

    protected override void BindGridView()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@domain", dropDomain.SelectedValue);
            param[1] = new SqlParameter("@type", dropTackingType.SelectedValue);
            param[2] = new SqlParameter("@bom", txtBOM.Text.Trim());
            param[3] = new SqlParameter("@date", txtBomDate.Text.Trim());
            param[4] = new SqlParameter("@uID", Session["uID"].ToString());

            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, "sp_selectProductTracking", param);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                for (int col = 0; col < ds.Tables[0].Columns.Count; col++)
                {
                    if (ds.Tables[0].Columns[col].ColumnName == "尺寸")
                    {
                        if (Convert.ToInt32(row[col]) >= 0 && (Convert.ToDecimal(row["box_weight"]) > 0 || Convert.ToDecimal(row["box_size"]) > 0))
                        {
                            row[col] = "99999";
                        }
                    }
                }
            }

            gv.Width = Unit.Pixel(80 * (ds.Tables[0].Columns.Count - 4) + 450);

            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        { }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txtBOM.Text.Length == 0)
        {
            ltlAlert.Text = "alert('BOM 不能为空!可用*号做匹配来查询多个!')";
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
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;

            for (int i = 8; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Width = Unit.Pixel(80);
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
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;

            for (int i = 8; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;

                if (Convert.ToInt32(e.Row.Cells[i].Text) == 0)
                {
                    e.Row.Cells[i].Text = string.Empty;
                }
                else if (Convert.ToInt32(e.Row.Cells[i].Text) < 0)
                {
                    e.Row.Cells[i].Text = "--";
                }
                else if (Convert.ToInt32(e.Row.Cells[i].Text) == 99999)
                {
                    e.Row.Cells[i].Text = "有";
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

            this.Redirect("/QadDoc/qad_bomviewdoc.aspx?cmd=track&part=" + gv.DataKeys[index].Values["product"].ToString());
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
            param[0] = new SqlParameter("@domain", dropDomain.SelectedValue);
            param[1] = new SqlParameter("@bom", txtBOM.Text.Trim());

            SqlHelper.ExecuteNonQuery(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, "sp_jobProductDocTracking", param);

            BindGridView();
        }
        catch
        { }
    }
    protected void btnExportError_Click(object sender, EventArgs e)
    {
        string ExtTitle = "100^<b>整灯QAD</b>~^200^<b>整灯部件号</b>~^100^<b>子键QAD</b>~^200^<b>子键部件号</b>~^100^<b>实际文档数</b>~^100^<b>需要文档数</b>~^";
        string ExtSQL = @"select distinct product, p.code, ps_comp
                            , itm.code 
                            , Isnull(cnt_docs, 0)
                            , Isnull(itm.isdoc, 0)
                          from qaddoc..ProductTracking p
                          left join tcpc0..Items itm on itm.item_qad = p.ps_comp
                          where isnull(itm.isdoc,0) > 0
                            and Isnull(cnt_docs, 0) < isnull(itm.isdoc,0)
                            and ptt_type = '" + dropTackingType.SelectedItem.Text + "'";

        this.ExportExcel(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"]
            , ExtTitle
            , ExtSQL
            , false);
    }
}
