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

public partial class wsline_cs_wl_calendar_pivot_excel : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        bindData();
        string style = @"<style> .text { mso-number-format:\@; } </script> ";
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=excel.xls");
        Response.ContentType = "application/excel";
        Response.Charset = "defect";
        StringWriter sw = new StringWriter();
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        gv_hac.RenderControl(htw);
        Response.Write(style);
        Response.Write(sw.ToString());
        Response.End();
    }

    private void bindData()
    {
        int plantCode = Convert.ToInt32(Request.QueryString["pc"]);
        int departmentID = Convert.ToInt32(Request.QueryString["dp"]);
        string loginName = Request.QueryString["un"];
        int staffType = Convert.ToInt32(Request.QueryString["tp"]);
        int uYear = Convert.ToInt32(Request.QueryString["ye"]);
        int uMonth = Convert.ToInt32(Request.QueryString["mo"]);
        int createdBy = Convert.ToInt32(Request.QueryString["cb"]);
        DataTable dt = GetWlCalendar(plantCode,departmentID,loginName,staffType,uYear,uMonth,createdBy);
        DynamicBindgvpv(dt, gv_hac);
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

    private void DynamicBindgvpv(DataTable dt, GridView gv)
    {
        gv.Columns.Clear();

        for (int i = 0; i < dt.Columns.Count; i++)
        {
            TemplateField tf = new TemplateField();
            tf.HeaderText = dt.Columns[i].Caption.ToString();
            if (i < 2)
            {
                tf.ItemStyle.Width = Unit.Pixel(45);
                tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
            }
            else
            {
                tf.ItemStyle.Width = Unit.Pixel(50);
                tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                tf.EditItemTemplate = new GridViewTemplate("TextBox", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
            }
            tf.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            gv.Columns.Add(tf);
        }

        gv.DataSource = dt;
        gv.DataBind();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void gv_pv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "部门";
            gv_hac.Columns[1].Visible = false;
            e.Row.Cells[2].Text = "工号";
            e.Row.Cells[3].Text = "姓名";
            e.Row.Cells[4].Text = "入司日期";
            e.Row.Cells[5].Text = "离职日期";
            gv_hac.Columns[6].Visible = false;

            int y = gv_hac.Columns.Count - 4;
            e.Row.Cells[y].Text = "小计";

            int i = gv_hac.Columns.Count - 3;
            e.Row.Cells[i].Text = "人员类型";

            int j = gv_hac.Columns.Count - 2;
            e.Row.Cells[j].Text = "计酬方式";

            int z = gv_hac.Columns.Count - 1;
            e.Row.Cells[z].Text = "工段";

            foreach (TableCell cell in e.Row.Cells)
            {
                cell.Attributes.Add("class", "text");
            } 
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
        }
    }
}
