using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class part_NPart_ShowPartList : BasePage
{
    adamClass chk = new adamClass();
    Npart_help help = new Npart_help();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidFlowID.Value = Request.QueryString["Flow_Id"];
            hidNodeID.Value = Request.QueryString["Node_Id"];
            bindDDLType();

            
        }

        bindBehandGVData();
    }
    private void bindDDLType()
    {
        ddlModleList.Items.Clear();
        ddlModleList.DataSource = help.selectAllTypeMstr();
        ddlModleList.DataBind();
        ddlModleList.Items.Insert(0, new ListItem("--", "00000000-0000-0000-0000-000000000000"));
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {

        bindBehandGVData();
    }

    private void bindBehandGVData()
    {
        if (ddlModleList.SelectedValue == "00000000-0000-0000-0000-000000000000")
        {
            ltlAlert.Text = "alter('请选择模板')";
            return;
        }
        else
        {
            BuildGV();//制作gv
            BindData();          
        }

    }

    private void BuildGV()
    {
        string typeID = ddlModleList.SelectedValue;
       
        DataTable dt = getGvColByTypeDetForSupplier(typeID);

        bool choice = true;

        help.createGridView(gvDet, dt, choice, false);

        gvDet.PageIndexChanging += new GridViewPageEventHandler(gvDet_PageIndexChanging);
    }

    private DataTable getGvColByTypeDetForSupplier(string mstrID)
    {
        string sqlstr = "sp_Npart_getGvColByTypeDetForNPartList";

        SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@mstrID",mstrID)
            
            };

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    private new void BindData()
    {

        if (ddlModleList.SelectedValue.Equals("00000000-0000-0000-0000-000000000000"))
        {
            Alert("请选择零件类型");
            return;
        }

        DataTable dtTemp = new DataTable("dtTemp");
        DataColumn TempColumn;
        DataRow TempRow;

        SqlDataReader drSelect = getSelectToPartList(ddlModleList.SelectedValue);

        List<object> list = new List<object>();

        while (drSelect.Read())
        {
            string id = drSelect["id"].ToString();
            string name = drSelect["colName"].ToString();
            string englishName = drSelect["colEnglishName"].ToString();
            string isEnum = drSelect["colIsEnum"].ToString();

            TempColumn = new DataColumn();
            TempColumn.DataType = System.Type.GetType("System.String");
            TempColumn.ColumnName = englishName;
            dtTemp.Columns.Add(TempColumn);



            if (isEnum.Equals("1") || isEnum.Equals("True"))
            {
                list.Add(((DropDownList)tableInselect.Rows[0].FindControl("englishName")));
            }
            else
            {
                list.Add(((TextBox)tableInselect.Rows[0].FindControl("englishName")));
            }

                
        }

        foreach (object obj in list)
        {
            Alert("1");
        }




        string typeID = ddlModleList.SelectedValue;
        string Qad = QadNumber.Text.Trim();
        string Part = PartNumber.Text.Trim();


        gvDet.DataSource = getNPartList(typeID, Qad, Part);
        gvDet.DataBind();
    }
    private DataTable getNPartList(string mstrID,string Qad, string Part)
    {
        string str = "sp_NPart_SelectNPartList";
        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@mstrID", mstrID);
        sqlParam[1] = new SqlParameter("@Qad", Qad);
        sqlParam[2] = new SqlParameter("@Part", Part);


       return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, sqlParam).Tables[0];
    }

    private void gvDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDet.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void ddlModleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BuildGV();
        tableInselect.Rows[0].Cells.Clear();

        //获取相关的查询条件的
        string typeID = ddlModleList.SelectedValue;
        SqlDataReader drSelect = getSelectToPartList(typeID);

       

        while (drSelect.Read())
        {
            HtmlTableCell cell;
            string id = drSelect["id"].ToString();
            string name = drSelect["colName"].ToString();
            string englishName = drSelect["colEnglishName"].ToString();
            string isEnum = drSelect["colIsEnum"].ToString();
            string isDate = drSelect["colIsDate"].ToString();
            string isNumber = drSelect["colIsNumber"].ToString();
            string sort = drSelect["colSort"].ToString();
            string item = drSelect["item"].ToString(); // 如果是0 说明是固定字段，如果不是0 则是一个数字 n  col+n就是这个格子

            cell = new HtmlTableCell();
            cell.InnerText = name;
            tableInselect.Rows[0].Cells.Add( cell);

            if (isEnum.Equals("1") || isEnum.Equals("True"))
            {
                DataTable enumtable = getEnumTableByMstrIDandName(typeID, id);

                DropDownList drop = new DropDownList();
                drop.ID = englishName;
                drop.Width = Unit.Pixel(100);

                drop.DataTextField = "partEnumValue";
                drop.DataValueField = "ID";
                drop.DataSource = enumtable;
                drop.DataBind();
                drop.Items.Insert(0,new ListItem("--", "0"));



                cell = new HtmlTableCell();
                cell.Controls.Add(drop);
                tableInselect.Rows[0].Cells.Add(cell);


            }
            else
            {
                TextBox txt = new TextBox();
                txt.ID = englishName;
                txt.MaxLength = 500;
                txt.Width = Unit.Pixel(100);

                if (isNumber.Equals("1") || isNumber.Equals("True"))
                {
                    txt.CssClass += " Decimal ";
                }
                if (isDate.Equals("1") || isDate.Equals("True"))
                {
                    txt.CssClass += " Date ";
                }

                cell = new HtmlTableCell();
                cell.Controls.Add(txt);
                tableInselect.Rows[0].Cells.Add(cell);

            }


        }
        drSelect.Close();

        BindData();
    }

    private DataTable getEnumTableByMstrIDandName(string typeID, string id)
    {
        string sqlstr = "sp_Npart_getEnumTableByMstrIDandName";
        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@mstrID", typeID);
        sqlParam[1] = new SqlParameter("@id", id);
     




        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, sqlstr, sqlParam).Tables[0];

    }

    private SqlDataReader getSelectToPartList(string mstrID)
    {
        string sqlstr = "sp_Npart_getGvColByImportPartList";

        SqlParameter[] sqlParam = new SqlParameter[3];
        sqlParam[0] = new SqlParameter("@mstrID", mstrID);



        return SqlHelper.ExecuteReader(chk.dsn0(), CommandType.StoredProcedure, sqlstr, sqlParam);



    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        BuildGV();//制作gv
        DataTable dtdown = exportExcelFromPage(ddlModleList.SelectedValue, QadNumber.Text.Trim(), PartNumber.Text.Trim());
        StringBuilder title = new StringBuilder("");

        foreach (DataColumn dc in dtdown.Columns)
        {
            title.Append("100^<b>");
            title.Append(dc.ColumnName);
            title.Append("</b>~^");
        }


        if (dtdown != null && dtdown.Rows.Count > 0)
        {
            ExportExcel(title.ToString(), dtdown, false);
        }
    }

    private DataTable exportExcelFromPage(string Module,string QadNumber, string PartNumber)
    {
        string str = "sp_NPart_exportExcelFromPage";

        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@module", Module);
        param[1] = new SqlParameter("@QadNumber", QadNumber);
        param[2] = new SqlParameter("@PartNumber", PartNumber);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, str, param).Tables[0];
    }
}