using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class wo2_wo2_mop_rate : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["sop_orderby"] = "wo2_mop_proc";
        Session["sop_direction"] = "ASC";
        if (!IsPostBack)
        {
            txtDate.Text = DateTime.Now.Year.ToString()+'-'+DateTime.Now.Month.ToString() + '-'+'1';
            DataGridBind();
        }
        
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        if (txtProcCode.Text.Trim() != String.Empty)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{1,}$");
            if (!regex.IsMatch(txtProcCode.Text.Trim()))
            {
                ltlAlert.Text = "alert('工序代码必须是一串数字序列');";
                txtProcCode.Text = String.Empty;
            }
        }

        DataGridBind();
    }
    //通过工序代码和工序名找——修改
    private void DataGridBind()
    {
        try
        {
            string strSql = "sp_wo2_selectSopProcByMop";

            int year = Convert.ToDateTime(txtDate.Text.Trim()).Year;
            int month = Convert.ToDateTime(txtDate.Text.Trim()).Month;

            SqlParameter[] parmArray = new SqlParameter[4];
            parmArray[0] = new SqlParameter("@proccode", txtProcCode.Text.Trim());
            parmArray[1] = new SqlParameter("@procname", txtProcName.Text.Trim());
            parmArray[2] = new SqlParameter("@year", year);
            parmArray[3] = new SqlParameter("@month", month);


            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);
            dgSopProc.DataSource = ds;
            dgSopProc.DataBind();

            ds.Dispose();
        }
        catch
        {
            ;
        }
    }
    protected void dgSopProc_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgSopProc.CurrentPageIndex = e.NewPageIndex;

        DataGridBind();
    }
     protected void dgSopProc_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        Session["sop_orderby"] = e.SortExpression;

        if (Session["sop_direction"] == "ASC")
            Session["sop_direction"] = "DESC";
        else
            Session["sop_direction"] = "ASC";

        DataGridBind();
    }

    protected void dgSopProc_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgSopProc.EditItemIndex = e.Item.ItemIndex;

        DataGridBind();
    }

    protected void dgSopProc_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgSopProc.EditItemIndex = -1;

        DataGridBind();
    }
      protected void dgSopProc_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        TextBox tRate30 = (TextBox)e.Item.Cells[4].FindControl("txtRate30");
        TextBox tRate60 = (TextBox)e.Item.Cells[5].FindControl("txtRate60");
        TextBox tRate90 = (TextBox)e.Item.Cells[6].FindControl("txtRate90");
        if (tRate30.Text.Length < 0 || tRate30.Text.Length == 0 || tRate30.Text.Length < 0 || tRate30.Text.Length == 0 || tRate30.Text.Length < 0 || tRate30.Text.Length == 0)
          {
              ltlAlert.Text = "alert('3个补贴率都要填，并且只能是数字哦!');";
              return;
          }

            try
            {
                Decimal d=Convert.ToDecimal(tRate30.Text.Trim());
                if (d < 0)
                {
                    ltlAlert.Text = "alert('30天补贴率必须大于0哦!');";
                    return;
                }
            }
            catch 
            {
                ltlAlert.Text = "alert('30天补贴率可以是小数或整数，只能是数字哦!');";
                return;
            }
            try
            {
                Decimal d = Convert.ToDecimal(tRate60.Text.Trim());
                if (d < 0)
                {
                    ltlAlert.Text = "alert('60天补贴率必须大于0哦!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('60天补贴率可以是小数或整数，只能是数字哦!');";
                return;
            }
            try
            {
                Decimal d = Convert.ToDecimal(tRate90.Text.Trim());
                if (d < 0)
                {
                    ltlAlert.Text = "alert('90天补贴率必须大于0哦!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('90天补贴率可以是小数或整数，只能是数字哦!');";
                return;
            }
        

        try
        {
            string strSql = "sp_wo2_updateSopProcRate";


            SqlParameter[] parmArray = new SqlParameter[9];
            parmArray[0] = new SqlParameter("@wo2_sop_proc", Convert.ToInt32(e.Item.Cells[2].Text.Trim()));
            parmArray[1] = new SqlParameter("@uID", Session["uID"].ToString().Trim());
            parmArray[2] = new SqlParameter("@rate30", Convert.ToDecimal(tRate30.Text.Trim()));
            parmArray[3] = new SqlParameter("@rate60", Convert.ToDecimal(tRate60.Text.Trim()));
            parmArray[4] = new SqlParameter("@rate90", Convert.ToDecimal(tRate90.Text.Trim()));
            parmArray[5] = new SqlParameter("@date",Convert.ToDateTime(txtDate.Text.Trim()));
            parmArray[6] = new SqlParameter("@wo2_sop_procname", e.Item.Cells[3].Text.Trim());
            parmArray[7] = new SqlParameter("@wo2_mop_proc", Convert.ToInt32(e.Item.Cells[0].Text.Trim()));
            parmArray[8] = new SqlParameter("@wo2_mop_procname", e.Item.Cells[1].Text.Trim());

            int nRet = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSql, parmArray);

            dgSopProc.EditItemIndex = -1;

            DataGridBind();
        }
        catch
        {
            ;
        }
    }
}