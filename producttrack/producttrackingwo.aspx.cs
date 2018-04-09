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


public partial class producttrackingwo : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProductTrackingType();
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
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@domain", dropDomain.SelectedValue);
            param[1] = new SqlParameter("@type", dropTackingType.SelectedValue);
            param[2] = new SqlParameter("@part", txtQad.Text.Trim());
            param[3] = new SqlParameter("@nbr1", txtNbr1.Text.Trim());
            param[4] = new SqlParameter("@nbr2", txtNbr2.Text.Trim());
            param[5] = new SqlParameter("@dueDate1", txtDueDate1.Text.Trim());
            param[6] = new SqlParameter("@dueDate2", txtDueDate2.Text.Trim());

            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, "sp_selectProductTrackingWO", param);

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

            gv.Width = Unit.Pixel(80 * (ds.Tables[0].Columns.Count - 4) + 610);

            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        { }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDueDate1.Text))
        {
            if (!this.IsDate(txtDueDate1.Text))
            {
                ltlAlert.Text = "alert('截至日期 的格式不正确!')";
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtDueDate2.Text))
        {
            if (!this.IsDate(txtDueDate2.Text))
            {
                ltlAlert.Text = "alert('截至日期 的格式不正确!')";
                return;
            }
        }

        BindGridView();
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;

            for (int i = 12; i < e.Row.Cells.Count; i++)
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

            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;

            for (int i = 12; i < e.Row.Cells.Count; i++)
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
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Link")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            this.Redirect("/QadDoc/qad_bomviewdoc.aspx?cmd=wo&part=" + gv.DataKeys[index].Values["product"].ToString());
        }
    }
}
