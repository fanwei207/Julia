using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class part_Npart_partTypeDetEdit : BasePage
{

    Npart_help help = new Npart_help();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string mstrID = Request.QueryString["mstrID"];

            hidMstrID.Value = mstrID;

            string mstrName = string.Empty;

            DataTable dt = help.selectTypeMstrByID(mstrID);

            if (dt.Rows.Count > 0)
            {
                lbmstrName.Text = dt.Rows[0][1].ToString() + "模板编辑管理";
                lbmstrName.Font.Size = 25;
            }
            else
            {
                lbmstrName.Text = mstrName;
            }

            Bind();
        }
    }

    private void Bind()
    {
        hidDetID.Value = string.Empty;
        txtColName.Text = string.Empty;
        txtColEnglishName.Text = string.Empty;
        txtSort.Text = string.Empty;
        txtPrefix.Text = string.Empty;
        txtSuffix.Text = string.Empty;
        chkDate.Checked = false;
        chkEnum.Checked = false;
        chkSpace.Checked = true;
        chkNumber.Checked = false;
        gvDet.Columns[10].Visible = true;//将删除隐藏掉

        gvDet.DataSource = help.selectAllTypeDetByMstrID(hidMstrID.Value);
        gvDet.DataBind();
    }
    protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbEide")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            hidDetID.Value = gvDet.DataKeys[index].Values["ID"].ToString();



            txtColName.Text = gvDet.Rows[index].Cells[0].Text.ToString().Replace("&nbsp;", "");
            txtColEnglishName.Text = gvDet.Rows[index].Cells[1].Text.ToString().Replace("&nbsp;", "");
            txtPrefix.Text = gvDet.Rows[index].Cells[2].Text.ToString().Replace("&nbsp;", "");
            txtSuffix.Text = gvDet.Rows[index].Cells[3].Text.ToString().Replace("&nbsp;", "");
            txtSort.Text = gvDet.Rows[index].Cells[4].Text.ToString().Replace("&nbsp;", "");

            

            chkNumber.Checked = gvDet.Rows[index].Cells[5].Text.ToString().Equals("True")?true:false;

            chkDate.Checked = gvDet.Rows[index].Cells[6].Text.ToString().Equals("True") ? true : false;

            chkSpace.Checked = gvDet.Rows[index].Cells[7].Text.ToString().Equals("True") ? true : false;
            
            chkEnum.Checked = (gvDet.Rows[index].FindControl("lkbEide") as LinkButton).Text.ToString().Equals("True") ? true : false;
            
            gvDet.Columns[10].Visible = false;//将删除隐藏掉

            btnAdd.Text = "保存修改";
            btnClear.Text = "取消修改";
        }
        if (e.CommandName == "lkbDelete")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string detID = gvDet.DataKeys[index].Values["ID"].ToString();

            string mstrID = hidMstrID.Value;

            string flag = help.deleteTypeDetByID(mstrID, detID);

            if (flag == "1")
            {
                Alert("删除成功");
                Bind();
            }
            else if (flag == "-1")
            {
                Alert("选择的是已经使用过的列不允许删除");
            }
            else if (flag == "-2")
            {
                Alert("固定列不允许删除");
            }
            else 
            {
                Alert("删除失败，请联系管理员");
            }
        
        }
        if (e.CommandName == "lkbEideEnum")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;

            string detID = gvDet.DataKeys[index].Values["ID"].ToString();

            if (detID.Equals("d464caf1-568f-4653-80c8-5d8dd02d9d62"))
            {
                ltlAlert.Text = "$.window('修改物料类型',1000,800,'/part/Npart_partTypeForQADManager.aspx?mstrID="+ hidMstrID.Value + "');";
            }
            else
            {
                ltlAlert.Text = "$.window('修改物料类型',1000,800,'/part/Npart_partTypeEnumManager.aspx?mstrID=" + hidMstrID.Value + "&detId=" + detID + "');";
            }
            
        }
    }
    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string isCanUpdate = gvDet.DataKeys[e.Row.RowIndex].Values["colIsCanUpdate"].ToString();
            if (isCanUpdate != "True")
            {
                ((LinkButton)e.Row.FindControl("lkbDelete")).Visible = false;
                ((LinkButton)e.Row.FindControl("lkbEide")).Visible = false;
            }

            string IsEnum = gvDet.DataKeys[e.Row.RowIndex].Values["colIsEnum"].ToString();

           

            if (IsEnum != "True")
            {
                ((LinkButton)e.Row.FindControl("lkbEideEnum")).Enabled = false;
                ((LinkButton)e.Row.FindControl("lkbEideEnum")).Font.Underline = false;
            }

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string mstrID = hidMstrID.Value;

        if (string.Empty.Equals(hidDetID.Value))
        {
            //新增
            string colname = txtColName.Text.Trim();

            if (string.Empty.Equals(colname))
            {
                Alert("列名禁止为空");
                return;
            }

            string colEngilshName = txtColEnglishName.Text.Trim();

            if (string.Empty.Equals(colEngilshName))
            {
                Alert("英文列名禁止为空");
                return;
            }

            

            string prefix = txtPrefix.Text.Trim();

            string srefix = txtSuffix.Text.Trim();

            string sort = txtSort.Text.Trim();
 
            int sortint = 0;
            if (string.Empty.Equals(sort))
            {
                Alert("顺序不能为空");
                return;
            }

            if (int.TryParse(sort, out sortint))
            {
                if (sortint < 0)
                {
                    Alert("顺序必须是正整数");
                    return;
                }
            }
            else
            {
                Alert("顺序列请输入整数");
                return;
            }

            int isnum = chkNumber.Checked ? 1 : 0;
            int isDate = chkDate.Checked ? 1 : 0;
            int isEnum = chkEnum.Checked ? 1 : 0;
            int isSpace = chkSpace.Checked ? 1 : 0;

            if (isnum == isDate && isDate == 1)
            {
                Alert("列上不可能既是数字又是日期");
                return;
            }

            string flag = help.addTypeDet(mstrID, colname, colEngilshName, prefix, srefix, sort, isnum, isDate, isEnum,isSpace, Session["uID"].ToString(), Session["uName"].ToString());

  

            switch (flag)
            { 
                case  "-1":
                    Alert("该顺序已经存在于模板中，请更换");
                    return;

                case "-2":
                     Alert("已使用的模板列中存在顺序较后的，请更换");
                    return;

                case "-3":
                    Alert("列名已存在，请更换");
                    return;

                case "-4":
                    Alert("列英文名已存在，请更换");
                    return;

                case "1":
                    Alert("保存成功");
                    Bind();
                    return;
                case "0":
                    Alert("保存失败，请联系管理员");
                    return;
            }


        }
        else
        { 
            //修改
            string detID = hidDetID.Value;

            string colname = txtColName.Text.Trim();

            if (string.Empty.Equals(colname))
            {
                Alert("列名禁止为空");
                return;
            }

            string colEngilshName = txtColEnglishName.Text.Trim();

            if (string.Empty.Equals(colEngilshName))
            {
                Alert("英文列名禁止为空");
                return;
            }

            string prefix = txtPrefix.Text.Trim();

            string srefix = txtSuffix.Text.Trim();

            string sort = txtSort.Text.Trim();

            int sortint = 0;
            if (int.TryParse(sort, out sortint))
            {
                if (sortint < 0)
                {
                    Alert("顺序必须是正整数");
                    return;
                }
            }
            else
            {
                Alert("顺序列请输入整数");
                return;
            }

            int isnum = chkNumber.Checked ? 1 : 0;
            int isDate = chkDate.Checked ? 1 : 0;
            int isEnum = chkEnum.Checked ? 1 : 0;
            int isSpace = chkSpace.Checked ? 1 : 0;

            if (isnum == isDate && isDate == 1)
            {
                Alert("列上不可能既是数字又是日期");
                return;
            }


            string flag = help.modifiyTypeDet(mstrID, detID, colname, colEngilshName, prefix, srefix, sort, isnum, isDate, isEnum, isSpace, Session["uID"].ToString(), Session["uName"].ToString());



            switch (flag)
            {
                case "-1":
                    Alert("该顺序已经存在于模板中，请更换");
                    return;

                case "-2":
                    Alert("已使用的模板列中存在顺序较后的，请更换");
                    return;

                case "-3":
                    Alert("列名已存在，请更换");
                    return;

                case "-4":
                    Alert("列英文名已存在，请更换");
                    return;

                case "-5":
                    Alert("固定列英文名不可修改");
                    return;
                case "-6":
                    Alert("固定列中文名不可修改");
                    return;
                case "-7":
                    Alert("固定列是否枚举不允许修改");
                    return;


                case "1":
                    Alert("保存成功");
                    btnAdd.Text = "新增";
                    btnClear.Text = "清空";
                    Bind();
                    return;
                case "0":
                    Alert("保存失败，请联系管理员");
                    return;
            }
            
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        hidDetID.Value = string.Empty;
        txtColName.Text = string.Empty;
        txtColEnglishName.Text = string.Empty;
        txtSort.Text = string.Empty;
        txtPrefix.Text = string.Empty;
        txtSuffix.Text = string.Empty;
        chkDate.Checked = false;
        chkEnum.Checked = false;
        chkNumber.Checked = false;
        chkSpace.Checked = true;
        gvDet.Columns[10].Visible = true;//将删除隐藏掉

        btnAdd.Text = "新增";
        btnClear.Text = "清空";
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Npart_typeManage.aspx");
    }
}