using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductStruApply_Update : BasePage
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
                btn_Submit.Visible = false;
                btn_Cancel.Visible = false;
                trReason.Visible = false;
                trUpload.Visible = true;
                trUpload1.Visible = true;
                this.txt_prodCode.ReadOnly = false;
                this.txtRmks.ReadOnly = false;
            }
  
        }
    }
    private void BindMstrData()
    {
        IDataReader reader = bll.GetProductStruApplyUpdateMstr(lbl_id.Text);
        if (reader.Read())
        {
            txt_nbr.Text = reader["Code"].ToString();
            txt_status.Text = reader["Status"].ToString();
            txt_prodCode.Text = reader["ProdCode"].ToString();
            txtRmks.Text = reader["Desc"].ToString();
            lbl_Status.Text = reader["StatusValue"].ToString();
            txtReason.Text = reader["Reason"].ToString();
            BindDetailData();
            //BindProductData();
        }
        reader.Close();
        if (this.Security["45301040"].isValid && lbl_Status.Text == "10")
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
            if (this.Security["45301050"].isValid)
            {
                switch (lbl_Status.Text)
                {
                    case "0":
                        btn_Save.Visible = true;
                        btn_Submit.Visible = true;
                        btn_Cancel.Visible = true;
                        trReason.Visible = false;
                        trUpload.Visible = true;
                        trUpload1.Visible = true;
                        this.txt_prodCode.ReadOnly = false;
                        this.txtRmks.ReadOnly = false;
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
                        break;
                    default:
                        btn_Save.Visible = false;
                        btn_Submit.Visible = false;
                        btn_Cancel.Visible = false;
                        trReason.Visible = true;
                        trUpload.Visible = false;
                        trUpload1.Visible = false;
                        this.txt_prodCode.ReadOnly = true;
                        this.txtRmks.ReadOnly = true;
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
            }
        }
    }
    private void BindDetailData()
    {
        gv_det.DataSource = bll.GetProductStruApplyUpdateDetail(lbl_id.Text);
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
            this.Alert("请输入项目号！");
            return;
        }
        if (lbl_id.Text == "")
        {
            string id = bll.UpdateProductStruApplyMstr(txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
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
            bll.ProductStruApplyUpdateMstr(lbl_id.Text, txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), lbl_Status.Text, Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
        }
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        if (lbl_id.Text == "")
        {
            //if (txt_prodCode.Text.Trim() == "")
            //{
            //    this.Alert("请输入项目号！");
            //    return;
            //}
            string id = bll.ProductStruApplyUpdateMstr(txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
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
        bll.DeleteProductStruApplyUpdateDetail(lbl_id.Text);
        //bll.DeleteProductStruApply_UpdateProduct(lbl_id.Text);
        ImportExcelFile();
        BindMstrData();
        this.Alert("导入成功！");
        btn_Submit.Enabled = true;
    }
    private string CheckColumns(DataTable dt)
    {
        string err = string.Empty;
        string _parent, _qad, _child, _childqad, _pos, _qty, _replace, _dec, _newchild, _newchildqad, _newpos, _newqty, _newreplace, _newdec, _reson, _types, _no;
        for (int i = 0; i < 1; i++)
        {
             DataRow row = dt.Rows[i];
            _parent = row[0].ToString().Trim();
            _qad = row[1].ToString().Trim();
            _types = row[2].ToString().Trim();
            _child = row[3].ToString().Trim();
            _childqad = row[4].ToString().Trim();
            _pos = row[5].ToString().Trim();
            _qty = row[6].ToString().Trim();
            _replace = row[7].ToString().Trim();
            _dec = row[8].ToString().Trim();

            _newchild = row[9].ToString().Trim();
            _newchildqad = row[10].ToString().Trim();
            _newpos = row[11].ToString().Trim();
            _newqty = row[12].ToString().Trim();
            _newreplace = row[13].ToString().Trim();
            _newdec = row[14].ToString().Trim();
            _reson = row[15].ToString().Trim();
            _no = row[16].ToString().Trim();

            if (_parent != "父级部件号")
            {
                err += "模板第1列标题有误!";
            }
            if (_qad != "QAD号")
            {
                err += "模板第2列;";
            }
            if (_types != "操作类型")
            {
                err += "模板第3列标题有误!";
            }
            if (_child != "子级部件号")
            {
                err += "4;";
            }
            if (_childqad != "QAD号")
            {
                err += "5;";
            }
            if (_pos != "位号")
            {
                err += "6;";
            }
            if (_qty != "单计")
            {
                err += "7;";
            }
            if (_replace != "次选")
            {
                err += "8;";
            }
            if (_dec != "描述")
            {
                err += "9;";
            }

            if (_newchild != "子级部件号")
            {
                err += "10;";
            }
            if (_newchildqad != "QAD号")
            {
                err += "11;";
            }

            if (_newpos != "位号(可空)")
            {
                err += "12;";
            }
            if (_newqty != "单计(必填)")
            {
                err += "13;";
            }
            if (_newreplace != "次选(可空)")
            {
                err += "14;";
            }
            if (_newdec != "描述(可空)")
            {
                err += "15;";
            }
            if (_reson != "原因(必填)")
            {
                err += "16;";
            }
            if (_no != "跟踪号(必填)")
            {
                err += "17;";
            }
        }
        return err;
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
                    //string _new, _parent, _parentDesc, _child, _qty, _note, _pos, store, _replace, itemType, itemNum, itemstr, itemNums, qad;
                    string _parent, _parentqad, _child, _qad, _childqad, _pos, _qty, _replace, _parentDesc, _newqad, _newchild, _newchildqad, _newpos, _newqty, _newreplace, _newdec, _reson
                            , store, itemType, newitemType, itemNum, itemstr, itemNums, qad, _types, _dec, _no;
                    int _parentID, _childID, prodID, partID, semiProdID, ind, _newchildID, newprodID, newpartID, newsemiProdID;
                    Tuple<int, string> prodResult, semiProdResult, partResult, newprodResult, newsemiProdResult, newpartResult;
                    store = "";
                    StringBuilder newCode =new StringBuilder(";");
                    //if (dt.Columns[0].ColumnName != "是否新产品(Y/N)" || dt.Columns[1].ColumnName != "父级部件号" || dt.Columns[2].ColumnName != "产品描述" || dt.Columns[3].ColumnName != "子级部件号")
                    //{
                    //    dt.Reset();
                    //    this.Alert("导入文件不是产品结构修改导入模版");
                    //    return;
                    //}
                    DataTable newProducts = null;//dt.Select("[是否新产品(Y/N)]='y' or [是否新产品(Y/N)]='Y'").CopyToDataTable();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            string strerr = CheckColumns(dt);
                            if (!string.IsNullOrEmpty(strerr))
                            {
                                this.Alert("导入文件第 " + strerr + "列不是产品结构修改导入模版列");
                                return;
                            }
                        }
                        if (i > 0)
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
                            _types = row[2].ToString().Trim();

                            _child = row[3].ToString().Trim();
                            _qad = row[4].ToString().Trim();
                            _pos = row[5].ToString().Trim();
                            _qty = row[6].ToString().Trim();
                            _replace = row[7].ToString().Trim();
                            _dec = row[8].ToString().Trim();

                            _newchild = row[9].ToString().Trim();
                            _newqad = row[10].ToString().Trim();
                            _newpos = row[11].ToString().Trim();
                            _newqty = row[12].ToString().Trim();
                            _newreplace = row[13].ToString().Trim();
                            _newdec = row[14].ToString().Trim();
                            _reson = row[15].ToString().Trim();
                            _no = row[16].ToString().Trim();

                            //if ((string.IsNullOrEmpty(_parent) || string.IsNullOrEmpty(_child)) && !string.IsNullOrEmpty(_newchild))
                            if (string.IsNullOrEmpty(_parent))
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，父级部件号或子级部件号不能为空！");
                                dt.Reset();
                                return;
                            }

                            if (_types != "A" && _types != "D" && _types != "U")
                            {
                                this.Alert("操作类型不正确！");
                                dt.Reset();
                                return;
                            }
                            if (string.IsNullOrEmpty(_child) && _types != "A")
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，修改的子级部件号不能为空！");
                                dt.Reset();
                                return;
                            }
                            int errnum = bll.CheckQADExit(_parent, _child, _newchild, _pos, _newpos, _types);
                            if (errnum == -2)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "判断QAD信息失败，请联系管理员！");
                                dt.Reset();
                                return;
                            }
                            else if (errnum == 1)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "QAD信息失败,请确认QAD中QAD号信息是否存在！");
                                dt.Reset();
                                return;
                            }
                            else if (errnum == 2)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "所修改子级部件号已存在，无法更新！");
                                dt.Reset();
                                return;
                            }
                            if (string.IsNullOrEmpty(_newchild) && _types != "D")
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，修改的子级部件号不能为空！");
                                dt.Reset();
                                return;
                            }
                            if (_parent != "")
                            {
                                store = _parent;
                            }
                            if (_newpos.ToString().Length > 36)
                            {
                                this.Alert("位号不能超过36码！");
                                dt.Reset();
                                return;
                            }

                            while (_newreplace.Length > 0)
                            {
                                if (_newreplace.IndexOf(",") != -1 || _newreplace.IndexOf("，") != -1)
                                {
                                    if (_newreplace.IndexOf(",") != -1)
                                        ind = _newreplace.IndexOf(",");
                                    else if (_newreplace.IndexOf("，") != -1)
                                        ind = _newreplace.IndexOf("，");

                                    itemNum = _newreplace.Substring(0, ind).Trim();
                                    _newreplace = _newreplace.Substring(ind + 1).Trim();
                                }
                                else
                                {
                                    itemNum = _newreplace;
                                    _newreplace = "";
                                }
                                itemNums += itemNum + ",";
                                //if (newProducts.Select("父级部件号='" + itemNum + "'").Count() == 0)
                                //if (dt.Select("column1='" + itemNum + "'").Count() == 0)
                                //{
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
                            //}

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
                                //if (dt.Select("column1='" + _parent + "'").Count() == 0)
                                //{
                                _parentID = bll.FindProdID(_parent).Item1;
                                if (_parentID <= 0)
                                {
                                    _parentID = bll.FindSemiProdID(_parent).Item1;
                                    if (_parentID <= 0)
                                    {
                                        bBomError = true;
                                        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "产品/半成品型号不存在！");
                                        dt.Reset();
                                        return;
                                    }
                                }

                                if (string.IsNullOrEmpty(bll.FindProdID(_parent).Item2))
                                {
                                    this.Alert("父级QAD号不能为空！");
                                    dt.Reset();
                                    return;
                                }

                                if (bll.IsLocked(bll.FindProdID(_parent).Item2))
                                {
                                    this.Alert("已被锁定！");
                                    dt.Reset();
                                    return;
                                }
                                //}
                            }
                            else
                            {
                                _parent = store;
                            }

                            #region //判断原子部件信息
                            if (!bBomError && _types != "A")
                            {
                                _childID = 0;

                                //if (dt.Select("columns1='" + _child + "'").Count() == 0)
                                //{
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

                                            if (!bll.BomItemExist(_parent, prodID, "PROD",_pos) && _types == "U" && _child != _newchild)
                                            {
                                                bBomError = true;
                                                this.Alert(_child + "在产品结构中不存在！");
                                                dt.Reset();
                                                return;
                                            }
                                        }
                                        else if (semiProdID > 0)
                                        {

                                            if (bll.BomItemExist(_parent, semiProdID, "PROD", _pos) && _types == "U" && _child != _newchild)
                                            {
                                                bBomError = true;
                                                this.Alert(_child + "在产品结构中已经存在！");
                                                dt.Reset();
                                                return;
                                            }
                                        }
                                        else
                                        {

                                            if (!bll.BomItemExist(_parent, partID, "PART", _pos))
                                            {
                                                bBomError = true;
                                                this.Alert(_child + "在产品结构中不存在！");
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

                                //}
                            }
                            if (string.IsNullOrEmpty(_qty) && _types == "A")
                            {
                                _qty = "0";
                            }
                            else
                            {
                                decimal Qty = 0;
                                try
                                {
                                    Qty = decimal.Parse(_qty, System.Globalization.NumberStyles.Float);
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不是数值！");
                                    dt.Reset();
                                    return;
                                }
                                _qty = Qty.ToString();

                                if (Convert.ToDouble(_qty) <= 0)
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能小于零！");
                                    dt.Reset();
                                    return;
                                }
                            }
                            if (!bll.BomItemExistAndQty(_parent, _childID, _pos, _qty) && _types != "A")
                            {
                                bBomError = true;
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，"+_child + "原产品结构中信息不符！");
                                dt.Reset();
                                return;
                            }

                            #endregion

                            #region //判断更新部件号信息

                            if (_newchild != "" && _types != "D")
                            {
                                newprodResult = bll.FindProdID(_newchild);
                                newprodID = newprodResult.Item1;
                                newpartResult = bll.FindPartID(_newchild);
                                newpartID = newpartResult.Item1;
                                newsemiProdResult = bll.FindSemiProdID(_newchild);
                                newsemiProdID = newsemiProdResult.Item1;

                                if (newprodID > 0 && (newpartID > 0 || newsemiProdID > 0))
                                {
                                    bBomError = true;
                                    this.Alert(_newchild + "在产品库和部件库中均存在！");
                                    dt.Reset();
                                    return;
                                }

                                if (newprodID == 0 && newpartID == 0 && newsemiProdID == 0)
                                {
                                    bBomError = true;
                                    this.Alert(_newchild + "在库中不存在！");
                                    dt.Reset();
                                    return;
                                }
                                if (_parentID > 0)
                                {

                                    if (newprodID > 0)
                                    {

                                        if (bll.BomItemExist(_parent, newprodID, "PROD", _pos))
                                        {
                                            if (_types == "U" && _child != _newchild)
                                            {
                                                bBomError = true;
                                                this.Alert(_newchild + "在产品结构中已经存在！");
                                                dt.Reset();
                                                return;
                                            }
                                        }
                                    }
                                    else if (newsemiProdID > 0)
                                    {

                                        if (bll.BomItemExist(_parent, newsemiProdID, "PROD", _pos))
                                        {
                                            if (_types == "U" && _child != _newchild)
                                            {
                                                bBomError = true;
                                                this.Alert(_newchild + "在产品结构中已经存在！");
                                                dt.Reset();
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (bll.BomItemExist(_parent, newpartID, "PART", _pos))
                                        {
                                            if (_types == "U" && _child != _newchild)
                                            {
                                                bBomError = true;
                                                this.Alert(_newchild + "在产品结构中已经存在！");
                                                dt.Reset();
                                                return;
                                            }
                                        }
                                    }
                                }

                                if (newprodID > 0)
                                {
                                    newitemType = "PROD";
                                    _newchildID = newprodID;
                                    qad = newprodResult.Item2;
                                }
                                else if (newsemiProdID > 0)
                                {
                                    newitemType = "PROD";
                                    _newchildID = newsemiProdID;
                                    qad = newsemiProdResult.Item2;
                                }
                                else
                                {
                                    newitemType = "PART";
                                    _newchildID = newpartID;
                                    qad = newpartResult.Item2;
                                }
                            }
                            else
                            {
                                _newchildID = 0;
                                _newqty = "0";
                                newitemType = "";
                                //bBomError = true;
                                //this.Alert(_newchild + "在库中不存在！");
                                //dt.Reset();
                                //return;
                            }

                            if (_newqty == "" && _newchildID != 0)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能为空！");
                                dt.Reset();
                                return;
                            }
                            else
                            {

                                decimal NewQty = 0;
                                try
                                {
                                    NewQty = decimal.Parse(_newqty, System.Globalization.NumberStyles.Float);
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不是数值！");
                                    dt.Reset();
                                    return;
                                }
                                _newqty = NewQty.ToString();
                            }
                            if (!IsNumber(_newqty) && _newchildID != 0)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不是数值！");
                                dt.Reset();
                                return;
                            }
                            if (Convert.ToDouble(_newqty) <= 0 && _newchildID != 0)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能小于零！");
                                dt.Reset();
                                return;
                            }

                            if (_newpos.Length > 150)
                            {
                                _newpos = _newpos.Substring(0, 150).Trim();
                            }
                            #endregion

                            if (string.IsNullOrEmpty(_reson))
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，原因不能为空！");
                                dt.Reset();
                                return;
                            }

                            if (string.IsNullOrEmpty(_no))
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，跟踪单号不能为空！");
                                dt.Reset();
                                return;
                            }
                            string tmepno = "";
                            string no = _no;
                            int ii = 0;
                            while (no.Length > 0 && no != "未开跟踪单")
                            {

                                if (no.IndexOf(",") != -1 || no.IndexOf("，") != -1)
                                {
                                    if (no.IndexOf(",") != -1)
                                        ind = no.IndexOf(",");
                                    else if (no.IndexOf("，") != -1)
                                        ind = no.IndexOf("，");

                                    tmepno = no.Substring(ind + 1).Trim();
                                    no = no.Substring(0, ind).Trim();
                                }
                                else
                                {
                                    tmepno = no;
                                    no = "";
                                }
                                if (tmepno != "" && bll.CheckGenZhongHaoExit(tmepno).Rows.Count <= 0)
                                {
                                    ii++;
                                }
                            }
                            //if (bll.CheckGenZhongHaoExit(_no).Rows.Count <= 0 && _no != "未开跟踪单")
                            if (ii > 0 && _no != "未开跟踪单")
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，存在跟踪单号不不存在，如未开跟踪单请维护为：“未开跟踪单”！");
                                dt.Reset();
                                return;
                            }
                            if (!bBomError)
                            {
                                bll.InsertProductStruApplyUpdateDetail(lbl_id.Text, _parent, _parentqad, _child, itemNums, _parentID, _childID, _qad, _qty, itemType, _pos, itemstr, _newchild, _newchildID, _newqad, _newpos, _newqty, _reson, _types, newitemType, _dec, _newdec, _no, Session["plantCode"].ToString());
                            }
                        }
                    }
                }
            }
        }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (bll.CheckDataDouble(lbl_id.Text).Rows.Count > 0)
        {
            this.Alert("存在相同修改记录,请删除！");
            return;
        }

        if (gv_det.Rows.Count > 0)
        {
            bll.SubmitProductStruApplyUpdateMstr(lbl_id.Text, txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
            this.Alert("提交成功！");
        }
        else
        {
            this.Alert("请导入产品结构！");
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        bll.CancelProductStruApplyMstr(lbl_id.Text, txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-10", Session["uID"].ToString(), Session["uName"].ToString());
        BindMstrData();
        this.Alert("已取消！");
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        this.Redirect("productStruApplyUpdate_List.aspx");
    }
    //protected void gv_product_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gv_product.PageIndex = e.NewPageIndex;
    //    BindProductData();
    //}
    protected void gv_det_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_det.PageIndex = e.NewPageIndex;
        BindDetailData();
    }
    protected void btn_Confirm_Click(object sender, EventArgs e)
    {
        //string _parent = "";
        //DataTable dt = bll.GetProductStruApplyUpdateDetail(lbl_id.Text);
        //for(int i = 0,i<= dt.Rows.Count, i++)
        //{
        //_parent = dt.Rows.Contains("productNumber");
        //if (bll.IsLocked(_parent))
        //{
        //    this.Alert("已被锁定！");
        //    dt.Reset();
        //    return;
        //}
        //}
        if (!this.Security["45301040"].isValid)
        {
            this.Alert("没有操作权限！");
            return;
        }

        int result = bll.ProductStruUpdateImport(lbl_id.Text, Session["uID"].ToString(), Session["plantCode"].ToString());
        if (result == 0)
        {
            BindMstrData();
            QadService.WebService1SoapClient re = new QadService.WebService1SoapClient();
            re.Product_UPDATE_Submit();
            this.Alert("产品结构操作成功！");
        }
        else if (result == 1)
        {
            this.Alert("物料号库中不存在！");
        }
        else if (result == 2 || result == 13)
        {
            this.Alert("原产品结构不存在！");
        }
        else if (result == 3)
        {
            this.Alert("产品结构已存在！");
        }
        else
        {
            this.Alert("产品结构导入操作失败！");
        }
    }
    protected void btn_Reject_Click(object sender, EventArgs e)
    {
        if (!this.Security["45301040"].isValid)
        {
            this.Alert("没有操作权限！");
            return;
        }

        if (txtReason.Text.Trim() == "")
        {
            this.Alert("请填写驳回意见！");
        }
        else
        {
            bll.ReturnProductStruApplyMstr(lbl_id.Text, txt_prodCode.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-20", Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
            this.Alert("已驳回！");
        }
    }
}