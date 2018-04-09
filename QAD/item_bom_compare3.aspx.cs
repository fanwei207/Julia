using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using adamFuncs;
using Item;
using System.Drawing;
using System.Data.Odbc;

public partial class QAD_item_bom_compare : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    private void databind()
    {
        adamClass chk = new adamClass();
        CreateItemBom(chk.sqlEncode(txtQad.Text.Trim()));//创建了Qad结构临时表

        Item_Bom_Compare ibc = new Item_Bom_Compare();
        ibc.CreateProductTempTable1(chk.sqlEncode(txtCode.Text.Trim()), Convert.ToInt32(Session["uID"]), chkOnlyPakage.Checked);//创建了100库结构临时表

        DataTable dt = ibc.CompareItemStru1(Convert.ToInt32(Session["uID"]), txtLevel.Text.Trim(), chkOnlyPakage.Checked);
        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            this.gvCompare.DataSource = dt;
            this.gvCompare.DataBind();
            int ColunmCount = gvCompare.Rows[0].Cells.Count;
            gvCompare.Rows[0].Cells.Clear();
            gvCompare.Rows[0].Cells.Add(new TableCell());
            gvCompare.Rows[0].Cells[0].ColumnSpan = ColunmCount;
            gvCompare.Rows[0].Cells[0].Text = "没有数据";
            gvCompare.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            btnExport.Enabled = false;
        }
        else
        {
            this.gvCompare.DataSource = dt;
            this.gvCompare.DataBind();
            btnExport.Enabled = true;
        }
    }
 
    protected void gvCompare_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text.Trim().ToLower() == "&nbsp;")
            {
                e.Row.Cells[1].BackColor = Color.Red;
                e.Row.Cells[2].BackColor = Color.Red;
                e.Row.Cells[3].BackColor = Color.Red;
            }

            if (e.Row.Cells[4].Text.Trim().ToLower() == "&nbsp;")
            {
                e.Row.Cells[4].BackColor = Color.Red;
                e.Row.Cells[5].BackColor = Color.Red;
                e.Row.Cells[6].BackColor = Color.Red;
            }

            if (e.Row.Cells[1].Text.Trim().ToLower() != "&nbsp;" && e.Row.Cells[4].Text.Trim().ToLower() != "&nbsp;" && e.Row.Cells[2].Text.Trim() != e.Row.Cells[5].Text.Trim())
            {
                e.Row.Cells[2].BackColor = Color.Red;
                e.Row.Cells[5].BackColor = Color.Red;
            }
        }
    }
    /// <summary>
    /// 直接从QAD去ps_mstr递归结构。存入临时表tcpc0..item_bom_temp
    /// </summary>
    /// <param name="part"></param>
    protected void CreateItemBom(string part)
    {
        Item_Bom_Compare.DelItemTempTable(Convert.ToInt32(Session["uID"]));//删除tcpc0..item_bom_temp表里当前用户的记录

        if (part != null)
        {
            Item_Bom_Compare.InsertOrigin(part, part, Convert.ToInt32(Session["uID"]));//往tcpc0..item_bom_temp插入当前初始数据
            GetItemBomFromQad(part, 0);
        }
    }
    /// <summary>
    /// 展开指定零件的结构（可递归）
    /// </summary>
    /// <param name="partQad"></param>
    /// <param name="lel"></param>
    protected void GetItemBomFromQad(string partQad, int lel)
    {
        adamClass chk = new adamClass();
        if (lel > 9)
            return;

        string item_bom_par;
        string item_bom_comp;
        decimal item_bom_qty;
        Int64 item_bom_id = 0;

        SqlDataReader reader;
        reader = Item_Bom_Compare.GetOrigin(Convert.ToInt32(Session["uID"]), lel, partQad);
        while (reader.Read())
        {
            item_bom_id = Convert.ToInt64(reader[0].ToString());
            item_bom_par = reader[1].ToString();
            item_bom_comp = reader[2].ToString();
            item_bom_qty = Convert.ToDecimal(reader[3].ToString());

            OdbcDataReader dr;
            dr = Item_Bom_Compare.GetQadOrigin(chk.sqlEncode(txtDomain.Text.ToString().Trim()), partQad);
            while (dr.Read())
            {
                Item_Bom_Compare.InsertChild(item_bom_comp, dr.GetValue(0).ToString(), Convert.ToInt32(Session["uID"]), Convert.ToDecimal(dr.GetValue(1)), lel + 1);
                GetItemBomFromQad(dr.GetValue(0).ToString(), lel + 1);
            }
            dr.Close();
        }
        reader.Close();
    }
    protected void gvCompare_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                //总表头                
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();

                //第一行表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].RowSpan = 2;
                tcHeader[0].Text = "父件";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Attributes.Add("colspan", "3");
                tcHeader[1].Text = "Qad库";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[2].Attributes.Add("colspan", "3");
                tcHeader[2].Text = "100库</th></tr><tr class=\"GridViewHeaderStyle\">";

                //第二行表头
                tcHeader.Add(new TableHeaderCell());
                tcHeader[3].Text = "子件";
                tcHeader[3].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[4].Text = "单位用量";
                tcHeader[4].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Text = "层级";
                tcHeader[5].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Text = "子件";
                tcHeader[6].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Text = "单位用量";
                tcHeader[7].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Text = "层级";
                tcHeader[8].Style.Add("text-align", "center");
                break;
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.open('item_bom_compare3Excel.aspx?uId=" + Convert.ToInt32(Session["uID"]) + "&level=" + txtLevel.Text.Trim() + "')";
    }
    protected void btnCompare_Click(object sender, EventArgs e)
    {
        if (txtQad.Text.Trim() == "")
        {
            if (txtCode.Text.Trim() == "")
            {
                if (txtDomain.Text.Trim() == "")
                {
                    ltlAlert.Text = "alert('请输入Qad号、Code号、域名代码!')";
                }
                else
                {
                    ltlAlert.Text = "alert('请输入Qad号、Code号!')";
                }
            }
            else
            {
                if (txtDomain.Text.Trim() == "")
                {
                    ltlAlert.Text = "alert('请输入Qad号、域名代码!')";
                }
                else
                {
                    ltlAlert.Text = "alert('请输入Qad号!')";
                }
            }
        }
        else
        {
            if (txtCode.Text.Trim() == "")
            {
                if (txtDomain.Text.Trim() == "")
                {
                    ltlAlert.Text = "alert('请输Code号、域名代码!')";
                }
                else
                {
                    ltlAlert.Text = "alert('请输入Code号!')";
                }

            }
            else
            {
                if (txtDomain.Text.Trim() == "")
                {
                    ltlAlert.Text = "alert('请输入域名代码!')";
                }
                else
                {
                    databind();
                }
            }
        }
    }
}
