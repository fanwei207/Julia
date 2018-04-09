using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class EDI_EDI_changePoResultList : BasePage
{
    poc_helper helper = new poc_helper();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        bind();
    }

    private void bind()
    {
        string No = txtPocCode.Text.Trim();
        string ponbr = txtPoNbr.Text.Trim();
        string beginDate = txtDateBegin.Text.Trim();
        string enddate = txtDateEnd.Text.Trim();

        try
        {

            if(!string.Empty.Equals(beginDate))
            {
                Convert.ToDateTime(beginDate);
            }
             if(!string.Empty.Equals(enddate))
            {
                Convert.ToDateTime(enddate);
            }

        }
        catch
        {
            this.Alert("您的日期输入错误");
            return;
        
        }


        gvlist.DataSource = helper.selectResultList(No, ponbr, beginDate, enddate);
        gvlist.DataBind();

    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            GridViewRow row = e.Row;
            bool flag = false;

            if (gvlist.DataKeys[e.Row.RowIndex].Values["addflag"].ToString() == "add")
            {
                flag = true;
                ((Label)e.Row.FindControl("lbChangeType")).Text = "A";
            }
            else if (gvlist.DataKeys[e.Row.RowIndex].Values["isdelete"].ToString() == "True")
            {
                ((Label)e.Row.FindControl("lbChangeType")).Text = "D";
            }
            else
            {
                ((Label)e.Row.FindControl("lbChangeType")).Text = "U";
            }
            // Make sure we aren't in header/footer rows
            if (row.DataItem == null)
            {
                return;
            }

            CellShow(row, "partNbr", ref flag);
            CellShow(row, "sku", ref flag);
            CellShow(row, "qadPart", ref flag);
            CellShow(row, "ordQty", ref flag);
            CellShow(row, "um", ref flag);
            CellShow(row, "price", ref flag);
            CellShow(row, "reqDate", ref flag);
            CellShow(row, "dueDate", ref flag);
            CellShow(row, "remark", ref flag);



            //if (flag)
            //{

            //    ((LinkButton)e.Row.FindControl("lkbCancel")).Visible = true;
            //    ((LinkButton)e.Row.FindControl("lkbEdit")).Visible = true;
            //}
        }
    }

    /// <summary>
    /// 处理行的方法
    /// </summary>
    /// <param name="row">当前行的对象</param>
    /// <param name="field">列的字段</param>
    private void CellShow(GridViewRow row, string field, ref bool flag)
    {
        string value = ((DataRowView)row.DataItem)[field].ToString();
        Label link = row.FindControl("link" + field) as Label;
        Label label = row.FindControl("lbl" + field) as Label;
        if (value.Contains("|"))
        {
            string[] values = value.Split(new char[] { '|' });
            string oldValue = values[0];
            string newValue = values[1];
            if (link != null)
            {
                link.Text = newValue;

            }
            if (label != null)
            {
                label.Text = oldValue;
                label.Font.Strikeout = true;
            }
            flag = true;
        }
        else
        {
            if (label != null)
            {
                label.Text = value;
                label.Font.Strikeout = false;
            }
            if (link != null)
            {
                link.Visible = false;
            }
        }
    }
}