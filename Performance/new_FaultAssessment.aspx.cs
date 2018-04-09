using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;

public partial class new_FaultAssessment : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    private bool flag;
    /// <summary>
    /// 标志是否是编辑状态
    /// </summary>
    public bool Flag
    {
        get
        {
            if (ViewState["flag"] == null)
            {
                bool flag = true;
                ViewState["flag"] = flag;
            }
            return Convert.ToBoolean( ViewState["flag"]);
        }
        set
        {
            ViewState["flag"] = value;
        }
    }


    private DataTable dt;

    public DataTable Dt
    {
         get
        {
            if (ViewState["DataTable"] == null)
            {
                ViewState["DataTable"] = new DataTable();
            }
            return ViewState["DataTable"] as DataTable;
        }
        set
        {
            ViewState["DataTable"] = value;
        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            selectPerfDemeritNew(txtassessment.Text.ToString().Trim());
            bind();
        }
    }
    /// <summary>
    /// 查找记过考核名信息
    /// </summary>
    /// <param name="type"></param>
    private void selectPerfDemeritNew(string type)
    {
        string sqlstr = "sp_perf_selectPerfDemeritNew";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@type",type)
        };
        Dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
        
    
    }
    /// <summary>
    /// 设置显示/隐藏按钮
    /// </summary>
    private void btnSwitch()
    {
        if (Flag)
        {
            btnAdd.Visible = true;
            
            btnSave.Visible = false;
            

        }
        else
        {
            btnAdd.Visible = false;
           
            btnSave.Visible = true;
        }
    
    }
    private void bind()
    {
        btnSwitch();
        gv.DataSource = Dt;
        gv.DataBind();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        selectPerfDemeritNew(txtassessment.Text.ToString().Trim());
        bind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        bind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //激活按钮
            ((LinkButton)e.Row.FindControl("lnkEdit")).Style.Add("font-weight", "normal");
            ((LinkButton)e.Row.FindControl("lnkDelete")).Style.Add("font-weight", "normal");
           
            if (!Flag)//设置删除和编辑的隐藏和显示
            {
                ((LinkButton)e.Row.FindControl("lnkEdit")).Visible = false;
                ((LinkButton)e.Row.FindControl("lnkDelete")).Visible = false;
            }
            else
            {
                ((LinkButton)e.Row.FindControl("lnkEdit")).Visible = true;
                ((LinkButton)e.Row.FindControl("lnkDelete")).Visible = true;
            }
        }

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ModifyDesc")//编辑
        {
            LinkButton linkBtn = (LinkButton)(e.CommandSource);
            int index = ((GridViewRow)(linkBtn.Parent.Parent)).RowIndex;

            Flag = false;
            btnSwitch();
            txtassessment.Text = gv.DataKeys[index][1].ToString();
            txtNote.Text = gv.DataKeys[index][2].ToString();
            lblId.Text = e.CommandArgument.ToString();
            bind();
        }
        if (e.CommandName == "Delete")//删除
        {
            string sqlstr = "sp_perf_deletePerfDemeritNew";
            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@perfdm_id",e.CommandArgument)
            };

            int mark = SqlHelper.ExecuteNonQuery(adam.dsn0(),CommandType.StoredProcedure,sqlstr,param);
            if (mark != 1)
            {
                ltlAlert.Text = "alert('删除失败')";
            }
            else
            {
                ltlAlert.Text = "alert('删除成功')";
            }
            selectPerfDemeritNew(txtassessment.Text.ToString().Trim());
            bind();
            
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtassessment.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('记过考核类型不能为空')";
            txtassessment.Focus();
            return;
        }
        if (!checkRepeatType(0))
        {
            return;
        }
        string sqlstr = "sp_perf_insertPerfDemeritNew";
        SqlParameter[] param = new SqlParameter[] { 
        new SqlParameter("@perfdm_type",txtassessment.Text.Trim())
        ,new SqlParameter("@perfdm_remark",txtNote.Text.Trim())
        ,new SqlParameter("@perfdm_createBy" ,Convert.ToInt32( Session["uID"].ToString()))
        
        };

       int mark= SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param);
       if (mark != 1)
       {
           ltlAlert.Text = "alert('添加失败')";
       }
       else
       {
           ltlAlert.Text = "alert('添加成功')";
       }
       txtassessment.Text = "";
       txtNote.Text = "";
       selectPerfDemeritNew(txtassessment.Text.ToString().Trim());
       bind();
            
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Flag = true;
        txtassessment.Text = "";
        txtNote.Text = "";
        lblId.Text = "";
        selectPerfDemeritNew(txtassessment.Text.ToString().Trim());
        bind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtassessment.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('记过考核类型不能为空')";
            txtassessment.Focus();
            return;
        }
        if (!checkRepeatType(1))
        {
            return;
        }
        Flag = true;
        string sqlstr = "sp_perf_updatePerfDemeritNew";
        SqlParameter[] param = new SqlParameter[] { 
        new SqlParameter("@perfdm_type",txtassessment.Text.Trim())
        ,new SqlParameter("@perfdm_remark",txtNote.Text.Trim())
        ,new SqlParameter("@perfdm_modifiedBy" ,Convert.ToInt32( Session["uID"].ToString()))
        ,new SqlParameter("@perfdm_ID",Convert.ToInt32(  lblId.Text.Trim()))
        
        };
        int mark = SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param);
        if (mark != 1)
        {
            ltlAlert.Text = "alert('保存失败')";
        }
        else
        {
            ltlAlert.Text = "alert('保存成功')";
        }
         lblId.Text = "";
         selectPerfDemeritNew(txtassessment.Text.ToString().Trim());
        bind();

    }
    /// <summary>
    /// 检查记过考核名是否重复
    /// </summary>
    /// <param name="flag">标志是添加还是修改</param>
    /// <returns></returns>
    private bool checkRepeatType(int flag)
    {

        int perfdm_id = 0;
        int mark=1;
        if (lblId.Text != string.Empty)
        {
            perfdm_id = Convert.ToInt32(lblId.Text.Trim());
        }

        string sqlstr = "sp_perf_checkRepeatType";
        SqlParameter[] param = new SqlParameter[] { 
            new SqlParameter("@perfdm_type",txtassessment.Text.Trim())
           , new SqlParameter("@perfdm_id",perfdm_id)
             };
        SqlDataReader sdr = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param);
        while (sdr.Read())
        {
           mark= Convert.ToInt32(sdr["msg"]);
        }
        sdr.Close();
        if (mark != 0)
        {
            if(flag == 0)
            {
                ltlAlert.Text = "alert('此记过考核类型已存在，无法添加')";
                txtassessment.Focus();
                return false;
            }
            else
            {
                ltlAlert.Text = "alert('此记过考核类型已存在，无法修改成该考核类型')";
                txtassessment.Focus();
                return false;
            }

        }
        return true;
    
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
    }
}