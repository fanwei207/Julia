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

public partial class QC_qc_report_complete : BasePage
{
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
            if (FormType == "read")
            {
                tbHeader.Visible = false;
                txtReceiver.Text = Request.QueryString["receiver"];
                txtOrder.Text  = Request.QueryString["poNbr"];
                txtLine.Text = Request.QueryString["line"];
                txtPart.Text = Request.QueryString["part"];
            }
            else
            {
                txtStd.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
                txtEnd.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            }

            txtPageIndex.Text = "1";
            txtIndex.Text = "0";



            GridViewBind();
        }
        else
        {
            this.gvReport_RowDataBound(this, new GridViewRowEventArgs(gvReport.FooterRow));
        }
    }
    protected void GridViewBind()
    {
        bool bFlag = true;

        if (bFlag && txtStd.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _date = Convert.ToDateTime(txtStd.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('起始日期格式不正确');Form1.txtStd.focus();";
                bFlag = false;
            }
        }

        if (bFlag && txtEnd.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _date = Convert.ToDateTime(txtEnd.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('结束日期格式不正确');Form1.txtEnd.focus();";
                bFlag = false;
            }
        }

        if (bFlag)
        {
            int uID = int.Parse(Session["uID"].ToString());
            int plantid = int.Parse(Session["PlantCode"].ToString());

            DataSet _dataset = oqc.GetReportComplete(txtReceiver.Text.Trim(),
                                            txtOrder.Text.Trim(),
                                            txtLine.Text.Trim(),
                                            txtPart.Text.Trim(),
                                            txtCus.Text.Trim(),
                                            txtStd.Text.Trim(),
                                            txtEnd.Text.Trim(),
                                            Int32.Parse(DropDownList1.SelectedValue),
                                            23,
                                            Int32.Parse(txtPageIndex.Text.Trim()),
                                            uID,
                                            plantid);

            if (_dataset.Tables[0].Rows.Count < 1)
                gvReport.ShowFooter = false;
            else
                gvReport.ShowFooter = true;

            txtPageCount.Text = _dataset.Tables[1].Rows[0][0].ToString();

            ogv.GridViewDataBind(gvReport, _dataset.Tables[0]);
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        string strIndex = ((TextBox)gvReport.FooterRow.Cells[0].FindControl("txtIndex")).Text;

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[1-9]\d{0,}$");
        if (regex.IsMatch(strIndex))
        {
            if (int.Parse(strIndex) <= int.Parse(txtPageCount.Text))
            {
                txtPageIndex.Text = ((TextBox)gvReport.FooterRow.Cells[0].FindControl("txtIndex")).Text;
                txtIndex.Text = Convert.ToString((int.Parse(txtPageIndex.Text) - 1) / 10);
            }
            else
                ltlAlert.Text = "alert('输入的页码超出了范围');";
        }
        else
            ltlAlert.Text = "alert('输入的页码必须为大于零的整数');";

        GridViewBind();
    }
    protected void btnLink_Click(object sender, EventArgs e)
    {
        txtPageIndex.Text = ((LinkButton)sender).Text;

        GridViewBind();
    }
    protected void btnIndexAdd_Click(object sender, EventArgs e)
    {
        int nCount = int.Parse(txtPageCount.Text);
        int nIndex = int.Parse(txtIndex.Text);

        if ((nIndex + 1) * 10 <= nCount)
        {
            txtIndex.Text = Convert.ToString(nIndex + 1);
            txtPageIndex.Text = Convert.ToString((nIndex + 1) * 10 + 1);
        }

        GridViewBind();
    }
    protected void btnIndexSub_Click(object sender, EventArgs e)
    {
        int nCount = int.Parse(txtPageCount.Text);
        int nIndex = int.Parse(txtIndex.Text);

        if (nIndex - 1 >= 0 && (nIndex - 1) * 10 <= nCount)
        {
            txtIndex.Text = Convert.ToString(nIndex - 1);
            txtPageIndex.Text = Convert.ToString((nIndex - 1) * 10 + 1);
        }

        GridViewBind();
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[11].Text.Trim() == "1")
                e.Row.Cells[11].Text = "免检";
            else if (e.Row.Cells[11].Text.Trim() == "2")
                e.Row.Cells[11].Text = "完成";
            else if (e.Row.Cells[11].Text.Trim() == "3")
                e.Row.Cells[11].Text = "特采";
            else if (e.Row.Cells[11].Text.Trim() == "4")
                e.Row.Cells[11].Text = "退货";

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[0-9]+$");
            if (regex.IsMatch(gvReport.DataKeys[e.Row.RowIndex].Values[1].ToString()))
            {
                if (int.Parse(gvReport.DataKeys[e.Row.RowIndex].Values[1].ToString()) != 0)
                    e.Row.Cells[10].Text = "NO";
                else
                    e.Row.Cells[10].Text = "OK";
            }

            e.Row.Cells[1].Text = e.Row.Cells[1].Text.Replace(";", "</br>");
            e.Row.Cells[3].Text = e.Row.Cells[3].Text.Replace(";", "</br>");
            e.Row.Cells[6].Text = e.Row.Cells[6].Text.Replace(";", "</br>");
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Controls.Clear();

            TableCell cell = new TableCell();
            cell.ColumnSpan = 14;

            Label label1 = new Label();
            label1.Text = string.Empty;
            label1.Width = Unit.Pixel(20);
            cell.Controls.Add(label1);

            int nPageIndex = int.Parse(txtPageIndex.Text);
            int nPageCount = int.Parse(txtPageCount.Text);
            int nIndex = int.Parse(txtIndex.Text);
            int nRow = (nPageCount - nIndex * 10) > 10 ? 10 : (nPageCount - nIndex * 10);

            if (nPageIndex > 10)
            {
                LinkButton linkSub = new LinkButton();
                linkSub.ID = "linkIndexSub";
                linkSub.Width = Unit.Pixel(12);
                linkSub.Text = "&nbsp;&nbsp;...";
                linkSub.Click += new EventHandler(this.btnIndexSub_Click);

                cell.Controls.Add(linkSub);
            }

            for (int i = 1; i <= nRow; i++)
            {
                LinkButton link = new LinkButton();
                link.Text = Convert.ToString(i + nIndex * 10).ToString();
                link.Width = Unit.Pixel(12);
                link.Height = Unit.Pixel(15);
                link.Style.Add("padding-left", "5px");
                link.Click += new EventHandler(this.btnLink_Click);

                if ((i + nIndex * 10) == Int64.Parse(txtPageIndex.Text))
                {
                    link.Font.Size = FontUnit.Point(12);
                    link.Font.Bold = true;
                }

                cell.Controls.Add(link);
            }

            if (nRow == 10)
            {
                LinkButton linkAdd = new LinkButton();
                linkAdd.ID = "linkIndexAdd";
                linkAdd.Width = Unit.Pixel(12);
                linkAdd.Text = "&nbsp;&nbsp;...";
                linkAdd.Click += new EventHandler(this.btnIndexAdd_Click);

                cell.Controls.Add(linkAdd);
            }

            Label label2 = new Label();
            label2.Text = string.Empty;
            label2.Width = Unit.Pixel(30);
            cell.Controls.Add(label2);

            TextBox txt = new TextBox();
            txt.ID = "txtIndex";
            txt.Width = Unit.Pixel(30);
            txt.Text = txtPageIndex.Text;
            txt.CssClass = "SmallTextBox";
            cell.Controls.Add(txt);

            Label label3 = new Label();
            label3.Text = "/" + txtPageCount.Text;
            label3.Style.Add("font-size", "12px");
            label3.Style.Add("color", "Silver");
            label3.Style.Add("text-decoration", "none");
            cell.Controls.Add(label3);

            Button btn = new Button();
            btn.Text = "GO";
            btn.ID = "btnGo";
            btn.Height = Unit.Pixel(20);
            btn.Click += new EventHandler(this.btnGo_Click);
            cell.Controls.Add(btn);

            e.Row.Controls.Add(cell);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtPageIndex.Text = "1";
        txtIndex.Text = "0";

        GridViewBind();
    }
    protected void gvReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "link")
        {
            LinkButton link = (LinkButton)(e.CommandSource);
            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;
            string _group = gvReport.DataKeys[index].Values["prh_group"].ToString();
            string flag = gvReport.DataKeys[index].Values["flag"].ToString();
            string Identity = gvReport.DataKeys[index].Values["Identity"].ToString();

            if (link.ID.Trim() == "LinkButton1")
            {
                if (flag != string.Empty)
                    Response.Redirect("qc_report_project.aspx?page=100103110&group=" + _group + "&id=" + Identity + "&type=" + FormType + "&ponbr=" + txtOrder.Text + "&receiver=" + txtReceiver.Text + "&line=" + txtLine.Text + "&part=" + txtPart.Text);
                else
                    Response.Redirect("qc_report_history.aspx?page=100103110&group=" + _group + "&id=" + Identity + "&type=" + FormType + "&ponbr=" + txtOrder.Text + "&receiver=" + txtReceiver.Text + "&line=" + txtLine.Text + "&part=" + txtPart.Text);
            }

            else
                Response.Redirect("qc_luminousflux_detail.aspx?page=100103110&group=" + _group + "&id=" + Identity + "&type=" + FormType + "&ponbr=" + txtOrder.Text + "&receiver=" + txtReceiver.Text + "&line=" + txtLine.Text + "&part=" + txtPart.Text);

        }
        if (e.CommandName == "reCheck")
        {
            LinkButton link = (LinkButton)(e.CommandSource);
            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;
            string _group = gvReport.DataKeys[index].Values["prh_group"].ToString();


            if (oqc.reCheckReport(_group, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                Alert("操作成功，请前往进料检验（未完成）继续操作");
                GridViewBind();
            }
            else
            {
                Alert("操作失败，请联系管理员");
            }
            
            
        }
    }
}
