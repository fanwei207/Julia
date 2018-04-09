using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WOCProgress;
using QCProgress;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Util;

public partial class QC_qc_report : BasePage
{
    WOC oqc = new WOC();
    GridViewNullData ogv = new GridViewNullData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStd.Text = DateTime.Now.AddDays(-1) > DateTime.Parse("2009-9-1") ? DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd") : "2009-9-1";
            txtEnd.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");

            txtPageIndex.Text = "1";
            txtIndex.Text = "0";
            dType.DataSource = oqc.GetDefectType(string.Empty, 1);  //绑定巡检类别
            dType.DataBind();
            dType.Items.Insert(0, new ListItem("--", "0"));
            dType.SelectedIndex = 0;
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

            DataSet _dataset = oqc.GetReport(txtReceiver.Text.Trim(),
                                            txtOrder.Text.Trim(),
                                            txtLine.Text.Trim(),
                                            txtPart.Text.Trim(),
                                            txtCus.Text.Trim(),
                                            txtStd.Text.Trim(),
                                            txtEnd.Text.Trim(),
                                            24,
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

            ((LinkButton)e.Row.Cells[12].FindControl("LinkButton3")).Attributes.Add("onclick", "return confirm('你确定要完成吗?');");
            ((LinkButton)e.Row.Cells[13].FindControl("LinkButton5")).Attributes.Add("onclick", "return confirm('你确定要重检吗?');");

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
            string _group = gvReport.DataKeys[index].Values[2].ToString();
            string flag = gvReport.DataKeys[index].Values[3].ToString();
            string Identity = gvReport.DataKeys[index].Values[4].ToString();

            if (link.ID.Trim() == "LinkButton1")
            {
                Response.Redirect("qc_report_project.aspx?page=100103121&group=" + _group + "&id=" + Identity);
              //  if (flag != string.Empty)
               //     Response.Redirect("qc_report_project.aspx?page=100103121&group=" + _group + "&id=" + Identity);
              //  else
              //      Response.Redirect("qc_report_history.aspx?page=100103121&group=" + _group + "&id=" + Identity);
            }

            else if (link.ID.Trim() == "LinkButton2")
                Response.Redirect("qc_luminousflux.aspx?page=100103121&group=" + _group + "&id=" + Identity);

            else
                Response.Redirect("qc_luminousflux_detail.aspx?page=100103121&group=" + _group + "&id=" + Identity);

        }

