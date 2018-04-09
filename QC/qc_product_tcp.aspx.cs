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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class QC_qc_product : BasePage
{
    adamClass adam = new adamClass();
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("1001031070", "成品检验删除权限（包括TCP和外厂）");
        }

        base.OnInit(e);
    }
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
            Session["TCP"] = true;
            chkTcp.Checked = true;

            Session["OUT"] = false;
            chkOut.Checked = false;

            if (Request.QueryString["TID"] != null)
            {
                SqlDataReader reader = GetProductTcpByID(Convert.ToInt32(Request.QueryString["TID"].ToString().Trim()));

                if (reader.Read())
                {
                    txtOrder.Text = reader["prd_nbr"].ToString().Trim();
                    txtID.Text = reader["prd_lot"].ToString().Trim();
                    txtFloor.Text = reader["prd_cc"].ToString().Trim() + ";";
                    txtDate.Text = reader["prd_checkdate"].ToString().Trim();
                    txtQad.Text = reader["prd_part"].ToString().Trim();
                    txtOrderNum.Text = reader["prd_qty1"].ToString().Trim();
                }
                reader.Close();
            }

            BindLine();

            txtPageIndex.Text = "1";
            txtIndex.Text = "0";

            txtFloor.Focus();
            if (FormType == "read")
            {
                tbHeader.Visible = false;
                gvProduct.Columns[16].Visible = false;
                gvProduct.Columns[17].Visible = false;
                txtID.Text = Request.QueryString["woLot"];
            }
            else
            {

                txtDate.Text = DateTime.Now.AddDays(-5).ToShortDateString();

                txtDate2.Text = DateTime.Now.AddDays(1).ToShortDateString();
            }

            GridViewBind();
        }
        else
        {
            this.gvProduct_RowDataBound(this, new GridViewRowEventArgs(gvProduct.FooterRow));
        }
    }

    private void dropOutBind()
    {
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_SelectDomain").Tables[0];
        dropOut.DataSource = dt;
        dropOut.DataBind();
        dropOut.Items.Insert(0, new ListItem("--", ""));
        dropOut.SelectedIndex = 0;
    }

    private void GridViewBind()
    {
        bool bFlag = true;

        if (bFlag && txtDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _date = Convert.ToDateTime(txtDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('检验日期格式不正确');Form1.txtDate.focus();";
                bFlag = false;
            }
        }

        if (bFlag)
        {
            DataSet _dataset = oqc.GetProduct(txtOrder.Text.Trim(),
                                                txtID.Text.Trim(),
                                                txtBoard.Text.Trim(),
                                                txtDate.Text.Trim(),
                                                txtInspector.Text.Trim(),
                                                txtPeriod.Text.Trim(),
                                                txtGuest.Text.Trim(),
                                                txtExaminer.Text.Trim(),
                                                chkTcp.Checked,
                                                txtDate.Text.Trim(),
                                                txtDate2.Text.Trim(),
                                                16,
                                                int.Parse(txtPageIndex.Text.Trim()),
                                                chkOut.Checked,
                                                chkfree.Checked);

            if (_dataset.Tables[0].Rows.Count < 1)
                gvProduct.ShowFooter = false;
            else
                gvProduct.ShowFooter = true;

            txtPageCount.Text = _dataset.Tables[1].Rows[0][0].ToString();

            ogv.GridViewDataBind(gvProduct, _dataset.Tables[0]);
        }
    }

    private void BindLine()
    {
        dropLine.Items.Clear();

        string domain = "SZX";

        if (Session["PlantCode"].ToString() == "1")
        {
            domain = "SZX";
        }
        else if (Session["PlantCode"].ToString() == "2")
        {
            domain = "ZQL";
        }
        else if (Session["PlantCode"].ToString() == "5")
        {
            domain = "YQL";
        }

        try
        {
            string strSql = "sp_wo2_selectLine";

            SqlParameter[] parmArray = new SqlParameter[1];

            parmArray[0] = new SqlParameter("@domain", domain);

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, parmArray);

            dropLine.DataSource = ds.Tables[0];
            dropLine.DataBind();
        }
        catch { }

        dropLine.Items.Insert(0, new ListItem("--", ""));
        dropLine.SelectedIndex = 0;
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        string strIndex = ((TextBox)gvProduct.FooterRow.Cells[0].FindControl("txtIndex")).Text;

        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[1-9]\d{0,}$");
        if (regex.IsMatch(strIndex))
        {
            if (int.Parse(strIndex) <= int.Parse(txtPageCount.Text))
            {
                txtPageIndex.Text = ((TextBox)gvProduct.FooterRow.Cells[0].FindControl("txtIndex")).Text;
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

    protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (!gvProduct.DataKeys[e.Row.RowIndex].Values["prdCDate"].ToString().Equals(string.Empty))
            {
                DateTime createdate = new DateTime(1900, 1, 1);
                if (DateTime.TryParse(gvProduct.DataKeys[e.Row.RowIndex].Values["prdCDate"].ToString(), out createdate))
                {
                    bool createCanDelete = false;
                    createdate= createdate.AddDays(2);

                    DateTime createUse = new DateTime(createdate.Year, createdate.Month, createdate.Day);



                    if (DateTime.Compare(createUse, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)) >= 0)
                    {
                        createCanDelete = true;
                    }
                   

                    if ((!this.Security["1001031070"].isValid) && (!(gvProduct.DataKeys[e.Row.RowIndex].Values["prdCBy"].ToString().Equals(Session["uID"].ToString())
                        && createCanDelete)))
                    {
                        e.Row.Cells[17].Text = "";
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('工单号为：" + gvProduct.DataKeys[e.Row.RowIndex].Values["prdOrder"].ToString() + "ID号为：" + gvProduct.DataKeys[e.Row.RowIndex].Values["prdLine"].ToString() + "创建日期出了点问题，请联系管理员')";
                    return;
                }
            }

            bool fleg = false;
            string isFree = gvProduct.DataKeys[e.Row.RowIndex]["prdIsExemption"].ToString().Trim().ToLower();
            if (!(string.Empty.Equals(isFree) || "false".Equals(isFree)))
            {
                fleg = true;
            }

            if (fleg)
            {
                e.Row.Cells[14].Text = "";
                e.Row.Cells[16].Text = "";
                ((Label)e.Row.FindControl("lbfree")).Text = "免检";
            }
            //order
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }
            
            e.Row.Cells[1].Text = ((DataBoundLiteralControl)e.Row.Cells[1].Controls[0]).Text.Trim().Replace(";", "</br>");
            e.Row.Cells[2].Text = ((DataBoundLiteralControl)e.Row.Cells[2].Controls[0]).Text.Trim().Replace(";", "</br>");
            e.Row.Cells[4].Text = ((DataBoundLiteralControl)e.Row.Cells[4].Controls[0]).Text.Trim().Replace(";", "</br>");
            if (FormType == "read")
            {
                string url = (e.Row.Cells[13].Controls[0] as HyperLink).NavigateUrl + "&type=read";
                (e.Row.Cells[13].Controls[0] as HyperLink).NavigateUrl = url;
                (e.Row.Cells[13].Controls[0] as HyperLink).Text = "查看";
                (e.Row.Cells[14].FindControl("LinkButton1") as LinkButton).Visible = false;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Controls.Clear();

            TableCell cell = new TableCell();
            cell.ColumnSpan = 17;

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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        bool bFlag = true;

        if (bFlag && txtFloor.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('生产车间不能为空')";
            txtFloor.Focus();
            bFlag = false;
        }

        if (bFlag && txtBoard.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('印刷版号不能为空')";
            txtBoard.Focus();
            bFlag = false;
        }

        if (bFlag && txtDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('检验日期不能为空')";
            txtDate.Focus();
            bFlag = false;
        }

        if (bFlag && Convert.ToDateTime(txtDate.Text.Trim()) > DateTime.Now.Date)
        {
            ltlAlert.Text = "alert('检验日期不能在当天之后')";
            txtDate.Focus();
            bFlag = false;
        }

        if (bFlag && txtInspector.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('报检人不能为空')";
            txtInspector.Focus();
            bFlag = false;
        }

        if (bFlag && txtQad.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('QAD号不能为空')";
            txtQad.Focus();
            bFlag = false;
        }

        if (bFlag && txtID.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('ID号不能为空')";
            txtID.Focus();
            bFlag = false;
        }
        else
        {
            if (txtID.Text.Trim().IndexOf(";") >= 0 || txtID.Text.Trim().IndexOf("；") >= 0) 
            {
                ltlAlert.Text = "alert('ID号中不能有分号');";
                txtID.Focus();
                bFlag = false;
            }
        }
        if (bFlag && txtOrder.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('订单号不能为空')";
            txtOrder.Focus();
            bFlag = false;
        }
        else
        {
            if (txtOrder.Text.Trim().IndexOf(";") >= 0 || txtOrder.Text.Trim().IndexOf("；") >= 0)
            {
                ltlAlert.Text = "alert('订单号中不能有分号');";
                txtOrder.Focus();
                bFlag = false;
            }
        }

        if (bFlag && txtPeriod.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('周期章不能为空')";
            txtPeriod.Focus();
            bFlag = false;
        }

        if (bFlag && txtGuest.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('客户不能为空')";
            txtGuest.Focus();
            bFlag = false;
        }

        if (bFlag && txtOrderNum.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('加工单数量不能为空')";
            txtOrderNum.Focus();
            bFlag = false;
        }

        if (bFlag && txtNum.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('本次数量不能为空')";
            txtNum.Focus();
            bFlag = false;
        }

        if (bFlag && txtExaminer.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('检验员不能为空')";
            txtExaminer.Focus();
            bFlag = false;
        }

        if (bFlag)
        {
            if (lbOut.Text == "False")
            {
                if (txtProcessMgt.Text.Trim() == string.Empty)
                {
                    ltlAlert.Text = "alert('过控员不能为空！')";
                    txtProcessMgt.Focus();
                    bFlag = false;
                }
                else if (!oqc.checkUserNo(txtProcessMgt.Text.Trim(), Convert.ToInt32(Session["PlantCode"])))
                {
                    ltlAlert.Text = "alert('过控员工号不正确！')";
                    txtProcessMgt.Focus();
                    bFlag = false;
                }
            }
        }

        if (bFlag)
        {
            try
            {
                int n = int.Parse(txtNum.Text.Trim());

                if (n < 0)
                {
                    ltlAlert.Text = "alert('本次数量必须为非负整数')";
                    txtNum.Focus();
                    bFlag = false;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('本次数量必须为整数')";
                txtNum.Focus();
                bFlag = false;
            }
        }

        if (bFlag)
        {
            try
            {
                int n = int.Parse(Convert.ToDouble(txtOrderNum.Text.Trim()).ToString());

                if (txtQad.Text.Trim().Substring(0, 2) != "22" && n < int.Parse(txtNum.Text.Trim()))
                {
                    ltlAlert.Text = "alert('本次数量不能大于加工单数量')";
                    txtNum.Focus();
                    bFlag = false;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('加工单数量必须为整数')";
                bFlag = false;
            }
        }
        //then operator db

        if (bFlag)
        {
            string strFloor = txtFloor.Text.Trim();
            string strBoard = txtBoard.Text.Trim();
            string strDate = txtDate.Text.Trim();
            string strInspector = txtInspector.Text.Trim();
            string strExaminer = txtExaminer.Text.Trim();
            string strQad = txtQad.Text.Trim();
            string strID = txtID.Text.Trim().Replace(" ", "");
            string strOrder = txtOrder.Text.Trim().Replace(" ", "");
            string strPeriod = txtPeriod.Text.Trim();
            int intNum = int.Parse(Convert.ToDouble(txtNum.Text.Trim()).ToString());
            string stGuest = txtGuest.Text.Trim();
            int person = int.Parse(Session["uID"].ToString());

            string strLineMgt = txtLineMgt.Text;
            string strLine = dropLine.SelectedValue;
            string strProcessMgt = txtProcessMgt.Text.Trim();

            FuncErrType errtype = FuncErrType.操作成功;

            errtype = oqc.AddProduct(strFloor, strBoard, strDate, strInspector,
                                     strExaminer, strQad, strID, strOrder, strPeriod,
                                     intNum, stGuest, chkTcp.Checked,
                                     person, txtRemarks.Text.Trim(), strLineMgt, strLine, chkOut.Checked, strProcessMgt);

            if (errtype != FuncErrType.操作成功)
                ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            else
            {
                txtRemarks.Text = string.Empty;
                txtExaminer.Text = string.Empty;
                txtNum.Text = string.Empty;
                txtGuest.Text = string.Empty;
                txtPeriod.Text = string.Empty;
                txtInspector.Text = string.Empty;
                txtDate.Text = string.Empty;
                txtBoard.Text = string.Empty;
            }
        }

        GridViewBind();
    }
    protected void txtID_TextChanged(object sender, EventArgs e)
    {
        bool bFlag = true;

        if (bFlag && txtOrder.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请填写加工单号');";
            txtOrder.Focus();
            bFlag = false;
        }

        if (bFlag && txtID.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请填写加工单ID号');";
            txtID.Focus();
            bFlag = false;
        }

        if (bFlag)
        {
            DataTable table = null;
            FuncErrType error = FuncErrType.操作成功;

            error = oqc.GetProductSnatch(txtID.Text.Trim().Replace(" ", ""), txtOrder.Text.Trim().Replace(" ", ""), ref table);

            if (error != FuncErrType.操作成功)
            {
                ltlAlert.Text = "alert('" + error.ToString() + "');";
                txtID.Text = string.Empty;
                txtID.Focus();
                bFlag = false;
            }

            txtOrderNum.Text = table.Rows[0][2].ToString();
            txtQad.Text = table.Rows[0][1].ToString();
            txtFloor.Text = table.Rows[0][3].ToString();
            txtLineMgt.Text = table.Rows[0][4].ToString();
            txtProcessMgt.Text = table.Rows[0][5].ToString();
            lbOut.Text  = table.Rows[0][6].ToString();
            if (txtLineMgt.Text.Trim().Length > 0)
            {
                txtLineMgt.Enabled = false;
            }
            else
            {
                txtLineMgt.Enabled = true;
            }
            if (txtProcessMgt.Text.Trim().Length > 0)
            {
                txtProcessMgt.Enabled = false;
            }
            else
            {
                txtProcessMgt.Enabled = true;
            }
        }

        if (bFlag && (txtOrderNum.Text.Trim().Length <= 0))
        {
            ltlAlert.Text = "alert('请输入正确的加工单号和ID号');";
            txtID.Text = string.Empty;
            txtID.Focus();
        }

        txtBoard.Focus();

        GridViewBind();
    }
    protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string strMsg = "";
        FuncErrType errtype = FuncErrType.操作成功;
        string strID = gvProduct.DataKeys[e.RowIndex].Values[0].ToString();

        int uID = int.Parse(Session["uID"].ToString());

        errtype = oqc.DeleteProduct(int.Parse(strID), uID, ref strMsg);

        if (errtype != FuncErrType.操作成功)
            ltlAlert.Text = "alert('" + errtype.ToString() + "');";
        else
        {
            txtRemarks.Text = string.Empty;
            txtExaminer.Text = string.Empty;
            txtNum.Text = string.Empty;
            txtGuest.Text = string.Empty;
            txtPeriod.Text = string.Empty;
            txtInspector.Text = string.Empty;
            txtDate.Text = string.Empty;
            txtBoard.Text = string.Empty;
            txtOrder.Text = string.Empty;
            txtID.Text = string.Empty;
            txtOrderNum.Text = string.Empty;
            txtQad.Text = string.Empty;
            txtFloor.Text = string.Empty;
        }

        GridViewBind();
    }
    protected void gvProduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvProduct.EditIndex = -1;

        GridViewBind();
    }
    protected void gvProduct_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvProduct.EditIndex = e.NewEditIndex;

        GridViewBind();

        TextBox tDate = (TextBox)gvProduct.Rows[e.NewEditIndex].FindControl("tDate");
        //((System.Web.UI.HtmlControls.HtmlAnchor)gvProduct.Rows[e.NewEditIndex].FindControl("link")).Attributes.Add("onclick", "g_Calendar.show(event,'Form1." + tDate.ClientID + "', false, 'yyyy-mm-dd', new Date()); return false;");
    }
    protected void gvProduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string prdID = gvProduct.DataKeys[e.RowIndex].Values[0].ToString();
        TextBox tGuest = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tGuest");
        TextBox tBoard = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tBoard");
        TextBox tDate = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tDate");
        TextBox tInspector = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tInspector");
        TextBox tExaminer = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tExaminer");
        TextBox tPeriod = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tPeriod");
        Label tOrderNum = (Label)gvProduct.Rows[e.RowIndex].FindControl("tOrderNum");
        //TextBox tNum = (TextBox)gvProduct.Rows[e.RowIndex].Cells[12].FindControl("tNum");
        TextBox tRemarks = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tRemarks");
        int person = int.Parse(Session["uID"].ToString());
        bool bFlag = true;

        if (bFlag && tBoard.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('印刷版号不能为空');";
            tBoard.Focus();
            bFlag = false;
        }
        if (bFlag && tDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('检验日期不能为空');";
            tDate.Focus();
            bFlag = false;
        }
        if (bFlag && tInspector.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('报检人不能为空');";
            tInspector.Focus();
            bFlag = false;
        }
        if (bFlag && tExaminer.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('检验员不能为空');";
            tExaminer.Focus();
            bFlag = false;
        }
        if (bFlag && tPeriod.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('周期章不能为空');";
            tPeriod.Focus();
            bFlag = false;
        }
        if (bFlag && tGuest.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('供应商不能为空');";
            tGuest.Focus();
            bFlag = false;
        }
        //if (bFlag && tNum.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('本次数量不能为空');";
        //    tNum.Focus();
        //    bFlag = false;
        //}

        //if (bFlag)
        //{
        //    try
        //    {
        //        int n = int.Parse(tNum.Text.Trim());

        //        if (((DataBoundLiteralControl)gvProduct.Rows[e.RowIndex].Cells[9].Controls[0]).Text.ToString().Trim().Substring(0, 2) != "22" && n > int.Parse(Convert.ToDouble(tOrderNum.Text.Trim()).ToString()))
        //        {
        //            ltlAlert.Text = "alert('本次数量不能大于加工单数');";
        //            tNum.Focus();

        //            bFlag = false;
        //        }
        //    }
        //    catch
        //    {
        //        ltlAlert.Text = "alert('本次数量必须为整数');";
        //        tNum.Focus();

        //        bFlag = false;
        //    }
        //}

        if (bFlag)
        {
            FuncErrType errtype = FuncErrType.操作成功;

            errtype = oqc.ModifyProduct(prdID, tBoard.Text.Trim(), tDate.Text.Trim(), tInspector.Text.Trim(), tExaminer.Text.Trim(),
                                        tPeriod.Text.Trim(), "0", tGuest.Text.Trim(), person, tRemarks.Text.Trim());

            if (errtype != FuncErrType.操作成功)
                ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            else
                gvProduct.EditIndex = -1;
        }

        GridViewBind();

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        txtPageIndex.Text = "1";
        txtIndex.Text = "0";

        GridViewBind();
    }
    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "add")
        {
            LinkButton link = (LinkButton)(e.CommandSource);
            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;

            string nbr = gvProduct.DataKeys[index].Values[1].ToString();
            string lot = gvProduct.DataKeys[index].Values[2].ToString();
            string rcvd = gvProduct.DataKeys[index].Values[4].ToString();
            string part = gvProduct.DataKeys[index].Values[3].ToString();

            Response.Redirect("qc_product_lum.aspx?tcp=" + Session["TCP"].ToString() + "&nbr=" + nbr + "&lot=" + lot + "&rcvd=" + rcvd + "&part=" + part);
        }
        else if (e.CommandName == "det")
        {
            LinkButton link = (LinkButton)(e.CommandSource);
            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;

            string nbr = gvProduct.DataKeys[index].Values[1].ToString();
            string lot = gvProduct.DataKeys[index].Values[2].ToString();
            string rcvd = gvProduct.DataKeys[index].Values[4].ToString();
            string part = gvProduct.DataKeys[index].Values[3].ToString();
            if (FormType == "read")
            {
                this.Response.Redirect("qc_product_lum_det.aspx?type=read&tcp=" + Session["TCP"].ToString() + "&nbr=" + nbr + "&lot=" + lot + "&rcvd=" + rcvd + "&part=" + part);
            }
            else
            {
                this.Response.Redirect("qc_product_lum_det.aspx?tcp=" + Session["TCP"].ToString() + "&nbr=" + nbr + "&lot=" + lot + "&rcvd=" + rcvd + "&part=" + part);
            }
        }
        else if (e.CommandName == "lkbtnRework")
        {
            LinkButton link = (LinkButton)(e.CommandSource);
            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;

            string prdID = gvProduct.DataKeys[index].Values["prdID"].ToString();



            ltlAlert.Text = "$.window('返工单', '70%', '80%','" + "/QC/qc_product_return.aspx?prdID=" + prdID + "', '', true);";

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtRemarks.Text = string.Empty;
        txtExaminer.Text = string.Empty;
        txtNum.Text = string.Empty;
        txtGuest.Text = string.Empty;
        txtPeriod.Text = string.Empty;
        txtInspector.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtBoard.Text = string.Empty;
        txtOrder.Text = string.Empty;
        txtID.Text = string.Empty;
        txtOrderNum.Text = string.Empty;
        txtQad.Text = string.Empty;
        txtFloor.Text = string.Empty;

        txtPageIndex.Text = "1";
        txtIndex.Text = "0";

        GridViewBind();
    }

    protected SqlDataReader GetProductTcpByID(int id)
    {
        SqlParameter param = new SqlParameter("@prd_ID", id);

        return SqlHelper.ExecuteReader(adam.dsnx(), CommandType.StoredProcedure, "sp_QC_GetProductTcpByID", param);
    }
}
