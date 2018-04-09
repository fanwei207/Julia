using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class product_ProductStruApply_New : BasePage
{
    private ProductStru bll = new ProductStru();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("45301011", "BOM新增申请");
            this.Security.Register("45301012", "BOM组新增确认");
        }

        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                lbl_id.Text = Request.QueryString["id"];
                BindMstrData();
            }
            else
            {
                txt_status.Text = "新增";
                this.trReview.Visible = false;
                this.trApply.Visible = true;
                this.txtReason.ReadOnly = true;
                btn_Save.Visible = true;
                btn_Submit.Visible = false;
                btn_Cancel.Visible = false;
                trReason.Visible = false;
                trUpload.Visible = true;
                trUpload1.Visible = true;
                this.txt_prodCode.ReadOnly = false;
                this.txtRmks.ReadOnly = false;
            }

            if (lblProdId.Text == "")
            {
                linkProd.Visible = false;
            }
            else
            {
                linkProd.Visible = true;
            }
  
        }
    }
    private void BindMstrData()
    {
        IDataReader reader = bll.GetProductStruApplyMstr(lbl_id.Text);
        if (reader.Read())
        {
            txt_nbr.Text = reader["Code"].ToString();
            txt_status.Text = reader["Status"].ToString();
            txt_prodCode.Text = reader["ProdCode"].ToString();
            txtReason.Text = reader["Reason"].ToString();
            txtRmks.Text = reader["Desc"].ToString();
            lbl_Status.Text = reader["StatusValue"].ToString();
            hid_CreatedBy.Value = reader["CreatedBy"].ToString();
            lblProdId.Text = reader["rdw_mstrid"].ToString();
            txt_projectCategory.Text = reader["cate_name"].ToString();
            txt_project.Text = reader["RDW_Project"].ToString();
            txt_prodDesc.Text = reader["RDW_ProdDesc"].ToString();
            BindDetailData();
            BindProductData();
        }
        reader.Close();
        if (this.Security["45301012"].isValid && lbl_Status.Text == "20" )
        {
            this.trReview.Visible = true;
            this.trReason.Visible = true;
            this.txtReason.ReadOnly = false;
            this.trApply.Visible = false;
            this.trUpload.Visible = false;
            this.trUpload1.Visible = false;
            this.txt_prodCode.ReadOnly = true;
            this.txtRmks.ReadOnly = true;
        }
        else
        {
            this.trReview.Visible = false;
            this.trApply.Visible = true;
            this.txtReason.ReadOnly = true;
            if (this.Security["45301011"].isValid )
            {
                if (lbl_Status.Text == "0" && Session["uID"].ToString() == hid_CreatedBy.Value)
                {
                    btn_Save.Visible = true;
                    btn_Submit.Visible = true;
                    btn_Cancel.Visible = true;
                    trReason.Visible = false;
                    trUpload.Visible = true;
                    trUpload1.Visible = true;
                    this.txt_prodCode.ReadOnly = false;
                    this.txtRmks.ReadOnly = false;
                }
                else if (lbl_Status.Text == "-20" && Session["uID"].ToString() == hid_CreatedBy.Value)
                {
                    btn_Save.Visible = true;
                    btn_Submit.Visible = true;
                    btn_Cancel.Visible = true;
                    btn_Back.Visible = true;
                    trReason.Visible = true;
                    trUpload.Visible = true;
                    trUpload1.Visible = true;
                    this.txt_prodCode.ReadOnly = false;
                    this.txtRmks.ReadOnly = false;
                }
                else
                {
                    btn_Save.Visible = false;
                    btn_Submit.Visible = false;
                    btn_Cancel.Visible = false;
                    btn_Back.Visible = true;
                    trReason.Visible = true;
                    trUpload.Visible = false;
                    trUpload1.Visible = false;
                    this.txt_prodCode.ReadOnly = true;
                    this.txtRmks.ReadOnly = true;
                    if (lbl_Status.Text == "10")
                    {
                        this.trReview.Visible = true;
                        btn_Back.Visible = false;
                        btn_Confirm.Visible = false;
                        txtReason.ReadOnly = false;
                    }
                }
            }
            else
            {
                btn_Save.Visible = false;
                btn_Submit.Visible = false;
                btn_Cancel.Visible = false;
                trReason.Visible = true;
                trUpload.Visible = false;
                trUpload1.Visible = false;
                this.txt_prodCode.ReadOnly = true;
                this.txtRmks.ReadOnly = true;
            }
        }
    }
    private void BindDetailData()
    {
        gv_det.DataSource = bll.GetProductStruApplyDetail(lbl_id.Text);
        gv_det.DataBind();
    }

    private void BindProductData()
    {
        gv_product.DataSource = bll.GetProductStruApplyNewProduct(lbl_id.Text);
        gv_product.DataBind();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (txt_prodCode.Text.Trim() != "" && !bll.ProjectExists(txt_prodCode.Text.Trim()))
        {
            this.Alert("请输入正确项目号！");
            return;
        }
        if (lbl_id.Text == "")
        {
            string id = bll.AddProductStruApplyMstr(txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            if (id != "0")
            {
                lbl_id.Text = id;
                BindMstrData();
            }
            else
            {
                this.Alert("申请保存失败");
                return;
            }
        }
        else
        {
            bll.UpdateProductStruApplyMstr(lbl_id.Text,txt_prodCode.Text.Trim(), txtRmks.Text.Trim(),txtReason.Text.Trim(),lbl_Status.Text, Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
        }
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        if (lbl_id.Text == "")
        {
            if (txt_prodCode.Text.Trim() != "" && !bll.ProjectExists(txt_prodCode.Text.Trim()))
            {
                this.Alert("请输入正确项目号！");
                return;
            }
            string id = bll.AddProductStruApplyMstr(txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            if (id != "0")
            {
                lbl_id.Text = id;
            }
            else
            {
                this.Alert("申请保存失败！");
                return;
            }
        }
        bll.DeleteProductStruApplyDetail(lbl_id.Text);
        bll.DeleteProductStruApply_NewProduct(lbl_id.Text);
        ImportExcelFile();
        BindMstrData();
        this.Alert("导入成功！");
        btn_Submit.Enabled = true;
    }

    private void ImportExcelFile()
    {
        DataTable dt;
        bool bBomError;
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;
        //Boolean boolError = false;

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

        strUserFileName = filename1.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }
        strUserFileName = strFileName;

        //Modified By Shanzm 2012-12-27：唯一字符串可以设定为“年月日时分秒毫秒”
        string strKey = string.Format("{0:yyyyMMddhhmmssfff}", DateTime.Now);
        strFileName = strCatFolder + "\\" + strKey + strUserFileName;

        if (filename1.PostedFile != null)
        {
            if (filename1.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }

            try
            {
                filename1.PostedFile.SaveAs(strFileName);
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
                    dt = this.GetExcelContents(strFileName);
                    //ds = chk.getExcelContents(strFileName);
                }
                catch (Exception e)
                {

                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }

                    ltlAlert.Text = "alert('导入文件必须是Excel格式!');";
                    return;
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    string _new, _parent, _parentDesc, _child, _qty, _note, _pos, store, _replace, itemType, itemNum, itemstr, itemNums, qad, prodQad;
                    int _parentID, _childID, prodID, partID, semiProdID, ind;
                    Tuple<int, string> prodResult, semiProdResult, partResult;
                    store = "";
                    prodQad = "";
                    _parentID = 0;
                    StringBuilder newCode =new StringBuilder(";");
                    if (dt.Columns[0].ColumnName != "是否新产品(Y/N)" || dt.Columns[1].ColumnName != "父级类型" || dt.Columns[2].ColumnName != "父级部件号" || dt.Columns[3].ColumnName != "产品描述" || dt.Columns[4].ColumnName != "子级部件号")
                    {
                        dt.Reset();
                        this.Alert("导入文件不是产品结构申请导入模版");
                        return;
                    }
                    DataRow[] newProduct = dt.Select("[是否新产品(Y/N)]='y' or [是否新产品(Y/N)]='Y'");
                    DataTable newProducts = null;
                    if (newProduct.Length > 0)
                    {
                        newProducts = dt.Select("[是否新产品(Y/N)]='y' or [是否新产品(Y/N)]='Y'").CopyToDataTable();
                    }
                    else
                    {
                        newProducts = dt.Clone();
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        bBomError = false;
                        itemstr = "";
                        itemNums="";
                        ind = 0;
                        //_parentID = 0;
                        _childID = 0;
                        itemType = "PROD";
                        qad = "";
                        DataRow row = dt.Rows[i];
                        _new = row[0].ToString().Trim().ToUpper();
                        _parent = row[2].ToString().Trim();
                        _parentDesc = row[3].ToString().Trim();
                        _child = row[4].ToString().Trim();
                        _pos = row[5].ToString().Trim();
                        _qty = row[7].ToString().Trim();
                        _note = row[8].ToString().Trim();
                        _replace = row[9].ToString().Trim();
                        if (_new == "Y")
                        {
                            if (_parent == "")
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，新产品的父级部件号不能为空！");
                                dt.Reset();
                                return;
                            }
                            else if (newCode.ToString().Contains(";" + _parent + ";"))
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，新产品的父级部件号重复！");
                                dt.Reset();
                                return;
                            }
                            if (_parentDesc == "")
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，新产品的产品描述不能为空！");
                                dt.Reset();
                                return;
                            }
                            newCode.Append(_parent).Append(";");

                        }
                        if (_parent != "")
                        {
                            store = _parent;
                            bBomError = false;
                            _parentID = 0;
                            prodQad = "";
                            if (newProducts.Select("父级部件号='" + _parent + "'").Count() == 0 || bll.NewProductExists(lbl_id.Text, ref _parent))
                            {                          
                                prodResult = bll.FindProdID(_parent);
                                _parentID = prodResult.Item1;
                                prodQad = prodResult.Item2;
                                if (_parentID <= 0)
                                {
                                    semiProdResult = bll.FindSemiProdID(_parent);
                                    _parentID = semiProdResult.Item1;
                                    prodQad = semiProdResult.Item2;
                                    if (_parentID <= 0)
                                    {
                                        bBomError = true;
                                        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "父级部件号不存在！");
                                        dt.Reset();
                                        return;
                                    }
                                }
                                if (_parent.ToUpper().EndsWith("-BZ") && prodQad == "")
                                {
                                    foreach (string parQad in bll.GetParentQad(_parentID.ToString()))
                                    {
                                        if (bll.IsLocked(parQad))
                                        {
                                            this.Alert("父级部件号 " + _parent + "已被锁定！");
                                            dt.Reset();
                                            return;
                                        }
                                    }
                                }
                                if (bll.IsLocked(prodQad))
                                {
                                    this.Alert("父级部件号 " + _parent + "已被锁定！");
                                    dt.Reset();
                                    return;
                                }
                            }

                        }
                        else
                        {
                            _parent = store;
                        }
                        if (_child == "")
                        {
                            continue;
                            this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，子级部件号不能为空！");
                            dt.Reset();
                            return;
                        }
                        if (_qty == "")
                        {
                            this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能为空！");
                            dt.Reset();
                            return;
                        }
                        else
                        {
                            decimal dQty = 0;
                            try
                            {
                                dQty = decimal.Parse(_qty, System.Globalization.NumberStyles.Float);
                            }
                            catch
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不是数值！");
                                dt.Reset();
                                return;
                            }
                            if (dQty <= 0)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能小于零！");
                                dt.Reset();
                                return;
                            }
                            _qty = dQty.ToString();
                        }

                        if (_pos.Length > 150)
                        {
                            _pos = _pos.Substring(0, 150).Trim();
                        }
                        if (_note.Length > 255)
                        {
                            _note = _note.Substring(0, 255).Trim();
                        }

                        while (_replace.Length > 0)
                        {
                            if (_replace.IndexOf(",") != -1 || _replace.IndexOf("，") != -1)
                            {
                                if (_replace.IndexOf(",") != -1)
                                    ind = _replace.IndexOf(",");
                                else if (_replace.IndexOf("，") != -1)
                                    ind = _replace.IndexOf("，");

                                itemNum = _replace.Substring(0, ind).Trim();
                                _replace = _replace.Substring(ind + 1).Trim();
                            }
                            else
                            {
                                itemNum = _replace;
                                _replace = "";
                            }
                            itemNums+=itemNum+",";
                            if (newProducts.Select("父级部件号='" + itemNum + "'").Count() == 0 || bll.NewProductExists(lbl_id.Text, ref itemNum))
                            {
                                prodID = bll.FindProdID(itemNum).Item1;
                                partID = bll.FindPartID(itemNum).Item1;
                                semiProdID = bll.FindSemiProdID(itemNum).Item1;
                                if (prodID > 0 && (partID > 0 || semiProdID > 0))
                                {
                                    bBomError = true;
                                    this.Alert(itemNum + "在产品库和部件库中均存在！");
                                    dt.Reset();
                                    return;
                                }

                                if (prodID == 0 && partID == 0 && semiProdID == 0)
                                {
                                    bBomError = true;
                                    this.Alert(itemNum + "在库中不存在！");
                                    dt.Reset();
                                    return;
                                }
                                if (prodID > 0)
                                    itemstr += prodID + ",";
                                else if (partID > 0)
                                    itemstr += partID + ",";
                                else if (semiProdID > 0)
                                    itemstr += semiProdID + ",";
                            }
                        }

                        if (itemstr.Length > 0)
                        {
                            itemstr = itemstr.Trim().Substring(0, itemstr.Length - 1);
                        }

                        if (itemNums.Length > 0)
                        {
                            itemNums = itemNums.Trim().Substring(0, itemNums.Length - 1);
                        }



                        if (!bBomError)
                        {
                            _childID = 0;
                            if (newProducts.Select("父级部件号='" + _child + "'").Count() == 0 || bll.NewProductExists(lbl_id.Text, ref _child))
                            {
                                prodResult = bll.FindProdID(_child);
                                prodID = prodResult.Item1;
                                partResult = bll.FindPartID(_child);
                                partID = partResult.Item1;
                                semiProdResult = bll.FindSemiProdID(_child);
                                semiProdID = semiProdResult.Item1;

                                if (prodID > 0 && (partID > 0 || semiProdID > 0))
                                {
                                    bBomError = true;
                                    this.Alert(_child + "在产品库和部件库中均存在！");
                                    dt.Reset();
                                    return;
                                }

                                if (prodID == 0 && partID == 0 && semiProdID == 0)
                                {
                                    bBomError = true;
                                    this.Alert(_child + "在库中不存在！");
                                    dt.Reset();
                                    return;
                                }
                                if (_parentID > 0)
                                {

                                    if (prodID > 0)
                                    {

                                        if (bll.BomItemExist(_parent, prodID, "PROD", _pos))
                                        {
                                            bBomError = true;
                                            ltlAlert.Text = "alert('" + _child + "在产品结构中已经存在！');";
                                            dt.Reset();
                                            return;
                                        }
                                    }
                                    else if (semiProdID > 0)
                                    {

                                        if (bll.BomItemExist(_parent, semiProdID, "PROD", _pos))
                                        {
                                            bBomError = true;
                                            ltlAlert.Text = "alert('" + _child + "在产品结构中已经存在！');";
                                            dt.Reset();
                                            return;
                                        }
                                    }
                                    else
                                    {

                                        if (bll.BomItemExist(_parent, partID, "PART", _pos))
                                        {
                                            bBomError = true;
                                            ltlAlert.Text = "alert('" + _child + "在产品结构中已经存在！');";
                                            dt.Reset();
                                            return;
                                        }
                                    }
                                }

                                if (prodID > 0)
                                {
                                    itemType = "PROD";
                                    _childID = prodID;
                                    qad = prodResult.Item2;
                                }
                                else if (semiProdID > 0)
                                {
                                    itemType = "PROD";
                                    _childID = semiProdID;
                                    qad = semiProdResult.Item2;
                                }
                                else
                                {
                                    itemType = "PART";
                                    _childID = partID;
                                    qad = partResult.Item2;
                                }

                            }
                        }

                        if (!bBomError)
                        {
                            bll.AddProductStruApplyDetail(lbl_id.Text, _parent, _child, itemNums, _parentID, prodQad,_childID, qad, _qty, itemType, _note, _pos, itemstr, Session["plantCode"].ToString());
                        }
                    }

                    foreach (DataRow row in newProducts.Rows)
                    {
                        string type = row[1].ToString();
                        string no = row[2].ToString();
                        string desc = row[3].ToString();
                        string source = "0";
                        switch (type)
                        {
                            case "整灯": source = "1";
                                break;
                            case "包材": source = "2";
                                break;
                            default: source = "0";
                                break;
                        }

                        bll.AddProductStruApply_NewProduct(lbl_id.Text, source, no, desc, Session["plantCode"].ToString());
                    }
                    
                }
            }
        }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        //if (gv_det.Rows.Count > 0)
        //{
        if (txt_prodCode.Text.Trim() != "" && !bll.ProjectExists(txt_prodCode.Text.Trim()))
        {
            this.Alert("请输入正确项目号！");
            return;
        }
        string result = bll.CheckProductSize(lbl_id.Text);
        if (result != "")
        {
            this.Alert(result + "缺少尺寸，请先添加！");
            return;
        }
            bll.SubmitProductStruApplyMstr(lbl_id.Text, txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
            string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            string to="";
            string copy = "";
            string subject = "新增产品通知";
            string body = "";
            string uEmail="";
            if (lbl_Status.Text == "10")
            {
                
                bool b1 = false;
                bool b2 = false;
               
                string[] emails=null;
                if (gv_product != null)
                {
                    foreach (GridViewRow row in gv_product.Rows)
                    {
                        if (b1 && b2)
                        {
                            break;
                        }
                        if (row.Cells[6].Text.Replace("&nbsp;","").Trim() == "")
                        {
                            if (row.Cells[0].Text.Trim().Replace("&nbsp;", "") == "")
                            {
                                if (b1)
                                {
                                    continue;
                                }
                                b1 = true;
                                emails = bll.GetFlowNodePersonEmail("E5729681-D42D-4C24-94D2-D43504E718C5");
                                
                            }
                            else
                            {
                                if (b2)
                                {
                                    continue;
                                }
                                b2 = true;
                                emails = bll.GetFlowNodePersonEmail("857198CB-6F9B-4038-9DB4-36CADF97B57C");
                            }
                            if (uEmail == "")
                            {
                                uEmail = string.Join(";", emails);
                            }
                            else
                            {
                                uEmail += ";" + string.Join(";", emails);
                            }
                        }
                    }
                }
                to = uEmail;
                if (b1 || b2)
                {
                    #region 写Body
                    body += "<font style='font-size: 12px;'>申请单号：" + txt_nbr.Text + "</font><br />";
                    body += "<font style='font-size: 12px;'>项目号：" + txt_prodCode.Text + "</font><br />";
                    body += "<font style='font-size: 12px;'>有新产品需要你添加</font><br />";
                    body += "<br /><br />";
                    body += "<font style='font-size: 12px;'>详情请登陆 "+baseDomain.getPortalWebsite()+"/productApprove.aspx </font><br />";
                    #endregion
                    this.SendEmail(from, to, copy, subject, body);
                }
            }
            else if (lbl_Status.Text == "20")
            {
               
                subject = "BOM新增导入通知";
                to = "suhuiming@" + baseDomain.Domain[0]+";luosaihong.szx@"+baseDomain.Domain[0];
                #region 写Body
                body = "<font style='font-size: 12px;'>申请单号：" + txt_nbr.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>项目号：" + txt_prodCode.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>BOM需要导入系统</font><br />";
                body += "<br /><br />";
                body += "<font style='font-size: 12px;'>详情请登陆 " + baseDomain.getPortalWebsite() + "/ProductStruApply_New.aspxid=" + lbl_id.Text + "&rt=" + DateTime.Now.ToFileTime().ToString() + " </font><br />";
                #endregion
                this.SendEmail(from, to, copy, subject, body);
            }
            this.Alert("提交成功！");
        //}
        //else
        //{
        //    this.Alert("请导入产品结构！");
        //}
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        bll.UpdateProductStruApplyMstr(lbl_id.Text, txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-10", Session["uID"].ToString(), Session["uName"].ToString());
        BindMstrData();
        this.Alert("已取消！");
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        this.Redirect("productStruApply_List.aspx");
    }
    protected void gv_product_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_product.PageIndex = e.NewPageIndex;
        BindProductData();
    }
    protected void gv_det_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_det.PageIndex = e.NewPageIndex;
        BindDetailData();
    }
    protected void btn_Confirm_Click(object sender, EventArgs e)
    {
        QadService.WebService1SoapClient client = new QadService.WebService1SoapClient();
        client.Product_Add_Submit();
        int result = bll.ProductStruImport(lbl_id.Text, Session["uID"].ToString(), Session["plantCode"].ToString());
        if (result == 0)
        {
            client.Product_UPDATE_Submit();
            BindMstrData();
            this.Alert("产品结构导入成功！");


            IDataReader dr = bll.FindUsersInfo(hid_CreatedBy.Value.ToString());

            string uName = string.Empty;
            string uEmail = string.Empty;

            if (dr.Read())
            {
                uName = dr["userName"].ToString();
                uEmail = dr["email"].ToString();

            }
            dr.Close();

            bll.SelectRelationEmail(txt_nbr.Text, txt_prodCode.Text);


            #region 发送邮件
            string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            string to = uEmail;
            string copy = "";
            string subject = "BOM新增申请通过通知";
            string body = "";
            #region 写Body
            body += "<font style='font-size: 12px;'>申请单号：" + txt_nbr.Text + "</font><br />";
            body += "<font style='font-size: 12px;'>申请人:" + uName + "</font><br />";
            body += "<font style='font-size: 12px;'>项目号：" + txt_prodCode.Text + "</font><br />";
            body += "<font style='font-size: 12px;'>您的申请被" + Session["uName"].ToString() + "通过</font><br />";
            body += "<br /><br />";
            body += "<font style='font-size: 12px;'>详情请登陆 " + baseDomain.getPortalWebsite() + " </font><br />";
            #endregion
            if (!this.SendEmail(from, to, copy, subject, body))
            {
                this.ltlAlert.Text = "alert('邮件发送失败');";
            }
            else
            {
                this.ltlAlert.Text = "alert('邮件发送成功');";
            }
            string[] newZd = bll.GetNewZhengDeng(lbl_id.Text);
            if (newZd.Length > 0)
            {
                #region 发送邮件
                from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                to = "xinghaiqin@" +baseDomain.Domain[0];
                copy = "";
                subject = "整灯添加包装通知";
                body = "";
                #region 写Body
                body += "<font style='font-size: 12px;'>申请单号：" + txt_nbr.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>申请人:" + uName + "</font><br />";
                body += "<font style='font-size: 12px;'>项目号：" + txt_prodCode.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>以下申请的新整灯型号需要添加包装</font><br />";
                foreach (string zd in newZd)
                {
                    body += "<font style='font-size: 12px;'>" + zd + "</font><br />";
                }
                body += "<br /><br />";
                body += "<font style='font-size: 12px;'>详情请登陆 " + baseDomain.getPortalWebsite() + " </font><br />";
                #endregion
                this.SendEmail(from, to, copy, subject, body);
                #endregion
            }
            #endregion

        }
        else if (result == 2)
        {
            this.Alert("产品结构已存在！");
        }
        else
        {

            this.Alert("产品结构导入失败！");
        }
    }
    protected void btn_Reject_Click(object sender, EventArgs e)
    {
        if (txtReason.Text.Trim() == "")
        {
            this.Alert("请填写驳回意见！");
        }
        else
        {
            bll.UpdateProductStruApplyMstr(lbl_id.Text, txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-20", Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
            this.Alert("已驳回！");

            IDataReader dr = bll.FindUsersInfo(hid_CreatedBy.Value.ToString());

            string uName= string.Empty;
            string uEmail = string.Empty;

            if(dr.Read())
            {
                uName = dr["userName"].ToString();
                uEmail = dr["email"].ToString();
            
            }
            dr.Close();

            #region 发送邮件
            string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
            string to = uEmail;
            string copy = "";
            string subject = "BOM新增申请驳回通知";
            string body = "";
            #region 写Body
            body += "<font style='font-size: 12px;'>申请单号：" +txt_nbr.Text  + "</font><br />";
            body += "<font style='font-size: 12px;'>申请人:" + uName + "</font><br />";
            body += "<font style='font-size: 12px;'>项目号：" + txt_prodCode.Text + "</font><br />";
            body += "<font style='font-size: 12px;'>您的申请被" + Session["uName"].ToString() + "驳回，请及时处理</font><br />";
            body += "<br /><br />";
            body += "<font style='font-size: 12px;'>详情请登陆 "+baseDomain.getPortalWebsite()+" </font><br />";
            #endregion
            if (!this.SendEmail(from, to, copy, subject, body))
            {
                this.ltlAlert.Text = "alert('邮件发送失败');";
            }
            else
            {
                this.ltlAlert.Text = "alert('邮件发送成功');";
            }
            #endregion


            
        }
    }
    protected void likbtn_Click(object sender, EventArgs e)
    {
        string title = "100^<b>父级部件号</b>~^100^<b>QAD号</b>~^160^<b>子级部件号</b>~^100^<b>QAD号</b>~^300^<b>详细描述</b>~^80^<b>数量</b>~^80^<b>类型</b>~^100^<b>位号</b>~^250^<b>备注</b>~^100^<b>替代品</b>~^";

        DataTable dt = bll.GetProductStruApplyDetail(lbl_id.Text);

        DataTable dtExcel = new DataTable("temp");
        DataColumn col;
        DataRow drs ;

        #region 添加列
        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "productNumber";
        dtExcel.Columns.Add(col);

        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "productQad";
        dtExcel.Columns.Add(col);

        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "childNumber";
        dtExcel.Columns.Add(col);

        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "childQad";
        dtExcel.Columns.Add(col);

        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "description";
        dtExcel.Columns.Add(col);

        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "numOfChild";
        dtExcel.Columns.Add(col);

        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "childCategory";
        dtExcel.Columns.Add(col);

        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "posCode";
        dtExcel.Columns.Add(col);

        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "notes";
        dtExcel.Columns.Add(col);

        col = new DataColumn();
        col.DataType = System.Type.GetType("System.String");
        col.ColumnName = "itemNumber";
        dtExcel.Columns.Add(col);
        #endregion
       
        foreach(DataRow dr in dt.Rows)
        {
            drs = dtExcel.NewRow();
            drs["productNumber"] = dr["productNumber"].ToString();
            drs["productQad"] = dr["productQad"].ToString();
            drs["childNumber"] = dr["childNumber"].ToString();
            drs["childQad"] = dr["childQad"].ToString();
            drs["description"] = dr["description"].ToString();
            drs["numOfChild"] = dr["numOfChild"].ToString();
            drs["childCategory"] = dr["childCategory"].ToString();
            drs["posCode"] = dr["posCode"].ToString();
            drs["notes"] = dr["notes"].ToString();
            drs["itemNumber"] = dr["itemNumber"].ToString();
            dtExcel.Rows.Add(drs);
        
        }




        ExportExcel(title, dtExcel, false);
    }

    protected void linkProd_Click(object sender, EventArgs e)
    {
        string _src = "/RDW/RDW_DetailList.aspx?mid=" + lblProdId.Text
            + "&@__pn=&@__pc=&@__sd=&@__st=0&@__sk=0&@__pg=1&@__pc=&rm=" + DateTime.Now.ToString();
        ltlAlert.Text = "$.window('项目信息',1000,800,'" + _src + "');";
    }
}