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
using System.Reflection;
using System.IO;
using CommClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class Purchase_RP_editDeliverysAppv :BasePage
{
    public adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtgList.Columns[9].Visible = false;

            if (Request.QueryString["type"] == "add")
            {
                lblDelivery.Text = GenerDeliveryBill(Request.QueryString["po"].ToString().Trim());
                btnPrint.Enabled = false;
            }
            else
            {
                lblDelivery.Text = Request.QueryString["prd"].ToString().Trim();
            }


            SqlDataReader reader = GetPoMstr(Request.QueryString["po"].ToString().Trim(), Request.QueryString["domain"].ToString().Trim());
            if (reader.Read())
            {
                lblPo.Text = Request.QueryString["po"].ToString().Trim();
                lblVend.Text = reader["po_vend"].ToString();
                lblSite.Text = reader["po_ship"].ToString();
                lblDomain.Text = reader["po_domain"].ToString();
                lblOrderDate.Text = reader["po_ord_date"].ToString();
                lblDueDate.Text = reader["po_due_date"].ToString();

            }
            else
            {
                this.Alert("找不到该寄售订单" + Request.QueryString["po"].ToString().Trim() + "信息!");
                return;
            }



            if (Request.QueryString["pg"] != null && Request.QueryString["pg"].ToString() != string.Empty)
            {
                dtgList.PageIndex = Convert.ToInt32(Request.QueryString["pg"]);
            }

            BindData();
        }
    }
    public SqlDataReader GetPoMstr(string ponbr, string domain)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@po_nbr", ponbr);
            param[1] = new SqlParameter("@domain", domain);
            return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_selectPoMstr", param);
        }
        catch
        {
            return null;
        }
    }
    public string GenerDeliveryBill(string poNbr)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@po_nbr", poNbr);
            param[1] = new SqlParameter("@db_nbr", SqlDbType.VarChar, 8);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_generDeliveryBill", param);

            return param[1].Value.ToString();
        }
        catch
        {
            return "Error";
        }
    }
    public DataTable GetDeliveryDetail(string prdnbr, string ponbr, string domain)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@prdnbr", prdnbr);
            param[1] = new SqlParameter("@ponbr", ponbr);
            param[2] = new SqlParameter("@domain", domain);
            param[3] = new SqlParameter("@uid", Session["uID"].ToString());
            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_selectDeliveryDetail", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public void BindData()
    {
        System.Data.DataTable dt = GetDeliveryDetail(lblDelivery.Text.Trim(), Request.QueryString["po"].ToString().Trim(), lblDomain.Text);

        if (dt.Rows.Count == 0)
        {
            lblPodCount.Text = "0";
            dt.Rows.Add(dt.NewRow());
            dtgList.DataSource = dt;
            dtgList.DataBind();
            int columnCount = dtgList.Rows[0].Cells.Count;
            dtgList.Rows[0].Cells.Clear();
            dtgList.Rows[0].Cells.Add(new TableCell());
            dtgList.Rows[0].Cells[0].ColumnSpan = columnCount;
            dtgList.Rows[0].Cells[0].Text = "没有记录";
            dtgList.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            lblPodCount.Text = dt.Rows.Count.ToString();
            dtgList.DataSource = dt;
            dtgList.DataBind();

        }
    }

    protected void dtgList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (lblPodCount.Text != "0")
            {
                System.Web.UI.WebControls.CheckBox chkSinger = (System.Web.UI.WebControls.CheckBox)e.Row.FindControl("chkSinger");


                if (Convert.ToBoolean(dtgList.DataKeys[e.Row.RowIndex].Values["isExists"]))
                {
                    //当送货数量大于订单数量的10%时此物料显示为灰色不能操作
                    int sum = Convert.ToInt32(dtgList.DataKeys[e.Row.RowIndex].Values["prd_qty_sum"]) * 100;
                    int qty_ord = Convert.ToInt32(dtgList.DataKeys[e.Row.RowIndex].Values["prd_qty_ord"]);
                    int judget_qty = sum / qty_ord;
                    //if (judget_qty >= 150) Marked By Shanzm 2014-10-09
                    if (1 == 2)
                    {
                        this.Alert("第 " + dtgList.DataKeys[e.Row.RowIndex].Values["prd_line"] + "行累计送货总量不能超过订单数量的10%!");
                    }
                    else
                    {
                        chkSinger.Checked = true;
                    }
                }
                else
                {
                    //e.Row.Cells[12].Text = string.Empty;
                }

                //DropDownList ddlfactory = (DropDownList)e.Row.Cells[11].FindControl("ddl_factory");


                //当订单行被质检停止后，则该订单行不可用


                if (e.Row.RowIndex != 0)
                {
                    if (Convert.ToInt32(dtgList.DataKeys[e.Row.RowIndex].Values["prd_line"]) == Convert.ToInt32(dtgList.DataKeys[e.Row.RowIndex - 1].Values["prd_line"]))
                    {
                        e.Row.Visible = false;
                    }
                }


                //当订单行已经上传了质检报告时，把“上传两字变为绿色”


            }
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        bool bValid = true;
        int chkNum = 0;

        foreach (GridViewRow row in dtgList.Rows)
        {
            System.Web.UI.WebControls.CheckBox chkSinger = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkSinger");
            System.Web.UI.WebControls.TextBox txt_prd_qty_dev = (System.Web.UI.WebControls.TextBox)row.FindControl("txt_prd_qty_dev");

           


            if (chkSinger.Checked)
            {
                
                 PassPrhDet(Session["uID"].ToString(),Session["uName"].ToString(),lblDelivery.Text.Trim(), dtgList.DataKeys[row.RowIndex]["prd_line"].ToString(),"1");

            

               
            }
        }
        BindData();
       
    }
    public bool TransDataToPrhDet(int createBy, string prd_nbr, string prd_po_nbr, string domain)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@create", createBy);
            param[1] = new SqlParameter("@prd_nbr", prd_nbr);
            param[2] = new SqlParameter("@prd_po_nbr", prd_po_nbr);
            param[3] = new SqlParameter("@domain", domain);
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_transDataToPrhDet", param);

            return Convert.ToBoolean(param[4].Value);
        }
        catch
        {
            return false;
        }
    }
    public bool PassPrhDet(string createBy, string createname, string prdnbr, string poline, string status)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@prdnbr", prdnbr);
            param[1] = new SqlParameter("@poline", poline);
            param[2] = new SqlParameter("@status", status);
            param[3] = new SqlParameter("@uid", createBy);
            param[4] = new SqlParameter("@uname", createname);
          
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_passDeliveryDetail", param);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool ClearTempData(int createBy)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@createBy", createBy);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_deletePrhTemp", param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
    private bool TransTempData()
    {
        if (ClearTempData(Convert.ToInt32(Session["uID"])))
        {
            #region//创建存放数据源的表prh_temp
            System.Data.DataTable prh_temp = new System.Data.DataTable("prh_temp");
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "prd_nbr";
            prh_temp.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "prd_po_nbr";
            prh_temp.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "prd_line";
            prh_temp.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "prd_qty_dev";
            prh_temp.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "prd_box_ent";
            prh_temp.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "prd_box_sca";
            prh_temp.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "prd_createBy";
            prh_temp.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.DateTime");
            column.ColumnName = "prd_createDate";
            prh_temp.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "prd_factory";
            prh_temp.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Boolean");
            column.ColumnName = "prd_selected";
            prh_temp.Columns.Add(column);

            #endregion

            foreach (GridViewRow gvRow in dtgList.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSinger = (System.Web.UI.WebControls.CheckBox)gvRow.FindControl("chkSinger");
                System.Web.UI.WebControls.TextBox txt_prd_qty_dev = (System.Web.UI.WebControls.TextBox)gvRow.FindControl("txt_prd_qty_dev");


                decimal prd_qty_dev = txt_prd_qty_dev.Text.Trim() == string.Empty ? 0 : Convert.ToDecimal(txt_prd_qty_dev.Text.Trim());


                DataRow tempRow = prh_temp.NewRow();
                tempRow["prd_nbr"] = lblDelivery.Text.Trim();
                tempRow["prd_po_nbr"] = Request.QueryString["po"].ToString().Trim();
                tempRow["prd_line"] = Convert.ToInt32(gvRow.Cells[1].Text.Trim());
                tempRow["prd_qty_dev"] = prd_qty_dev;

                tempRow["prd_createBy"] = Convert.ToInt32(Session["uID"]);
                tempRow["prd_createDate"] = DateTime.Now;

                tempRow["prd_selected"] = chkSinger.Checked;
                prh_temp.Rows.Add(tempRow);
            }

            if (prh_temp != null && prh_temp.Rows.Count > 0)
            {
                using (SqlBulkCopy bulckCopy = new SqlBulkCopy(adam.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                {
                    bulckCopy.DestinationTableName = "prh_temp";
                    bulckCopy.ColumnMappings.Add("prd_nbr", "prd_nbr");
                    bulckCopy.ColumnMappings.Add("prd_po_nbr", "prd_po_nbr");
                    bulckCopy.ColumnMappings.Add("prd_line", "prd_line");
                    bulckCopy.ColumnMappings.Add("prd_qty_dev", "prd_qty_dev");
                    bulckCopy.ColumnMappings.Add("prd_box_ent", "prd_box_ent");
                    bulckCopy.ColumnMappings.Add("prd_box_sca", "prd_box_sca");
                    bulckCopy.ColumnMappings.Add("prd_createBy", "prd_createBy");
                    bulckCopy.ColumnMappings.Add("prd_createDate", "prd_createDate");
                    bulckCopy.ColumnMappings.Add("prd_factory", "prd_factory");
                    bulckCopy.ColumnMappings.Add("prd_selected", "prd_selected");
                    try
                    {
                        bulckCopy.WriteToServer(prh_temp);
                    }
                    catch
                    {
                        return false;
                    }
                    finally
                    {
                        prh_temp.Dispose();
                    }
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("RP_deliverylistsAppv.aspx", true);


    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        bool bValid = true;
        int chkNum = 0;

        foreach (GridViewRow row in dtgList.Rows)
        {
            System.Web.UI.WebControls.CheckBox chkSinger = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkSinger");
            System.Web.UI.WebControls.TextBox txt_prd_qty_dev = (System.Web.UI.WebControls.TextBox)row.FindControl("txt_prd_qty_dev");




            if (chkSinger.Checked)
            {

                PassPrhDet(Session["uID"].ToString(), Session["uName"].ToString(), lblDelivery.Text.Trim(), dtgList.DataKeys[row.RowIndex]["prd_line"].ToString(), "0");




            }
        }
        BindData();
    }
    public DataTable GetDeliveryPrint(string prdnbr, string ponbr)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@prdnbr", prdnbr);
            param[1] = new SqlParameter("@ponbr", ponbr);

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_RP_selectDeliveryPrint", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }


    public void OpenWindow(string url)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OpenWindow", "window.open('" + url + "', '_blank','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=500,top=0,left=0');", true);
    }
    public DataTable GetPurchaseOrderVendandCompanyInfo(string pono)
    {
        try
        {
            DataSet ds = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure,
                                                  "sp_RP_selectVendandCompanyInfoNew", new SqlParameter("@pono", pono));
            return ds.Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public void Templete(iTextSharp.text.Document document, string imgName, string company, string addr, int page_size, int pages, int current, string supp, string nbr, string date, System.Data.DataTable rows)
    {
        BaseFont bfSong = BaseFont.CreateFont("C:/WINDOWS/Fonts/STSong.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//宋体

        iTextSharp.text.Font fontHei = new iTextSharp.text.Font(bfSong, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//送货单
        iTextSharp.text.Font fontSong = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//其他

        //header
        float[] h_widths = { 0.36f, 0.28f, 0.36f };
        PdfPTable header = new PdfPTable(h_widths);
        header.WidthPercentage = 100f;

        PdfPCell cell = new PdfPCell(new Phrase("", fontHei));
        cell.BorderWidth = 0f;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("送货单", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgName);
        img.ScalePercent(70);
        cell = new PdfPCell(img, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(company, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(addr, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("页: " + current.ToString() + " / " + pages.ToString(), fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("供应商: " + supp, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("采购单: " + nbr, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("送货日期: " + date, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        //items
        float[] i_widths = { 0.05f, 0.25f, 0.25f, 0.05f, 0.08f, 0.08f, 0.08f };
        PdfPTable item = new PdfPTable(i_widths);
        item.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("行号", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("物料编码", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("品名/规格/型号", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("单位", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("订单数", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("欠交数", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("送货数", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("整箱", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("零箱", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        for (int i = 0; i < page_size; i++)
        {
            if (i < rows.Rows.Count)
            {
                cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_line"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_part"].ToString() + (rows.Rows[i]["prd_factory"].ToString() == "" ? "" : "\n" + rows.Rows[i]["prd_factory"].ToString()), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_xpart"].ToString() + (rows.Rows[i]["cuspart"].ToString() == "" ? "" : "\n" + rows.Rows[i]["cuspart"].ToString()), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_um"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_qty_ord"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_qty_short"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_qty_dev"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                //cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_box_ent"].ToString(), fontSong));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                //item.AddCell(cell);

                //cell = new PdfPCell(new Phrase(rows.Rows[i]["prd_box_sca"].ToString(), fontSong));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                //item.AddCell(cell);
            }
            else
            {
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                //item.AddCell(new Phrase(" ", fontSong));
                //item.AddCell(new Phrase(" ", fontSong));
            }
        }
        //footer
        float[] f_widths = { 0.33f, 0.33f, 0.33f };
        PdfPTable footer = new PdfPTable(f_widths);
        footer.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("送货人:", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("仓库保管员:", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("记账员:", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);


        //写入一个段落, Paragraph
        document.Add(header);
        document.Add(item);
        document.Add(footer);

    }

    protected void dtgList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "docs")
        {
            Response.Redirect("supplier_docsImport.aspx?pg=" + dtgList.PageIndex.ToString() + "&po=" + lblPo.Text.Trim() + "&prd=" + lblDelivery.Text.Trim() + "&line=" + dtgList.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text + "&vend=" + lblVend.Text.Trim() + "&do=" + lblDomain.Text.Trim() + "&tp=2" + "&rm=" + DateTime.Now.ToString() + "&isDel=" + btn_Save.Enabled, true);

        }
    }




    protected void dtgList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dtgList.PageIndex = e.NewPageIndex;
        BindData();
    }
}