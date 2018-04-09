using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_PCF_InquiryDet : BasePage
{
    PCF_helper helper = new PCF_helper();
    private DataTable dataSouth
    {
        get
        {

            return (DataTable)ViewState["dataSouth"];
        }
        set
        {
            ViewState["dataSouth"] = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["PCF_inquiryID"] != null)
            {
                bindHead();
                bindGv();
            }
        }
    }

    private void bindGv()
    {
        string PCF_inquiryID = Request["PCF_inquiryID"].ToString();
        dataSouth = helper.selectInquiryDet(PCF_inquiryID);
        gvInfo.DataSource = dataSouth;
        gvInfo.DataBind();
    }

    private void bindHead()
    {
        string PCF_inquiryID = Request["PCF_inquiryID"].ToString();

        SqlDataReader sdr = helper.selectInqyuryHead(PCF_inquiryID);

        if (sdr.Read())
        {
            lbIMID.Text = sdr["PCF_IMID"].ToString();
            lbCreate.Text = sdr["createdName"].ToString();
            lbCreateDate.Text = sdr["createdDate"].ToString();
            lbStutas.Text = sdr["PCF_states"].ToString();
            //lbTelephone.Text = sdr["ad_phone"].ToString();
            lbVender.Text = sdr["PCF_vender"].ToString();
            lbVenderName.Text = sdr["PCF_venderName"].ToString();
        }
        sdr.Dispose();
        sdr.Close();

        if (lbStutas.Text == "已提交")
        {
            btnSave.Enabled = false;
        }

    }
    protected void btnUploadBasis_Click(object sender, EventArgs e)
    {
        Response.Redirect("PCF_uploadCheckPriceBasis.aspx?IMID=" + lbIMID.Text + "&PCF_inquiryID=" + Request.QueryString["PCF_inquiryID"] + "&TVender=" + Request.QueryString["TVender"] + "&TVenderName=" + Request.QueryString["TVenderName"] + "&TQAD=" + Request.QueryString["TQAD"] + "&ddlStatus=" + Request.QueryString["ddlStatus"]);
    }
    protected void btnInquiry_Click(object sender, EventArgs e)
    {
        string IMID = lbIMID.Text;//询价单号
        string company = string.Empty;//公司
        string createdDate = string.Empty;//日期
        string vendor = "(" + lbVender.Text + ")" + lbVenderName.Text;
        string createByName = string.Empty;//创建询价单人姓名
        string stroutFile = "pc_inquiry_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        helper.createinquiry(stroutFile, IMID, company, vendor, createdDate, createByName, Convert.ToInt32(Session["uID"]), "RMB");
        ltlAlert.Text = "window.open('/Excel/" + stroutFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int rowindex = e.Row.RowIndex;
            string status = gvInfo.DataKeys[rowindex].Values["PCF_states"].ToString();
            bool isDeductible = gvInfo.DataKeys[rowindex].Values["PCF_isDeductible"].ToString() == "True" ? true : false;
            ((CheckBox)e.Row.FindControl("chkDeductible")).Checked = isDeductible;
            if (lbVender.Text.Trim().ToUpper().Equals("S9999999"))
            {

                ((TextBox)e.Row.FindControl("txtTaxes")).Enabled = isDeductible;
            }
            else
            {
                ((CheckBox)e.Row.FindControl("chkDeductible")).Enabled = false;
                ((TextBox)e.Row.FindControl("txtTaxes")).Enabled = false;
            }




            if ("30".Equals(status) || "40".Equals(status))
            {
                ((TextBox)e.Row.FindControl("Price")).Enabled = false;
                ((TextBox)e.Row.FindControl("checkPrice")).Enabled = false;
                ((TextBox)e.Row.FindControl("FinCheckPriceBasis")).Enabled = false;
                ((LinkButton)e.Row.FindControl("lkbtnCancel")).Text = "";
                ((CheckBox)e.Row.FindControl("chkDeductible")).Enabled = false;
                ((TextBox)e.Row.FindControl("txtTaxes")).Enabled = false;
                
            }

        }
    }
    protected void gvInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnCancel")
        {
            string PCF_ID = e.CommandArgument.ToString();

            if (helper.CancelinquiryDetByID(PCF_ID, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                ltlAlert.Text = "alert('取消成功');";
                bindGv();
            }
            else
            {
                ltlAlert.Text = "alert('取消失败，请联系管理员');";
            }
        
        }
        if (e.CommandName == "lkbtnSelectApplyDOC")
        { 
            //连接到马博的地方
            string PCF_sourceID = e.CommandArgument.ToString();
            string id = helper.selectApplyMstrID(PCF_sourceID);

            Response.Redirect("../Purchase/rp_purchaseMstrDetial.aspx?ID=" + id + "&PCF_inquiryID=" + Request.QueryString["PCF_inquiryID"]  + "&TVender=" + Request.QueryString["TVender"] + "&TVenderName=" + Request.QueryString["TVenderName"] + "&TQAD=" + Request.QueryString["TQAD"] + "&ddlStatus=" + Request.QueryString["ddlStatus"]);
        }
    }
    protected void lkbtnDownList_Click(object sender, EventArgs e)
    {

        string PCF_inquiryID = Request.QueryString["PCF_inquiryID"];
        string title = "160^<b>QAD</b>~^100^<b>报价</b>~^100^<b>核价</b>~^160^<b>折扣表</b>~^160^<b>最小值</b>~^100^<b>核价备注</b>~^100^<b>是否可抵扣</b>~^100^<b>税率</b>~^200^<b>验证ID(请勿修改)</b>~^";

        //string[] titleSub = title.Split(new char[] { '~' });

        //DataTable dtExcel = new DataTable("temp");
        //DataColumn col;
        DataTable dtExcel = helper.selectInquiryDetTemp(PCF_inquiryID);
        //foreach (string colName in titleSub)
        //{
        //    col = new DataColumn();
        //    col.DataType = System.Type.GetType("System.String");
        //    col.ColumnName = colName;
        //    dtExcel.Columns.Add(col);
        //}

        ExportExcel(title, dtExcel, false);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("PCF_InquiryList.aspx?TVender=" + Request.QueryString["TVender"] + "&TVenderName=" + Request.QueryString["TVenderName"] + "&TQAD=" + Request.QueryString["TQAD"] + "&ddlStatus=" + Request.QueryString["ddlStatus"]);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 1，状态为已提交的部分
    /// 
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int southRowsCount = dataSouth.Rows.Count;
        int gvRowsCount = gvInfo.Rows.Count;
        #region datatable初始化
        DataTable TempTable = new DataTable("gvTable");
        DataColumn TempColumn;
        DataRow TempRow;

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_ID";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_checkPrice";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_amt";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_min";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_checkPriceBasis";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_price";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_isDeductible";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PCF_taxes";
        TempTable.Columns.Add(TempColumn);

       
        #endregion
        //判断上传的数据和源数据数据量是否相等
        //相等的时候进行 不相等的时候提示刷新
        if (southRowsCount == gvRowsCount)
        {
            foreach (DataRow dr in dataSouth.Rows)
            {
                foreach (GridViewRow gvr in gvInfo.Rows)
                {

                    string gvPCF_ID = gvInfo.DataKeys[gvr.RowIndex].Values["PCF_ID"].ToString();

                    string dtPCF_ID = dr["PCF_ID"].ToString();

                    string dtStutas =  dr["PCF_states"].ToString();

                    //如果两个id相同，查看下面数据是否完全相等，存在不相等的将数据成为datatable
                    if (gvPCF_ID.Equals(dtPCF_ID) && dtStutas != "30")
                    {
                        bool change = false;

                        string gvPriceStr = ((TextBox)gvr.FindControl("Price")).Text.Trim();
                        string gvQAD = gvInfo.DataKeys[gvr.RowIndex].Values["PCF_part"].ToString();
                        string gvCheckPriceStr = ((TextBox)gvr.FindControl("checkPrice")).Text.Trim();
                        string gvAmt = ((Label)gvr.FindControl("lbAmt")).Text.Trim();
                        string gvMin = ((Label)gvr.FindControl("lbMin")).Text.Trim();
                        string gvCheckPriceBasis = ((TextBox)gvr.FindControl("FinCheckPriceBasis")).Text.Trim();
                        string gvDeductible = ((CheckBox)gvr.FindControl("chkDeductible")).Checked ? "1" : "0";
                        string gvTaxesStr = ((TextBox)gvr.FindControl("txtTaxes")).Text.Trim();

                        decimal gvCheckPrice = 0;
                        decimal gvPrice = 0;
                        decimal gvTaxes = 0;

                        if (!decimal.TryParse(gvCheckPriceStr, out gvCheckPrice) && !gvCheckPriceStr.Equals(string.Empty))
                        {
                            ltlAlert.Text = "alert('"+gvQAD+" 核价价格不是数字请重新填写 ');";
                            return;
                        }

                        if (!decimal.TryParse(gvPriceStr, out gvPrice) && !gvPriceStr.Equals(string.Empty))
                        {
                            ltlAlert.Text = "alert('" + gvQAD + " 报价价格不是数字请重新填写 ');";
                            return;
                        }

                        if (!decimal.TryParse(gvTaxesStr, out gvTaxes) )
                        {
                            //if (gvDeductible.Equals("1"))
                            //{
                                ltlAlert.Text = "alert('" + gvQAD + " 不是抵扣率数字请重新填写 ');";
                                return;
                            //}
                            
                        }
                        else 
                        {
                            if (gvDeductible.Equals("1"))
                            {
                                if (gvTaxes > 100 || gvTaxes < 0)
                                {
                                    ltlAlert.Text = "alert('" + gvQAD + " 抵扣率必须在0-99之间 ');";
                                    return;
                                }
                            }
                            
                        }
                        


                        string dtPriceStr = dr["PCF_price"].ToString();
                        string dtCheckPriceStr = dr["PCF_checkPrice"].ToString();
                        string dtAmt = dr["PCF_amt"].ToString();
                        string dtMin = dr["PCF_min"].ToString();
                        string dtCheckPriceBasis = dr["PCF_checkPriceBasis"].ToString();
                        string dtDeductible = dr["PCF_isDeductible"].ToString()=="True" ? "1":"0";
                        string dtTaxesStr = dr["PCF_taxes"].ToString();



                        decimal dtCheckPrice = 0;
                        decimal dtPrice = 0;
                        decimal dtTaxes = 0;

                        //if (gvDeductible.Equals("1"))
                        //{
                            if (gvTaxesStr.Equals(string.Empty) && !dtTaxesStr.Equals(string.Empty))
                            {
                                change = true;
                            }
                            else
                            {

                                if (decimal.TryParse(dtTaxesStr, out dtTaxes))
                                {
                                    if (dtTaxesStr != gvTaxesStr)
                                    {
                                        change = true;
                                    }
                                }
                                else
                                {
                                    if (!gvTaxesStr.Equals(dtTaxesStr))
                                    {
                                        change = true;
                                    }
                                }
                            }
                        //}
                        

                        if (gvPriceStr.Equals(string.Empty) && !dtPriceStr.Equals(string.Empty))
                        {
                            change = true;
                        }
                        else
                        {

                            if (decimal.TryParse(dtPriceStr, out dtPrice))
                            {
                                if (dtPrice != gvPrice)
                                {
                                    change = true;
                                }
                            }
                            else
                            {
                                if (!gvPriceStr.Equals(dtPriceStr))
                                {
                                    change = true;
                                }
                            }
                        }

                        if (gvCheckPriceStr.Equals(string.Empty) && !dtCheckPriceStr.Equals(string.Empty))
                        {
                            change = true;
                        }
                        else
                        {

                            if (decimal.TryParse(dtCheckPriceStr, out dtCheckPrice))
                            {
                                if (dtCheckPrice != gvCheckPrice)
                                {
                                    change = true;
                                }
                            }
                            else
                            {
                                if (!gvCheckPriceStr.Equals(dtCheckPriceStr))
                                {
                                    change = true;
                                }
                            }
                        }

                        if (!gvAmt.Equals(dtAmt))
                        {
                            change = true;
                        }

                        if (!gvMin.Equals(dtMin))
                        {
                            change = true;
                        }

                        if (!dtDeductible.Equals(gvDeductible))
                        {
                            change = true;
                        }

                        if (!gvCheckPriceBasis.Equals(dtCheckPriceBasis))
                        {
                            change = true;
                        }


                      

                       if (change)
                       {
                           TempRow = TempTable.NewRow();
                           TempRow["PCF_ID"] = gvPCF_ID;
                           TempRow["PCF_checkPrice"] = gvCheckPriceStr;
                           TempRow["PCF_amt"] = gvAmt;
                           TempRow["PCF_min"] = gvMin;
                           TempRow["PCF_checkPriceBasis"] = gvCheckPriceBasis;
                           TempRow["PCF_price"] = gvPrice;
                           TempRow["PCF_isDeductible"] = gvDeductible;
                           TempRow["PCF_taxes"] = gvTaxesStr;
                           TempTable.Rows.Add(TempRow);
                       }
                       else
                       {
                           continue;
                       }


                    }


                }
                
            }

            //如果没有任何修改则不相数据库提出修改
            if (TempTable.Rows.Count >= 1)
            {
                int flag = helper.updateInquiryDet(TempTable, Request["PCF_inquiryID"], Session["uID"].ToString(), Session["uName"].ToString());

                if (flag == 1)
                {
                    ltlAlert.Text = "alert('核价成功');";
                    bindHead();
                    bindGv();
                }
                else if(flag == 2)
                {
                    ltlAlert.Text = "alert('保存成功,但必须要所有物料均有价格以及有上传依据，询价单才可询价成功');";
                    bindHead();
                    bindGv();
                }
                else if (flag == -1)
                {
                    ltlAlert.Text = "alert('保存失败，请联系管理员！');";
                }


                

            }
            else
            {
                ltlAlert.Text = "alert('您提交的页面没有修改项');";
            }

        }
        else
        {
            ltlAlert.Text = "alert('保存的数据量与原数据不一致，请刷新页面再尝试。');";
        }
       
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImportExcelFile();
    }

    public void ImportExcelFile()
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
            DataTable dt = null;
            if (File.Exists(strFileName))
            {
                try
                {
                     dt = GetExcelContents(strFileName);
                    
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式" + ex.Message.ToString() + ".')";
                    return;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
               
                if (
                    dt.Columns[0].ColumnName != "QAD" ||
                    dt.Columns[1].ColumnName != "报价" ||
                    dt.Columns[2].ColumnName != "核价" ||
                    dt.Columns[3].ColumnName != "折扣表" ||
                    dt.Columns[4].ColumnName != "最小值" ||
                    dt.Columns[5].ColumnName != "核价备注" ||
                    dt.Columns[6].ColumnName != "是否可抵扣" ||
                     dt.Columns[7].ColumnName != "税率" ||
                     dt.Columns[8].ColumnName != "验证ID(请勿修改)"
                    )
                {
                    dt.Reset();
                    ltlAlert.Text = "alert('导入文件的模版不正确，请更新模板再导入!');";
                    return;
                }
                #region datatable初始化
                DataTable TempTable = new DataTable("gvTable");
                DataColumn TempColumn;
                DataRow TempRow;

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PCF_ID";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PCF_checkPrice";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PCF_amt";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PCF_min";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PCF_checkPriceBasis";
                TempTable.Columns.Add(TempColumn);

                 TempColumn = new DataColumn();
                 TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PCF_part";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PCF_price";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PCF_isDeductible";
                TempTable.Columns.Add(TempColumn);

                TempColumn = new DataColumn();
                TempColumn.DataType = System.Type.GetType("System.String");
                TempColumn.ColumnName = "PCF_taxes";
                TempTable.Columns.Add(TempColumn);

                #endregion
                int southRowsCount = dataSouth.Rows.Count;
                int dtRowsCount = dt.Rows.Count;


                for (int j = 0; j < dtRowsCount; j++)
                {
                    if (dt.Rows[j]["QAD"].Equals(string.Empty) || dt.Rows[j]["验证ID(请勿修改)"].Equals(string.Empty))
                    {
                        dt.Rows[j].Delete();
                    }
                }
                dtRowsCount = dt.Rows.Count;

                if (dtRowsCount <= southRowsCount)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        foreach (DataRow drs in dataSouth.Rows)
                        {
                            string exPCF_ID = dr["验证ID(请勿修改)"].ToString().Trim();

                            string dtPCF_ID = drs["PCF_ID"].ToString();

                            string dtStutas = drs["PCF_states"].ToString();

                            //如果两个id相同，查看下面数据是否完全相等，存在不相等的将数据成为datatable
                            if (exPCF_ID.Equals(dtPCF_ID) && dtStutas != "30")
                            {
                                bool change = false;

                                string gvPriceStr = dr["报价"].ToString();
                                string gvQAD = dr["QAD"].ToString();
                                string gvCheckPriceStr = dr["核价"].ToString().Trim();
                                string gvAmt = dr["折扣表"].ToString().Trim();
                                string gvMin = dr["最小值"].ToString().Trim();
                                string gvCheckPriceBasis = dr["核价备注"].ToString().Trim();
                                string gvDeductible = dr["是否可抵扣"].ToString() == "是" ? "1" : "0";
                                string gvTaxesStr = dr["税率"].ToString();

                                decimal gvCheckPrice = 0;
                                decimal gvPrice = 0;
                                decimal gvTaxes = 0;

                                if (!decimal.TryParse(gvCheckPriceStr, out gvCheckPrice) )
                                {
                                   
                                }
                                if (!decimal.TryParse(gvPriceStr, out gvPrice))
                                {

                                }
                                if (!decimal.TryParse(gvTaxesStr, out gvTaxes))
                                {

                                }

                                string dtPriceStr = drs["PCF_price"].ToString();
                                string dtCheckPriceStr = drs["PCF_checkPrice"].ToString();
                                string dtAmt = drs["PCF_amt"].ToString();
                                string dtMin = drs["PCF_min"].ToString();
                                string dtCheckPriceBasis = drs["PCF_checkPriceBasis"].ToString();
                                string dtDeductible = drs["PCF_isDeductible"].ToString() == "True" ? "1" : "0";
                                string dtTaxesStr = drs["PCF_taxes"].ToString();
                                decimal dtCheckPrice = 0;
                                decimal dtPrice = 0;
                                decimal dtTaxes = 0;


                                //if (gvDeductible.Equals("1"))
                                //{
                                    if (gvTaxesStr.Equals(string.Empty) && !dtTaxesStr.Equals(string.Empty))
                                    {
                                        change = true;
                                    }
                                    else
                                    {

                                        if (decimal.TryParse(dtTaxesStr, out dtTaxes))
                                        {
                                            if (dtTaxesStr != gvTaxesStr)
                                            {
                                                change = true;
                                            }
                                        }
                                        else
                                        {
                                            if (!gvTaxesStr.Equals(dtTaxesStr))
                                            {
                                                change = true;
                                            }
                                        }
                                    }
                                //}




                                if (gvPriceStr.Equals(string.Empty) && !dtPriceStr.Equals(string.Empty))
                                {
                                    change = true;
                                }
                                else
                                {

                                    if (decimal.TryParse(dtPriceStr, out dtPrice))
                                    {
                                        if (dtPrice != gvPrice)
                                        {
                                            change = true;
                                        }
                                    }
                                    else
                                    {
                                        if (!gvPriceStr.Equals(dtPriceStr))
                                        {
                                            change = true;
                                        }
                                    }
                                }



                                if (gvCheckPriceStr.Equals(string.Empty) && !dtCheckPriceStr.Equals(string.Empty))
                                {
                                    change = true;
                                }
                                else
                                {
                                    if (decimal.TryParse(dtCheckPriceStr, out dtCheckPrice))
                                    {
                                        if (dtCheckPrice != gvCheckPrice)
                                        {
                                            change = true;
                                        }
                                    }
                                    else
                                    {
                                        if (!gvCheckPriceStr.Equals(dtCheckPriceStr))
                                        {
                                            change = true;
                                        }
                                    }
                                }
                                


                                if (!gvAmt.Equals(dtAmt))
                                {
                                    change = true;
                                }

                                if (!gvMin.Equals(dtMin))
                                {
                                    change = true;
                                }


                                if (!gvCheckPriceBasis.Equals(dtCheckPriceBasis))
                                {
                                    change = true;
                                }


                                if (change)
                                {
                                    TempRow = TempTable.NewRow();
                                    TempRow["PCF_ID"] = exPCF_ID;
                                    TempRow["PCF_checkPrice"] = gvCheckPriceStr;
                                    TempRow["PCF_amt"] = gvAmt;
                                    TempRow["PCF_min"] = gvMin;
                                    TempRow["PCF_checkPriceBasis"] = gvCheckPriceBasis;
                                    TempRow["PCF_part"] = gvQAD;
                                    TempRow["PCF_price"] = gvPrice;
                                    TempRow["PCF_isDeductible"] = gvDeductible;
                                    TempRow["PCF_taxes"] = gvTaxesStr;
                                    TempTable.Rows.Add(TempRow);
                                }
                                else
                                {
                                    continue;
                                }


                            }

                        }

                    }

                    //插入数据库

                    DataTable returnDataTable = helper.updateInquiryDetByExcel(TempTable, Request["PCF_inquiryID"], Session["uID"].ToString(), Session["uName"].ToString());

                    if(returnDataTable.Rows[0][0].ToString() =="1")
                    {
                        ltlAlert.Text = "alert('核价成功');";
                        bindHead();
                        bindGv();
                    
                    }
                    else if(returnDataTable.Rows[0][0].ToString() =="2")
                    {
                        ltlAlert.Text = "alert('保存成功,但必须要所有物料均有价格以及有上传依据，询价单才可询价成功');";
                        bindHead();
                        bindGv();
                    }
                    else if(returnDataTable.Rows[0][0].ToString() =="-2")
                    {
                        ltlAlert.Text = "alert('保存失败,请联系管理员');";
                    }
                    else
                    {
                        string title = "100^<b>QAD</b>~^100^<b>报价</b>~^100^<b>核价</b>~^160^<b>折扣表</b>~^160^<b>最小值</b>~^100^<b>核价备注</b>~^100^<b>是否可抵扣</b>~^100^<b>税率</b>~^100^<b>验证ID(请勿修改)</b>~^100^<b>错误信息</b>~^";
                        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
                        {
                            ExportExcel(title, returnDataTable, false);
                        }
                    }
              


                }
            }

        }
    }

    
}