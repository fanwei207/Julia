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

public partial class pcb_equconfirm : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        sqlParam[9] = new SqlParameter("@rmks", string.Empty);

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
            gv.Rows[0].Cells[0].Text = "û������";
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
            if (Convert.ToInt32(lbEqu.Text) > 0)
            {
                e.Row.Cells[10].Text = ((TextBox)e.Row.FindControl("txtRmks")).Text;
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtDate.Text != string.Empty)
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('���ڸ�ʽ����ȷ!');";
                return;
            }
        }

        BindData();
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkItem");

            chk.Checked = ((CheckBox)sender).Checked;
        }
    }
    protected void btnHave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkItem");

            if (chk.Checked)
            {
                try
                {
                    string strSql = "sp_PVK_confirmPcbVersion";

                    SqlParameter[] sqlParam = new SqlParameter[5];
                    sqlParam[0] = new SqlParameter("@id", gv.DataKeys[row.RowIndex].Value.ToString());
                    sqlParam[1] = new SqlParameter("@dept", Convert.ToInt32(lbEqu.Text) > 0 ? "equ" : "ws");
                    sqlParam[2] = new SqlParameter("@confirm", true);
                    sqlParam[3] = new SqlParameter("@rmks", string.Empty);
                    sqlParam[4] = new SqlParameter("@user", Session["uName"].ToString());

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                }
                catch
                {
                    ltlAlert.Text = "alert('ȷ��ʧ��!');";
                }
            }
        }

        BindData();
    }
    protected void btnNone_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(lbEqu.Text) <= 0)
        { //����ȷ��û�еĻ���Ҫ��дԭ���

            foreach (GridViewRow row in gv.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkItem");
                TextBox txtRmks = (TextBox)row.FindControl("txtRmks");

                if (chk.Checked && txtRmks.Text == string.Empty)
                {
                    ltlAlert.Text = "alert('���û�У�����д��ע��д��ԭ��!');";
                    return;
                }
            }
        }

        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chkItem");
            TextBox txtRmks = (TextBox)row.FindControl("txtRmks");

            if (chk.Checked)
            {
                try
                {
                    string strSql = "sp_PVK_confirmPcbVersion";

                    SqlParameter[] sqlParam = new SqlParameter[5];
                    sqlParam[0] = new SqlParameter("@id", gv.DataKeys[row.RowIndex].Value.ToString());
                    sqlParam[1] = new SqlParameter("@dept", Convert.ToInt32(lbEqu.Text) > 0 ? "equ" : "ws");
                    sqlParam[2] = new SqlParameter("@confirm", false);
                    sqlParam[3] = new SqlParameter("@rmks", txtRmks.Text);
                    sqlParam[4] = new SqlParameter("@user", Session["uName"].ToString());

                    SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, sqlParam);
                }
                catch
                {
                    ltlAlert.Text = "alert('ȷ��ʧ��!');";
                }
            }
        }

        BindData();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('pcb_export.aspx?date=" + txtDate.Text + "&model=" + txtModel.Text + "&version=" + txtVersion.Text + "&green=" + txtGreen.Text + "&steel=" + txtSteel.Text + "&copper=" + txtCopper.Text + "&content=" + txtContents.Text + "&equ=" + dropEqu.SelectedValue + "&workshop=" + dropWorkShop.SelectedValue + "&rmks=&t=" + DateTime.Now.ToString() + "', '_blank');";
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindData();
    }
}
