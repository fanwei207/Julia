//summary
//     Author :   Simon
//Create Date :   May 24 ,2009
//Description :   Maintenance  fix asset, detail and it's increment .
//summary

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
using Microsoft.ApplicationBlocks.Data;
using TCPNEW;
using System.Text.RegularExpressions;
using System.IO;

public partial class FixIncMaintenance : BasePage
{
    adamClass adam = new adamClass();

    protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("110103116", "固定资产编辑");
        }

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Security Checked
            string pagemode = "100103117";
            if (this.Request.QueryString["pageMode"] != null)
            {
                File.Visible = true;
                chkDelImg.Visible = true;
            }

            btnSaveAsset.Text = "保存";
            btnIncrementSave.Text = "保存";
            btnAssetDetail.Text = "保存";


            // Dropdown list initialization
            ListItem item;
            item = new ListItem("--", "-1");

            dropDetailEntity.Items.Add(item);
            dropDetailCenter.Items.Add(item);
            dropDetailStatus.Items.Add(item);
            dropLine.Items.Add(item);

            DataTable dtDropDown = GetDataTcp.GetEntityFixAsset();

            if (dtDropDown.Rows.Count > 0)
            {
                int i;
                for (i = 0; i < dtDropDown.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDown.Rows[i].ItemArray[1].ToString());
                    item.Value = dtDropDown.Rows[i].ItemArray[0].ToString();
                    dropEntity.Items.Add(item);
                    dropIncrementEntity.Items.Add(item);
                }
            }

            if (dtDropDown.Rows.Count > 0)
            {
                int i;
                for (i = 0; i < dtDropDown.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDown.Rows[i].ItemArray[1].ToString());
                    item.Value = dtDropDown.Rows[i].ItemArray[0].ToString();
                    dropDetailEntity.Items.Add(item);
                }
            }

            dtDropDown = null;
            dtDropDown = GetDataTcp.GetTypeFixAsset();

           

            if (dtDropDown.Rows.Count > 0)
            {
                int i;
                for (i = 0; i < dtDropDown.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDown.Rows[i].ItemArray[1].ToString());
                    item.Value = dtDropDown.Rows[i].ItemArray[0].ToString();
                    dropType.Items.Add(item);
                }
                dropType.Items.Insert(0, new ListItem("--", "-1"));

            }
            else
            {
                dropType.Items.Insert(0, new ListItem("--", "-1"));
            }





            dtDropDown = null;
            dtDropDown = GetDataTcp.GetStatusFixAsset();
            if (dtDropDown.Rows.Count > 0)
            {
                int i;
                for (i = 0; i < dtDropDown.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDown.Rows[i].ItemArray[1].ToString());
                    item.Value = dtDropDown.Rows[i].ItemArray[0].ToString();
                    dropDetailStatus.Items.Add(item);
                }
            }

            dtDropDown = null;

            #region deal with the url param Pagemode
            if (pagemode == "100103116")
            {

                if (this.Request.QueryString["AssetNo"] != null)
                {
                    txtAssetNo.Text = Server.UrlDecode(this.Request.QueryString["AssetNo"].ToString());
                    GetAsset(txtAssetNo.Text.Trim(), 0);
                }

                AssetSetup(1);
                AssetEnable(0);
                txtAssetNo.Enabled = true;
                AssetIncrementSetup(0);
            }
            else
            {
                if (this.Request.QueryString["AssetNo"] != null) //Modify Modal
                {
                    AssetIncrementEnable();
                    txtIncrementNo.Text = Server.UrlDecode(this.Request.QueryString["AssetNo"].ToString());
                    DataTable dtAssetIncrement = ProgressDataTcp.CheckAssetIncrementNo(txtIncrementNo.Text).Tables[0];

                    if (dtAssetIncrement.Rows.Count == 0)
                    {
                        txtIncrementName.Focus();
                    }
                    else
                    {
                        txtIncrementName.Text = dtAssetIncrement.Rows[0].ItemArray[0].ToString();
                        dropIncrementEntity.Text = dtAssetIncrement.Rows[0].ItemArray[1].ToString();
                        txtIncrementVoucher.Text = dtAssetIncrement.Rows[0].ItemArray[2].ToString();
                        txtIncrementDate.Text = String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(dtAssetIncrement.Rows[0].ItemArray[3].ToString()));
                        txtIncrementCost.Text = dtAssetIncrement.Rows[0].ItemArray[4].ToString();
                        txtIncrementSupplier.Text = dtAssetIncrement.Rows[0].ItemArray[5].ToString();
                        txtIncrementComment.Text = dtAssetIncrement.Rows[0].ItemArray[6].ToString();
                        txtIncDiscount.Text = dtAssetIncrement.Rows[0]["fixas_inc_discount"].ToString();

                        lblIncrementID.Text = dtAssetIncrement.Rows[0].ItemArray[7].ToString();
                    }
                    txtIncremnetNo_Changed(this, new EventArgs());
                }
                else  //New Modal
                {
                    AssetIncrementNew();
                    File.Visible = true;
                    chkDelImg.Visible = true;
                }
                AssetSetup(0);
                trAssetDetail1.Visible = false;
                trAssetDetail2.Visible = false;
                trAssetDetail3.Visible = false;
                gvAssetDetail.Columns[8].Visible = false;
                gvAssetDetail.Columns[9].Visible = false;
                AssetIncrementSetup(1);
            }
            #endregion

            dropEntity.Items.Insert(0, new ListItem("--", "-1"));

            dropIncrementEntity.Items.Insert(0, new ListItem("--", "-1"));

            //Added By Chenyb
            if (!this.Security["110103116"].isValid)
            {
                btnEditAsset.Visible = false;
                btnDelAsset.Visible = false;
                txtAssetNo.Enabled = Request.QueryString["AssetNo"] == null ? true : false;
            }
            //End Added BY Chenyb
        }

        #region Validation Checks JavaScript Code
        string strVal = @"<script language='javascript'>
         function CheckAsset()
         {
            if(document.getElementById('txtAssetName').value == '')
            {
                    alert('必须填写资产名称.');
                    return false;
            }    
        
            if(document.getElementById('dropType').selectedIndex == 0)
            {
                    alert('必须填写资产类型.');
                    return false;
            } 

            if(document.getElementById('dropEntity').selectedIndex != 0 || document.getElementById('txtVoucher').value != '' || document.getElementById('txtVouchDate').value != '')
            {
                if(document.getElementById('dropEntity').selectedIndex == 0 || document.getElementById('txtVoucher').value == '' || document.getElementById('txtVouchDate').value == '')
                {
                    alert('入账公司、入账凭证、入账日期必须同时填写');
                    return false;
                }

                if(document.getElementById('txtSupplier').value == '')
                {
                    alert('供应商不能为空.');
                    return false;
                }
            }            
            else
            {
                if(document.getElementById('txtReference').value == '')
                {
                    alert('估价依据不能为空.');
                    return false;
                }
            }

            if(document.getElementById('txtCost').value == '')
            {
                    alert('必须填写原值.');
                    return false;
            } 
            else
            {
                  var regx=/^(\d+)(\.\d+)?$/;
                  if(!regx.test(document.getElementById('txtCost').value))
                  {
                             alert('原值必须大于0.');
                             return false;   
                  }
            }

         }</script>";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "validationA", strVal);
        #endregion



        #region Validation Checks JavaScript Code // Asset Increment
        string strVal1 = @"<script language='javascript'>
         function CheckAssetIncrement()
         {
            if(document.getElementById('txtIncrementName').value == '')
            {
                    alert('必须填写名称.');
                    return false;
            }
 
            if(document.getElementById('dropIncrementEntity').selectedIndex > 0)
            {
                if(document.getElementById('txtIncrementVoucher').value == '')
                {
                        alert('必须填写入账凭证.');
                        return false;
                }
 
                if(document.getElementById('txtIncrementDate').value == '')
                {
                        alert('必须填写入账日期.');
                        return false;
                }   
            }               

            if(document.getElementById('txtIncrementCost').value == '')
            {
                    alert('必须填写原值.');
                    return false;
            } 
           else
            {
                  var regx=/^(-?\d+)(\.\d+)?$/;
                  if(!regx.test(document.getElementById('txtIncrementCost').value))
                  {
                             alert('原值必须大于0.');
                             return false;   
                  }
            }            
            
         }</script>";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "validationB", strVal1);
        #endregion

    }
    #region Asset
    protected void AssetSetup(int type)
    {
        if (type == 0)
        {
            trAsset1.Visible = false;
            trAsset2.Visible = false;
            trAsset3.Visible = false;
            trAsset4.Visible = false;
            trAsset5.Visible = false;
            trAsset6.Visible = false;
            trAsset7.Visible = false;
        }
        else
        {
            trAsset1.Visible = true;
            trAsset2.Visible = true;
            trAsset3.Visible = true;
            trAsset4.Visible = true;
            trAsset5.Visible = true;
            trAsset6.Visible = true;
            trAsset7.Visible = true;
        }


    }

    protected void AssetEnable(int type)
    {
        if (type == 0)
        {
            txtAssetNo.Enabled = false;
            txtAssetName.Enabled = false;
            txtAssetSpec.Enabled = false;
            dropType.Enabled = false;
            dropDetailType.Enabled = false;
            dropEntity.Enabled = false;
            txtVoucher.Enabled = false;
            txtVouchDate.Enabled = false;
            txtCost.Enabled = false;
            txtSupplier.Enabled = false;
            txtReference.Enabled = false;
            txtcomment.Enabled = false;
            txtMachineCode.Enabled = false;
            txtDiscount.Enabled = false;

            btnSaveAsset.Enabled = false;

        }
        else
        {
            txtAssetName.Enabled = true;
            txtAssetSpec.Enabled = true;
            dropType.Enabled = true;
            dropDetailType.Enabled = true;
            dropEntity.Enabled = true;
            txtVoucher.Enabled = true;
            txtVouchDate.Enabled = true;
            txtCost.Enabled = true;
            txtSupplier.Enabled = true;
            txtReference.Enabled = true;
            txtcomment.Enabled = true;
            txtMachineCode.Enabled = true;
            txtDiscount.Enabled = true;

            btnSaveAsset.Enabled = true;
        }
    }


    protected void AssetNew()
    {
        txtAssetName.Text = "";
        txtAssetSpec.Text = "";
        dropType.SelectedIndex = 0;
        dropDetailType.SelectedIndex = 0;
        dropEntity.SelectedIndex = 0;

        txtVoucher.Text = "";
        txtVouchDate.Text = "";
        txtCost.Text = "";
        txtSupplier.Text = "";
        txtReference.Text = "";
        txtcomment.Text = "";
        txtMachineCode.Text = "";
        btnSaveAsset.Text = "保存";
        btnEditAsset.Visible = false;
        btnDelAsset.Visible = false;
        btnSaveAsset.Visible = true;

        lblAssetID.Text = "";
    }

    protected void txtAssetNo_Changed(object sender, EventArgs e)
    {
        btnSaveAsset.Enabled = true;
        File.Visible = true;
        chkDelImg.Visible = true;
        try
        {
            if (txtAssetNo.Text.Length < 8)
            {
                ltlAlert.Text = "alert('资产编号必须为8位!');";
            }
            else  //8 byte
            {
                string strEtity = txtAssetNo.Text.Substring(0, 3); // first three must be a charactor
                string strMath = txtAssetNo.Text.Substring(3, 5);  //

                bool bflag = true;
                //资产编号前三位
                for (int i = 0; i < dropEntity.Items.Count; i++)
                {
                    if (dropEntity.Items[i].Text.ToLower() == strEtity.ToLower())
                        bflag = false;
                }

                if (bflag)
                {
                    ltlAlert.Text = "alert('资产编号前三位不正确!');";
                }
                else
                {
                    //资产编号后五位
                    Regex r1 = new Regex("^[0-9]+$");
                    Match m1 = r1.Match(strMath);
                    if (m1.Success)
                    {
                        // 资产编号第四位
                        Regex r2 = new Regex("^[1-9]+$");
                        Match m2 = r2.Match(strMath.Substring(0, 1));
                        if (!m2.Success)
                        {
                            ltlAlert.Text = "alert('资产编号第四位应在1-9之间!');";
                        }
                        else
                        {
                            DataTable dtAsset = ProgressDataTcp.CheckAssetNo(txtAssetNo.Text.Trim());
                            if (dtAsset.Rows.Count > 0)
                            {
                                btnSaveAsset.Enabled = false;
                                ltlAlert.Text = "alert('资产编号已经存在,请重新编号!');";
                                return;
                            }

                            GetAsset(txtAssetNo.Text.Trim(), 0);

                        } //End 资产编号第四位
                    } //
                    else
                    {
                        ltlAlert.Text = "alert('资产编号后五位应为数字!');";
                    }  //End 资产编号后五位

                } //End 资产编号前三位
            }
        }
        catch (Exception ex)
        {
            ;

        }

    }
    public void GetAsset(string AssetNo, int flag)
    {
        DataTable dtAsset = ProgressDataTcp.CheckAssetNo(AssetNo);
        if (dtAsset.Rows.Count == 0)
        {
            AssetEnable(1);
            AssetNew();
            txtAssetName.Focus();
        }
        else
        {
            txtAssetName.Text = dtAsset.Rows[0].ItemArray[0].ToString();
            txtAssetSpec.Text = dtAsset.Rows[0].ItemArray[1].ToString();
            dropType.Text = dtAsset.Rows[0].ItemArray[2].ToString();
            DataTable dtDropDown = (GetDataTcp.selectTypeDetailFixAsset(Convert.ToInt32(dropType.SelectedValue))).Tables[0];
            if (dtDropDown.Rows.Count > 0)
            {
                ListItem item;
                int i;
                for (i = 0; i < dtDropDown.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDown.Rows[i].ItemArray[1].ToString());
                    item.Value = dtDropDown.Rows[i].ItemArray[0].ToString();
                    dropDetailType.Items.Add(item);
                }
                dropDetailType.Items.Insert(0, new ListItem("--", "-1"));
            }
            else
            {
                dropDetailType.Items.Insert(0, new ListItem("--", "-1"));
            }
            dropDetailType.Text = dtAsset.Rows[0].ItemArray[14].ToString();
            try
            {
                dropType.Text = dtAsset.Rows[0].ItemArray[2].ToString();
            }
            catch { }
            try
            {
                dropDetailType.Text = dtAsset.Rows[0].ItemArray[14].ToString();
            }
            catch { }
            try
            {
                dropEntity.Text = dtAsset.Rows[0].ItemArray[3].ToString();
            }
            catch { }

            txtVoucher.Text = dtAsset.Rows[0].ItemArray[4].ToString();

            if (dtAsset.Rows[0].ItemArray[5].ToString() != "")
                txtVouchDate.Text = Convert.ToDateTime(dtAsset.Rows[0].ItemArray[5]).ToShortDateString();
            else
                txtVouchDate.Text = "";

            txtCost.Text = dtAsset.Rows[0].ItemArray[6].ToString();
            txtSupplier.Text = dtAsset.Rows[0].ItemArray[7].ToString();
            txtReference.Text = dtAsset.Rows[0].ItemArray[8].ToString();
            txtcomment.Text = dtAsset.Rows[0].ItemArray[9].ToString();
            txtMachineCode.Text = dtAsset.Rows[0].ItemArray[10].ToString();
            txtDiscount.Text = dtAsset.Rows[0].ItemArray[13].ToString();

            string photoPath = dtAsset.Rows[0]["fixas_photo_path"].ToString();
            if (string.IsNullOrEmpty(photoPath))
            {
                long tick = DateTime.Now.Ticks;
                Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                int result = (int)ran.Next();

                string strImgName = Session["uID"].ToString().Trim() + ".JPG";

                if (dtAsset.Rows[0].ItemArray[11].ToString().Trim() != string.Empty)
                {
                    Byte[] ImgContent = (Byte[])dtAsset.Rows[0].ItemArray[11];
                    int nImgLenght = ImgContent.GetUpperBound(0);

                    string strImgFile = Server.MapPath("../images/fixset/" + strImgName);

                    System.IO.File.Delete(strImgFile);

                    FileStream fs = new FileStream(strImgFile, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(ImgContent, 0, nImgLenght);

                    fs.Close();

                }
                else
                    strImgName = "nopic.PNG";
                imgAsset.ImageUrl = "../images/fixset/" + strImgName + "?" + Convert.ToString(result);
                thickbox.HRef = imgAsset.ImageUrl;
            }
            else
            {
                thickbox.HRef = photoPath;
            }
            lblAssetID.Text = dtAsset.Rows[0].ItemArray[12].ToString();


            if (flag == 0)
            {
                btnSaveAsset.Text = "保存";

                AssetEnable(0);
                AssetDetailSetup(1);
            }
            else
            {
                txtAssetNo.Enabled = false;
                btnEditAsset_Click(this, new EventArgs());
            }
        }
    }

    protected void btnEditAsset_Click(object sender, EventArgs e)
    {
        if (dropEntity.Items[0].Text == "--")
            dropEntity.Items.RemoveAt(0);

        dropEntity.Items.Insert(0, new ListItem("--", "0"));

        AssetEnable(1);

        AssetDetailSetup(0);
        btnBackAsset.Visible = true;
        File.Visible = true;
        chkDelImg.Visible = true;
    }

    protected void btnDelAsset_Click(object sender, EventArgs e)
    {
        try
        {
            int intAssetID = ProgressDataTcp.DelAssetRecord(Convert.ToInt32(lblAssetID.Text), Convert.ToInt32(Session["uID"]));
            if (intAssetID == 0)
            {
                AssetNew();
                AssetDetailSetup(0);
                txtAssetNo.Text = "";
                imgAsset.ImageUrl = string.Empty;
                thickbox.HRef = string.Empty;
                ltlAlert.Text = "alert('记录已删除!');";
            }
            else
            {
                ltlAlert.Text = "alert('删除有误，请重新操作!');";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSaveAsset_Click(object sender, EventArgs e)
    {
        if (txtVouchDate.Text.Trim() != String.Empty)
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txtVouchDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('入账日期格式不正确!正确格式如:1900-01-01!');";
                return;
            }
        }

        //如果折旧日期不为空的话，则必须是大于0的整数
        if (txtDiscount.Text != string.Empty)
        {
            try
            {
                Int32 n = Convert.ToInt32(txtDiscount.Text);

                if (n <= 0)
                {
                    ltlAlert.Text = "alert('折旧年限必须是大于0的整数!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('折旧年限必须是大于0的整数!');";
                return;
            }
        }

        string strImgName = Session["uID"].ToString().Trim() + ".JPG";
        int nFlag = 0;
        string strImgType;
        int nImgLenght;
        Stream ImgStream;
        Byte[] ImgContent;

        string filePath = "";

        nImgLenght = File.PostedFile.ContentLength;

        if (nImgLenght != 0)
        {
            strImgType = File.PostedFile.ContentType;

            if (strImgType.ToUpper() != "IMAGE/PJPEG" && strImgType.ToUpper() != "IMAGE/BMP" && strImgType.ToUpper() != "IMAGE/JPEG")
            {
                ltlAlert.Text = "alert('图片格式只能为JPEG、JPG、BMP、PNG或者GIF!');";
                return;
            }
            string fileName = File.PostedFile.FileName;
            string ExtenName = System.IO.Path.GetExtension(fileName);//获取扩展名
            filePath = "../TecDocs/fixas/" + txtAssetNo.Text.Trim() + "_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ExtenName;
            File.PostedFile.SaveAs(MapPath(filePath));

            //ImgStream = File.PostedFile.InputStream;

            //ImgContent = new Byte[nImgLenght];

            //int nStatu = ImgStream.Read(ImgContent, 0, nImgLenght);


            //ImgStream.Close();
        }
        else
        {
            nImgLenght = 1;
            strImgType = "IMAGE/PJPEG";

            //ImgContent = new Byte[nImgLenght];

            nFlag = 1;
        }

        if (chkDelImg.Checked == true)
            nFlag = 2;

        int intFlag;

        if (lblAssetID.Text.Length == 0)
        {

            intFlag = ProgressDataTcp.SaveAndModifyAsset(txtAssetNo.Text, txtAssetName.Text, txtAssetSpec.Text, Convert.ToInt32(dropType.SelectedValue),
                                      txtCost.Text, Convert.ToInt32(dropEntity.SelectedValue), txtMachineCode.Text, txtVoucher.Text, txtVouchDate.Text,
                                      txtSupplier.Text, txtcomment.Text, txtReference.Text, null, 0, Convert.ToInt32(Session["uID"]),
                                      Convert.ToInt32(Session["uID"]), strImgType, nFlag, txtDiscount.Text, Convert.ToInt32(dropDetailType.SelectedValue),"",filePath);
        }
        else
        {
            intFlag = ProgressDataTcp.SaveAndModifyAsset(txtAssetNo.Text, txtAssetName.Text, txtAssetSpec.Text, Convert.ToInt32(dropType.SelectedValue),
                                      txtCost.Text, Convert.ToInt32(dropEntity.SelectedValue), txtMachineCode.Text, txtVoucher.Text, txtVouchDate.Text,
                                      txtSupplier.Text, txtcomment.Text, txtReference.Text, null, Convert.ToInt32(lblAssetID.Text), 0,
                                      Convert.ToInt32(Session["uID"]), strImgType, nFlag, txtDiscount.Text, Convert.ToInt32(dropDetailType.SelectedValue), "", filePath);
        }

        if (intFlag > 0)
        {
            AssetEnable(0);
            //Detail
            lblAssetID.Text = intFlag.ToString();
            AssetDetailSetup(1);
            if (txtAssetNo.Enabled)
                ltlAlert.Text = "alert('保存成功!');";
            else
                ltlAlert.Text = "alert('保存成功!');";

            //long tick = DateTime.Now.Ticks;
            //Random ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
            //int result = (int)ran.Next();

            //DataTable dtAsset = ProgressDataTcp.CheckAssetNo(txtAssetNo.Text.Trim());

            //if (dtAsset.Rows[0].ItemArray[11].ToString().Trim() != string.Empty)
            //{
            //    ImgContent = (Byte[])dtAsset.Rows[0].ItemArray[11];
            //    nImgLenght = ImgContent.GetUpperBound(0);

            //    string strImgFile = Server.MapPath("../images/fixset/" + strImgName);

            //    FileStream fs = new FileStream(strImgFile, FileMode.OpenOrCreate, FileAccess.Write);
            //    fs.Write(ImgContent, 0, nImgLenght);
            //    fs.Close();
            //}
            //else
            //    strImgName = "nopic.PNG";
            //imgAsset.ImageUrl = "../images/fixset/" + strImgName + "?" + Convert.ToString(result);
            imgAsset.ImageUrl = filePath;
            thickbox.HRef = imgAsset.ImageUrl;

            File.Visible = false;
            chkDelImg.Checked = false;
            chkDelImg.Visible = false;
        }
        else
        {
            ltlAlert.Text = "alert('操作出错，请重新再试!');";
        }
    }

    protected void btnBackAsset_Click(object sender, EventArgs e)
    {
        if (this.Request.QueryString["AssetNo"] != null)
        {
            this.Response.Redirect("FixAssetView.aspx");
        }
        else
        {
            if (btnDelAsset.Visible)
            {
                AssetNew();
                AssetEnable(0);
                AssetDetailSetup(0);
                txtAssetNo.Enabled = true;
                txtAssetNo.Text = "";
                imgAsset.ImageUrl = string.Empty;
                thickbox.HRef = string.Empty;
            }
            else
            {
                AssetEnable(0);
                AssetDetailSetup(1);
            }
        }
    }

    #endregion


    #region Asset Increment
    protected void AssetIncrementSetup(int itype)
    {
        if (itype == 0)
        {
            trAssetIncrement1.Visible = false;
            trAssetIncrement2.Visible = false;
            trAssetIncrement3.Visible = false;
            trAssetIncrement4.Visible = false;
            trAssetIncrement5.Visible = false;
        }
        else
        {
            trAssetIncrement1.Visible = true;
            trAssetIncrement2.Visible = true;
            trAssetIncrement3.Visible = true;
            trAssetIncrement4.Visible = true;
            trAssetIncrement5.Visible = true;
        }
    }


    protected void AssetIncrementEnable()
    {

        btnIncrementSave.Text = "保存";
        btnIncrementSave.Visible = true;
        btnIncrementDel.Visible = true;
        btnIncrementBak.Visible = true;

        txtIncrementNo.Enabled = false;
        txtIncrementName.Enabled = true;
        dropIncrementEntity.Enabled = true;
        txtIncrementVoucher.Enabled = true;
        txtIncrementDate.Enabled = true;
        txtIncrementCost.Enabled = true;
        txtIncrementSupplier.Enabled = true;
        txtIncrementComment.Enabled = true;
    }

    protected void AssetIncrementNew()
    {
        txtIncrementNo.Text = string.Empty;
        txtIncrementName.Text = string.Empty;
        dropIncrementEntity.SelectedIndex = 0;
        txtIncrementVoucher.Text = string.Empty;
        txtIncrementDate.Text = string.Empty;
        txtIncrementCost.Text = string.Empty;
        txtIncrementSupplier.Text = string.Empty;
        txtIncrementComment.Text = string.Empty;
        txtAssetName1.Text = string.Empty;
        txtType1.Text = string.Empty;
        txtDetailType1.Text = string.Empty;
        btnIncrementSave.Text = "新增";
        btnIncrementSave.Visible = true;
        btnIncrementDel.Visible = false;
        btnIncrementBak.Visible = false;

        lblIncrementID.Text = string.Empty;
    }

    protected void txtIncremnetNo_Changed(object sender, EventArgs e)
    {
        btnIncrementSave.Enabled = true;

        try
        {
            if (txtIncrementNo.Text.Trim().Length < 10)
            {
                ltlAlert.Text = "alert('编号必须为10位!');";
                txtIncrementNo.Text = string.Empty;

                return;
            }
            DataTable dtAssetIncrement = ProgressDataTcp.CheckAssetNo(txtIncrementNo.Text.Trim().Substring(0, 8));
            if (dtAssetIncrement.Rows.Count == 0)
            {
                ltlAlert.Text = "alert('编号前8位输入不正确!');";
                txtIncrementNo.Text = string.Empty;

                return;
            }
            else
            {
                lblAssetID.Text = dtAssetIncrement.Rows[0].ItemArray[12].ToString();
            }

            if (txtIncrementNo.Text.Trim().Substring(8, 1) != "+")
            {
                ltlAlert.Text = "alert('编号第9位应输为+!');";

                txtIncrementNo.Text = string.Empty;

                return;
            }

            Regex r1 = new Regex("^[0-9a-zA-Z]+$");
            Match m1 = r1.Match(txtIncrementNo.Text.Trim().Substring(9, 1));
            if (!m1.Success)
            {
                ltlAlert.Text = "alert('编号第10位应输为数字!');";
                return;
            }
            dtAssetIncrement = null;
            dtAssetIncrement = ProgressDataTcp.CheckAssetIncrementNo(txtIncrementNo.Text).Tables[0];

            DataTable dtAsset = ProgressDataTcp.CheckAssetIncrementNo(txtIncrementNo.Text).Tables[1];
            DataTable dtAsset1 = ProgressDataTcp.CheckAssetIncrementNo(txtIncrementNo.Text).Tables[2];
            txtAssetName1.Text = dtAsset.Rows[0][0].ToString().Trim();
            txtType1.Text = dtAsset.Rows[0][1].ToString().Trim();
            if (dtAsset1.Rows.Count > 0)
            {
                txtDetailType1.Text = dtAsset1.Rows[0][1].ToString().Trim();
            }
            if (this.Request.QueryString["AssetNo"] != null)
            {
                return;
            }

            if (dtAssetIncrement.Rows.Count > 0)
            {
                btnIncrementSave.Enabled = false;
                ltlAlert.Text = "alert('编号已经存在,请重新编号!');";
                return;
            }
        }
        catch (Exception ex)
        {
            ;
        }
    }

    protected void btnIncrementDel_Click(object sender, EventArgs e)
    {
        try
        {
            int intAssetIncrementID = ProgressDataTcp.DelAssetIncrementRecord(Convert.ToInt32(lblIncrementID.Text), Convert.ToInt32(Session["uID"]));
            if (intAssetIncrementID == 0)
            {
                AssetIncrementNew();
                ltlAlert.Text = "alert('记录已删除!');";
            }
            else
            {
                ltlAlert.Text = "alert('删除有误，请重新操作!');";
            }
        }
        catch
        {

        }
    }

    protected void btnIncrementSave_Click(object sender, EventArgs e)
    {
        if (txtIncrementNo.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('编号 不能为空!');";
            return;
        }

        if (txtIncrementName.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('名称 不能为空!');";
            return;
        }

        if (txtIncrementCost.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('原值 不能为空!');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtIncrementCost.Text.Trim());
                //if (_dc < 0)
                //{
                //    ltlAlert.Text = "alert('原值 只能大于0!');";
                //    return;
                //}
            }
            catch
            {
                ltlAlert.Text = "alert('原值 只能是数字!');";
                return;
            }
        }

        //如果折旧日期不为空的话，则必须是大于0的整数
        if (txtIncDiscount.Text != string.Empty)
        {
            try
            {
                Int32 n = Convert.ToInt32(txtIncDiscount.Text);

                if (n <= 0)
                {
                    ltlAlert.Text = "alert('折旧年限必须是大于0的整数!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('折旧年限必须是大于0的整数!');";
                return;
            }
        }

        int intFlag;

        if (lblIncrementID.Text.Length == 0)
        {
            intFlag = ProgressDataTcp.SaveAndModifyAssetIncrement(Convert.ToInt32(lblAssetID.Text), txtIncrementNo.Text, Convert.ToInt32(dropIncrementEntity.SelectedValue),
                                                                  txtIncrementVoucher.Text, txtIncrementDate.Text, Convert.ToDecimal(txtIncrementCost.Text), txtIncrementSupplier.Text,
                                                                   txtIncrementComment.Text, txtIncrementName.Text, 0, Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["uID"]), txtIncDiscount.Text);
        }
        else
        {
            intFlag = ProgressDataTcp.SaveAndModifyAssetIncrement(Convert.ToInt32(lblAssetID.Text), txtIncrementNo.Text, Convert.ToInt32(dropIncrementEntity.SelectedValue),
                                                                  txtIncrementVoucher.Text, txtIncrementDate.Text, Convert.ToDecimal(txtIncrementCost.Text), txtIncrementSupplier.Text,
                                                                   txtIncrementComment.Text, txtIncrementName.Text, Convert.ToInt32(lblIncrementID.Text), Convert.ToInt32(Session["uID"]), Convert.ToInt32(Session["uID"]), txtIncDiscount.Text);
        }

        if (intFlag > 0)
        {
            lblIncrementID.Text = intFlag.ToString();

            if (btnIncrementSave.Text.Trim() == "新增")
            {
                AssetIncrementEnable();
                //AssetIncrementNew();
            }

            Page.RegisterStartupScript("AssetSave", "<script>alert('保存成功!');Form1.txtAssetNo.focus();</script>");
        }
        else
        {
            Page.RegisterStartupScript("AssetSave", "<script>alert('操作出错，请重新再试!');</script>");
        }
    }

    protected void btnIncrementBak_Click(object sender, EventArgs e)
    {

        Response.Redirect("FixAssetView.aspx");
    }

    #endregion


    protected void AssetDetailSetup(int dtype)
    {
        if (dtype == 0)
        {
            txtStartDate.Text = "";
            dropDetailEntity.SelectedIndex = 0;
            dropDetailCenter.Items.Clear();
            dropLine.Items.Clear();
            dropDetailStatus.SelectedIndex = 0;
            txtDetailSite.Text = "";
            txtDetailComment.Text = "";
            txtResponsibler.Text = "";

            txtStartDate.Enabled = false;
            dropDetailEntity.Enabled = false;
            dropDetailCenter.Enabled = false;
            dropLine.Enabled = false;
            dropDetailStatus.Enabled = false;
            dropLine.Enabled = false;
            txtDetailSite.Enabled = false;
            txtDetailComment.Enabled = false;
            txtResponsibler.Enabled = false;

            btnAssetDetail.Enabled = false;

            btnEditAsset.Visible = false;
            btnDelAsset.Visible = false;
            btnBackAsset.Visible = false;
            btnSaveAsset.Visible = true;
        }
        else
        {
            txtStartDate.Enabled = true;
            dropDetailEntity.Enabled = true;
            dropDetailCenter.Enabled = true;
            dropLine.Enabled = true;
            dropDetailStatus.Enabled = true;
            dropLine.Enabled = true;
            txtDetailSite.Enabled = true;
            txtDetailComment.Enabled = true;
            txtResponsibler.Enabled = true;

            btnAssetDetail.Enabled = true;

            //dispay edit and delete button in Asset
            btnEditAsset.Visible = true;
            btnDelAsset.Visible = true;
            btnBackAsset.Visible = true;
            btnSaveAsset.Visible = false;

        }

    }

    protected void btnAssetDetail_Click(object sender, EventArgs e)
    {
        if (txtStartDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('开始日期 不能为空!');";
            return;
        }
        else
        {
            try
            {
                DateTime dd = Convert.ToDateTime(txtStartDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('开始日期 格式不正确!正确格式如:1900-01-01!');";
                return;
            }
        }

        if (dropDetailEntity.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择 所在公司!');";
            return;
        }

        if (dropDetailCenter.Items.Count < 1 || dropDetailCenter.SelectedItem.Text == "--")
        {
            ltlAlert.Text = "alert('请选择 成本中心!');";
            return;
        }

        if (dropDetailStatus.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('请选择 状态!');";
            return;
        }
        string str = dropLine.SelectedValue;

        int intFlag = ProgressDataTcp.SaveAndModifiyAssetDetail(lblfixid.Text.Trim(), txtStartDate.Text, Convert.ToInt32(dropDetailEntity.SelectedValue), Convert.ToInt32(dropDetailCenter.SelectedValue),
                                                                Convert.ToInt32(dropDetailStatus.SelectedValue), txtDetailSite.Text, txtDetailComment.Text, txtResponsibler.Text,
                                                                Convert.ToInt32(lblAssetID.Text), Convert.ToInt32(Session["uID"]), dropLine.SelectedValue.Trim());

        if (intFlag > 0)
        {
            btnAssetDetail.Text = "保存";
            lblfixid.Text = "0";
            txtStartDate.Text = "";
            dropDetailEntity.SelectedIndex = 0;
            dropDetailCenter.Items.Clear();
            dropLine.Items.Clear();
            dropLine.Items.Clear();
            dropDetailStatus.SelectedIndex = 0;
            txtDetailSite.Text = "";
            txtDetailComment.Text = "";
            txtResponsibler.Text = "";
            ltlAlert.Text = "alert('保存成功!');";
            gvAssetDetail.DataBind();
        }
        else
        {
            if (intFlag == -1)
                ltlAlert.Text = "alert('开始日期不能大于入账日期!');";
            else if (intFlag == 0)
                ltlAlert.Text = "alert('开始日期不能小于已有的最大开始日期!');";
        }

        if (!this.Security["110103116"].isValid)
        {
            btnEditAsset.Visible = false;
            btnDelAsset.Visible = false;
        }
    }

    protected void dropType_changed(object sender, EventArgs e)
    {
        if (dropType.SelectedIndex >= 0)
        {
            DataTable typeDetail = GetDataTcp.selectTypeDetailFixAsset(Convert.ToInt32(dropType.SelectedValue)).Tables[0];
            txtDiscount.Text = GetDataTcp.GetDiscountByTypeID(Convert.ToInt32(dropType.SelectedValue));//2015-10-10新增，将指定的时间带出
          
            if (typeDetail.Rows.Count > 0)
            {
                int i;
                ListItem item;
                dropDetailType.Items.Clear();

                for (i = 0; i < typeDetail.Rows.Count; i++)
                {
                    item = new ListItem(typeDetail.Rows[i].ItemArray[1].ToString());
                    item.Value = typeDetail.Rows[i].ItemArray[0].ToString();
                    dropDetailType.Items.Add(item);
                }
                dropDetailType.Items.Insert(0, new ListItem("--", "-1"));
            }
            else
            {
                dropDetailType.Items.Clear();
                dropDetailType.Items.Insert(0, new ListItem("--", "-1"));
            }
        }
    }

    protected void dropDetailEntity_changed(object sender, EventArgs e)
    {
        if (dropDetailEntity.SelectedIndex > 0)
        {
            DataTable dtDEntity = GetDataTcp.GetCostCenterFixAsset(Convert.ToInt32(dropDetailEntity.SelectedValue));
            if (dtDEntity.Rows.Count > 0)
            {
                int i;
                ListItem item;
                dropDetailCenter.Items.Clear();

                for (i = 0; i < dtDEntity.Rows.Count; i++)
                {
                    item = new ListItem("(" + dtDEntity.Rows[i].ItemArray[1].ToString() + ")" + dtDEntity.Rows[i].ItemArray[2].ToString());
                    item.Value = dtDEntity.Rows[i].ItemArray[0].ToString();
                    dropDetailCenter.Items.Add(item);
                }
                dropDetailCenter.Items.Insert(0, new ListItem("--", "--"));
            }
            else
            {
                dropDetailCenter.Items.Clear();
                dropDetailCenter.Items.Insert(0, new ListItem("--", ""));
            }


            DataTable dtDLine = GetDataTcp.GetLineFixAsset(Convert.ToInt32(dropDetailEntity.SelectedValue));
            if (dtDLine.Rows.Count > 0)
            {
                int i;
                ListItem item;
                dropLine.Items.Clear();

                for (i = 0; i < dtDLine.Rows.Count; i++)
                {
                    item = new ListItem("(" + dtDLine.Rows[i].ItemArray[1].ToString() + ")" + dtDLine.Rows[i].ItemArray[2].ToString());
                    item.Value = dtDLine.Rows[i].ItemArray[1].ToString();
                    dropLine.Items.Add(item);
                }
                dropLine.Items.Insert(0, new ListItem("--", "--"));
            }
            else
            {
                dropLine.Items.Clear();
                dropLine.Items.Insert(0, new ListItem("--", ""));
            }
        }
    }

    protected void gvAssetDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt") //edit
        {
            btnAssetDetail.Text = "保存修改";

            AssetEnable(0);

            AssetDetailSetup(1);

            if (!this.Security["110103116"].isValid)
            {
                btnEditAsset.Visible = false;
                btnDelAsset.Visible = false;
            }

            int index = int.Parse(e.CommandArgument.ToString());
            txtStartDate.Text = gvAssetDetail.Rows[index].Cells[0].Text.Trim();
            txtDetailSite.Text = gvAssetDetail.Rows[index].Cells[5].Text.Trim();
            txtResponsibler.Text = gvAssetDetail.Rows[index].Cells[6].Text.Trim();
            txtDetailComment.Text = gvAssetDetail.Rows[index].Cells[7].Text.Trim();

            lblfixid.Text = gvAssetDetail.DataKeys[index].Value.ToString().Trim();

            if (txtResponsibler.Text.Trim() == "&nbsp;")
            {
                txtResponsibler.Text = "";
            }

            if (txtDetailSite.Text.Trim() == "&nbsp;")
            {
                txtDetailSite.Text = "";
            }

            if (txtDetailComment.Text.Trim() == "&nbsp;")
            {
                txtDetailComment.Text = "";
            }

            dropDetailEntity.SelectedIndex = -1;

            dropDetailEntity.Items.FindByText(gvAssetDetail.Rows[index].Cells[1].Text.Trim()).Selected = true;

            dropDetailEntity_changed(this, new EventArgs());

            dropDetailCenter.SelectedIndex = -1;

            string center = "--";
            if (gvAssetDetail.Rows[index].Cells[2].Text.Trim() != "&nbsp;")
            {
                center = gvAssetDetail.Rows[index].Cells[2].Text.Trim();
            }
            dropDetailCenter.Items.FindByText(center).Selected = true;

            dropLine.SelectedIndex = -1;
            string line = "--";
            if (gvAssetDetail.Rows[index].Cells[3].Text.Trim() != "&nbsp;")
            {
                line = gvAssetDetail.Rows[index].Cells[3].Text.Trim();
            }
            dropLine.Items.FindByText(line).Selected = true;



            dropDetailStatus.SelectedIndex = -1;

            dropDetailStatus.Items.FindByText(gvAssetDetail.Rows[index].Cells[4].Text.Trim()).Selected = true;
        }
    }
    protected void gvAssetDetail_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
    }


}
