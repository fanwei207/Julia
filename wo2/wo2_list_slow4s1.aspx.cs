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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class wo2_list_slow4s1 : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtRelDate1.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
            txtRelDate2.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
            BindData();
        }
    }

    protected void BindData()
    {
        try
        {
            string strSql = "sp_wo2_selectWorkOrderHoursList3";

            SqlParameter[] sqlParam = new SqlParameter[10];
            sqlParam[0] = new SqlParameter("@cc", txtCostCenter.Text.Trim());
            sqlParam[1] = new SqlParameter("@nbr", txtNbr.Text.Trim());
            sqlParam[2] = new SqlParameter("@lot", txtLot.Text.Trim());
            sqlParam[3] = new SqlParameter("@part", txtPart.Text.Trim());
            sqlParam[4] = new SqlParameter("@relDate1", txtRelDate1.Text);
            sqlParam[5] = new SqlParameter("@relDate2", txtRelDate2.Text);
            sqlParam[6] = new SqlParameter("@enterDate1", txtEnterDate1.Text);
            sqlParam[7] = new SqlParameter("@enterDate2", txtEnterDate2.Text);
            sqlParam[8] = new SqlParameter("@repDate1", txtRepDate1.Text);
            sqlParam[9] = new SqlParameter("@repDate2", txtRepDate2.Text);

            DataTable table = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam).Tables[0];

            gvSummary.DataSource = table;
            gvSummary.DataBind();
        }
        catch
        {
            ;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        #region 日期验证
        if (!string.IsNullOrEmpty(txtRelDate1.Text))
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtRelDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('下达日期 格式不正确！');";
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtRelDate2.Text))
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtRelDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('下达日期 格式不正确！');";
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtEnterDate1.Text))
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEnterDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('结算日期 格式不正确！');";
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtEnterDate2.Text))
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtEnterDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('结算日期 格式不正确！');";
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtRepDate1.Text))
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtRepDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期 格式不正确！');";
                return;
            }
        }

        if (!string.IsNullOrEmpty(txtRepDate2.Text))
        {
            try
            {
                DateTime _dt = Convert.ToDateTime(txtRepDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期 格式不正确！');";
                return;
            }
        }

        //下达日期、结算日期、生效日期必须填写一个
        if (string.IsNullOrEmpty(txtRelDate1.Text) && string.IsNullOrEmpty(txtRelDate2.Text)
            && string.IsNullOrEmpty(txtEnterDate1.Text) && string.IsNullOrEmpty(txtEnterDate2.Text)
            && string.IsNullOrEmpty(txtRepDate1.Text) && string.IsNullOrEmpty(txtRepDate2.Text))
        {
            ltlAlert.Text = "alert('三个日期至少要填写一组！');";
            return;
        }
        #endregion

        BindData();
    }
    protected void gvSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSummary.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        //string str = "date1=" + txtRelDate1.Text.Trim() + "&date2=" + txtRelDate2.Text.Trim() + "&part=" + txtPart.Text.Trim() + "&cc=" + txtCostCenter.Text.Trim()
        //                + "&nbr=" + txtNbr.Text.Trim() + "&lot=" + txtLot.Text.Trim() + "&isClosed=" + chkIsClosed.Checked.ToString() + "&isReported=" + chkIsReported.Checked.ToString();
        //ltlAlert.Text = "window.open('wo2_list_slow4sExcel.aspx?" + str + "','_blank')";
    }
    protected void gvSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Details")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            //Response.Redirect("wo2_list_detail4.aspx?ty=slow&s=" + gvSummary.DataKeys[index].Values["wo_site"].ToString() + "&id=" + gvSummary.DataKeys[index].Values["wo_lot"].ToString() + "&pi=" + gvSummary.DataKeys[index].Values["ro_tool"].ToString() + "&cls=" + chkIsClosed.Checked.ToString());
        }
    }
}
