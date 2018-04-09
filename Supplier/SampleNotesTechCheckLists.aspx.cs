using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SampleManagement;


public partial class supplier_SampleNotesLists : BasePage
{
    Sample sap = new Sample();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            txt_CreatedDate1.Text = System.DateTime.Now.Date.AddMonths(-2).ToString("yyyy-MM-dd");
            BindVend();
             
            Bindgv_Bos(); 
        }
    }

    private void Bindgv_Bos()
    {
        string strBosnbr = txt_bosnbr.Text.Trim().ToString();
        string strVend = ddl_vend.SelectedValue.ToString();
        DateTime createdDate1;
        DateTime createdDate2;
        if (txt_CreatedDate1.Text.Trim() != string.Empty)
        {
            try
            {
                createdDate1 = Convert.ToDateTime(txt_CreatedDate1.Text);

            }
            catch
            {
                this.Alert("打样单生成日期输入格式不对！");
                return;
            }
        }
        else
        {
            createdDate1 = Convert.ToDateTime("1900-01-01");
        }

        if (txt_CreatedDate2.Text.Trim() != string.Empty)
        {
            try
            {
                createdDate2 = Convert.ToDateTime(txt_CreatedDate2.Text);
            }
            catch
            {
                this.Alert("打样单生成日期输入格式不对！");
                return;
            }
        }
        else
        {
            createdDate2 = Convert.ToDateTime("1900-01-01");
        }
         
        string checkValue = ddl_TechValue.SelectedValue;
        DataTable dt = sap.getBosMstrToCheck(strBosnbr, strVend, createdDate1, createdDate2, checkValue,"Tech");
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            gv_bos_mstr.DataSource = dt;
            gv_bos_mstr.DataBind();
            int columnCount = gv_bos_mstr.Rows[0].Cells.Count;
            gv_bos_mstr.Rows[0].Cells.Clear();
            gv_bos_mstr.Rows[0].Cells.Add(new TableCell());
            gv_bos_mstr.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv_bos_mstr.Rows[0].Cells[0].Text = "没有记录";
            gv_bos_mstr.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            gv_bos_mstr.DataSource = dt;
            gv_bos_mstr.DataBind();
        }
    }

    protected void BindVend()
    {
        ddl_vend.DataSource = sap.getBosSuppliers((SysRole)Enum.Parse(typeof(SysRole), "Supplier", true));//ddlUserType.SelectedValue

        ddl_vend.DataBind();
        ddl_vend.Items.Insert(0, new ListItem("--", "0"));

    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txt_bosnbr.Text = "";
        ddl_vend.SelectedIndex = -1; 
        txt_CreatedDate1.Text = System.DateTime.Now.Date.AddDays(-7).ToString("yyyy-MM-dd");
        txt_CreatedDate2.Text = "";
        Bindgv_Bos();

    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        Bindgv_Bos();
    }

    protected void gv_bos_mstr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            if(gv_bos_mstr.DataKeys[e.Row.RowIndex].Values[1].ToString().ToLower() == "true")
            {
                e.Row.Cells[5].Text = "已确认";
            }
            else 
            {
                e.Row.Cells[5].Text = " "; 
            }
            if ( gv_bos_mstr.DataKeys[e.Row.RowIndex].Values[2].ToString().ToLower() == "true")
            {
                e.Row.Cells[6].Text = "已收";
            }
            else
            {
                e.Row.Cells[6].Text = " ";
            }

        }

    }

    protected void gv_bos_mstr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_bos_mstr.PageIndex = e.NewPageIndex;
        Bindgv_Bos();
    }

    protected void gv_bos_mstr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail")
        {
            string bosNbr = e.CommandArgument.ToString();

            Response.Redirect("SampleNotesTechCheckMaintain.aspx?bos_nbr=" + bosNbr);
        }
        else if (e.CommandName.ToString() == "DoMaintain") 
        {
            string strbos_nbr = e.CommandArgument.ToString();
            Response.Redirect("SampleNotesTechCheckMaintain.aspx?bos_nbr=" + strbos_nbr); 
        }
    }
}
