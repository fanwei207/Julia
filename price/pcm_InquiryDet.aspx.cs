using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class price_pcm_InquiryDet : BasePage
{
    PCM_price pc = new PCM_price();

    /// <summary>
    /// 原则上0是查询页面
    /// 1是生成页面
    /// </summary>
    private int ComeFrom
    {
        get
        {
            if (ViewState["ComeFrom"] == null)
            {
                ViewState["ComeFrom"] = 0;
            }
            return Convert.ToInt32(ViewState["ComeFrom"]);
        }
        set
        {
            ViewState["ComeFrom"] = value;
        }
    }

    private int Statue
    {
        get
        {
            if (ViewState["Statue"] == null)
            {
                ViewState["Statue"] = -1;
            }
            return Convert.ToInt32(ViewState["Statue"]);
        }
        set
        {
            ViewState["Statue"] = value;
        }
    }




    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbIMID.Text= Request["IMID"].ToString();
            if (Request["ComeFrom"]!= null)
            {
                ComeFrom = Convert.ToInt32( Request["ComeFrom"].ToString());
            }
            bind();
        }

    }

    private void bind()
    {
        string vender = string.Empty;
        string verderName = string.Empty;
        int statue = -1;
        string curr = string.Empty;
        string create = string.Empty;
        string createDate = string.Empty;
        string venderPhone = string.Empty;
        string venderEmail = string.Empty;
        DataTable dt = pc.selectInquiryDet(lbIMID.Text, out vender, out verderName, out statue, out create, out createDate, out venderPhone ,out venderEmail);

        lbCreate.Text = create;
        lbCreateDate.Text = createDate;
        lbVender.Text = vender;
        lbVenderName.Text = verderName;
        lbVenderPhone.Text = venderPhone;
        lbVenderEmail.Text = venderEmail;
        string strstatue = "";
        if (statue == 0)//save核价
        {
            strstatue="未报价";
            btnSave.Visible = true;
        }
        else if(statue == 1)
        {
            strstatue="已报价";

         
            btnSave.Visible = true;
         
        }
        else if(statue == 2)
        {
            strstatue="已核价";

         
        }
         else if(statue == 3)
        {
            strstatue="完成";
            btnSave.Visible = false;
        }
        Statue = statue;
        lbStatus.Text = strstatue;

        //if ("RMB".Equals(curr))
        //{
        //    ddlcurr.SelectedValue = "1";
        //}
        //else if ("USD".Equals(curr))
        //{
        //    ddlcurr.SelectedValue = "2";
        //}
        // else if ("EUR".Equals(curr))
        //{
        //    ddlcurr.SelectedValue = "3";
        //}
        // else if ("HKD".Equals(curr))
        //{
        //    ddlcurr.SelectedValue = "4";
        //    //ddlcurr.SelectedItem.Value = "4";
        //}

        gvQuotationAndCalculation.DataSource = dt;
        gvQuotationAndCalculation.DataBind();
        
    }



    protected void btnBasis_Click(object sender, EventArgs e)
    {
        if (Request["vender"] != null)//新生成的询价单没有vender
        {
            //打开页面，显示所有凭据，点击详细可以查看，可以删除，但报价的在已报价的状态下不能删除，且删除的只是隐藏了
            Response.Redirect("pcm_ViewBasis.aspx?IMID=" + lbIMID.Text + "&status=" + Statue + "&ComeFrom=" + ComeFrom + "&vender=" + Request["vender"].ToString() + "&ItemCode="
                    + Request["ItemCode"].ToString() + "&QAD=" + Request["QAD"].ToString() + "&VenderName=" + Request["VenderName"].ToString() + "&chkSelf=" + Request["chkSelf"].ToString() + "&ddlStatus=" + Request["ddlStatus"].ToString() + "&txtIMID=" + Request["txtIMID"].ToString());
        }
        else 
        {
            //打开页面，显示所有凭据，点击详细可以查看，可以删除，但报价的在已报价的状态下不能删除，且删除的只是隐藏了
            Response.Redirect("pcm_ViewBasis.aspx?IMID=" + lbIMID.Text + "&status=" + Statue + "&ComeFrom=" + ComeFrom + "&vender=" +"" + "&ItemCode="
                    + ""+ "&QAD=" + "" + "&VenderName=" +"" + "&chkSelf=" + "" + "&ddlStatus=" + "" + "&txtIMID=" + "");
        }
    }

    private DataTable gvChangedt()
    {
        DataTable TempTable = new DataTable("gvTable");
        DataColumn TempColumn;
        DataRow TempRow;


        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "IMID";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.Int32");
        TempColumn.ColumnName = "applyDetID";
        TempTable.Columns.Add(TempColumn);

        //TempColumn = new DataColumn();
        //TempColumn.DataType = System.Type.GetType("System.Decimal");
        //TempColumn.ColumnName = "price";
        //TempTable.Columns.Add(TempColumn);

        //TempColumn = new DataColumn();
        //TempColumn.DataType = System.Type.GetType("System.Decimal");
        //TempColumn.ColumnName = "priceSelf";
        //TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "priceDiscount";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.Decimal");
        TempColumn.ColumnName = "checkPrice";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.Int32");
        TempColumn.ColumnName = "createdBy";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "createdDate";
        TempTable.Columns.Add(TempColumn);

        //TempColumn = new DataColumn();
        //TempColumn.DataType = System.Type.GetType("System.String");
        //TempColumn.ColumnName = "PriceBasis";
        //TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "FinCheckPriceBasis";
        TempTable.Columns.Add(TempColumn);


        foreach (GridViewRow gvr in gvQuotationAndCalculation.Rows)
        {
            //decimal price=-1;
            //decimal priceSelf=-1;
            decimal checkPrice=-1;
            TempRow = TempTable.NewRow();
            TempRow["IMID"] = lbIMID.Text;
            TempRow["applyDetID"] = gvQuotationAndCalculation.DataKeys[gvr.RowIndex].Values["applyDetID"];
            //if (decimal.TryParse(((TextBox)gvr.FindControl("price")).Text.ToString().Trim(), out price))
            //{
            //    TempRow["price"] = price;
            //}
            //else
            //{
            //    TempRow["price"] = DBNull.Value;
            //}
            //if (decimal.TryParse(((TextBox)gvr.FindControl("priceSelf")).Text.ToString().Trim(), out priceSelf))
            //{
            //    TempRow["priceSelf"] = priceSelf;
            //}
            //else
            //{
            //    TempRow["priceSelf"] = DBNull.Value;
            //}
            if (decimal.TryParse(((TextBox)gvr.FindControl("checkPrice")).Text.ToString().Trim(), out checkPrice))
            {
                TempRow["checkPrice"] = checkPrice;
            }
            else
            {
                TempRow["checkPrice"] = DBNull.Value;
            }
            TempRow["priceDiscount"] = ((TextBox)gvr.FindControl("priceDiscount")).Text.ToString().Trim();
            TempRow["createdBy"] = Convert.ToInt32( Session["uID"]);
            TempRow["createdDate"] = DateTime.Now.ToString("yyyy-MM-dd");
            //TempRow["PriceBasis"] = ((TextBox)gvr.FindControl("PriceBasis")).Text;
            TempRow["FinCheckPriceBasis"] = ((TextBox)gvr.FindControl("FinCheckPriceBasis")).Text;


            TempTable.Rows.Add(TempRow);
        }

        return TempTable;
        
    
    }



    /// <summary>
    /// 核价或保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int flag = pc.updateCheck(gvChangedt(), Convert.ToInt32(Session["uID"]),  lbIMID.Text);
        if (flag == 1)
        {
            ltlAlert.Text = "alert('保存成功，核价必须每条记录都有核价且有核价依据');";
            bind();
        }
        else if (flag == 2)
        {
            ltlAlert.Text = "alert('核价成功');";
            bind();
        }
        else
        {
            ltlAlert.Text = "alert('核价失败');";
        }
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        if (ComeFrom != 1)
        {
            Response.Redirect("pcm_InquiryList.aspx?IMID=" + Request["IMID"].ToString() + "&vender=" + Request["vender"].ToString() + "&ItemCode="
                + Request["ItemCode"].ToString() + "&QAD=" + Request["QAD"].ToString() + "&VenderName=" + Request["VenderName"].ToString() + "&chkSelf=" + Request["chkSelf"].ToString() + "&ddlStatus=" + Request["ddlStatus"].ToString() + "&txtIMID=" + Request["txtIMID"].ToString());
            //ltlAlert.Text = "window.close();";
        }
        else
        {
            Response.Redirect("pcm_MadeInquiryList.aspx");
        }
    }
    protected void gvQuotationAndCalculation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //gvQuotationAndCalculation.DataKeys[e.Row.RowIndex].Values


            if (Request["QAD"] != null)
            {
                if (gvQuotationAndCalculation.DataKeys[e.Row.RowIndex].Values["Part"].Equals(Request["QAD"].ToString()))
                {
                    e.Row.BackColor = System.Drawing.Color.LightSeaGreen;
                }
            }

            if (Statue == 0)
            {
                ((TextBox)e.Row.FindControl("checkPrice")).Enabled = true;
            }
            if (Statue == 3)
            {
                //((TextBox)e.Row.FindControl("price")).Enabled = false;
               // ((TextBox)e.Row.FindControl("priceSelf")).Enabled = false;
                ((TextBox)e.Row.FindControl("priceDiscount")).Enabled = true;
                ((TextBox)e.Row.FindControl("checkPrice")).Enabled = true;
            }
            if (Convert.ToInt32(((Label)e.Row.FindControl("lbstatus")).Text) >= 5)
            {
               // ((TextBox)e.Row.FindControl("price")).Enabled = false;
                //((TextBox)e.Row.FindControl("priceSelf")).Enabled = false;
                ((TextBox)e.Row.FindControl("priceDiscount")).Enabled = false;
                ((TextBox)e.Row.FindControl("checkPrice")).Enabled = false;
              //  ((TextBox)e.Row.FindControl("PriceBasis")).Enabled = false;
                ((TextBox)e.Row.FindControl("FinCheckPriceBasis")).Enabled = false;
            }

            string infoFrom = ((Label)e.Row.FindControl("lbInfoFrom")).Text;

            if ("1".Equals(infoFrom))
            {
                ((Label)e.Row.FindControl("lbInfoFrom")).Text="是";
                ((LinkButton)e.Row.FindControl("lkbtnCancel")).Text = "";
            }
            else
            {
                ((Label)e.Row.FindControl("lbInfoFrom")).Text = "";

                if ("False".Equals(((Label)e.Row.FindControl("lbIsCancel")).Text))
               {
                   if (this.Security["121000040"].isValid && Convert.ToInt32(((Label)e.Row.FindControl("lbstatus")).Text) <= 4)
                   {
                       ((LinkButton)e.Row.FindControl("lkbtnCancel")).Text = "取消";
                   }
               }
               else
               {
                   ((LinkButton)e.Row.FindControl("lkbtnCancel")).Text = "已取消";
                   ((LinkButton)e.Row.FindControl("lkbtnCancel")).Enabled = false;
                   ((LinkButton)e.Row.FindControl("lkbtnCancel")).Font.Underline = false;
                   //((TextBox)e.Row.FindControl("price")).Enabled = false;
                  // ((TextBox)e.Row.FindControl("priceSelf")).Enabled = false;
                   ((TextBox)e.Row.FindControl("priceDiscount")).Enabled = false;
                   ((TextBox)e.Row.FindControl("checkPrice")).Enabled = false;
                  // ((TextBox)e.Row.FindControl("PriceBasis")).Enabled = false;
                   ((TextBox)e.Row.FindControl("FinCheckPriceBasis")).Enabled = false;
               }
            }

            if (!((TextBox)e.Row.FindControl("checkPrice")).Text.ToString().Equals(e.Row.Cells[3].Text.ToString()))
            {
                e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
            }
            if (Convert.ToBoolean(gvQuotationAndCalculation.DataKeys[e.Row.RowIndex].Values["isout"]))
            {
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Orange;
            }
          
            
        
        }
    }
    protected void gvQuotationAndCalculation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnSelectQADDOC")
        {
            Response.Redirect("pcm_selectQadDoc.aspx?IMID=" + lbIMID.Text + "&QADDet=" + e.CommandArgument.ToString() + "&ComeFrom=" + ComeFrom + "&vender=" + Request["vender"].ToString() + "&ItemCode="
                + Request["ItemCode"].ToString() + "&QAD=" + Request["QAD"].ToString() + "&VenderName=" + Request["VenderName"].ToString() + "&chkSelf=" + Request["chkSelf"].ToString() + "&ddlStatus=" + Request["ddlStatus"].ToString() + "&txtIMID=" + Request["txtIMID"].ToString());
        }
        if (e.CommandName == "lkbtnCancel")
        {
            string applyDetId = e.CommandArgument.ToString();

            if (pc.cancelInquiryDet(applyDetId, Convert.ToInt32(Session["uID"])))
            {
                ltlAlert.Text = "alert('取消成功.');";
                bind();
            }
            else
            {

                ltlAlert.Text = "alert('取消失败.');";
            }
        }

    }
    protected void btnMakeInquiry_Click(object sender, EventArgs e)
    {
        string IMID = lbIMID.Text;//询价单号
        string company = string.Empty;//公司
        string createdDate = string.Empty;//日期
        string vendor = "(" + lbVender.Text + ")" + lbVenderName.Text;
        string createByName = string.Empty;//创建询价单人姓名
        string stroutFile = "pc_inquiry_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        pc.createinquiry(stroutFile, IMID, company, vendor, createdDate, createByName, Convert.ToInt32(Session["uID"]));
        ltlAlert.Text = "window.open('/Excel/" + stroutFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";

        //DataTable dt = pc.generationInquiry(IMID, out company, out createdDate, out createByName, Convert.ToInt32(Session["uID"]));

        //导出excel的方法
    }

    /// <summary>
    /// 上传价格
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImportExcelFile();
        bind();
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

            if (File.Exists(strFileName))
            {
                
                DataTable dt = null;
                
                try
                {
                    //NPOIHelper helper = new NPOIHelper();
                    dt = this.GetExcelContents(strFileName);
                    //dt = adam.getExcelContents(strFileName).Tables[0];
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式');";
                    return;
                    
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                try
                {
                    string message = "";
                    bool success = pc.uploadPrice(strFileName, strUID, lbIMID.Text, out message,dt );//插入，
                    if (success)
                    {
                        if (message != "")
                        {
                            ltlAlert.Text = "alert('" + message + "');";
                        }
                    }
                    else
                    {
                        DataTable errDt = pc.GetInquiryPriceImportError(strUID);//输出错误信息
                        string title = "100^<b>询价单号</b>~^100^<b>申请id</b>~^100^<b>QAD</b>~^100^<b>供应商</b>~^100^<b>期望价格</b>~^100^<b>折扣方式</b>~^100^<b>核价</b>~^100^<b>核价备注</b>~^100^<b>错误信息</b>~^";
                        ltlAlert.Text = "alert('" + message + "');";
                        if (errDt != null && errDt.Rows.Count > 0)
                        {
                            ExportExcel(title, errDt, false);
                        }
                    }

                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('导入文件必须是Excel格式'" + ex.Message.ToString() + "'.');";
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
    /// <summary>
    /// 下载模板将询价单模板下载下来，内容有QAD 供应商，申请价格， 空栏位有，折扣，核价价格，核价备注
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDownExcel_Click(object sender, EventArgs e)
    {
        string ExcelString = "100^<b>询价单号</b>~^100^<b>申请id</b>~^100^<b>QAD</b>~^100^<b>供应商</b>~^100^<b>期望价格</b>~^100^<b>折扣方式</b>~^"
                    + "100^<b>核价</b>~^100^<b>核价备注</b>~^";

        DataTable dt = pc.selectInquiryToExport(lbIMID.Text.ToString());

        if (dt != null && dt.Rows.Count > 0)
        {
            ExportExcel(ExcelString, dt, false);
        }
        else 
        {
            ltlAlert.Text = "alert('导出数据为空，请查询数据状态！');";
        }
    }
}