        else if (e.CommandName == "expand")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)(e.CommandSource)).Parent.Parent); 

            Session["PRH_GROUP"] = gvReport.DataKeys[row.RowIndex].Values[2].ToString();
            Session["PRH_FLAG"] = "1";
        }

        else if (e.CommandName == "block")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)(e.CommandSource)).Parent.Parent);

            Session["PRH_GROUP"] = gvReport.DataKeys[row.RowIndex].Values[2].ToString();
            Session["PRH_FLAG"] = "0";
        }

        else if (e.CommandName == "compete")
        {
             GridViewRow row = (GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent);

             string group = gvReport.DataKeys[row.RowIndex].Values[2].ToString();

            //20140923 fanwei update 检验数量与进货数量相符
            if (oqc.CheckCheckNumEqualsPrhNum(group))
            {

               

                string recieve = row.Cells[1].Text.Trim();
                string order = row.Cells[2].Text.Trim();
                string line = row.Cells[3].Text.Trim();
               

                oqc.SetReportComplete(recieve, order, line, group, 2, Int32.Parse(Session["uID"].ToString()));
                ltlAlert.Text = "alert('完成检验！');";
            }
            else 
            {
                ltlAlert.Text = "alert('只有当该收货单和该单的检验数量相同后才可完成检验！');";
            
            }
        }
        else if (e.CommandName == "tecai")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent);

            string recieve = row.Cells[1].Text.Trim();
            string order = row.Cells[2].Text.Trim();
            string line = row.Cells[3].Text.Trim();
            string group = gvReport.DataKeys[row.RowIndex].Values[2].ToString();

            oqc.SetReportComplete(recieve, order, line, group, 3, Int32.Parse(Session["uID"].ToString()));
        }
        else if (e.CommandName == "tuihuo")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent);

            string recieve = row.Cells[1].Text.Trim();
            string order = row.Cells[2].Text.Trim();
            string line = row.Cells[3].Text.Trim();
            string group = gvReport.DataKeys[row.RowIndex].Values[2].ToString();

            oqc.SetReportComplete(recieve, order, line, group, 4, Int32.Parse(Session["uID"].ToString()));
        }

        else if (e.CommandName == "refuse") 
        {
            LinkButton link = (LinkButton)(e.CommandSource);
            int index = ((GridViewRow)(link.Parent.Parent)).RowIndex;
            string _group = gvReport.DataKeys[index].Values[2].ToString();

            oqc.RefuseReport(_group);
        }
        GridViewBind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime d1 = Convert.ToDateTime(txtStd.Text.Trim());
            DateTime d2 = Convert.ToDateTime(txtEnd.Text.Trim());
        }
        catch
        {
            ltlAlert.Text = "alert('请输入正常的日期！');";
            return;
        }
        DataTable dt = oqc.GetXunExportData(txtStd.Text.Trim(), txtEnd.Text.Trim(), dType.SelectedValue, txtOrder.Text.Trim(), txtReceiver.Text.Trim(), txtPart.Text.Trim(), txtCus.Text.Trim());
        ExportExcel(dt);
    }
    #region 新增导出特定格式Excel
    /// <summary>
    /// 拼接SQL语句的导出Excel通用方法(NPOI方法)
    /// </summary>
    /// <param name="dsnx">chk.dsn0()或chk.dsnx()</param>
    /// <param name="EXHeader">Header。通常位于第一行</param>
    /// <param name="EXTitle">格式如：<b>工号</b>~^250^<b>姓名</b>~^</param>
    /// <param name="EXSQL">SQL语句</param>
    /// <param name="fullDateFormat">日期格式：yyyy-MM-dd HH:mm:ss还是yyyy-MM-dd</param>
    public void ExportExcel(DataTable dt)
    {
        List<string> header = new List<string> 
        {
            "检验日期","关单日期","加工单号","ID号","QAD号","产品名称","生产数量","UL Model","PCB及铝基板版号","周期章","生产线","检验员","生产日期"
        };
        IWorkbook workbook = new HSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");

        IRow row = null;
        ICell cell = null;
        ICellStyle style = workbook.CreateCellStyle();
        IFont font = workbook.CreateFont();
        #region //设定每个单元格的长度
        for (int i = 0; i < header.Count; i++)
        {
            sheet.SetColumnWidth(i, 25 * 256);
        }

        for (int i = header.Count; i < header.Count + 10; i++)
        {
            sheet.SetColumnWidth(i, 45 * 256);
        }
        #endregion

        #region 头栏
        //row = sheet.CreateRow(1);//index代表多少行
        //SetCellRangeAddress(sheet, 1, 1, 0, 8);
        //row.HeightInPoints = 28;//行高   
        //cell = row.CreateCell(0);
        //cell.SetCellValue("镇江强凌电子有限公司");
        //cell.CellStyle = SetCellStyle(workbook, HorizontalAlignment.Center, VerticalAlignment.Top, "宋体", short.MaxValue, 16);
        #endregion

        #region 第二栏
        row = sheet.CreateRow(1);//index代表多少行
        SetCellRangeAddress(sheet, 1, 1, 0, 8);
        row.HeightInPoints = 28;//行高   
        cell = row.CreateCell(0);
        cell.SetCellValue("LED生产过控巡检表");
        cell.CellStyle = SetCellStyle(workbook, style, font, HorizontalAlignment.Center, VerticalAlignment.Top, "宋体", short.MaxValue, 14);
        #endregion

        #region 表头栏

        IRow row1 = sheet.CreateRow(2);//index代表多少行
        row1.HeightInPoints = 30;
        row = sheet.CreateRow(3);
        row.HeightInPoints = 30;
        for (int i = 0; i < header.Count; i++)
        {
            SetCellRangeAddress(sheet, 2, 3, i, i);
            row1.HeightInPoints = 20;
            cell = row1.CreateCell(i);
            cell.SetCellValue(header[i]);
            cell.CellStyle = SetCellStyle(workbook, style, font, HorizontalAlignment.Center, VerticalAlignment.Top, "宋体", short.MaxValue, 12);
        }
        style = SetCellStyle(workbook, style, font, HorizontalAlignment.Center, VerticalAlignment.Top, "宋体", short.MaxValue, 12);
        SetCellRangeAddress(sheet, 2, 2, header.Count, header.Count + 4);
        cell = row1.CreateCell(header.Count);
        cell.SetCellValue("发现质量问题");
        cell.CellStyle = style;


        //创建时间栏
        List<string> times = new List<string> 
        {
            "9:00","11:00","14:00","16:00","18:00"
        };
        int rospan = 0;
        for(int ii=0;ii<times.Count;ii++)
        {
            cell = row.CreateCell(header.Count + rospan++);
            cell.SetCellValue(times[ii]);
            cell.CellStyle = style;
            cell = row.CreateCell(header.Count + rospan);
            sheet.SetColumnWidth(header.Count + rospan, 7 * 256);
            rospan++;
            cell.SetCellValue("检验结论");
            cell.CellStyle = style;
        }
        #endregion

        #region 导出的数据
        int r = 4;        
        foreach (DataRow dr in dt.Rows)
        {
            //int maxRowNum = 1;
            row = sheet.CreateRow(r);
            row.HeightInPoints = 35;//行高            
            int lastLen=0,maxRowNum=0, colNum=0;
            bool isCreRow = false;  //是否创建了新的行
            for (int i = 0; i < dt.Columns.Count; i++)
            {              
                string s = dr[i].ToString();
                string[] a = s.Split('\n');   
                int len = a.Length - 1;                
                int index = 0;
                if (len > 0 && i > 12)
                {
                    maxRowNum = maxRowNum > len ? maxRowNum : len;//每一次循环中，记录最大的行数
                    if (isCreRow)  //if it had created Row
                    {
                        r = r-lastLen;   //向前移动行数 ，向前对齐
                    }
                    lastLen = len;   //记住上一次数组的长度
                    while (len > 0) //从第13列开始导出明细
                    {
                        string result = a[index].Substring(a[index].Length - 2);
                        string item = a[index].Substring(0,a[index].Length-2);
                        r++;
                        IRow ro = sheet.GetRow(r);
                        row = ro == null ? sheet.CreateRow(r) : ro;
                        row.HeightInPoints = 25;
                        cell = row.CreateCell(colNum);
                        cell.CellStyle = SetCellStyle(workbook, style, font, HorizontalAlignment.Left, VerticalAlignment.Top, "宋体", short.MinValue, 12);
                        cell.SetCellValue(item);

                        cell = row.CreateCell(colNum+1);
                        cell.CellStyle = SetCellStyle(workbook, style, font,HorizontalAlignment.Center, VerticalAlignment.Top, "宋体", short.MinValue, 12);
                        cell.SetCellValue(result);
                        len--;
                        index++;                                               
                    }
                    if(len==0)
                        isCreRow = true;//创建了行
                    colNum+=2;
                }
                else
                {
                    if (i < 13)
                    {
                        cell = row.CreateCell(colNum);
                        ICellStyle sty = SetCellStyle(workbook, style,font, HorizontalAlignment.Left, VerticalAlignment.Top, "宋体", short.MinValue, 12);
                        cell.CellStyle = sty;
                        cell.SetCellValue(dr[i].ToString());
                        colNum++;
                    }
                    else
                    {
                        colNum+=2;
                    }
                    
                }
            }            
            r=r+maxRowNum-lastLen+1; //以最大的行数作为标准
        }
        #endregion

        #region 下载
        string _localFileName = string.Format("{0}.xls", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);

            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.Length);
            localFile.Dispose();
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "','_blank', 'width=800,height=600,top=0,left=0');</script>");
        #endregion
    }
    /// <summary>
    /// 设置单元格格式
    /// </summary>
    /// <param name="workbook">工作簿</param>
    /// <param name="hAlign">水平格式</param>
    /// <param name="vAlign">垂直格式</param>
    /// <param name="fontName">字体名称</param>
    /// <param name="fontBold">字体粗细</param>
    /// <param name="fontSize">字体大小</param>
    /// <returns>ICellStyle</returns>
    public ICellStyle SetCellStyle(IWorkbook workbook, ICellStyle style, IFont font, HorizontalAlignment hAlign, VerticalAlignment vAlign, string fontName, short fontBold, short fontSize)
    {
        style.Alignment = hAlign;
        style.VerticalAlignment = vAlign;
        font.FontName = fontName;
        font.Boldweight = fontBold;
        font.FontHeightInPoints = fontSize;
        style.SetFont(font);
        style.WrapText = true;
        return style;
    }

    /// <summary>
    /// 合并单元格
    /// </summary>
    /// <param name="sheet">要合并单元格所在的sheet</param>
    /// <param name="rowstart">开始行的索引</param>
    /// <param name="rowend">结束行的索引</param>
    /// <param name="colstart">开始列的索引</param>
    /// <param name="colend">结束列的索引</param>
    public void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
    {
        CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
        sheet.AddMergedRegion(cellRangeAddress);
    }
    #endregion
}
