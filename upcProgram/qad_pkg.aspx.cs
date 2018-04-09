using System;
using System.Data;
using System.Web.UI.WebControls;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class qad_pkg : BasePage
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindData();
        } 
    }

    private void bindData()
    {
        DataTable dt = GetQadPkg(chk.sqlEncode(txtPar.Text.Trim()), chk.sqlEncode(txtParCode.Text.Trim()), chk.sqlEncode(txtParItemNumber.Text.Trim())
                                 ,chk.sqlEncode(txtComp.Text.Trim()), chk.sqlEncode(txtCompCode.Text.Trim()));

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.gv_qad_pkg.DataSource = dt;
            this.gv_qad_pkg.DataBind();
            int ColunmCount = gv_qad_pkg.Rows[0].Cells.Count;
            gv_qad_pkg.Rows[0].Cells.Clear();
            gv_qad_pkg.Rows[0].Cells.Add(new TableCell());
            gv_qad_pkg.Rows[0].Cells[0].ColumnSpan = ColunmCount;
            gv_qad_pkg.Rows[0].Cells[0].Text = "No Data";
            gv_qad_pkg.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            this.gv_qad_pkg.DataSource = dt;
            this.gv_qad_pkg.DataBind();
        }
    }

    private DataTable GetQadPkg(string par, string par_code, string par_itemNumber, string comp, string comp_code)
    {
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@par", par);
        param[1] = new SqlParameter("@par_code", par_code);
        param[2] = new SqlParameter("@par_itemNumber", par_itemNumber);
        param[3] = new SqlParameter("@comp", comp);
        param[4] = new SqlParameter("@comp_code", comp_code);
        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_selectQadPkg", param).Tables[0];
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
         bindData();
    }

    protected void gv_qad_pkg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_qad_pkg.PageIndex = e.NewPageIndex;
        bindData();
    }
}
