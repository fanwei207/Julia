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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;

public partial class pur_ResultCreate : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    PurResult result = new PurResult();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddl_mounth.SelectedIndex = DateTime.Now.Month - 1;
            BindVendor();
            BindVDate();
            GridViewBind("");
            //this.gv.Columns[0].Visible = false;
            if(this.Security["120000066"].isValid)
            {
                btn_technolobyUpdate.Visible = true;
                btn_technolobyUpdate.Attributes.Add("onclick", "return confirm('更新技术部未维护考评数据" + " 是否继续？');");
            }
        }
    }
    private void BindVendor()
    {
        try
        {
            //string strName = "sp_purresult_selectPro";
            //SqlParameter[] param = new SqlParameter[1];
            //param[0] = new SqlParameter("@proid", ddl_checkpro.SelectedValue);

            //DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            //ddl_vendor.DataSource = result.GetVerdorList(ddl_vendor.SelectedValue, ddl_checkpro.SelectedValue);//ds;
            //ddl_vendor.DataBind();
            //ddl_vendor.Items.Insert(0, "--请选择--");
            //ddl_vendor.SelectedIndex = 0;
        }
        catch
        {
            this.Alert("获取");
        }
    }
    private void BindVDate()
    {
        ddl_year.Items.Clear();
        try
        {
            ddl_year.Items.Insert(0, DateTime.Now.AddYears(-1).Year.ToString());
            ddl_year.Items.Insert(1, DateTime.Now.Year.ToString());
            ddl_year.SelectedIndex = 1;
        }
        catch
        {
            this.Alert("获取");
        }
    }
    private void GridViewBind(string vendor) 
    {
        try
        {
            gv.DataSource = result.CerateVendorResult(vendor, ddl_year.SelectedItem.Text, ddl_mounth.SelectedItem.Text); ;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    private bool checkNoteIsExist(string type)
    {
        try
        {
            string strName = "sp_vc_checkVendCompCateIsExist";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@name", type);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        GridViewBind("");
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string strName = "sp_vc_deleteVendCompCate";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", gv.DataKeys[e.RowIndex].Values["vcc_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[3].Value))
            {
                this.Alert("删除成功！");
            }
            else
            {
                this.Alert("删除失败！");
            }
        }
        catch
        {
            this.Alert("删除失败！请联系管理员！"); ;
        }

        GridViewBind(txt_vendor.Text);
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        GridViewBind("");
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string _id = gv.DataKeys[e.RowIndex].Values["pur_id"].ToString();
        TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].FindControl("txDesc");
        if (txDesc.Text.Length == 0)
        {
            this.Alert("罚款科目不能为空！");
            return;
        }

        if (checkNoteIsExist(txDesc.Text))
        {
            ltlAlert.Text = "alert('罚款科目不能重复!');";
            return;
        }
        try
        {
            int valuescore = 0;
            string vendor = txt_vendor.Text;//ddl_vendor.SelectedValue;
            string vendorname = "";//ddl_vendor.SelectedItem.ToString();
            string proid = gv.DataKeys[e.RowIndex].Values["pur_proid"].ToString();
            string proname = gv.Rows[e.RowIndex].Cells[3].Text.ToString();//gv.DataKeys[e.RowIndex].Values["pur_proname"].ToString();
            string typeid = gv.DataKeys[e.RowIndex].Values["pur_typeid"].ToString();
            string typename = gv.Rows[e.RowIndex].Cells[4].Text.ToString();
            string mounth = ddl_mounth.SelectedValue;
            string year = ddl_year.SelectedValue;

            if (string.IsNullOrEmpty(txt_vendor.Text))
            {
                this.Alert("供应商不能为空!");
                return;
            }
            if (!result.CheckVendExists(vendor))
            {
                this.Alert("供应商不存在!");
                return;
            }

            //int result1 = result.PurResultValueCheckExists(vendor, vendorname, proid, proname, typeid, typename, valuescore, mounth, Session["uID"].ToString(), Session["uName"].ToString());
            //if (result1 != 0)
            //{
            //    this.Alert("已存在相同记录!");
            //    return;
            //}
            try
            {
                valuescore = Convert.ToInt32(txDesc.Text);
            }
            catch
            {
                this.Alert("判定值必须为数字!");
                return;
            }
            if (valuescore > 100 || valuescore < 0)
            {
                this.Alert("判定值需在0~100范围内!");
                return;
            }
            //if (!result.SavePurResultValueByList(vendor, vendorname, proid, proname, typeid, typename, valuescore, 0, year, mounth, Session["uID"].ToString(), Session["uName"].ToString()))
            //{
            //    this.Alert("记录保存失败!");
            //    return;
            //}
            this.Alert("记录保存成功!");
        }
        catch
        {
            this.Alert("记录保存失败!");
            return;
        }
        gv.EditIndex = -1;
        GridViewBind("");
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string vendor = "";
        //if (string.IsNullOrEmpty(txt_vendor.Text))
        //{
        //    this.Alert("供应商不能为空!");
        //    return;
        //}
        vendor = txt_vendor.Text;
        if (!result.CheckVendExists(vendor) && !string.IsNullOrEmpty(txt_vendor.Text))
        {
            this.Alert("供应商不存在!");
            return;
        }
        GridViewBind(vendor);
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind(txt_vendor.Text);
    }
    protected void ddl_vendor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txt_vendor_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        string vendor = "";
        //if (string.IsNullOrEmpty(txt_vendor.Text))
        //{
        //    this.Alert("供应商不能为空!");
        //    return;
        //}
        vendor = txt_vendor.Text;
        if (!result.CheckVendExists(vendor) && !string.IsNullOrEmpty(txt_vendor.Text))
        {
            this.Alert("供应商不存在!");
            return;
        }
        DataTable dt = result.CerateVendorResultExport(vendor, ddl_year.SelectedItem.Text, ddl_mounth.SelectedItem.Text); ;
        //string title = "<b>供应商代码</b>~^<b>供应商名称</b>~^<b>批次合格率</b>~^<b>反馈及时性</b>~^<b>8D报告结案率</b>~^<b>交货及时率</b>~^<b>订单确认率</b>~^<b>是否寄售</b>~^<b>超额次数</b>~^<b>账期</b>~^<b>价格水平</b>~^<b>技术资料的提供</b>~^<b>样品交付合格率</b>~^<b>样品交付及时率</b>~^<b>批次合格率ID</b>~^<b>反馈及时性</b>~^<b>8D报告结案率</b>~^<b>交货及时率</b>~^<b>订单确认率</b>~^<b>是否寄售</b>~^<b>超额次数</b>~^<b>账期</b>~^<b>价格水平</b>~^<b>技术资料的提供</b>~^<b>样品交付合格率</b>~^<b>样品交付及时率</b>~^<b>年份</b>~^<b>月份</b>~^<b>创建者</b>~^";
        //string title = "<b>供应商代码</b>~^<b>供应商名称</b>~^<b>年份</b>~^<b>月份</b>~^<b>状态</b>~^<b>批次合格率</b>~^<b>反馈及时性</b>~^<b>8D报告结案率</b>~^<b>交货及时率</b>~^<b>订单确认率</b>~^<b>是否寄售</b>~^<b>超额次数</b>~^<b>账期</b>~^<b>价格水平</b>~^<b>技术资料的提供</b>~^<b>样品交付合格率</b>~^<b>样品交付及时率</b>~^<b>创建者</b>~^";
        string title = "<b>供应商代码</b>~^<b>供应商名称</b>~^<b>供货类别</b>~^<b>年份</b>~^<b>月份</b>~^<b>状态</b>~^<b>批次合格率</b>~^<b>反馈及时性</b>~^<b>退件次数</b>~^<b>异常重复发生次数</b>~^<b>客诉次数</b>~^<b>产线抱怨次数</b>~^<b>交货及时率</b>~^<b>订单确认率</b>~^<b>账期</b>~^<b>报价及时性</b>~^<b>价格水平</b>~^<b>技术资料的提供</b>~^<b>样品交付合格率</b>~^<b>样品交付及时率</b>~^<b>备注</b>~^<b>创建者</b>~^";

        ExportExcel(title, dt, false);
    }
    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        if (!result.deletePurResultValueByListTemp(Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("删除临时表数据失败!");
            return;
        } 
        ImportExcelFile();
        GridViewBind("");
        this.Alert("导入成功！");
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
                bool qualityPower, purchasePower, technologyPower;
                qualityPower = false;
                purchasePower = false;
                technologyPower = false;
                if (this.Security["120000062"].isValid)
                {
                    qualityPower = true;
                }
                if (this.Security["120000063"].isValid)
                {
                    purchasePower = true;
                }
                if (this.Security["120000064"].isValid)
                {
                    technologyPower = true;
                }
                if (!qualityPower && !purchasePower && !technologyPower)
                {
                    ltlAlert.Text = "alert('没有操作的权限,请先申请权限!');";
                    return;
                }

                if (dt.Rows.Count > 0)
                {
                    string _vendor, _vendorname, _consignRate, _overRate, _priceRate, _docRate, _year, _mounth, _remark, _createname;
                    int _fackBackRate, _8dRate, _finRate, _8dReturnRate, _8dUnusualRate, _8dCustComplainRate, _8dLineComplainRate;
                    decimal _lotPassRate, _deliveryRate, _orderCheckRate, _samplePassRate, _sampleTimeRate;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Columns[0].ColumnName != "供应商代码" || dt.Columns[1].ColumnName != "供应商名称" //|| dt.Columns[2].ColumnName != "ID"
                            || dt.Columns[2].ColumnName != "供货类别" || dt.Columns[3].ColumnName != "年份" || dt.Columns[4].ColumnName != "月份"
                            || dt.Columns[5].ColumnName != "状态" || dt.Columns[6].ColumnName != "批次合格率" || dt.Columns[7].ColumnName != "反馈及时性"
                            //|| dt.Columns[7].ColumnName != "8D报告结案率" 
                            || dt.Columns[8].ColumnName != "退件次数" || dt.Columns[9].ColumnName != "异常重复发生次数" || dt.Columns[10].ColumnName != "客诉次数"
                            || dt.Columns[11].ColumnName != "产线抱怨次数" || dt.Columns[12].ColumnName != "交货及时率" || dt.Columns[13].ColumnName != "订单确认率"
                            //|| dt.Columns[14].ColumnName != "是否寄售" 
                            || dt.Columns[14].ColumnName != "账期" || dt.Columns[15].ColumnName != "报价及时性"
                            || dt.Columns[16].ColumnName != "价格水平" || dt.Columns[17].ColumnName != "技术资料的提供" || dt.Columns[18].ColumnName != "样品交付合格率"
                            || dt.Columns[19].ColumnName != "样品交付及时率" || dt.Columns[20].ColumnName != "备注")
                        {
                            dt.Reset();
                            this.Alert("导入文件不是产品结构申请导入模版");
                            return;
                        }
                        if (i >= 0)
                        {
                            bBomError = false;
                            DataRow row = dt.Rows[i];
                            _vendor = row[0].ToString().Trim();
                            _vendorname = row[1].ToString().Trim();
                            _year = row[3].ToString().Trim();
                            _mounth = row[4].ToString().Trim();

                            //批次合格路
                            _lotPassRate = 0;
                            //if (purchasePower)
                            //{
                            //    try
                            //    {
                            //        _lotPassRate = 0;//Convert.ToDecimal(row[6].ToString().Trim()) * 100;
                            //        if (_lotPassRate > 100 || _lotPassRate < 0)
                            //        {
                            //            this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，批次合格率范围在1~100！");
                            //            dt.Reset();
                            //            return;
                            //        }
                            //    }
                            //    catch
                            //    {
                            //        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，批次合格率必须为数字！");
                            //        dt.Reset();
                            //        return;
                            //    }
                            //}
                            //else
                            //{
                            //    _lotPassRate = 0;
                            //}

                            if (qualityPower)
                            {
                                //反馈及时率
                                try
                                {
                                    _fackBackRate = Convert.ToInt32(row[7].ToString().Trim());
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，反馈及时性必须为数字！");
                                    dt.Reset();
                                    return;
                                }
                                //_fackBackRate = row[3].ToString().Trim();
                                //if (string.IsNullOrEmpty(_fackBackRate))
                                //{
                                //    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，反馈及时性不能为空！");
                                //    dt.Reset();
                                //    return;
                                //}        

                                _8dRate = 0;

                                //8D退件次数
                                try
                                {
                                    _8dReturnRate = Convert.ToInt32(row[8].ToString().Trim());
                                    if (_8dReturnRate > 100 || _8dReturnRate < 0)
                                    {
                                        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，退件次数率范围在1~100！");
                                        dt.Reset();
                                        return;
                                    }
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，退件次数率必须为数字！");
                                    dt.Reset();
                                    return;
                                }
                                //8D异常重复发生次数
                                try
                                {
                                    _8dUnusualRate = Convert.ToInt32(row[9].ToString().Trim());
                                    if (_8dUnusualRate > 100 || _8dUnusualRate < 0)
                                    {
                                        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，异常重复发生次数率范围在1~100！");
                                        dt.Reset();
                                        return;
                                    }
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，异常重复发生次数率必须为数字！");
                                    dt.Reset();
                                    return;
                                }
                                //8D客诉次数
                                try
                                {
                                    _8dCustComplainRate = Convert.ToInt32(row[10].ToString().Trim());
                                    if (_8dCustComplainRate > 100 || _8dCustComplainRate < 0)
                                    {
                                        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，客诉次数率范围在1~100！");
                                        dt.Reset();
                                        return;
                                    }
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，客诉次数率必须为数字！");
                                    dt.Reset();
                                    return;
                                }
                                //8D产线抱怨次数
                                try
                                {
                                    _8dLineComplainRate = Convert.ToInt32(row[11].ToString().Trim());
                                    if (_8dLineComplainRate > 100 || _8dLineComplainRate < 0)
                                    {
                                        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，产线抱怨次数率范围在1~100！");
                                        dt.Reset();
                                        return;
                                    }
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，产线抱怨次数率必须为数字！");
                                    dt.Reset();
                                    return;
                                }
                            }
                            else
                            {
                                _fackBackRate = 0;
                                _8dRate = 0;
                                _8dReturnRate = 0;
                                _8dUnusualRate = 0;
                                _8dCustComplainRate = 0;
                                _8dLineComplainRate = 0;
                            }

                            if (purchasePower)
                            {
                                //交货及时率
                                _deliveryRate = 0;
                                //try
                                //{
                                //    _deliveryRate = 0;//Convert.ToDecimal(row[12].ToString().Trim()) * 100;
                                //    if (_deliveryRate > 100 || _deliveryRate < 0)
                                //    {
                                //        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，交货及时率范围在1~100！");
                                //        dt.Reset();
                                //        return;
                                //    }
                                //}
                                //catch
                                //{
                                //    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，交货及时率必须为数字！");
                                //    dt.Reset();
                                //    return;
                                //}
                                //订单确认率
                                _orderCheckRate = 0;
                                //try
                                //{
                                //    _orderCheckRate = 0;//Convert.ToDecimal(row[13].ToString().Trim()) * 100;
                                //    if (_orderCheckRate > 100 || _orderCheckRate < 0)
                                //    {
                                //        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，订单确认率范围在1~100！");
                                //        dt.Reset();
                                //        return;
                                //    }
                                //}
                                //catch
                                //{
                                //    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，订单确认率必须为数字！");
                                //    dt.Reset();
                                //    return;
                                //}
                                //是否寄售
                                _consignRate = null;//row[14].ToString().Trim();
                                //if (string.IsNullOrEmpty(_consignRate))
                                //{
                                //    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，是否寄售不能为空！");
                                //    dt.Reset();
                                //    return;
                                //}

                                //try
                                //{
                                //    _overRate = Convert.ToInt32(row[11].ToString().Trim());
                                //}
                                //catch
                                //{
                                //    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，报价及时性！");
                                //    dt.Reset();
                                //    return;
                                //}
                                //账期
                                try
                                {
                                    _finRate = Convert.ToInt32(row[14].ToString().Trim());
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，账期必须为数字！");
                                    dt.Reset();
                                    return;
                                }
                                //报价及时性
                                _overRate = row[15].ToString().Trim();
                                if (string.IsNullOrEmpty(_overRate))
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，报价及时性不能为空！");
                                    dt.Reset();
                                    return;
                                }
                                //价格水平
                                _priceRate = row[16].ToString().Trim();
                                if (string.IsNullOrEmpty(_priceRate))
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，价格水平不能为空！");
                                    dt.Reset();
                                    return;
                                }
                            }
                            else
                            {
                                _deliveryRate = 0;
                                _orderCheckRate = 0;
                                _consignRate = "";
                                _finRate = 0;
                                _overRate = "";
                                _priceRate = "";
                            }

                            if (technologyPower)
                            {
                                //技术资料的提供
                                _docRate = row[17].ToString().Trim();
                                if (string.IsNullOrEmpty(_docRate))
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，技术资料的提供不能为空！");
                                    dt.Reset();
                                    return;
                                }
                                //样品交付合格率
                                try
                                {
                                    _samplePassRate = Convert.ToDecimal(row[18].ToString().Trim()) * 100;
                                    if (_samplePassRate > 100 || _samplePassRate < 0)
                                    {
                                        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，样品交付合格率率范围在1~100！");
                                        dt.Reset();
                                        return;
                                    }
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，样品交付合格率必须为数字！");
                                    dt.Reset();
                                    return;
                                }
                                //样品交付及时率
                                try
                                {
                                    _sampleTimeRate = Convert.ToDecimal(row[19].ToString().Trim()) * 100;
                                    if (_sampleTimeRate > 100 || _sampleTimeRate < 0)
                                    {
                                        this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，样品交付及时率范围在1~100！");
                                        dt.Reset();
                                        return;
                                    }
                                }
                                catch
                                {
                                    this.Alert("文件格式错误 --行 " + (i + 2).ToString() + "，订样品交付及时率必须为数字！");
                                    dt.Reset();
                                    return;
                                }
                            }
                            else
                            {
                                _docRate = "";
                                _samplePassRate = 0;
                                _sampleTimeRate = 0;
                            }
                            _remark = row[20].ToString().Trim();
                            _createname = Session["uName"].ToString().Trim(); //row[22].ToString().Trim();

                            string re = result.SavePurResultValueCheckValueName(_vendor, _vendorname, _lotPassRate, _fackBackRate, _8dRate, _8dReturnRate, _8dUnusualRate, _8dCustComplainRate, _8dLineComplainRate, _deliveryRate, _orderCheckRate
                                                , _consignRate, _overRate, _finRate, _priceRate, _docRate, _samplePassRate, _sampleTimeRate, _year, _mounth, Session["uID"].ToString(), Session["uName"].ToString(), qualityPower, purchasePower, technologyPower);
                            if (!string.IsNullOrEmpty(re))
                            {
                                this.Alert("--行 " + (i + 2).ToString() + "维护值不符合要求：" + re + "!");
                                return;
                            }  

                            if (!bBomError)
                            {
                                if (!result.SavePurResultValueByList(_vendor, _vendorname, _lotPassRate, _fackBackRate, _8dRate, _8dReturnRate, _8dUnusualRate, _8dCustComplainRate, _8dLineComplainRate, _deliveryRate, _orderCheckRate
                                                    , _consignRate, _overRate, _finRate, _priceRate, _docRate, _samplePassRate, _sampleTimeRate, _year, _mounth, Session["uID"].ToString(), Session["uName"].ToString(), _remark))
                                {
                                    this.Alert("记录保存临时表失败!");
                                    return;
                                }                           
                            }

                            if (!result.SavePurResultValueByListByTemp(_vendor, _vendorname, _lotPassRate, _fackBackRate, _8dRate, _8dReturnRate, _8dUnusualRate, _8dCustComplainRate, _8dLineComplainRate, _deliveryRate, _orderCheckRate
                                    , _consignRate, _overRate, _finRate, _priceRate, _docRate, _samplePassRate, _sampleTimeRate, _year, _mounth, Session["uID"].ToString(), Session["uName"].ToString(), _remark, qualityPower, purchasePower, technologyPower))
                            {
                                this.Alert("记录保存失败!");
                                return;
                            }   
                        }
                    }
                }
            }
        }
    }
    protected void btn_technolobyUpdate_Click(object sender, EventArgs e)
    {
        if (!result.TechnolobyUpdate(Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("记录保存失败!");
            return;
        }   
    }
}
