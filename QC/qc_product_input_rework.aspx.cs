using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using QCProgress;
using adamFuncs;
using System.Drawing;
public partial class QC_qc_product_input_rework : BasePage
{
    adamClass adam = new adamClass();
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblLine.Text = Request.QueryString["line"].ToString().Replace(";", "<br>");
            lblOrder.Text = Request.QueryString["ord"].ToString().Replace(";", "<br>");
            lblRcvd.Text = Request.QueryString["rec"].ToString();
            lblPart.Text = Request.QueryString["part"].ToString();
            lblID.Text = Request.QueryString["ID"].ToString();

            dropType.DataSource = oqc.GetProductDefectType(Session["uID"].ToString());
            dropType.DataBind();

            txtHNum.Text = oqc.GetCP100Rework(lblID.Text);

            btnBack.Attributes.Add("onclick", "window.close();");

            GridTypeBind();
            GridProcedureBind(0, 0);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("qc_product_rework.aspx");
    }

    protected void GridTypeBind()
    {
        int tID = int.Parse(dropType.SelectedValue);
        int prdID = int.Parse(lblID.Text);

        dgProcedure.DataSource = oqc.GetProductItemRework(prdID, tID);
        dgProcedure.DataBind();
    }

    protected void GridProcedureBind(int prcdID, int prcdItemID)
    {
        dgDefect.DataSource = oqc.GetProductDefectRework(prcdID, prcdItemID);
        dgDefect.DataBind();
    }

    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgProcedure.SelectedIndex = -1;
        GridProcedureBind(0, 0);

        if (dropType.SelectedIndex != 0)
        {
            txtHNum.Text = oqc.GetCP100(lblID.Text);

            GridTypeBind();
        }
    }
    protected void dgProcedure_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            if (e.Item.ItemIndex != -1)
            {
                int id = e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = id.ToString();
            }

            e.Item.Cells[1].Style.Add("text-align", "left");

            if (e.Item.Cells[5].Text.Trim() != "0")
                e.Item.Cells[2].Text = e.Item.Cells[5].Text.Trim();
            else
                e.Item.Cells[2].Text = string.Empty;

            if (Convert.ToDouble(e.Item.Cells[6].Text.Trim()) != -1)
                e.Item.Cells[3].Text = e.Item.Cells[6].Text.Trim();
            else
                e.Item.Cells[3].Text = string.Empty;

            ((Button)e.Item.Cells[7].FindControl("btnAdd")).Visible = true;
        }
        else if (e.Item.ItemType == ListItemType.SelectedItem)
        {
            if (e.Item.ItemIndex != -1)
            {
                int id = e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = id.ToString();
            }

            DropDownList dropLevel = (DropDownList)e.Item.Cells[2].FindControl("dropLevel");
            DropDownList dropAql = (DropDownList)e.Item.Cells[3].FindControl("dropAql");

            dropLevel.Attributes.Add("onchange", "document.getElementById('" + e.Item.ClientID + "').style.color='red';");
            dropAql.Attributes.Add("onchange", "document.getElementById('" + e.Item.ClientID + "').style.color='red';");

            dropLevel.DataSource = oqc.GetGbtLevel(string.Empty);
            dropLevel.DataBind();
            dropLevel.Items.Insert(0, new ListItem("--", "0"));

            dropLevel.SelectedValue = e.Item.Cells[5].Text.Trim();

            dropAql.DataSource = oqc.GetAqlLevel(" Where AQL < 15");
            dropAql.DataBind();
            dropAql.Items.Insert(0, new ListItem("--", "-1"));

            dropAql.SelectedValue = e.Item.Cells[6].Text.Trim();

            ((Button)e.Item.Cells[7].FindControl("btnAdd")).Visible = false;
        }
    }

    int nTotal = 0;
    protected void dgDefect_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            if (e.Item.ItemIndex != -1)
            {
                int id = e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = id.ToString();
            }

            e.Item.Cells[1].Style.Add("text-align", "left");

            TextBox txtNum = (TextBox)e.Item.Cells[2].FindControl("txtNum");
            txtNum.Text = e.Item.Cells[4].Text;

            nTotal += int.Parse(e.Item.Cells[4].Text);

            if (e.Item.Cells[5].Text.Trim() == "False")
            {
                e.Item.BackColor = Color.Chocolate;
            }

        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            e.Item.Cells[1].Text = "合计";
            e.Item.Cells[2].Text = nTotal.ToString();

            e.Item.Cells[1].Style.Add("text-align", "right");
        }
    }
    protected void dgProcedure_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            bool bFlag = true;
            bool bPro = false;
            bool bDef = true;

            int prcdID = 0;
            int prcdItemID = 0;
            int qty = int.Parse(lblRcvd.Text.Trim());

            if (dgProcedure.SelectedIndex > -1)
            {
                DropDownList dropLevel = (DropDownList)dgProcedure.SelectedItem.Cells[2].FindControl("dropLevel");
                DropDownList dropAql = (DropDownList)dgProcedure.SelectedItem.Cells[3].FindControl("dropAql");

                int prcID = int.Parse(lblID.Text);
                int typeID = int.Parse(dropType.SelectedValue);
                string Level = dropLevel.SelectedValue;
                decimal Aql = decimal.Parse(dropAql.SelectedValue);

                string orgLevel = dgProcedure.SelectedItem.Cells[5].Text.Trim();
                decimal orgAql = decimal.Parse(dgProcedure.SelectedItem.Cells[6].Text.Trim());

                if (orgLevel == Level && orgAql == Aql)
                    bPro = true;

                prcdID = int.Parse(dgProcedure.SelectedItem.Cells[12].Text);

                string strDef = "";
                string strNum = "";
                int num = 0;

                foreach (DataGridItem item in dgDefect.Items)
                {
                    TextBox txtNum = (TextBox)item.Cells[2].FindControl("txtNum");
                    string orgNum = item.Cells[4].Text.Trim();

                    if (txtNum.Text.Trim() == string.Empty)
                    {
                        ltlAlert.Text = "alert('数量不能有为空!');";
                        bFlag = false;
                        break;
                    }
                    else
                    {
                        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
                        if (!regex.IsMatch(txtNum.Text.Trim()))
                        {
                            ltlAlert.Text = "alert('数量的格式不对!');";
                            bFlag = false;
                            break;
                        }
                    }

                    num += int.Parse(txtNum.Text.Trim());

                    if (txtNum.Text.Trim() != orgNum)
                    {
                        bDef = false;

                        strDef += item.Cells[3].Text.Trim() + ";";
                        strNum += txtNum.Text.Trim() + ";";
                    }
                }

                if ((dropLevel.SelectedIndex == 0 || dropAql.SelectedIndex == 0) && !(dropLevel.SelectedIndex == 0 && dropAql.SelectedIndex == 0))
                {
                    ltlAlert.Text = "alert('请选择检验水平或AQL值!');";
                    bFlag = false;
                }
                else if (dropLevel.SelectedIndex == 0 && dropAql.SelectedIndex == 0 && num != 0)
                {
                    ltlAlert.Text = "alert('请选择检验水平或AQL值!');";
                    bFlag = false;
                }

                if (!(bPro && bDef)) //若左、右数据没有被更改，那么就不做数据库操作
                {
                    if (bFlag && !oqc.AddProductItemRework(prcID, typeID, prcdID, Level, Aql, strDef, strNum, bPro, bDef, qty, num))
                    {
                        ltlAlert.Text = "alert('保存失败!');";
                        bFlag = false;
                    }
                }
            }
            if (bFlag)
                dgProcedure.SelectedIndex = e.Item.ItemIndex;

            prcdID = int.Parse(dgProcedure.SelectedItem.Cells[12].Text.Trim());
            prcdItemID = int.Parse(dgProcedure.SelectedItem.Cells[4].Text.Trim());

            GridTypeBind();
            GridProcedureBind(prcdID, prcdItemID);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtHNum.Text.Trim() != string.Empty)
        {
            try
            {
                Int32 n = Convert.ToInt32(txtHNum.Text.Trim());

                if (n > 0)
                {
                    if (oqc.SaveCP100(lblID.Text, lblOrder.Text.Trim(), lblLine.Text.Trim(), dropType.SelectedValue.Trim(), lblSerial.Text, Convert.ToInt32(txtHNum.Text), Session["uName"].ToString(), Convert.ToBoolean(Session["TCP"].ToString())))
                    {
                        txtHNum.BackColor = Color.LawnGreen;
                        lbError.Visible = false;
                    }
                    else
                    {
                        txtHNum.BackColor = Color.Red;
                        lbError.Visible = true;
                        lbError.Text = "保存失败!";
                    }
                }
            }
            catch
            {
                txtHNum.Text = string.Empty;
                lbError.Visible = true;
                lbError.Text = "保存失败!";
            }
        }

        if (dgProcedure.SelectedItem != null)
        {
            dgProcedure_ItemCommand(this, new DataGridCommandEventArgs(dgProcedure.SelectedItem, (Button)dgProcedure.SelectedItem.Cells[7].FindControl("btnAdd"), new CommandEventArgs("Add", null)));
        }
        else
        {
            GridTypeBind();
        }
    }
}