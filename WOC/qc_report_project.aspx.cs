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
using WOCProgress;
using QCProgress;

public partial class QC_qc_report_project : BasePage
{
    WOC oqc = new WOC();
    GridViewNullData ogv = new GridViewNullData();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            dropType.DataSource = oqc.GetDefectType(string.Empty,1);
            dropType.DataBind();
            dropType.Items.Insert(0, new ListItem("--", "0"));
            dropType.SelectedIndex = 1;
            dropProject.Items.Insert(0, new ListItem("--", "0"));


            dType.DataSource = oqc.GetDefectType(string.Empty, 1);//绑定巡检类别
            dType.DataBind();
            dType.Items.Insert(0, new ListItem("--", "0"));
            dType.SelectedIndex = 0;

            dynamic[] date =new dynamic[5];

            date[0] = new { dateText = 9, dateValue = 9};
            date[1] = new { dateText = 11, dateValue = 11 };
            date[2] = new { dateText = 14, dateValue = 14 };
            date[3] = new { dateText = 16, dateValue = 16 };
            date[4] = new { dateText = 18, dateValue = 18 };
 
            dropDate.DataSource = date;
            dropDate.DataBind();
            dropDate.Items.Insert(0, new ListItem("--", "0"));
            dropDate.SelectedIndex = 0;

            dropLevel.DataSource = oqc.GetGbtLevel("");
            dropLevel.DataBind();
            dropLevel.Items.Insert(0, new ListItem("--", "0"));

            dropAql.DataSource = oqc.GetAqlLevel("");
            dropAql.DataBind();
            dropAql.Items.Insert(0, new ListItem("--", "0"));


            

            dropLevel.SelectedIndex = 0;
            dropAql.SelectedIndex = 0;
            txtNum.Text = "0";
            txtCheckNum.Text = "0";

            lblPage.Text = Request.QueryString["page"].ToString().Trim();
            lblGroup.Text = Request.QueryString["group"].ToString().Trim();
            lblIdentity.Text = Request.QueryString["id"].ToString().Trim();

            if (lblPage.Text.Trim() == "100103110")
            {
                TR1.Visible = false;
                TR2.Visible = false;
                gvReport.Columns[10].Visible = false;
                chkDiv.Visible = false;
            }

            DataTable table = oqc.GetReportSnapByGroup(lblPage.Text.Trim(), lblGroup.Text.Trim());
           
            lblPrhid.Text = table.Rows[0][0].ToString().Trim();
            lblPart.Text = table.Rows[0][4].ToString().Trim();

            lblCust.Items.Add(new MenuItem(table.Rows[0][5].ToString().Trim().ToUpper(),table.Rows[0][5].ToString().Trim().ToUpper()));
            lblCust.Items[0].Selectable = false;

            mRecv.Items.Add(new MenuItem(table.Rows[0][1].ToString().Trim(), table.Rows[0][1].ToString().Trim()));
            mRecv.Items[0].Selectable = false;

            txtPCB.Text = "";
            txtLvjiban.Text = "";
            txtZhouqizhang.Text = "";
            string nbr = table.Rows[0][2].ToString().Trim();
            string lot = table.Rows[0][1].ToString().Trim();
            //获取周期章，UL,铝基板版号

            txtLvjiban.Enabled = false;
            txtZhouqizhang.Enabled = false;
            txtUL.Enabled = false;

