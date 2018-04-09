using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class price_pcm_FinPrice : BasePage
{
    private PCM_FinCheckApply helper = new PCM_FinCheckApply();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["isPrice"] != null)
            {
                radioCheckPrice.Checked = true;
                radioPrice.Checked = false;
                
                ddlStatus.SelectedValue = "0";
            }
            BindData();
        }
    }

    private void BindData()
    {
        
        btnHandle.Visible = false;
        btnExport.Visible = false;
        btnCancel.Visible = false;
        
        int type = 0;
        //报价
        if (radioPrice.Checked)
        {
            type = 1;
            //gvDet.Columns[13].Visible = false;
            //gvDet.Columns[14].Visible = false;
            //报价处理
            if (this.Security["122000036"].isValid)
            {

                btnHandle.Visible = true;
                if (radioPrice.Checked && ddlStatus.SelectedValue == "0")
                {
                    btnHandle.Enabled = true;
                }
                else
                {
                    btnHandle.Enabled = false;
                }

            }
  
        }
        else //核价
        {
            type = 2;
            btnCancel.Visible = true;
           
            //gvDet.Columns[13].Visible = true;
            //gvDet.Columns[14].Visible = true;
            //核价处理
            if (this.Security["122000037"].isValid)
            {
                btnHandle.Visible = true;
                btnCancel.Visible = true;
                if (!radioPrice.Checked && ddlStatus.SelectedValue != "1")
                {
                    btnHandle.Enabled = true;
                    btnCancel.Enabled = true;
                    
                }
                else
                {
                    btnHandle.Enabled = false;
                    btnCancel.Enabled = false;
                    

                }

            }
        }
        if (ddlStatus.SelectedValue == "0")//未处理
        {
            

          
        }
        else if (ddlStatus.SelectedValue == "1")//已处理
        {
           

        }
        else if (ddlStatus.SelectedValue == "2")//挂起
        {
            //gvDet.Columns[7].Visible = false;

           
            
        }

        string status = ddlStatus.SelectedValue;
        string itemCode = txtItemCode.Text.Trim();
        string part = txtPart.Text.Trim();
        string vender = txtVender.Text.Trim();
        DataTable dt= helper.GetFinPrice(itemCode, part, vender, type, status);
        gvDet.DataSource = dt;
        gvDet.DataBind();

        
      
      
      
        //导出处理
        if (this.Security["122000038"].isValid )
        {
            btnExport.Visible = true;
          
            if (radioPrice.Checked)
            {
                if (ddlStatus.SelectedValue == "1")
                {
                    btnExport.Enabled = true;
                    if (radioPrice.Checked)
                    {

                       

                    }
                    else
                    {
                      
                    }
                }
                else
                {
                    btnExport.Enabled = false;
                  
                }
            }
            else
            {
             
                if (ddlStatus.SelectedValue == "1")
                {
                    btnExport.Enabled = true;
                    if (radioPrice.Checked)
                    {

                     
                    }
                    else
                    {
                        

                    }
                }
                else
                {
                    btnExport.Enabled = false;
                  
                }
            }
            
        }
      


    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToBoolean(gvDet.DataKeys[e.Row.RowIndex].Values["isout"]))
            {
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Orange;
            }


            if (gvDet.DataKeys[e.Row.RowIndex].Values["statusName"].ToString() == "已忽略")
            {
                CheckBox chk = e.Row.FindControl("chk") as CheckBox;
                chk.Enabled = false;
            }

            TextBox txtStartDate = e.Row.FindControl("txtStartDate") as TextBox;
            if (txtStartDate.Text.Trim() != "")
            {
                txtStartDate.Text = DateTime.Parse(txtStartDate.Text).ToString("yyyy-MM-dd");
            }
            if (radioCheckPrice.Checked && ddlStatus.SelectedValue == "1")
            {
                TextBox txtStart = e.Row.FindControl("txtStartDate") as TextBox;
                txtStart.Enabled = false;
                TextBox txtEnd = e.Row.FindControl("txtEndDate") as TextBox;
                txtEnd.Enabled = false;

            }


            if (ddlStatus.SelectedValue == "1")
            {
                int rowindex = e.Row.RowIndex;
                if (gvDet.DataKeys[rowindex].Values["checkPriceToQAD"].ToString() != "True")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.LightGreen;
                    ((CheckBox)e.Row.Cells[0].FindControl("chk")).Checked = true;
                }

                ((TextBox)e.Row.Cells[14].FindControl("txtStartDate")).Enabled = false;
                ((TextBox)e.Row.Cells[15].FindControl("txtEndDate")).Enabled = false;
            }



        }


    }


    protected void btnHandle_Click(object sender, EventArgs e)
    {
        DataTable dt = GetSelectedId();
        if (dt.Rows.Count > 0)
        {
            int type = 0;//1是报价 2是核价
            if (radioPrice.Checked)
            {
                type = 1;
            }
            else
            {
                DateTime dateTiemTest = DateTime.Now;
                DateTime dateNowAdd30 = DateTime.Now.AddDays(30);
                type = 2;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["startDate"].ToString() == "")
                    {
                        ltlAlert.Text = "alert('请选择生效日期');";
                        return;
                    }
                    dateTiemTest = DateTime.Parse(row["startDate"].ToString());
                    if (DateTime.Compare(dateTiemTest, dateNowAdd30) > 0)
                    {
                        ltlAlert.Text = "alert('生效日期超过当前时间的30天，为不合理数据，请查清后填写');";
                        return;
                    
                    }
                    
                }
            }
            try
            {
                helper.FinHandle(dt, type);
            }
            catch (Exception ex)
            {

                ltlAlert.Text = "alert('" + ex.Message + "');";
                return;
            }
            //string tempFIle = Server.MapPath("/docs/QadPrice.xls");
            //string outputFile = "QadPrice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            //helper.ExportExcel(tempFIle, Server.MapPath("../Excel/") + outputFile, dt, type);           
            BindData();
            //ltlAlert.Text = "window.open('/Excel/" + outputFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }
        else
        {
            ltlAlert.Text = "alert('请选择数据！')";
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
             dt = GetSelectedIdByCancel();
        }
        catch (Exception ex)
        {
            ltlAlert.Text = "alert('存在选中项是技术部指定的，请去掉技术部指定后再进行忽略操作');";
            return;
        }
        if (dt.Rows.Count > 0)
        {
            int type = 0;
            if (radioPrice.Checked )
            {
                type = 1;
            }
            else
            {
                type = 2;
            }
            helper.FinCancel(dt, type);
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('请选择数据！')";
        }
    }
    private DataTable GetSelectedIdByCancel()
    {
        DataTable table = new DataTable("ApplyDet");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "DetId";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "startDate";
        table.Columns.Add(column);

        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            CheckBox chk = gvRow.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {
                if (gvDet.DataKeys[gvRow.RowIndex].Values["InfoFrom"].ToString().Equals("是"))
                {
                    throw new Exception("存在选中项是技术部指定的，请去掉技术部指定后再进行忽略操作");
                   
                }
                TextBox txtStartDate = gvRow.FindControl("txtStartDate") as TextBox;
                row = table.NewRow();
                row["DetId"] = gvDet.DataKeys[gvRow.RowIndex].Values["DetId"].ToString();
                
                if (txtStartDate != null && txtStartDate.Text.Trim() != "")
                {
                    row["startDate"] = txtStartDate.Text.Trim();
                }
                table.Rows.Add(row);
            }
        }
        return table;
    }
    private DataTable GetSelectedId()
    {
        DataTable table = new DataTable("ApplyDet");
        DataColumn column;
        DataRow row;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "DetId";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "startDate";
        table.Columns.Add(column);

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "EndDate";
        table.Columns.Add(column);

      

        foreach (GridViewRow gvRow in gvDet.Rows)
        {
            CheckBox chk = gvRow.FindControl("chk") as CheckBox;
            if (chk != null && chk.Checked)
            {
                TextBox txtStartDate = gvRow.FindControl("txtStartDate") as TextBox;
                TextBox txtEndDate = gvRow.FindControl("txtEndDate") as TextBox;
                row = table.NewRow();
                row["DetId"] = gvDet.DataKeys[gvRow.RowIndex].Values["DetId"].ToString();
             
                if (txtStartDate != null && txtStartDate.Text.Trim() != "")
                {
                    row["startDate"] = txtStartDate.Text.Trim();
                }
                if (txtEndDate != null && txtEndDate.Text.Trim() != "")
                {
                    row["EndDate"] = txtEndDate.Text.Trim();
                }
              
                table.Rows.Add(row);
            }
        }
        return table;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = GetSelectedId();
        if (dt.Rows.Count > 0)
        {
            int type = 0;
            if (radioPrice.Checked)
            {
                type = 1;
            }
            else
            {
                type = 2;
            }
            string tempFile = Server.MapPath("/docs/QadPrice.xls");
            string outputFile = "QadPrice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            try
            {
                helper.ExportExcel(tempFile, Server.MapPath("../Excel/") + outputFile, dt, type);
            }
            catch (Exception ex)
            {
                  ltlAlert.Text = "alert('" + ex.Message.ToString().Replace("'", "|") + "');";
                  return;
            }
            ltlAlert.Text = "window.open('/Excel/" + outputFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }
        else
        {
            ltlAlert.Text = "alert('请选择数据！')";
        }
        BindData();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindData();
    }
    protected void btnCostExport_Click(object sender, EventArgs e)
    {
        DataTable dt = GetSelectedId();
        if (dt.Rows.Count > 0)
        {
            string costTempFile = Server.MapPath("/docs/QadCost.xls");
            string costOutputFile = "QadCost_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            helper.ExportCostExcel(costTempFile, Server.MapPath("../Excel/") + costOutputFile, dt);
            ltlAlert.Text += "window.open('/Excel/" + costOutputFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        }
        else
        {
            ltlAlert.Text = "alert('请选择数据！')";
        }
       
    }
    protected void gvDet_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnPenging")
        {
            string DetID = e.CommandArgument.ToString();
            Response.Redirect("pcm_Pending.aspx?DetID=" + DetID + "&isPrice=" + (radioPrice.Checked?1:0) + "&status=" +ddlStatus.SelectedItem.Value);
            BindData();
        }
        if (e.CommandName == "lkbtnHist")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string QAD = gvDet.DataKeys[rowIndex].Values["part"].ToString();
            string vender = gvDet.DataKeys[rowIndex].Values["Vender"].ToString();
            ltlAlert.Text = "$.window('历史价格查看', 800, 600,'./price/pcm_HistoryPriceByQADAndVender.aspx?QAD=" + QAD + "&vender=" + vender + "')";
        }
    }
    protected void gvDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDet.PageIndex = e.NewPageIndex;
        BindData();
    }
}