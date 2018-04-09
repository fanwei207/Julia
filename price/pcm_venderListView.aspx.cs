using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class price_pcm_venderListView : System.Web.UI.Page
{

    PCM_price pc = new PCM_price();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["QAD"] != null)
            {
                bind();
            }
            else
            {
                ltlAlert.Text = "alert('该QAD价格表中没有已存在的供应商');";
                ltlAlert.Text = "window.close();";
            }
        
        }
    }

    private void bind()
    {
        gvVenderList.DataSource = pc.selectOldVenderList(Request["QAD"].ToString(), Request["PQID"].ToString());
        gvVenderList.DataBind();
    
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.close();";
    }

    private DataTable gvchangeDatatable()
    {
        DataTable TempTable = new DataTable("gvTable");
        DataColumn TempColumn;
        DataRow TempRow;

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "PQID";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "QAD";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "vender";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.Decimal");
        TempColumn.ColumnName = "price";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "oldprice";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "um";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "curr";
        TempTable.Columns.Add(TempColumn);

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "venderName";
        TempTable.Columns.Add(TempColumn);


        foreach(GridViewRow gvr in gvVenderList.Rows )
        {
            CheckBox chkVender = gvr.FindControl("chkVender") as CheckBox;//循环将gv中的内容添加入table
            if (chkVender.Checked )
            {
                 string  PQID = Request["PQID"].ToString();
                string QAD = Request["QAD"].ToString();
                decimal price = -1;

                if (!((TextBox)gvr.FindControl("changePrice")).Text.ToString().Trim().Equals(string.Empty))
                {
                    if (decimal.TryParse(((TextBox)gvr.FindControl("changePrice")).Text.ToString().Trim(), out price))
                    {
                        TempRow = TempTable.NewRow();
                        TempRow["PQID"] = PQID;
                        TempRow["QAD"] = QAD;
                        TempRow["vender"] = gvVenderList.DataKeys[gvr.RowIndex].Values["pc_list"].ToString();
                        TempRow["price"] = price;
                        TempRow["oldprice"] = gvVenderList.DataKeys[gvr.RowIndex].Values["pc_price"].ToString();
                        TempRow["um"] = gvVenderList.DataKeys[gvr.RowIndex].Values["pc_um"].ToString();
                        TempRow["curr"] = gvVenderList.DataKeys[gvr.RowIndex].Values["pc_curr"].ToString();
                        TempRow["venderName"] = gvVenderList.DataKeys[gvr.RowIndex].Values["ad_name"].ToString();
                    

                        TempTable.Rows.Add(TempRow);
                    }
                    else
                    {
                        throw new Exception("添加的记录价格必须为数字。");
                        return TempTable;
                    }
                }
                else
                {
                    throw new Exception("添加的记录价格不能为空。");
                    return TempTable;
                }

                
               

            
            }
        }
        return TempTable;

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {


        string PQID = Request["PQID"].ToString();
        string QAD = Request["QAD"].ToString();
        string formate = Request["formate"].ToString();
        DataTable dt =null;
        try
        {
            dt = gvchangeDatatable();
        }
        catch(Exception ex )
        {
            ltlAlert.Text = "alert('" + ex.Message.ToString() + "');";
            return;
        }

        if (dt.Rows.Count == 0)
        {
            ltlAlert.Text = "alert('您没有选中添加的供应商');";
            return;
        }

        if (pc.addVenderMore(dt, Convert.ToInt32(Session["uID"]), PQID, QAD,formate))
        {
            ltlAlert.Text = "alert('添加成功！');";
            ltlAlert.Text += "window.close();";
        }
        else
        {
            ltlAlert.Text = "alert('添加失败！');";
            return;
        
        }

        ltlAlert.Text = "window.close();";




        //    if (pc.addVenderMore(Request["QAD"].ToString(), sb.ToString(), Convert.ToInt32(Session["uID"]), Request["PQID"].ToString(), Request["formate"].ToString()))
        //    {
        //        

        //    }
        //    else
        //    {
        //        
        //    }
 
    }
}