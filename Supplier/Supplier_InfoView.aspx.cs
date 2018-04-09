using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;

public partial class Supplier_InfoView : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbsupplierNo.Text = Request["supplierNo"].ToString();
            hidSupplierNo.Value = Request["supplierNo"].ToString();
            hidSupplierID.Value = Request["supplierID"].ToString();
            bind();

            if (Request.QueryString["pc_mstr"].Equals("1"))
            {
                btnReturn.Visible = false;
            }


        }
    }



    private DataTable selectFileType()
    {
        string sqlstr = "sp_supplier_selectFileType";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr).Tables[0];
    }

    private void bind()
    {
        
        SqlDataReader sdr = selectInfoBySupplierNo(hidSupplierNo.Value, hidSupplierID.Value);
        while (sdr.Read())
        {
            labPlant.Text = sdr["domain"].ToString();
            labDate.Text = sdr["applyDate"].ToString();
            labAPPUserName.Text = sdr["applyName"].ToString();
            labAPPDeptName.Text = sdr["applyDepartment"].ToString();
            labChineseSupplierName.Text = sdr["supplierName"].ToString();
            labEnglishSupplierName.Text = sdr["supplierEnglish"].ToString();
            labChineseSupplierAddress.Text = sdr["suppChineseAddress"].ToString();
            labEnglishSupplierAddress.Text = sdr["suppEnglishAddress"].ToString();
            labBusinesstype.Text = sdr["SupplieType"].ToString();
            labBroadHeading.Text = sdr["BroadHeading"].ToString();
            labSubDivision.Text = sdr["SubDivision"].ToString();
            //txtSupplierfax.Text = sdr[""].ToString();
            labFactoryInspection.Text = sdr["FactoryInspection"].ToString();
            lbTerms.Text = sdr["vd_cr_terms"].ToString();
            lbCurr.Text = sdr["vd_curr"].ToString();
            lbTax.Text = sdr["ad_taxc"].ToString();
            txtRemark.Text = sdr["Remark"].ToString();
        }
        sdr.Dispose();
        sdr.Close();

        bindLinkManGV();
        bindFAgv();
        bindSignedGV();
        bindGvFormal();

        hidTabIndex.Value = "0";

    }

    private void bindSignedGV()
    {
        gvFile.DataSource = selectSignedInfoGVByNo();
        gvFile.DataBind();
        hidTabIndex.Value = "2";
    }

    private DataTable selectSignedInfoGVByNo()
    {
        string sqlstr = "sp_supplier_selectSignedInfoGVByNo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@uID",Session["uID"].ToString())
            ,new SqlParameter("@supplierNo",hidSupplierNo.Value)
            , new SqlParameter("@supplierID",hidSupplierID.Value)
        
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];

    }

    private void bindFAgv()
    {
        

        FAgv.DataSource = selectsupplierInfoFQByNo();
        FAgv.DataBind();
        hidTabIndex.Value = "1";
    }

    private DataTable selectsupplierInfoFQByNo()
    {
        string sqlstr = "sp_supplier_selectSupplierInfoFAByNo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@uID",Session["uID"].ToString())
            ,new SqlParameter("@supplierNo",hidSupplierNo.Value)
            , new SqlParameter("@supplierID",hidSupplierID.Value)
        
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    private void bindLinkManGV()
    {
        gvBasisInfo.DataSource = bindLink(hidSupplierNo.Value, hidSupplierID.Value);
        gvBasisInfo.DataBind();


        hidTabIndex.Value = "0";
    }

    private DataTable bindLink(string supplierNo, string supplierID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@supplierNo", supplierNo);
        param[1] = new SqlParameter("@supplierID", supplierID);

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, "sp_supplier_selectInfoLinkNameByNo", param).Tables[0];
    }


    private SqlDataReader selectInfoBySupplierNo(string supplierNo, string supplierID)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@supplierNo", supplierNo);
        param[1] = new SqlParameter("@supplierID", supplierID);

        return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, "sp_supplier_selectInfoByNo", param);
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supplier_InfoList.aspx");
    }







    /// <summary>
    /// 没写完，存储过程没有创建
    /// </summary>
    /// <returns></returns>

    protected void gvBasisInfo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnEdit")
        {
            int index = ((GridViewRow)((LinkButton)(e.CommandSource)).Parent.Parent).RowIndex;
            string no = gvBasisInfo.Rows[index].Cells[0].Text.ToString();
            string name = gvBasisInfo.Rows[index].Cells[1].Text.ToString();
            string role = gvBasisInfo.Rows[index].Cells[2].Text.ToString();
            string mobliePhone = gvBasisInfo.Rows[index].Cells[3].Text.ToString();
            string phone = gvBasisInfo.Rows[index].Cells[4].Text.ToString();
            string email = gvBasisInfo.Rows[index].Cells[5].Text.ToString();

           


            hidTabIndex.Value = "0";

        }
        else if (e.CommandName == "lkbtnDelete")
        {
            int index = ((GridViewRow)((LinkButton)(e.CommandSource)).Parent.Parent).RowIndex;
            string no = gvBasisInfo.Rows[index].Cells[0].Text.ToString();


            int flag = deleteLinkByNo(no);

            if (flag == 1)
            {
                Alert("删除成功！");
                bindLinkManGV();
                
            }
            else
            {
                Alert("删除失败！");
            }

        }
    }

    private int deleteLinkByNo(string no)
    {
        string sqlstr = "sp_supplier_deleteLinkByNo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@supplierNo",hidSupplierNo.Value)
            , new SqlParameter("@supplierID",hidSupplierID.Value)
            , new SqlParameter("@Num",no)
           
        };
        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }
    protected void FAgv_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = FAgv.DataKeys[intRow].Values["supplier_FilePath"].ToString().Trim();
            string fileName = FAgv.DataKeys[intRow].Values["supplier_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }        

    }
    protected void FAgv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (FAgv.DataKeys[e.Row.RowIndex].Values["supplier_FilePath"].ToString() == string.Empty)
            {
                e.Row.Cells[4].Text = "无文件";
                e.Row.Cells[4].Enabled = false;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }
            //此处允许查看
            if (FAgv.DataKeys[e.Row.RowIndex].Values["supplier_FileIsEffect"].ToString() == "0")//已过期
            {
                //e.Row.Cells[4].Text = "文件已过期";
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }

        }
    }



    protected void gvFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gvFile.DataKeys[e.Row.RowIndex].Values["SignFile_FileStatus"].ToString() == "2")
            {
                e.Row.Cells[4].Text = "已作废";
                e.Row.Cells[4].Enabled = false;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                e.Row.Cells[4].Text = "";
            }

        }
    }
    protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvFile.DataKeys[intRow].Values["SignFile_FilePath"].ToString().Trim();
            string fileName = gvFile.DataKeys[intRow].Values["SignFile_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        //作废
        if (e.CommandName.ToString() == "NotEff")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string fileID = gvFile.DataKeys[intRow].Values["SignFile_FileID"].ToString().Trim();
            if (disApproveForSignedFileStatus(fileID))
            {
                this.Alert("作废失败！");
                return;
            }
            else
            {
                bindSignedGV();
            }
        }
    }


    private bool disApproveForSignedFileStatus(string fileID)
    {
        string sql = "Update supplier_InfoSignedFileDet Set SignFile_FileStatus = 2 Where SignFile_FileID = '" + fileID + "'";
        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql));
    }


    protected void gvFormal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvFile.DataKeys[intRow].Values["SignFile_FilePath"].ToString().Trim();
            string fileName = gvFile.DataKeys[intRow].Values["SignFile_FileName"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";
        }
        if (e.CommandName.ToString() == "DeleteGF")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string fileID = gvFile.DataKeys[intRow].Values["SignFile_FileID"].ToString().Trim();

            int flag = deleteFormalFile(fileID);

            if (flag == 1)
            {
                ltlAlert.Text = "alert('删除成功')";
                bindGvFormal();
              
            }
            else
            {
                Alert("删除失败！");
            }

        }
    }

    private int deleteFormalFile(string fileID)
    {
        string sqlstr = "sp_supplier_deleteFormalFile";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@fileID",fileID)
            
        };

        return Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param));
    }

    private void bindGvFormal()
    {
        gvFormal.DataSource = selectFormalInfoGVByNo();
        gvFormal.DataBind();
        hidTabIndex.Value = "3";
    }

    private DataTable selectFormalInfoGVByNo()
    {
        string sqlstr = "sp_supplier_selectFormalInfoGVByNo";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@uID",Session["uID"].ToString())
            ,new SqlParameter("@supplierNo",hidSupplierNo.Value)
            , new SqlParameter("@supplierID",hidSupplierID.Value)
        
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }


}
