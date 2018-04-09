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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txb_year.Text = DateTime.Now.Year.ToString();
            dropDepartmentBind();
            bindData();
        } 
    }

    private void bindData()
    {
        DataTable dt = GetStaffInfo(Convert.ToInt32(txb_year.Text.Trim()), Convert.ToInt32(ddl_dp.SelectedValue), Convert.ToInt32(ddl_type.SelectedValue));
        DynamicBindgvpv(dt, gv_hac);
    }

    private void dropDepartmentBind()
    {
        DataTable dt = GetDepartment();
        this.ddl_dp.DataSource = dt;
        this.ddl_dp.DataBind();
        ddl_dp.Items.Insert(0, new ListItem("----", "0"));
    }

    private void DynamicBindgvpv(DataTable dt, GridView gv)
    {
        gv.Columns.Clear();

        for (int i = 0; i < dt.Columns.Count; i++)
        {
            TemplateField tf = new TemplateField();
            tf.HeaderText = dt.Columns[i].Caption.ToString();
            tf.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#006699");
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

    private DataTable GetUsertype(int usertype)
    {
        SqlParameter param = new SqlParameter("@depid", usertype);
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectUsertypeBaseDep", param).Tables[0];
    }

    private DataTable GetStaffInfo(int uYear, int departmentID, int staffType)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@departmentid", departmentID);
        param[1] = new SqlParameter("@usertype", staffType);
        param[2] = new SqlParameter("@uYear", uYear);
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_hr_SelectStaffInfo", param).Tables[0];
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txb_year.Text == string.Empty)
        {
            ltlAlert.Text = "alert('年份不能为空!')";
        }
        else
        {
            if (txb_year.Text.Length != 4)
            {
                ltlAlert.Text = "alert('年份必须为4位整数!')";
            }
            else
            {
                bindData();
            }
        } 
    }

    protected void ddl_dp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_type.Items.Clear();
        if (ddl_dp.SelectedIndex == 0)
        {
            ddl_type.Items.Insert(0, new ListItem("----", "-1"));
            bindData();
        }
        else
        {
            DataTable dt = GetUsertype(Convert.ToInt32(ddl_dp.SelectedValue));
            this.ddl_type.DataSource = dt;
            this.ddl_type.DataBind();
            ddl_type.Items.Insert(0, new ListItem("----", "-1"));
            bindData();
        }
    }

    protected void gv_hac_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;部门&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
            e.Row.Cells[1].Text = "人员类型";
        }
    }
    protected void gv_hac_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                //总表头  
                TableCellCollection tcHeader = e.Row.Cells;
                //tcHeader.Clear();

                //第一行第一列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].RowSpan = 2;
                tcHeader[0].Text = "部门";
                tcHeader[0].Font.Size = new FontUnit(8);
                tcHeader[0].Font.Name = "Tahoma,Arial";

                //第一行第二列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].RowSpan = 2;
                tcHeader[1].Text = "人员类型";
                tcHeader[1].Font.Size = new FontUnit(8);
                tcHeader[1].Font.Name = "Tahoma,Arial";

                //第一行第三列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[2].Attributes.Add("colspan", "3");
                tcHeader[2].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[2].Text = "1月";

                //第一行第四列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[3].Attributes.Add("colspan", "3");
                tcHeader[3].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[3].Text = "2月";

                //第一行第五列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[4].Attributes.Add("colspan", "3");
                tcHeader[4].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[4].Text = "3月";

                //第一行第六列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Attributes.Add("colspan", "3");
                tcHeader[5].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[5].Text = "4月";

                //第一行第七列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Attributes.Add("colspan", "3");
                tcHeader[6].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[6].Text = "5月";

                //第一行第八列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Attributes.Add("colspan", "3");
                tcHeader[7].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[7].Text = "6月";

                //第一行第九列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Attributes.Add("colspan", "3");
                tcHeader[8].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[8].Text = "7月";

                //第一行第十列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[9].Attributes.Add("colspan", "3");
                tcHeader[9].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[9].Text = "8月";

                //第一行第十一列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[10].Attributes.Add("colspan", "3");
                tcHeader[10].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[10].Text = "9月";

                //第一行第十二列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[11].Attributes.Add("colspan", "3");
                tcHeader[11].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[11].Text = "10月";

                //第一行第十三列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[12].Attributes.Add("colspan", "3");
                tcHeader[12].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[12].Text = "11月";

                //第一行第十四列表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[13].Attributes.Add("colspan", "3");
                tcHeader[13].Style.Add("border-bottom", "solid 1px sliver");
                tcHeader[13].Text = "12月</th></tr><tr>";

                //第二列1月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[14].Text = "入职";
                tcHeader[14].Font.Size = new FontUnit(8);
                tcHeader[14].Font.Name = "Tahoma,Arial";
                tcHeader[14].Style.Add("background-color", "#006699");
                tcHeader[14].Style.Add("color", "#ffffff");
                tcHeader[14].Style.Add("text-align", "center");

                //第二列1月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[15].Text = "离职";
                tcHeader[15].Font.Size = new FontUnit(8);
                tcHeader[15].Font.Name = "Tahoma,Arial";
                tcHeader[15].Style.Add("background-color", "#006699");
                tcHeader[15].Style.Add("color", "#ffffff");
                tcHeader[15].Style.Add("text-align", "center");

                //第二列1月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[16].Text = "在职";
                tcHeader[16].Font.Size = new FontUnit(8);
                tcHeader[16].Font.Name = "Tahoma,Arial";
                tcHeader[16].Style.Add("background-color", "#006699");
                tcHeader[16].Style.Add("color", "#ffffff");
                tcHeader[16].Style.Add("text-align", "center");

                //第二列2月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[17].Text = "入职";
                tcHeader[17].Font.Size = new FontUnit(8);
                tcHeader[17].Font.Name = "Tahoma,Arial";
                tcHeader[17].Style.Add("background-color", "#006699");
                tcHeader[17].Style.Add("color", "#ffffff");
                tcHeader[17].Style.Add("text-align", "center");

                //第二列2月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[18].Text = "离职";
                tcHeader[18].Font.Size = new FontUnit(8);
                tcHeader[18].Font.Name = "Tahoma,Arial";
                tcHeader[18].Style.Add("background-color", "#006699");
                tcHeader[18].Style.Add("color", "#ffffff");
                tcHeader[18].Style.Add("text-align", "center");

                //第二列2月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[19].Text = "在职";
                tcHeader[19].Font.Size = new FontUnit(8);
                tcHeader[19].Font.Name = "Tahoma,Arial";
                tcHeader[19].Style.Add("background-color", "#006699");
                tcHeader[19].Style.Add("color", "#ffffff");
                tcHeader[19].Style.Add("text-align", "center");

                //第二列3月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[20].Text = "入职";
                tcHeader[20].Font.Size = new FontUnit(8);
                tcHeader[20].Font.Name = "Tahoma,Arial";
                tcHeader[20].Style.Add("background-color", "#006699");
                tcHeader[20].Style.Add("color", "#ffffff");
                tcHeader[20].Style.Add("text-align", "center");

                //第二列3月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[21].Text = "离职";
                tcHeader[21].Font.Size = new FontUnit(8);
                tcHeader[21].Font.Name = "Tahoma,Arial";
                tcHeader[21].Style.Add("background-color", "#006699");
                tcHeader[21].Style.Add("color", "#ffffff");
                tcHeader[21].Style.Add("text-align", "center");

                //第二列3月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[22].Text = "在职";
                tcHeader[22].Font.Size = new FontUnit(8);
                tcHeader[22].Font.Name = "Tahoma,Arial";
                tcHeader[22].Style.Add("background-color", "#006699");
                tcHeader[22].Style.Add("color", "#ffffff");
                tcHeader[22].Style.Add("text-align", "center");

                //第二列4月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[23].Text = "入职";
                tcHeader[23].Font.Size = new FontUnit(8);
                tcHeader[23].Font.Name = "Tahoma,Arial";
                tcHeader[23].Style.Add("background-color", "#006699");
                tcHeader[23].Style.Add("color", "#ffffff");
                tcHeader[23].Style.Add("text-align", "center");

                //第二列4月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[24].Text = "离职";
                tcHeader[24].Font.Size = new FontUnit(8);
                tcHeader[24].Font.Name = "Tahoma,Arial";
                tcHeader[24].Style.Add("background-color", "#006699");
                tcHeader[24].Style.Add("color", "#ffffff");
                tcHeader[24].Style.Add("text-align", "center");

                //第二列4月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[25].Text = "在职";
                tcHeader[25].Font.Size = new FontUnit(8);
                tcHeader[25].Font.Name = "Tahoma,Arial";
                tcHeader[25].Style.Add("background-color", "#006699");
                tcHeader[25].Style.Add("color", "#ffffff");
                tcHeader[25].Style.Add("text-align", "center");


                //第二列5月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[26].Text = "入职";
                tcHeader[26].Font.Size = new FontUnit(8);
                tcHeader[26].Font.Name = "Tahoma,Arial";
                tcHeader[26].Style.Add("background-color", "#006699");
                tcHeader[26].Style.Add("color", "#ffffff");
                tcHeader[26].Style.Add("text-align", "center");

                //第二列5月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[27].Text = "离职";
                tcHeader[27].Font.Size = new FontUnit(8);
                tcHeader[27].Font.Name = "Tahoma,Arial";
                tcHeader[27].Style.Add("background-color", "#006699");
                tcHeader[27].Style.Add("color", "#ffffff");
                tcHeader[27].Style.Add("text-align", "center");

                //第二列5月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[28].Text = "在职";
                tcHeader[28].Font.Size = new FontUnit(8);
                tcHeader[28].Font.Name = "Tahoma,Arial";
                tcHeader[28].Style.Add("background-color", "#006699");
                tcHeader[28].Style.Add("color", "#ffffff");
                tcHeader[28].Style.Add("text-align", "center");

                //第二列6月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[29].Text = "入职";
                tcHeader[29].Font.Size = new FontUnit(8);
                tcHeader[29].Font.Name = "Tahoma,Arial";
                tcHeader[29].Style.Add("background-color", "#006699");
                tcHeader[29].Style.Add("color", "#ffffff");
                tcHeader[29].Style.Add("text-align", "center");

                //第二列6月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[30].Text = "离职";
                tcHeader[30].Font.Size = new FontUnit(8);
                tcHeader[30].Font.Name = "Tahoma,Arial";
                tcHeader[30].Style.Add("background-color", "#006699");
                tcHeader[30].Style.Add("color", "#ffffff");
                tcHeader[30].Style.Add("text-align", "center");

                //第二列6月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[31].Text = "在职";
                tcHeader[31].Font.Size = new FontUnit(8);
                tcHeader[31].Font.Name = "Tahoma,Arial";
                tcHeader[31].Style.Add("background-color", "#006699");
                tcHeader[31].Style.Add("color", "#ffffff");
                tcHeader[31].Style.Add("text-align", "center");

                //第二列7月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[32].Text = "入职";
                tcHeader[32].Font.Size = new FontUnit(8);
                tcHeader[32].Font.Name = "Tahoma,Arial";
                tcHeader[32].Style.Add("background-color", "#006699");
                tcHeader[32].Style.Add("color", "#ffffff");
                tcHeader[32].Style.Add("text-align", "center");

                //第二列7月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[33].Text = "离职";
                tcHeader[33].Font.Size = new FontUnit(8);
                tcHeader[33].Font.Name = "Tahoma,Arial";
                tcHeader[33].Style.Add("background-color", "#006699");
                tcHeader[33].Style.Add("color", "#ffffff");
                tcHeader[33].Style.Add("text-align", "center");

                //第二列7月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[34].Text = "在职";
                tcHeader[34].Font.Size = new FontUnit(8);
                tcHeader[34].Font.Name = "Tahoma,Arial";
                tcHeader[34].Style.Add("background-color", "#006699");
                tcHeader[34].Style.Add("color", "#ffffff");
                tcHeader[34].Style.Add("text-align", "center");

                //第二列8月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[35].Text = "入职";
                tcHeader[35].Font.Size = new FontUnit(8);
                tcHeader[35].Font.Name = "Tahoma,Arial";
                tcHeader[35].Style.Add("background-color", "#006699");
                tcHeader[35].Style.Add("color", "#ffffff");
                tcHeader[35].Style.Add("text-align", "center");

                //第二列8月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[36].Text = "离职";
                tcHeader[36].Font.Size = new FontUnit(8);
                tcHeader[36].Font.Name = "Tahoma,Arial";
                tcHeader[36].Style.Add("background-color", "#006699");
                tcHeader[36].Style.Add("color", "#ffffff");
                tcHeader[36].Style.Add("text-align", "center");

                //第二列8月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[37].Text = "在职";
                tcHeader[37].Font.Size = new FontUnit(8);
                tcHeader[37].Font.Name = "Tahoma,Arial";
                tcHeader[37].Style.Add("background-color", "#006699");
                tcHeader[37].Style.Add("color", "#ffffff");
                tcHeader[37].Style.Add("text-align", "center");

                //第二列9月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[38].Text = "入职";
                tcHeader[38].Font.Size = new FontUnit(8);
                tcHeader[38].Font.Name = "Tahoma,Arial";
                tcHeader[38].Style.Add("background-color", "#006699");
                tcHeader[38].Style.Add("color", "#ffffff");
                tcHeader[38].Style.Add("text-align", "center");

                //第二列9月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[39].Text = "离职";
                tcHeader[39].Font.Size = new FontUnit(8);
                tcHeader[39].Font.Name = "Tahoma,Arial";
                tcHeader[39].Style.Add("background-color", "#006699");
                tcHeader[39].Style.Add("color", "#ffffff");
                tcHeader[39].Style.Add("text-align", "center");

                //第二列9月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[40].Text = "在职";
                tcHeader[40].Font.Size = new FontUnit(8);
                tcHeader[40].Font.Name = "Tahoma,Arial";
                tcHeader[40].Style.Add("background-color", "#006699");
                tcHeader[40].Style.Add("color", "#ffffff");
                tcHeader[40].Style.Add("text-align", "center");


                //第二列10月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[41].Text = "入职";
                tcHeader[41].Font.Size = new FontUnit(8);
                tcHeader[41].Font.Name = "Tahoma,Arial";
                tcHeader[41].Style.Add("background-color", "#006699");
                tcHeader[41].Style.Add("color", "#ffffff");
                tcHeader[41].Style.Add("text-align", "center");

                //第二列10月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[42].Text = "离职";
                tcHeader[42].Font.Size = new FontUnit(8);
                tcHeader[42].Font.Name = "Tahoma,Arial";
                tcHeader[42].Style.Add("background-color", "#006699");
                tcHeader[42].Style.Add("color", "#ffffff");
                tcHeader[42].Style.Add("text-align", "center");

                //第二列10月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[43].Text = "在职";
                tcHeader[43].Font.Size = new FontUnit(8);
                tcHeader[43].Font.Name = "Tahoma,Arial";
                tcHeader[43].Style.Add("background-color", "#006699");
                tcHeader[43].Style.Add("color", "#ffffff");
                tcHeader[43].Style.Add("text-align", "center");

                //第二列11月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[44].Text = "入职";
                tcHeader[44].Font.Size = new FontUnit(8);
                tcHeader[44].Font.Name = "Tahoma,Arial";
                tcHeader[44].Style.Add("background-color", "#006699");
                tcHeader[44].Style.Add("color", "#ffffff");
                tcHeader[44].Style.Add("text-align", "center");

                //第二列11月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[45].Text = "离职";
                tcHeader[45].Font.Size = new FontUnit(8);
                tcHeader[45].Font.Name = "Tahoma,Arial";
                tcHeader[45].Style.Add("background-color", "#006699");
                tcHeader[45].Style.Add("color", "#ffffff");
                tcHeader[45].Style.Add("text-align", "center");

                //第二列11月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[46].Text = "在职";
                tcHeader[46].Font.Size = new FontUnit(8);
                tcHeader[46].Font.Name = "Tahoma,Arial";
                tcHeader[46].Style.Add("background-color", "#006699");
                tcHeader[46].Style.Add("color", "#ffffff");
                tcHeader[46].Style.Add("text-align", "center");

                //第二列12月入职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[47].Text = "入职";
                tcHeader[47].Font.Size = new FontUnit(8);
                tcHeader[47].Font.Name = "Tahoma,Arial";
                tcHeader[47].Style.Add("background-color", "#006699");
                tcHeader[47].Style.Add("color", "#ffffff");
                tcHeader[47].Style.Add("text-align", "center");

                //第二列12月离职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[48].Text = "离职";
                tcHeader[48].Font.Size = new FontUnit(8);
                tcHeader[48].Font.Name = "Tahoma,Arial";
                tcHeader[48].Style.Add("background-color", "#006699");
                tcHeader[48].Style.Add("color", "#ffffff");
                tcHeader[48].Style.Add("text-align", "center");

                //第二列12月在职
                tcHeader.Add(new TableHeaderCell());
                tcHeader[49].Text = "在职";
                tcHeader[49].Font.Size = new FontUnit(8);
                tcHeader[49].Font.Name = "Tahoma,Arial";
                tcHeader[49].Style.Add("background-color", "#006699");
                tcHeader[49].Style.Add("color", "#ffffff");
                tcHeader[49].Style.Add("text-align", "center");

                break;
        }
    }

    protected void BtnExcel_Click(object sender, EventArgs e)
    {
        bindData();

        string style = @"<style> .text { mso-number-format:\@; word-break:keep-all; word-wrap:normal; } td {mso-number-format:\@ ;} </script> ";
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment; filename=excel.xls");
        //Response.ContentType = "application/excel";
        Response.ContentType = "application/vnd.xls";
        this.EnableViewState = false;

        StringWriter sw = new StringWriter();

        HtmlTextWriter htw = new HtmlTextWriter(sw);

        gv_hac.RenderControl(htw);

        // Style is added dynamically
        Response.Write(style);
        Response.Write(sw.ToString());
        Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}
