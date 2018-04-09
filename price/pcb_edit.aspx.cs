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
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.IO;

public partial class pcb_edit : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["nbr"] != null)
            {
                txtDate.Text = Request.QueryString["nbr"].ToString();
                txtModel.Text = Request.QueryString["lot"].ToString();

                btnAdd.Visible = false;
                btnExport.Visible = false;
            }

            BindData();
        }
    }
    protected void BindData()
    {
        string strSql = "sp_PVK_selectPcbVersionKeys";

        SqlParameter[] sqlParam = new SqlParameter[10];
        sqlParam[0] = new SqlParameter("@date", txtDate.Text);
        sqlParam[1] = new SqlParameter("@model", txtModel.Text);
        sqlParam[2] = new SqlParameter("@version", txtVersion.Text);
        sqlParam[3] = new SqlParameter("@green", txtGreen.Text);
        sqlParam[4] = new SqlParameter("@steel", txtSteel.Text);
        sqlParam[5] = new SqlParameter("@copper", txtCopper.Text);
        sqlParam[6] = new SqlParameter("@content", txtContents.Text);
        sqlParam[7] = new SqlParameter("@equ", dropEqu.SelectedValue);
        sqlParam[8] = new SqlParameter("@workshop", dropWorkShop.SelectedValue);
        sqlParam[9] = new SqlParameter("@rmks", txtRmks.Text);

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

        if (dt.Rows.Count == 0)
        {
            btnExport.Enabled = false;

            dt.Rows.Add(dt.NewRow());
            this.gv.DataSource = dt;
            this.gv.DataBind();
            int columnCount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv.Rows[0].Cells[0].Text = "没有数据";
            gv.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            btnExport.Enabled = true;

            this.gv.DataSource = dt;
            this.gv.DataBind();
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (btnSearch.Text == "取消")
        {
            lbID.Text = "0";

            txtDate.Text = string.Empty;
            txtModel.Text = string.Empty;
            txtVersion.Text = string.Empty;
            txtGreen.Text = string.Empty;
            txtSteel.Text = string.Empty;
            txtCopper.Text = string.Empty;
            txtContents.Text = string.Empty;
            txtRmks.Text = string.Empty;
            dropEqu.SelectedIndex = -1;
            dropWorkShop.SelectedIndex = -1;
        }
        else
        {
            if (txtDate.Text != string.Empty)
            {
                try
                {
                    DateTime _dt = Convert.ToDateTime(txtDate.Text);
                }
                catch
                {
                    ltlAlert.Text = "alert('日期格式不正确!');";
                    return;
                }
            }
        }

        btnSearch.Text = "查询";
        btnAdd.Text = " 新增";
        txtModel.ReadOnly = false;

        BindData();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtModel.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写线路板名称!');";
            return;
        }

        try
        {
            string strSql = "sp_PVK_insertPcbVersionKeys";

            SqlParameter[] sqlParam = new SqlParameter[8];
            sqlParam[0] = new SqlParameter("@id", lbID.Text);
            sqlParam[1] = new SqlParameter("@model", txtModel.Text);
            sqlParam[2] = new SqlParameter("@version", txtVersion.Text);
            sqlParam[3] = new SqlParameter("@green", txtGreen.Text);
            sqlParam[4] = new SqlParameter("@steel", txtSteel.Text);
            sqlParam[5] = new SqlParameter("@copper", txtCopper.Text);
            sqlParam[6] = new SqlParameter("@content", txtContents.Text);
            sqlParam[7] = new SqlParameter("@uName", Session["uName"]);

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            ltlAlert.Text = "alert('增加失败!');";
        }

        btnAdd.Text = "新增";
        btnSearch.Text = "查询";

        lbID.Text = "0";
        txtModel.ReadOnly = false;

        BindData();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            btnAdd.Text = "保存";
            btnSearch.Text = "取消";

            int index = int.Parse(e.CommandArgument.ToString());
            lbID.Text = gv.DataKeys[index].Value.ToString();

            txtDate.Text = gv.Rows[index].Cells[0].Text == "&nbsp;" ? string.Empty : gv.Rows[index].Cells[0].Text;
            txtModel.Text = gv.Rows[index].Cells[1].Text == "&nbsp;" ? string.Empty : gv.Rows[index].Cells[1].Text;
            txtVersion.Text = gv.Rows[index].Cells[2].Text == "&nbsp;" ? string.Empty : gv.Rows[index].Cells[2].Text;
            txtGreen.Text = gv.Rows[index].Cells[3].Text == "&nbsp;" ? string.Empty : gv.Rows[index].Cells[3].Text;
            txtSteel.Text = gv.Rows[index].Cells[4].Text == "&nbsp;" ? string.Empty : gv.Rows[index].Cells[4].Text;
            txtCopper.Text = gv.Rows[index].Cells[5].Text == "&nbsp;" ? string.Empty : gv.Rows[index].Cells[5].Text;
            txtContents.Text = gv.Rows[index].Cells[6].Text == "&nbsp;" ? string.Empty : gv.Rows[index].Cells[6].Text;

            txtModel.ReadOnly = true;
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strSql = "sp_PVK_deletePcbVersionKeys";

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Value.ToString());

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch
        {
            ltlAlert.Text = "alert('删除失败!');";
        }

        BindData();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('pcb_export.aspx?date=" + txtDate.Text + "&model=" + txtModel.Text + "&version=" + txtVersion.Text + "&green=" + txtGreen.Text + "&steel=" + txtSteel.Text + "&copper=" + txtCopper.Text + "&content=" + txtContents.Text + "&equ=" + dropEqu.SelectedValue + "&workshop=" + dropWorkShop.SelectedValue + "&rmks=" + txtRmks.Text + "&t=" + DateTime.Now.ToString() + "', '_blank');";
    }
}
