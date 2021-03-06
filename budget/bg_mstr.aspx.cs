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
using BudgetProcess;
using System.Text;
using System.IO;

namespace BudgetProcess
{
    public partial class budget_bg_mstr : BasePage
    {
        adamClass chk = new adamClass();
        Budget budget = new Budget();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                int year = DateTime.Now.Year;
                dropYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
                 

                gvMstr.DataSource = budget.GetMstr(SearchConditions, dropYear.SelectedValue);
                gvMstr.DataBind();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvMstr.DataSource = budget.GetMstr(SearchConditions, dropYear.SelectedValue);
            gvMstr.DataBind();
        }
        private string SearchConditions
        {
            get
            {
                string str = " where 1=1 ";
                if (Session["uRole"].ToString() != "1")
                {
                    str = " where bg_modifier like N'%," + Session["uID"].ToString() + ",%' ";
                }

                str += " and bg_year = " + dropYear.SelectedValue + " ";

                if (txtMstr.Text.Trim() != string.Empty)
                {
                    str += " and bg_masterC like N'%" + txtMstr.Text.Trim() + "%'";
                }

                if (txtDep.Text.Trim() != string.Empty)
                {
                    str += " and bg_dept like N'%" + txtDep.Text.Trim() + "%'";
                }

                if (txtPro.Text.Trim() != string.Empty)
                {
                    str += " and bg_project like N'%" + txtPro.Text.Trim() + "%'";
                }

                if (txtAcc.Text.Trim() != string.Empty)
                {
                    str += " and bg_acc like N'%" + txtAcc.Text.Trim() + "%'";
                }

                if (txtCC.Text.Trim() != string.Empty)
                {
                    str += " and bg_cc like N'%" + txtCC.Text.Trim() + "%'";
                }

                return str;
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            gvMstr.DataSource = budget.GetMstr(SearchConditions, dropYear.SelectedValue);
            gvMstr.DataBind();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            gvMstr.EnableViewState = false;

            page.EnableEventValidation = false;

            page.DesignerInitialize();

            page.Controls.Add(form);
            form.Controls.Add(gvMstr);

            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("Content-Disposition", "attachment;filename=budget.xls");
            Response.Charset = "gb2312";
            //Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void gvMstr_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMstr.PageIndex = e.NewPageIndex;

            gvMstr.DataSource = budget.GetMstr(SearchConditions, dropYear.SelectedValue);
            gvMstr.DataBind();
        }
        protected void gvMstr_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string strYear = DateTime.Now.Year.ToString();

                TableCellCollection header = e.Row.Cells;
                header.Clear();
                #region
                header.Add(new TableCell());
                header[0].RowSpan = 2;
                header[0].Text = "主管";

                header.Add(new TableCell());
                header[1].RowSpan = 2;
                header[1].Text = "部门";

                header.Add(new TableCell());
                header[2].RowSpan = 2;
                header[2].Text = "账户";

                header.Add(new TableCell());
                header[3].RowSpan = 2;
                header[3].Text = "分账户";

                header.Add(new TableCell());
                header[4].RowSpan = 2;
                header[4].Text = "描述";

                header.Add(new TableCell());
                header[5].RowSpan = 2;
                
                header[5].Text = "成本中心";

                header.Add(new TableCell());
                header[6].RowSpan = 2;
                header[6].Text = "项目";

                header.Add(new TableCell());
                header[7].RowSpan = 2;
                header[7].Text = "预测";

                header.Add(new TableCell());
                header[8].ColumnSpan = 3;
                header[8].Style.Add("border-bottom", "solid 1px sliver");
                header[8].Text = strYear + "01";

                header.Add(new TableCell());
                header[9].ColumnSpan = 3;
                header[9].Style.Add("border-bottom", "solid 1px sliver");
                header[9].Text = strYear + "02";

                header.Add(new TableCell());
                header[10].ColumnSpan = 3;
                header[10].Style.Add("border-bottom", "solid 1px sliver");
                header[10].Text = strYear + "03";

                header.Add(new TableCell());
                header[11].ColumnSpan = 3;
                header[11].Style.Add("border-bottom", "solid 1px sliver");
                header[11].Text = strYear + "04";

                header.Add(new TableCell());
                header[12].ColumnSpan = 3;
                header[12].Style.Add("border-bottom", "solid 1px sliver");
                header[12].Text = strYear + "05";

                header.Add(new TableCell());
                header[13].ColumnSpan = 3;
                header[13].Style.Add("border-bottom", "solid 1px sliver");
                header[13].Text = strYear + "06";

                header.Add(new TableCell());
                header[14].ColumnSpan = 3;
                header[14].Style.Add("border-bottom", "solid 1px sliver");
                header[14].Text = strYear + "07";

                header.Add(new TableCell());
                header[15].ColumnSpan = 3;
                header[15].Style.Add("border-bottom", "solid 1px sliver");
                header[15].Text = strYear + "08";

                header.Add(new TableCell());
                header[16].ColumnSpan = 3;
                header[16].Style.Add("border-bottom", "solid 1px sliver");
                header[16].Text = strYear + "09";

                header.Add(new TableCell());
                header[17].ColumnSpan = 3;
                header[17].Style.Add("border-bottom", "solid 1px sliver");
                header[17].Text = strYear + "010";

                header.Add(new TableCell());
                header[18].ColumnSpan = 3;
                header[18].Style.Add("border-bottom", "solid 1px sliver");
                header[18].Text = strYear + "11";

                header.Add(new TableCell());
                header[19].ColumnSpan = 3;
                header[19].Style.Add("border-bottom", "solid 1px sliver");
                header[19].Text = strYear + "12";

                header.Add(new TableCell());
                header[20].ColumnSpan = 2;
                header[20].Style.Add("border-bottom", "solid 1px sliver");
                header[20].Text = "合计</TD></TR><TR><TD style='background-color:#006699;color:ffffff;text-align:center;'>实际";

                header.Add(new TableCell());
                header[21].RowSpan = 1;
                header[21].Style.Add("background-color", "#006699");
                header[21].Style.Add("color", "#ffffff");
                header[21].Style.Add("text-align", "center");
                header[21].Text = "预测";

                header.Add(new TableCell());
                header[22].RowSpan = 1;
                header[22].Style.Add("background-color", "#006699");
                header[22].Style.Add("color", "#ffffff");
                header[22].Style.Add("text-align", "center");
                header[22].Text = "差额";

                header.Add(new TableCell());
                header[23].RowSpan = 1;
                header[23].Style.Add("background-color", "#006699");
                header[23].Style.Add("color", "#ffffff");
                header[23].Style.Add("text-align", "center");
                header[23].Text = "实际";

                header.Add(new TableCell());
                header[24].RowSpan = 1;
                header[24].Style.Add("background-color", "#006699");
                header[24].Style.Add("color", "#ffffff");
                header[24].Style.Add("text-align", "center");
                header[24].Text = "预测";

                header.Add(new TableCell());
                header[25].RowSpan = 1;
                header[25].Style.Add("background-color", "#006699");
                header[25].Style.Add("color", "#ffffff");
                header[25].Style.Add("text-align", "center");
                header[25].Text = "差额";

                header.Add(new TableCell());
                header[26].RowSpan = 1;
                header[26].Style.Add("background-color", "#006699");
                header[26].Style.Add("color", "#ffffff");
                header[26].Style.Add("text-align", "center");
                header[26].Text = "实际";

                header.Add(new TableCell());
                header[27].RowSpan = 1;
                header[27].Style.Add("background-color", "#006699");
                header[27].Style.Add("color", "#ffffff");
                header[27].Style.Add("text-align", "center");
                header[27].Text = "预测";

                header.Add(new TableCell());
                header[28].RowSpan = 1;
                header[28].Style.Add("background-color", "#006699");
                header[28].Style.Add("color", "#ffffff");
                header[28].Style.Add("text-align", "center");
                header[28].Text = "差额";

                header.Add(new TableCell());
                header[29].RowSpan = 1;
                header[29].Style.Add("background-color", "#006699");
                header[29].Style.Add("color", "#ffffff");
                header[29].Style.Add("text-align", "center");
                header[29].Text = "实际";

                header.Add(new TableCell());
                header[30].RowSpan = 1;
                header[30].Style.Add("background-color", "#006699");
                header[30].Style.Add("color", "#ffffff");
                header[30].Style.Add("text-align", "center");
                header[30].Text = "预测";

                header.Add(new TableCell());
                header[31].RowSpan = 1;
                header[31].Style.Add("background-color", "#006699");
                header[31].Style.Add("color", "#ffffff");
                header[31].Style.Add("text-align", "center");
                header[31].Text = "差额";

                header.Add(new TableCell());
                header[32].RowSpan = 1;
                header[32].Style.Add("background-color", "#006699");
                header[32].Style.Add("color", "#ffffff");
                header[32].Style.Add("text-align", "center");
                header[32].Text = "实际";

                header.Add(new TableCell());
                header[33].RowSpan = 1;
                header[33].Style.Add("background-color", "#006699");
                header[33].Style.Add("color", "#ffffff");
                header[33].Style.Add("text-align", "center");
                header[33].Text = "预测";

                header.Add(new TableCell());
                header[34].RowSpan = 1;
                header[34].Style.Add("background-color", "#006699");
                header[34].Style.Add("color", "#ffffff");
                header[34].Style.Add("text-align", "center");
                header[34].Text = "差额";

                header.Add(new TableCell());
                header[35].RowSpan = 1;
                header[35].Style.Add("background-color", "#006699");
                header[35].Style.Add("color", "#ffffff");
                header[35].Style.Add("text-align", "center");
                header[35].Text = "实际";

                header.Add(new TableCell());
                header[36].RowSpan = 1;
                header[36].Style.Add("background-color", "#006699");
                header[36].Style.Add("color", "#ffffff");
                header[36].Style.Add("text-align", "center");
                header[36].Text = "预测";

                header.Add(new TableCell());
                header[37].RowSpan = 1;
                header[37].Style.Add("background-color", "#006699");
                header[37].Style.Add("color", "#ffffff");
                header[37].Style.Add("text-align", "center");
                header[37].Text = "差额";

                header.Add(new TableCell());
                header[38].RowSpan = 1;
                header[38].Style.Add("background-color", "#006699");
                header[38].Style.Add("color", "#ffffff");
                header[38].Style.Add("text-align", "center");
                header[38].Text = "实际";

                header.Add(new TableCell());
                header[39].RowSpan = 1;
                header[39].Style.Add("background-color", "#006699");
                header[39].Style.Add("color", "#ffffff");
                header[39].Style.Add("text-align", "center");
                header[39].Text = "预测";

                header.Add(new TableCell());
                header[40].RowSpan = 1;
                header[40].Style.Add("background-color", "#006699");
                header[40].Style.Add("color", "#ffffff");
                header[40].Style.Add("text-align", "center");
                header[40].Text = "差额";

                header.Add(new TableCell());
                header[41].RowSpan = 1;
                header[41].Style.Add("background-color", "#006699");
                header[41].Style.Add("color", "#ffffff");
                header[41].Style.Add("text-align", "center");
                header[41].Text = "实际";

                header.Add(new TableCell());
                header[42].RowSpan = 1;
                header[42].Style.Add("background-color", "#006699");
                header[42].Style.Add("color", "#ffffff");
                header[42].Style.Add("text-align", "center");
                header[42].Text = "预测";

                header.Add(new TableCell());
                header[43].RowSpan = 1;
                header[43].Style.Add("background-color", "#006699");
                header[43].Style.Add("color", "#ffffff");
                header[43].Style.Add("text-align", "center");
                header[43].Text = "差额";

                header.Add(new TableCell());
                header[44].RowSpan = 1;
                header[44].Style.Add("background-color", "#006699");
                header[44].Style.Add("color", "#ffffff");
                header[44].Style.Add("text-align", "center");
                header[44].Text = "实际";

                header.Add(new TableCell());
                header[45].RowSpan = 1;
                header[45].Style.Add("background-color", "#006699");
                header[45].Style.Add("color", "#ffffff");
                header[45].Style.Add("text-align", "center");
                header[45].Text = "预测";

                header.Add(new TableCell());
                header[46].RowSpan = 1;
                header[46].Style.Add("background-color", "#006699");
                header[46].Style.Add("color", "#ffffff");
                header[46].Style.Add("text-align", "center");
                header[46].Text = "差额";

                header.Add(new TableCell());
                header[47].RowSpan = 1;
                header[47].Style.Add("background-color", "#006699");
                header[47].Style.Add("color", "#ffffff");
                header[47].Style.Add("text-align", "center");
                header[47].Text = "实际";

                header.Add(new TableCell());
                header[48].RowSpan = 1;
                header[48].Style.Add("background-color", "#006699");
                header[48].Style.Add("color", "#ffffff");
                header[48].Style.Add("text-align", "center");
                header[48].Text = "预测";

                header.Add(new TableCell());
                header[49].RowSpan = 1;
                header[49].Style.Add("background-color", "#006699");
                header[49].Style.Add("color", "#ffffff");
                header[49].Style.Add("text-align", "center");
                header[49].Text = "差额";

                header.Add(new TableCell());
                header[50].RowSpan = 1;
                header[50].Style.Add("background-color", "#006699");
                header[50].Style.Add("color", "#ffffff");
                header[50].Style.Add("text-align", "center");
                header[50].Text = "实际";

                header.Add(new TableCell());
                header[51].RowSpan = 1;
                header[51].Style.Add("background-color", "#006699");
                header[51].Style.Add("color", "#ffffff");
                header[51].Style.Add("text-align", "center");
                header[51].Text = "预测";

                header.Add(new TableCell());
                header[52].RowSpan = 1;
                header[52].Style.Add("background-color", "#006699");
                header[52].Style.Add("color", "#ffffff");
                header[52].Style.Add("text-align", "center");
                header[52].Text = "差额";

                header.Add(new TableCell());
                header[53].RowSpan = 1;
                header[53].Style.Add("background-color", "#006699");
                header[53].Style.Add("color", "#ffffff");
                header[53].Style.Add("text-align", "center");
                header[53].Text = "实际";

                header.Add(new TableCell());
                header[54].RowSpan = 1;
                header[54].Style.Add("background-color", "#006699");
                header[54].Style.Add("color", "#ffffff");
                header[54].Style.Add("text-align", "center");
                header[54].Text = "预测";

                header.Add(new TableCell());
                header[55].RowSpan = 1;
                header[55].Style.Add("background-color", "#006699");
                header[55].Style.Add("color", "#ffffff");
                header[55].Style.Add("text-align", "center");
                header[55].Text = "差额";

                header.Add(new TableCell());
                header[56].RowSpan = 1;
                header[56].Style.Add("background-color", "#006699");
                header[56].Style.Add("color", "#ffffff");
                header[56].Style.Add("text-align", "center");
                header[56].Text = "实际";

                header.Add(new TableCell());
                header[57].RowSpan = 1;
                header[57].Style.Add("background-color", "#006699");
                header[57].Style.Add("color", "#ffffff");
                header[57].Style.Add("text-align", "center");
                header[57].Text = "预测";
                #endregion
            }
        }

        protected void gvMstr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Year = dropYear.SelectedValue;
                Decimal bg_em = 0;
                Decimal bg_budget = 0;

                e.Row.Cells[7].Text = "编辑";
                e.Row.Cells[7].Style.Add("cursor", "hand");
                e.Row.Cells[7].Attributes["onclick"] = "window.open('bg_mstr_edit.aspx?master=" + Server.UrlDecode(gvMstr.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim()) + "&dept=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[1].ToString().Trim()) + "&acc=" + Server.UrlDecode(gvMstr.DataKeys[e.Row.RowIndex].Values[2].ToString().Trim()) + "&sub=" + Server.UrlDecode(gvMstr.DataKeys[e.Row.RowIndex].Values[3].ToString().Trim()) +"&desc="+Server.UrlDecode(e.Row.Cells[4].Text.Trim())+ "&project=" + Server.UrlDecode(gvMstr.DataKeys[e.Row.RowIndex].Values[4].ToString().Trim()) + "&cc=" + Server.UrlDecode(gvMstr.DataKeys[e.Row.RowIndex].Values[5].ToString().Trim()) + "&year=" + Server.UrlDecode(Year.ToString()) + "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0');";

                int index = 1;
                for (int i = 8; i < e.Row.Cells.Count; )
                {
                    if (e.Row.Cells[i].Text.Trim() != string.Empty && e.Row.Cells[i].Text.Trim() != "&nbsp;")
                    {
                        e.Row.Cells[i].Style.Add("cursor", "hand");
                        e.Row.Cells[i].Attributes["onclick"] = "window.showModalDialog('bg_acd.aspx?master=" + Server.UrlDecode(gvMstr.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim()) + "&dept=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[1].ToString().Trim()) + "&acc=" + Server.UrlDecode(gvMstr.DataKeys[e.Row.RowIndex].Values[2].ToString().Trim()) + "&sub=" + Server.UrlDecode(gvMstr.DataKeys[e.Row.RowIndex].Values[3].ToString().Trim()) + "&project=" + Server.UrlDecode(gvMstr.DataKeys[e.Row.RowIndex].Values[4].ToString().Trim()) + "&year=" + Server.UrlDecode(Year.ToString()) + "&per=" + Server.UrlDecode(index.ToString()) + "',null,'dialogWidth=1000px;dialogHeight=500px');";
                        bg_em += Convert.ToDecimal(e.Row.Cells[i].Text.Trim());
                    }

                    if (e.Row.Cells[i + 1].Text.Trim() != string.Empty && e.Row.Cells[i + 1].Text.Trim() != "&nbsp;")
                    {
                        bg_budget += Convert.ToDecimal(e.Row.Cells[i+1].Text.Trim());
                    }


                    i += 3;
                    index += 1;
                }

                e.Row.Cells[e.Row.Cells.Count - 2].Text = bg_em.ToString();


                e.Row.Cells[e.Row.Cells.Count - 1].Text = bg_budget.ToString();

                for (int j = 8; j < e.Row.Cells.Count; j++)
                {
                    e.Row.Cells[j].Style.Add("text-align", "right");
                }
            }
        }
    }
}
