using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class product_LampUpgradeApply_New : BasePage
{
    private LampUpgrade bll = new LampUpgrade();
    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("45304011", "裸灯升级新增申请");
            this.Security.Register("45304012", "裸灯升级BOM组导入");
            this.Security.Register("45304013", "裸灯升级包装组确认");
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
                trUpload.Visible = false;
                btn_ExportProd.Visible = false;
                btn_ExportProd.Visible = false;
                this.txt_lampCode.ReadOnly = false;
                this.txt_lampCodeNew.ReadOnly = false;
                this.txtRmks.ReadOnly = false;
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
            txt_lampCode.Text = reader["LampCode"].ToString();
            txt_lampCodeNew.Text = reader["LampCodeNew"].ToString();
            txtReason.Text = reader["Reason"].ToString();
            txtRmks.Text = reader["Desc"].ToString();
            lbl_Status.Text = reader["StatusValue"].ToString();
            BindDetailData();
            BindProductData();
        }
        reader.Close();
        //this.trReview.Visible = true;
        //this.trApply.Visible = true;
        //this.txtReason.ReadOnly = true;
        //btn_Save.Visible = true;
        //btn_Submit.Visible = true;
        //btn_Cancel.Visible = true;
        //trReason.Visible = true;
        //trUpload.Visible = true;
        //btn_ExportProd.Visible = true;
        //this.txt_lampCode.ReadOnly = true;
        //this.txt_lampCodeNew.ReadOnly = true;
        //this.txtRmks.ReadOnly = true;

        if (this.Security["45304013"].isValid && lbl_Status.Text == "10")
        {
            this.trReview.Visible = true;
            this.trReason.Visible = true;
            this.txtReason.ReadOnly = false;
            this.trApply.Visible = false;
            btn_ExportProd.Visible = false;
            btn_ExportProd.Visible = true;
            this.trUpload.Visible = true;
            this.txt_lampCode.ReadOnly = true;
            this.txt_lampCodeNew.ReadOnly = true;
            this.txtRmks.ReadOnly = true;
        }
        else if (this.Security["45304012"].isValid && lbl_Status.Text == "20")
        {
            this.trReview.Visible = false;
            this.trReason.Visible = true;
            this.txtReason.ReadOnly = false;
            this.trApply.Visible = true;
            btn_Save.Visible = false;
            btn_Submit.Visible = false;
            btn_Confirm.Visible = true;
            btn_Cancel.Visible = true;
            btn_ExportProd.Visible = false;
            btn_ExportDetail.Visible = true;
            this.trUpload.Visible = false;
            this.txt_lampCode.ReadOnly = true;
            this.txt_lampCodeNew.ReadOnly = true;
            this.txtRmks.ReadOnly = true;
        }
        else
        {
            this.trReview.Visible = false;
            this.trApply.Visible = true;
            this.txtReason.ReadOnly = true;
            if (this.Security["45304011"].isValid)
            {
                switch (lbl_Status.Text)
                {
                    case "0":
                        btn_Save.Visible = true;
                        btn_Submit.Visible = true;
                        btn_Cancel.Visible = true;
                        trReason.Visible = false;
                        btn_Confirm.Visible = false;
                        btn_ExportProd.Visible = true;
                        btn_ExportDetail.Visible = true;
                        trUpload.Visible = true;
                        this.txt_lampCode.ReadOnly = false;
                        this.txt_lampCodeNew.ReadOnly = false;
                        this.txtRmks.ReadOnly = false;
                        break;
                    case "-20":
                        btn_Save.Visible = true;
                        btn_Submit.Visible = true;
                        btn_Cancel.Visible = true;
                        trReason.Visible = true;
                        btn_Confirm.Visible = false;
                        btn_ExportProd.Visible = true;
                        btn_ExportDetail.Visible = true;
                        trUpload.Visible = true;
                        this.txt_lampCode.ReadOnly = false;
                        this.txt_lampCodeNew.ReadOnly = false;
                        this.txtRmks.ReadOnly = false;
                        break;
                    default:
                        btn_Save.Visible = false;
                        btn_Submit.Visible = false;
                        btn_Cancel.Visible = false;
                        trReason.Visible = true;
                        btn_Confirm.Visible = false;
                        btn_ExportProd.Visible = false;
                        btn_ExportDetail.Visible = true;
                        trUpload.Visible = false;
                        this.txt_lampCode.ReadOnly = false;
                        this.txt_lampCodeNew.ReadOnly = true;
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
                btn_Confirm.Visible = false;
                btn_ExportProd.Visible = false;
                btn_ExportDetail.Visible = true;
                trUpload.Visible = false;
                this.txt_lampCode.ReadOnly = false;
                this.txt_lampCodeNew.ReadOnly = true;
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
        gv_product.DataSource = bll.GetLampParent(txt_lampCode.Text.Trim(),lbl_id.Text);
        gv_product.DataBind();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (txt_lampCode.Text.Trim() == "")
        {
            this.Alert("请输入原裸灯号！");
            return;
        }
        else if (bll.FindProdID(txt_lampCode.Text.Trim()).Item1==0)
        {
            this.Alert("请输入正确原裸灯号！");
            return;
        }
        if (txt_lampCodeNew.Text.Trim() != "" && bll.FindProdID(txt_lampCodeNew.Text.Trim()).Item1 == 0)
        {
            this.Alert("请输入正确新裸灯号！");
            return;
        }
        if (lbl_id.Text == "")
        {
            string id = bll.AddProductStruApplyMstr(txt_lampCode.Text.Trim(), txt_lampCodeNew.Text.Trim(), txtRmks.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
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
            bll.UpdateProductStruApplyMstr(lbl_id.Text, txt_lampCode.Text.Trim(), txt_lampCodeNew.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), lbl_Status.Text, Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
        }
    }
    protected void btn_ExportProd_Click(object sender, EventArgs e)
    {
        StringBuilder ids = new StringBuilder();
        foreach (GridViewRow row in gv_product.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            if (chk.Checked)
            { 
                string id=gv_product.DataKeys[row.RowIndex].Values["id"].ToString();
                ids.Append(";").Append(id);
            }
        }
        if (ids.Length > 0)
        {
            ids.Remove(0, 1);
            
        }
        DataTable dt = bll.GetSelectedLampParent(ids.ToString(), txt_lampCodeNew.Text.Trim(),lbl_id.Text);
        string title = "<b>原整灯型号</b>~^<b>新整灯型号</b>~^<b>新整灯描述</b>~^<b>QAD号</b>~^<b>QAD描述1</b>~^<b>QAD描述2</b>~^<b>裸灯型号</b>~^<b>用量</b>~^<b>备注(可空)</b>~^<b>替代品(可空)</b>~^";
        ExportExcel(title, dt, false);
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        
        ImportExcelFile();
        BindMstrData();       
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
               
                string message = "";
                switch (lbl_Status.Text)
                { 
                    case "-20":
                    case "0":                   
                        bBomError = BOMImport(dt, out message);
                        break;
                    case "10":
                        bBomError = PackImport(dt, out message);
                        break;
                    default:
                        bBomError = false;
                        break;
                }
                
                if (bBomError)
                {
                    this.Alert("导入成功！");
                }
                else if (message != "")
                {
                    this.Alert(message);
                }
                else
                {
                    this.Alert("导入失败！");
                }
            }
        }

    }

    private bool BOMImport(DataTable dt,out string message)
    {
        message = "";
        bool bBomError;
        if (dt.Rows.Count > 0)
        {
            string _parentOld, _parent, _child, _qty, _note, store, storeOld, _replace, itemType, itemNum, itemstr, itemNums, qad, prodQad;
            int _parentID, _childID, prodID, partID, semiProdID, ind;
            Tuple<int, string> prodResult, semiProdResult, partResult;
            store = "";
            storeOld = "";
            prodQad = "";
            StringBuilder newCode = new StringBuilder(";");
            if (dt.Columns[0].ColumnName != "原整灯型号" || dt.Columns[1].ColumnName != "新整灯型号" || dt.Columns[2].ColumnName != "新整灯描述" || dt.Columns[3].ColumnName != "QAD号" || dt.Columns[4].ColumnName != "QAD描述1" || dt.Columns[5].ColumnName != "QAD描述2" || dt.Columns[6].ColumnName != "裸灯型号")
            {
                dt.Reset();
                message = "导入文件不是裸灯升级导入模版";
                return false;
            }
            bll.DeleteProductStruApplyDetail(lbl_id.Text);
            bll.DeleteProductStruApply_NewProduct(lbl_id.Text);
            DataRow[] newProduct = dt.Select("新整灯型号<>''");
            DataTable newProducts = null;
            if (newProduct.Length > 0)
            {
                newProducts = newProduct.CopyToDataTable();
            }
            else
            {
                newProducts = dt.Clone();
            }
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
                _parentOld = row[0].ToString().Trim();
                _parent = row[1].ToString().Trim();
                prodQad = row[3].ToString().Trim();
                _child = row[6].ToString().Trim();
                _qty = row[7].ToString().Trim();
                _note = row[8].ToString().Trim();
                _replace = row[9].ToString().Trim();

                if (_parentOld != "")
                {
                    if (_parent == "")
                    {
                        message="文件格式错误 --行 " + (i + 2).ToString() + "，新整灯型号不能为空！";
                        dt.Reset();
                        return false;
                    }
                    if (bll.FindProdID(_parentOld).Item1 == 0)
                    {
                        message="文件格式错误 --行 " + (i + 2).ToString() + "，原整灯型号不存在！";
                        dt.Reset();
                        return false;
                    }
                    storeOld = _parentOld;
                }
                else
                {
                    _parentOld = storeOld;
                }
                if (_parent != "")
                {
                    if (_parentOld == "")
                    {
                        message="文件格式错误 --行 " + (i + 2).ToString() + "，原整灯型号不能为空！";
                        dt.Reset();
                        return false;
                    }
                    if (prodQad.Length < 8)
                    {
                        message="文件格式错误 --行 " + (i + 2).ToString() + "，QAD号不能少于8位！";
                        dt.Reset();
                        return false;
                    }
                    if (newCode.ToString().Contains(";" + _parent + ";"))
                    {
                         message="文件格式错误 --行 " + (i + 2).ToString() + "，新整灯型号重复！";
                        dt.Reset();
                        return false; 
                    }
                    else if (bll.FindProdID(_parent).Item1 > 0)
                    {
                        message="文件格式错误 --行 " + (i + 2).ToString() + "，新整灯型号已存在！";
                        dt.Reset();
                        return false;
                    }
                    newCode.Append(_parent).Append(";");
                    store = _parent;
                }
                else
                {
                    _parent = store;
                }
                if (_child == "")
                {
                    continue;
                    message="文件格式错误 --行 " + (i + 2).ToString() + "，裸灯型号不能为空！";
                    dt.Reset();
                    return false;
                }
                if (_qty == "")
                {
                    message = "文件格式错误 --行 " + (i + 2).ToString() + "，数量不能为空！";
                    dt.Reset();
                    return false;
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
                        message = "文件格式错误 --行 " + (i + 2).ToString() + "，数量不是数值！";
                        dt.Reset();
                        return false;
                    }
                    if (dQty <= 0)
                    {
                        message = "文件格式错误 --行 " + (i + 2).ToString() + "，数量不能小于零！";
                        dt.Reset();
                        return false;
                    }
                    _qty = dQty.ToString();
                }
                if (_note.Length > 255)
                {
                    _note = _note.Substring(0, 255).Trim();
                }

                _parentID = bll.FindProdID(_parentOld).Item1;
                if (_parentID == 0)
                {
                    this.Alert(_parentOld + "在产品库不存在！");
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
                        message = itemNum + "在产品库和部件库中均存在！";
                        dt.Reset();
                        return false;
                    }

                    if (prodID == 0 && partID == 0 && semiProdID == 0)
                    {
                        bBomError = true;
                        message =itemNum + "在库中不存在！";
                        dt.Reset();
                        return false;
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
                        message =_child + "在产品库和部件库中均存在！";
                        dt.Reset();
                        return false;
                    }

                    if (prodID == 0 && partID == 0 && semiProdID == 0)
                    {
                        bBomError = true;
                        message =_child + "在库中不存在！";
                        dt.Reset();
                        return false; 
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

                if (!bBomError)
                {
                    bll.AddProductStruApplyDetail(lbl_id.Text, _parentOld, _parent, _child, itemNums, _childID, qad, _qty, itemType, _note, itemstr, Session["plantCode"].ToString());
                }
            }

            foreach (DataRow row in newProducts.Rows)
            {
                _parentOld = row[0].ToString();
                _parent = row[1].ToString();
                prodQad = row[3].ToString();
                string desc = row[2].ToString();
                string desc1 = row[4].ToString();
                string desc2 = row[5].ToString();

                bll.AddProductStruApply_NewProduct(lbl_id.Text, _parentOld, _parent, desc, prodQad, desc1, desc2);
            }

        }
        return true;
    }

    private bool PackImport(DataTable dt, out string message)
    {
        message = "";
        bool bBomError;
        if (dt.Rows.Count > 0)
        {
            string _parentOld,_parent, _child, _qty, _note, store, storeOld, _replace, itemType, itemNum, itemstr, itemNums, qad;
            int _childID, prodID, partID, semiProdID, ind;
            Tuple<int, string> prodResult, semiProdResult, partResult;
            store = "";
            storeOld = "";
            _parentOld = "";
            StringBuilder newCode = new StringBuilder(";");
            if (dt.Columns[0].ColumnName != "父级型号" || dt.Columns[1].ColumnName != "父级QAD号" || dt.Columns[2].ColumnName != "子级型号" || dt.Columns[3].ColumnName != "子级QAD号" || dt.Columns[4].ColumnName != "子级描述" || dt.Columns[5].ColumnName != "数量" || dt.Columns[6].ColumnName != "类型")
            {
                dt.Reset();
                message = "导入文件不是裸灯升级导入模版";
                return false;
            }
            bll.DeleteProductStruApplyDetail(lbl_id.Text);
            //DataRow[] newProduct = dt.Select("新整灯型号<>''");
            //DataTable newProducts = null;
            //if (newProduct.Length > 0)
            //{
            //    newProducts = newProduct.CopyToDataTable();
            //}
            //else
            //{
            //    newProducts = dt.Clone();
            //}
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bBomError = false;
                itemstr = "";
                itemNums = "";
                ind = 0;
                _childID = 0;
                itemType = "PROD";
                qad = "";
                DataRow row = dt.Rows[i];
                _parent = row[0].ToString().Trim();
                _child = row[2].ToString().Trim();
                _qty = row[5].ToString().Trim();
                _note = row[7].ToString().Trim();
                _replace = row[8].ToString().Trim();

                if (_parent != "")
                {
                    if (!newCode.ToString().Contains(";" + _parent + ";"))
                    {
                        if (bll.FindProdID(_parent).Item1 > 0)
                        {
                            message = "文件格式错误 --行 " + (i + 2).ToString() + "，父级型号已存在！";
                            dt.Reset();
                            return false;
                        }
                        else if (!bll.NewProductExists(lbl_id.Text, _parent, out _parentOld))
                        {
                            message = "文件格式错误 --行 " + (i + 2).ToString() + "，父级型号不是BOM组提交的型号！";
                            dt.Reset();
                            return false;
                        }
                        newCode.Append(_parent).Append(";");
                    }
                    store = _parent;
                }
                else
                {
                    _parent = store;
                }
                if (_child == "")
                {
                    continue;
                    message = "文件格式错误 --行 " + (i + 2).ToString() + "，子级型号不能为空！";
                    dt.Reset();
                    return false;
                }
                if (_qty == "")
                {
                    message = "文件格式错误 --行 " + (i + 2).ToString() + "，数量不能为空！";
                    dt.Reset();
                    return false;
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
                        message = "文件格式错误 --行 " + (i + 2).ToString() + "，数量不是数值！";
                        dt.Reset();
                        return false;
                    }
                    if (dQty <= 0)
                    {
                        message = "文件格式错误 --行 " + (i + 2).ToString() + "，数量不能小于零！";
                        dt.Reset();
                        return false;
                    }
                    _qty = dQty.ToString();
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
                    itemNums += itemNum + ",";

                    prodID = bll.FindProdID(itemNum).Item1;
                    partID = bll.FindPartID(itemNum).Item1;
                    semiProdID = bll.FindSemiProdID(itemNum).Item1;
                    if (prodID > 0 && (partID > 0 || semiProdID > 0))
                    {
                        bBomError = true;
                        message = itemNum + "在产品库和部件库中均存在！";
                        dt.Reset();
                        return false;
                    }

                    if (prodID == 0 && partID == 0 && semiProdID == 0)
                    {
                        bBomError = true;
                        message = itemNum + "在库中不存在！";
                        dt.Reset();
                        return false;
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
                        message = _child + "在产品库和部件库中均存在！";
                        dt.Reset();
                        return false;
                    }

                    if (prodID == 0 && partID == 0 && semiProdID == 0)
                    {
                        bBomError = true;
                        message = _child + "在库中不存在！";
                        dt.Reset();
                        return false;
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

                if (!bBomError)
                {
                    bll.AddProductStruApplyDetail(lbl_id.Text, _parentOld, _parent, _child, itemNums, _childID, qad, _qty, itemType, _note, itemstr, Session["plantCode"].ToString());
                }
            }

        }
        return true;
    }

    protected void btn_Submit_Click(object sender, EventArgs e)
    {
        if (gv_det.Rows.Count > 0)
        {
            bll.UpdateProductStruApplyMstr(lbl_id.Text, txt_lampCode.Text.Trim(), txt_lampCodeNew.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "10", Session["uID"].ToString(), Session["uName"].ToString());
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
        bll.UpdateProductStruApplyMstr(lbl_id.Text, txt_lampCode.Text.Trim(), txt_lampCodeNew.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-10", Session["uID"].ToString(), Session["uName"].ToString());
        BindMstrData();
        this.Alert("已取消！");
    }
    protected void btn_Back_Click(object sender, EventArgs e)
    {
        this.Redirect("LampUpgrade_Mstr.aspx");
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
        string message = "";
        int result = bll.ProductStruImport(lbl_id.Text, Session["uID"].ToString(), Session["plantCode"].ToString(),out message);
        if (result == 1)
        {
            QadService.WebService1SoapClient client = new QadService.WebService1SoapClient();
            client.Product_Add_Submit();
            client.Product_UPDATE_Submit();
            BindMstrData();
            bll.SelectRelationEmail(txt_nbr.Text, txt_prodCode.Text);
            this.Alert("产品结构导入成功！");
        }
        else if (message != "")
        {
            this.Alert(message);
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
            bll.UpdateProductStruApplyMstr(lbl_id.Text, txt_lampCode.Text.Trim(), txt_lampCodeNew.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "-20", Session["uID"].ToString(), Session["uName"].ToString());
            BindMstrData();
            this.Alert("已驳回！");
        }
    }
    protected void btn_Packing_Click(object sender, EventArgs e)
    {
        bll.UpdateProductStruApplyMstr(lbl_id.Text, txt_lampCode.Text.Trim(), txt_lampCodeNew.Text.Trim(), txtRmks.Text.Trim(), txtReason.Text.Trim(), "20", Session["uID"].ToString(), Session["uName"].ToString());
        BindMstrData();
        this.Alert("已确认！");
    }
    protected void btn_ExportDetail_Click(object sender, EventArgs e)
    {
        DataTable dt = bll.GetProductStruApplyDetail(lbl_id.Text);
        string title = "150^<b>父级型号</b>~^110^<b>父级QAD号</b>~^150^<b>子级型号</b>~^110^<b>子级QAD号</b>~^200^<b>子级描述</b>~^<b>数量</b>~^<b>类型</b>~^<b>位号</b>~^<b>备注</b>~^<b>替代品</b>~^<b>子级负责人</b>~^";
        ExportExcel(title, dt, false, 2, "productNumber");
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
}