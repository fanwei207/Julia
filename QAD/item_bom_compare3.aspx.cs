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
        CreateItemBom(chk.sqlEncode(txtQad.Text.Trim()));//������Qad�ṹ��ʱ��

        Item_Bom_Compare ibc = new Item_Bom_Compare();
        ibc.CreateProductTempTable1(chk.sqlEncode(txtCode.Text.Trim()), Convert.ToInt32(Session["uID"]), chkOnlyPakage.Checked);//������100��ṹ��ʱ��

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
            gvCompare.Rows[0].Cells[0].Text = "û������";
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
    /// ֱ�Ӵ�QADȥps_mstr�ݹ�ṹ��������ʱ��tcpc0..item_bom_temp
    /// </summary>
    /// <param name="part"></param>
    protected void CreateItemBom(string part)
    {
        Item_Bom_Compare.DelItemTempTable(Convert.ToInt32(Session["uID"]));//ɾ��tcpc0..item_bom_temp���ﵱǰ�û��ļ�¼

        if (part != null)
        {
            Item_Bom_Compare.InsertOrigin(part, part, Convert.ToInt32(Session["uID"]));//��tcpc0..item_bom_temp���뵱ǰ��ʼ����
            GetItemBomFromQad(part, 0);
        }
    }
    /// <summary>
    /// չ��ָ������Ľṹ���ɵݹ飩
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
                //�ܱ�ͷ                
                TableCellCollection tcHeader = e.Row.Cells;
                tcHeader.Clear();

                //��һ�б�ͷ
                tcHeader.Add(new TableHeaderCell());
                tcHeader[0].RowSpan = 2;
                tcHeader[0].Text = "����";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[1].Attributes.Add("colspan", "3");
                tcHeader[1].Text = "Qad��";

                tcHeader.Add(new TableHeaderCell());
                tcHeader[2].Attributes.Add("colspan", "3");
                tcHeader[2].Text = "100��</th></tr><tr class=\"GridViewHeaderStyle\">";

                //�ڶ��б�ͷ
                tcHeader.Add(new TableHeaderCell());
                tcHeader[3].Text = "�Ӽ�";
                tcHeader[3].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[4].Text = "��λ����";
                tcHeader[4].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[5].Text = "�㼶";
                tcHeader[5].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[6].Text = "�Ӽ�";
                tcHeader[6].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[7].Text = "��λ����";
                tcHeader[7].Style.Add("text-align", "center");

                tcHeader.Add(new TableHeaderCell());
                tcHeader[8].Text = "�㼶";
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
                    ltlAlert.Text = "alert('������Qad�š�Code�š���������!')";
                }
                else
                {
                    ltlAlert.Text = "alert('������Qad�š�Code��!')";
                }
            }
            else
            {
                if (txtDomain.Text.Trim() == "")
                {
                    ltlAlert.Text = "alert('������Qad�š���������!')";
                }
                else
                {
                    ltlAlert.Text = "alert('������Qad��!')";
                }
            }
        }
        else
        {
            if (txtCode.Text.Trim() == "")
            {
                if (txtDomain.Text.Trim() == "")
                {
                    ltlAlert.Text = "alert('����Code�š���������!')";
                }
                else
                {
                    ltlAlert.Text = "alert('������Code��!')";
                }

            }
            else
            {
                if (txtDomain.Text.Trim() == "")
                {
                    ltlAlert.Text = "alert('��������������!')";
                }
                else
                {
                    databind();
                }
            }
        }
    }
}
