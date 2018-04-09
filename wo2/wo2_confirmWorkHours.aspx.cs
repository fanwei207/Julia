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
using System.ComponentModel;
using adamFuncs;


public partial class wo2_confirmWorkHours : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();

            BindGridView();
        }
    }

    protected void BindGridView()
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@year", txtYear.Text.Trim());
        param[1] = new SqlParameter("@month", txtMonth.Text.Trim());
        param[2] = new SqlParameter("@uID", Session["uID"].ToString());
        param[3] = new SqlParameter("@dept", Session["deptID"].ToString());

        DataTable dt = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_selectConfirmWorkHours", param).Tables[0];

        gv.DataSource = dt;
        gv.DataBind();
    }

    protected void btn_queryClick(object sender, EventArgs e)
    {
        if (txtYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('年 不能为空！')";
            return;
        }

        if (txtMonth.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('月 不能为空！')";
            return;
        }

        try
        {
            DateTime _dt = Convert.ToDateTime(txtYear.Text.Trim() + "-" + txtMonth.Text.Trim() + "-1");
        }
        catch
        {
            ltlAlert.Text = "alert('输入的年或月不正确！')";
            return;
        }

        BindGridView();
    }

    private bool ConfirmWorkHours(string year, string month, string dept, string dept_name, string totalHours, string Wo2All, string wo2Active, string WoAll, string WoActive, string uID, string uName)
    {
        SqlParameter[] param = new SqlParameter[12];
        param[0] = new SqlParameter("@year", year);
        param[1] = new SqlParameter("@month", month);
        param[2] = new SqlParameter("@dept", dept);
        param[3] = new SqlParameter("@dept_name", dept_name);
        param[4] = new SqlParameter("@totalHours", totalHours);
        param[5] = new SqlParameter("@Wo2All", Wo2All);
        param[6] = new SqlParameter("@wo2Active", wo2Active);
        param[7] = new SqlParameter("@WoAll", WoAll);
        param[8] = new SqlParameter("@WoActive", WoActive);
        param[9] = new SqlParameter("@confirmBy", uID);
        param[10] = new SqlParameter("@confirmName", uName);
        param[11] = new SqlParameter("@retValue", DbType.Int32);
        param[11].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteNonQuery(adam.dsnx(), CommandType.StoredProcedure, "sp_wo2_confirmWorkHours", param);

        return Convert.ToBoolean(param[11].Value);
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToInt32(gv.DataKeys[e.Row.RowIndex].Values["whc_id"]) > 0)
            {
                ((LinkButton)e.Row.FindControl("linkConfirm")).Visible = false;
            }
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "confirm")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string dept = gv.DataKeys[index].Values["whc_dept"].ToString();
            string deptName = gv.DataKeys[index].Values["whc_dept_name"].ToString();
            string totalHours = gv.Rows[index].Cells[2].Text.Trim();
            string Wo2All = gv.Rows[index].Cells[3].Text.Trim();
            string wo2Active = gv.Rows[index].Cells[4].Text.Trim();
            string WoAll = gv.Rows[index].Cells[5].Text.Trim();
            string WoActive = gv.Rows[index].Cells[6].Text.Trim();

            if (!ConfirmWorkHours(txtYear.Text.Trim(), txtMonth.Text.Trim(), dept, deptName, totalHours, Wo2All, wo2Active,WoAll,WoActive,  Session["uID"].ToString(), Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('确认失败！请刷新后重新确认一次！')";
                return;
            }

            BindGridView();
        }
    }
}   
