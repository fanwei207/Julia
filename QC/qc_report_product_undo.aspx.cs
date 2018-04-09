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

public partial class QC_qc_report_product_undo : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStd.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
            txtEnd.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            txtPageIndex.Text = "1";
            txtIndex.Text = "0";

            GridViewBind();
        }
        else
        {
            this.gvReport_RowDataBound(this, new GridViewRowEventArgs(gvReport.FooterRow));
            ////绑定事件
            //Button btnGo = (Button)gvReport.FooterRow.Cells[0].FindControl("btnGo");
            //if (btnGo != null)
            //    btnGo.Click += new EventHandler(this.btnGo_Click);
        }
    }

    private void GridViewBind()
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
            DataSet _dataset = oqc.GetReportUndo(txtReceiver.Text.Trim(),
                                                             txtOrder.Text.Trim(),
                                                             txtLine.Text.Trim(),
                                                             txtCus.Text.Trim(),
                                                             txtPart.Text.Trim(),
                                                             txtStd.Text.Trim(),
                                                             txtEnd.Text.Trim(),
                                                             Session["uID"].ToString(),
                                                             Session["PlantCode"].ToString(),
                                                             17,
                                                             int.Parse(txtPageIndex.Text.Trim()),
                                                             1
                                                             );

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
            string[] arg = e.CommandArgument.ToString().Split(';');
            string _recv = arg[0];//收货单
            string _ord = arg[1];//订单
            string _line = arg[2];//行号
            string _part = arg[3];//QAD号
            string _provider = arg[4];//供应商
            string _rcvd = arg[5];//接收数量
            string _date = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(arg[6]));
            string _group = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

            LinkButton link = (LinkButton)(e.CommandSource);

            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;

            CheckBox checkbox = (CheckBox)gvReport.Rows[index].Cells[0].FindControl("CheckBox1");
            checkbox.Checked = true;

            string rev = string.Empty;
            string order = string.Empty;
            string ln = string.Empty;

            for (int i = 0; i < gvReport.Rows.Count; i++)
            {
                bool check = ((CheckBox)gvReport.Rows[i].Cells[0].FindControl("CheckBox1")).Checked;
            }


            foreach (GridViewRow row in gvReport.Rows)
            {
                bool result = ((CheckBox)row.FindControl("CheckBox1")).Checked;

                if (result)
                {
                    string recv = row.Cells[1].Text.Trim();
                    string ord = row.Cells[2].Text.Trim();
                    string line = row.Cells[3].Text.Trim();
                    string part = row.Cells[4].Text.Trim();
                    string provider = row.Cells[5].Text.Trim();
                    string rcvd = row.Cells[6].Text.Trim();
                    string date = row.Cells[7].Text.Trim();

                    if (_part.ToUpper() != part.ToUpper() || _provider.ToUpper() != provider.ToUpper() || _date.ToUpper() != date.ToUpper())
                    {
                        ltlAlert.Text = "alert('物料号、供应商和收货日期相同时才允许合并检验');";

                        return;
                    }

                    rev += ";" + recv;
                    order += ";" + ord;
                    ln += ";" + line;
                }
            }

            checkbox.Checked = false;

            string _uID = Session["uID"].ToString();

            if (oqc.SetReportInGroup(rev, order, ln, _group, _uID) > 0)
            {
                if (link.ID.Trim() == "LinkButton1")
                    Response.Redirect("qc_report_product_project.aspx?group=" + _group + "&undo=1" );

        
            }
        }
        else if (e.CommandName == "compete")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent);

            string recieve = row.Cells[1].Text.Trim();
            string order = row.Cells[2].Text.Trim();
            string line = row.Cells[3].Text.Trim();
            string group = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

            oqc.SetReportComplete(recieve, order, line, group, 1, Int32.Parse(Session["uID"].ToString()));

            GridViewBind();
        }
    }

    private void RememberOldValues()
    {
        ArrayList categoryIDList = new ArrayList();

        string index = string.Empty;

        foreach (GridViewRow row in gvReport.Rows)
        {
            index = gvReport.Rows[row.RowIndex].Cells[1].Text.Trim() + gvReport.Rows[row.RowIndex].Cells[2].Text.Trim() + gvReport.Rows[row.RowIndex].Cells[3].Text.Trim();

            bool result = ((CheckBox)row.FindControl("CheckBox1")).Checked;

            // Check in the Session

            if (Session["CHECKED_ITEMS"] != null)

                categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];

            if (result)
            {
                if (!categoryIDList.Contains(index))

                    categoryIDList.Add(index);
            }
            else
                categoryIDList.Remove(index);
        }

        if (categoryIDList != null && categoryIDList.Count > 0)

            Session["CHECKED_ITEMS"] = categoryIDList;
    }

    private void RePopulateValues()
    {
        ArrayList categoryIDList = (ArrayList)Session["CHECKED_ITEMS"];

        if (categoryIDList != null && categoryIDList.Count > 0)
        {
            foreach (GridViewRow row in gvReport.Rows)
            {
                string index = gvReport.Rows[row.RowIndex].Cells[1].Text.Trim() + gvReport.Rows[row.RowIndex].Cells[2].Text.Trim() + gvReport.Rows[row.RowIndex].Cells[3].Text.Trim();

                if (categoryIDList.Contains(index))
                {
                    CheckBox myCheckBox = (CheckBox)row.FindControl("CheckBox1");

                    myCheckBox.Checked = true;
                }
            }
        }
    }

    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Style.Add("display","none");

            LinkButton link8 = (LinkButton)e.Row.Cells[8].FindControl("LinkButton1");
            LinkButton link9 = (LinkButton)e.Row.Cells[9].FindControl("LinkButton2");

            link8.CommandArgument = e.Row.Cells[1].Text.Trim() + ";" + e.Row.Cells[2].Text.Trim() + ";" + e.Row.Cells[3].Text.Trim() + ";" + e.Row.Cells[4].Text.Trim() + ";" + e.Row.Cells[5].Text.Trim() + ";" + e.Row.Cells[6].Text.Trim() + ";" + e.Row.Cells[7].Text.Trim();
            link9.CommandArgument = e.Row.Cells[1].Text.Trim() + ";" + e.Row.Cells[2].Text.Trim() + ";" + e.Row.Cells[3].Text.Trim() + ";" + e.Row.Cells[4].Text.Trim() + ";" + e.Row.Cells[5].Text.Trim() + ";" + e.Row.Cells[6].Text.Trim() + ";" + e.Row.Cells[7].Text.Trim();
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Controls.Clear();

            TableCell cell = new TableCell();
            cell.ColumnSpan = 10;

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
}