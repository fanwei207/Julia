using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EDI_EdiPoDetSplitLine : BasePage
{
    private DataTable Source
    {
        get
        {
            if (ViewState["SplitLine"] == null)
            {
                DataTable dt = new DataTable("SplitLine");
                DataColumn TempColumn;

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.Int32");
                TempColumn.ColumnName = "detId";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "poLine";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "qadPart";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "ordQty";
                dt.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "date";
                dt.Columns.Add(TempColumn);

                ViewState["SplitLine"] = dt;

            }
            return ViewState["SplitLine"] as DataTable;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblPoLine.Text = Request.QueryString["poLine"];
            lblPartNbr.Text = Request.QueryString["partNbr"];
            lblQadPart.Text = Request.QueryString["qadPart"];
            lblOrdQty.Text = Request.QueryString["ordQty"];
            lblSurplusQty.Text = Request.QueryString["ordQty"];
            string date = Request.QueryString["date"];
            DateTime Date = Convert.ToDateTime(date);
            Date = Date.AddDays(-1);
            lbldate.Text = Date.ToString("yyyy/MM/dd");
           
            BindEmptySource();
            ((TextBox)gvlist.FooterRow.FindControl("txtDate")).Text = lbldate.Text;
        }
        
    }

    private void BindEmptySource()
    {
        if (Source.Rows.Count == 0)
        {
            DataTable dt = new DataTable();
            DataColumn TempColumn;

            TempColumn = new DataColumn();
            TempColumn.DataType = System.Type.GetType("System.String");
            TempColumn.ColumnName = "poLine";
            dt.Columns.Add(TempColumn);

            TempColumn = new DataColumn();
            TempColumn.DataType = System.Type.GetType("System.String");
            TempColumn.ColumnName = "qadPart";
            dt.Columns.Add(TempColumn);

            TempColumn = new DataColumn();
            TempColumn.DataType = System.Type.GetType("System.String");
            TempColumn.ColumnName = "ordQty";
            dt.Columns.Add(TempColumn);

            TempColumn = new DataColumn();
            TempColumn.DataType = System.Type.GetType("System.String");
            TempColumn.ColumnName = "date";
            dt.Columns.Add(TempColumn);

            dt.Rows.Add(dt.NewRow());

            gvlist.DataSource = dt;
            gvlist.DataBind();

            gvlist.Rows[0].Cells.Clear();
            gvlist.Rows[0].Cells.Add(new TableCell());
            gvlist.Rows[0].Cells[0].ColumnSpan = 4;

        }
    }
    protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        decimal qty = decimal.Parse(Source.Rows[e.RowIndex]["ordQty"].ToString());       
        Source.Rows.RemoveAt(e.RowIndex);
        gvlist.DataSource = Source;
        gvlist.DataBind();
        lblSurplusQty.Text = (decimal.Parse(lblSurplusQty.Text) + qty).ToString();
    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddLine")
        {
            try
            {
                string detId = Request.QueryString["detId"];
                //Get the values stored in the text boxes
                string line = ((TextBox)gvlist.FooterRow.FindControl("txtLine")).Text.Trim();
                string part = ((TextBox)gvlist.FooterRow.FindControl("txtPart")).Text.Trim();
                string qty = ((TextBox)gvlist.FooterRow.FindControl("txtQty")).Text.Trim();
                if (line == "")
                {
                    this.Alert("请输入行号！");
                    return;
                }
                else if (Source.Select("poLine=" + line).Length > 0)
                {
                    this.Alert("行号已存在！");
                    return;
                }
                else if (getEdiData.CheckPoLineExists(detId, line))
                {
                    this.Alert("行号已存在！");
                    return;
                }
                //else if (part == "")
                //{
                //    this.Alert("请输入QAD号！");
                //    return;
                //}
                else if (qty == "")
                {
                    this.Alert("请输入数量！");
                    return;
                }
                else if (decimal.Parse(qty) > decimal.Parse(lblSurplusQty.Text))
                {
                    this.Alert("请输入的数量大于分配剩余数量！");
                    return;
                }

                DataRow row = Source.NewRow();
                row["detId"] = detId;
                row["poLine"] = line;
                row["qadPart"] = part;
                row["ordQty"] = qty;
                row["date"] = lbldate.Text.Trim();
                Source.Rows.Add(row);
                lblSurplusQty.Text = (decimal.Parse(lblSurplusQty.Text) - decimal.Parse(qty)).ToString();
                gvlist.DataSource = Source;
                gvlist.DataBind();

                string date = lbldate.Text.Trim();
                DateTime Date = Convert.ToDateTime(date);
                Date = Date.AddDays(1);
                lbldate.Text = Date.ToString("yyyy/MM/dd");
                ((TextBox)gvlist.FooterRow.FindControl("txtDate")).Text = lbldate.Text;
                
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message.ToString().Replace("'", "") + "');</script>");
            }
        }
    }
    protected void btnSure_Click(object sender, EventArgs e)
    {
        if (decimal.Parse(lblSurplusQty.Text) != 0)
        {
            this.Alert("订单数量未分配结束！");
            return;
        }
        string detId = Request.QueryString["detId"];
        if (getEdiData.PoDetSplitLine(detId, Source))
        {
            ltlAlert.Text = "$('body', $('body', parent.document).find('#ifrm_10000300').contents()).find('#btnQuery').click();$.loading('none');$('BODY', parent.parent.parent.document).find('#JULIA-MODAL-DIALOG').remove();";
        }
        else
        {
            ltlAlert.Text = "alert('拆分行失败');";
        }
    }
}