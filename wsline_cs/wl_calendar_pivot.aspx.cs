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


public partial class wl_calendar : BasePage
{
    adamClass chk = new adamClass();
    DataSet ds;
    WSExcelHelper.WSExcelHelper excel = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            txb_year.Text = DateTime.Now.Year.ToString();
            txb_month.Text = DateTime.Now.Month.ToString();
            dropDepartmentBind();
            bindData();
        } 
    }

    private void bindData()
    {
        DataTable dt = GetWlCalendar(Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(ddl_dp.SelectedValue), txb_userno.Text.Trim(), Convert.ToInt32(ddl_type.SelectedValue), Convert.ToInt32(txb_year.Text.Trim()), Convert.ToInt32(txb_month.Text.Trim()), Convert.ToInt32(Session["uID"].ToString()));
        DynamicBindgvpv(dt, gv_hac);
    }

    private void dropDepartmentBind()
    {
        DataTable dt = GetDepartment();
        this.ddl_dp.DataSource = dt;
        this.ddl_dp.DataBind();
    }

    private void DynamicBindgvpv(DataTable dt, GridView gv)
    {
        gv.Columns.Clear();

        for (int i = 0; i < dt.Columns.Count; i++)
        {
            TemplateField tf = new TemplateField();
            tf.HeaderText = dt.Columns[i].Caption.ToString();
            tf.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("white");
            if (i < 2)
            {
                tf.ItemStyle.Width = Unit.Pixel(100);
                tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
            }
            else
            {
                tf.ItemStyle.Width = Unit.Pixel(100);
                tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                tf.EditItemTemplate = new GridViewTemplate("TextBox", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
            }
            tf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gv.Columns.Add(tf);
        }

        gv.DataSource = dt;
        gv.DataBind();
    }

    private DataTable GetDepartment()
    {
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectDepartment").Tables[0];
    }

    private DataTable GetWlCalendar(int plantCode, int departmentID, string loginName, int staffType, int uYear, int uMonth, int createdBy)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@plantCode", plantCode);
        param[1] = new SqlParameter("@departmentID", departmentID);
        param[2] = new SqlParameter("@loginName", loginName);
        param[3] = new SqlParameter("@staffType", staffType);
        param[4] = new SqlParameter("@uYear", uYear);
        param[5] = new SqlParameter("@uMonth", uMonth);
        param[6] = new SqlParameter("@createdBy", createdBy);
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_selectAttendanceCalendar", param).Tables[0];
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txb_year.Text == string.Empty || txb_month.Text == string.Empty)
        {
            ltlAlert.Text = "alert('年、月不能为空!')";
        }
        else
        {
            int _month = Convert.ToInt32(txb_month.Text.Trim());
            if (_month > 12 || _month < 1)
            {
                ltlAlert.Text = "alert('月份必须是1-12之间的数字！');";
            }
            else
            {
                bindData();
            }
        } 
    }

    protected void gv_pv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp部门&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            gv_hac.Columns[1].Visible = false;
            e.Row.Cells[2].Text = "工号";
            e.Row.Cells[3].Text = "&nbsp;&nbsp;&nbsp;&nbsp姓名&nbsp;&nbsp;&nbsp;&nbsp";
            e.Row.Cells[4].Text = "&nbsp;&nbsp入司日期&nbsp;&nbsp;&nbsp";
            e.Row.Cells[5].Text = "&nbsp;&nbsp离职日期&nbsp;&nbsp;&nbsp";
            gv_hac.Columns[6].Visible = false;

            int y = gv_hac.Columns.Count - 4;
            e.Row.Cells[y].Text = "小计";

            int i = gv_hac.Columns.Count - 3;
            e.Row.Cells[i].Text = "人员类型";

            int j = gv_hac.Columns.Count - 2;
            e.Row.Cells[j].Text = "计酬方式";

            int z = gv_hac.Columns.Count - 1;
            e.Row.Cells[z].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp工段&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";

            foreach (TableCell cell in e.Row.Cells)
            {
                cell.Attributes.Add("class","text");
            } 
        }
      
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
       ltlAlert.Text = "window.open('wl_calendar_pivot_excel.aspx?pc=" + Session["PlantCode"].ToString() + "&dp=" + ddl_dp.SelectedValue.ToString() + "&un=" + txb_userno.Text.Trim()
                            + "&tp=" + ddl_type.SelectedValue.ToString() + "&ye=" + txb_year.Text.Trim() + "&mo=" + txb_month.Text.Trim() + "&cb=" + Session["uID"].ToString() + "', '_blank');";

       bindData();
    }
}
