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
using System.Text.RegularExpressions;

public partial class QC_qc_aql : BasePage
{
    QC oqc = new QC();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //bind aqlCode
            dropCode.DataSource = oqc.GetAqlCode("");
            dropCode.DataBind();
            dropCode.Items.Insert(0, new ListItem("--", "--"));

            //bind aqlLevel
            dropAqlLevel.DataSource = oqc.GetAqlLevel("");
            dropAqlLevel.DataBind();
            dropAqlLevel.Items.Insert(0, new ListItem("--", "--"));


            oqc.DynamicBindgvAql(oqc.GetAql(), this.gvAql);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string strInt =  @"^\d+$";
        bool blFalg = false;
        if (dropCode.SelectedIndex == 0)
        {
            lblCode.Visible = true;
            blFalg = true;
        }
        else 
        {
            lblCode.Visible = false;
        }

        if (dropAqlLevel.SelectedIndex == 0)
        {
            lblAql.Visible = true;
            blFalg = true;
        }
        else
        {
            lblAql.Visible = false;
        }

        if (dropWay.SelectedIndex == 0)
        {
            lblWay.Visible = true;
            blFalg = true;
        }
        else
        {
            lblWay.Visible = false;
        }
        if (dropWay.SelectedItem.Text == "NONE")
        {
            if (txtAc.Text.Length == 0)
            {
                ltlAlert.Text = "alert('AC不能为空');";
                return;
            }
            else 
            {
                if (!new Regex(strInt).IsMatch(txtAc.Text.Trim()))
                {
                    ltlAlert.Text = "alert('AC必须为整数');";
                    return;
                }
            }

            if (txtRe.Text.Length == 0)
            {
                ltlAlert.Text = "alert('RE不能为空');";
                return;
            }
            else
            {
                if (!new Regex(strInt).IsMatch(txtRe.Text.Trim()))
                {
                    ltlAlert.Text = "alert('RE必须为整数');";
                    return;
                }
            }

            if (int.Parse(txtAc.Text.Trim()) >= int.Parse(txtRe.Text.Trim()))
            {
                lblCompare.Visible = true;
                blFalg = true;
            }
            else
            {
                lblCompare.Visible = false;
            }
        }
        if (!blFalg)
        {
            string strMsg = "";
            oqc.ModifyAql(dropCode.SelectedItem.Text, dropAqlLevel.SelectedItem.Text, txtAc.Text.Trim(), txtRe.Text.Trim(), dropWay.SelectedItem.Value, int.Parse(Session["uID"].ToString()), ref strMsg);

            if (strMsg != "") 
            {
                ltlAlert.Text = "alert('" + strMsg + "');";

                oqc.DynamicBindgvAql(oqc.GetAql(), this.gvAql);
                return;
            }
        }

        oqc.DynamicBindgvAql(oqc.GetAql(), this.gvAql);
    }
    protected void dropWay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(dropWay.SelectedIndex == 3)
        {
            lblAc.Visible = true;
            lblRe.Visible = true;

            txtAc.Visible = true;
            txtRe.Visible = true;
        }
        else
        {
            lblAc.Visible = false;
            lblRe.Visible = false;

            txtAc.Visible = false;
            txtRe.Visible = false;
        }

        oqc.DynamicBindgvAql(oqc.GetAql(), this.gvAql);
    }
    protected void gvAql_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header) 
        {
            e.Row.Cells[0].Text = "字码";
            e.Row.Cells[1].Text = "样本量";
        }

        if (e.Row.RowType == DataControlRowType.DataRow) 
        {
            int intAc;
            int intRe;
            int intWay;
            string str;
            for (int i = 2; i < e.Row.Cells.Count; i++) 
            {
                str = ((Label)e.Row.Cells[i].Controls[0]).Text.Trim();
                intAc = int.Parse(str.Split(' ')[0]);
                intRe = int.Parse(str.Split(' ')[1]);
                intWay = int.Parse(str.Split(' ')[2]);

                if (intWay == 0)
                {
                    e.Row.Cells[i].Text = "&uarr;";
                    e.Row.Cells[i].Style.Add("font-weight", "bold");
                }
                else if (intWay == 1)
                {
                    e.Row.Cells[i].Text = "&darr;";
                    e.Row.Cells[i].Style.Add("font-weight", "bold");
                }
                else
                {
                    e.Row.Cells[i].Text = intAc.ToString() + "\t" + intRe.ToString();
                }
            }
        }
    }
}
