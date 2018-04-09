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
using BudgetProcess;
using adamFuncs;
using System.Text;
using System.IO;

namespace BudgetProcess
{
    public partial class budget_bg_query : BasePage
    {
        adamClass chk = new adamClass();
        Budget budget = new Budget();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                Session["orderby"] = "bg_masterC";
                Session["orderdir"] = "ASC";

                GridViewNullData.GridViewDataBind(gvMstr, budget.QueryMstr(SearchConditions));
            }
        }

        private string SearchConditions
        {
            get
            {
                string str = " where 1=1 ";
                if (Session["uRole"].ToString() != "1")
                {
                    str = " where bg_reader like N'%," + Session["uID"].ToString() + ",%' ";
                }

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

                //if (Session["orderby"] == null || Session["orderby"].ToString() == string.Empty)
                //    Session["orderby"] = "bg_masterC";

                //if (Session["orderdir"] == null || Session["orderdir"].ToString() == string.Empty)
                //    Session["orderdir"] = "ASC";

                str += " order by " + Session["orderby"].ToString() + " " + Session["orderdir"];
                return str;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridViewNullData.GridViewDataBind(gvMstr, budget.QueryMstr(SearchConditions));
        }
        protected void gvMstr_Sorting(object sender, GridViewSortEventArgs e)
        {
            
            GridViewNullData.GridViewDataBind(gvMstr, budget.QueryMstr(SearchConditions));
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {

            gvMstr.Columns[1].Visible = true;

            GridViewNullData.GridViewDataBind(gvMstr, budget.QueryMstr(SearchConditions));

            gvMstr.EnableViewState = false;
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            page.EnableEventValidation = false;

            page.DesignerInitialize();

            page.Controls.Add(form);
            form.Controls.Add(gvMstr);

            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "gb2312";
            Response.Write(sb.ToString());
            Response.End();


        }
        protected void gvMstr_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow) 
            {
                HyperLink hlink = (HyperLink)e.Row.Cells[10].Controls[0];
                if (hlink.Text.Trim() != string.Empty) 
                {
                    //hlink.Attributes["onclick"] = "window.showModalDialog('bg_acd.aspx?master=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim()) + "&dept=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[1].ToString().Trim()) + "&acc=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[2].ToString().Trim()) + "&sub=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[3].ToString().Trim()) + "&project=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[4].ToString().Trim()) + "&year=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[6].ToString().Trim()) + "&per=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[7].ToString().Trim()) + "',null,'dialogWidth=1000px;dialogHeight=500px');";
                    hlink.Attributes["onclick"] = "window.open('bg_acd.aspx?master=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[0].ToString().Trim()) + "&dept=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[1].ToString().Trim()) + "&acc=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[2].ToString().Trim()) + "&sub=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[3].ToString().Trim()) + "&project=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[4].ToString().Trim()) + "&year=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[6].ToString().Trim()) + "&per=" + Server.UrlEncode(gvMstr.DataKeys[e.Row.RowIndex].Values[7].ToString().Trim()) + "','','menubar=no,scrollbars=no,resizable=no,width=1000,height=500,top=0,left=0');";
                }
            }
        }
        protected void gvMstr_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }
}
}
