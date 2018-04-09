using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class ProductStruApply_UpdateByECN : BasePage
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
                this.txt_ECNNO.ReadOnly = false;
                this.txtRmks.ReadOnly = false;
            }
  
        }
    }
    private void BindMstrData()
    {
        IDataReader reader = bll.GetProductStruApplyUpdateByECNMstr(lbl_id.Text);
        if (reader.Read())
        {
            txt_nbr.Text = reader["Code"].ToString();
            txt_status.Text = reader["Status"].ToString();
            txt_ECNNO.Text = reader["ProdCode"].ToString();
            txt_nbr1.Text = reader["ProdCode"].ToString();
            txtRmks.Text = reader["Desc"].ToString();
            lbl_Status.Text = reader["StatusValue"].ToString();
            txtReason.Text = reader["Reason"].ToString();
            BindDetailData();
            //BindProductData();
        }
        reader.Close();
        if (this.Security["45305060"].isValid && lbl_Status.Text == "10")
        {
            this.trReview.Visible = true;
            this.trReason.Visible = true;
            this.txtReason.ReadOnly = false;
            this.trApply.Visible = false;
            this.trUpload.Visible = false;
            this.trUpload1.Visible = false;
            this.txt_ECNNO.ReadOnly = true;
            this.txtRmks.ReadOnly = true;
            btn_Confirm.Visible = false;
            btn_Confirm1.Visible = true;
            dviecn.Visible = false;
        }
        else if (this.Security["45305030"].isValid && lbl_Status.Text == "20")
        {
            this.trReview.Visible = true;
            this.trReason.Visible = true;
            this.txtReason.ReadOnly = false;
            this.trApply.Visible = false;
            this.trUpload.Visible = false;
            this.trUpload1.Visible = false;
            this.txt_ECNNO.ReadOnly = true;
            this.txtRmks.ReadOnly = true;
            btn_Confirm.Visible = true;
            btn_Confirm1.Visible = false;
            dviecn.Visible = false;
        }
        else
        {
            this.trReview.Visible = false;
            this.trApply.Visible = true;
            this.txtReason.ReadOnly = true;
            if (this.Security["45305040"].isValid)
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
                        this.txt_ECNNO.ReadOnly = false;
                        this.txtRmks.ReadOnly = false;
                        break;
                    case "-20":
                        btn_Save.Visible = true;
                        btn_Submit.Visible = true;
                        btn_Cancel.Visible = true;
                        trReason.Visible = true;                       
                        trUpload.Visible = true;
                        trUpload1.Visible = true;
                        this.txt_ECNNO.ReadOnly = false;
                        this.txtRmks.ReadOnly = false;
                        break;
                    default:
                        btn_Save.Visible = false;
                        btn_Submit.Visible = false;
                        btn_Cancel.Visible = false;
                        trReason.Visible = true;
                        trUpload.Visible = false;
                        trUpload1.Visible = false;
                        this.txt_ECNNO.ReadOnly = true;
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
                this.txt_ECNNO.ReadOnly = true;
                this.txtRmks.ReadOnly = true;
            }
        }
    }
    private void BindDetailData()
    {
        gv_det.DataSource = bll.GetProductStruApplyUpdateByECNDetail(lbl_id.Text);
        gv_det.DataBind();
    }

    //private void BindProductData()
    //{
    //    gv_product.DataSource = bll.GetProductStruApplyUpdateProduct(lbl_id.Text);
    //    gv_product.DataBind();
    //}
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (txt_ECNNO.Text.Trim() == "")
        {
            this.Alert("请输入ECN号！");
            return;
        }
        int errnum = bll.CheckECNStatusByECN(txt_ECNNO.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
        if (errnum == -2)
        {
            this.Alert("获取ECN号信息失败！");
            return;
        }
        else if (errnum == 1)
        {
            this.Alert("此ECN号不存在！");
            return;
        }
        else if (errnum == 2)
        {
            this.Alert("此ECN号还未通过！");
            return;
        }
        if (lbl_id.Text == "")
        {
            string id = bll.UpdateProductStruApplyByECNMstr(txt_ECNNO.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
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
            bll.ProductStruApplyUpdateByECNMstr(lbl_id.Text, txt_ECNNO.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), lbl_Status.Text, Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
        }
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        if (lbl_id.Text == "")
        {
            if (txt_ECNNO.Text.Trim() == "")
            {
                this.Alert("请输入ECN号！");
                return;
            }
            int errnum = bll.CheckECNStatusByECN(txt_ECNNO.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            if (errnum == -2)
            {
                this.Alert("获取ECN号信息失败！");
                return;
            }
            else if (errnum == 1)
            {
                this.Alert("此ECN号不存在！");
                return;
            }
            else if (errnum == 2)
            {
                this.Alert("此ECN号还未通过！");
                return;
            }
            else if (errnum == 3)
            {
                this.Alert("此ECN号安规有变动不能做修改！");
                return;
            }
            string id = bll.ProductStruApplyUpdateByECNMstr(txt_ECNNO.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
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
        bll.DeleteProductStruApplyUpdateByECNDetail(lbl_id.Text);
        //bll.DeleteProductStruApply_UpdateProduct(lbl_id.Text);
        ImportExcelFile();
        BindMstrData();
        this.Alert("导入成功！");
        btn_Submit.Enabled = true;
    }
    private string CheckColumns(DataTable dt)
    {
        string err = string.Empty;
        string _top, _topqad, _parent, _qad, _child, _childqad, _pos, _qty, _replace, _dec, _newchild, _newchildqad, _newpos, _newqty, _newreplace, _newdec, _reson, _types;
        for (int i = 0; i < 1; i++)
        {
             DataRow row = dt.Rows[i];
             _top = row[0].ToString().Trim();
             _topqad = row[1].ToString().Trim();
            _parent = row[2].ToString().Trim();
            _qad = row[3].ToString().Trim();
            _types = row[4].ToString().Trim();
            _child = row[5].ToString().Trim();
            _childqad = row[6].ToString().Trim();
            _pos = row[7].ToString().Trim();
            _qty = row[8].ToString().Trim();
            _replace = row[9].ToString().Trim();
            _dec = row[10].ToString().Trim();

            _newchild = row[11].ToString().Trim();
            _newchildqad = row[12].ToString().Trim();
            _newpos = row[13].ToString().Trim();
            _newqty = row[14].ToString().Trim();
            _newreplace = row[15].ToString().Trim();
            _newdec = row[16].ToString().Trim();
            _reson = row[17].ToString().Trim();

            if (_top != "TOP")
            {
                err += "模板第1列标题有误!";
            }
            if (_topqad != "QAD号")
            {
                err += "模板第2列标题有误!";
            }
            if (_parent != "父级部件号")
            {
                err += "模板第3列标题有误!";
            }
            if (_qad != "QAD号")
            {
                err += "模板第4列;";
            }
            if (_types != "操作类型")
            {
                err += "模板第5列标题有误!";
            }
            if (_child != "子级部件号")
            {
                err += "6;";
            }
            if (_childqad != "QAD号")
            {
                err += "7;";
            }
            if (_pos != "位号")
            {
                err += "8;";
            }
            if (_qty != "单计")
            {
                err += "9;";
            }
            if (_replace != "次选")
            {
                err += "10;";
            }
            if (_dec != "描述")
            {
                err += "11;";
            }

            if (_newchild != "子级部件号")
            {
                err += "12;";
            }
            if (_newchildqad != "QAD号")
            {
                err += "13;";
            }

            if (_newpos != "位号(可空)")
            {
                err += "14;";
            }
            if (_newqty != "单计(必填)")
            {
                err += "15;";
            }
            if (_newreplace != "次选(可空)")
            {
                err += "16;";
            }
            if (_newdec != "描述(可空)")
            {
                err += "17;";
            }
            if (_reson != "原因(必填)")
            {
                err += "18;";
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
                    string _top, _topqad, _parent, _parentqad, _child, _qad, _childqad, _pos, _replace, _parentDesc, _newqad, _newchild, _newchildqad, _newpos, _newqty, _newreplace, _newdec, _reson
                            , store, itemType, newitemType, itemNum, itemstr, itemNums, qad, _types, _dec;
                    int _parentID, _childID, prodID, partID, semiProdID, ind, _newchildID, newprodID, newpartID, newsemiProdID;
                    decimal _qty;
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
                            _top = row[0].ToString().Trim();
                            _topqad = row[1].ToString().Trim();
                            _parent = row[2].ToString().Trim();
                            _parentqad = row[3].ToString().Trim();
                            _types = row[4].ToString().Trim();

                            _child = row[5].ToString().Trim();
                            _qad = row[6].ToString().Trim();
                            _pos = row[7].ToString().Trim();
                            if (string.IsNullOrEmpty(row[8].ToString().Trim()))
                            {
                                _qty = 0;
                            }
                            else
                            {
                                _qty = decimal.Parse(row[8].ToString().Trim(), System.Globalization.NumberStyles.Float);//row[8].ToString().Trim();
                            }
                            _replace = row[9].ToString().Trim();
                            _dec = row[10].ToString().Trim();

                            _newchild = row[11].ToString().Trim();
                            _newqad = row[12].ToString().Trim();
                            _newpos = row[13].ToString().Trim();
                            _newqty = row[14].ToString().Trim();
                            _newreplace = row[15].ToString().Trim();
                            _newdec = row[16].ToString().Trim();
                            _reson = row[17].ToString().Trim();

                            int CheckTOP = bll.CheckTOPExistsISOnGoing(lbl_id.Text, _parent);
                            if (CheckTOP == 1)
                            {
                                bBomError = true;
                                this.Alert(_top + "存在进行中修改，请待修改完整再进行！");
                                dt.Reset();
                                return;
                            }
                            else if (CheckTOP == -1)
                            {
                                bBomError = true;
                                this.Alert(_top + "TOP检验失败！");
                                dt.Reset();
                                return;
                            }

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
                            int errnum = bll.CheckQADExitByECN(_parent, _child, _newchild, _pos, _newpos, _types);
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

                                if (!bll.IsLocked(bll.FindProdID(_parent).Item2))
                                {
                                    this.Alert("未被锁定！");
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
                            if (_types == "A")
                            {
                                if (string.IsNullOrEmpty(_qty.ToString()))
                                {
                                    _qty = 0;
                                }
                            }
                            else
                            {
                                try
                                {
                                    Convert.ToDecimal(_qty);
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
                                if (string.IsNullOrEmpty(_qad))
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，子级部件号QAD号不能为空！");
                                    dt.Reset();
                                    return;
                                }
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

                                if (newprodID == 0 && newpartID == 0 && newsemiProdID == 0 && _newchild.Substring(_newchild.Length - 5).ToUpper() != "(NEW)")
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

                            if ((string.IsNullOrEmpty(_newqty) ||  _newqty == "") && _newchildID != 0)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能为空！");
                                dt.Reset();
                                return;
                            }
                            else if (!IsNumber(_newqty) && _newchildID != 0)
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不是数值！");
                                dt.Reset();
                                return;
                            }
                            else if (Convert.ToDouble(_newqty == null ||  _newqty == "" ? "0" : _newqty) <= 0 && _newchildID != 0 && _newchild.Substring(_newchild.Length - 5).ToUpper() != "(NEW)")
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，数量不能小于零！");
                                dt.Reset();
                                return;
                            }

                            if (_newpos.Length > 150)
                            {
                                _newpos = _newpos.Substring(0, 150).Trim();
                            }
                            if (string.IsNullOrEmpty(_newqad) && _newchild.Substring(_newchild.Length - 5).ToUpper() != "(NEW)")
                            {
                                this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，QAD号不能为空！");
                                dt.Reset();
                                return;
                            }

                            #endregion

                            if (!bBomError)
                            {
                                bll.InsertProductStruApplyUpdateByECNDetail(lbl_id.Text, _top, _topqad, _parent, _parentqad, _child, itemNums, _parentID, _childID, _qad, _qty, itemType, _pos, itemstr, _newchild, _newchildID, _newqad, _newpos, _newqty, _reson, _types, newitemType, _dec, _newdec, Session["plantCode"].ToString());
                            }
                        }
                    }
                }
            }
        }
    }
    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        int errnum = bll.CheckECNStatusByECN(txt_ECNNO.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
        if (errnum == -2)
        {
            this.Alert("获取ECN号信息失败！");
            return;
        }
        else if (errnum == 1)
        {
            this.Alert("此ECN号不存在！");
            return;
        }
        else if (errnum == 2)
        {
            this.Alert("此ECN号还未通过！");
            return;
        }
        else if (errnum == 3)
        {
            this.Alert("此ECN号安规有变动不能做修改！");
            return;
        }

        if (bll.CheckDataDoubleByECN(lbl_id.Text).Rows.Count > 0)
        {
            this.Alert("存在相同修改记录,请删除！");
            return;
        }

        if (gv_det.Rows.Count > 0)
        {
            bll.SubmitProductStruApplyUpdateByECNMstr(lbl_id.Text, txtRmks.Text.Trim(), "Ap", Session["uID"].ToString(), Session["uName"].ToString());
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
        bll.CancelProductStruApplyByECNMstr(lbl_id.Text, txt_ECNNO.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-10", Session["uID"].ToString(), Session["uName"].ToString());
        BindMstrData();
        this.Alert("已取消！");
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        this.Redirect("productStruApplyUpdateByECN_List.aspx");
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

        string modifylockedbom = ConfigurationSettings.AppSettings["ModifyLockedBom"];
        if (modifylockedbom != "N")
        {
            this.Alert("请走修改流程！");
            return;
        }

        if (lbl_Status.Text == "0")
        {
            if (!this.Security["45305030"].isValid)
            {
                this.Alert("没有操作权限！");
                return;
            }

            int result = bll.ProductStruUpdateByECNImport(lbl_id.Text, Session["uID"].ToString(), Session["plantCode"].ToString());
            if (result == 0)
            {
                BindMstrData();
                QadService.WebService1SoapClient re = new QadService.WebService1SoapClient();
                re.Product_UPDATE_Submit();
                this.Alert("产品结构操作成功！");
                this.Redirect("productStruApply_New.aspx?id=" + lbl_id.Text + "&rt=" + DateTime.Now.ToFileTime().ToString());
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
            else if (result == 4)
            {
                this.Alert("存在相同部件，不能同时提交！");
            }
            else
            {
                this.Alert("产品结构导入操作失败！");
            }
        }
        else
        {
            this.Alert("产品结构导入操作失败！");
        }
    }
    protected void btn_Reject_Click(object sender, EventArgs e)
    {
        if (!this.Security["45305030"].isValid)
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
            bll.ReturnProductStruApplyByECNMstr(lbl_id.Text, txt_ECNNO.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-20", Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
            this.Alert("已驳回！");
        }
    }
    protected void txt_nbr1_Click(object sender, EventArgs e)
    {
        string _no = txt_nbr1.Text;
        ltlAlert.Text = "window.showModalDialog('m5_detail.aspx?no=" + _no + "', window, 'dialogHeight: 800px; dialogWidth: 1100px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
        //this.Redirect("m5_new_Edit.aspx?no=" + _no + "&rt=" + DateTime.Now.ToFileTime().ToString());
    }
}