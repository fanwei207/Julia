using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Text;

public partial class part_Npart_skipFlowIntoPartList : BasePage
{

    Npart_help help = new Npart_help();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindDDLModleType();
        }
       
    }

    private void bindDDLModleType()
    {
        ddlModle.Items.Clear();
        ddlModle.DataSource = help.selectAllTypeMstr();
        ddlModle.DataBind();
        ddlModle.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
    }

    /// <summary>
    /// 形成模板的方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbModle_Click(object sender, EventArgs e)
    {




        if (ddlModle.SelectedValue.Equals("00000000-0000-0000-0000-000000000000"))
        {
            Alert("请选择一个模板");
            return;
        }

        string flag = help.checkModleEnumComplete(ddlModle.SelectedValue);

        if (flag == "0")
        {
            Alert("该模板中枚举类型的列,没有维护枚举值,请联系DCC");
            return;
        }

        XSSFWorkbook workbook = new XSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("excel");


  
        
        //string title = //help.selectAllTypeDetByMstrIDReturnString(ddlModle.SelectedValue);
        
        

        DataTable dtlist = help.getGvColByImportPartList(ddlModle.SelectedValue);

        int total = dtlist.Rows.Count;
        //string[] titleSub = title.Split(new char[] { '~' });
        StringBuilder title = new StringBuilder();

        DataTable dt = new DataTable("temp");
        DataColumn col;

        foreach (DataRow colName in dtlist.Rows)
        {
            col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = colName["colName"].ToString();
            dt.Columns.Add(col);
            title.Append("100^<b>" + colName["colName"].ToString() + "</b>~^");
        }
        IList<ExcelTitle> ItemList = GetExcelTitles(title.ToString());
        //头栏样式
        ICellStyle styleHeader = SetHeaderStyle2007(workbook);

        //写标题栏
        IRow rowHeader = sheet.CreateRow(0);
        SetColumnTitleAndStyle2007(workbook, sheet, ItemList, dt, styleHeader, rowHeader, true);

        //写约束
        setDataValidationHelper(workbook, sheet);

        ////明细样式
        //ICellStyle styleDet = SetDetStyle2007(workbook); 

        ////写明细数据
        //SetDetailsValue2007(workbook, sheet, total, dt, 1, styleDet, fullDateFormat);

        dt.Reset();

        string _localFileName = string.Format("{0}.xlsx", DateTime.Now.ToFileTime().ToString());

        using (MemoryStream ms = new MemoryStream())
        {
            workbook.Write(ms);

            Stream localFile = new FileStream(Server.MapPath("/Excel/") + _localFileName, FileMode.OpenOrCreate);
            localFile.Write(ms.ToArray(), 0, (int)ms.ToArray().Length);
            localFile.Dispose();
            ms.Flush();
            //ms.Position = 0;
            sheet = null;
            workbook = null;
        }

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "ExportExcel", "<script language=\"JavaScript\" type=\"text/javascript\">window.open('/Excel/" + _localFileName + "', '_blank', 'width=800,height=600,top=0,left=0');</script>");

    }

    private class ExcelTitle
    {
        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        private int _width;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get
            {
                return this._width;
            }
            set
            {
                this._width = value;
            }
        }

        public ExcelTitle(string name, int width)
        {
            this.Name = name;
            this.Width = width;
        }

        public ExcelTitle()
        {

        }
    }

    private IList<ExcelTitle> GetExcelTitles(string EXTitle)
    {
        var ItemList = new List<ExcelTitle>();

        string str = EXTitle;
        int total = 0;
        int ind = 0;
        while (str.Length > 0)
        {
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                total = total + 1;
                str = "";
                break;
            }
            total = total + 1;
            str = str.Substring(ind + 2);
        }

        str = EXTitle;

        for (int i = 0; i <= total - 1; i++)
        {
            ExcelTitle item = new ExcelTitle();
            int width = 100 * 6000 / 164;
            ind = str.IndexOf("~^");
            if (ind == -1)
            {
                ind = str.IndexOf("L~");
                if (ind > -1)
                {
                    str = str.Substring(2);
                }

                ind = str.IndexOf("^");
                if (ind == -1)
                {
                    item.Name = str.Substring(2);
                    item.Width = width;
                }
                else
                {
                    item.Name = str.Substring(ind + 1);
                    item.Width = Convert.ToInt32(str.Substring(0, ind)) * 6000 / 164;
                }
                str = "";
                break;
            }
            else
            {
                item.Name = str.Substring(0, ind);
                item.Width = width;
                str = str.Substring(ind + 2);

                ind = item.Name.IndexOf("L~");
                if (ind > -1)
                {
                    item.Name = item.Name.Substring(2);
                }

                ind = item.Name.IndexOf("^");
                if (ind > -1)
                {
                    item.Width = Convert.ToInt32(item.Name.Substring(0, ind)) * 6000 / 164;
                    item.Name = item.Name.Substring(ind + 1);
                }
            }

            item.Name = item.Name.Replace("<b>", "").Replace("</b>", "");
            ItemList.Add(item);
        }
        return ItemList;
    }


    private ICellStyle SetHeaderStyle2007(IWorkbook workbook)
    {
        ICellStyle styleHeader = workbook.CreateCellStyle();
        styleHeader.Alignment = HorizontalAlignment.Center;//居中对齐

        styleHeader.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
        styleHeader.FillPattern = FillPattern.SolidForeground;

        styleHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        styleHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleHeader.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        IFont fontHeader = workbook.CreateFont();
        fontHeader.FontHeightInPoints = 10;
        fontHeader.Boldweight = 600;
        fontHeader.FontName = "Arial";
        fontHeader.Boldweight = short.MaxValue;
        styleHeader.SetFont(fontHeader);

        return styleHeader;
    }

    private void SetColumnTitleAndStyle2007(IWorkbook workbook, ISheet sheet, IList<ExcelTitle> ItemList, DataTable dt, ICellStyle styleHeader, IRow rowHeader, bool fullDateFormat)
    {
        int total = ItemList.Count;
        foreach (ExcelTitle item in ItemList)
        {
            int titleIndex = ItemList.IndexOf(item);
            sheet.SetColumnWidth(titleIndex, item.Width);

            ICell cell = rowHeader.CreateCell(titleIndex);
            cell.CellStyle = styleHeader;
            cell.SetCellValue(item.Name);

            int dtCol = 0;

            if (dt.Columns.Count == total)
            {
                dtCol = titleIndex;
            }
            else
            {
                dtCol = titleIndex + (dt.Columns.Count - total);
            }

            ICellStyle columnStyle = SetColumnStyleByDataType(workbook, dt.Columns[dtCol].DataType.ToString(), fullDateFormat);
            sheet.SetDefaultColumnStyle(titleIndex, columnStyle);
        }
    }

    private ICellStyle SetColumnStyleByDataType(IWorkbook workbook, string dataType, bool fullDateFormat)
    {
        ICellStyle style = workbook.CreateCellStyle();
        IFont font = workbook.CreateFont();
        style.VerticalAlignment = VerticalAlignment.Center;
        IDataFormat dataFormat = workbook.CreateDataFormat();
        short formatIndex;
        if (fullDateFormat)
        {
            formatIndex = dataFormat.GetFormat("yyyy-MM-dd hh:mm:ss");
        }
        else
        {
            formatIndex = dataFormat.GetFormat("yyyy-MM-dd");
        }
        switch (dataType)
        {
            case "System.DateTime":
                style.Alignment = HorizontalAlignment.Center;
                style.DataFormat = formatIndex;
                break;
            case "System.Int16":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int32":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Int64":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Decimal":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Double":
                style.Alignment = HorizontalAlignment.Right;
                break;
            case "System.Boolean":
                style.Alignment = HorizontalAlignment.Center;
                break;
            case "System.String":
                style.Alignment = HorizontalAlignment.Left;
                style.WrapText = true;
                break;
        }
        style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        style.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        style.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BottomBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
        style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

        font.FontHeightInPoints = 9;
        style.SetFont(font);
        return style;
    }

    private void setDataValidationHelper(XSSFWorkbook workbook, ISheet sheet)
    {

        //IName range = workbook.CreateName();//创建一个命名公式  
        //range.NameName = "aaa";//公式内容，就是上面的区域  
        //range.NameName = "sectionName";//公式名称，可以在"公式"-->"名称管理器"中看到  

        //string[3] aaa = { "itemA", "itemB", "itemC" };
        //string[] aaa = new string[3];
        //aaa[0]="1";
        //aaa[1]="2";
        //aaa[2]="3";

        SqlDataReader sdr = help.selectAllEnumByTypeIDByImport(ddlModle.SelectedValue);

        IList<string> list = new List<string>();
        bool flst = true;//标志第一次运行
        string numFlag = string.Empty;

        while (sdr.Read())
        {
            if (flst)
            {

                if (!sdr["enum"].ToString().Equals(string.Empty))
                {
                    list.Add(sdr["enum"].ToString());
                }

                numFlag = sdr["num"].ToString();

                flst = false;
            }
            else
            {
                if (sdr["num"].ToString().Equals(numFlag))
                {
                    if (!sdr["enum"].ToString().Equals(string.Empty))
                    {
                        list.Add(sdr["enum"].ToString());
                    }


                }
                else
                {


                    if (list.Count != 0)
                    {
                        string[] stringFlag = new string[list.Count];

                        for (int i = 0; i < list.Count; i++)
                        {
                            stringFlag[i] = list[i];
                        }

                        CellRangeAddressList regions = new CellRangeAddressList(1, 65535, Convert.ToInt32(numFlag), Convert.ToInt32(numFlag));//约束范围：B1到B65535 

                        //CellRangeAddressList regions = new CellRangeAddressList(1, 65535, 0, 0);//约束范围：B1到B65535 

                        XSSFDataValidationHelper helper = new XSSFDataValidationHelper((XSSFSheet)sheet);//获得一个数据验证

                        IDataValidation validation = helper.CreateValidation(helper.CreateExplicitListConstraint(stringFlag), regions);//创建一个特定约束范围内的公式列表约束（即第一节里说的"自定义"方式）  

                        validation.CreateErrorBox("错误", "请按右侧下拉箭头选择!");//不符合约束时的提示  
                        validation.ShowErrorBox = true;//显示上面提示 = True  
                        sheet.AddValidationData(validation);//添加进去  
                        sheet.ForceFormulaRecalculation = true;

                        list.Clear();

                    }


                    if (!sdr["enum"].ToString().Equals(string.Empty))
                    {
                        list.Add(sdr["enum"].ToString());
                    }






                }
            }
            numFlag = sdr["num"].ToString();

        }

        sdr.Close();
        sdr.Dispose();

        if (list.Count != 0)
        {
            string[] stringFlag = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                stringFlag[i] = list[i];
            }

            CellRangeAddressList regions = new CellRangeAddressList(1, 65535, Convert.ToInt32(numFlag), Convert.ToInt32(numFlag));//约束范围：B1到B65535 

            //CellRangeAddressList regions = new CellRangeAddressList(1, 65535, 0, 0);//约束范围：B1到B65535 

            XSSFDataValidationHelper helper = new XSSFDataValidationHelper((XSSFSheet)sheet);//获得一个数据验证

            IDataValidation validation = helper.CreateValidation(helper.CreateExplicitListConstraint(stringFlag), regions);//创建一个特定约束范围内的公式列表约束（即第一节里说的"自定义"方式）  

            validation.CreateErrorBox("错误", "请按右侧下拉箭头选择!");//不符合约束时的提示  
            validation.ShowErrorBox = true;//显示上面提示 = True  
            sheet.AddValidationData(validation);//添加进去  
            sheet.ForceFormulaRecalculation = true;

            list.Clear();

        }

    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }
        }

        strUserFileName = filename.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }

        if (filename.PostedFile != null)
        {
            if (filename.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename.PostedFile.SaveAs(strFileName);//上传 文件
            }
            catch
            {
                ltlAlert.Text = "alert('上传文件失败.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {


                    DataTable errDt = null;
                    DataTable dt = null;
                    bool success = false;
                    try
                    {
                        dt = GetExcelContents(strFileName);
                    }
                    catch (Exception ex)
                    {
                        ltlAlert.Text = "alert('导入文件必须是Excel格式a');";

                    }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }
                    }

                    string message = "";
                    try
                    {
                        success = help.importPartListForTypeSkip(dt, ddlModle.SelectedValue.ToString(), out message, strUID, Session["uName"].ToString(), out errDt);//插入，
                    }
                    catch { }
                    finally
                    {
                        if (File.Exists(strFileName))
                        {
                            File.Delete(strFileName);
                        }

                    }
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "')";
                            Response.Redirect("Naprt_ApplyNewPart.aspx?modleID=" + ddlModle.SelectedValue.ToString());
                        }
                    }
                    else
                    {

                        //string title = "100^<b>QAD</b>~^100^<b>物料号</b>~^100^<b>供应商</b>~^160^<b>单位</b>~^100^<b>技术参考价</b>~^100^<b>需求规格</b>~^100^<b>备注</b>~^100^<b>错误信息</b>~^";
                        DataTable dtlist = help.getGvColByImportPartList(ddlModle.SelectedValue);

                        int total = dtlist.Rows.Count;
                        //string[] titleSub = title.Split(new char[] { '~' });
                        StringBuilder title = new StringBuilder();

                       

                        foreach (DataRow colName in dtlist.Rows)
                        {
                            
                            title.Append("100^<b>" + colName["colName"].ToString() + "</b>~^");
                        }
                        
                        title .Append("400^<b>错误信息</b>~^");
                        ltlAlert.Text = "alert('" + message + "')";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title.ToString(), errDt, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式a');";
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }
    }
}