using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adamFuncs;
using QCProgress;
using System.Data;

public partial class QC_qc_product_input_size : BasePage
{
    QC qc = new QC();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hidID.Value = Request["ID"].ToString();

            Bind();
        
        }
    }

    private void Bind()
    {
        int prdID = Convert.ToInt32(hidID.Value);
        string error = string.Empty;
        string  avgLong =  string.Empty;
        string  avgWide =  string.Empty;
        string  avgHigh =  string.Empty;
        string  avgVolume =  string.Empty;
        string  avgQuality =  string.Empty;
        int uID = Convert.ToInt32(Session["uID"].ToString());
        string part = Request.QueryString["part"].ToString();

        DataTable dt = qc.getInfoByPrdID(prdID, uID, out avgLong, out avgWide, out avgHigh, out avgVolume, out avgQuality, out error,part);

        txtError.Text = error;
        lbAvgHigh.Text = avgHigh;
        lbAvgLong.Text = avgLong;
        lbAvgQuality.Text = avgQuality;
        lbAvgVolume.Text = avgVolume;
        lbAvgWide.Text = avgWide;

        if(dt != null)
        {
            if (dt.Rows.Count != 0)
            {
                if (dt.Rows[0][0].ToString().Equals("1"))
                {
                    ltlAlert.Text = "alert('新建数据出错请联系管理员');";
                }
                else
                {
                    gvInfo.DataSource = dt;
                    gvInfo.DataBind();
                }
            }
            else
            {
                ltlAlert.Text = "alert('程序出错请联系管理员');";
            
            }
        }
        else
        {
            ltlAlert.Text = "alert('程序出错请联系管理员');";
        
        }

    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        /*
         创建一个datatable
         * 将数据写入table
         * 做成xml输入
         */
        DataTable dt = null;
        try
        {
             dt = this.createDataTable();
        }
        catch(Exception ex)
        {
            ltlAlert.Text = "alert('"+ex.Message.ToString()+"');";
            return;
        }
        string strError =txtError.Text.ToString();

        float ferror = 0;

        if(!float.TryParse(strError,out ferror ))
        {
             ltlAlert.Text = "alert('误差输入框只能输入数字');";
            return;
        }
        if (dt == null)
        {
            ltlAlert.Text = "alert('程序出错请联系管理员');";
            return;
        }
        if (qc.saveProductSize(Session["uID"].ToString(), dt, hidID.Value.ToString(), strError))
        {
            ltlAlert.Text = "alert('保存成功');";
            Bind();
        }
        else
        {
            ltlAlert.Text = "alert('保存失败，请重试');";
        }

        

    }

    private DataTable createDataTable()
    {
        DataTable table = new DataTable("qcSizeTable");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "ID";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "long";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "wide";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "high";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "quality";
        table.Columns.Add(column);

        foreach (GridViewRow gvRow in gvInfo.Rows)
        {
             TextBox txtlong = gvRow.FindControl("txtlong") as TextBox;
             TextBox txtwide = gvRow.FindControl("txtwide") as TextBox;
             TextBox txthigh = gvRow.FindControl("txthigh") as TextBox;
             TextBox txtquality = gvRow.FindControl("txtquality") as TextBox;
            row = table.NewRow();
            string strlong = txtlong.Text.Trim();
            string strwide = txtwide.Text.Trim();
            string strhigh = txthigh.Text.Trim();
            string strquality = txtquality.Text.Trim();

            float flong = 0;
            float fwide = 0;
            float fhigh = 0;
            float fquality = 0;

   

            if (string.Empty.Equals(strlong) || string.Empty.Equals(strhigh) || string.Empty.Equals(strquality) ||
                string.Empty.Equals(strwide) )
            {
                if ((!string.Empty.Equals(strlong)) || (!string.Empty.Equals(strhigh)) || (!string.Empty.Equals(strquality)) ||
                (!string.Empty.Equals(strwide)))
                {
                    throw new Exception("单行数据或者不填，或者必须填写完整");
                }
            }
            if (!string.Empty.Equals(strlong) && !string.Empty.Equals(strhigh) && !string.Empty.Equals(strquality) &&
               !string.Empty.Equals(strwide))
            {
                if (!float.TryParse(strlong, out flong))
                {
                    throw new Exception("输入框中请填写数字");
                }
                if (!float.TryParse(strwide, out fwide))
                {
                    throw new Exception("输入框中请填写数字");
                }
                if (!float.TryParse(strhigh, out fhigh))
                {
                    throw new Exception("输入框中请填写数字");
                }
                if (!float.TryParse(strquality, out fquality))
                {
                    throw new Exception("输入框中请填写数字");
                }
            }

            row["ID"] = gvInfo.DataKeys[gvRow.RowIndex].Values["ID"].ToString();
            row["long"] = strlong;
            row["wide"] = strwide;
            row["high"] = strhigh;
            row["quality"] = strquality;
               
            
            table.Rows.Add(row);
        }
        return table;
    }
}