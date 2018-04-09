using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using Wage;

public partial class wo2_wo2_UserWorkHoursDisplay : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStart.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(1 - DateTime.Now.Day));
            txtEnd.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CheckDateFormat();
        gvUserBind();
    }

    protected void BtnExcel_Click(object sender, EventArgs e)
    {

    }

    protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUsers.PageIndex = e.NewPageIndex;
        gvUserBind();
    }

    private void CheckDateFormat()
    {
        if (txtUserNo.Text.Length == 0)
        {
            ltlAlert.Text = "alert(' 请输入工号！ ')";
            return;
        }
        
        try
        {
            DateTime timeStart = Convert.ToDateTime(txtStart.Text.ToString().Trim());
            DateTime timeEnd = Convert.ToDateTime(txtEnd.Text.ToString().Trim());
        }
        catch
        {
            ltlAlert.Text = "alert(' 日期格式不对，请确认！ ')";
            return;
        }
    }

    private DataTable GetUserWorkHours(string strSdate, string strEdate, int intPlant, string strNbr, string strOrder, bool blflag, string strUserNo)
    {
        try
        {
            string str = "sp_hr_selectUserWorkHours";
            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@starttime", Convert.ToDateTime(strSdate));
            sqlParam[1] = new SqlParameter("@endtime", Convert.ToDateTime(strEdate).AddDays(1));
            sqlParam[2] = new SqlParameter("@PlantCode", intPlant);
            sqlParam[3] = new SqlParameter("@WorkOrder", strNbr);
            sqlParam[4] = new SqlParameter("@OrderID", strOrder);
            sqlParam[5] = new SqlParameter("@flag", blflag);
            sqlParam[6] = new SqlParameter("@UserNo", strUserNo);

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, sqlParam).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    private void gvUserBind()
    {
        try
        {
            DataTable dtUser = GetUserWorkHours(txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtWoNbr.Text, txtWoLot.Text, chkClose.Checked, txtUserNo.Text);
            if (dtUser.Rows.Count > 0)
            {
                lblSum.Text = string.Format("{0:F2}", Convert.ToDouble(dtUser.Compute("sum(wo2_workhours)", "true")));
            }
            gvUsers.DataSource = dtUser;
            gvUsers.DataBind();

            dtUser.Clear();
        }
        catch
        {

        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        //Response.Redirect("wo2_UserWorkHoursDisplayExcel.aspx?start=" + txtStart.Text.Trim() + "&end=" + txtEnd.Text.Trim() + "&uno=" + txtUserNo.Text.Trim() 
        //    + "&nbr=" + txtWoNbr.Text.Trim() + "&lot=" + txtWoLot.Text.Trim() + "&clo=" + chkClose.Checked, true);

        ltlAlert.Text = "window.open('wo2_UserWorkHoursDisplayExcel.aspx?start=" + txtStart.Text.Trim() + "&end=" + txtEnd.Text.Trim() + "&uno=" + txtUserNo.Text.Trim() 
            + "&nbr=" + txtWoNbr.Text.Trim() + "&lot=" + txtWoLot.Text.Trim() + "&clo=" + chkClose.Checked + "','_blank')";
    }
}