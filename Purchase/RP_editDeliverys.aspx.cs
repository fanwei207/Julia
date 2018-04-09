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
public partial class Purchase_RP_editDeliverys : BasePage
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


            SqlDataReader reader = GetPoMstr(Request.QueryString["po"].ToString().Trim(), Request.QueryString["domain"].ToString().Trim(), lblDelivery.Text);
            if (reader.Read())
            {
                lblPo.Text = Request.QueryString["po"].ToString().Trim();
                lblVend.Text = reader["po_vend"].ToString();
                lblSite.Text = reader["po_ship"].ToString();
                lblDomain.Text = reader["po_domain"].ToString();
                lblOrderDate.Text = reader["po_ord_date"].ToString();
                lblDueDate.Text = reader["po_due_date"].ToString();
                if (reader["prh_appv"].ToString() == "1")
                {
                    btnPrint.Enabled = true;
                    btn_Save.Enabled = false;
                }
                else
                {
                    btnPrint.Enabled = false;
                    btn_Save.Enabled = true;
                }
               string stat = reader["po_stat"].ToString();
               if (stat == "C" || stat == "c")
               {
                   btnPrint.Enabled = true;
               }
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
    public SqlDataReader GetPoMstr(string ponbr, string domain, string prh_nbr)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@po_nbr", ponbr);
            param[1] = new SqlParameter("@domain", domain);
            param[2] = new SqlParameter("@prh_nbr", prh_nbr);
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
            param[3] = new SqlParameter("@print", btn_Save.Enabled);
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
        string  line = "-1";
        foreach (GridViewRow row in dtgList.Rows)
        {
            System.Web.UI.WebControls.CheckBox chkSinger = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkSinger");
            System.Web.UI.WebControls.TextBox txt_prd_qty_dev = (System.Web.UI.WebControls.TextBox)row.FindControl("txt_prd_qty_dev");

            if (row.Cells[1].Text == line)
            {
                continue;
            }
            else
            {
                line = row.Cells[1].Text;
            }




            if (chkSinger.Checked)
            {
                string strSql2 = "SELECT  * FROM   dbo.rp_purchaseDet purdet " +
                "LEFT  JOIN dbo.rp_purchaseMstr purmstr ON purmstr.id = purdet.MID " +
                "WHERE   purdet.pur_Nbr = N'" + Request.QueryString["po"].ToString().Trim() + "' AND purdet.pur_Line = " + dtgList.DataKeys[row.RowIndex]["prd_line"].ToString() +
                "AND purmstr.rp_BusinessDept IS NOT NULL ";
                DataSet ds2;
                ds2 = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql2);
                if (!(ds2 != null && ds2.Tables[0].Rows.Count > 0))
                {
                    this.Alert("第" + dtgList.DataKeys[row.RowIndex]["prd_line"].ToString() + "行没有采购申请");
                    return;
                }

                //已送出数
                double prd_qty_sum1 = Convert.ToDouble(dtgList.DataKeys[row.RowIndex]["prd_qty_sum"].ToString());
                //送货数
                double prd_qty_dev1 = Convert.ToDouble(txt_prd_qty_dev.Text);
                string stt = dtgList.DataKeys[row.RowIndex]["prd_qty_dev"].ToString();
                if (stt == string.Empty)
                {
                    stt = "0";
                }
                double prd_qty_dev2 = Convert.ToDouble(stt);
                double sum = prd_qty_sum1 - prd_qty_dev2 + prd_qty_dev1;
                //当送货数与已送货数量之和大于订单数10%时，不能保存
                //if (sum > Convert.ToDouble(dtgList.DataKeys[row.RowIndex]["prd_qty_ord"].ToString()) * 1.5) Marked By Shanzm 2014-10-09
                if (1 == 2)
                {
                    bValid = false;
                    this.Alert("第 " + row.Cells[1].Text + "行送货数量超过订单数量10%，请重新输入送货数量");
                    BindData();
                    return;
                }
                else
                {
                    if (dtgList.DataKeys[row.RowIndex]["pt_part"].ToString() == string.Empty || dtgList.DataKeys[row.RowIndex]["pt_part"].ToString() == "")
                    {
                        this.Alert(" " + row.Cells[2].Text + "对应的产品信息在系统上不存在！请业务员联系技术部修改零件状态！");
                        BindData();
                        return;
                    }
                    chkNum = chkNum + 1;
                    if (txt_prd_qty_dev.Text.Trim() != string.Empty)
                    {
                        try
                        {
                            decimal prd_qty_dev = Convert.ToDecimal(txt_prd_qty_dev.Text.Trim());
                            if (prd_qty_dev <= 0)
                            {
                                bValid = false;
                                string error = "行号:" + row.Cells[1].Text.Trim() + "送货数必须大于0!";
                                this.Alert("" + error + "");
                                break;
                            }
                        }
                        catch
                        {
                            bValid = false;
                            string error = "行号:" + row.Cells[1].Text.Trim() + "送货数必须为数字格式!";
                            this.Alert("" + error + "");
                            break;
                        }
                    }
                    else
                    {
                        bValid = false;
                        string error = "行号:" + row.Cells[1].Text.Trim() + "送货数不能为空!";
                        this.Alert("" + error + "");
                        break;
                    }


                }
            }
        }

        if (chkNum == 0)
        {
            bValid = false;
            this.Alert("请选择需要参与到该送货单发运的物料!");
            BindData();
            return;
        }

        if (bValid)
        {
            if (TransTempData())
            {
                if (TransDataToPrhDet(Convert.ToInt32(Session["uID"]), lblDelivery.Text.Trim(), Request.QueryString["po"].ToString().Trim(), lblDomain.Text.Trim()))
                {
                    BindData();
                    // btnPrint.Enabled = true;
                }
                else
                {
                    this.Alert("发运失败,请联系管理员!");
                    BindData();
                    return;
                }
            }
            else
            {
                this.Alert("保存到临时表失败,请联系管理员!");
                BindData();
                return;
            }
        }
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

    public bool setEmail(string prd_nbr)
    {
        try
        {
            string sql = "SELECT  DISTINCT mstr.createBy,us.email FROM dbo.prh_det prh LEFT JOIN dbo.rp_purchaseDet det ON prh.prd_po_nbr = det.pur_Nbr AND prh.prd_line = det.pur_Line " +
                "LEFT JOIN  dbo.rp_purchaseMstr mstr ON mstr.id = det.MID " +
                "LEFT JOIN tcpc0..users us ON us.userID = mstr.createBy " +
                "WHERE prh.prd_nbr = '" + prd_nbr + "'";
            DataTable dt1 = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
            DataTable dt = null;
            for (int row = 0; row < dt1.Rows.Count; row++)
            {
                string createBy = dt1.Rows[row]["createBy"].ToString().Trim();
                string email = dt1.Rows[row]["email"].ToString().Trim();
                if (email != string.Empty)
                {

                    
                    sql = "SELECT mstr.rp_No,det.pur_Line,prh.prd_part,prh.prd_xpart,prd_qty_dev = CAST(prh.prd_qty_dev AS FLOAT) FROM dbo.prh_det prh " +

                     "LEFT JOIN dbo.rp_purchaseDet det ON prh.prd_po_nbr = det.pur_Nbr AND prh.prd_line = det.pur_Line " +
                     "LEFT JOIN  dbo.rp_purchaseMstr mstr ON mstr.id = det.MID " +
                     "WHERE prh.prd_nbr = '" + prd_nbr + "' AND mstr.createBy = " + createBy;
                    dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
                    string body = "你的采购申请即将入库:<br/>";
                    body += "<table border='1' cellspacing='0' cellpadding='0'>" +
                    "<tr><th>申请单号</th><th>行号</th><th>QAD</th><th>描述</th><th>入库数量</th></tr>";

                    //    N'</table>'	
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        body += "<tr><th>" + dt.Rows[i]["rp_No"].ToString().Trim() + "</th><th>" + dt.Rows[i]["pur_Line"].ToString().Trim() + "</th><th>" + dt.Rows[i]["prd_part"].ToString().Trim() + "</th><th>" + dt.Rows[i]["prd_xpart"].ToString().Trim() + "</th><th>" + dt.Rows[i]["prd_qty_dev"].ToString().Trim() + "</th></tr>";
                    }
                    body += "</table>";
                    this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), email, "", "你的采购申请即将入库", body);
                    dt.Clear();
                    
                }
            }
            return true;
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
        if (Request.QueryString["type"] == "add")
        {
            Response.Redirect("RP_polist.aspx", true);
        }
        else
        {
            Response.Redirect("RP_deliverylists.aspx", true);
        }


    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //检查订单是否需要重新确认,直接全选
        


        if (txtPageSize.Text.Length == 0)
        {
            txtPageSize.Text = "10";
        }
        else
        {
            try
            {
                int intFormat = Convert.ToInt32(txtPageSize.Text.Trim());

                if (intFormat <= 0)
                {
                    this.Alert("送货单每页显示记录数只能为大于0的整数！");
                    BindData();
                    return;
                }
            }
            catch
            {
                this.Alert("送货单每页显示记录数只能为大于0的整数！");
                BindData();
                return;
            }
        }
        setEmail(lblDelivery.Text);
       
        #region 新的PDF导出法
        string po = lblPo.Text.Trim();
        string rcvd = lblDelivery.Text.Trim();
        int page_size = Convert.ToInt32(txtPageSize.Text.Trim());

        System.Data.DataTable poMstr = GetPurchaseOrderVendandCompanyInfo(po);

        string vend_name = poMstr.Rows[0]["vend"].ToString().Trim();
        string company_name = poMstr.Rows[0]["company"].ToString().Trim();
        string company_address = poMstr.Rows[0]["address"].ToString().Trim();
        string order_date = poMstr.Rows[0]["orderdate"].ToString().Trim();
        string po_consignment = Convert.ToBoolean(poMstr.Rows[0]["po_consignment"]) ? "寄售单:" : "采购单:";

        string path = Server.MapPath("/Temp/" + rcvd + ".pdf");
        string imgBar = Server.MapPath("/Temp/" + rcvd + ".Jpeg");




        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            this.Alert("调转单正在被使用！");
            BindData();
            return;
        }
       
        //画条形码
        try
        {
            MemoryStream ms = new MemoryStream();
            System.Drawing.Image myimg = BarCodeHelper.MakeBarcodeImage(rcvd, 1, true);
            myimg.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Position = 0;
            byte[] data = null;
            data = new byte[ms.Length];
            ms.Read(data, 0, Convert.ToInt32(ms.Length));
            ms.Flush();
           
            File.WriteAllBytes(imgBar, data);
        }
        catch
        {
            this.Alert("条形码创建失败！");
            //this.Alert("条形码创建失败！");
            BindData();
            return;
        }

        try
        {
            //创建PDF文档
            iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
            iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(path, System.IO.FileMode.Create));
            document.Open();

            System.Data.DataTable poDet = GetDeliveryPrint(rcvd, po);

            string strDelivery = string.Empty;
            int pages = 1 + poDet.Rows.Count / page_size;
            int current = 1;

            System.Data.DataTable table = new System.Data.DataTable();
            table = poDet.Clone();
            for (int row = 0; row < poDet.Rows.Count; row++)
            {
                if (row < current * page_size)
                {
                    table.ImportRow(poDet.Rows[row]);
                }

                if (row == current * page_size - 1 || row == poDet.Rows.Count - 1)
                {
                    Templete(document, imgBar, company_name, company_address, page_size, pages, current, vend_name, po, order_date, table);

                    current += 1;
                    table.Rows.Clear();

                    document.NewPage();
                }
            }


            document.Close();
            BindData();
            OpenWindow("/Temp/" + rcvd + ".pdf?rt=" + DateTime.Now.Millisecond.ToString() + "','PDF','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0");
           
        }
        catch
        {
            ;
        }
        #endregion
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

        iTextSharp.text.Font fontHei = new iTextSharp.text.Font(bfSong, 15, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//送货单
        iTextSharp.text.Font fontSong = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//其他

        //header
        float[] h_widths = { 0.36f, 0.28f, 0.36f };
        PdfPTable header = new PdfPTable(h_widths);
        header.WidthPercentage = 100f;

        PdfPCell cell = new PdfPCell(new Phrase("", fontHei));
        cell.BorderWidth = 0f;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("零星物资收货流转单", fontHei));
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
        float[] i_widths = { 0.05f, 0.25f, 0.25f, 0.05f, 0.08f, 0.08f, 0.08f, 0.12f, 0.08f, 0.09f };
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

        cell = new PdfPCell(new Phrase("采购申请", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("数量", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("未税价格", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        for (int i = 0; i < page_size; i++)
        {
            if (i < rows.Rows.Count)
            {
               

                if (i > 0)
                {
                    if (rows.Rows[i]["prd_line"].ToString() == rows.Rows[i - 1]["prd_line"].ToString())
                    {
                        cell = new PdfPCell(new Phrase(" ", fontSong));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        item.AddCell(cell);

                        cell = new PdfPCell(new Phrase(" ", fontSong));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        item.AddCell(cell);

                        cell = new PdfPCell(new Phrase(" ", fontSong));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        item.AddCell(cell);

                        cell = new PdfPCell(new Phrase(" ", fontSong));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        item.AddCell(cell);

                        cell = new PdfPCell(new Phrase(" ", fontSong));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        item.AddCell(cell);

                        cell = new PdfPCell(new Phrase(" ", fontSong));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        item.AddCell(cell);

                        cell = new PdfPCell(new Phrase(" ", fontSong));
                        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                        item.AddCell(cell);
                    }
                    else
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
                    }
                }
                else
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
                }

                cell = new PdfPCell(new Phrase(rows.Rows[i]["rp_No"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["rp_Qty"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);

                cell = new PdfPCell(new Phrase(rows.Rows[i]["rp_Price"].ToString(), fontSong));
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                item.AddCell(cell);
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
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
                item.AddCell(new Phrase(" ", fontSong));
            }
        }
        //footer
        float[] f_widths = { 0.33f, 0.33f, 0.33f };
        PdfPTable footer = new PdfPTable(f_widths);
        footer.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("采购员:_______________", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("仓库保管员:_______________", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("记账员:_______________", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        float[] d_widths = { 0.5f };
        PdfPTable footerd = new PdfPTable(d_widths);
        footerd.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("财务确认发票价格不高于订单价格:_______________（财务签字）", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footerd.AddCell(cell);

        //写入一个段落, Paragraph
        document.Add(header);
        document.Add(item);
        document.Add(footer);
        document.Add(footerd);
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
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < dtgList.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)dtgList.Rows[i].FindControl("chkSinger");
            if (i > 0)
            { 
              
                if (dtgList.Rows[i].Cells[1].Text == dtgList.Rows[i - 1].Cells[1].Text)
                {
                    cb.Checked = false;
                    continue;
                }
            }
            if (chkAll.Checked)
            {
                cb.Checked = true;
            }
            else
            {
                cb.Checked = false;
            }
        }
    }
}