            DataSet ds = oqc.GetZQAndPCB(nbr, lot);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtZhouqizhang.Text = ds.Tables[0].Rows[0][0].ToString();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                txtUL.Text = ds.Tables[1].Rows[0][0].ToString();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                hidDomain.Value = ds.Tables[2].Rows[0][0].ToString();
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                txtLvjiban.Text = ds.Tables[3].Rows[0][0].ToString();
            }
            mOrder.Items.Add(new MenuItem(table.Rows[0][2].ToString().Trim(), table.Rows[0][2].ToString().Trim()));
            mOrder.Items[0].Selectable = false;

            mLine.Items.Add(new MenuItem(table.Rows[0][3].ToString().Trim(), table.Rows[0][3].ToString().Trim()));
            mLine.Items[0].Selectable = false;

            decimal dd = Convert.ToDecimal(table.Rows[0][6].ToString().Trim());

            if (table.Rows.Count > 1)
            {
                dd -= Convert.ToDecimal(table.Rows[0][6].ToString().Trim());
                chkDiv.Visible = false;

                for (int i = 0; i < table.Rows.Count; i++) 
                {
                    //mRecv.Items[0].ChildItems.Add(new MenuItem(table.Rows[i][1].ToString().Trim(), table.Rows[i][1].ToString().Trim()));
                    //mRecv.Items[0].ChildItems[i].Selectable = false;

                    //mOrder.Items[0].ChildItems.Add(new MenuItem(table.Rows[i][2].ToString().Trim(), table.Rows[i][2].ToString().Trim()));
                    //mOrder.Items[0].ChildItems[i].Selectable = false;

                     lblCust.Items[0].ChildItems.Add(new MenuItem(table.Rows[i][5].ToString().Trim().ToUpper(),table.Rows[i][5].ToString().Trim().ToUpper()));
                     lblCust.Items[0].ChildItems[i].Selectable = false;

                    mLine.Items[0].ChildItems.Add(new MenuItem(table.Rows[i][3].ToString().Trim(), table.Rows[i][3].ToString().Trim()));
                    mLine.Items[0].ChildItems[i].Selectable = false;

                    dd += Convert.ToDecimal(table.Rows[i][6].ToString().Trim());
                }
            }

            if (Request.QueryString["parent"] != null && Request.QueryString["parent"].ToString() == "100103111")
            {
                dd = Convert.ToDecimal(Request.QueryString["rcvd"].ToString().Trim());

                if (lblIdentity.Text.Trim() != string.Empty)
                    chkDiv.Visible = false;
            }

            if (lblIdentity.Text.Trim() == string.Empty)
                lblIdentity.Text = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

            lblRmn.Text = dd.ToString();

            txtRmn.Text = dd.ToString();
            table.Dispose();

            ogv.GridViewDataBind(gvReport, oqc.GetReportProject(lblPrhid.Text.Trim(), lblIdentity.Text.Trim()));
        }
        else
        {
            ogv.ResetGridView(gvReport);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (chkDiv.Checked == true)
        {
            if (txtRmn.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('当前数量不能为空!');";
                return;
            }

            try
            {
                Decimal d = Convert.ToDecimal(txtRmn.Text.Trim());

                if (d <= 0)
                {
                    ltlAlert.Text = "alert('当前数量必须大于0!');";
                    return;
                }
                if (d > Convert.ToDecimal(lblRmn.Text.Trim()))
                {
                    ltlAlert.Text = "alert('当前数量不能大于总剩余数!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('当前数量必须为数字!');";
                return;
            }
        }

        if (dropProject.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择检验项目!');";
            return;
        }

        if (dropDate.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择检验时间!');";
            return;
        }

        if (dropLevel.SelectedIndex == 0 && dropLevel.Enabled == true)
        {
            ltlAlert.Text = "alert('请选择检验水平!');";
            return;
        }

        if (dropAql.SelectedIndex == 0 && dropAql.Enabled == true)
        {
            ltlAlert.Text = "alert('请选择接收质量限!');";
            return;
        }

        if (txtNum.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('不良数不能为空!');";
            return;
        }

        try
        {
            int n = Convert.ToInt32(txtNum.Text.Trim());
            if (n < 0) 
            {
                ltlAlert.Text = "alert('不良数必须为非负整数!');";
                return;
            }

            if (n > Convert.ToDecimal(lblRmn.Text.Trim())) 
            {
                ltlAlert.Text = "alert('不良数不能大于当前数量!');";
                return;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('不良数必须为整数!');";
            return;
        }

        if (txtCheckNum.Enabled == true)
        {
            if (txtCheckNum.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('检查数量不能为空!');";
                return;
            }

            try
            {
                int n = Convert.ToInt32(txtCheckNum.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('检查数量必须为整数!');";
                return;
            }

            if (Convert.ToInt32(txtCheckNum.Text.Trim()) > Convert.ToInt32(Convert.ToDouble(lblRmn.Text.Trim())))
            {
                ltlAlert.Text = "alert('检查数量不能大于当前数量!');";
                return;
            }
        }

        int total = 0;

        if (dltItem.Items.Count > 0)
        {
            //check first
            for (int i = 0; i < dltItem.Items.Count; i++)
            {
                if (((TextBox)dltItem.Items[i].FindControl("txt")).Text.Trim() == string.Empty)
                {
                    ltlAlert.Text = "alert('缺陷内容不能有空项!');";
                    return;
                }

                try
                {
                    int n = Convert.ToInt32(((TextBox)dltItem.Items[i].FindControl("txt")).Text.Trim());

                    total += n;
                }
                catch
                {
                    ltlAlert.Text = "alert('缺陷内容数值必须为非负整数!');";
                    return;
                }
            }
        }

        if (Label3.Text != "gb")
        {
            if (Convert.ToInt32(txtCheckNum.Text.Trim()) < Convert.ToInt32(txtNum.Text.Trim()))
            {
                ltlAlert.Text = "alert('不良数不能大于检查数量!');";
                return;
            }
        }

        if (Convert.ToInt32(txtNum.Text.Trim()) == 0)//如果不良数不填的话 直接取和
        {
            txtNum.Text = total.ToString();
            if (total > Convert.ToInt32(txtCheckNum.Text.Trim()) && Label3.Text != "gb")
            {
                ltlAlert.Text = "alert('不良数不能大于检查数量!');";
                return;
            }
        }
        else
        {
            if (Convert.ToInt32(txtNum.Text.Trim()) > Convert.ToInt32(txtCheckNum.Text.Trim()) && Label3.Text != "gb")
            {
                ltlAlert.Text = "alert('不良数不能大于检查数量!');";
                return;
            }

            if (total != Convert.ToInt32(txtNum.Text.ToString().Trim()) && total != 0)
            {
                ltlAlert.Text = "alert('缺陷内容数值总和必须等于不良数!');";
                return;
            }
        }

        //db operation
        string dItemID = string.Empty;
        string dItemNum = string.Empty;

        for (int j = 0; j < dltItem.Items.Count; j++)
        {
            dItemID += ((Label)dltItem.Items[j].FindControl("lblpItemID")).Text.Trim() + ";";
            dItemNum += ((TextBox)dltItem.Items[j].FindControl("txt")).Text.Trim() + ";";
        }

        FuncErrType errtype = FuncErrType.操作成功;

        DataTable table = oqc.GetReportSnapByGroup(lblPage.Text.Trim(), lblGroup.Text.Trim());

        //如果最后一个项目被删除的话
        if (table.Rows.Count == 0) 
        {
            string _recv = string.Empty;
            string _ord = string.Empty;
            string _line = string.Empty;

            if (mRecv.Items[0].ChildItems.Count == 0)
            {
                _recv = mRecv.Items[0].Text.Trim();
                _ord = mOrder.Items[0].Text.Trim();
                _line = mLine.Items[0].Text.Trim();
            }
            else 
            {
                for (int i = 0; i < mRecv.Items[0].ChildItems.Count; i++)
                {
                    _recv += ";" + mRecv.Items[0].ChildItems[i].Text.Trim();
                    _ord += ";" + mOrder.Items[0].ChildItems[i].Text.Trim();
                    _line += ";" + mLine.Items[0].ChildItems[i].Text.Trim();
                }
            }

            oqc.SetReportInGroup(_recv, _ord, _line, lblGroup.Text.Trim(), Session["uID"].ToString());

            table.Dispose();

            table = oqc.GetReportSnapByGroup(lblPage.Text.Trim(), lblGroup.Text.Trim());

            lblPrhid.Text = table.Rows[0][0].ToString().Trim();
        }

        for (int i = 0; i < table.Rows.Count; i++)
        {
            string time = Convert.ToInt32(dropDate.SelectedValue.ToString()) < 10 ? "0" + dropDate.SelectedValue.ToString() : dropDate.SelectedValue.ToString();
            string dTime = DateTime.Now.ToString("yyyy-MM-dd") + " " + time+":00:00";
            errtype = oqc.AddReportProject(    table.Rows[i][0].ToString(),
                                               dropProject.SelectedValue,
                                               dropLevel.SelectedValue,
                                               dropAql.SelectedValue,
                                               txtNum.Text.Trim(),
                                               txtCheckNum.Text.Trim(),
                                               lblGroup.Text.Trim(),
                                               dItemID,
                                               dItemNum,
                                               lblIdentity.Text.Trim(),
                                               Convert.ToInt32(Convert.ToDouble(txtRmn.Text.Trim())),                                     
                                               Convert.ToInt32(Session["uID"].ToString()),
                                               txtRemarks.Text.Trim(),
                                             dTime);

            if (errtype != FuncErrType.操作成功)
            {
                lblFlag.Text = "1";

                ltlAlert.Text = "alert('" + errtype.ToString() + "');";
                return;
            }
        }

        if (chkDiv.Visible == true)
        {


            lblRmn.Text = txtRmn.Text;
            lblRmn.Visible = true;
            chkDiv.Visible = false;
            txtRmn.Visible = false;
        }

        //then save
        table.Dispose();
        dropProject.SelectedIndex = 0;
        dropLevel.SelectedIndex = 0;
        dropAql.SelectedIndex = 0;
        txtCheckNum.Text = "0";

        txtNum.Text = "0";
        dltItem.DataSource = null;
        dltItem.DataBind();

        ogv.GridViewDataBind(gvReport, oqc.GetReportProject(lblPrhid.Text.Trim(), lblIdentity.Text.Trim()));
    }
    protected void gvReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReport.PageIndex = e.NewPageIndex;

        ogv.GridViewDataBind(gvReport, oqc.GetReportProject(lblPrhid.Text.Trim(), lblIdentity.Text.Trim()));
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }

            if (e.Row.Cells[3].Text.Trim().Contains("n="))
                e.Row.Cells[3].Style.Add("text-align","left");

            if (e.Row.Cells[10].Text.Trim() != "" && e.Row.Cells[10].Text.Trim() != "&nbsp;")
            {
                if (Convert.ToInt32(e.Row.Cells[10].Text.Trim()) == 0)
                {
                    e.Row.Cells[10].Text = "NO";
                }
                else
                {
                    e.Row.Cells[10].Text = "OK";
                }
                #region
                //bind 缺陷内容  可能有用
                //DataTable dst = oqc.GetReportItem(gvReport.DataKeys[e.Row.RowIndex].Value.ToString());

                //for (int i = 0; i < dst.Rows.Count; i++)
                //{
                //    e.Row.Cells[8].Text += dst.Rows[i][0].ToString() + ":" + dst.Rows[i][1].ToString() + " ";
                //}
                #endregion
            }
        }
    }
    protected void gvReport_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        FuncErrType errtype = FuncErrType.操作成功;
        string uID = Session["uID"].ToString();

        errtype = oqc.DeleteReport(gvReport.DataKeys[e.RowIndex].Value.ToString(), uID);

        if (errtype != FuncErrType.操作成功) 
        {
            ltlAlert.Text = "alert('" + errtype.ToString() + "');";
            return;
        }

        ogv.GridViewDataBind(gvReport, oqc.GetReportProject(lblPrhid.Text.Trim(), lblIdentity.Text.Trim()));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["parent"] == null)
        {
            if (lblPage.Text.Trim() == "100103097")
                this.Response.Redirect("qc_report_Undo.aspx");

            else if (lblPage.Text.Trim() == "100103121")
                this.Response.Redirect("qc_report.aspx");

            else if (lblPage.Text.Trim() == "100103110")
                this.Response.Redirect("qc_report_complete.aspx");
        }
        else 
        {
            this.Response.Redirect("qc_report_history.aspx?page=" + lblPage.Text.Trim()+"&group="+lblGroup.Text.Trim());
        }  
    }
    protected void dropProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropProject.SelectedIndex != 0)
        {
            dropLevel.SelectedIndex = 0;
            dropAql.SelectedIndex = 0;
            txtNum.Text = "0";
            txtCheckNum.Text = "0";

            string Rmn = string.Empty;

            if (chkDiv.Checked)
                Rmn = txtRmn.Text.Trim();
            else
                Rmn = lblRmn.Text.Trim();
            #region
            //bind project detail
            //DataTable dst = oqc.GetProjectItem(dropProject.SelectedValue, Rmn).Tables[0];

            //if (dst.Rows.Count == 0)
            //{
            //    lblNothing.Visible = true;
            //    dltItem.Visible = false;
            //}
            //else
            //{
            //    lblNothing.Visible = false;
            //    dltItem.Visible = true;

            //    dltItem.DataSource = dst;
            //    dltItem.DataBind();
            //}
            #endregion 
            //get Standard then decide controls to ennable or not

            string proName = dropProject.SelectedValue;
           // string tID = dropType.SelectedValue.Trim();
           // string tID = "1"; //默认类别
            int org = 0;

            if (oqc.IsGB(dType.SelectedValue,proName, string.Empty, string.Empty, Rmn, ref org))//国标
            {
                dropLevel.Enabled = true;
                dropAql.Enabled = true;
                txtCheckNum.Text = "0";
              //  txtCheckNum.ReadOnly = true;
                try
                {
                    txtCheckNum.Text = Convert.ToInt32(org) > Convert.ToInt32(Convert.ToDouble(lblRmn.Text.Trim())) ? Convert.ToInt32(Convert.ToDouble(lblRmn.Text)).ToString() : org.ToString();
                }
                catch
                {
                     txtCheckNum.Text = org.ToString();
                }
                Label3.Text = "gb";
            }
            else
            {
                dropLevel.Enabled = false;
                dropAql.Enabled = false;

                if (org < 0)
                {
                    dropType.SelectedIndex = 0;
                    dropProject.SelectedIndex = 0;
                    dropLevel.SelectedIndex = 0;
                    dropAql.SelectedIndex = 0;
                    txtNum.Text = "0";
                    txtCheckNum.Text = "0";
                    lblNothing.Visible = true;
                    txtCheckNum.ReadOnly = false;
                    ltlAlert.Text = "alert('\"当前数量\"不在已有项目的样本量区间里,请在<巡检检验->工序维护>中维护');";
                    return;
                }
                try
                {
                    txtCheckNum.Text = Convert.ToInt32(org) > Convert.ToInt32(Convert.ToDouble(lblRmn.Text.Trim())) ? Convert.ToInt32(Convert.ToDouble(lblRmn.Text)).ToString() : org.ToString();
                }
                catch
                {
                    txtCheckNum.Text = org.ToString();
                }
                Label3.Text = "gd";
            }
        }
        dropProject.Focus();

        ogv.GridViewDataBind(gvReport, oqc.GetReportProject(lblPrhid.Text.Trim(), lblIdentity.Text.Trim()));
    }
    protected void chkDiv_CheckedChanged(object sender, EventArgs e)
    {
        dropType.SelectedIndex = 0;
        dropProject.SelectedIndex = 0;
        dropLevel.SelectedIndex = 0;
        dropAql.SelectedIndex = 0;
        txtNum.Text = "0";
        txtCheckNum.Text = "0";
        dltItem.Visible = false;
        lblNothing.Visible = true;

        if (chkDiv.Checked == true)
        {
            if (Convert.ToDecimal(lblRmn.Text.Trim()) == 0) 
            {
                ltlAlert.Text = "alert('当前数量已经为零,不能拆分');";
                chkDiv.Checked = false;

                return;
            }

            lblRmn.Visible = false;
            txtRmn.Visible = true;
        }
        else 
        {
            lblRmn.Visible = true;
            txtRmn.Visible = false;
        }
    }
    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex != 0) 
        {
            dropProject.DataSource = oqc.GetProject(" WHERE tID=" + dropType.SelectedValue,1);
            dropProject.DataBind();
            dropProject.Items.Insert(0, new ListItem("--", "--"));

            dropLevel.SelectedIndex = 0;
            dropAql.SelectedIndex = 0;
            txtNum.Text = "0";
            txtCheckNum.Text = "0";
        }
    }
    protected void dropAql_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropAql.SelectedIndex != 0) 
        {
            if (dropProject.SelectedIndex == 0) 
            {
                ltlAlert.Text = "alert('请先选择一项检验项目');";
                return;
            }

            if (dropLevel.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('请先选择一项检验水平');";
                return;
            }

            if (dropAql.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('请先选择一项接受质量限');";
                return;
            }

            int org = 0;

            string tID = dropType.SelectedValue;
            string proName = dropProject.SelectedValue;
            string level = dropLevel.SelectedValue;
            string aql = dropAql.SelectedValue;
            string recv = string.Empty;

            if(chkDiv.Checked)
                recv = txtRmn.Text.Trim();
            else
                recv = lblRmn.Text.Trim();

            tID = "1"; ///直接赋值
            oqc.IsGB(tID,proName, level, aql, recv, ref org);

            if (org > 0)
            {
                txtCheckNum.ReadOnly = true;
                txtCheckNum.Text = org.ToString();
            }
            else
            {
                txtCheckNum.Text = "0";
            }
        }
    }
    protected void txtRmn_TextChanged(object sender, EventArgs e)
    {
        dropType.SelectedIndex = 0;
        dropProject.SelectedIndex = 0;
        dropLevel.SelectedIndex = 0;
        dropAql.SelectedIndex = 0;
        txtNum.Text = "0";
        txtCheckNum.Text = "0";
        dltItem.Visible = false;
        lblNothing.Visible = true;
    }
    protected void dropLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropAql.SelectedIndex != 0 && dropLevel.SelectedIndex != 0)
        {
            int org = 0;

            string tID = dropType.SelectedValue;//////////////////
            string proName = dropProject.SelectedValue;
            string level = dropLevel.SelectedValue;
            string aql = dropAql.SelectedValue;
            string recv = string.Empty;

            if (chkDiv.Checked)
                recv = txtRmn.Text.Trim();
            else
                recv = lblRmn.Text.Trim();

            tID = "1"; ///直接赋值
            oqc.IsGB(tID, proName, level, aql, recv, ref org);

            if (org > 0)
            {
                txtCheckNum.ReadOnly = true;
                txtCheckNum.Text = org.ToString();
            }
            else
            {
                txtCheckNum.Text = "0";
            }
        }
    }

    protected void dType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropProject.DataSource = oqc.GetProject(" WHERE tID="+dType.SelectedValue, 1);
        dropProject.DataBind();
        dropProject.Items.Insert(0, new ListItem("--", "--"));
    }
}
