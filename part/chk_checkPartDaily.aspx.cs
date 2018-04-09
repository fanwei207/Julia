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
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

public partial class part_chk_checkPartDaily : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txbCheckedDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txbCheckedDateEnd.Text = DateTime.Today.ToString("yyyy-MM-dd");
            BindLocs();
            BindData();  
        }
    }

    protected void BindData()
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@loc", dropLocs.SelectedValue.ToString() == "0" ? string.Empty : dropLocs.SelectedValue);
        param[1] = new SqlParameter("@checkedDate", txbCheckedDate.Text.Trim());
        param[2] = new SqlParameter("@checkedDateEnd", txbCheckedDateEnd.Text.Trim());
        DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_chk_selectPartDaily", param);
        gvCheckedPart.DataSource = ds.Tables[0];
        gvCheckedPart.DataBind();

        lblCount.Text = ds.Tables[0].Rows.Count.ToString();
        lblcheckedName.Text = ds.Tables[1].Rows[0]["checkedName"].ToString();
        lblkeepedName.Text = ds.Tables[1].Rows[0]["keepedName"].ToString();

        ds.Dispose();
    }

    protected void BindLocs()
    {
        dropLocs.Items.Clear();
        dropLocs.Items.Add(new ListItem("--请选择库位--", "0"));
        SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.StoredProcedure, "sp_chk_selectLocs");
        while (reader.Read())
        {
            dropLocs.Items.Add(new ListItem(reader["locName"].ToString(), reader["loc"].ToString()));
        }
        if (reader != null)
        {
            reader.Dispose();
        }
    }

    protected void gvCheckedPart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[5].Text != "&nbsp;" && e.Row.Cells[4].Text != e.Row.Cells[5].Text)
            {
                e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void gvCheckedPart_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCheckedPart.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (lblCount.Text == "0")
        {
            ltlAlert.Text = "alert('没有要导出的内容！')";
        }
        else
        {
            string strLoc = dropLocs.SelectedValue.ToString() == "0" ? string.Empty : dropLocs.SelectedValue;
            ltlAlert.Text = "window.open('/part/chk_exportPartDaily.aspx?loc=" + strLoc + "&checkedDate=" + txbCheckedDate.Text.Trim() + "&checkedDateend=" + txbCheckedDateEnd.Text.Trim() + "&checkedName=" + lblcheckedName.Text.Trim() + "&keepedName=" + lblkeepedName.Text.Trim() + "&rm=" + DateTime.Now.ToString() + "', '_blank');";
        }
    }
}
