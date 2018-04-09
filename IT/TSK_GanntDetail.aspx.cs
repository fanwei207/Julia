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

public partial class TSK_GanntDetail : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            string _id = Request.QueryString["id"];
            //gv.DataSource = TaskHelper.SelectTaskDetByIdDate(_id);
           // gv.DataBind();
            BindGridView(_id);
        }
    }
    protected void BindGridView(string _id)
    {
        DataTable dt = TaskHelper.SelectTaskDetByIdDate(_id);
        if(dt==null)
        {
            this.Alert("当前任务还没有开发日志！");
            return;
        }
        else if (dt.Rows.Count == 0)
        {
            this.Alert("当前任务还没有开发日志！");
            return;
        }
        try
        {
            int num1 = 0;
            int num2 = 0;
            gv.Columns.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                TemplateField tf = new TemplateField();
                tf.HeaderText = dt.Columns[i].Caption.ToString();
                //tf.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#006699");
                if (i < 1)
                {
                    //if (dt.Rows[0][i].ToString().Trim()==string.Empty)
                    //{
                        tf.ItemStyle.Width = Unit.Pixel(60);
                        tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                        num1 += 1;
                    //}
                    //else
                    //{
                    //    tf.ItemStyle.Width = Unit.Pixel(630);
                    //    tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                    //    num2 += 1;
                    //}
                 }
                else
                {
                    //if (dt.Rows[0][i].ToString().Trim() == string.Empty)
                    //{
                    //    tf.ItemStyle.Width = Unit.Pixel(60);
                    //    tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                    //    num1 += 1;
                    //}
                    //else
                    //{
                        tf.ItemStyle.Width = Unit.Pixel(630);
                        tf.ItemTemplate = new GridViewTemplate("Label", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                        tf.EditItemTemplate = new GridViewTemplate("TextBox", dt.Columns[i].ColumnName.ToString(), DataControlRowType.DataRow);
                        num2 += 1;
                   // }
                }
                tf.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
                tf.ItemStyle.VerticalAlign = VerticalAlign.Top;
                //tf.ItemStyle.Width = 400;
                gv.Columns.Add(tf);
            }
            gv.Width = (num2 * 630) + (num1 * 60);
            gv.DataSource = dt;
            gv.DataBind();
        }
        catch (Exception)
        {

            this.Alert("当前任务还没有开发日志！");
            return;
        }
       
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        string _id = Request.QueryString["id"];
        if (txt_date.Text.Trim() == string.Empty)
        {
            this.Alert("日期不能为空！");
            return;
        }
        else if (txt_dec.Text.Trim() == string.Empty)
        {
            this.Alert("目标内容不能为空！");
            return;
        }
        if (!TaskHelper.updateTaskDevping(_id, txt_dec.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString(), "3",txt_date.Text.Trim()))
        {
            this.Alert("保存失败！");
            return;
        }
        else
        { 
            BindGridView(_id); 
        }
       
    }
}