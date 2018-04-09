using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductStru_PackingUpgradeApply : BasePage
{
    private ProductStru bll = new ProductStru();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.Security.Register("45301011", "BOM新增申请");
            //this.Security.Register("45301012", "BOM组新增确认");
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
                btn_Submit.Visible = true;
                btn_Cancel.Visible = true;
                trReason.Visible = false;
                trUpload.Visible = true;
                trUpload1.Visible = true;
                this.txt_prodCode.ReadOnly = false;
                this.txtRmks.ReadOnly = false;
                this.trUpload2.Visible = false;
                this.trLine.Visible = false;
                btn_ExportProd.Visible = false;
                trprodBom.Visible = false;
                trprodupgrade.Visible = false;
                trUpload.Visible = false;
                trUpload1.Visible = false;
                trpdetail.Visible = false;

                //light1.Visible = false;
                //qad1.Visible = false;
                //num.Visible = false;
                //txtread.Visible = false;
                //Butcal.Visible = false;
                //Butsave.Visible = false;
                //Butadd.Visible = false;
            }
  
        }
    }
    private void BindMstrData()
    {
        IDataReader reader = bll.GetPackingUpgradepplyMstr(lbl_id.Text);
        if (reader.Read())
        {
            txt_nbr.Text = reader["Code"].ToString();
            txt_status.Text = reader["Status"].ToString();
            txt_prodCode.Text = reader["ProdCode"].ToString();
            txtRmks.Text = reader["Desc"].ToString();
            lbl_Status.Text = reader["StatusValue"].ToString();
            txtReason.Text = reader["Reason"].ToString();
            BindDetailData();
            BindProductData();
            Bindprod();
        }
        reader.Close();
        if (this.Security["46001020"].isValid && (lbl_Status.Text == "30" || lbl_Status.Text == "10"))
        {
            this.trReview.Visible = true;
            this.trReason.Visible = true;
            this.txtReason.ReadOnly = false;
            this.trApply.Visible = false;
            this.trUpload.Visible = false;
            this.trUpload1.Visible = false;
            this.txt_prodCode.ReadOnly = true;
            this.trLine.Visible = true;
            this.txtRmks.ReadOnly = true;
            btn_ExportProd.Visible = true;
            btn_submitbom.Visible = false;

            if (lbl_Status.Text == "10")
            {
                this.trUpload2.Visible = true;
                //gv_product.Columns[0].Visible = true;
                btn_Confirm.Visible = false;
                trprodBom.Visible = false;
                trpdetail.Visible = false;
                gv_det.Visible = false;
                btn_submitbom.Visible = true;
            }
            else
            {
                this.trUpload2.Visible = false;
                //gv_product.Columns[0].Visible = false;
                btn_Confirm.Visible = true;
                gv_det.Visible = true;
            }
        }
        else if(lbl_Status.Text == "20")
        {
                this.trReview.Visible = false;
                this.trApply.Visible = true;
                this.txtReason.ReadOnly = true;
                btn_Save.Visible = false;
                btn_Submit.Visible = true;
                btn_Cancel.Visible = true;
                trReason.Visible = false;
                trUpload.Visible = true;
                trUpload1.Visible = true;
                this.txt_prodCode.ReadOnly = false;
                this.txtRmks.ReadOnly = false;
                this.trUpload2.Visible = false;
                this.trLine.Visible = false;
                trprodBom.Visible = true;
                trpdetail.Visible = true;
                trprodupgrade.Visible = true;
                trUpload.Visible = true;
                trUpload1.Visible = true;
                btn_Submit.Visible = true;
                btn_submitbom.Visible = false;
        }
        else
        {
            this.trReview.Visible = false;
            this.trApply.Visible = true;
            this.txtReason.ReadOnly = true;
            if (this.Security["46001030"].isValid)
            {
                switch (lbl_Status.Text)
                {
                    case "0":
                        btn_Save.Visible = true;
                        btn_Submit.Visible = true;
                        btn_Cancel.Visible = true;
                        trReason.Visible = false;
                        //trUpload.Visible = true;
                        //trUpload1.Visible = true;
                        this.txt_prodCode.ReadOnly = false;
                        
                        this.txtRmks.ReadOnly = false;
                        this.trUpload2.Visible = false;
                        this.trLine.Visible = false;
                        btn_ExportProd.Visible = true;
                        trprodBom.Visible = false;
                        trpdetail.Visible = false;
                        trprodupgrade.Visible = true;
                        trUpload.Visible = false;
                        trUpload1.Visible = false;
                        btn_submitbom.Visible = false;
                        //gv_product.Columns[0].Visible = false;
                        break;
                    case "-20":
                        btn_Save.Visible = true;
                        btn_Submit.Visible = true;
                        btn_Cancel.Visible = true;
                        trReason.Visible = true;                       
                        trUpload.Visible = true;
                        trUpload1.Visible = true;
                        this.txt_prodCode.ReadOnly = false;
                        this.txtRmks.ReadOnly = false;
                        this.trUpload2.Visible = false;
                        this.trLine.Visible = false;
                        btn_ExportProd.Visible = false;
                        btn_ExportProd.Visible = true;
                        btn_submitbom.Visible = false;
                        trUpload.Visible = false;
                        trUpload1.Visible = false;
                        trprodBom.Visible = false;
                        trpdetail.Visible = false;
                        break;
                    default:
                        btn_Save.Visible = false;
                        btn_Submit.Visible = false;
                        btn_Cancel.Visible = false;
                        trReason.Visible = true;
                        trUpload.Visible = false;
                        trUpload1.Visible = false;
                        trUpload2.Visible = false;
                        this.txt_prodCode.ReadOnly = true;
                        this.txtRmks.ReadOnly = true;
                        btn_submitbom.Visible = false;
                        //gv_product.Columns[0].Visible = false;
                        trprodBom.Visible = false;
                        trpdetail.Visible = false;
                        break;
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
                btn_submitbom.Visible = false;
            }
        }
    }
    private void BindDetailData()
    {
        gv_det.DataSource = bll.GetPackingUpgradeApplyDetail(lbl_id.Text, lbl_Status.Text);
        gv_det.DataBind();
    }

    //private void BindProductData()
    //{
    //    gv_product.DataSource = bll.GetProductStruApplyUpdateProduct(lbl_id.Text);
    //    gv_product.DataBind();
    //}
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (txt_prodCode.Text.Trim() == "")
        {
            this.Alert("请输入产品型号！");
            return;
        }
        int prodid = bll.PackingUpgradeFindProdIDFind(txt_prodCode.Text.Trim()).Item1;
        string prodqad = bll.PackingUpgradeFindProdIDFind(txt_prodCode.Text.Trim()).Item2.ToString();
        if (prodid == 0 || string.IsNullOrEmpty(prodqad))
        {
            this.Alert("未找到产品信息或QAD号为空！");
            return;
        }
        if (lbl_id.Text == "")
        {
            string id = bll.PackingUpgradeApplyMstr(txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
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
        else
        {
            bll.ProductStruApplyUpdateMstr(lbl_id.Text, txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), lbl_Status.Text, Session["uID"].ToString(), Session["uName"].ToString());
        }
        BindMstrData();
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        if (lbl_id.Text == "")
        {
            string id = bll.PackingUpgradeApplyMstr(txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
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
        bll.DeletePackingUpgradeDetail(lbl_id.Text);
        //bll.DeleteProductStruApply_UpdateProduct(lbl_id.Text);
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
                    string _parent, _parentqad, _parentnew, _parentqadnew, _child, _childqad, _dec, _qad, _dec1, _dec2, _pos, _qty, _replace, _reson
                            , itemNum, itemstr, itemType, itemNums, qad, _parentTemp;
                            //, itemType, itemNum, itemstr,  qad;
                    int _parentID, _childID, prodID, partID, semiProdID, ind;
                    Tuple<int, string> prodResult, semiProdResult, partResult;
                    _parentTemp = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Columns[0].ColumnName != "原整灯型号" || dt.Columns[1].ColumnName != "原整灯QAD" || dt.Columns[2].ColumnName != "新整灯型号"
                            || dt.Columns[3].ColumnName != "新整灯QAD" || dt.Columns[4].ColumnName != "子级型号"
                            || dt.Columns[5].ColumnName != "子级QAD号" || dt.Columns[6].ColumnName != "子级描述"|| dt.Columns[7].ColumnName != "QAD"
                            || dt.Columns[8].ColumnName != "QAD描述1"|| dt.Columns[9].ColumnName != "QAD描述2" || dt.Columns[10].ColumnName != "位号"
                            || dt.Columns[11].ColumnName != "用量" || dt.Columns[12].ColumnName != "替代品" || dt.Columns[13].ColumnName != "备注")
                        {
                            dt.Reset();
                            this.Alert("导入文件不是产品结构申请导入模版");
                            return;
                        }
                        if (i >= 0)
                        {
                            bBomError = false;
                            itemstr = "";
                            itemNums = "";
                            ind = 0;
                            _parentID = 0;
                            _childID = 0;
                            itemType = "PROD";
                            qad = "";
                            DataRow row = dt.Rows[i];
                            _parent = row[0].ToString().Trim();
                            _parentqad = row[1].ToString().Trim();
                            _parentnew = row[2].ToString().Trim();
                            _parentqadnew = row[3].ToString().Trim();
                            _child = row[4].ToString().Trim();
                            _childqad = row[5].ToString().Trim();
                            _dec = row[6].ToString().Trim();
                            _qad = row[7].ToString().Trim();
                            _dec1 = row[8].ToString().Trim();
                            _dec2 = row[9].ToString().Trim();
                            _pos = row[10].ToString().Trim();
                            _qty = row[11].ToString().Trim();
                            _replace = row[12].ToString().Trim();
                            _reson = row[13].ToString().Trim();

                            //if (!string.IsNullOrEmpty(_parentTemp))
                            //{
                            //    if (_parentTemp != _parent && !string.IsNullOrEmpty(_parent))
                            //    {
                            //        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，产品型号不同,每次只能升级一个产品型号！");
                            //        dt.Reset();
                            //        return;
                            //    }
                            //}

                            _parentTemp = _parent;

                            if (string.IsNullOrEmpty(_parent))
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，产品型号不能为空！");
                                dt.Reset();
                                return;
                            }

                            if (string.IsNullOrEmpty(_childqad))
                            {
                                this.Alert("文件格式错误 --行 " + (i).ToString() + "，子部件QAD号不能为空！");
                                dt.Reset();
                                return;
                            }

                            int errnum = bll.PackingUpgradeCheckUpgradeInfo(_parent, _child, lbl_id.Text);
                            if (errnum == -2)
                            {
                                this.Alert("判断BOM信息失败，请联系管理员！");
                                dt.Reset();
                                return;
                            }
                            else if (errnum == 1)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "原子父级部件信息不存在，无法更新！");
                                dt.Reset();
                                return;
                            }
                            else if (errnum == 2)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "原子级部件信息不存在，无法更新！");
                                dt.Reset();
                                return;
                            }
                            if (_pos.ToString().Length > 36)
                            {
                                this.Alert("位号不能超过36码！");
                                dt.Reset();
                                return;
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
                                itemNums += itemNum + ",";
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

                            if (itemstr.Length > 0)
                            {
                                itemstr = itemstr.Trim().Substring(0, itemstr.Length - 1);
                            }

                            if (itemNums.Length > 0)
                            {
                                itemNums = itemNums.Trim().Substring(0, itemNums.Length - 1);
                            }

                            if (_parent != "")
                            {
                                bBomError = false;
                                _parentID = 0;
                                _parentID = bll.PackingUpgradeFindProdID(_parent).Item1;
                                if (_parentID <= 0)
                                {
                                    bBomError = true;
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "产品型号不存在！");
                                    dt.Reset();
                                    return;
                                }

                                if (string.IsNullOrEmpty(bll.PackingUpgradeFindProdID(_parent).Item2))
                                {
                                    this.Alert("父级QAD号不能为空！");
                                    dt.Reset();
                                    return;
                                }

                                //if (!bll.IsLocked(bll.FindProdID(_parent).Item2))
                                //{
                                //    this.Alert("未锁定,请到BOM修改里做修改！");
                                //    dt.Reset();
                                //    return;
                                //}
                            }

                            #region //判断原子部件信息
                            if (!bBomError)
                            {
                                _childID = 0;
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
                            if (string.IsNullOrEmpty(_qty))
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能为空！");
                                dt.Reset();
                                return;
                            }
                            else
                            {
                                try
                                {
                                    Convert.ToDouble(_qty);
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能小于零！");
                                    dt.Reset();
                                    return;
                                }

                                if (Convert.ToDouble(_qty) <= 0)
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能小于零！");
                                    dt.Reset();
                                    return;
                                }
                            }
                            if (_parentqad.Length != 14)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，父级QAD不正确,QAD不为14码！");
                                dt.Reset();
                                return;
                            }
                            if (_childqad.Length != 14 && !string.IsNullOrEmpty(_childqad))
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，子级QAD不正确,QAD不为14码！");
                                dt.Reset();
                                return;
                            }
                            if (_childqad.Substring(0, 1) == "1" && _childqad.Substring(_childqad.Length - 1, 1) == "1")
                            {
                                int count = bll.CheckStruExists(_parent, _parentqad, _child, _childqad, true);
                                if (count == 1 || count == 2)
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，物料在QAD与100中不存在！");
                                    dt.Reset();
                                    return;
                                }
                                else if (count == 3)
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，结构在100中不存在！");
                                    dt.Reset();
                                    return;
                                }
                            }
                            else
                            {
                                int count = bll.CheckStruExists(_parent, _parentqad, _child, _childqad, false);
                                if (count == 1 || count == 2)
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，物料在QAD与100中不存在！");
                                    dt.Reset();
                                    return;
                                }
                            }

                            #endregion

                            if (!bBomError)
                            {
                                bll.InsertPackingUpgradeApplyDetail(lbl_id.Text, _parent, _parentqad, _parentnew, _parentqadnew, _child, _childqad, _parentID, _childID
                                        , _dec, _qad, _dec1, _dec2, _pos, _qty, itemNums, itemType, itemstr, _reson, Session["uID"].ToString(), Session["plantCode"].ToString());
                            }
                        }
                    }
                }
            }
        }    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lbl_id.Text))
        {
            this.Alert("记录不存在,请保存后再做提交！");
            return;
        }
        if (bll.CheckPackingUpgradeDetailExists(lbl_id.Text).Rows.Count <= 0 && lbl_Status.Text == "20")
        {
            this.Alert("请先上传明细,再做提交！");
            return;
        }
        if (bll.CheckPackingUpgradeDataDouble(lbl_id.Text).Rows.Count > 0)
        {
            this.Alert("存在相同修改记录,请删除！");
            return;
        }
        if (lbl_Status.Text == "20")
        {
            if (gv_det.Rows.Count > 0)
            {
                bll.Submit_PackingUpgradeApplyMstr(lbl_id.Text, txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
                BindMstrData();
                this.Alert("提交成功！");
            }
            else
            {
                this.Alert("请导入产品结构！");
            }
        }
        else
        {
            bll.Submit_PackingUpgradeApplyMstr(lbl_id.Text, txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
            this.Alert("提交成功！");
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(lbl_id.Text))
        {
            this.Alert("还未提交,无法取消！");
            return;
        }

        bll.CancelPackingUpgradeApplyMstr(lbl_id.Text, txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-10", Session["uID"].ToString(), Session["uName"].ToString());
        bll.DeletePackingUpgradeDetail(lbl_id.Text);
        bll.DeletePackingUpgradeDetailNew(lbl_id.Text);
        BindMstrData();
        this.Alert("已取消！");
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        this.Redirect("ProductStru_PackingUpgradeApply_List.aspx?status=" + Request.QueryString["status"]);
    }
    //protected void gv_product_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gv_product.PageIndex = e.NewPageIndex;
    //    BindProductData();
    //}

    protected void gvShipdetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_product.EditIndex = -1;
        BindProductData();
    }
    protected void gvShipdetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_product.EditIndex = e.NewEditIndex;
        BindProductData();
    }
    protected void gvShipdetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Adds")
        {
            // ltlAlert.Text = "window.open('SID_ShipDetailAdds.aspx?DID=" + Server.UrlEncode(gvShipdetail.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString().Trim()) + "&rm=" + DateTime.Now + "','','menubar=no,scrollbars=no,resizable=no,width=1000,height=500,top=0,left=0');";
            //Response.Redirect("SID_ShipDetailAdds.aspx?DID=" + Server.UrlEncode(gvShipdetail.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString().Trim()) + "&DID_ori=" + Convert.ToString(Request.QueryString["DID"]) + "&rm=" + DateTime.Now.ToString());
        }
    }
    protected void gv_product_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string strDID = gv_product.DataKeys[e.RowIndex].Values["code"].ToString();
        String _parentcode = gv_product.Rows[e.RowIndex].Cells[1].Text.Trim();
        String _parentqad = gv_product.Rows[e.RowIndex].Cells[2].Text.Trim();
        int _parentID = bll.FindProdID(_parentcode).Item1;
        String _parentcodenew = ((TextBox)gv_product.Rows[e.RowIndex].Cells[3].FindControl("txt_ProdCodeNew")).Text.ToString().Trim();
        String _description = ((TextBox)gv_product.Rows[e.RowIndex].Cells[4].FindControl("txt_description")).Text.ToString().Trim();
        DataTable dt = bll.GetPackingUpgradeExport(strDID, txt_prodCode.Text.Trim(), lbl_id.Text);
        string _parentqadnew,_dec, _dec1, _dec2, _qad;
        _parentqadnew = "";
        _dec = _description;//dt.Rows[0]["description"].ToString().Trim();
        _dec1 = dt.Rows[0]["item_qad_desc1"].ToString().Trim();
        _dec2 = dt.Rows[0]["item_qad_desc2"].ToString().Trim();
        _qad = dt.Rows[0]["qad"].ToString().Trim();
        if (bll.FindProdID(_parentcodenew).Item1 > 0)
        {
            this.Alert(_parentcodenew + "新整灯型号已存在，请更新一个新的型号！");
            dt.Reset();
            return;
        }
        int errnum = bll.InsertPackingUpgradeApplyDetailNew1(lbl_id.Text, _parentcode, _parentqad, _parentID, _parentcodenew, _parentqadnew, _dec, _dec1, _dec2
                                       , _qad, Session["uID"].ToString(), Session["uName"].ToString(), Session["plantCode"].ToString());
        if (errnum == 0)
        {
            this.Alert("_parentcodenew" + "新产品型号维护失败，请联系管理员！");
            return;
        }
        gv_product.EditIndex = -1;
        BindMstrData();
        this.Alert("维护成功！");
    }
    protected void gv_det_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_det.PageIndex = e.NewPageIndex;
        BindDetailData();
    }
    protected void btn_Confirm_Click(object sender, EventArgs e)
    {
        int result = bll.PackingUpgrade_Import(lbl_id.Text, Session["uID"].ToString(), Session["plantCode"].ToString());
        if (result == 0)
        {
            BindMstrData();
            bll.SelectRelationEmail(txt_nbr.Text, txt_prodCode.Text);
            QadService.WebService1SoapClient re = new QadService.WebService1SoapClient();
            re.Product_Add_Submit();
            re.Product_UPDATE_Submit();
            this.Alert("产品结构操作成功！");

        }
        else if (result == 1)
        {
            this.Alert("生成QAD号错误,请联系管理员！");
        }
        else if (result == 2 || result == 3 || result == 6)
        {
            this.Alert("新整灯数据插入错误,请联系管理员！");
        }
        else if (result == 4)
        {
            this.Alert("原整灯不存在！");
        }
        else if (result == 5)
        {
            this.Alert("子级部件号不存在！");
        }
        else if (result == 6)
        {
            this.Alert("新整灯数据插入错误！");
        }
        else if (result == 7)
        {
            this.Alert("BOM结构插入失败！");
        }
        else if (result == 8 || result == 9)
        {
            this.Alert("替代品插入失败！");
        }
        else if (result == 11 || result == 12)
        {
            this.Alert("整灯信息或BOM写入QAD错误！");
        }
        else
        {
            this.Alert("产品结构导入操作失败！");
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
            bll.PackingUpgrade_RejectApplyMstr(lbl_id.Text, txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-20", Session["uID"].ToString(), Session["uName"].ToString());
            bll.DeletePackingUpgradeDetail(lbl_id.Text);
            bll.DeletePackingUpgradeDetailNew(lbl_id.Text);
            BindMstrData();
            this.Alert("已驳回！");
        }
    }
    protected void btn_ExportProd_Click(object sender, EventArgs e)
    {
        int count= 0;
        StringBuilder ids = new StringBuilder();
        foreach (GridViewRow row in gv_product.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk.Checked)
            {
                string id = gv_product.DataKeys[row.RowIndex].Values["code"].ToString();
                ids.Append(";").Append(id);
                count = count + 1;
            }
        }
        //允许同时升级多个版本,UPDATE WANGLW AT:2016-05-23
        //if (count > 1)
        //{
        //    ltlAlert.Text = "alert('每次只能升级一个版本!');";
        //    return;
        //}
        if (ids.Length > 0)
        {
            ids.Remove(0, 1);
        }
        else
        {
            ltlAlert.Text = "alert('请选择要升级产品!');";
            return;
        }

        DataTable dt = bll.GetPackingUpgradeExport(ids.ToString(), txt_prodCode.Text.Trim(), lbl_id.Text);
        string title = "<b>原整灯型号</b>~^<b>原整灯QAD</b>~^<b>新整灯型号</b>~^<b>新整灯QAD</b>~^<b>新整灯描述</b>~^<b>QAD描述1</b>~^<b>QAD描述2</b>~^<b>裸灯用量</b>~^<b>QAD</b>~^";
        ExportExcel(title, dt, false);

        //else if (lbl_Status.Text == "20")
        //{
        //    DataTable dt = bll.GetPackingUpgradeBomExport(ids.ToString(), txt_prodCode.Text.Trim(), lbl_id.Text);
        //    string title = "<b>原整灯型号</b>~^<b>原整灯QAD</b>~^<b>新整灯型号</b>~^<b>新整灯QAD</b>~^<b>新整灯描述</b>~^<b>QAD描述1</b>~^<b>QAD描述2</b>~^<b>裸灯用量</b>~^";
        //    ExportExcel(title, dt, false);
        //}
    }

    private void ImportExcelFileNew()
    {
        DataTable dt;
        bool bBomError;
        String strFileName = "";
        String strCatFolder = "";
        String strUserFileName = "";
        int intLastBackslash = 0;

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
        strUserFileName = filename2.PostedFile.FileName;
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
        if (filename2.PostedFile != null)
        {
            if (filename2.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('上传的文件最大为 8 MB!');";
                return;
            }
            try
            {
                filename2.PostedFile.SaveAs(strFileName);
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
                    string _parentcode, _parentqad, _parentcodenew, _parentqadnew, _dec, _dec1, _dec2, itemType, _qad;
                    int _parentID, _childID, prodID, partID, semiProdID, ind, _newchildID, newprodID, newpartID, newsemiProdID;
                    Tuple<int, string> prodResult, semiProdResult, partResult, newprodResult, newsemiProdResult, newpartResult;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Columns[0].ColumnName != "原整灯型号" || dt.Columns[1].ColumnName != "原整灯QAD" || dt.Columns[2].ColumnName != "新整灯型号" || dt.Columns[3].ColumnName != "新整灯QAD" || dt.Columns[4].ColumnName != "新整灯描述" || dt.Columns[5].ColumnName != "QAD描述1" || dt.Columns[6].ColumnName != "QAD描述2" || dt.Columns[7].ColumnName != "裸灯用量" || dt.Columns[8].ColumnName != "QAD")
                        {
                            dt.Reset();
                            this.Alert("导入文件不是产品结构申请导入模版");
                            return;
                        }
                        if (i >= 0)
                        {
                            bBomError = false;
                            _parentID = 0;
                            _childID = 0;
                            itemType = "PROD";
                            DataRow row = dt.Rows[i];
                            _parentcode = row[0].ToString().Trim();
                            _parentqad = row[1].ToString().Trim();
                            _parentcodenew = row[2].ToString().Trim();
                            _parentqadnew = row[3].ToString().Trim();
                            _dec = row[4].ToString().Trim();
                            _dec1 = row[5].ToString().Trim();
                            _dec2 = row[6].ToString().Trim();
                            _qad = row[8].ToString().Trim();

                            if (string.IsNullOrEmpty(_parentcode))
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，产品型号不能为空！");
                                dt.Reset();
                                return;
                            }
                            else
                            {
                                bBomError = false;
                                _parentID = 0;
                                _parentID = bll.FindProdID(_parentcode).Item1;
                                if (_parentID <= 0)
                                {
                                    _parentID = bll.FindSemiProdID(_parentcode).Item1;
                                    if (_parentID <= 0)
                                    {
                                        bBomError = true;
                                        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "产品/半成品型号不存在！");
                                        dt.Reset();
                                        return;
                                    }
                                }
                                if (string.IsNullOrEmpty(bll.FindProdID(_parentcode).Item2))
                                {
                                    this.Alert("父级QAD号不能为空！");
                                    dt.Reset();
                                    return;
                                }
                            }
                            if (string.IsNullOrEmpty(_parentcodenew))
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "新整灯型号不能为空，无法更新！");
                                dt.Reset();
                                return;
                            }
                            if (bll.FindProdID(_parentcodenew).Item1 > 0)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "新整灯型号已存在，请更新一个新的型号！");
                                dt.Reset();
                                return;
                            }
                            if (_qad.Length > 8)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "QAD号不能超过8位，请更新！");
                                dt.Reset();
                                return;
                            }

                            if (!bBomError)
                            {
                                int errnum =  bll.InsertPackingUpgradeApplyDetailNew(lbl_id.Text, _parentcode, _parentqad, _parentID, _parentcodenew, _parentqadnew, _dec, _dec1, _dec2
                                                                       , _qad, Session["uID"].ToString(), Session["uName"].ToString(), Session["plantCode"].ToString());
                                if (errnum == -2)
                                {
                                    this.Alert("新产品型号维护失败，请联系管理员！");
                                    dt.Reset();
                                    return;
                                }
                                else if (errnum == 1)
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "新产品型号维护失败，请联系管理员！");
                                    dt.Reset();
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        //bll.SubmitPackingUpgradeDetailNew(lbl_id.Text);
    }

    protected void btn_UploadProdcut_Click(object sender, EventArgs e)
    {
        bll.DeletePackingUpgradeDetailNew(lbl_id.Text);
        ImportExcelFileNew();
        BindMstrData();
        this.Alert("导入成功！");
    }
    protected void gv_product_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string selected = gv_product.DataKeys[e.Row.RowIndex].Values["selected"].ToString();
            if (selected == "1")
            {
                CheckBox chk = e.Row.FindControl("chk") as CheckBox;
                chk.Checked = true;
            }
        }
    }

    protected void gv_product_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_product.PageIndex = e.NewPageIndex;
        BindProductData();
    }

    private void BindProductData()
    {
        gv_product.DataSource = bll.GetPackingUpgradeVersion(txt_prodCode.Text.Trim(), lbl_id.Text, lbl_Status.Text);
        gv_product.DataBind();
        if (lbl_Status.Text == "20" || lbl_Status.Text == "30" || lbl_Status.Text == "40")
        {
            foreach (GridViewRow row in gv_product.Rows)
            {
                CheckBox chk = row.FindControl("chk") as CheckBox;
                chk.Enabled = false;
                //if (chk.Checked)
                //{
                //    string id = gv_product.DataKeys[row.RowIndex].Values["code"].ToString();
                //}
            }
        }
    }
    protected void btn_bomexport_Click(object sender, EventArgs e)
    {
        DataTable dt = bll.GetPackingUpgradeBomExport(lbl_id.Text);
        string title = "150^<b>原整灯型号</b>~^110^<b>原整灯QAD</b>~^150^<b>新整灯型号</b>~^110^<b>新整灯QAD</b>~^150^<b>子级型号</b>~^110^<b>子级QAD号</b>~^200^<b>子级描述</b>~^100^<b>QAD</b>~^200^<b>QAD描述1</b>~^200^<b>QAD描述2</b>~^<b>位号</b>~^<b>用量</b>~^<b>替代品</b>~^<b>备注</b>~^";
        ExportExcel(title, dt, false);
    }
    protected void btn_submitbom_Click(object sender, EventArgs e)
    {
        if (bll.CheckPackingUpgradeExists(txt_prodCode.Text.Trim(), lbl_id.Text, lbl_Status.Text).Rows.Count <= 0)
        {
            this.Alert("请先上传升级产品信息,再做提交！");
            return;
        }
        bll.SubmitPackingUpgradeDetailNew(lbl_id.Text);
        BindMstrData();
    }

    protected void Butadd_Click(object sender, EventArgs e)
    {
      
        edtflg.Value = "1";
        clear(); 
        if (Prodcd.Items.Count > 1)
        {
            //string lsval = Prodcd.Items[1].Value.ToString();
            Prodcd.SelectedIndex = 1;
            Prodcd_SelectedIndexChanged(null, null);
        }
        controlbtn(true);
       
    }


    protected void Butcal_Click(object sender, EventArgs e)
    {
        edtflg.Value = "0";
        controlbtn(false);
        clear();
    }


    protected void controlbtn(bool s)
    {  
        light1.Enabled = s;
        qad1.Enabled = s;
        num.Enabled  = s;
        Textrep.Enabled = s;
        txtread.Enabled = s;
        Butcal.Enabled = s;
        Butsave.Enabled = s;
        posino.Enabled = s;
        Prodcd.Enabled = s;
        Prodcdnew.Enabled = edtflg.Value.Trim()=="1" ? true : false ;
        Butadd.Visible = !s;
    }

    protected void clear()
    {
        Prodcd.SelectedValue = "";
        prodcdqad.Text = "";
        num.Text = "";
        txtread.Text = "";
        light.Value = "";
        qad.Value = "";
        light1.Text = "";
        qad1.Text = "";
        Textrep.Text = "";
        posino.Text = "";
        Prodcdnew.Text = "";
        Textdesc.Text = "";
    }


    protected void gv_det_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "edit")
        {
            clear();
            int intRow = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            string light11 = gv_det.DataKeys[intRow].Values["childcode"].ToString().Trim();
            string qad11 = gv_det.DataKeys[intRow].Values["childqad"].ToString();
            string qua = gv_det.DataKeys[intRow].Values["numofchild"].ToString().Trim();
            string rea = gv_det.DataKeys[intRow].Values["reason"].ToString();
            string qad10 = gv_det.DataKeys[intRow].Values["ProdCodeQad"].ToString();
            string procd = gv_det.DataKeys[intRow].Values["ProdCode"].ToString();
            string rpl = gv_det.DataKeys[intRow].Values["itemreplace"].ToString();
            string posi = gv_det.DataKeys[intRow].Values["posCode"].ToString();
            string newpro = gv_det.DataKeys[intRow].Values["productNumbernew"].ToString(); 
            string descs  = gv_det.DataKeys[intRow].Values["description"].ToString();
            try
            {
                Prodcd.SelectedValue = procd;
            }
            catch
            {
                controlbtn(false);
                this.Alert("修改选择的产品型号不在升级产品选择列表中！");
                return;
            }
            prodcdqad.Text = qad10;
            edtflg.Value = "2";
            light1.Text = light11;
            qad1.Text = qad11;
            num.Text = qua;
            light.Value = light11;
            qad.Value = qad11;
            posino.Text = posi == null ? "" : posi;
            txtread.Text = rea;
            Textrep.Text = rpl == null ? "" : rpl;
            Prodcdnew.Text = newpro == null ? "" : newpro;
            Textdesc.Text = descs == null ? "" : descs;
            if (string.IsNullOrEmpty(Prodcdnew.Text.Trim()))
                Prodcd_SelectedIndexChanged(null,null);
            controlbtn(true);
        }
        else if (e.CommandName.ToString() == "delete")
        {
            int intRow = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            string qad10 = gv_det.DataKeys[intRow].Values["ProdCodeQad"].ToString();
            string procd = gv_det.DataKeys[intRow].Values["ProdCode"].ToString();
            string light11 = gv_det.DataKeys[intRow].Values["childcode"].ToString().Trim();
            string qad11 = gv_det.DataKeys[intRow].Values["childqad"].ToString();

            bll.delPackingUpgradeApplyDetail(lbl_id.Text, procd, qad10, light11, qad11);

            if (Prodcd.Text.Trim() == procd.Trim() && light1.Text.Trim() == light11.Trim())
            {
                Butcal_Click(null, null);             
            }
            BindDetailData();

        }

    }


    protected void gv_det_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void Butsave_Click(object sender, EventArgs e)
    {
        string lsorgcd       = Prodcd.SelectedValue;     //.Text.Trim();
        string lsorgcdqad    = prodcdqad.Text.Trim();
        string lschildnew    = light1.Text.Trim();
        string lschildnewqad = qad1.Text.Trim();
        string lschild       = light.Value.Trim();
        string lschildqad    = qad.Value.Trim();
        string lsqua         = num.Text.Trim();
        string lsposi        = posino.Text.Trim();
        string reasons       = txtread.Text.Trim();
        string rpls          = Textrep.Text.Trim();
        string newlsorgcd    = Prodcdnew.Text.Trim();
        string flg           = edtflg.Value;

        string itemNum, itemstr, itemNums, qadstr, itemType;
        bool bBomError;
        int prodID, partID, semiProdID, ind, _parentID, _childID ;
        Tuple<int, string> prodResult, semiProdResult, partResult;

        int chk1 = bll.checkPackingUpgradeApplyDetailexist(lbl_id.Text);
        if (chk1==0)
        {
            int chk2 = ImportExcelFileauto();
            if (chk2 == 0)
            {
                bll.DeletePackingUpgradeDetail(lbl_id.Text);
                return;
             }
        }
          
        bBomError = false;
        itemstr = "";
        itemNums = "";
        ind = 0;
        _childID = 0;
        _parentID = 0;
        itemType = "PROD";

        if (string.IsNullOrEmpty(lsorgcd))
        {
            this.Alert("整灯型号不能为空！");
            return;
        }

        if (string.IsNullOrEmpty(newlsorgcd))
        {
            this.Alert("新整灯型号不能为空！");
            return;
        }

        if (string.IsNullOrEmpty(lschildnew))
        {
            this.Alert("子级部件号不能为空！");
            return;
        }

        if (string.IsNullOrEmpty(lschildnewqad))
        {
            this.Alert("子级部件QAD号不能为空！");
            return;
        }

        int errnum = bll.PackingUpgradeCheckUpgradeInfo(lsorgcd, lschildnew, lbl_id.Text);
        if (errnum == -2)
        {
            this.Alert("判断BOM信息失败，请联系管理员！");
            return;
        }
        else if (errnum == 1)
        {
            this.Alert("原子父级部件信息不存在，无法更新！");
            return;
        }
        else if (errnum == 2)
        {
            this.Alert("原子级部件信息不存在，无法更新！");
            return;
        }
        if (lsposi.ToString().Length > 36)
        {
            this.Alert("位号不能超过36码！");
            return;
        }


        while (rpls.Length > 0)
        {
            if (rpls.IndexOf(",") != -1 || rpls.IndexOf("，") != -1)
            {
                if (rpls.IndexOf(",") != -1)
                    ind = rpls.IndexOf(",");
                else if (rpls.IndexOf("，") != -1)
                    ind = rpls.IndexOf("，");

                 itemNum = rpls.Substring(0, ind).Trim();
                 rpls = rpls.Substring(ind + 1).Trim();
            }
            else
            {
                itemNum = rpls;
                rpls = "";
            }
            itemNums += itemNum + ",";
            prodID = bll.FindProdID(itemNum).Item1;
            partID = bll.FindPartID(itemNum).Item1;
            semiProdID = bll.FindSemiProdID(itemNum).Item1;
            if (prodID > 0 && (partID > 0 || semiProdID > 0))
            {
                bBomError = true;
                this.Alert(itemNum + "在产品库和部件库中均存在！");
                return;
            }

            if (prodID == 0 && partID == 0 && semiProdID == 0)
            {
                bBomError = true;
                this.Alert(itemNum + "在库中不存在！");
                return;
            }
            if (prodID > 0)
                itemstr += prodID + ",";
            else if (partID > 0)
                itemstr += partID + ",";
            else if (semiProdID > 0)
                itemstr += semiProdID + ",";
            }

            if (itemstr.Length > 0)
            {
                itemstr = itemstr.Trim().Substring(0, itemstr.Length - 1);
            }

            if (itemNums.Length > 0)
            {
                itemNums = itemNums.Trim().Substring(0, itemNums.Length - 1);
            }


            if (lsorgcd != "")
            {
                bBomError = false;
                _parentID = 0;
                _parentID = bll.PackingUpgradeFindProdID(lsorgcd).Item1;
                if (_parentID <= 0)
                {
                    bBomError = true;
                    this.Alert("产品型号不存在！");
                    return;
                }

                if (string.IsNullOrEmpty(bll.PackingUpgradeFindProdID(lsorgcd).Item2))
                {
                    this.Alert("父级QAD号为空！");
                    return;
                }   
            }


            if (!bBomError)
            {
                prodResult = bll.FindProdID(lschildnew);
                prodID = prodResult.Item1;
                partResult = bll.FindPartID(lschildnew);
                partID = partResult.Item1;
                semiProdResult = bll.FindSemiProdID(lschildnew);
                semiProdID = semiProdResult.Item1;
               

                if (prodID > 0 && (partID > 0 || semiProdID > 0))
                {
                    bBomError = true;
                    this.Alert(lschildnew + "在产品库和部件库中均存在！");
                    return;
                }

                if (prodID == 0 && partID == 0 && semiProdID == 0)
                {
                    bBomError = true;
                    this.Alert(lschildnew + "在库中不存在！");
                    return;
                }
                if (prodID > 0)
                {
                    itemType = "PROD";
                    _childID = prodID;
                    qadstr = prodResult.Item2;
                }
                else if (semiProdID > 0)
                {
                    itemType = "PROD";
                    _childID = semiProdID;
                    qadstr = semiProdResult.Item2;
                }
                else
                {
                    itemType = "PART";
                    _childID = partID;
                    qadstr = partResult.Item2;
                }

            }
            if (string.IsNullOrEmpty(lsqua))
            {
                this.Alert("数量不能为空！");
                return;
            }
            else
            {
                try
                {
                    Convert.ToDouble(lsqua);
                }
                catch
                {
                    this.Alert("数量不能小于零或空！");
                    return;
                }

                if (Convert.ToDouble(lsqua) <= 0)
                {
                    this.Alert("数量不能小于零！");
                    return;
                }
            }
            if (lsorgcdqad.Length != 14)
            {
                this.Alert("父级QAD不正确,QAD不为14码！");
                return;
            }
            if (lschildnewqad.Length != 14 && !string.IsNullOrEmpty(lschildnewqad))
            {
                this.Alert("子级QAD不正确,QAD不为14码或为空！");
                return;
            }
            if (lschildnewqad.Substring(0, 1) == "1" && lschildnewqad.Substring(lschildnewqad.Length - 1, 1) == "1")
            {
                int count = bll.CheckStruExists(lsorgcd, lsorgcdqad, lschildnew, lschildnewqad, true);
                if (count == 1 || count == 2)
                {
                    this.Alert("物料在QAD与100中不存在！");
                    return;
                }
                else if (count == 3)
                {
                    this.Alert("结构在100中不存在！");
                    return;
                }
            }
            else
            {
                int count = bll.CheckStruExists(lsorgcd, lsorgcdqad, lschildnew, lschildnewqad, false);
                if (count == 1 || count == 2)
                {
                    this.Alert("物料在QAD与100中不存在！");
                    return;
                }
            }

            if (!bBomError)
            {
            if (flg == "2")
            {
                if (lschildnew.Trim() != lschild.Trim())
                {
                    int chksum = bll.checkPackingUpgradeApplyDetailbymanul(lbl_id.Text, lsorgcd, lsorgcdqad, lschildnew, lschildnewqad);
                    if (chksum != 0)
                    {
                        this.Alert("更改的子级部件已存在不得重复！");
                        return;
                    }
                }
                bll.updatePackingUpgradeApplyDetail(lbl_id.Text, lsorgcd, lsorgcdqad, lschildnew, lschildnewqad, _childID, lsposi, lsqua, itemNums, itemType, itemstr, reasons, lschild, lschildqad);
            }
            else if (flg == "1")
            {
                int chksum = bll.checkPackingUpgradeApplyDetailbymanul(lbl_id.Text, lsorgcd, lsorgcdqad, lschildnew, lschildnewqad);
                if (chksum != 0)
                {
                    this.Alert("子级部件已存在不得重复增加！");
                    return;
                }
                bll.InsertPackingUpgradeApplyDetailbymanul(lbl_id.Text, lsorgcd, lsorgcdqad, newlsorgcd, "", lschildnew, lschildnewqad, _parentID, _childID, lsposi
                    , lsqua, itemNums, itemType, itemstr, reasons, Session["uID"].ToString(), Session["plantCode"].ToString());
            }
                Butcal_Click(null, null);
                BindDetailData();

            }


    }

    protected void gv_det_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    private void Bindprod()
    {
        Prodcd.DataSource = bll.getproduct(lbl_id.Text);
        Prodcd.DataBind();
        Prodcd.SelectedIndex = 0;
    }

    protected void Prodcd_SelectedIndexChanged(object sender, EventArgs e)
    {
        string svalue = Prodcd.SelectedValue;
        if (string.IsNullOrEmpty(svalue))
        {
            prodcdqad.Text = "";
            Prodcdnew.Text="";
         }
        else
        {
            prodcdqad.Text = bll.PackingUpgradeFindProdID(svalue).Item2 == null ? "" : bll.PackingUpgradeFindProdID(svalue).Item2;
            DataTable getnewpro= bll.getnewpro(lbl_id.Text, svalue);
            if (getnewpro != null && getnewpro.Rows.Count > 0)
            {
                Prodcdnew.Text = getnewpro.Rows[0][0].ToString();
            }
        }
    }



    private int ImportExcelFileauto()
    {
        DataTable dt;
        bool bBomError;     
        dt = bll.GetPackingUpgradeBomExport(lbl_id.Text);

        if (dt.Rows.Count > 0)
        {
            string _parent, _parentqad, _parentnew, _parentqadnew, _child, _childqad, _dec, _qad, _dec1, _dec2, _pos, _qty, _replace, _reson
                    , itemNum, itemstr, itemType, itemNums, qad, _parentTemp;
            int _parentID, _childID, prodID, partID, semiProdID, ind;
            Tuple<int, string> prodResult, semiProdResult, partResult;
            _parentTemp = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bBomError = false;
                itemstr = "";
                itemNums = "";
                ind = 0;
                _parentID = 0;
                _childID = 0;
                itemType = "PROD";
                qad = "";
                DataRow row = dt.Rows[i];
                _parent = row[0].ToString().Trim();
                _parentqad = row[1].ToString().Trim();
                _parentnew = row[2].ToString().Trim();
                _parentqadnew = row[3].ToString().Trim();
                _child = row[4].ToString().Trim();
                _childqad = row[5].ToString().Trim();
                _dec = row[6].ToString().Trim();
                _qad = row[7].ToString().Trim();
                _dec1 = row[8].ToString().Trim();
                _dec2 = row[9].ToString().Trim();
                _pos = row[10].ToString().Trim();
                _qty = row[11].ToString().Trim();
                _replace = row[12].ToString().Trim();
                _reson = row[13].ToString().Trim();

                _parentTemp = _parent;

                if (string.IsNullOrEmpty(_parent))
                {
                    this.Alert("自动导入升级部件-文件格式错误 --行 " + i.ToString() + "，产品型号不能为空！");
                    dt.Reset();
                    return 0;
                }

                int errnum = bll.PackingUpgradeCheckUpgradeInfo(_parent, _child, lbl_id.Text);
                if (errnum == -2)
                {
                    this.Alert("自动导入升级部件-判断BOM信息失败，请联系管理员！");
                    dt.Reset();
                    return 0;
                }
                else if (errnum == 1)
                {
                    this.Alert("自动导入升级部件-文件格式错误 --行 " + i.ToString() + "原子父级部件信息不存在，无法更新！");
                    dt.Reset();
                    return 0;
                }
                else if (errnum == 2)
                {
                    this.Alert("行 " + (i + 2).ToString() + "自动导入升级部件-原子级部件信息不存在，无法更新！");
                    dt.Reset();
                    return 0;
                }
                if (_pos.ToString().Length > 36)
                {
                    this.Alert("自动导入升级部件-位号不能超过36码！");
                    dt.Reset();
                    return 0;
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
                    itemNums += itemNum + ",";
                    prodID = bll.FindProdID(itemNum).Item1;
                    partID = bll.FindPartID(itemNum).Item1;
                    semiProdID = bll.FindSemiProdID(itemNum).Item1;
                    if (prodID > 0 && (partID > 0 || semiProdID > 0))
                    {
                        bBomError = true;
                        this.Alert(itemNum + "自动导入升级部件在产品库和部件库中均存在！");
                        dt.Reset();
                        return 0;
                    }

                    if (prodID == 0 && partID == 0 && semiProdID == 0)
                    {
                        bBomError = true;
                        this.Alert(itemNum + "自动导入升级部件-在库中不存在！");
                        dt.Reset();
                        return 0;
                    }
                    if (prodID > 0)
                        itemstr += prodID + ",";
                    else if (partID > 0)
                        itemstr += partID + ",";
                    else if (semiProdID > 0)
                        itemstr += semiProdID + ",";
                }

                if (itemstr.Length > 0)
                {
                    itemstr = itemstr.Trim().Substring(0, itemstr.Length - 1);
                }

                if (itemNums.Length > 0)
                {
                    itemNums = itemNums.Trim().Substring(0, itemNums.Length - 1);
                }

                if (_parent != "")
                {
                    bBomError = false;
                    _parentID = 0;
                    _parentID = bll.PackingUpgradeFindProdID(_parent).Item1;
                    if (_parentID <= 0)
                    {
                        bBomError = true;
                        this.Alert("自动导入升级部件-文件格式错误 --行 " + i.ToString() + "产品型号不存在！");
                        dt.Reset();
                        return 0;
                    }

                    if (string.IsNullOrEmpty(bll.PackingUpgradeFindProdID(_parent).Item2))
                    {
                        this.Alert("自动导入升级部件-父级QAD号不能为空！");
                        dt.Reset();
                        return 0;
                    }

                }

                #region //判断原子部件信息
                if (!bBomError)
                {
                    _childID = 0;
                    prodResult = bll.FindProdID(_child);
                    prodID = prodResult.Item1;
                    partResult = bll.FindPartID(_child);
                    partID = partResult.Item1;
                    semiProdResult = bll.FindSemiProdID(_child);
                    semiProdID = semiProdResult.Item1;

                    if (prodID > 0 && (partID > 0 || semiProdID > 0))
                    {
                        bBomError = true;
                        this.Alert(_child + "自动导入升级部件在产品库和部件库中均存在！");
                        dt.Reset();
                        return 0;
                    }

                    if (prodID == 0 && partID == 0 && semiProdID == 0)
                    {
                        bBomError = true;
                        this.Alert(_child + "自动导入升级部件在库中不存在！");
                        dt.Reset();
                        return 0;
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
                if (string.IsNullOrEmpty(_qty))
                {
                    this.Alert("自动导入升级部件 --行 " + i.ToString() + "，数量不能为空！");
                    dt.Reset();
                    return 0;
                }
                else
                {
                    try
                    {
                        Convert.ToDouble(_qty);
                    }
                    catch
                    {
                        this.Alert("自动导入升级部件 --行 " + i.ToString() + "，数量不能小于零！");
                        dt.Reset();
                        return 0;
                    }

                    if (Convert.ToDouble(_qty) <= 0)
                    {
                        this.Alert("自动导入升级部件--行 " + i .ToString() + "，数量不能小于零！");
                        dt.Reset();
                        return 0;
                    }
                }
                if (_parentqad.Length != 14)
                {
                    this.Alert("自动导入升级部件--行 " + i .ToString() + "，父级QAD不正确,QAD不为14码！");
                    dt.Reset();
                    return 0;
                }
                if (_childqad.Length != 14 && !string.IsNullOrEmpty(_childqad))
                {
                    this.Alert("自动导入升级部件 --行 " + i.ToString() + "，子级QAD不正确,QAD不为14码！");
                    dt.Reset();
                    return 0;
                }
                if (_childqad.Substring(0, 1) == "1" && _childqad.Substring(_childqad.Length - 1, 1) == "1")
                {
                    int count = bll.CheckStruExists(_parent, _parentqad, _child, _childqad, true);
                    if (count == 1 || count == 2)
                    {
                        this.Alert("自动导入升级部件 --行 " + i.ToString() + "，物料在QAD与100中不存在！");
                        dt.Reset();
                        return 0;
                    }
                    else if (count == 3)
                    {
                        this.Alert("自动导入升级部件 --行 " + i.ToString() + "，结构在100中不存在！");
                        dt.Reset();
                        return 0;
                    }
                }
                else
                {
                    int count = bll.CheckStruExists(_parent, _parentqad, _child, _childqad, false);
                    if (count == 1 || count == 2)
                    {
                        this.Alert("自动导入升级部件 --行 " + i.ToString() + "，物料在QAD与100中不存在！");
                        dt.Reset();
                        return 0;
                    }
                }

                #endregion

                if (!bBomError)
                {
                    bll.InsertPackingUpgradeApplyDetail(lbl_id.Text, _parent, _parentqad, _parentnew, _parentqadnew, _child, _childqad, _parentID, _childID
                            , _dec, _qad, _dec1, _dec2, _pos, _qty, itemNums, itemType, itemstr, _reson, Session["uID"].ToString(), Session["plantCode"].ToString());
                }

            }
            return 1;
        }
        else
        {
            this.Alert("无数据自动导入升级部件！");
            return 0;
        }
      }


    protected void qad1_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(qad1.Text.Trim()))
        {
            Tuple<int, string> semiProdResultProd = null;
            Tuple<int, string> semiProdResultPart = null;
            Tuple<int, string> semiProdResultsemi = null;
            semiProdResultProd = bll.FindProdIDqad(qad1.Text.Trim());
            semiProdResultPart = bll.FindPartIDqad(qad1.Text.Trim());
            semiProdResultsemi = bll.FindSemiProdqad(qad1.Text.Trim());


            if (semiProdResultProd.Item1> 0 && (semiProdResultPart.Item1 > 0 || semiProdResultsemi.Item1> 0))
            {           
                this.Alert(qad1.Text.Trim() + "在产品库和部件库中均存在！");
                light1.Text = "";
                return;
            }

            if (semiProdResultProd.Item1 == 0 && semiProdResultPart.Item1 == 0 && semiProdResultsemi.Item1 == 0)
            {

                this.Alert(qad1.Text.Trim() + "在库中不存在！");
                light1.Text = "";
                return;
            }

            if (semiProdResultProd.Item1 > 0)
            {

                light1.Text = semiProdResultProd.Item2 == null ?"": semiProdResultProd.Item2;
            }
            else if (semiProdResultsemi.Item1 > 0)
            {

                light1.Text = semiProdResultsemi.Item2 == null ? "" : semiProdResultsemi.Item2;
            }
            else
            {
                light1.Text = semiProdResultPart.Item2 == null ? "" : semiProdResultPart.Item2;
            }

         
        }
        else
            light1.Text = "";

    }
}
        
    


