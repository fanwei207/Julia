using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;

public partial class wsline_cs_wl_postAttendance : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDepartments();
            BindWorkShop();
            BindDate();
            dropYears.SelectedValue = DateTime.Now.Year.ToString();
            dropMonths.SelectedValue = DateTime.Now.Month.ToString();
        }
    }

    protected void BindData()
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@departmentID", ddl_dp.SelectedValue);
        param[1] = new SqlParameter("@workShop", dropWorkShop.SelectedValue);
        param[2] = new SqlParameter("@uYear", dropYears.SelectedValue);
        param[3] = new SqlParameter("@uMonth", dropMonths.SelectedValue);
        param[4] = new SqlParameter("@uID", Session["uID"].ToString());
        param[5] = new SqlParameter("@uName", Session["uName"].ToString());
        param[6] = new SqlParameter("@plantCode", Session["plantCode"].ToString());
        DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_selectPostAttendace", param).Tables[0];
        gvCalendarCheck.DataSource = dt;
        gvCalendarCheck.DataBind();
    }

    protected void BindDate()
    {
        for (int i = 2005; i <= 2025; i++)
        {
            dropYears.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        for (int i = 1; i <= 12; i++)
        {
            dropMonths.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }

    protected void BindDepartments()
    {
        try
        {
            DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectDepartment").Tables[0];
            this.ddl_dp.DataSource = dt;
            this.ddl_dp.DataBind();
        }
        catch
        { }
        finally
        {
            ddl_dp.Items.Insert(0, new ListItem(" -- ", "0"));
        }
    }

    protected void gvCalendarCheck_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string strYear = dropYears.SelectedValue;
            string strMonth = dropMonths.SelectedValue;
            int daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(dropYears.SelectedValue), Convert.ToInt32(dropMonths.SelectedValue));

            TableCellCollection header = e.Row.Cells;
            header.Clear();

            #region 重建表头

            header.Add(new TableCell());
            header[0].RowSpan = 2;
            header[0].Text = "部门";

            header.Add(new TableCell());
            header[1].RowSpan = 2;
            header[1].Text = "工段";

            int index = 2;
            for (int i = 1; i <= 30; i++)
            {
                header.Add(new TableCell());
                header[index].ColumnSpan = 2;
                header[index].Style.Add("border-bottom", "solid 1px sliver");
                header[index].Text = strYear + "/" + strMonth + "/" + i.ToString();
                index++;
            }

            header.Add(new TableCell());
            header[index].ColumnSpan = 2;
            header[index].Style.Add("border-bottom", "solid 1px sliver");
            header[index].Text = strYear + "/" + strMonth + "/" + "31" + "</TD></TR><TR><TD style='background-color:#006699;color:ffffff;text-align:center;'>在职";
            index++;
            header.Add(new TableCell());
            header[index].RowSpan = 1;
            header[index].Style.Add("background-color", "#006699");
            header[index].Style.Add("color", "#ffffff");
            header[index].Style.Add("text-align", "center");
            header[index].Text = "出勤";
            index++;

            for (int i = 1; i <= 30; i++)
            {
                header.Add(new TableCell());
                header[index].RowSpan = 1;
                header[index].Style.Add("background-color", "#006699");
                header[index].Style.Add("color", "#ffffff");
                header[index].Style.Add("text-align", "center");
                header[index].Text = "在职";
                index++;
                header.Add(new TableCell());
                header[index].RowSpan = 1;
                header[index].Style.Add("background-color", "#006699");
                header[index].Style.Add("color", "#ffffff");
                header[index].Style.Add("text-align", "center");
                header[index].Text = "出勤";
                index++;
            }

            #endregion
        }

    }

    protected void gvCalendarCheck_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvCalendarCheck.Rows.Count > 0)
        {
            for (int i = gvCalendarCheck.Columns.Count - 1; i >= 6; i--)
            {
                gvCalendarCheck.Columns[i].Visible = true;
            }
            int daysInMonth = DateTime.DaysInMonth(Convert.ToInt32(dropYears.SelectedValue), Convert.ToInt32(dropMonths.SelectedValue));
            int colCount = gvCalendarCheck.Columns.Count;
            if (daysInMonth < 31)
            {
                for (int i = 0; i < (31 - daysInMonth); i++)
                {
                    gvCalendarCheck.Columns[colCount - 1 - i].Visible = false;
                    gvCalendarCheck.Columns[colCount - 2 - i].Visible = false;
                }
            }
        }
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Cells[3].Text = string.Format("{0:yyyy-MM-dd}", e.Row.Cells[3].Text);
        //}
    }

    protected void gvCalendarCheck_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCalendarCheck.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('wl_postAttendanceToExcel.aspx?departmentID=" + ddl_dp.SelectedValue + "&workShop=" + dropWorkShop.SelectedValue + "&uYear=" + dropYears.SelectedValue + "&uMonth=" + dropMonths.SelectedValue + "','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0'); w.focus(); ";

    }

    protected void BindWorkShop()
    {
        string strSQL = "select id, name from WorkShop where departmentID = " + ddl_dp.SelectedValue + " and workshopID IS NULL order by code";
        try
        {
            dropWorkShop.Items.Clear();
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsnx(), CommandType.Text, strSQL);
            while (reader.Read())
            {
                dropWorkShop.Items.Add(new ListItem(reader["name"].ToString(), reader["id"].ToString()));
            }
            reader.Dispose();
        }
        catch
        { }
        finally
        {
            dropWorkShop.Items.Insert(0, new ListItem(" -- ", "-1"));
            dropWorkShop.Items.Insert(1, new ListItem(" 无 ", "0"));
        }
    }

    protected void ddl_dp_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindWorkShop();
    }
}