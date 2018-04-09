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

public partial class QC_qc_process : BasePage
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
            txtPageIndex.Text = "1";
            txtIndex.Text = "0";

            txtWorkOrder.Focus();
            if (FormType == "read")
            {
                tbHeader.Visible = false;
                txtID.Text = Request.QueryString["woLot"];
                gvProcess.Columns[13].Visible = false;
                gvProcess.Columns[14].Visible = false;
            }
            else
            {
                txtDate1.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";

                txtDate2.Text = DateTime.Now.AddDays(1).ToShortDateString();
            }

            GridViewBind();
        }
        else
        {
            this.gvProcess_RowDataBound(this, new GridViewRowEventArgs(gvProcess.FooterRow));
        }
    }
    private void GridViewBind()
    {
        DataSet _dataset = oqc.GetProcess(txtWorkOrder.Text.Trim(),
                                          txtID.Text.Trim(),
                                          txtPart.Text.Trim(),
                                          txtProvider.Text.Trim(),
                                          txtNO.Text.Trim(),
                                          txtPartM.Text.Trim(),
                                          txtWorkGroup.Text.Trim(),
                                          txtDate1.Text.Trim(),
                                          txtDate2.Text.Trim(),
                                          txtLineMgt.Text.Trim(),
                                          19,
                                          int.Parse(txtPageIndex.Text.Trim()));

        if (_dataset.Tables[0].Rows.Count < 1)
            gvProcess.ShowFooter = false;
        else
            gvProcess.ShowFooter = true;

        txtPageCount.Text = _dataset.Tables[1].Rows[0][0].ToString();

        ogv.GridViewDataBind(gvProcess, _dataset.Tables[0]);
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string strIndex = ((TextBox)gvProcess.FooterRow.Cells[0].FindControl("txtIndex")).Text;

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[1-9]\d{0,}$");
        if (regex.IsMatch(strIndex))
        {
            if (int.Parse(strIndex) <= int.Parse(txtPageCount.Text))
            {
                txtPageIndex.Text = ((TextBox)gvProcess.FooterRow.Cells[0].FindControl("txtIndex")).Text;
                txtIndex.Text = Convert.ToString((int.Parse(txtPageIndex.Text) - 1) / 10);

                GridViewBind();
            }
            else
                ltlAlert.Text = "alert('输入的页码超出了范围');";
        }
        else
            ltlAlert.Text = "alert('输入的页码必须为大于零的整数');";
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool bFlag = true;

        if (bFlag && txtWorkOrder.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('加工单号不能为空');";
            txtWorkOrder.Focus();
            bFlag = false;
        }
        else
        {
            if (txtWorkOrder.Text.Trim().IndexOf(";") > 0)
            {
                ltlAlert.Text = "alert('加工单号中不能有分号');";
                txtWorkOrder.Focus();
                bFlag = false;
            }
        }

        if (bFlag && txtID.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('ID号不能为空');";
            txtID.Focus();
            bFlag = false;
        }
        else
        {
            if (txtID.Text.Trim().IndexOf(";") > 0)
            {
                ltlAlert.Text = "alert('ID号中不能有分号');";
                txtID.Focus();
                bFlag = false;
            }
        }

        if (bFlag && txtPart.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('加工零件不能为空')";
            txtPart.Focus();
            bFlag = false;
        }

        if (bFlag && txtOrdered.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('加工数量不能为空')";
            txtOrdered.Focus();
            bFlag = false;
        }

        if (bFlag && txtNO.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('批号、编号不能为空')";
            txtNO.Focus();
            bFlag = false;
        }

        if (bFlag && txtIn.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('本次投入数不能为空')";
            txtIn.Focus();
            bFlag = false;
        }

        if (bFlag && txtOut.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('本次产出数不能为空')";
            txtOut.Focus();
            bFlag = false;
        }

        if (bFlag && txtDate1.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('检验日期不能为空')";
            txtDate1.Focus();
            bFlag = false;
        }

        if (bFlag && Convert.ToDateTime(txtDate1.Text.Trim()) > DateTime.Now.Date)
        {
            ltlAlert.Text = "alert('检验日期不能在当天之后')";
            txtDate1.Focus();
            bFlag = false;
        }

        if (bFlag && txtLineMgt.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('线长不能为空')";
            txtLineMgt.Focus();
            bFlag = false;
        }
        else
        {
            //int type = 512;//线长的roleid
            int plantId = Convert.ToInt32(Session["PlantCode"].ToString());
            String userId = txtLineMgt.Text.Replace("；", ";");
            if (!oqc.checkUserNo(userId, plantId))
            {
                ltlAlert.Text = "alert('线长填写错误')";
                txtLineMgt.Focus();
                bFlag = false;
            }

        }
        if (bFlag)
        {
            try
            {
                int n = int.Parse(txtIn.Text.Trim());
                if (n <= 0)
                {
                    ltlAlert.Text = "alert('本次投入数必须大于零')";
                    txtIn.Focus();
                    bFlag = false;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('本次投入数必须为整数')";
                txtIn.Focus();
                bFlag = false;
            }

            try
            {
                int n = int.Parse(txtOut.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('本次产出数必须为整数')";
                txtOut.Focus();
                bFlag = false;
            }
        }

        if (bFlag && (int.Parse(txtOut.Text.Trim()) > int.Parse(txtIn.Text.Trim())))
        {
            ltlAlert.Text = "alert('本次投入数不能小于本次产出数')";
            txtOut.Focus();
            bFlag = false;
        }

        //then operate db

        if (bFlag)
        {
            FuncErrType errtype = FuncErrType.操作成功;

            string ID = txtID.Text.Trim();
            string workorder = txtWorkOrder.Text.Trim();
            string provider = txtProvider.Text.Trim();
            string no = txtNO.Text.Trim();
            int nIn = int.Parse(txtIn.Text.Trim());
            int nOut = int.Parse(txtOut.Text.Trim());
            string part = txtPartM.Text.Trim();
            string lineMgt = txtLineMgt.Text.Trim();
            int person = int.Parse(Session["uID"].ToString());

            errtype = oqc.AddProcess(ID, workorder, provider, no, nIn, nOut, part, person, txtRemarks.Text.Trim(), txtWorkGroup.Text.Trim(), lineMgt);

            if (errtype != FuncErrType.操作成功)
            {
                ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            }
            else
            {
                txtID.Text = string.Empty;
                txtWorkOrder.Text = string.Empty;
                txtPart.Text = string.Empty;
                txtOrdered.Text = "0";
                txtProvider.Text = string.Empty;
                txtNO.Text = string.Empty;
                txtIn.Text = "0";
                txtOut.Text = "0";
                txtPartM.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtWorkGroup.Text = string.Empty;
                txtLineMgt.Text = string.Empty;
            }
        }

        txtPageIndex.Text = "1";
        txtIndex.Text = "0";

        GridViewBind();
    }
    protected void gvProcess_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //order
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }

            e.Row.Cells[1].Text = ((DataBoundLiteralControl)e.Row.Cells[1].Controls[0]).Text.ToString().Replace(";", "</br>");
            e.Row.Cells[2].Text = ((DataBoundLiteralControl)e.Row.Cells[2].Controls[0]).Text.ToString().Replace(";", "</br>");

            if (gvProcess.EditIndex < 0) 
            {
                e.Row.Cells[5].Text = ((DataBoundLiteralControl)e.Row.Cells[5].Controls[0]).Text.ToString().Replace(";", "</br>");
            }
            

            e.Row.Cells[4].Style.Add("text-align","right");
            e.Row.Cells[6].Style.Add("text-align", "left");
            e.Row.Cells[7].Style.Add("text-align", "right");
            e.Row.Cells[8].Style.Add("text-align", "right");

            if (FormType == "read")
            {
                string url = (e.Row.Cells[12].Controls[0] as HyperLink).NavigateUrl + "&type=read";
                (e.Row.Cells[12].Controls[0] as HyperLink).NavigateUrl = url;
                (e.Row.Cells[12].Controls[0] as HyperLink).Text = "查看";
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Controls.Clear();

            TableCell cell = new TableCell();
            cell.ColumnSpan = 15;

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
    protected void txtID_TextChanged(object sender, EventArgs e)
    {
        if (txtWorkOrder.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请填写加工单号');";
            txtWorkOrder.Focus();
        }

        if (txtID.Text.Trim() != String.Empty)
        {
            DataTable table = null;
            FuncErrType error = FuncErrType.操作成功;

            error = oqc.GetProductSnatch(txtID.Text.Trim().Replace(" ", ""), txtWorkOrder.Text.Trim().Replace(" ", ""), ref table);

            if (error != FuncErrType.操作成功)
            {
                txtID.Text = string.Empty;
                txtPart.Focus();
            }
            else
            {
                txtOrdered.Text = table.Rows[0][2].ToString();
                txtPart.Text = table.Rows[0][1].ToString();

                table.Dispose();
            }
        }

        GridViewBind();
    }
    protected void gvProcess_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProcess.EditIndex = -1;

        GridViewBind();
    }
    protected void gvProcess_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvProcess.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void gvProcess_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strMsg = "";
        FuncErrType errtype = FuncErrType.操作成功;
        string strID = gvProcess.DataKeys[e.RowIndex].Values[0].ToString();

        int uID = int.Parse(Session["uID"].ToString());

        errtype = oqc.DeleteProcess(int.Parse(strID),uID, ref strMsg);

        if (errtype != FuncErrType.操作成功)
        {
            ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            return;
        }

        GridViewBind();
    }
    protected void gvProcess_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string prcID = gvProcess.DataKeys[e.RowIndex].Values[0].ToString();
        Label tQty = (Label)gvProcess.Rows[e.RowIndex].Cells[4].FindControl("tQty");
        TextBox tGuest = (TextBox)gvProcess.Rows[e.RowIndex].Cells[5].FindControl("tGuest");
        TextBox tNo = (TextBox)gvProcess.Rows[e.RowIndex].Cells[6].FindControl("tNo");
        TextBox tIn = (TextBox)gvProcess.Rows[e.RowIndex].Cells[7].FindControl("tIn");
        TextBox tOut = (TextBox)gvProcess.Rows[e.RowIndex].Cells[8].FindControl("tOut");
        TextBox tPart = (TextBox)gvProcess.Rows[e.RowIndex].Cells[9].FindControl("tPart");
        TextBox tWorkGroup = (TextBox)gvProcess.Rows[e.RowIndex].Cells[10].FindControl("tWorkGroup");
        TextBox tLineMgt = (TextBox)gvProcess.Rows[e.RowIndex].Cells[11].FindControl("tLineMgt");
        TextBox tRemarks = (TextBox)gvProcess.Rows[e.RowIndex].Cells[15].FindControl("tRemarks");
        int person = int.Parse(Session["uID"].ToString());

        bool bFlag = true;

        if (bFlag && tNo.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('批号、编号不能为空');";
            tNo.Focus();
            bFlag = false;
        }

        if (bFlag && tIn.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('本次投入数不能为空');";
            tIn.Focus();
            bFlag = false;
        }
        if (bFlag && tOut.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('合格产出数不能为空');";
            tOut.Focus();
            bFlag = false;
        }
        if (bFlag && tLineMgt.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('线长不能为空');";
            tLineMgt.Focus();
            bFlag = false;
        }

        if (bFlag)
        {
            try
            {
                int n = int.Parse(tIn.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('本次投入数必须为整数');";
                tIn.Focus();
                bFlag = false;
            }
            try
            {
                int n = int.Parse(tOut.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('合格产出数必须为整数');";
                tOut.Focus();
                bFlag = false;
            }
        }

        if (bFlag && (int.Parse(tOut.Text.Trim()) > int.Parse(tIn.Text.Trim()))) 
        {
            ltlAlert.Text = "alert('合格产出数不能大于本次投入数');";
            tOut.Focus();
            bFlag = false;
        }

        if (bFlag)
        {
            FuncErrType errtype = FuncErrType.操作成功;

            errtype = oqc.ModifyProcess(prcID, tGuest.Text.Trim(), tNo.Text.Trim(), tIn.Text.Trim(), tOut.Text.Trim(), tPart.Text.Trim(), person, tRemarks.Text.Trim(), tWorkGroup.Text.Trim(), tLineMgt.Text.Trim());

            if (errtype != FuncErrType.操作成功)
            {
                ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            }
            else
            {
                gvProcess.EditIndex = -1;
            }
        }

        GridViewBind();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        txtPageIndex.Text = "1";
        txtIndex.Text = "0";

        GridViewBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPageIndex.Text = "1";
        txtIndex.Text = "0";

        txtID.Text = string.Empty;
        txtWorkOrder.Text = string.Empty;
        txtPart.Text = string.Empty;
        txtOrdered.Text = "0";
        txtProvider.Text = string.Empty;
        txtNO.Text = string.Empty;
        txtIn.Text = "0";
        txtOut.Text = "0";
        txtPartM.Text = string.Empty;
        txtRemarks.Text = string.Empty;
        txtWorkGroup.Text = string.Empty;

        GridViewBind();
    }
    protected void txtPart_TextChanged(object sender, EventArgs e)
    {
        if (txtWorkOrder.Text.Trim() == String.Empty || txtWorkOrder.Text.Trim().Length < 8)
        {
            ltlAlert.Text = "alert('请正确填写加工单号!');";
        }

        if (txtPart.Text.Trim() != String.Empty)
        {
            if (txtPart.Text.Trim().Length != 14)
            {
                ltlAlert.Text = "alert('加工零件位数不对');";
            }
            else
            {
                String strLots = oqc.GetProcessLot(txtWorkOrder.Text.Trim(), txtPart.Text.Trim());

                if (String.Empty == strLots)
                {
                    ltlAlert.Text = "alert('ID号不匹配!');";
                }
                else
                {
                    txtID.Text = strLots;
                }
            }
        }

        GridViewBind();
    }
}