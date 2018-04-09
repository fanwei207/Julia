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
using adamFuncs;
using QADSID;
using Microsoft.ApplicationBlocks.Data;

using System.Reflection;
using System.IO;
using CommClass;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Data.SqlClient;

public partial class SID_PackingList : BasePage
{
    adamClass chk = new adamClass();
    //SID sid = new SID();
    SID_Packing packing = new SID_Packing();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Int32 Ierr = 0;

            Ierr = packing.ImportShipCheckPriceStatus(Convert.ToInt32(Session["uID"]));
            if (Ierr < 0)
            {
                btn_pricecheck.Enabled = false;
            }
        }
        DataTable dt = packing.CheckEDIWithShipPrice(Convert.ToInt32(Session["uID"]));
        if (!string.IsNullOrEmpty(dt.Rows[0]["sid_so_line"].ToString()))//dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["sid_so_cust"].ToString() == "C0000032")
            {
                btn_pricecheck.Attributes.Add("onclick", "return confirm('EDI订单与出运订单存在价格差异，NO " + dt.Rows[0]["sid_so_line"].ToString() + " 是否继续？');");
            }
        }
        BindData();
        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));
        string ship = nbr1.Rows[0]["sid_so_ship"].ToString();

        if (string.IsNullOrEmpty(txt_boxno.Text.ToString()) && ship == "C0000006")
        {
            btn_print_Invoice.Attributes.Add("onclick", "return confirm('提单号为空, 请确认是否继续？');");
            btn_print_PackingList.Attributes.Add("onclick", "return confirm('提单号为空, 请确认是否继续？');");
        }
        if ((string.IsNullOrEmpty(txt_boxno.Text.ToString()) || string.IsNullOrEmpty(txt_bl.Text.ToString())) && ship == "C0000006")
        {
            btn_print_CusInvoice.Attributes.Add("onclick", "return confirm('提单号为空, 请确认是否继续？');");
            btn_print_CusPackingList.Attributes.Add("onclick", "return confirm('提单号为空, 请确认是否继续？');");
        }
        DataTable dt1 = packing.CheckEDIWithShipPrice(Convert.ToInt32(Session["uID"]));
        if (!string.IsNullOrEmpty(dt1.Rows[0]["sid_so_line"].ToString()))//dt.Rows.Count > 0)
        {
            if (dt1.Rows[0]["sid_so_cust"].ToString() != "C0000032" && dt1.Rows[0]["sid_so_cust"].ToString() != "C0000006")
            {
                btn_pricecheck.Attributes.Add("onclick", "return confirm('EDI订单与出运订单存在价格差异请确认是否继续？');");
            }
        }
    }

    protected void BindData()
    {
        //定义参数
        System.Data.DataTable nbr = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
        txtShipNo.Text = nbr.Rows[0]["sid_pk"].ToString();
        string strNbr = nbr.Rows[0]["sid_fob"].ToString();
        string strShipdate = nbr.Rows[0]["PackingDate"].ToString();
        string strcheckpricedate = "";
        string printdate = "";
        string currdate = DateTime.Now.ToString("yyyy-MM-dd");

        if (!packing.UpdateInvoiceInfo(strNbr, txt_boxno.Text.Trim().ToString(), txt_bl.Text.Trim().ToString()))
        {
            this.Alert("装箱单号与提单号保存失败，请联系管理员！");
            return;
        }

        txt_shipdate.Text = strShipdate;

        if (!string.IsNullOrEmpty(strNbr))
        {
            System.Data.DataTable PackingMstr = packing.SelectExportInfo(strNbr);
            if (PackingMstr.Rows.Count > 0)
            {
                printdate = PackingMstr.Rows[0]["SID_printdate"].ToString().Trim();
                txt_boxno.Text = PackingMstr.Rows[0]["sid_boxno"].ToString().Trim();
                txt_bl.Text = PackingMstr.Rows[0]["sid_bl"].ToString().Trim();
                if (string.IsNullOrEmpty(txt_shipto.Text))
                {
                    txt_shipto.Text = PackingMstr.Rows[0]["sid_shipto"].ToString().Trim();
                }
                txt_lcno.Text = PackingMstr.Rows[0]["sid_lcno"].ToString().Trim();
                if (string.IsNullOrEmpty(txt_nbrno.Text))
                {
                    txt_nbrno.Text = PackingMstr.Rows[0]["sid_nbrno"].ToString().Trim();
                }
                string CheckPriceStatus = PackingMstr.Rows[0]["sid_checkprice"].ToString().Trim();

                if ( CheckPriceStatus == "True")
                {
                    txt_checkpricedate.Text = PackingMstr.Rows[0]["SID_printdate"].ToString().Trim();
                    txt_checkpricedate1.Text = PackingMstr.Rows[0]["SID_printdate"].ToString().Trim();
                    strcheckpricedate = PackingMstr.Rows[0]["SID_printdate"].ToString().Trim();
                    txt_checkpricedate.Visible = false;
                    txt_checkpricedate1.Visible = true;
                    txt_checkpricedate1.ReadOnly = true;
                    txt_checkpricedate1.Enabled = true;
                    btn_pricecheck.Enabled = false;
                }
                else
                {
                    txt_checkpricedate.Visible = true;
                    txt_checkpricedate1.Visible = false;
                    txt_checkpricedate1.ReadOnly = true;
                    txt_checkpricedate1.Enabled = false;
                    btn_pricecheck.Enabled = true;
                    if (string.IsNullOrEmpty(txt_checkpricedate.Text))
                    {
                        txt_checkpricedate.Text = currdate;
                        txt_checkpricedate1.Text = PackingMstr.Rows[0]["SID_printdate"].ToString().Trim();
                        strcheckpricedate = txt_checkpricedate.Text;
                    }
                    else
                    {
                        txt_checkpricedate1.Text = PackingMstr.Rows[0]["SID_printdate"].ToString().Trim();
                        strcheckpricedate = txt_checkpricedate.Text;
                    }

                }
            }
            else
            {
                txt_boxno.Text = string.Empty;
                txt_bl.Text = string.Empty;
                txt_shipto.Text = string.Empty;
                txt_checkpricedate.Text = strShipdate;
                strcheckpricedate = strShipdate;
                this.Alert("销售订单不存在！");
                return;
            }
            //if (strNbr.Substring(0, 1) != "3" && strNbr.Substring(0, 1) != "5" && txt_nbrno.Text == "")
            //{
            //    txt_nbrno.Text = strNbr;
            //}
            if (nbr.Rows[0]["sid_so_ship"].ToString() != "C0000004" && nbr.Rows[0]["sid_so_ship"].ToString() != "C0000067")
            {
                txt_nbrno.Text = strNbr;
            }
        }


        if (Request.QueryString["type"] != null)
        {
            gvSID.DataSource = packing.SelectPackingListInfo1(strNbr, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), strcheckpricedate);
            gvSID.DataBind();

        }
        else
        {
            gvSID.DataSource = packing.SelectPackingListInfo(strNbr, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), strcheckpricedate);
            gvSID.DataBind();
        }

        Session["EXTitle"] = "120^<b>报关发票号</b>~^120^<b>税务发票号</b>~^120^<b>发票日期</b>~^140^<b>出口核销单号</b>~^60^<b>系列</b>~^400^<b>商品名称</b>~^120^<b>商品代码</b>~^120^<b>数量</b>~^60^<b>单价</b>~^120^<b>金额</b>~^";
        Session["EXSQL"] = "";//sid.SelectDeclarationInfoExcel(strShipNo);
        Session["EXHeader"] = "/SID/ExportSIDDeclarationInfo.aspx^~^";
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSID_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvSID_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSID.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        Int32 Ierr = 0;

        Ierr = packing.ImportShipCheckPriceStatus(Convert.ToInt32(Session["uID"]));
        if (Ierr < 0)
        {
            btn_pricecheck.Enabled = false;
        }
        else
        {
            btn_pricecheck.Enabled = true;
        }
        BindData();
    }

    protected void btn_print_CusPackingList_Click(object sender, EventArgs e)
    {
        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
        string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
        string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();

        //if (string.IsNullOrEmpty(txt_bl.Text.ToString()) || string.IsNullOrEmpty(txt_boxno.Text.ToString()))
        //{
        //    this.Alert("提单号或装箱单号不能为空！");
        //    return;
        //}
        //if (!packing.UpdateInvoiceInfo(nbr, txt_boxno.Text.Trim().ToString(), txt_bl.Text.Trim().ToString()))
        //{
        //    this.Alert("装箱单号与提单号保存失败，请联系管理员！");
        //    return;
        //}
        if (string.IsNullOrEmpty(nbr))
        {
            this.Alert("请输入出运单号！");
            return;
        }
        if (string.IsNullOrEmpty(txt_checkpricedate.Text))
        {
            ltlAlert.Text = "alert('出运日期不能为空!');";
            return;
        }

        #region PDF导出法



        System.Data.DataTable poMstr = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

        if (poMstr.Rows.Count <= 0)
        {
            this.Alert("此出运单信息不全！");
            return;
        }

        if (!checkdecisfull())
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请先维护描述!');";
            return;
        }

        string billto = "";
        string shipto = "";
        string shipdate = "";
        string boxno = "";
        string bl = "";
        string lcno = "";
        string nbrno = "";
        //获取CONTAINERNO，提单号，shipto
        //System.Data.DataTable poMstr1 = packing.SelectExportInfo(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
        if (poMstr.Rows.Count > 0)
        {
            billto = poMstr.Rows[0]["SID_billto"].ToString().Trim();
            shipto = poMstr.Rows[0]["SID_shipto"].ToString().Trim();
            shipdate = poMstr.Rows[0]["SID_shipdate"].ToString().Trim();
            boxno = poMstr.Rows[0]["sid_boxno"].ToString().Trim();
            bl = poMstr.Rows[0]["SID_bl"].ToString().Trim();
            lcno = poMstr.Rows[0]["SID_lcno"].ToString().Trim();
            nbrno = poMstr.Rows[0]["SID_nbr"].ToString().Trim();
        }

        string path = Server.MapPath("/Excel/" + rcvd + ".pdf");
        string imgBar = Server.MapPath("/Excel/" + rcvd + ".Jpeg");
        string PicLogo = Server.MapPath("../images/login-logo.Jpg");
        string PicSeal = Server.MapPath("../images/Seal.jpg");

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            this.Alert("送货单正在被使用！");
            BindData();
            return;
        }

        //画条形码

        #region  Delete
        /*
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
            BindData();
            return;
        }
         */
        #endregion

        try
        {

            string strFile = string.Empty;
            PackingExcel.PackingExcel excel = null;
            if (ckb_exporttype.Checked == true)
            {
                //导出CUST装箱单到EXCEL
                //定义参数
                string strShipNo = txtShipNo.Text.Trim();

                //strFile = "SID_Cust_Packing_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                strFile = "SID_" + nbrno + "_Cust_Packing" + ".xls";//DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (ckb_version.Checked == true)
                {
                    excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_Packing.xls"), Server.MapPath("../Excel/") + strFile);
                    excel.CUSTPackingToExcel("报关单", strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]));
                }
                else
                {
                    if (!ckb_pages.Checked)
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_NewPacking.xls"), Server.MapPath("../Excel/") + strFile);
                        //excel.NEWCUSTPackingToExcel("报关单", strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), PicSeal);
                        //导出方法更改为NPOI
                        excel.NEWCUSTPackingToExcelByNPOI("报关单", strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), PicSeal);
                    }
                    else
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_PageNewPacking.xls"), Server.MapPath("../Excel/") + strFile);
                        excel.NEWCUSTPackingToExcelPageByNPOI("报关单", strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), PicSeal);
                    }
                }
                ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }
            else
            {
                //创建PDF文档
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(path, System.IO.FileMode.Create));
                document.Open();

                System.Data.DataTable poDet = packing.SelectPackingDetailsInfo(nbr, Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetDeliveryPrint(rcvd, po);

                string strDelivery = string.Empty;
                int page_size = Convert.ToInt32("18");// Convert.ToInt32(txtPageSize.Text.Trim());
                int pages = 1 + poDet.Rows.Count / page_size;
                int current = 1;

                System.Data.DataTable table = new System.Data.DataTable();
                table = poDet.Clone();

                for (int row = 0; row < poDet.Rows.Count; row++)
                {
                    //if (row <= current * page_size)
                    if (row <= current * poDet.Rows.Count)
                    {
                        table.ImportRow(poDet.Rows[row]);
                    }

                    if (row == current * page_size - 1 || row == poDet.Rows.Count - 1)
                    {
                        TempleteCusPackingList(document, imgBar, PicLogo, PicSeal, billto, shipto, boxno, bl, lcno, page_size, pages, current, nbrno, shipdate, table);

                        current += 1;
                        pages -= 1;
                        table.Rows.Clear();

                        document.NewPage();
                    }
                }

                document.Close();
                BindData();
                this.OpenWindow("/Excel/" + rcvd + ".pdf?rt=" + DateTime.Now.Millisecond.ToString() + "','PDF','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0");

                Int32 Ierr = 0;
                Ierr = packing.ImportPrintData(nbr, Convert.ToInt32(Session["uID"]));
                if (Ierr < 0)
                {
                    Response.Redirect(chk.urlRand("打印失败！请重试"));
                }
            }
        }
        catch
        {
            ;
        }
        #endregion
    }

    /// <summary>
    /// 打开一个新窗口
    /// </summary>
    /// <param name="url"></param>
    public void OpenWindow(string url)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(string), "OpenWindow", "window.open('" + url + "', '_blank','menubar=yes,scrollbars=yes,resizable=yes,width=800,height=500,top=0,left=0');", true);
    }

    public void TempleteCusPackingList(iTextSharp.text.Document document, string imgName, string PicLogo, string PicSeal, string billto, string shipto, string boxno, string bl, string lcno, int page_size, int pages, int current, string nbr, string shipdate, System.Data.DataTable rows)
    {
        BaseFont bfSong = BaseFont.CreateFont("C:/WINDOWS/Fonts/STSong.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//宋体

        iTextSharp.text.Font fontCompany = new iTextSharp.text.Font(bfSong, 16, iTextSharp.text.Font.BOLD, BaseColor.RED);//公司
        iTextSharp.text.Font fontCompanyEn = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.RED);//公司EN
        iTextSharp.text.Font fontHei = new iTextSharp.text.Font(bfSong, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//装箱单
        iTextSharp.text.Font fonthead = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//标题
        iTextSharp.text.Font fontSong = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//其他
        iTextSharp.text.Font fontTotal = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.RED);//Total

        //Title

        float[] h_widths = { 0.35f, 0.65f };
        PdfPTable Title = new PdfPTable(h_widths);
        Title.WidthPercentage = 100f;

        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(PicLogo);//imgName);
        img.ScalePercent(76);
        PdfPCell cell = new PdfPCell(img, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        Title.AddCell(cell);


        cell = new PdfPCell(new Phrase("上  海  强  凌  电  子  有  限  公  司", fontCompany));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);


        cell = new PdfPCell(new Phrase("SHANGHAI  QIANG  LING", fontCompanyEn));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontCompany));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("ELECTRONIC  CO.,LTD", fontCompanyEn));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("   ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("装 箱 单" + "(PACKING LIST)", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgName);
        //img.ScalePercent(70);
        //cell = new PdfPCell(img, false);
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        //header.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //header.AddCell(cell);

        //header
        float[] i_width = { 0.13f, 0.47f, 0.16f, 0.24f };
        PdfPTable header = new PdfPTable(i_width);
        header.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("发 票 号 码:", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("INVOICE NO:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(nbr, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("DATE:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(shipdate, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("L/C  NO:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(lcno, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);


        cell = new PdfPCell(new Phrase("TO: ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("TECHNICAL CONSUMER PRODUCTS,INC.", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("325 CAMPUS DRIVE AURORA,OHIO 44202", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("U.S.A.", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHIP TO: ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(shipto, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHIPPING DATE: ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(shipdate, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);


        #region
        /*
        //items
        float[] i_widths = { 0.04f, 0.09f,0.14f, 0.30f, 0.08f, 0.05f, 0.05f, 0.06f, 0.08f, 0.09f };
        PdfPTable item = new PdfPTable(i_widths);
        item.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("NO", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PO#", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PART", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("DESCRIPTION", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("QTY", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNIT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("NO.", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PKGS.", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("(KGS.)"+"    "+"WEIGHT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("(M3)"+"    "+"VOLUME", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        //for (int i = 0; i <= page_size; i++)
        for (int i = 0; i < rows.Rows.Count; i++)
        {
            if (i < rows.Rows.Count)
            {
                if (i == rows.Rows.Count - 1)
                {
                    cell = new PdfPCell(new Phrase("", fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_box"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_ptype"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_weight"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_volume"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_so_line"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_box"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_ptype"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_weight"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_volume"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
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
        */
        #endregion

        float[] f_total = { 0.57f, 0.13f, 0.05f, 0.06f, 0.08f, 0.09f };
        PdfPTable total = new PdfPTable(f_total);
        total.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        float[] f_widths1 = { 1f };
        PdfPTable footer1 = new PdfPTable(f_widths1);
        footer1.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        if (!string.IsNullOrEmpty(boxno))
        {
            cell = new PdfPCell(new Phrase("CONTAINER NO:" + boxno, fontSong));
            cell.BorderWidth = 0f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            footer1.AddCell(cell);
        }
        if (!string.IsNullOrEmpty(bl))
        {
            cell = new PdfPCell(new Phrase("BL:" + bl, fontSong));
            cell.BorderWidth = 0f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            footer1.AddCell(cell);
        }

        cell = new PdfPCell(new Phrase("Country of Origin: China", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        int row = 18;
        for (int i = rows.Rows.Count; i < row; i++)
        {

            cell = new PdfPCell(new Phrase(" ", fontSong));
            cell.BorderWidth = 0f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            footer1.AddCell(cell);
        }

        //footer
        float[] f_widths = { 0.05f, 0.31f, 0.36f, 0.28f };
        PdfPTable footer = new PdfPTable(f_widths);
        footer.WidthPercentage = 100f;

        //int row;
        //if (string.IsNullOrEmpty(boxno))
        //{
        //    if (string.IsNullOrEmpty(boxno))
        //    {
        //        row = 26;
        //    }
        //    else
        //    {
        //        row = 26;
        //    }
        //}
        //else
        //{
        //    if (string.IsNullOrEmpty(boxno))
        //    {
        //        row = 26;
        //    }
        //    else
        //    {
        //        row = 25;
        //    }
        //}
        //if (rows.Rows.Count < row)
        //{
        //    for (int i = 1; i < row - rows.Rows.Count; i++)
        //    {

        //        cell = new PdfPCell(new Phrase(" ", fontSong));
        //        cell.BorderWidth = 0f;
        //        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //        footer.AddCell(cell);
        //        footer.AddCell(cell);
        //        footer.AddCell(cell);
        //        footer.AddCell(cell);
        //    }
        //}

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);


        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //footer.AddCell(cell);

        iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(PicSeal);//imgName);
        img1.ScalePercent(56);
        cell = new PdfPCell(img1, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHANG HAI QIANG LING", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("ELECTRONIC CO.,LTD.", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("----------------------------------------", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("AUTHORIZED.SIGNATURE", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("NO.139 WANG DONG ROAD(S.)", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SJ  JING  SONG  JIANG", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHANGHAI201601,CHINA", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        float[] f_width = { 0.72f, 0.28f };
        PdfPTable add1 = new PdfPTable(f_width);
        add1.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("地址：上海松江泗泾望东南路139号  邮编：201601", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add1.AddCell(cell);

        cell = new PdfPCell(new Phrase("TEL:021-57619108", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add1.AddCell(cell);

        cell = new PdfPCell(new Phrase("电话：021-57619108  传真：021-57619961", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add1.AddCell(cell);

        cell = new PdfPCell(new Phrase("FAX:021-57619961", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add1.AddCell(cell);


        //for (int k = 1; k < 3; k++)
        //{

        //    cell = new PdfPCell(new Phrase(" ", fontSong));
        //    cell.BorderWidth = 0f;
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    add1.AddCell(cell);
        //    add1.AddCell(cell);
        //    add1.AddCell(cell);
        //    add1.AddCell(cell);
        //}


        //items
        float[] i_widths = { 0.04f, 0.09f, 0.14f, 0.30f, 0.08f, 0.05f, 0.11f, 0.08f, 0.09f };//0.05f, 0.06f, 0.08f, 0.09f };
        PdfPTable item = new PdfPTable(i_widths);
        item.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("NO", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PO#", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PART", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("DESCRIPTION", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("QTY", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNIT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("NO.PKGS", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("PKGS.", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        cell = new PdfPCell(new Phrase("(KGS.)" + "    " + "WEIGHT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("(M3)" + "    " + "VOLUME", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        //for (int i = 0; i <= page_size; i++)


        int j = 0;
        for (int i = 0; i < rows.Rows.Count; i++)
        {
            //for (int j = 0; j < 5; j++)
            //{

            if (i / 18 == j + 1)
            {

                //换页写入一个段落, Paragraph
                document.Add(Title);
                document.Add(header);
                document.Add(item);
                document.Add(footer1);
                document.Add(footer);
                document.Add(add1);
                item.DeleteBodyRows();
                j++;
            }
            //}


            if (i < rows.Rows.Count)
            {
                if (i == rows.Rows.Count - 1)
                {
                    cell = new PdfPCell(new Phrase("", fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pkgs"].ToString() + "  " + rows.Rows[i]["sid_ptype"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    //cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_ptype"].ToString(), fontTotal));
                    //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_weight"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_volume"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_so_line"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pkgs"].ToString() + "  " + rows.Rows[i]["sid_ptype"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    //cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_ptype"].ToString(), fontSong));
                    //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_weight"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_volume"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
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


        //写入一个段落, Paragraph
        document.Add(Title);
        document.Add(header);
        document.Add(item);
        document.Add(footer1);
        document.Add(footer);
        document.Add(add1);

    }

    public void TempletePackingList(iTextSharp.text.Document document, string imgName, string PicLogo, string PicSeal, string shipto, string shipdate, string via, int page_size, int pages, int current, string nbrno, string nbr, string bl, string boxno, System.Data.DataTable rows)
    {
        BaseFont bfSong = BaseFont.CreateFont("C:/WINDOWS/Fonts/STSong.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//宋体

        iTextSharp.text.Font fontCompany = new iTextSharp.text.Font(bfSong, 16, iTextSharp.text.Font.BOLD, BaseColor.RED);//公司
        iTextSharp.text.Font fontCompanyEn = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//公司EN
        iTextSharp.text.Font fontHei = new iTextSharp.text.Font(bfSong, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//装箱单
        iTextSharp.text.Font fonthead = new iTextSharp.text.Font(bfSong, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//标题
        iTextSharp.text.Font fontSong = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//其他
        iTextSharp.text.Font fontTotal = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.RED);//Total

        //Title
        float[] h_widths = { 1f };
        PdfPTable Title = new PdfPTable(h_widths);
        Title.WidthPercentage = 100f;

        PdfPCell cell = new PdfPCell(new Phrase("ATTN: WEIYING", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        Title.AddCell(cell);

        //items1
        float[] i_widths1 = { 0.04f, 0.16f, 0.42f, 0.38f };
        PdfPTable item1 = new PdfPTable(i_widths1);
        item1.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("", fontCompanyEn));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item1.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontCompanyEn));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item1.AddCell(cell);

        cell = new PdfPCell(new Phrase("FACTUAL PACKING LIST FOR", fontCompanyEn));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item1.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHIP DATE:" + shipdate, fontCompanyEn));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item1.AddCell(cell);

        //items
        //float[] i_widths2 = { 0.04f, 0.16f, 0.42f, 0.15f, 0.23f };//0.05f, 0.17f};
        //PdfPTable item2 = new PdfPTable(i_widths2);
        //item2.WidthPercentage = 100f;

        float[] i_widths2 = { 0.04f, 0.16f, 0.42f, 0.38f };//0.05f, 0.17f};
        PdfPTable item2 = new PdfPTable(i_widths2);
        item2.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("", fontCompanyEn));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item2.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontCompanyEn));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item2.AddCell(cell);


        cell = new PdfPCell(new Phrase("INVOICE NO." + nbrno, fontCompanyEn));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item2.AddCell(cell);


        cell = new PdfPCell(new Phrase("BY:" + via, fontCompanyEn));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item2.AddCell(cell);


        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item2.AddCell(cell);


        if (!string.IsNullOrEmpty(boxno))
        {
            cell = new PdfPCell(new Phrase("CONTAINER NO", fontCompanyEn));
            cell.BorderWidth = 1.2f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            item2.AddCell(cell);

            cell = new PdfPCell(new Phrase(boxno, fontCompanyEn));
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.BorderWidth = 1.2f;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            item2.AddCell(cell);
        }
        else
        { 
            cell = new PdfPCell(new Phrase("", fontCompanyEn));
            cell.BorderWidth = 1.2f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            item2.AddCell(cell);


            cell = new PdfPCell(new Phrase("", fontCompanyEn));
            cell.BorderWidth = 1.2f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            item2.AddCell(cell);
        }


        cell = new PdfPCell(new Phrase("TO:" + shipto, fontCompanyEn));
        cell.BorderWidth = 1.2f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item2.AddCell(cell);

        //cell = new PdfPCell(new Phrase(shipto, fontSong));
        //cell.BorderWidth = 1.2f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item2.AddCell(cell);

        //items
        float[] i_widths = { 0.04f, 0.16f, 0.42f, 0.10f, 0.05f, 0.11f, 0.12f };
        PdfPTable item = new PdfPTable(i_widths);
        item.WidthPercentage = 100f;

        //if (!string.IsNullOrEmpty(boxno))
        //{
        //    cell = new PdfPCell(new Phrase(" ", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("CONTAINER NO", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(boxno, fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(nbrno, fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);
        //}

        //if (!string.IsNullOrEmpty(bl))
        //{
        //    cell = new PdfPCell(new Phrase(" ", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("BL# ", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(bl, fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase(nbrno, fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);

        //    cell = new PdfPCell(new Phrase("", fontSong));
        //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //    item.AddCell(cell);
        //}

        //cell = new PdfPCell(new Phrase(" ", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);


        cell = new PdfPCell(new Phrase("NO", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PART", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("DESCRIPTION", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("QTY", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNIT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("CTNS", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        //cell = new PdfPCell(new Phrase("PKGS.", fontSong));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PO#", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);
        //for (int i = 0; i <= page_size; i++)
        for (int i = 0; i < rows.Rows.Count; i++)
        {
            if (i < rows.Rows.Count)
            {
                if (pages == 1 && i == rows.Rows.Count - 1)
                {
                    cell = new PdfPCell(new Phrase("", fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pkgs"].ToString() + "  " + rows.Rows[i]["sid_ptype"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    //cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_ptype"].ToString(), fontTotal));
                    //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_so_line"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pkgs"].ToString() + "  " + rows.Rows[i]["sid_ptype"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    //cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_ptype"].ToString(), fontSong));
                    //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    //cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    //item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
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
            }
        }

        float[] f_total = { 0.57f, 0.13f, 0.05f, 0.06f, 0.08f, 0.09f };
        PdfPTable total = new PdfPTable(f_total);
        total.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        float[] f_widths1 = { 1f };
        PdfPTable footer1 = new PdfPTable(f_widths1);
        footer1.WidthPercentage = 100f;


        int row = 25;
        for (int i = rows.Rows.Count; i < row; i++)
        {

            cell = new PdfPCell(new Phrase(" ", fontSong));
            cell.BorderWidth = 0f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            footer1.AddCell(cell);
        }

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase("Country of Origin: China", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        //footer
        float[] f_widths = { 0.05f, 0.31f, 0.36f, 0.28f };
        PdfPTable footer = new PdfPTable(f_widths);
        footer.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);


        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //footer.AddCell(cell);
        iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(PicSeal);//imgName);
        img1.ScalePercent(56);
        cell = new PdfPCell(img1, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        footer.AddCell(cell);



        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("----------------------------------------", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("AUTHORIZED.SIGNATURE", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        //写入一个段落, Paragraph
        document.Add(Title);
        document.Add(item1);
        document.Add(item2);
        document.Add(item);

        document.Add(footer1);
        document.Add(footer);
    }

    public void TempleteATLInvoice(iTextSharp.text.Document document, string imgName, string PicLogo, string PicSeal, string billto, string addr, string company, int page_size, int pages, int current, string LCNO, string nbr, string date, System.Data.DataTable rows)
    {
        BaseFont bfSong = BaseFont.CreateFont("C:/WINDOWS/Fonts/STSong.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//宋体

        iTextSharp.text.Font fontCompany = new iTextSharp.text.Font(bfSong, 16, iTextSharp.text.Font.BOLD, BaseColor.RED);//公司
        iTextSharp.text.Font fontCompanyEn = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.RED);//公司EN
        iTextSharp.text.Font fontHei = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//装箱单
        iTextSharp.text.Font fonthead = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//标题
        iTextSharp.text.Font fontSong = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//其他
        iTextSharp.text.Font fontTotal = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.RED);//Total

        //Title
        float[] h_widths = { 0.35f, 0.65f };
        PdfPTable Title = new PdfPTable(h_widths);
        Title.WidthPercentage = 100f;

        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(PicLogo);//imgName);
        img.ScalePercent(76);
        PdfPCell cell = new PdfPCell(img, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        Title.AddCell(cell);


        cell = new PdfPCell(new Phrase("上  海  强  凌  电  子  有  限  公  司", fontCompany));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);


        cell = new PdfPCell(new Phrase("SHANGHAI  QIANG  LING", fontCompanyEn));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("出  口  专  用 ", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("ELECTRONIC  CO.,LTD", fontCompanyEn));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("FOR    EXPORT     ONLY", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("INVOICE", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("发        票", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("HU  SONG  GUO S HUI  WAI(97)  ZI  NO.  80", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("沪松国税外 (97) 字第80号", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgName);
        //img.ScalePercent(70);
        //cell = new PdfPCell(img, false);
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        //header.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //header.AddCell(cell);

        //header
        float[] i_width = { 0.09f, 0.58f, 0.09f, 0.24f };
        PdfPTable header = new PdfPTable(i_width);
        header.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("TO:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("AURORA TECHNOLOGIES LIMITED", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("NO:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(nbr, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);


        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("ADDRESS:UNITS 1601-3 TAI RUNG BUILDING", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);


        cell = new PdfPCell(new Phrase("  ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("8 FLEMING ROAD", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("DATE:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(date, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("HONG KONG", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);


        cell = new PdfPCell(new Phrase("SHIP TO: ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(addr, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("L/C NO. : ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(LCNO, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        //items
        float[] i_widths = { 0.04f, 0.08f, 0.13f, 0.30f, 0.08f, 0.05f, 0.07f, 0.07f, 0.09f, 0.07f };
        PdfPTable item = new PdfPTable(i_widths);
        item.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("NO", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PO#", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PART", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("DESCRIPTION", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("QTY", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNIT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNIT PRICE", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("Currency", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("AMOUNT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("Currency", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        //for (int i = 0; i <= page_size; i++)
        for (int i = 0; i < rows.Rows.Count; i++)
        {
            if (i < rows.Rows.Count)
            {
                //if (i == rows.Rows.Count - 1)
                //if (rows.Rows.Count / page_size - pages > 1)
                if (pages == 1 && i == rows.Rows.Count - 1)
                {
                    cell = new PdfPCell(new Phrase("", fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_price1"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["amount1"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency1"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_so_line"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_price1"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["amount1"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency1"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
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

        float[] f_total = { 0.57f, 0.13f, 0.05f, 0.06f, 0.08f, 0.09f };
        PdfPTable total = new PdfPTable(f_total);
        total.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);


        float[] f_widths1 = { 1f };
        PdfPTable footer1 = new PdfPTable(f_widths1);
        footer1.WidthPercentage = 100f;



        int row = 18;
        for (int i = rows.Rows.Count; i < row; i++)
        {

            cell = new PdfPCell(new Phrase(" ", fontSong));
            cell.BorderWidth = 0f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            footer1.AddCell(cell);
        }


        cell = new PdfPCell(new Phrase("Country of Origin: China", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        //footer
        float[] f_widths = { 0.05f, 0.31f, 0.36f, 0.28f };
        PdfPTable footer = new PdfPTable(f_widths);
        footer.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);


        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //footer.AddCell(cell);
        iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(PicSeal);//imgName);
        img1.ScalePercent(56);
        cell = new PdfPCell(img1, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        footer.AddCell(cell);



        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHANG HAI QIANG LING", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("ELECTRONIC CO.,LTD.", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("----------------------------------------", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("AUTHORIZED.SIGNATURE", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("NO.139 WANG DONG ROAD(S.)", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SJ  JING  SONG  JIANG", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHANGHAI201601,CHINA", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        float[] f_width = { 0.72f, 0.28f };
        PdfPTable add = new PdfPTable(f_width);
        add.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("地址：上海松江泗泾望东南路139号  邮编：201601", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        cell = new PdfPCell(new Phrase("TEL:021-57619108", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        cell = new PdfPCell(new Phrase("电话：021-57619108  传真：021-57619961", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        cell = new PdfPCell(new Phrase("FAX:021-57619961", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        //写入一个段落, Paragraph
        document.Add(Title);
        document.Add(header);
        document.Add(item);
        document.Add(footer1);
        document.Add(footer);
        document.Add(add);

    }

    public void TempleteInvoice(iTextSharp.text.Document document, string imgName, string PicLogo, string PicSeal, string boxno, string billto, string shipto, string bl, int page_size, int pages, int current, string LCNO, string nbr, string date, System.Data.DataTable rows)
    {
        BaseFont bfSong = BaseFont.CreateFont("C:/WINDOWS/Fonts/STSong.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//宋体
        BaseFont bfSong1 = BaseFont.CreateFont("C:/WINDOWS/Fonts/FZSTK.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//宋体

        iTextSharp.text.Font fontATL = new iTextSharp.text.Font(bfSong1, 70, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//Total
        iTextSharp.text.Font fontCompany = new iTextSharp.text.Font(bfSong, 16, iTextSharp.text.Font.BOLD, BaseColor.RED);//公司
        iTextSharp.text.Font fontCompanyEn = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.RED);//公司EN
        iTextSharp.text.Font fontHei = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//装箱单
        iTextSharp.text.Font fonthead = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//标题
        iTextSharp.text.Font fontSong = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//其他
        iTextSharp.text.Font fontTotal = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.RED);//Total

        //Title
        float[] h_widths = { 0.35f, 0.65f };
        PdfPTable Title = new PdfPTable(h_widths);
        Title.WidthPercentage = 100f;

        PdfPCell cell = new PdfPCell(new Phrase("", fontHei));
        cell.BorderWidth = 0f;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("ATL", fontATL));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("AURORA   TECHNOLOGIES   LIMITEN", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("INVOICE", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("发        票", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);



        //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgName);
        //img.ScalePercent(70);
        //cell = new PdfPCell(img, false);
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        //header.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //header.AddCell(cell);

        //header
        float[] i_width = { 0.09f, 0.58f, 0.09f, 0.24f };
        PdfPTable header = new PdfPTable(i_width);
        header.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("TO:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("TECHNICAL CONSUMER PRODUCTS,INC.", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("NO:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(nbr, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);


        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("325 CAMPUS DRIVE AURORA,OHIO 44202", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("U.S.A.", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("DATE:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(date, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);


        cell = new PdfPCell(new Phrase("SHIP TO: ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(shipto, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("L/C NO. : ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(LCNO, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        //items
        float[] i_widths = { 0.04f, 0.08f, 0.13f, 0.30f, 0.08f, 0.05f, 0.06f, 0.07f, 0.10f, 0.07f };
        PdfPTable item = new PdfPTable(i_widths);
        item.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("NO", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PO#", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PART", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("DESCRIPTION", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("QTY", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNIT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNIT PRICE", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("Currency", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("AMOUNT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("Currency", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        //for (int i = 0; i <= page_size; i++)
        for (int i = 0; i < rows.Rows.Count; i++)
        {
            if (i < rows.Rows.Count)
            {
                //if (i == rows.Rows.Count - 1)
                if (pages == 1 && i == rows.Rows.Count - 1)
                {
                    cell = new PdfPCell(new Phrase("", fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_price2"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["amount2"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency1"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_so_line"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_price2"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["amount2"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency1"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
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

        float[] f_total = { 0.57f, 0.13f, 0.05f, 0.06f, 0.08f, 0.09f };
        PdfPTable total = new PdfPTable(f_total);
        total.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        float[] f_widths1 = { 0.50f, 0.50f };
        PdfPTable footer1 = new PdfPTable(f_widths1);
        footer1.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        if (!string.IsNullOrEmpty(bl))
        {
            cell = new PdfPCell(new Phrase("BL#: " + bl, fontSong));
            cell.BorderWidth = 0f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            footer1.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", fontSong));
            cell.BorderWidth = 0f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            footer1.AddCell(cell);
        }

        cell = new PdfPCell(new Phrase("Country of Origin: China", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        //footer
        float[] f_widths = { 0.36f, 0.36f, 0.28f };
        PdfPTable footer = new PdfPTable(f_widths);
        footer.WidthPercentage = 100f;

        int row = 21;
        for (int i = rows.Rows.Count; i < row; i++)
        {

            cell = new PdfPCell(new Phrase(" ", fontSong));
            cell.BorderWidth = 0f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            footer1.AddCell(cell);
        }

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);


        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //footer.AddCell(cell);
        iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(PicSeal);//imgName);
        img1.ScalePercent(76);
        cell = new PdfPCell(img1, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        footer.AddCell(cell);



        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("----------------------------------------", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("AUTHORIZED.SIGNATURE", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNITS 705 & 706 7/F", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("AURORA TECHNOLOGIES LIMITED", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("9 QUEEN'S ROAD CENTRAL HK", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHANGHAI201601,CHINA", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        float[] f_width = { 0.72f, 0.28f };
        PdfPTable add = new PdfPTable(f_width);
        add.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        cell = new PdfPCell(new Phrase("TEL:852-22364321", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        cell = new PdfPCell(new Phrase("FAX:021-22364322", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);


        //写入一个段落, Paragraph
        document.Add(Title);
        document.Add(header);
        document.Add(item);
        document.Add(footer1);
        document.Add(footer);
        document.Add(add);

    }

    public void TempleteCusInvoice(iTextSharp.text.Document document, string imgName, string PicLogo, string PicSeal, string billto, string shipto, string boxno, string bl, int page_size, int pages, int current, string LCNO, string nbr, string date, System.Data.DataTable rows)
    {
        BaseFont bfSong = BaseFont.CreateFont("C:/WINDOWS/Fonts/STSong.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//宋体

        iTextSharp.text.Font fontCompany = new iTextSharp.text.Font(bfSong, 16, iTextSharp.text.Font.BOLD, BaseColor.RED);//公司
        iTextSharp.text.Font fontCompanyEn = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.RED);//公司EN
        iTextSharp.text.Font fontHei = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//装箱单
        iTextSharp.text.Font fonthead = new iTextSharp.text.Font(bfSong, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);//标题
        iTextSharp.text.Font fontSong = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);//其他
        iTextSharp.text.Font fontTotal = new iTextSharp.text.Font(bfSong, 9, iTextSharp.text.Font.NORMAL, BaseColor.RED);//Total

        //Title
        float[] h_widths = { 0.35f, 0.65f };
        PdfPTable Title = new PdfPTable(h_widths);
        Title.WidthPercentage = 100f;

        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(PicLogo); //("D:\\TCP-File\\Julia\\docs\\login-logo.Jpg");//imgName);
        img.ScalePercent(76);
        PdfPCell cell = new PdfPCell(img, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        Title.AddCell(cell);


        cell = new PdfPCell(new Phrase("上  海  强  凌  电  子  有  限  公  司", fontCompany));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);


        cell = new PdfPCell(new Phrase("SHANGHAI  QIANG  LING", fontCompanyEn));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("出  口  专  用 ", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("ELECTRONIC  CO.,LTD", fontCompanyEn));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("FOR    EXPORT     ONLY", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("INVOICE", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("发        票", fontHei));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("HU  SONG  GUO S HUI  WAI(97)  ZI  NO.  80", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        cell = new PdfPCell(new Phrase("沪松国税外 (97) 字第80号", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        Title.AddCell(cell);

        //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgName);
        //img.ScalePercent(70);
        //cell = new PdfPCell(img, false);
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        //header.AddCell(cell);

        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //header.AddCell(cell);

        //header
        float[] i_width = { 0.09f, 0.58f, 0.09f, 0.24f };
        PdfPTable header = new PdfPTable(i_width);
        header.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("TO:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("TECHNICAL CONSUMER PRODUCTS,INC.", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("NO:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(nbr, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("325 CAMPUS DRIVE AURORA, OHIO 44202", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);


        cell = new PdfPCell(new Phrase("  ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("U.S.A.", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("DATE:", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(date, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("  ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);


        cell = new PdfPCell(new Phrase("SHIP TO: ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(shipto, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase("L/C NO. : ", fonthead));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        cell = new PdfPCell(new Phrase(LCNO, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        header.AddCell(cell);

        //items
        float[] i_widths = { 0.04f, 0.10f, 0.14f, 0.27f, 0.08f, 0.05f, 0.06f, 0.07f, 0.10f, 0.07f };
        PdfPTable item = new PdfPTable(i_widths);
        item.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("NO", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PO#", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("PART", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("DESCRIPTION", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("QTY", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNIT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("UNIT PRICE", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("Currency", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("AMOUNT", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        cell = new PdfPCell(new Phrase("Currency", fontSong));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        item.AddCell(cell);

        //for (int i = 0; i <= page_size; i++)
        for (int i = 0; i < rows.Rows.Count; i++)
        {
            if (i < rows.Rows.Count)
            {
                if (pages == 1 && i == rows.Rows.Count - 1)
                {
                    cell = new PdfPCell(new Phrase("", fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_price3"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["amount3"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency1"].ToString(), fontTotal));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_so_line"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_PO"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_part"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_cust_partdesc"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_pcs"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["sid_qty_unit"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_price3"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["amount3"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);

                    cell = new PdfPCell(new Phrase(rows.Rows[i]["SID_currency1"].ToString(), fontSong));
                    cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                    cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    item.AddCell(cell);
                }
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

        float[] f_total = { 0.57f, 0.13f, 0.05f, 0.06f, 0.08f, 0.09f };
        PdfPTable total = new PdfPTable(f_total);
        total.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        total.AddCell(cell);

        float[] f_widths1 = { 0.15f, 0.85f };
        PdfPTable footer1 = new PdfPTable(f_widths1);
        footer1.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase("THIS SHIPMENT CONTAINS NO", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase("SOLID-WOOD PACKING MATERIAL", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase("CONTAINER NO: ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(boxno, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase("BL#: ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(bl, fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        int row = 18;
        for (int i = rows.Rows.Count; i < row; i++)
        {

            cell = new PdfPCell(new Phrase(" ", fontSong));
            cell.BorderWidth = 0f;
            cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            footer1.AddCell(cell);
        }

        cell = new PdfPCell(new Phrase("Country of Origin: China ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        cell = new PdfPCell(new Phrase(" ", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer1.AddCell(cell);

        //footer
        float[] f_widths = { 0.05f, 0.31f, 0.36f, 0.28f };
        PdfPTable footer = new PdfPTable(f_widths);
        footer.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);


        //cell = new PdfPCell(new Phrase("", fontSong));
        //cell.BorderWidth = 0f;
        //cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        //cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        //footer.AddCell(cell);
        iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(PicSeal);//imgName);
        img1.ScalePercent(66);
        cell = new PdfPCell(img1, false);
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_BOTTOM;
        footer.AddCell(cell);



        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHANG HAI QIANG LING", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("ELECTRONIC CO.,LTD.", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("----------------------------------------", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("AUTHORIZED.SIGNATURE", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("NO.139 WANG DONG ROAD(S.)", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SJ  JING  SONG  JIANG", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        cell = new PdfPCell(new Phrase("SHANGHAI201601,CHINA", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        footer.AddCell(cell);

        float[] f_width = { 0.72f, 0.28f };
        PdfPTable add = new PdfPTable(f_width);
        add.WidthPercentage = 100f;

        cell = new PdfPCell(new Phrase("地址：上海松江泗泾望东南路139号  邮编：201601", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        cell = new PdfPCell(new Phrase("TEL:021-57619108", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        cell = new PdfPCell(new Phrase("电话：021-57619108  传真：021-57619961", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);

        cell = new PdfPCell(new Phrase("FAX:021-57619961", fontSong));
        cell.BorderWidth = 0f;
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        add.AddCell(cell);


        //写入一个段落, Paragraph
        document.Add(Title);
        document.Add(header);
        document.Add(item);
        document.Add(footer1);
        document.Add(footer);
        document.Add(add);

    }

    protected void btn_print_Invoice_Click(object sender, EventArgs e)
    {
        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));
        string nbr = nbr1.Rows[0]["sid_fob"].ToString();
        string rcvd = nbr1.Rows[0]["sid_fob"].ToString();
        if (Convert.ToDateTime(nbr1.Rows[0]["PackingDate"].ToString()) > System.DateTime.Now.AddDays(7))   //Update:有3天修改为10天 by:Wlw at:2017-03-30  
        {
            ltlAlert.Text = "alert('至少需在出运日期前七天才可开票!');";
            return;
            //if (!(Convert.ToDateTime(nbr1.Rows[0]["PackingDate"].ToString()).DayOfWeek.ToString() == "Tuesday" && Convert.ToDateTime(nbr1.Rows[0]["PackingDate"].ToString()) < System.DateTime.Now.AddDays(7)))
            //{
            //    ltlAlert.Text = "alert('至少需在出运日期前三天才可开票(下周一，二可在周五开票)!');";
            //    return;
            //}
        }
        //if (string.IsNullOrEmpty(txt_bl.Text.ToString()))
        //{
        //    this.Alert("提单号不能为空！");
        //    return;
        //}
        //if (!packing.UpdateInvoiceInfo(nbr, txt_boxno.Text.Trim().ToString(), txt_bl.Text.Trim().ToString()))
        //{
        //    this.Alert("装箱单号与提单号保存失败，请联系管理员！");
        //    return;
        //}
        if (string.IsNullOrEmpty(nbr))
        {
            this.Alert("请输入出运单号！");
            return;
        }
        if (string.IsNullOrEmpty(txt_checkpricedate.Text))
        {
            ltlAlert.Text = "alert('定价日期不能为空!');";
            return;
        }
        string checkpricedate = "";
        try
        {
            checkpricedate = txt_checkpricedate.Text.Trim();
        }
        catch
        {
            ltlAlert.Text = "alert('定价日期格式不正确!');";
            return;
        }

        if (!checkdecisfull())
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请先维护描述!');";
            return;
        }

        if (!CheckDetPriceWithPiPrice(checkpricedate))
        {
            ltlAlert.Text = "alert('价格确认与价格表价格不一致!   请核对价格表价格与所确认价格!');";
            return;
        }

        #region PDF导出法

        System.Data.DataTable poMstr = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

        if (poMstr.Rows.Count <= 0)
        {
            this.Alert("此出运单号信息不全！");
            return;
        }

        string nbrno = "";
        string billto = "";
        string shipto = "";
        string bl = "";
        string order_date = "";
        string LCNO = "";
        string boxno = "";

        //获取CONTAINERNO，提单号，shipto
        //System.Data.DataTable poMstr1 = packing.SelectExportInfo(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

        if (poMstr.Rows.Count > 0)
        {
            nbrno = poMstr.Rows[0]["SID_nbr"].ToString().Trim();
            billto = poMstr.Rows[0]["SID_billto"].ToString().Trim();//billto_name
            shipto = poMstr.Rows[0]["SID_shipto"].ToString().Trim();//company_name
            bl = poMstr.Rows[0]["SID_bl"].ToString().Trim();
            order_date = poMstr.Rows[0]["SID_shipdate"].ToString().Trim();
            LCNO = poMstr.Rows[0]["SID_lcno"].ToString().Trim();
        }

        string path = Server.MapPath("/Excel/" + rcvd + ".pdf");
        string imgBar = Server.MapPath("/Excel/" + rcvd + ".Jpeg");
        //string PicLogo = Server.MapPath("/images/" + ConfigurationManager.AppSettings["PicLogo"].ToString());
        //string PicSeal = Server.MapPath("/images/" + ConfigurationManager.AppSettings["PicSeal1"].ToString());
        string PicLogo = Server.MapPath("../images/login-logo.Jpg");
        string PicSeal = Server.MapPath("../images/SY_Seal.jpg");

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            this.Alert("送货单正在被使用！");
            BindData();
            return;
        }

        //画条形码

        #region  Delete
        /*
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
            BindData();
            return;
        }
         */
        #endregion

        try
        {

            System.Data.DataTable poDet = packing.SelectInvoiceDetailsInfo(nbr, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate);//PurchaseOrder.GetDeliveryPrint(rcvd, po);

            string strDelivery = string.Empty;
            int page_size = Convert.ToInt32("18");// Convert.ToInt32(txtPageSize.Text.Trim());
            int pages = 1 + poDet.Rows.Count / page_size;
            int current = 1;

            System.Data.DataTable table = new System.Data.DataTable();
            table = poDet.Clone();

            string error = "";
            int ErrorRecord = 0;
            packing.DelImportError(Convert.ToInt32(Session["uID"]));
            for (int row = 0; row < poDet.Rows.Count - 1; row++)
            {
                if (string.IsNullOrEmpty(poDet.Rows[row]["SID_price2"].ToString()))
                {
                    //error += poDet.Rows[row]["sid_qad"].ToString() + ";" + poDet.Rows[row]["sid_cust_part"].ToString();

                    #region 按ImportError的结构构建表
                    DataColumn column;

                    //构建ImportError
                    DataTable tblError = new DataTable("import_error");

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errInfo";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "uID";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "plantCode";
                    tblError.Columns.Add(column);

                    ErrorRecord += 1;
                    DataRow rowError;//错误表的行
                    rowError = tblError.NewRow();

                    rowError["errInfo"] = "QAD:" + poDet.Rows[row]["sid_qad"].ToString() + "对应价格不能为空";
                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                    tblError.Rows.Add(rowError);
                    //上传
                    if (tblError != null && tblError.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopyError.DestinationTableName = "dbo.ImportError";
                            bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                            bulkCopyError.ColumnMappings.Add("uID", "userID");
                            bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                            try
                            {
                                bulkCopyError.WriteToServer(tblError);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                                return;
                            }
                            finally
                            {
                                bulkCopyError.Close();
                                tblError.Dispose();
                            }
                        }
                    }
                    #endregion
                }

            }

            if (ErrorRecord > 0)
            {
                //this.Alert(error);
                Session["EXTitle"] = "500^<b>错误原因</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

                return;
            }

            string strFile = string.Empty;
            PackingExcel.PackingExcel excel = null;
            if (ckb_exporttype.Checked == true)
            {

                //定义参数
                string strShipNo = txtShipNo.Text.Trim();
                //修改导出文件名称 BY WLW AT:2016-04-07
                //strFile = "SID_TCP_Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls"; 
                strFile = "SID_" + nbrno + "_TCP_Invoice" + ".xls";//DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (ckb_version.Checked == true)
                {
                    excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_Invoice.xls"), Server.MapPath("../Excel/") + strFile);
                    excel.TCPInvoiceToExcel("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate);
                }
                else
                {
                    //使用老的方法导出
                    //excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_NewInvoice.xls"), Server.MapPath("../Excel/") + strFile);
                    //excel.NEWTCPInvoiceToExcel("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicSeal);
                    if (!ckb_pages.Checked)
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_NewInvoice.xls"), Server.MapPath("../Excel/") + strFile);
                        excel.NEWTCPInvoiceToExcelByNPOI("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicSeal);
                    }
                    else
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_PageNewInvoice.xls"), Server.MapPath("../Excel/") + strFile);
                        excel.NEWTCPInvoiceToExcelPageByNPOI("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicSeal);
                    }
                    //GC.Collect();
                }
                ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }
            else
            {

                //创建PDF文档
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(path, System.IO.FileMode.Create));
                document.Open();

                for (int row = 0; row < poDet.Rows.Count; row++)
                {

                    //if (row <= current * page_size)
                    if (row <= current * poDet.Rows.Count)
                    {
                        table.ImportRow(poDet.Rows[row]);
                    }

                    if (row == current * page_size - 1 || row == poDet.Rows.Count - 1)
                    //if (row == current * poDet.Rows.Count - 1 || row == poDet.Rows.Count - 1)
                    {
                        TempleteInvoice(document, imgBar, PicLogo, PicSeal, boxno, billto, shipto, bl, page_size, pages, current, LCNO, nbrno, order_date, table);

                        current += 1;
                        pages -= 1;
                        table.Rows.Clear();

                        document.NewPage();
                    }
                }

                document.Close();
                BindData();
                this.OpenWindow("/Excel/" + rcvd + ".pdf?rt=" + DateTime.Now.Millisecond.ToString() + "','PDF','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0");

                Int32 Ierr = 0;
                Ierr = packing.ImportPrintData(nbr, Convert.ToInt32(Session["uID"]));
                if (Ierr < 0)
                {
                    Response.Redirect(chk.urlRand("打印失败！请重试"));
                }
            }
        }
        catch
        {
            ltlAlert.Text = "alert('打印异常!');";
            return;
        }
        #endregion
    }

    protected void btn_print_AtlInvoice_Click(object sender, EventArgs e)
    {
        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
        string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
        string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();
        string checkpricedate = txt_checkpricedate.Text.Trim();
        if (Convert.ToDateTime(nbr1.Rows[0]["PackingDate"].ToString()) > System.DateTime.Now.AddDays(7))//Update:有3天修改为15天 by:Wlw at:2017-03-30  
        {
            ltlAlert.Text = "alert('至少需在出运日期前七天才可开票!');";
            return;
            //if (!(Convert.ToDateTime(nbr1.Rows[0]["PackingDate"].ToString()).DayOfWeek.ToString() == "Tuesday" && Convert.ToDateTime(nbr1.Rows[0]["PackingDate"].ToString()) < System.DateTime.Now.AddDays(7)))
            //{
            //    ltlAlert.Text = "alert('至少需在出运日期前三天才可开票(下周一，二可在周五开票)!');";
            //    return;
            //}
        }

        //if (!packing.UpdateInvoiceInfo(nbr, txt_boxno.Text.Trim().ToString(), txt_bl.Text.Trim().ToString()))
        //{
        //    this.Alert("装箱单号与提单号保存失败，请联系管理员！");
        //    return;
        //}

        if (string.IsNullOrEmpty(nbr))
        {
            this.Alert("请输入出运单号！");
            return;
        }
        if (string.IsNullOrEmpty(txt_checkpricedate.Text))
        {
            ltlAlert.Text = "alert('出运日期不能为空!');";
            return;
        }

        #region PDF导出法



        System.Data.DataTable poMstr = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

        if (poMstr.Rows.Count <= 0)
        {
            this.Alert("此出运单号信息不全！");
            return;
        }

        if (!checkdecisfull())
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请先维护描述!');";
            return;
        }

        if (!CheckDetPriceWithPiPrice(checkpricedate))
        {
            ltlAlert.Text = "alert('价格确认与价格表价格不一致!   请核对价格表价格与所确认价格!');";
            return;
        }

        string nbrno = poMstr.Rows[0]["SID_nbr"].ToString().Trim();
        string billto = poMstr.Rows[0]["SID_billto"].ToString().Trim();//billto_name
        string shipto = poMstr.Rows[0]["SID_shipto"].ToString().Trim();//company_name
        string bl = poMstr.Rows[0]["SID_bl"].ToString().Trim();
        string order_date = poMstr.Rows[0]["SID_shipdate"].ToString().Trim();
        string LCNO = poMstr.Rows[0]["SID_lcno"].ToString().Trim();

        string path = Server.MapPath("/Excel/" + rcvd + ".pdf");
        string imgBar = Server.MapPath("/Excel/" + rcvd + ".Jpeg");
        //string PicLogo = Server.MapPath("/images/" + ConfigurationManager.AppSettings["PicLogo"].ToString());
        //string PicSeal = Server.MapPath("/images/" + ConfigurationManager.AppSettings["PicSeal"].ToString());
        string PicLogo = Server.MapPath("../images/login-logo.Jpg");
        string PicSeal = Server.MapPath("../images/Seal.jpg");
        int uid = Convert.ToInt32(Session["uID"].ToString());

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            this.Alert("送货单正在被使用！");
            BindData();
            return;
        }

        try
        {
            System.Data.DataTable poDet = packing.SelectInvoiceDetailsInfo(nbr, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate);

            int page_size = Convert.ToInt32("18");// Convert.ToInt32(txtPageSize.Text.Trim());
            string strDelivery = string.Empty;
            int pages = 1 + poDet.Rows.Count / page_size;
            int current = 1;

            System.Data.DataTable table = new System.Data.DataTable();
            table = poDet.Clone();

            string error = "";
            int ErrorRecord = 0;
            packing.DelImportError(Convert.ToInt32(Session["uID"]));
            for (int row = 0; row < poDet.Rows.Count - 1; row++)
            {
                if (string.IsNullOrEmpty(poDet.Rows[row]["SID_price1"].ToString()))
                {
                    //error += poDet.Rows[row]["sid_qad"].ToString() + ";" + poDet.Rows[row]["sid_cust_part"].ToString();

                    #region 按ImportError的结构构建表
                    DataColumn column;

                    //构建ImportError
                    DataTable tblError = new DataTable("import_error");

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errInfo";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "uID";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "plantCode";
                    tblError.Columns.Add(column);

                    ErrorRecord += 1;
                    DataRow rowError;//错误表的行
                    rowError = tblError.NewRow();

                    rowError["errInfo"] = "QAD:" + poDet.Rows[row]["sid_qad"].ToString() + "对应价格不能为空";
                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                    tblError.Rows.Add(rowError);
                    //上传
                    if (tblError != null && tblError.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopyError.DestinationTableName = "dbo.ImportError";
                            bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                            bulkCopyError.ColumnMappings.Add("uID", "userID");
                            bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                            try
                            {
                                bulkCopyError.WriteToServer(tblError);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                                return;
                            }
                            finally
                            {
                                bulkCopyError.Close();
                                tblError.Dispose();
                            }
                        }
                    }
                    #endregion
                }

            }

            if (ErrorRecord > 0)
            {

                //this.Alert(error);
                Session["EXTitle"] = "500^<b>错误原因</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

                return;
            }

            string strFile = string.Empty;
            PackingExcel.PackingExcel excel = null;
            if (ckb_exporttype.Checked == true)
            {

                //定义参数
                string strShipNo = txtShipNo.Text.Trim();

                //strFile = "SID_ATL_Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                strFile = "SID_" + nbrno + "_ATL_Invoice" + ".xls";//DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (ckb_version.Checked)
                {
                    excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_ATL_Invoice.xls"), Server.MapPath("../Excel/") + strFile);
                    excel.ATLInvoiceToExcel("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicLogo, PicSeal);
                }
                else
                {
                    //QCExcel _qcexcel = new QCExcel(strFloder, Server.MapPath("../Excel/") + strImport);
                    if (!ckb_pages.Checked)
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_ATL_Invoice.xls"), Server.MapPath("../Excel/") + strFile);
                        excel.ATLInvoiceToExcelNewByNPOI("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicLogo, PicSeal);
                    }
                    else
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_ATL_PagePacking.xls"), Server.MapPath("../Excel/") + strFile);
                        excel.ATLInvoiceToExcelNewPageByNPOI("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicLogo, PicSeal);
                    }

                    GC.Collect();
                }
                ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }
            else
            {
                //创建PDF文档
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(path, System.IO.FileMode.Create));
                document.Open();



                for (int row = 0; row < poDet.Rows.Count; row++)
                {
                    //if (row <= current * page_size)
                    if (row <= current * poDet.Rows.Count)
                    {
                        table.ImportRow(poDet.Rows[row]);
                    }

                    if (row == current * page_size - 1 || row == poDet.Rows.Count - 1)
                    //if (row == current * poDet.Rows.Count - 1 || row == poDet.Rows.Count - 1)
                    {
                        TempleteATLInvoice(document, imgBar, PicLogo, PicSeal, billto, shipto, bl, page_size, pages, current, LCNO, nbrno, order_date, table);

                        current += 1;
                        pages -= 1;
                        table.Rows.Clear();

                        document.NewPage();
                    }
                }

                document.Close();
                BindData();
                this.OpenWindow("/Excel/" + rcvd + ".pdf?rt=" + DateTime.Now.Millisecond.ToString() + "','PDF','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0");
                //packing.SaveInfo(nbr, "", "", "", uid, "", 1, "atlinv");

                Int32 Ierr = 0;
                Ierr = packing.ImportPrintData(nbr, Convert.ToInt32(Session["uID"]));
                if (Ierr < 0)
                {
                    Response.Redirect(chk.urlRand("打印失败！请重试"));
                }
            }

        }
        catch
        {
            ;
        }
        BindData();
        #endregion
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        saveshipinfo();
        BindData();
    }

    protected void saveshipinfo()
    {
        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));
        string nbr = nbr1.Rows[0]["sid_fob"].ToString();
        string boxno = "";
        string bl = "";
        string shipto = "";
        string lcno = "";
        string nbrno = "";
        string shipdate = "";
        //string nbr = txtShipNo.Text.Trim();
        if (!string.IsNullOrEmpty(txt_boxno.Text))
        {
            boxno = txt_boxno.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_bl.Text))
        {
            bl = txt_bl.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_shipto.Text))
        {
            shipto = txt_shipto.Text.Trim();
        }
        int uid = Convert.ToInt32(Session["uID"].ToString());
        if (!string.IsNullOrEmpty(txt_lcno.Text))
        {
            lcno = txt_lcno.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_nbrno.Text))
        {
            nbrno = txt_nbrno.Text.Trim();
        }
        if (!string.IsNullOrEmpty(txt_checkpricedate.Text))
        {
            try
            {
                DateTime shipdate1 = Convert.ToDateTime(txt_checkpricedate.Text.Trim());
                shipdate = txt_checkpricedate.Text.Trim();
            }
            catch
            {
                ltlAlert.Text = "alert('出运日期格式不正确！');";
                return;
            }
        }
        Int32 Ierr = 0;

        Ierr = packing.ImportShipInvDataCheck(Convert.ToInt32(Session["uID"]));
        if (Ierr < 0)
        {
            ltlAlert.Text = "alert('存在价格为零或空情况!   请联系财务维护价格表!');";
            //Session["EXTitle"] = "80^<b>出运单号</b>~^80^<b>单证发票</b>~^100^<b>客户采购单</b>~^30^<b>行</b>~^120^<b>客户零件</b>~^200^<b>描述</b>~^80^<b>价格</b>~^60^<b>货币</b>~^100^<b>价格类型(PCS/SETS)</b>~^200^<b>错误信息</b>~^";
            //Session["EXHeader"] = "";
            //Session["EXSQL"] = " Select sid_nbr,sid_invoice,sid_po,sid_line,sid_cust_part,sid_cust_partdesc,sid_price,sid_ptype,sid_currency,sid_errinfo From tcpc0.dbo.SID_invTemp Where sid_createdby='" + Convert.ToInt32(Session["uID"]) + "'  Order By sid_vid ";
            //ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

            return;
        }

        try
        {
            DataTable dt = packing.SaveInfo(nbr, boxno, bl, shipto, uid, lcno, 0, nbrno, shipdate, "");
            if (dt.Rows.Count >= 0)
            {
                //SendEmailByTCPInviceATLpacking();
                ltlAlert.Text = "alert('Success!\t" + "\\n 提交信息成功;\\n');";
            }
            else
            {
                ltlAlert.Text = "alert('Fails!\t" + "\\n 提交信息失败;\\n');";
                return;
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Fails!\t" + "\\n 提交信息失败;\\n');";
            return;
        }
    }

    protected void SendEmailByTCPInviceATLpacking()
    {
        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));
        string nbr = nbr1.Rows[0]["sid_fob"].ToString();
        string rcvd = nbr1.Rows[0]["sid_fob"].ToString();
        if (string.IsNullOrEmpty(nbr))
        {
            this.Alert("请输入出运单号！");
            return;
        }
        if (string.IsNullOrEmpty(txt_checkpricedate.Text))
        {
            ltlAlert.Text = "alert('定价日期不能为空!');";
            return;
        }
        string checkpricedate = "";
        try
        {
            checkpricedate = txt_checkpricedate.Text.Trim();
        }
        catch
        {
            ltlAlert.Text = "alert('定价日期格式不正确!');";
            return;
        }

        if (!checkdecisfull())
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请先维护描述!');";
            return;
        }

        if (!CheckDetPriceWithPiPrice(checkpricedate))
        {
            ltlAlert.Text = "alert('价格确认与价格表价格不一致!   请核对价格表价格与所确认价格!');";
            return;
        }

        #region PDF导出法

        System.Data.DataTable poMstr = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

        if (poMstr.Rows.Count <= 0)
        {
            this.Alert("此出运单号信息不全！");
            return;
        }

        string nbrno = "";
        string billto = "";
        string shipto = "";
        string bl = "";
        string order_date = "";
        string LCNO = "";
        string boxno = "";

        //获取CONTAINERNO，提单号，shipto
        //System.Data.DataTable poMstr1 = packing.SelectExportInfo(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

        if (poMstr.Rows.Count > 0)
        {
            nbrno = poMstr.Rows[0]["SID_nbr"].ToString().Trim();
            billto = poMstr.Rows[0]["SID_billto"].ToString().Trim();//billto_name
            shipto = poMstr.Rows[0]["SID_shipto"].ToString().Trim();//company_name
            bl = poMstr.Rows[0]["SID_bl"].ToString().Trim();
            order_date = poMstr.Rows[0]["SID_shipdate"].ToString().Trim();
            LCNO = poMstr.Rows[0]["SID_lcno"].ToString().Trim();
        }

        string path = Server.MapPath("/Excel/" + rcvd + ".pdf");
        string imgBar = Server.MapPath("/Excel/" + rcvd + ".Jpeg");
        //string PicLogo = Server.MapPath("/images/" + ConfigurationManager.AppSettings["PicLogo"].ToString());
        //string PicSeal = Server.MapPath("/images/" + ConfigurationManager.AppSettings["PicSeal1"].ToString());
        string PicLogo = Server.MapPath("../images/login-logo.Jpg");
        string PicSeal = Server.MapPath("../images/SY_Seal.jpg");
        string filepath = "";
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            this.Alert("送货单正在被使用！");
            BindData();
            return;
        }
        try
        {
            System.Data.DataTable poDet = packing.SelectInvoiceDetailsInfo(nbr, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate);//PurchaseOrder.GetDeliveryPrint(rcvd, po);

            string strDelivery = string.Empty;
            int page_size = Convert.ToInt32("18");// Convert.ToInt32(txtPageSize.Text.Trim());
            int pages = 1 + poDet.Rows.Count / page_size;
            int current = 1;

            System.Data.DataTable table = new System.Data.DataTable();
            table = poDet.Clone();

            string error = "";
            int ErrorRecord = 0;
            packing.DelImportError(Convert.ToInt32(Session["uID"]));
            for (int row = 0; row < poDet.Rows.Count - 1; row++)
            {
                if (string.IsNullOrEmpty(poDet.Rows[row]["SID_price2"].ToString()))
                {
                    //error += poDet.Rows[row]["sid_qad"].ToString() + ";" + poDet.Rows[row]["sid_cust_part"].ToString();

                    #region 按ImportError的结构构建表
                    DataColumn column;

                    //构建ImportError
                    DataTable tblError = new DataTable("import_error");

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errInfo";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "uID";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "plantCode";
                    tblError.Columns.Add(column);

                    ErrorRecord += 1;
                    DataRow rowError;//错误表的行
                    rowError = tblError.NewRow();

                    rowError["errInfo"] = "QAD:" + poDet.Rows[row]["sid_qad"].ToString() + "对应价格不能为空";
                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                    tblError.Rows.Add(rowError);
                    //上传
                    if (tblError != null && tblError.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopyError.DestinationTableName = "dbo.ImportError";
                            bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                            bulkCopyError.ColumnMappings.Add("uID", "userID");
                            bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                            try
                            {
                                bulkCopyError.WriteToServer(tblError);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                                return;
                            }
                            finally
                            {
                                bulkCopyError.Close();
                                tblError.Dispose();
                            }
                        }
                    }
                    #endregion
                }

            }

            if (ErrorRecord > 0)
            {
                //this.Alert(error);
                Session["EXTitle"] = "500^<b>错误原因</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

                return;
            }

            string strFile = string.Empty;
            PackingExcel.PackingExcel excel = null;
            if (ckb_exporttype.Checked == true)
            {

                //定义参数
                string strShipNo = txtShipNo.Text.Trim();

                strFile = "SID_TCP_Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (ckb_version.Checked == true)
                {
                    excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_Invoice.xls"), Server.MapPath("../Excel/") + strFile);
                    excel.TCPInvoiceToExcel("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate);
                }
                else
                {
                    excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_NewInvoice.xls"), Server.MapPath("../Excel/") + strFile);
                    excel.NEWTCPInvoiceToExcel("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate,PicSeal);

                }
                ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }
            else
            {

                //创建PDF文档
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(path, System.IO.FileMode.Create));
                document.Open();

                for (int row = 0; row < poDet.Rows.Count; row++)
                {

                    //if (row <= current * page_size)
                    if (row <= current * poDet.Rows.Count)
                    {
                        table.ImportRow(poDet.Rows[row]);
                    }

                    if (row == current * page_size - 1 || row == poDet.Rows.Count - 1)
                    //if (row == current * poDet.Rows.Count - 1 || row == poDet.Rows.Count - 1)
                    {
                        TempleteInvoice(document, imgBar, PicLogo, PicSeal, boxno, billto, shipto, bl, page_size, pages, current, LCNO, nbrno, order_date, table);

                        current += 1;
                        pages -= 1;
                        table.Rows.Clear();

                        document.NewPage();
                    }
                }

                document.Close();
                BindData();
                this.OpenWindow("/Excel/" + rcvd + ".pdf?rt=" + DateTime.Now.Millisecond.ToString() + "','PDF','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0");

                Int32 Ierr = 0;
                Ierr = packing.ImportPrintData(nbr, Convert.ToInt32(Session["uID"]));
                if (Ierr < 0)
                {
                    Response.Redirect(chk.urlRand("打印失败！请重试"));
                }
            }
            filepath = "/Excel/" + strFile;
        }
        catch
        {
            ltlAlert.Text = "alert('打印异常!');";
            return;
        }

        
        Int32 Ierr1 = 0;
        Ierr1 = packing.sendemailbybl(nbr, Convert.ToInt32(Session["uID"]), filepath);
        if (Ierr1 < 0)
        {
            ltlAlert.Text = "alert('Fails!\t" + "\\n 发送邮件失败！请重试;\\n');";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('Success!\t" + "\\n 发送邮件成功;\\n');";
        }
        #endregion
    }

    protected void btn_print_CusInvoice_Click(object sender, EventArgs e)
    {

        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
        string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
        string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();
        string checkpricedate = txt_checkpricedate.Text.Trim();
        if (Convert.ToDateTime(nbr1.Rows[0]["PackingDate"].ToString()) > System.DateTime.Now.AddDays(7)) //Update:有3天修改为10天 by:Wlw at:2017-03-30  
        {
            ltlAlert.Text = "alert('至少需在出运日期前七天才可开票!');";
            return;
            //if (!(Convert.ToDateTime(nbr1.Rows[0]["PackingDate"].ToString()).DayOfWeek.ToString() == "Tuesday" && Convert.ToDateTime(nbr1.Rows[0]["PackingDate"].ToString()) < System.DateTime.Now.AddDays(7)))
            //{
            //    ltlAlert.Text = "alert('至少需在出运日期前三天才可开票(下周一，二可在周五开票)!');";
            //    return;
            //}
        }
        //if (string.IsNullOrEmpty(txt_bl.Text.ToString()) || string.IsNullOrEmpty(txt_boxno.Text.ToString()))
        //{
        //    this.Alert("提单号或装箱单号不能为空！");
        //    return;
        //}
        //if (!packing.UpdateInvoiceInfo(nbr, txt_boxno.Text.Trim().ToString(), txt_bl.Text.Trim().ToString()))
        //{
        //    this.Alert("装箱单号与提单号保存失败，请联系管理员！");
        //    return;
        //}
        if (string.IsNullOrEmpty(txtShipNo.Text))
        {
            this.Alert("请输入出运单号！");
            return;
        }
        if (string.IsNullOrEmpty(txt_checkpricedate.Text))
        {
            ltlAlert.Text = "alert('出运日期不能为空!');";
            return;
        }

        #region PDF导出法

        System.Data.DataTable poMstr = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

        if (poMstr.Rows.Count <= 0)
        {
            this.Alert("此出运单号信息不全！");
            return;
        }

        if (!checkdecisfull())
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请先维护描述!');";
            return;
        }

        if (!CheckDetPrice3WithPiPrice3(checkpricedate))
        {
            ltlAlert.Text = "alert('价格确认与价格表价格不一致!   请核对价格表价格与所确认价格!');";
            return;
        }

        string nbrno = poMstr.Rows[0]["SID_nbr"].ToString().Trim();
        string billto = poMstr.Rows[0]["SID_billto"].ToString().Trim();
        string shipto = poMstr.Rows[0]["SID_shiptocust"].ToString().Trim();//company_name
        string boxno = poMstr.Rows[0]["sid_boxno"].ToString().Trim();
        string bl = poMstr.Rows[0]["SID_bl"].ToString().Trim();
        string order_date = poMstr.Rows[0]["SID_shipdate"].ToString().Trim();
        string LCNO = poMstr.Rows[0]["SID_lcno"].ToString().Trim();

        string path = Server.MapPath("/Excel/" + rcvd + ".pdf");
        string imgBar = Server.MapPath("/Excel/" + rcvd + ".Jpeg");
        string PicLogo = Server.MapPath("../images/login-logo.Jpg");
        string PicSeal = Server.MapPath("../images/Seal.jpg");

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            this.Alert("送货单正在被使用！");
            BindData();
            return;
        }

        //画条形码

        #region  Delete
        /*
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
            BindData();
            return;
        }
         */
        #endregion

        try
        {

            System.Data.DataTable poDet = packing.SelectInvoiceDetailsInfo(nbr, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate);//PurchaseOrder.GetDeliveryPrint(rcvd, po);

            string strDelivery = string.Empty;
            int page_size = Convert.ToInt32("18");// Convert.ToInt32(txtPageSize.Text.Trim());
            int pages = 1 + poDet.Rows.Count / page_size;
            int current = 1;

            System.Data.DataTable table = new System.Data.DataTable();
            table = poDet.Clone();

            string error = "";
            int ErrorRecord = 0;
            packing.DelImportError(Convert.ToInt32(Session["uID"]));
            for (int row = 0; row < poDet.Rows.Count - 1; row++)
            {
                if (string.IsNullOrEmpty(poDet.Rows[row]["SID_price3"].ToString()))
                {
                    //error += poDet.Rows[row]["sid_qad"].ToString() + ";" + poDet.Rows[row]["sid_cust_part"].ToString();

                    #region 按ImportError的结构构建表
                    DataColumn column;

                    //构建ImportError
                    DataTable tblError = new DataTable("import_error");

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.String");
                    column.ColumnName = "errInfo";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "uID";
                    tblError.Columns.Add(column);

                    column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Int32");
                    column.ColumnName = "plantCode";
                    tblError.Columns.Add(column);

                    ErrorRecord += 1;
                    DataRow rowError;//错误表的行
                    rowError = tblError.NewRow();

                    rowError["errInfo"] = "QAD:" + poDet.Rows[row]["sid_qad"].ToString() + "对应价格不能为空";
                    rowError["uID"] = Convert.ToInt32(Session["uID"]);
                    rowError["plantCode"] = Convert.ToInt32(Session["PlantCode"]);

                    tblError.Rows.Add(rowError);
                    //上传
                    if (tblError != null && tblError.Rows.Count > 0)
                    {
                        using (SqlBulkCopy bulkCopyError = new SqlBulkCopy(chk.dsn0()))
                        {
                            bulkCopyError.DestinationTableName = "dbo.ImportError";
                            bulkCopyError.ColumnMappings.Add("errInfo", "ErrorInfo");
                            bulkCopyError.ColumnMappings.Add("uID", "userID");
                            bulkCopyError.ColumnMappings.Add("plantCode", "plantID");

                            try
                            {
                                bulkCopyError.WriteToServer(tblError);
                            }
                            catch (Exception ex)
                            {
                                ltlAlert.Text = "alert('导入错误,请联系系统管理员!');";
                                return;
                            }
                            finally
                            {
                                bulkCopyError.Close();
                                tblError.Dispose();
                            }
                        }
                    }
                    #endregion
                }

            }

            if (ErrorRecord > 0)
            {
                //this.Alert(error);
                Session["EXTitle"] = "500^<b>错误原因</b>~^";
                Session["EXHeader"] = "";
                Session["EXSQL"] = " Select ErrorInfo From tcpc0.dbo.ImportError Where userID='" + Convert.ToInt32(Session["uID"]) + "'  Order By Id ";
                ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

                return;
            }

            string strFile = string.Empty;
            PackingExcel.PackingExcel excel = null;
            if (ckb_exporttype.Checked == true)
            {
                //导出EXCEL

                //定义参数
                string strShipNo = txtShipNo.Text.Trim();

                //strFile = "SID_Cust_Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                strFile = "SID_" + nbrno + "_Cust_Invoice" + ".xls";
                if (ckb_version.Checked == true)
                {
                    excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_Invoice.xls"), Server.MapPath("../Excel/") + strFile);
                    excel.CustInvoiceToExcel("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate);
                }
                else
                {
                    if (!ckb_pages.Checked)
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_NewInvoice.xls"), Server.MapPath("../Excel/") + strFile);
                        //excel.NEWCustInvoiceToExcel("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicSeal);
                        excel.NEWCustInvoiceToExcelByNPOI("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicSeal);
                    }
                    else
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_Cust_PageNewInvoice.xls"), Server.MapPath("../Excel/") + strFile);
                        excel.NEWCustInvoiceToExcelPageByNPOI("报关单", strShipNo, false, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), checkpricedate, PicSeal);
                    }
                }
                ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }
            else
            {
                //创建PDF文档
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(path, System.IO.FileMode.Create));
                document.Open();

                for (int row = 0; row < poDet.Rows.Count; row++)
                {
                    //if (row <= current * page_size)
                    if (row <= current * poDet.Rows.Count)
                    {
                        table.ImportRow(poDet.Rows[row]);
                    }

                    if (row == current * page_size - 1 || row == poDet.Rows.Count - 1)
                    {
                        TempleteCusInvoice(document, imgBar, PicLogo, PicSeal, billto, shipto, boxno, bl, page_size, pages, current, LCNO, nbrno, order_date, table);

                        current += 1;
                        pages -= 1;
                        table.Rows.Clear();

                        document.NewPage();
                    }
                }

                document.Close();
                BindData();
                this.OpenWindow("/Excel/" + rcvd + ".pdf?rt=" + DateTime.Now.Millisecond.ToString() + "','PDF','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0");

                Int32 Ierr = 0;
                Ierr = packing.ImportPrintData(nbr, Convert.ToInt32(Session["uID"]));
                if (Ierr < 0)
                {
                    Response.Redirect(chk.urlRand("打印失败！请重试"));
                }
            }
        }
        catch
        {
            ;
        }
        #endregion
    }

    protected void btn_print_PackingList_Click(object sender, EventArgs e)
    {
        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);
        string nbr = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblPo.Text.Trim();
        string rcvd = nbr1.Rows[0]["sid_fob"].ToString();//txtShipNo.Text.Trim();//lblDelivery.Text.Trim();

        //if (string.IsNullOrEmpty(txt_bl.Text.ToString()))
        //{
        //    this.Alert("提单号不能为空！");
        //    return;
        //}
        //if (!packing.UpdateInvoiceInfo(nbr, txt_boxno.Text.Trim().ToString(), txt_bl.Text.Trim().ToString()))
        //{
        //    this.Alert("装箱单号与提单号保存失败，请联系管理员！");
        //    return;
        //}
        if (string.IsNullOrEmpty(nbr))
        {
            this.Alert("请输入出运单号！");
            return;
        }

        #region PDF导出法

        System.Data.DataTable poMstr = packing.SelectPackingListInfo1(nbr);//PurchaseOrder.GetPurchaseOrderVendandCompanyInfo(po);

        if (poMstr.Rows.Count <= 0)
        {
            this.Alert("此出运单号信息不全！");
            return;
        }

        if (!checkdecisfull())
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请先维护描述!');";
            return;
        }

        string nbrno = poMstr.Rows[0]["SID_nbr"].ToString().Trim();
        string ship_to = poMstr.Rows[0]["SID_shipto"].ToString().Trim();
        string ship_date = poMstr.Rows[0]["SID_shipdate"].ToString().Trim();
        string bl = poMstr.Rows[0]["SID_bl"].ToString().Trim();
        string boxno = poMstr.Rows[0]["sid_boxno"].ToString().Trim();
        string via = poMstr.Rows[0]["SID_via"].ToString().Trim();

        string path = Server.MapPath("/Excel/" + rcvd + ".pdf");
        string imgBar = Server.MapPath("/Excel/" + rcvd + ".Jpeg");
        string PicLogo = Server.MapPath("../images/login-logo.Jpg");
        string PicSeal = Server.MapPath("../images/Seal.jpg");

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch
        {
            this.Alert("送货单正在被使用！");
            BindData();
            return;
        }

        try
        {

            string strFile = string.Empty;
            PackingExcel.PackingExcel excel = null;
            if (ckb_exporttype.Checked == true)
            {
                //导出ATL装箱单到EXCEL，新方法使用NPOI导出
                //定义参数
                string strShipNo = txtShipNo.Text.Trim();
                //strFile = "SID_ATL_Packing_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                strFile = "SID_" + nbrno + "_ATL_Packing" + ".xls";//DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (ckb_version.Checked == true)
                {
                    //strFile = "SID_ATL_Packing_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_ATL_Packing.xls"), Server.MapPath("../Excel/") + strFile);

                    excel.ATLPackingToExcel("报关单", strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), PicSeal);
                }
                else
                {
                    //strFile = "SID_ATL_Packing_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                    if (!ckb_pages.Checked)
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_ATL_Packing.xls"), Server.MapPath("../Excel/") + strFile);
                        //excel.ATLPackingToExcel("报关单", strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), PicSeal);
                        //使用NPOI导出方法
                        excel.ATLPackingToExcelByNPOI("报关单", strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), PicSeal);
                    }
                    else
                    {
                        excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/SID_ATL_PagePacking.xls"), Server.MapPath("../Excel/") + strFile);
                        excel.ATLPackingToExcelPageByNPOI("报关单", strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), PicSeal);
                    }
                }
                ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }
            else
            {
                //创建PDF文档
                iTextSharp.text.Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4);
                iTextSharp.text.pdf.PdfWriter.GetInstance(document, new System.IO.FileStream(path, System.IO.FileMode.Create));
                document.Open();

                System.Data.DataTable poDet = packing.SelectPackingDetailsInfo(nbr, Convert.ToInt32(Session["uID"]));//PurchaseOrder.GetDeliveryPrint(rcvd, po);

                string strDelivery = string.Empty;
                int page_size = Convert.ToInt32("18");// Convert.ToInt32(txtPageSize.Text.Trim());
                int pages = 1 + poDet.Rows.Count / page_size;
                int current = 1;

                System.Data.DataTable table = new System.Data.DataTable();
                table = poDet.Clone();

                for (int row = 0; row < poDet.Rows.Count; row++)
                {
                    //if (row <= current * page_size)
                    if (row <= current * poDet.Rows.Count)
                    {
                        table.ImportRow(poDet.Rows[row]);
                    }

                    if (row == current * page_size - 1 || row == poDet.Rows.Count - 1)
                    {
                        TempletePackingList(document, imgBar, PicLogo, PicSeal, ship_to, ship_date, via, page_size, pages, current, nbrno, nbr, bl, boxno, table);

                        current += 1;
                        pages -= 1;
                        table.Rows.Clear();

                        document.NewPage();
                    }
                }

                document.Close();
                BindData();
                this.OpenWindow("/Excel/" + rcvd + ".pdf?rt=" + DateTime.Now.Millisecond.ToString() + "','PDF','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0");

                Int32 Ierr = 0;
                Ierr = packing.ImportPrintData(nbr, Convert.ToInt32(Session["uID"]));
                if (Ierr < 0)
                {
                    Response.Redirect(chk.urlRand("打印失败！请重试"));
                }
            }
        }
        catch
        {
            ;
        }
        #endregion
    }

    protected void dg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (e.Row.Cells[0].Text.Trim() != "&nbsp;" && e.Row.Cells[0].Text.Trim() != string.Empty)
            {
            }
        }
    }

    protected void dg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DownLoad")
        {
            LinkButton link = (LinkButton)e.CommandSource;

            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;
            string _filePath = gvSID.DataKeys[index].Values["CustPoDocPath"].ToString();
            ltlAlert.Text = "var w=window.open('/TecDocs/Po/" + _filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("/SID/SID_PackingList1.aspx?&type=temp&_pk=" + Request["_pk"] + "&_ShipDate1=" + Request["_ShipDate1"] + "&_ShipDate2=" + Request["_ShipDate2"] + "&_nbr=" + Request["_nbr"] + "&_pricestatus=" + Request["_pricestatus"] + "", true);
    }

    protected void btn_pricecheck_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_checkpricedate.Text))
        {
            ltlAlert.Text = "alert('定价日期不能为空!');";
            return;
        }
        if (string.IsNullOrEmpty(txt_checkpricedate1.Text))
        {
            ltlAlert.Text = "alert('定价日期不能为空!');";
            return;
        }
        if (string.IsNullOrEmpty(txt_nbrno.Text))
        {
            ltlAlert.Text = "alert('出运编号不能为空!');";
            return;
        }
        string checkpricedate = "";
        try
        {
            DateTime pricedate = Convert.ToDateTime(txt_checkpricedate.Text.Trim());
            checkpricedate = txt_checkpricedate.Text.Trim();
        }
        catch
        {
            ltlAlert.Text = "alert('出运日期格式不正确！');";
            return;
        }
        //string err = packing.CheckEDIWithShipPrice(Convert.ToInt32(Session["uID"]));
        DataTable dt = packing.CheckEDIWithShipPrice(Convert.ToInt32(Session["uID"]));
        if (!string.IsNullOrEmpty(dt.Rows[0]["sid_so_line"].ToString()))//dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["sid_so_cust"].ToString() != "C0000032" && dt.Rows[0]["sid_so_cust"].ToString() != "C0000006")
            {
                //ltlAlert.Text = "alert('EDI订单与出运订单存在价格差异无法确认! NO" + dt.Rows[0]["sid_so_line"].ToString() + "');";
                //return;
                ltlAlert.Text = "alert('EDI订单与出运订单存在价格差异请确认是否继续!" + dt.Rows[0]["sid_so_line"].ToString() + "');";
            }
        }
        Int32 Ierr = 0;
        Ierr = packing.ImportShipInvDataCheck(Convert.ToInt32(Session["uID"]));
        if (Ierr < 0)
        {
            ltlAlert.Text = "alert('存在价格为零情况!   请联系财务维护价格表!');";
            //Session["EXTitle"] = "80^<b>出运单号</b>~^80^<b>单证发票</b>~^100^<b>客户采购单</b>~^30^<b>行</b>~^120^<b>客户零件</b>~^200^<b>描述</b>~^80^<b>价格</b>~^60^<b>货币</b>~^100^<b>价格类型(PCS/SETS)</b>~^200^<b>错误信息</b>~^";
            //Session["EXHeader"] = "";
            //Session["EXSQL"] = " Select sid_nbr,sid_invoice,sid_po,sid_line,sid_cust_part,sid_cust_partdesc,sid_price,sid_ptype,sid_currency,sid_errinfo From tcpc0.dbo.SID_invTemp Where sid_createdby='" + Convert.ToInt32(Session["uID"]) + "'  Order By sid_vid ";
            //ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

            return;
        }
        if (!checkdecisfull())
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请先维护描述!');";
            return;
        }

        Ierr = 0;

        Ierr = packing.ImportShipInvDataCheckDec(Convert.ToInt32(Session["uID"]));
        if (Ierr < 0)
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请联系技术部维护部件信息!');";
            return;
        }
        //saveshipinfo();
        Ierr = packing.ImportShipInvNOCheck(Convert.ToInt32(Session["uID"]),txt_nbrno.Text);
        if (Ierr < 0)
        {
            ltlAlert.Text = "alert('发票号与系统存在差异，请保存后再做价格确认!');";
            return;
        }
        Ierr = 0;
        Ierr = packing.ImportShipInvData(Convert.ToInt32(Session["uID"]), checkpricedate);
        if (Ierr < 0)
        {
            ltlAlert.Text = "alert('单证价格保存失败!');";
            return;
        }
        else
        {
            ltlAlert.Text = "alert('单证价格保存成功!');";
        }
        Int32 Ierr1 = 0;

        Ierr1 = packing.ImportShipCheckPriceStatus(Convert.ToInt32(Session["uID"]));
        if (Ierr1 < 0)
        {
            btn_pricecheck.Enabled = false;
        }
        BindData();
    }
    protected void txt_checkpricedate_textchanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected bool checkdecisfull()
    {
        Int32 Ierr1 = 0;

        Ierr1 = packing.ImportShipInvDataCheckDec(Convert.ToInt32(Session["uID"]));
        //if (Ierr1 < 0)
        //{
        //    ltlAlert.Text = "alert('存在描述为空情况!   请联系技术部维护部件信息!');";
        //    //Session["EXTitle"] = "80^<b>出运单号</b>~^80^<b>单证发票</b>~^100^<b>客户采购单</b>~^30^<b>行</b>~^120^<b>客户零件</b>~^200^<b>描述</b>~^80^<b>价格</b>~^60^<b>货币</b>~^100^<b>价格类型(PCS/SETS)</b>~^200^<b>错误信息</b>~^";
        //    //Session["EXHeader"] = "";
        //    //Session["EXSQL"] = " Select sid_nbr,sid_invoice,sid_po,sid_line,sid_cust_part,sid_cust_partdesc,sid_price,sid_ptype,sid_currency,sid_errinfo From tcpc0.dbo.SID_invTemp Where sid_createdby='" + Convert.ToInt32(Session["uID"]) + "'  Order By sid_vid ";
        //    //ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

        //    return false;
        //}
        return true;
    }

    protected bool CheckDetPriceWithPiPrice(string checkpricedate)
    {
        Int32 Ierr1 = 0;

        Ierr1 = packing.ShipInvDataCheckDetPriceWithPiPrice(Convert.ToInt32(Session["uID"]), checkpricedate);
        if (Ierr1 < 0)
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请联系技术部维护部件信息!');";
            //Session["EXTitle"] = "80^<b>出运单号</b>~^80^<b>单证发票</b>~^100^<b>客户采购单</b>~^30^<b>行</b>~^120^<b>客户零件</b>~^200^<b>描述</b>~^80^<b>价格</b>~^60^<b>货币</b>~^100^<b>价格类型(PCS/SETS)</b>~^200^<b>错误信息</b>~^";
            //Session["EXHeader"] = "";
            //Session["EXSQL"] = " Select sid_nbr,sid_invoice,sid_po,sid_line,sid_cust_part,sid_cust_partdesc,sid_price,sid_ptype,sid_currency,sid_errinfo From tcpc0.dbo.SID_invTemp Where sid_createdby='" + Convert.ToInt32(Session["uID"]) + "'  Order By sid_vid ";
            //ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

            return false;
        }
        return true;
    }

    protected bool CheckDetPrice3WithPiPrice3(string checkpricedate)
    {
        Int32 Ierr1 = 0;

        Ierr1 = packing.ShipInvDataCheckDetPrice3WithPiPrice3(Convert.ToInt32(Session["uID"]), checkpricedate);
        if (Ierr1 < 0)
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请联系技术部维护部件信息!');";
            //Session["EXTitle"] = "80^<b>出运单号</b>~^80^<b>单证发票</b>~^100^<b>客户采购单</b>~^30^<b>行</b>~^120^<b>客户零件</b>~^200^<b>描述</b>~^80^<b>价格</b>~^60^<b>货币</b>~^100^<b>价格类型(PCS/SETS)</b>~^200^<b>错误信息</b>~^";
            //Session["EXHeader"] = "";
            //Session["EXSQL"] = " Select sid_nbr,sid_invoice,sid_po,sid_line,sid_cust_part,sid_cust_partdesc,sid_price,sid_ptype,sid_currency,sid_errinfo From tcpc0.dbo.SID_invTemp Where sid_createdby='" + Convert.ToInt32(Session["uID"]) + "'  Order By sid_vid ";
            //ltlAlert.Text = "window.open('/public/exportExcel.aspx','','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

            return false;
        }
        return true;
    }

    protected void btn_storagelist_Click(object sender, EventArgs e)
    {
        //System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));
        //string nbr = nbr1.Rows[0]["sid_fob"].ToString();
        //DataTable dt = packing.SelectPackingStorageList(nbr);
        //string title = "<b>Site</b>~^<b>PORef</b>~^<b>CustCode</b>~^<b>Batch2</b>~^<b>ProductCode</b>~^<b>Quantity</b>~^<b>Locn</b>~^<b>PalletRef</b>~^";
        //ExportExcel(title, dt, false);
        System.Data.DataTable nbr1 = packing.GetPackingInfo(Convert.ToInt32(Session["uID"]));
        string nbr = nbr1.Rows[0]["sid_fob"].ToString();
        string rcvd = nbr1.Rows[0]["sid_fob"].ToString();

        if (string.IsNullOrEmpty(nbr))
        {
            this.Alert("请输入出运单号！");
            return;
        }

        #region Excel导出法

        System.Data.DataTable poMstr = packing.SelectPackingListInfo1(nbr);

        if (poMstr.Rows.Count <= 0)
        {
            this.Alert("此出运单号信息不全！");
            return;
        }

        if (!checkdecisfull())
        {
            ltlAlert.Text = "alert('存在描述为空情况!   请先维护描述!');";
            return;
        }
        string nbrno = poMstr.Rows[0]["SID_nbr"].ToString().Trim();
        string boxno = poMstr.Rows[0]["sid_boxno"].ToString().Trim();
        string PicSeal = Server.MapPath("../images/Seal.jpg");
        try
        {

            string strFile = string.Empty;
            PackingExcel.PackingExcel excel = null;
            if (ckb_exporttype.Checked == true)
            {
                //导出ATL装箱单到EXCEL，新方法使用NPOI导出
                //定义参数
                string strShipNo = txtShipNo.Text.Trim();
                //strFile = "SID_ATL_Packing_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                strFile = "SID_" + nbrno + "_Cust_StorageList_" + boxno  + ".xls";//DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
                if (ckb_version.Checked == true)
                {
                    excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/TCP1_PreReceipt_Import.xls"), Server.MapPath("../Excel/") + strFile);
                    excel.CustStorageListToExcelByNPOI(nbrno, strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), PicSeal);
                }
                else
                {
                    excel = new PackingExcel.PackingExcel(Server.MapPath("/docs/TCP1_PreReceipt_Import.xls"), Server.MapPath("../Excel/") + strFile);
                    excel.CustStorageListToExcelByNPOI(nbrno, strShipNo, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["PlantCode"]), PicSeal);
                }
                ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            }
        }
        catch
        {
            ;
        }
        #endregion
    }
}