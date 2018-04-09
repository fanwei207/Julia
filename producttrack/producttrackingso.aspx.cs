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


public partial class producttrackingso : BasePage
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

            DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"], CommandType.StoredProcedure, "sp_selectProductTrackingSO", param);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                for (int col = 0; col < ds.Tables[0].Columns.Count; col++)
                {
                    if (ds.Tables[0].Columns[col].ColumnName == "�ߴ�")
                    {
                        if (Convert.ToInt32(row[col]) >= 0 && (Convert.ToDecimal(row["box_weight"]) > 0 || Convert.ToDecimal(row["box_size"]) > 0))
                        {
                            row[col] = "99999";
                        }
                    }
                }
            }

            gv.Width = Unit.Pixel(80 * (ds.Tables[0].Columns.Count - 4) + 580);

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
                ltlAlert.Text = "alert('�������� �ĸ�ʽ����ȷ!')";
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtDueDate2.Text))
        {
            if (!this.IsDate(txtDueDate2.Text))
            {
                ltlAlert.Text = "alert('�������� �ĸ�ʽ����ȷ!')";
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
                    e.Row.Cells[i].Text = "��";
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

            this.Redirect("/QadDoc/qad_bomviewdoc.aspx?cmd=so&part=" + gv.DataKeys[index].Values["product"].ToString());
        }
    }
    protected void btnExportError_Click(object sender, EventArgs e)
    {
        string ExtTitle = "100^<b>����QAD</b>~^200^<b>���Ʋ�����</b>~^100^<b>�Ӽ�QAD</b>~^200^<b>�Ӽ�������</b>~^100^<b>ʵ���ĵ���</b>~^100^<b>��Ҫ�ĵ���</b>~^";
        string ExtSQL = @"select distinct product, p.code, ps_comp
                            , itm.code 
                            , Isnull(cnt_docs, 0)
                            , Isnull(itm.isdoc, 0)
                          from qaddoc..ProductTracking p
                          left join tcpc0..Items itm on itm.item_qad = p.ps_comp
                          where exists(select * from qad_data..sod_det where sod_domain in ('szx', 'zqz') and sod_part = product)
                            And Isnull(cnt_docs, 0) < isnull(itm.isdoc,0)
                            And isnull(itm.isdoc, 0) > 0";
        ExtSQL += " and ptt_type = '" + dropTackingType.SelectedItem.Text + "'";

        this.ExportExcel(ConfigurationManager.AppSettings["SqlConn.Conn_qaddoc"]
            , ExtTitle
            , ExtSQL
            , false);
    }
}
