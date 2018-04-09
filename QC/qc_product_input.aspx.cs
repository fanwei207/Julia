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
using QCProgress;
using adamFuncs;
using System.Drawing;

public partial class QC_qc_product_input : BasePage
{
    adamClass adam = new adamClass();
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected string FormType
    {
        get
        {
            if (Request.QueryString["type"] != null)
            {
                return Request.QueryString["type"];
            }
            else
            {
                return "";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblLine.Text = Request.QueryString["line"].ToString().Replace(";","<br>");
            lblOrder.Text = Request.QueryString["ord"].ToString().Replace(";", "<br>");
            lblRcvd.Text = Request.QueryString["rec"].ToString();
            lblPart.Text = Request.QueryString["part"].ToString();
            lblID.Text = Request.QueryString["ID"].ToString();

            hidIsTimeUp.Value = oqc.getProductCanUpdate(lblID.Text);

            dropType.DataSource = oqc.GetProductDefectType(Session["uID"].ToString());
            dropType.DataBind();
            string selectedValue=oqc.GetProductDefectTypeById(lblID.Text).ToString();
            if (selectedValue != "0")
            {
                dropType.Items.FindByValue(selectedValue).Selected = true;
            }

            txtHNum.Text = oqc.GetCP100(lblID.Text);
            if (FormType == "read" || Convert.ToBoolean(hidIsTimeUp.Value))
            {
                btnSave.Visible = false;
            }

            btnBack.Attributes.Add("onclick", "window.close();");

            GridTypeBind();
            GridProcedureBind(0, 0);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("qc_product.aspx", false);
    }

    protected void GridTypeBind()
    {
        int tID = int.Parse(dropType.SelectedValue);
        int prdID = int.Parse(lblID.Text);

        dgProcedure.DataSource = oqc.GetProductItem(prdID, tID);
        dgProcedure.DataBind();
    }

    protected void GridProcedureBind(int prcdID, int prcdItemID)
    {
        dgDefect.DataSource = oqc.GetProductDefect(prcdID, prcdItemID);
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
            if (FormType == "read" || Convert.ToBoolean(hidIsTimeUp.Value))
            {
                ((Button)e.Item.Cells[7].FindControl("btnAdd")).Text = "�鿴";
            }

           
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


            if (Convert.ToBoolean(hidIsTimeUp.Value))
            {
                ((DropDownList)e.Item.FindControl("dropLevel")).Enabled = false;
                ((DropDownList)e.Item.FindControl("dropAql")).Enabled = false;
            }
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

            if (Convert.ToBoolean(hidIsTimeUp.Value))
            {
                txtNum.Enabled = false;
            }


            nTotal += int.Parse(e.Item.Cells[4].Text);

            if (e.Item.Cells[5].Text.Trim() == "False")
            {
                e.Item.BackColor = Color.Chocolate;
            }

        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            e.Item.Cells[1].Text = "�ϼ�";
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
                if (FormType != "read")
                {
                    foreach (DataGridItem item in dgDefect.Items)
                    {
                        TextBox txtNum = (TextBox)item.Cells[2].FindControl("txtNum");
                        string orgNum = item.Cells[4].Text.Trim();

                        if (txtNum.Text.Trim() == string.Empty)
                        {
                            ltlAlert.Text = "alert('����������Ϊ��!');";
                            bFlag = false;
                            break;
                        }
                        else
                        {
                            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
                            if (!regex.IsMatch(txtNum.Text.Trim()))
                            {
                                ltlAlert.Text = "alert('�����ĸ�ʽ����!');";
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
                        ltlAlert.Text = "alert('��ѡ�����ˮƽ��AQLֵ!');";
                        bFlag = false;
                    }
                    else if (dropLevel.SelectedIndex == 0 && dropAql.SelectedIndex == 0 && num != 0)
                    {
                        ltlAlert.Text = "alert('��ѡ�����ˮƽ��AQLֵ!');";
                        bFlag = false;
                    }

                    if (!(bPro && bDef)) //����������û�б����ģ���ô�Ͳ������ݿ����
                    {
                        if (bFlag && !oqc.AddProductItem(prcID, typeID, prcdID, Level, Aql, strDef, strNum, bPro, bDef, qty, num))
                        {
                            ltlAlert.Text = "alert('����ʧ��!');";
                            bFlag = false;
                        }
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
                        lbError.Text = "����ʧ��!";
                    }
                }
            }
            catch
            {
                txtHNum.Text = string.Empty;
                lbError.Visible = true;
                lbError.Text = "����ʧ��!";
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

    /// <summary>
    /// 20150807�����İ�ť
    /// fanwei
    /// ������дһ���ߴ����Ϣ��
    /// ��Ϊ�����Ƿ�ϸ�ļ����׼
    /// �������ƽ�������ƽ��������Ϊ
    /// �����һ����׼
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSize_Click(object sender, EventArgs e)
    {

        ltlAlert.Text = "$.window('�ߴ�', 900, 600, 'qc_product_input_size.aspx?ID=" + lblID.Text + "&part=" + Request.QueryString["part"] + "', '', true);";

    }



}
