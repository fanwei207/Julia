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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using CommClass;
using System.IO;
using System.Drawing;

public partial class EDIPoTrack : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtOrdDate1.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-5));

            bindData();

            GroupRows(gvlist, 0, 0, 1);
            GroupRows(gvlist, 1, 0, 1);
            GroupRows(gvlist, 2, 0, 1);
            GroupRows(gvlist, 3, 0, 1);
            GroupRows(gvlist, 4, 0, 1);
            GroupRows(gvlist, 5, 0, 1);
            GroupRows(gvlist, 6, 0, 1);
            GroupRows(gvlist, 7, 0, 1);
            GroupRows(gvlist, 8, 0, 1);
            GroupRows(gvlist, 9, 0, 1);
            GroupRows(gvlist, 10, 0, 1);
            GroupRows(gvlist, 15, 0, 1);
            GroupRows(gvlist, 16, 0, 1);
            GroupRows(gvlist, 17, 0, 1);
            GroupRows(gvlist, 18, 0, 1);
            GroupRows(gvlist, 19, 0, 1);
        }
    }

    private void bindData()
    {
        Int32 jde_line = 0;
        Int32 edi_line = 0;
        Int32 so_line = 0;
        Int32 wo_line = 0;

        string jde_nbr = "";
        string jde_det = "";

        string so_nbr = "";
        string so_det = "";

        string wo_nbr = "";
        string wo_lot = "";


        DataSet ds = GetPoTrack();


        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if (!(jde_nbr == row["ord_nbr"].ToString().Trim() && jde_det == row["det_line"].ToString().Trim()))
                {
                    jde_line++;

                    if (row["edi_loadDate"].ToString().Length > 0 && row["edi_loadDate"].ToString() != "&nbsp;")
                    {
                        edi_line++;
                    }

                    jde_nbr = row["ord_nbr"].ToString().Trim();
                    jde_det = row["det_line"].ToString().Trim();
                }

                if (!(so_nbr == row["so_nbr"].ToString().Trim() && so_det == row["det_line"].ToString().Trim()))
                {
                    if (row["so_nbr"].ToString().Length > 0 && row["so_nbr"].ToString() != "&nbsp;")
                    {
                        so_line++;
                    }

                    so_nbr = row["so_nbr"].ToString().Trim();
                    so_det = row["det_line"].ToString().Trim();
                }

                if (!(wo_nbr == row["wo_nbr"].ToString().Trim() && wo_lot == row["wo_lot"].ToString().Trim()))
                {
                    if (row["wo_nbr"].ToString().Length > 0 && row["wo_nbr"].ToString() != "&nbsp;")
                    {
                        wo_line++;
                    }

                    wo_nbr = row["wo_nbr"].ToString().Trim();
                    wo_lot = row["wo_lot"].ToString().Trim();
                }
            }
        }

        txtJdeLines.Text = jde_line.ToString();
        txtEdiLines.Text = edi_line.ToString();
        txtSoLines.Text = so_line.ToString();
        txtWoLines.Text = wo_line.ToString();

        this.gvlist.DataSource = ds;
        this.gvlist.DataBind();
    }

    public DataSet GetPoTrack()
    {
        SqlParameter[] sqlParam = new SqlParameter[24];
        //JDE
        sqlParam[0] = new SqlParameter("@po1", txtPo1.Text.Trim());
        sqlParam[1] = new SqlParameter("@po2", txtPo2.Text.Trim());
        sqlParam[2] = new SqlParameter("@ordDate1", txtOrdDate1.Text.Trim());
        sqlParam[3] = new SqlParameter("@ordDate2", txtOrdDate2.Text.Trim());
        sqlParam[4] = new SqlParameter("@item", txtItem.Text.Trim());
        sqlParam[5] = new SqlParameter("@shipDate1", txtShipDate1.Text.Trim());
        sqlParam[6] = new SqlParameter("@shipDate2", txtShipDate2.Text.Trim());
        sqlParam[7] = new SqlParameter("@noWO", chkNoWo.Checked);
        sqlParam[8] = new SqlParameter("@complete", chkComplete.Checked);

        //EDI
        sqlParam[9] = new SqlParameter("@loadEdiDate1", txtLoadEDIDate1.Text.Trim());
        sqlParam[10] = new SqlParameter("@loadEdiDate2", txtLoadEDIDate2.Text.Trim());

        sqlParam[11] = new SqlParameter("@loadQadDate1", txtLoadQADDate1.Text.Trim());
        sqlParam[12] = new SqlParameter("@loadQadDate2", txtLoadQADDate2.Text.Trim());

        //QAD
        sqlParam[13] = new SqlParameter("@so1", txtSO1.Text.Trim());
        sqlParam[14] = new SqlParameter("@so2", txtSO2.Text.Trim());

        sqlParam[15] = new SqlParameter("@soDueDate1", txtSoDueDate1.Text.Trim());
        sqlParam[16] = new SqlParameter("@soDueDate2", txtSoDueDate2.Text.Trim());

        sqlParam[17] = new SqlParameter("@wo1", txtWO1.Text.Trim());
        sqlParam[18] = new SqlParameter("@wo2", txtWO2.Text.Trim());

        sqlParam[19] = new SqlParameter("@woDueDate1", txtWoDueDate1.Text.Trim());
        sqlParam[20] = new SqlParameter("@woDueDate2", txtWoDueDate2.Text.Trim());

        //All
        sqlParam[21] = new SqlParameter("@notInEDI", chkNotInEDI.Checked);
        sqlParam[22] = new SqlParameter("@noSO", chkNoSo.Checked);

        sqlParam[23] = new SqlParameter("@backOrder", chkBackOrder.Checked);


        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectOrdTracking", sqlParam);
        return ds;
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //为导出做样式准备
            foreach (TableCell cell in e.Row.Cells)
            {
                cell.Attributes.Add("class", "text");
            }

            //如果工单已经删除的，加删除线
            if (e.Row.Cells[19].Text.Trim() == "D")
            {
                e.Row.Cells[8].Style.Add("text-decoration", "line-through");
                e.Row.Cells[9].Style.Add("text-decoration", "line-through");
                e.Row.Cells[10].Style.Add("text-decoration", "line-through");

                e.Row.Cells[19].Style.Add("text-decoration", "line-through");
                e.Row.Cells[20].Style.Add("text-decoration", "line-through");
                e.Row.Cells[21].Style.Add("text-decoration", "line-through");
                e.Row.Cells[22].Style.Add("text-decoration", "line-through");
                e.Row.Cells[23].Style.Add("text-decoration", "line-through");
                e.Row.Cells[24].Style.Add("text-decoration", "line-through");
            }
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        #region 日期格式验证
        //JDE
        if (txtOrdDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtOrdDate1.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Order Date Error!');";

                return;
            }
        }

        if (txtOrdDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtOrdDate2.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Order Date Error!');";

                return;
            }
        }

        if (txtShipDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtShipDate1.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Ship Date Error!');";

                return;
            }
        }

        if (txtShipDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtShipDate2.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Ship Date Error!');";

                return;
            }
        }
        //EDI
        if (txtLoadEDIDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtLoadEDIDate1.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Load EDI Date Error!');";

                return;
            }
        }

        if (txtLoadEDIDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtLoadEDIDate2.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Load EDI Date Error!');";

                return;
            }
        }

        if (txtLoadQADDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtLoadQADDate1.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Load QAD Date Error!');";

                return;
            }
        }

        if (txtLoadQADDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtLoadQADDate2.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Load QAD Date Error!');";

                return;
            }
        }

        //QAD
        if (txtSoDueDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtSoDueDate1.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input So Due Date Error!');";

                return;
            }
        }

        if (txtSoDueDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtSoDueDate2.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input So Due Date Error!');";

                return;
            }
        }

        if (txtWoDueDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtWoDueDate1.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Wo Rel Date Error!');";

                return;
            }
        }

        if (txtWoDueDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtWoDueDate2.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('Input Wo Rel Date Error!');";

                return;
            }
        }
        #endregion

        bindData();

        GroupRows(gvlist, 0, 0, 1);
        GroupRows(gvlist, 1, 0, 1);
        GroupRows(gvlist, 2, 0, 1);
        GroupRows(gvlist, 3, 0, 1);
        GroupRows(gvlist, 4, 0, 1);
        GroupRows(gvlist, 5, 0, 1);
        GroupRows(gvlist, 6, 0, 1);
        GroupRows(gvlist, 7, 0, 1);
        GroupRows(gvlist, 8, 0, 1);
        GroupRows(gvlist, 9, 0, 1);
        GroupRows(gvlist, 10, 0, 1);
        GroupRows(gvlist, 15, 0, 1);
        GroupRows(gvlist, 16, 0, 1);
        GroupRows(gvlist, 17, 0, 1);
        GroupRows(gvlist, 18, 0, 1);
        GroupRows(gvlist, 19, 0, 1);
    }

    //   <summary>   
    ///   合并GridView列中相同的行   
    ///   </summary>   
    ///   <param   name="GridView1">GridView对象</param>   
    ///   <param   name="cellNum">需要合并的列</param>   
    public static void GroupRows(GridView GridView1, int cellNum)
    {
        int i = 0, rowSpanNum = 1;
        while (i < GridView1.Rows.Count - 1)
        {
            GridViewRow gvr = GridView1.Rows[i];
            for (++i; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gvrNext = GridView1.Rows[i];
                if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
                {
                    gvrNext.Cells[cellNum].Visible = false;
                    rowSpanNum++;
                }
                else
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    rowSpanNum = 1;
                    break;
                }

                if (i == GridView1.Rows.Count - 1)
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                }
            }
        }
    }
    ///   <summary>   
    ///   根据条件列合并GridView列中相同的行   
    ///   </summary>   
    ///   <param   name="GridView1">GridView对象</param>   
    ///   <param   name="cellNum">需要合并的列</param>
    ///   ///   <param   name="cellNum2">条件列(根据某条件列还合并)</param>
    public static void GroupRows(GridView GridView1, int cellNum, int cellNum2, int cellNum3)
    {
        int i = 0, rowSpanNum = 1;
        while (i < GridView1.Rows.Count - 1)
        {
            GridViewRow gvr = GridView1.Rows[i];
            for (++i; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gvrNext = GridView1.Rows[i];
                if (gvr.Cells[cellNum].Text + gvr.Cells[cellNum2].Text + gvr.Cells[cellNum3].Text == gvrNext.Cells[cellNum].Text + gvrNext.Cells[cellNum2].Text + gvrNext.Cells[cellNum3].Text)
                {
                    gvrNext.Cells[cellNum].Visible = false;
                    rowSpanNum++;
                }
                else
                {
                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                    gvr.Cells[cellNum].Style.Add("border-right-color", "#A7A6AA");
                    gvr.Cells[cellNum].Style.Add("border-bottom-width", "1px");
                    rowSpanNum = 1;
                    break;
                }

                if (i == GridView1.Rows.Count - 1)
                {
                    gvr.Cells[cellNum].Style.Add("border-right-color", "#A7A6AA");
                    gvr.Cells[cellNum].Style.Add("border-bottom-width", "1px");

                    gvr.Cells[cellNum].RowSpan = rowSpanNum;
                }
            }
        }
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        bindData();

        GroupRows(gvlist, 0, 0, 1);
        GroupRows(gvlist, 1, 0, 1);
        GroupRows(gvlist, 2, 0, 1);
        GroupRows(gvlist, 3, 0, 1);
        GroupRows(gvlist, 4, 0, 1);
        GroupRows(gvlist, 5, 0, 1);
        GroupRows(gvlist, 6, 0, 1);
        GroupRows(gvlist, 7, 0, 1);
        GroupRows(gvlist, 8, 0, 1);
        GroupRows(gvlist, 9, 0, 1);
        GroupRows(gvlist, 10, 0, 1);
        GroupRows(gvlist, 15, 0, 1);
        GroupRows(gvlist, 16, 0, 1);
        GroupRows(gvlist, 17, 0, 1);
        GroupRows(gvlist, 18, 0, 1);
        GroupRows(gvlist, 19, 0, 1);
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string strCondition = " Where Isnull(op_type, '') <> 'D' ";//由于查询条件太多，故在此对where进行拼接

        //JDE
        if (txtPo1.Text.Trim() != string.Empty)
        {
            strCondition += " And ord_nbr >= '" + txtPo1.Text + "' ";
        }

        if (txtPo2.Text.Trim() != string.Empty)
        {
            strCondition += " And ord_nbr <= '" + txtPo2.Text + "' ";
        }

        if (txtItem.Text.Trim() != string.Empty)
        {
            strCondition += " And det_part Like Replace('" + txtItem.Text + "', '*', '%')' ";
        }

        if (chkComplete.Checked)
        {
            strCondition += " And det_ord_qty = Isnull(sod_qty_ship, 0) ";
        }

        //EDI 
        if (chkNotInEDI.Checked)
        {
            strCondition += " And edi_loadDate Is Null ";
        }

        //QAD
        if (txtSO1.Text.Trim() != string.Empty)
        {
            strCondition += " And so_nbr >= '" + txtSO1.Text + "' ";
        }

        if (txtSO2.Text.Trim() != string.Empty)
        {
            strCondition += " And so_nbr <= '" + txtSO2.Text + "' ";
        }

        if (txtWO1.Text.Trim() != string.Empty)
        {
            strCondition += " And wo_nbr >= '" + txtWO1.Text + "' ";
        }

        if (txtWO2.Text.Trim() != string.Empty)
        {
            strCondition += " And wo_nbr <= '" + txtWO2.Text + "' ";
        }

        if (chkNoSo.Checked)
        {
            strCondition += " And so_nbr Is Null ";
        }

        if (chkNoWo.Checked)
        {
            strCondition += " And wo_nbr Is Not Null ";
        }

        if (chkBackOrder.Checked)
        {
            strCondition += " And DateDiff(d, Isnull(wo_due_date, GetDate()), det_ship_date) < 0 ";
        }

        #region 日期格式验证
        //JDE
        if (txtOrdDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtOrdDate1.Text);

                strCondition += " And det_ord_date >= '" + txtOrdDate1.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Order Date Error!');";

                return;
            }
        }

        if (txtOrdDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtOrdDate2.Text);

                strCondition += " And det_ord_date <= '" + txtOrdDate2.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Order Date Error!');";

                return;
            }
        }

        if (txtShipDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtShipDate1.Text);

                strCondition += " And det_ship_date >= '" + txtShipDate1.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Ship Date Error!');";
                return;
            }
        }

        if (txtShipDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtShipDate2.Text);

                strCondition += " And det_ship_date <= '" + txtShipDate2.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Ship Date Error!');";

                return;
            }
        }
        //EDI
        if (txtLoadEDIDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtLoadEDIDate1.Text);

                strCondition += " And edi_loadDate >= '" + txtLoadEDIDate1.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Load EDI Date Error!');";

                return;
            }
        }

        if (txtLoadEDIDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtLoadEDIDate2.Text);

                strCondition += " And edi_loadDate <= '" + txtLoadEDIDate2.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Load EDI Date Error!');";

                return;
            }
        }

        if (txtLoadQADDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtLoadQADDate1.Text);

                strCondition += " And qad_loadDate >= '" + txtLoadQADDate1.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Load QAD Date Error!');";

                return;
            }
        }

        if (txtLoadQADDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtLoadQADDate2.Text);

                strCondition += " And qad_loadDate <= '" + txtLoadQADDate2.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Load QAD Date Error!');";

                return;
            }
        }

        //QAD
        if (txtSoDueDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtSoDueDate1.Text);

                strCondition += " And sod_due_date >= '" + txtSoDueDate1.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input So Due Date Error!');";

                return;
            }
        }

        if (txtSoDueDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtSoDueDate2.Text);

                strCondition += " And sod_due_date <= '" + txtSoDueDate2.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input So Due Date Error!');";

                return;
            }
        }

        if (txtWoDueDate1.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtWoDueDate1.Text);

                strCondition += " And wo_due_date >= '" + txtWoDueDate1.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Wo Rel Date Error!');";

                return;
            }
        }

        if (txtWoDueDate2.Text != string.Empty)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtWoDueDate2.Text);

                strCondition += " And wo_due_date <= '" + txtWoDueDate2.Text + "' ";
            }
            catch
            {
                ltlAlert.Text = "alert('Input Wo Rel Date Error!');";

                return;
            }
        }
        #endregion

        ltlAlert.Text = "window.open('EDIPoTrackExcel.aspx?condition=" + strCondition + "', '_blank');";
    }
}
