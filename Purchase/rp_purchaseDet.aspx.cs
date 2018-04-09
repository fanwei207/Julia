using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using Purchase;

public partial class Purchase_rp_purchaseDet : BasePage
{
    adamClass adam = new adamClass();
    RPPurchase pur = new RPPurchase();
    public string _fpath
    {
        get
        {
            return ViewState["fpath"].ToString();
        }
        set
        {
            ViewState["fpath"] = value;
        }
    }
    public string _fname
    {
        get
        {
            return ViewState["fname"].ToString();
        }
        set
        {
            ViewState["fname"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Um基本单位
        //UmTemp采购单位
        //PtUm实际采购单位
        if (!IsPostBack)
        {
            BindBusDept();
            hidPlant.Value = Session["plantCode"].ToString();
            //BindUmTemp();
            if(Request["type"].ToString() == "new")
            {
                labNo.Text = SelectPurchaseNo();
                txtRejection.Visible = false;
            }
            else if (Request["type"].ToString() == "det")
            {
                labNo.Text = Request["no"].ToString();
                ddlBusDept.SelectedValue = Request["deptid"];
                txtRejection.Text = bindRejection();
            }
            BindGV();
            BindFileList();
        }
    }
    private string bindRejection()
    {
        string no = Request["no"].ToString();

        string sqlstr = "sp_rp_selectRejectionByNo";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@no", no);


        return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
    }

    private void BindFileList()
    {
        DataTable dt = getPurchaseFileList();
        gvFile.DataSource = dt;
        gvFile.DataBind();
    }
    public void BindUmTemp()
    {
        ddlUmTemp.Items.Clear();
        ddlUmTemp.Items.Add("其他");
        string sql = "";
        sql += "select distinct pc_um,pc_um from tcpc0..pc_mstr where isnull(pc_start,'1900-1-1') <= GETDATE() and dateadd(day,1,isnull(pc_expire,'2999-1-1')) > getdate()";

        if (txtQAD.Text.Trim().ToString() != string.Empty)
        {
            sql += " And pc_part = '" + txtQAD.Text.Trim().ToString() + "'";
        }
        if (txtVend.Text.Trim().ToString() != string.Empty)
        {
            sql += " And pc_list = '" + txtVend.Text.Trim().ToString() + "'";
        }
        SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, sql);
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                ddlUmTemp.Items.Add(new ListItem(reader["pc_um"].ToString(), reader["pc_um"].ToString()));
            }
            reader.Close();
        }
        //txtChangeUm.Text = BindChangeUm();
    }
    private bool SelectMstrRecordQty()
    {
        string str = "sp_rp_SelectMstrRecordQty";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@uID", Session["uID"].ToString());
        param[1] = new SqlParameter("@busDeptID", ddlBusDept.SelectedValue.ToString());
        param[2] = new SqlParameter("@Plant", hidPlant.Value.ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private DataTable getPurchaseFileList()
    {
        string sql = string.Empty;
        if (Request["type"].ToString() == "new")
        {
            sql = "Select * From rp_purchaseFileListTemp Where Convert(varchar(10),CreateDate,120) = Convert(varchar(10),getdate(),120) and CreateBy = '" + Session["uID"].ToString() + "'";
        }
        else if (Request["type"].ToString() == "det")
        {
            sql = "Select * From rp_purchaseFileList Where MID = '" + Request["ID"] + "'";
        }

        //string sql = "Select * From rp_purchaseFileList Where rp_No = '" + labNo.Text + "'";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sql).Tables[0];
    }
    private string SelectPurchaseNo()
    {
        string str = "sp_rp_SelectPurchaseNo";
        SqlParameter param = new SqlParameter("@no", labNo.Text);

        return Convert.ToString(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure,str));
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        if (Request["type"].ToString() == "new")
        {
            //5
            bool havePrice = false;
            bool enterPrice = false;
         

            if (hidPrice.Value == "0.00000")
            {
                enterPrice = false;
            }
            else
            {
                enterPrice = true;
            }

            if (gv.Rows.Count >= 1)
            {
                foreach (GridViewRow gr in gv.Rows)
                {
                    if (gr.Cells[5].Text != "0.00000")
                    {
                        havePrice = true;
                    }
                }
            }
            else
            {
                havePrice = enterPrice;
            }
            //2016年9月7号杨洋要求取消此检验，暂注释
            //if (pur.SelectIsExistsQADFromDet("new", "appdept", "14558EFF-9B2E-45DC-9099-131CEFEAE731", "14558EFF-9B2E-45DC-9099-131CEFEAE731", txtQAD.Text.Trim(), Session["uID"].ToString()))
            //{
            //    ltlAlert.Text = "alert('物料已存在，请重新填写'); ";
            //    return;
            //}

            if (havePrice == enterPrice)
            {
                if (!InsertPurchaseDetTemp())
                {
                    ltlAlert.Text = "alert('物料添加失败，请联系管理员'); ";
                    return;
                }
                else
                {
                    BindGV();
                    txtQAD.Text = string.Empty;
                    txtVend.Text = string.Empty;
                    txtVendName.Text = string.Empty;
                    txtQtyTemp.Text = string.Empty;
                    txtUses.Text = string.Empty;
                    txtFormat.Text = string.Empty;
                    txtDesc.Text = string.Empty;
                    txtUmTemp.Text = string.Empty;
                    hidQty.Value = "";
                    hidUm.Value = "";
                }
            }
            else if (enterPrice)
            {
                ltlAlert.Text = "alert('本申请存在价格为0的申请，请输入没有价格的申请！如果要申请有价格的物料，请再新建一个申请只申请有价格的物料'); ";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('本申请存在有价格的申请，请输入价格的申请！如果要申请没有价格的物料，请再新建一个申请只申请没有价格的物料'); ";
                return;
            }
        }
        else if (Request["type"].ToString() == "det")
        {
            if (pur.SelectIsExistsQADFromDet("det", "appdept", Request["ID"].ToString(), Request["ID"].ToString(), txtQAD.Text.Trim(), Session["uID"].ToString()))
            {
                ltlAlert.Text = "alert('物料已存在，请重新填写'); ";
                return;
            }
            if (!InsertPurchaseDetial())
            {
                ltlAlert.Text = "alert('物料添加失败，请联系管理员'); ";
                return;
            }
            else
            {
                BindGV();
                txtQAD.Text = string.Empty;
                txtVend.Text = string.Empty;
                txtVendName.Text = string.Empty;
                txtQtyTemp.Text = string.Empty;
                txtUses.Text = string.Empty;
                txtFormat.Text = string.Empty;
                txtDesc.Text = string.Empty;
                txtUmTemp.Text = string.Empty;
                hidQty.Value = "";
                hidUm.Value = "";
            }
        }
    }
    private void BindBusDept()
    {
        DataTable dt = selectBusDept();
        ddlBusDept.Items.Clear();
        ddlBusDept.DataSource = dt;
        ddlBusDept.DataBind();
        ddlBusDept.Items.Insert(0, new ListItem("--业务部门--", "0"));        
    }
    private DataTable selectBusDept()
    {
        string sql = "Select departmentid,departmentname From RP_department Where plantcode = " + Session["plantCode"].ToString();

        return SqlHelper.ExecuteDataset(adam.dsn0(),  CommandType.Text, sql).Tables[0]; ;
    }
    private bool InsertPurchaseDetTemp()
    {
        if (txtPrice.Text == string.Empty)
        {
            txtPrice.Text = "0.00000";
        }
        if (txtUmTemp.Text == string.Empty)
        {
            txtUmTemp.Text = "EA";
        }
        string str = "sp_rp_InsertPurchaseDetTemp";
        SqlParameter[] param = new SqlParameter[20];
        param[0] = new SqlParameter("@qad", txtQAD.Text);
        param[1] = new SqlParameter("@desc", txtDesc.Text);
        param[2] = new SqlParameter("@format", txtFormat.Text);
        param[3] = new SqlParameter("@price", hidPrice.Value.ToString());        
        param[4] = new SqlParameter("@qadDesc1", hidDesc1.Value.ToString());
        param[5] = new SqlParameter("@qadDesc2", hidDesc2.Value.ToString());
        param[6] = new SqlParameter("@qty", hidQty.Value.ToString());
        param[7] = new SqlParameter("@um", hidUm.Value.ToString());
        param[8] = new SqlParameter("@uses", txtUses.Text);
        param[9] = new SqlParameter("@vend", txtVend.Text);
        param[10] = new SqlParameter("@vendName", txtVendName.Text);
        param[11] = new SqlParameter("@no", labNo.Text);
        param[12] = new SqlParameter("@uID", Session["uID"].ToString());
        param[13] = new SqlParameter("@uName", Session["uName"].ToString());
        param[14] = new SqlParameter("@Ptum", hidPtUm.Value.ToString());
        param[15] = new SqlParameter("@umTemp", txtUmTemp.Text);
        param[16] = new SqlParameter("@qtyTemp", txtQtyTemp.Text);
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool InsertPurchaseDetial()
    {
        string str = "sp_rp_InsertPurchaseDetial";
        SqlParameter[] param = new SqlParameter[20];
        param[0] = new SqlParameter("@qad", txtQAD.Text);
        param[1] = new SqlParameter("@desc", txtDesc.Text);
        param[2] = new SqlParameter("@format", txtFormat.Text);
        param[3] = new SqlParameter("@price", hidPrice.Value.ToString());
        param[4] = new SqlParameter("@qadDesc1", hidDesc1.Value.ToString());
        param[5] = new SqlParameter("@qadDesc2", hidDesc2.Value.ToString());
        param[6] = new SqlParameter("@qty", hidQty.Value.ToString());
        param[7] = new SqlParameter("@um", txtUmTemp.Text);
        param[8] = new SqlParameter("@uses", txtUses.Text);
        param[9] = new SqlParameter("@vend", txtVend.Text);
        param[10] = new SqlParameter("@vendName", txtVendName.Text);
        param[11] = new SqlParameter("@no", labNo.Text);
        param[12] = new SqlParameter("@mid", Request["ID"].ToString());
        param[13] = new SqlParameter("@Ptum", hidPtUm.Value.ToString());
        param[14] = new SqlParameter("@umTemp", txtUmTemp.Text.Trim());
        param[15] = new SqlParameter("@qtyTemp", txtQtyTemp.Text.Trim());
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private void BindGV()
    {
        DataTable dt = SelectPurchaseDet();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private DataTable SelectPurchaseDet()
    {
        string str = string.Empty;
        SqlParameter[] param = new SqlParameter[2];
        if (Request["type"].ToString() == "new")
        {
            str = "sp_rp_SelectPurchaseDetTemp";
            param[0] = new SqlParameter("@uID", Session["uID"].ToString());
        }
        else if (Request["type"].ToString() == "det")
        {
            str = "sp_rp_SelectPurchaseDet";
            param[0] = new SqlParameter("@ID", Request["ID"]);
        }

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (Request["type"].ToString() == "new")
        { 
             bool haveZero = false;
            bool havePrice = false;


            if (gv.Rows.Count >= 1)
            {
                foreach (GridViewRow gr in gv.Rows)
                {
                    if (gr.Cells[5].Text != "0.00000")
                    {
                        havePrice = true;
                    }
                    else
                    {
                        haveZero = true;
                    }
                }
            }


            if (havePrice != haveZero )
            {
                if (!SelectMstrRecordQty())
                {
                    ltlAlert.Text = "alert('您所在部门已申请过三条采购记录，请对采买有一个计划！'); ";
                    
                }
                
               labNo.Text = SelectPurchaseNo();


               int insertFlag = InsertPurchaseMstr("submit");

                if (insertFlag == 0)
                {
                    ltlAlert.Text = "alert('采购申请单提交失败，请联系管理员'); ";
                    return;
                }
                else if( insertFlag == 1)
                {
                    Response.Redirect("../Purchase/rp_purchaseMstrList.aspx");
                }
                else
                {
                    ltlAlert.Text = "alert('请您转换公司到您的所在公司提出申请！'); ";
                    return;
                }

          
               
            }
            else  if (!selectExistsDetRecord(labNo.Text, Request["type"].ToString()))
            {
                ltlAlert.Text = "alert('没有明细不允许提交'); ";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('同时存在有价格和没有价格的申请，请拆分再提交'); ";
                return;
            }
        }
        else if (Request["type"].ToString() == "det")
        {
            if (!SelectMstrRecordQty())
            {
                ltlAlert.Text = "alert('您所在部门已申请过三条采购记录，请对采买有一个计划！'); ";
                
            }
            labNo.Text = Request["no"].ToString();
             

            int updateFlag = UpdatePurchaseMstr("submit");
            if (updateFlag ==0)
            {
                ltlAlert.Text = "alert('采购申请单提交失败，请联系管理员'); ";
                return;
            }
            else if (updateFlag == 1)
            {
                Response.Redirect("../Purchase/rp_purchaseMstrList.aspx");
            }
            else
            {
                ltlAlert.Text = "alert('请您转换公司到您的所在公司修改申请！'); ";
                return;
            }
                 
            if (!selectExistsDetRecord(labNo.Text, Request["type"].ToString()))
            {
                ltlAlert.Text = "alert('没有明细不允许提交'); ";
                return;
            }
            else
            {
                ltlAlert.Text = "alert('同时存在有价格和没有价格的申请，请拆分再提交'); ";
                return;
            }
        }
       
    }
    private bool selectExistsDetRecord(string no, string type)
    {
        string str = "sp_rp_selectExistsDetRecord";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@type", type);
        param[2] = new SqlParameter("@uid", Session["uID"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private int InsertPurchaseMstr(string type)
    {
        if (Request["type"].ToString() == "new")
        {
            labNo.Text = SelectPurchaseNo();
        }
        else if (Request["type"].ToString() == "det")
        {
            labNo.Text = Request["no"].ToString();
        }
        string str = "sp_rp_InsertPurchaseMstr";
        SqlParameter[] param = new SqlParameter[15];
        param[0] = new SqlParameter("@no", labNo.Text);
        param[1] = new SqlParameter("@dept", ddlBusDept.SelectedValue.ToString());
        param[2] = new SqlParameter("@deptName", ddlBusDept.SelectedItem.ToString());
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@uName", Session["uName"].ToString());
        param[5] = new SqlParameter("@Plant", hidPlant.Value.ToString());
        param[6] = new SqlParameter("@type", type);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private int UpdatePurchaseMstr(string type)
    {
        string str = "sp_rp_UpdatePurchaseMstr";
        SqlParameter[] param = new SqlParameter[15];
        param[0] = new SqlParameter("@no", labNo.Text);
        param[1] = new SqlParameter("@dept", ddlBusDept.SelectedValue.ToString());
        param[2] = new SqlParameter("@deptName", ddlBusDept.SelectedItem.ToString());
        param[3] = new SqlParameter("@uID", Session["uID"].ToString());
        param[4] = new SqlParameter("@uName", Session["uName"].ToString());
        param[5] = new SqlParameter("@ID", Request["ID"].ToString());
        param[6] = new SqlParameter("@type", type);
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Redirect("../Purchase/rp_purchaseMstrList.aspx");
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (filename.Value.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('上传文件不能为空！')";
            return;
        }
        if (UpLoadFile(filename))
        {
            if (Request["type"].ToString() == "new")
            {
                if (!InsertPurchaseFileTemp(labNo.Text, _fname, _fpath, Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    ltlAlert.Text = "alert('保存文件信息失败，请联系管理员！')";
                    return;
                }
                else
                {
                    BindFileList();
                }
            }
            else if (Request["type"].ToString() == "new")
            {
                if (!InsertPurchaseFile(Request["no"], Request["ID"], _fname, _fpath, Session["uID"].ToString(), Session["uName"].ToString()))
                {
                    ltlAlert.Text = "alert('保存文件信息失败，请联系管理员！')";
                    return;
                }
                else
                {
                    BindFileList();
                }
            }
        }
        else
        {
            ltlAlert.Text = "alert('上传文件失败，请联系管理员！')";
            return;
        }
    }

    private bool InsertPurchaseFileTemp(string labNo, string _fname, string _fpath, string _uID, string _uName)
    {
        string str = "sp_rp_InsertPurchaseFileTemp";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@labNo", labNo);
        param[1] = new SqlParameter("@fname", _fname);
        param[2] = new SqlParameter("@fpath", _fpath);
        param[3] = new SqlParameter("@uID", _uID);
        param[4] = new SqlParameter("@uName", _uName);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    private bool InsertPurchaseFile(string labNo, string MID, string _fname, string _fpath, string _uID, string _uName)
    {
        string str = "sp_rp_InsertPurchaseFileFormat";
        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@labNo", labNo);
        param[1] = new SqlParameter("@fname", _fname);
        param[2] = new SqlParameter("@fpath", _fpath);
        param[3] = new SqlParameter("@uID", _uID);
        param[4] = new SqlParameter("@uName", _uName);
        param[5] = new SqlParameter("@MID", MID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param));
    }
    /// <summary>
    /// 上传文件
    /// </summary>
    protected bool UpLoadFile(HtmlInputFile fileID)
    {
        string _uID = Convert.ToString(Session["uID"]);
        string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);

        string strUserFileName = fileID.PostedFile.FileName;//是获取文件的路径，即FileUpload控件文本框中的所有内容，
        int flag = strUserFileName.LastIndexOf("\\");
        string _fileName = strUserFileName.Substring(flag + 1);

        string attachExtension = Path.GetExtension(fileID.PostedFile.FileName);
        string _newFileName = DateTime.Now.ToFileTime().ToString() + attachExtension;

        string catPath = @"/TecDocs/RP/";
        string strCatFolder = Server.MapPath(catPath);
        if (!Directory.Exists(strCatFolder))
        {
            Directory.CreateDirectory(strCatFolder);
        }

        string SaveFileName = System.IO.Path.Combine(strCatFolder, _newFileName);//合并两个路径为上传到服务器上的全路径
        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('fail to delete folder！')";

                return false;
            }
        }
        try
        {
            fileID.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file')";

            return false;
        }

        _fpath = catPath + _newFileName;
        _fname = _fileName;
        return true;
    }
    protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvFile.DataKeys[intRow].Values["rp_filePath"].ToString().Trim();
            string fileName = gvFile.DataKeys[intRow].Values["rp_fileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string ID = gv.DataKeys[e.RowIndex].Values["ID"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@ID", ID);
        sqlParam[1] = new SqlParameter("@uID", Session["uID"].ToString());
        sqlParam[2] = new SqlParameter("@uName", Session["uName"].ToString());
        string str = string.Empty;
        if (Request["type"].ToString() == "new")
        {
            str = "sp_rp_DeletePurchaseDetTemp";
        }
        else if (Request["type"].ToString() == "det")
        {
            str = "sp_rp_DeletePurchaseDet";
        }
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);

        BindGV();
    }
    protected void gvFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strDocID = gvFile.DataKeys[e.RowIndex].Values["ID"].ToString();
        string strPath = gvFile.DataKeys[e.RowIndex].Values["rp_filePath"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("@ID", strDocID);
        string str = string.Empty;
        if (Request["type"].ToString() == "new")
        {
            str = "sp_rp_DeleteFileTemp";
        }
        else if (Request["type"].ToString() == "det")
        {
            str = "sp_rp_DeleteFile";
        }
        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);
        try
        {
            File.Delete(strPath);
        }
        catch
        {
            ;
        }

        BindFileList();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!SelectMstrRecordQty())
        {
            ltlAlert.Text = "alert('您所在部门已申请过三条采购记录，请对采买有一个计划！'); ";
            
        }
        if (Request["type"].ToString() == "new")
        {
            int insertFlag = InsertPurchaseMstr("save");

                if (insertFlag == 0)
                {
                    ltlAlert.Text = "alert('采购申请单提交失败，请联系管理员'); ";
                    return;
                }
                else if( insertFlag == 1)
                {
                    Response.Redirect("../Purchase/rp_purchaseMstrList.aspx");
                }
                else
                {
                    ltlAlert.Text = "alert('请您转换公司到您的所在公司提出申请！'); ";
                    return;
                }
        }
        else if (Request["type"].ToString() == "det")
        {
            int updateFlag = UpdatePurchaseMstr("save");
            if (updateFlag == 0)
            {
                ltlAlert.Text = "alert('采购申请单提交失败，请联系管理员'); ";
                return;
            }
            else if (updateFlag == 1)
            {
                Response.Redirect("../Purchase/rp_purchaseMstrList.aspx");
            }
            else
            {
                ltlAlert.Text = "alert('请您转换公司到您的所在公司修改申请！'); ";
                return;
            }
        }
    }
    protected void ddlUmTemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUmTemp.SelectedValue.ToString() == "其他")
        {
            txtUmTemp.Text = string.Empty;
            txtUmTemp.Enabled = true;
            //txtChangeUm.Text = BindChangeUm();
        }
        else
        {
            txtUmTemp.Text = ddlUmTemp.SelectedItem.ToString();
            //txtChangeUm.Text = BindChangeUm();
            txtUmTemp.Enabled = false;
        }
    }
    private string BindChangeUm()
    {
        string result;
        string sql = "Select cast(cast(um_conv/um_alt_qty as float) as varchar) + um_um ";
        sql += " FROM QAD_Data.dbo.um_mstr";
        sql += " Where 1 = 1 And";
        if(txtUmTemp.Text.Trim() != string.Empty)
        {
            sql += " um_alt_um = '" + txtUmTemp.Text.Trim() + "' And";
        }
        switch(Session["pLantcode"].ToString())
        {
            case "1":
                sql += " um_domain = 'SZX' ";
                break;
            case "2":
                sql += " um_domain = 'ZQL' ";
                break;
            case "5":
                sql += " um_domain = 'HQL' ";
                break;
            case "8":
                sql += " um_domain = 'YQL' ";
                break;
        }
        if (txtQAD.Text.Trim() != string.Empty)
        {
            sql += " And um_part = '" + txtQAD.Text.Trim().ToString() + "'";
        }
        try
        {
            result = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql).ToString();
        }
        catch
        {
            result = string.Empty;
        }
        return result;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //BindUmTemp();
    }
    protected void txtVend_TextChanged(object sender, EventArgs e)
    {

    }
}