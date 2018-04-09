using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using IT;
using QCProgress;
public partial class IT_TSK_ChargerTotal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txb_year.Text = DateTime.Now.Year.ToString();
            txb_month.Text = DateTime.Now.Month.ToString();
            BindUser();
            BindGridView();
        }
    }
    protected  void BindGridView()
    {
        DataTable dt = TaskHelper.SelectChargerTotal(txb_year.Text.Trim(), txb_month.Text.Trim(), chkNotDis.Checked
                                , dropTracker.SelectedValue);
        gv.Columns.Clear();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            TemplateField tf = new TemplateField();
            tf.HeaderText = dt.Columns[i].Caption.ToString();
            //tf.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#006699");
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
    protected void BindUser()
    {
        DataTable table = TaskHelper.GetUsers(string.Empty, 404);
        dropTracker.Items.Clear();

        dropTracker.DataSource = table;
        dropTracker.DataBind();
        dropTracker.Items.Insert(0, new ListItem("--全部--", "0"));
    }

    protected void btnSearch_Click(object sender, EventArgs e)
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
                BindGridView();
            }
        } 
       
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            gv.Columns[0].Visible = false;
            gv.Columns[2].Visible = false;
            e.Row.Cells[1].Text = "&nbsp;&nbsp;&nbsp;&nbsp姓名&nbsp;&nbsp;&nbsp;&nbsp";
          
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("TSK_GanntMstr.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
}