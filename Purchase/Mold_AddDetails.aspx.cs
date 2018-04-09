using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Purchase_Mold_AddDetails : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            gv.Attributes.Add("style", "word-break:keep-all;word-wrap:normal");//正常换行          

            gv.Attributes.Add("style", "word-break:break-all;word-wrap:break-word");//自动换行

            GetMstr(Request.QueryString["pk0"]);
            txt_MoldNbr.Enabled = false;
            txt_MoldStatus.Enabled = false;
            txt_MoldType.Enabled = false;
            txt_vend.Enabled = false;
            //txt_WorkingLife.Enabled = false;
            txt_remark.Enabled = false;
            //txt_capacity.Enabled = false;
            ddl_detailsStatus.SelectedValue = "1";
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {

        int flag = 0;

        if(txt_detailsNbr.Text.Trim()=="")
        {
            this.Alert("模具编号不能为空");
            return;
        }
        if(txt_startDate.Text.Trim() == "")
        {
            this.Alert("模具启用时间不能为空");
            return;
        }
        try
        {
            Convert.ToDateTime(txt_startDate.Text.Trim());
        }
        catch
        {
            this.Alert("日期格式不正确！");
        }
        if(txt_qty.Text.Length==0)
        {
            this.Alert("数量不能为空");
            return;
        }


        if (txt_TimeCost.Text.Equals(string.Empty))
        {
            this.Alert("开模周期必须填写！");
        }
        else
        {
            flag = 0;
            try
            {
                flag = Convert.ToInt32(txt_TimeCost.Text.Trim());
                if (flag <= 0)
                {
                    this.Alert("开模周期是大于0的整数!");
                }
            }
            catch
            {
                this.Alert("开模周期是大于0的整数!");
            }
        }

        if (txt_Cavity.Text.Equals(string.Empty))
        {
            this.Alert("一模几穴必须填写！");
        }
        else
        {
            flag = 0;
            try
            {
                flag = Convert.ToInt32(txt_Cavity.Text.Trim());
                if (flag <= 0)
                {
                    this.Alert("一模几穴应该是大于0的整数！");
                }

            }
            catch
            {
                this.Alert("一模几穴应该是大于0的整数！");
            }
        }

        if (txt_capacity.Text.Equals(string.Empty))
        {
            this.Alert("产能必须填写！");
        }
        else
        {
            flag = 0;
            try
            {
                flag = Convert.ToInt32(txt_capacity.Text.Trim());
                if (flag <= 0)
                {
                    this.Alert("产能应该是大于0的整数！");
                }

            }
            catch
            {
                this.Alert("产能应该是大于0的整数！");
            }
        }

        if (txt_WorkingLife.Text.Equals(string.Empty))
        {
            this.Alert("寿命必须填写！");
        }
        else
        {
            flag = 0;
            try
            {
                flag = Convert.ToInt32(txt_WorkingLife.Text.Trim());
                if (flag <= 0)
                {
                    this.Alert("寿命应该是大于0的整数！！");
                }

            }
            catch
            {
                this.Alert("寿命应该是大于0的整数！");
            }
        }

       
           


        string sql = "sp_mold_insertMoldDetails";
        

        try
        {
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@mstrID", Request.QueryString["pk0"])
                ,new SqlParameter("@detailsNbr", txt_detailsNbr.Text.Trim())
                ,new SqlParameter("@detailsStatus", Convert.ToInt32(ddl_detailsStatus.SelectedValue))
                ,new SqlParameter("@strartDate", txt_startDate.Text.Trim())
                ,new SqlParameter("@detailsRemark", txt_detailsRemark.Text.Trim())
                ,new SqlParameter("@createBy ", Convert.ToInt32(Session["uID"]))
                ,new SqlParameter("@createName", Session["uName"].ToString())
                ,new SqlParameter("@CavityNbr", txt_cavityNbr.Text.Trim() )
                ,new SqlParameter("@Qty ", Convert.ToInt32(txt_qty.Text.Trim()))
                 , new SqlParameter("@timeCost" , txt_TimeCost.Text.Trim())
                , new SqlParameter("@cavity" , txt_Cavity.Text.Trim())
                , new SqlParameter("@capacity" , txt_capacity.Text.Trim())
                , new SqlParameter("@workingLife", txt_WorkingLife.Text.Trim())
                , new SqlParameter("@venderMoldNo", txt_venderMoldNo.Text.Trim())
            };

            
           flag =  Convert.ToInt32( SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sql, param));
            if (flag != 1)
            {
                this.Alert("编号不能重复！");
                return;
            }
        }
        catch
        {
            this.Alert("添加失败！可能是数量格式不正确");
        }



        txt_detailsNbr.Text = "";
        txt_startDate.Text = "";
        txt_cavityNbr.Text = "";
        txt_detailsRemark.Text = "";
        txt_qty.Text = "";
        txt_TimeCost.Text = "";
        txt_Cavity.Text = "";
        txt_capacity.Text = "";
        txt_WorkingLife.Text = "";
        ddl_detailsStatus.SelectedValue = "1";
        txt_venderMoldNo.Text = "";
        BindGridView();
    }
    protected void GetMstr(string mstrID)
    {
        string sql = "sp_mold_selectMoldMstrByID";

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@mstrID", mstrID);

        DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param).Tables[0];

        txt_MoldNbr.Text = dt.Rows[0]["Mold_Nbr"].ToString();
        txt_MoldStatus.Text = dt.Rows[0]["Mold_Status"].ToString();
        txt_MoldType.Text = dt.Rows[0]["Mold_Type"].ToString();
        txt_vend.Text = dt.Rows[0]["Mold_Vend"].ToString();
        //
        txt_remark.Text = dt.Rows[0]["Mold_Remark"].ToString();
        //
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected override void BindGridView()
    {
        string sql = "sp_mold_selectMoldDetByID";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@mstrID", Request.QueryString["pk0"]);

        gv.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param);
        gv.DataBind();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int intRow = Convert.ToInt32(e.CommandArgument.ToString());
        string id = gv.DataKeys[intRow].Values["Mold_ID"].ToString();
        
        
        if (e.CommandName.ToString() == "Edit")
        {
            
            lbl_MoldID.Text = id;

            string sql = "sp_mold_selectMoldDet";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ID", id);
            DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sql, param).Tables[0];
            txt_detailsNbr.Text =dt.Rows[0]["Mold_Nbr"].ToString();
            txt_startDate.Text = dt.Rows[0]["Mold_StartDate"].ToString();
            txt_cavityNbr.Text = dt.Rows[0]["Mold_CavityDetails"].ToString();
            txt_detailsRemark.Text = dt.Rows[0]["Mold_remark"].ToString();
            txt_qty.Text = dt.Rows[0]["Mold_Qty"].ToString();
            ddl_detailsStatus.SelectedValue = dt.Rows[0]["Mold_Status"].ToString();
            txt_WorkingLife.Text = dt.Rows[0]["Mold_WorkingLife"].ToString();
            txt_capacity.Text = dt.Rows[0]["Mold_Capacity"].ToString();
            txt_TimeCost.Text = dt.Rows[0]["Mold_TimeCost"].ToString();
            txt_Cavity.Text = dt.Rows[0]["Mold_Cavity"].ToString();
            txt_venderMoldNo.Text = dt.Rows[0]["Mold_venderMoldNbr"].ToString(); 
            btn_save.Visible = false;
            btn_mody.Visible = true;
        }
        if (e.CommandName.ToString() == "Del")
        {
            string sql = "sp_mold_deleteMoldDet";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ID", id);            
            param[1] = new SqlParameter("@reValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;
            param[2] = new SqlParameter("@vend", txt_vend.Text.Trim());
            param[3] = new SqlParameter("@moldNbr", (gv.Rows[intRow].Cells[1].FindControl("lblNbr") as Label).Text.Trim());

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, sql, param);
            if (Convert.ToInt32(param[1].Value) != 1)
            {
                if (Convert.ToInt32(param[1].Value) == 2) this.Alert("供应商已经使用了此模具，不能删除！");
                else this.Alert("删除失败！");
            }
            BindGridView();
        }
    }
    protected void btn_mody_Click(object sender, EventArgs e)
    {
        int flag = 0;

        if (txt_detailsNbr.Text.Trim() == "")
        {
            this.Alert("模具编号不能为空");
            return;
        }
        if (txt_startDate.Text.Trim() == "")
        {
            this.Alert("模具启用时间不能为空");
            return;
        }
        try
        {
            Convert.ToDateTime(txt_startDate.Text.Trim());
        }
        catch
        {
            this.Alert("日期格式不正确！");
        }
        try
        {
            Convert.ToInt32(txt_qty.Text.Trim());
        }
        catch
        {
            this.Alert("数量没有填写或者填写格式不正确！");
        }


        if (txt_TimeCost.Text.Equals(string.Empty))
        {
            this.Alert("开模周期必须填写！");
        }
        else
        {
            flag = 0;
            try
            {
                flag = Convert.ToInt32(txt_TimeCost.Text.Trim());
                if (flag <= 0)
                {
                    this.Alert("开模周期是大于0的整数!");
                }
            }
            catch
            {
                this.Alert("开模周期是大于0的整数!");
            }
        }

        if (txt_Cavity.Text.Equals(string.Empty))
        {
            this.Alert("一模几穴必须填写！");
        }
        else
        {
            flag = 0;
            try
            {
                flag = Convert.ToInt32(txt_Cavity.Text.Trim());
                if (flag <= 0)
                {
                    this.Alert("一模几穴应该是大于0的整数！");
                }

            }
            catch
            {
                this.Alert("一模几穴应该是大于0的整数！");
            }
        }

        if (txt_capacity.Text.Equals(string.Empty))
        {
            this.Alert("产能必须填写！");
        }
        else
        {
            flag = 0;
            try
            {
                flag = Convert.ToInt32(txt_capacity.Text.Trim());
                if (flag <= 0)
                {
                    this.Alert("产能应该是大于0的整数！");
                }

            }
            catch
            {
                this.Alert("产能应该是大于0的整数！");
            }
        }

        if (txt_WorkingLife.Text.Equals(string.Empty))
        {
            this.Alert("寿命必须填写！");
        }
        else
        {
            flag = 0;
            try
            {
                flag = Convert.ToInt32(txt_WorkingLife.Text.Trim());
                if (flag <= 0)
                {
                    this.Alert("寿命应该是大于0的整数！！");
                }

            }
            catch
            {
                this.Alert("寿命应该是大于0的整数！");
            }
        }

        string sql = "sp_mold_UpdateMoldDet";

        SqlParameter[] param = new SqlParameter[]{
        
            new SqlParameter("@detailsNbr", txt_detailsNbr.Text.Trim())
            ,new SqlParameter("@mstrID", Request.QueryString["pk0"])
            ,new SqlParameter("@detailsStatus", Convert.ToInt32(ddl_detailsStatus.SelectedValue))
            ,new SqlParameter("@strartDate", txt_startDate.Text.Trim())
            ,new SqlParameter("@detailsRemark", txt_detailsRemark.Text.Trim())
            ,new SqlParameter("@modifyBy ", Convert.ToInt32(Session["uID"]))
            ,new SqlParameter("@modifyName", Session["uName"].ToString())
            ,new SqlParameter("@CavityNbr", txt_cavityNbr.Text.Trim())
            , new SqlParameter("@ID", lbl_MoldID.Text)
            ,new SqlParameter("@Qty ", Convert.ToInt32(txt_qty.Text.Trim()))
            , new SqlParameter("@timeCost" , txt_TimeCost.Text.Trim())
            , new SqlParameter("@cavity" , txt_Cavity.Text.Trim())
            , new SqlParameter("@capacity" , txt_capacity.Text.Trim())
            , new SqlParameter("@workingLife", txt_WorkingLife.Text.Trim())
            , new SqlParameter("@venderMoldNo", txt_venderMoldNo.Text.Trim())

        };

        flag = 0;
        try
        {
          flag =   Convert.ToInt32( SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sql, param));
            
        }
        catch
        {
            this.Alert("修改失败！数量必须是整数！");
        }
        if (flag != 1)
        {
            this.Alert("编号不能重复！");
            return;
        }
        btn_save.Visible = true;
        btn_mody.Visible = false;
        txt_detailsNbr.Text = "";
        txt_startDate.Text = "";
        txt_cavityNbr.Text = "";
        txt_detailsRemark.Text = "";
        txt_qty.Text = "";
        txt_TimeCost.Text = "";
        txt_Cavity.Text = "";
        txt_capacity.Text = "";
        txt_WorkingLife.Text = "";
        ddl_detailsStatus.SelectedValue = "1";
        txt_venderMoldNo.Text = "";
        BindGridView();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //gv.EditIndex = e.NewEditIndex;
    }
}