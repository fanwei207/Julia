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
using adamFuncs;
using WOrder;
using WO2Group;

public partial class wo2_wo2_workOrderEnter4 : BasePage
{
    adamClass adam = new adamClass();
    WorkOrder order = new WorkOrder();
    WO2 wo2 = new WO2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["nbr"] != null)
            {
                txtOrder.Text = Request["nbr"];
            }
            if (Request["id"] != null)
            {
                txtOrderID.Text = Request["id"];
            }
            if (Request["ln"] != null)
            {
                txtLine.Text = Server.UrlDecode(Request["ln"]);
            }


            dropSiteBind();
            dropDownClear();

            if (Request["st"] != null)
            {
                dropSite.SelectedValue = Request["st"];
            }

            order.DeleteTmpdate(Convert.ToInt32(Session["Uid"]));
            BindData();

        }

        #region Add Validation
        string scr = @"<script>
           function Addbtn_Click()
           {
                var dpwork = document.getElementById('" + this.dropWorkproc.ClientID + "');";
        scr = scr + @" var dpgroup = document.getElementById('" + this.dropWorkGroup.ClientID + "');";
        scr = scr + @" var dppost = document.getElementById('" + this.dropPostion.ClientID + "');";
        scr = scr + @" var txUser = document.getElementById('" + this.txtUserNo.ClientID + "');";
        scr = scr + @" var txtSpe = document.getElementById('" + this.txtSpecial.ClientID + "');";
        scr = scr + @" var txNum = document.getElementById('" + this.txtComp.ClientID + "');";
        scr = scr + @" var txDate = document.getElementById('" + this.txtEffdate.ClientID + "');";
        scr = scr + @"
                             var workindex = dpwork.selectedIndex;
                       
                             if (dpwork.options[workindex].value == 0)
                             {
                                alert('必须选择工序！');
                                return false;
                             }
                              
                             
                             var groupindex = dpgroup.selectedIndex;
                             if (dpgroup.options[groupindex].value == 0 && txUser.value =='')
                             {
                                alert('必须选择用户组或填写工号！');
                                return false;
                             }
                             else
                             {
                                 var postindex = dppost.selectedIndex;
                                 if  (txUser.value != '' )
                                 {
                                    if(dppost.options[postindex].value == 0 || dpgroup.options[groupindex].value == 0)
                                    {
                                        alert('填写工号必须选择岗位与用户组！');
                                        return false;
                                    }
                                 }
                             }
                             
                             if (txtSpe.value != '')
                             {
                                  if (isNaN(txtSpe.value))
                                        txtSpe.value ='';
                             }

//                             if (txInput.value == '')
//                             {
//                                   alert('必须填写工段线！');
//                                    return false;  
//                             }

                             if (txNum.value == '')
                             {
//                                   alert('必须填写完工数！');
//                                    return false;  
                             }
                             else
                             {
                                  if(isNaN(txNum.value))
                                  {
                                    alert('完工数必须为数字！');
                                    return false;  
                                  }
                             } 

                              if (txDate.value == '')
                             {
                                   alert('必须填写生效日期！');
                                    return false;  
                             }
          
              return true;
             
           }
          </script>";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "AddValidation", scr);
        #endregion
    }

    protected void BindData()
    {

        gvWorkOrder.DataSource = order.GetTmpWorkOder(lblWorkOrderID.Text.Trim().Length > 0 ? Convert.ToInt32(lblWorkOrderID.Text) : 0, Convert.ToInt32(Session["Uid"]));
        gvWorkOrder.DataBind();
    }
    /// <summary>
    /// Bind site data from database by Plancode
    /// </summary>
    private void dropSiteBind()
    {
        dropSite.Items.Clear();
        DataTable dtSite = order.GetDomainSite(Convert.ToInt32(Session["PlantCode"]));
        if (dtSite.Rows.Count > 0)
        {
            ListItem item;
            for (int i = 0; i < dtSite.Rows.Count; i++)
            {
                item = new ListItem(dtSite.Rows[i].ItemArray[0].ToString(), dtSite.Rows[i].ItemArray[0].ToString());
                dropSite.Items.Add(item);
            }
        }
        dropSite.SelectedIndex = 0;
    }

    private void dropWorkprocBind()
    {
        dropWorkproc.Items.Clear();
        dropWorkproc.DataSource = wo2.SelectMOPInfo();
        dropWorkproc.DataBind();

    }

    private void dropWorkGroupBind()
    {
        dropWorkGroup.Items.Clear();
        //dropWorkGroup.DataSource = wo2.SelectGroupInfo();
        dropWorkGroup.DataSource = order.SelectGroupInfo(dropWorkproc.SelectedItem.Text.Substring(0, dropWorkproc.SelectedItem.Text.IndexOf(",")), Convert.ToInt32(Session["PlantCode"]));
        dropWorkGroup.DataBind();

    }

    private void dropPostionBind()
    {
        dropPostion.Items.Clear();
        string[] strMOP;
        int intMopProc = 0;
        if (dropWorkproc.SelectedIndex != 0)
        {
            strMOP = dropWorkproc.SelectedItem.Text.Split(',');
            intMopProc = Convert.ToInt32(strMOP[0]);
        }
        dropPostion.DataSource = wo2.SelectSOPInfo(intMopProc);
        dropPostion.DataBind();

    }

    private void dropDownClear()
    {
        dropWorkproc.Items.Clear();
        dropWorkGroup.Items.Clear();
        dropPostion.Items.Clear();

        ListItem item;
        item = new ListItem("--", "0");
        dropWorkproc.Items.Add(item);
        dropWorkGroup.Items.Add(item);
        dropPostion.Items.Add(item);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Check the Comp Qty
        try
        {
            DateTime dt1 = Convert.ToDateTime(txtEffdate.Text.Trim());
        }
        catch
        {
            ltlAlert.Text = "alert('生效日期有误，请重新输入!')";
            return;
        }
        // if (order.AddWorkOrderPeople(Convert.ToInt32(dropWorkproc.SelectedItem.Text.Substring(0, dropWorkproc.SelectedItem.Text.IndexOf(","))), Convert.ToInt32(dropWorkGroup.SelectedItem.Text.Substring(0, dropWorkGroup.SelectedItem.Text.IndexOf(","))), txtUserNo.Text.Trim(), dropPostion.SelectedIndex >0 ? Convert.ToInt32(dropPostion.SelectedItem.Text.Substring(0, dropPostion.SelectedItem.Text.IndexOf(","))) : 0, Convert.ToInt32(Session["plantCode"]), Convert.ToInt32(lblWorkOrderID.Text), Convert.ToInt32(Session["Uid"]), Convert.ToString(Session["uName"])) < 0)
        //if (order.AddWorkOrderPeople(Convert.ToInt32(dropWorkproc.SelectedItem.Text.Substring(0, dropWorkproc.SelectedItem.Text.IndexOf(","))), Convert.ToInt32(dropWorkGroup.SelectedValue), txtUserNo.Text.Trim(), dropPostion.SelectedIndex > 0 ? Convert.ToInt32(dropPostion.SelectedItem.Text.Substring(0, dropPostion.SelectedItem.Text.IndexOf(","))) : 0, Convert.ToInt32(Session["plantCode"]), Convert.ToInt32(lblWorkOrderID.Text), Convert.ToInt32(Session["Uid"]), Convert.ToString(Session["uName"]), "",txtComp.Text.Trim().Length >0 ? Convert.ToDecimal (txtComp.Text.Trim()) : 0,  txtSpecial.Text.Trim().Length > 0 ? Convert.ToDecimal(txtSpecial.Text) : 0, txtEffdate.Text.Trim().Length > 0 ? txtEffdate.Text.Trim() : DateTime.Today.ToShortDateString()) < 0)
        if (order.AddWorkOrderPeople(Convert.ToInt32(dropWorkproc.SelectedItem.Text.Substring(0, dropWorkproc.SelectedItem.Text.IndexOf(","))), Convert.ToInt32(dropWorkGroup.SelectedValue), txtUserNo.Text.Trim(), dropPostion.SelectedIndex > 0 ? Convert.ToInt32(dropPostion.SelectedItem.Text.Substring(0, dropPostion.SelectedItem.Text.IndexOf(","))) : 0, Convert.ToInt32(Session["plantCode"]), Convert.ToInt32(lblWorkOrderID.Text), Convert.ToInt32(Session["Uid"]), Convert.ToString(Session["uName"]), "", txtComp.Text.Trim().Length > 0 ? Convert.ToDecimal(txtComp.Text.Trim()) : 0, txtSpecial.Text.Trim().Length > 0 ? Convert.ToDecimal(txtSpecial.Text) : 0, txtEffdate.Text.Trim().Length > 0 ? txtEffdate.Text.Trim() : DateTime.Today.ToShortDateString(), dropSite.SelectedValue, lbl_part.Text, lbl_desc.Text) < 0)
        {
            ltlAlert.Text = "alert('增加有错误，请重新操作!')";
            return;
        }
        else
        {
            BindData();
            dropWorkGroup.SelectedIndex = 0;
            if (txtUserNo.Text.Trim().Length == 0)
            {
                dropWorkproc.SelectedIndex = 0;
                dropPostion.SelectedIndex = 0;
                txtSpecial.Text = "";
            }
            txtUserNo.Text = "";
            //txtNum.Text = "";
            //txtInput.Text = "";
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int intflag = order.SaveWorkOrderData(Convert.ToInt32(Session["plantcode"]), Convert.ToInt32(Session["Uid"]), Convert.ToInt32(lblWorkOrderID.Text.Trim()), Convert.ToInt32(dropSite.SelectedValue));
        if (intflag < 0)
        {
            ltlAlert.Text = "alert('保存失败，请重新操作!')";
            return;
        }
        else
        {
            order.DeleteTmpdate(Convert.ToInt32(Session["Uid"]));
            ltlAlert.Text = "alert('保存成功!');window.location.href='/wsline/wl_wsplan5.aspx?rm=" + DateTime.Now + "'";
            if (Request["ty"] != null)
            {
                if (Convert.ToInt32(Request["ty"]) == 0)
                    ltlAlert.Text = "alert('保存成功!'); window.location.href='/wsline/wl_wsplan32.aspx?rm=" + DateTime.Now + "'";
            }

        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        order.DeleteTmpdate(Convert.ToInt32(Session["Uid"]));
        BindData();
    }


    protected void dropWorkproc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropWorkproc.SelectedIndex == 0)
        {
            dropWorkGroup.Items.Clear();
            dropPostion.Items.Clear();
            ListItem item;
            item = new ListItem("--", "0");
            dropWorkGroup.Items.Add(item);
            dropPostion.Items.Add(item);
        }
        else
        {
            dropPostionBind();
            dropWorkGroupBind();
        }
    }


    protected void gvWorkOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (order.DelWorkOrderEntertmp(Convert.ToInt32(gvWorkOrder.DataKeys[e.RowIndex].Value.ToString()), 0, 0) >= 0)
        {
            ltlAlert.Text = "alert('删除成功!')";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('删除数据过程中出错!')";
        }
    }

    protected void gvWorkOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            LinkButton lbtnL = (LinkButton)e.CommandSource;

            GridViewRow row = (GridViewRow)(lbtnL.Parent.Parent);

            int intIndex = row.RowIndex;
            TextBox tb = (TextBox)gvWorkOrder.Rows[intIndex].FindControl("txtDistribution");
            if (order.DelWorkOrderEntertmp(Convert.ToInt32(gvWorkOrder.DataKeys[intIndex].Value.ToString()), tb.Text.Trim().Length > 0 ? Convert.ToDecimal(tb.Text) : 0, 1) >= 0)
            {
                ltlAlert.Text = "alert('修改成功!')";
                BindData();
            }
            else
            {
                ltlAlert.Text = "alert('修改数据过程中出错!')";
            }
        }
    }

    protected void btn_woload_Click(object sender, EventArgs e)
    {
        if (txtOrder.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('加工单号不能为空!')";
            return;
        }
        if (txtOrderID.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('加工单ID不能为空!')";
            return;
        }
        if (txtLine.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('流水线不能为空!')";
            return;
        }
        
        
        WorkOrderInfor wk = order.GetPlanWorkOrder(Convert.ToInt32(Session["Plantcode"]), txtOrder.Text.Trim(), txtOrderID.Text.Trim(), dropSite.SelectedValue, txtLine.Text.Trim());
        if (wk.Part.Trim().Length != 0)
        {
            lbl_cc.Text = wk.Center;
            lbl_part.Text = wk.Part;
            lbl_desc.Text = wk.PartDesc;
            lblTec.Text = wk.Tec;
            lblWorkOrderID.Text = wk.WorkOrderID.ToString();
            lblPlan.Text = wk.Order.ToString();
            dropWorkprocBind();
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('输入的加工单信息不正确!')";
            return;
        }
    }

    protected void gvWorkOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ((LinkButton)e.Row.FindControl("lbtnDVDelete")).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + e.Row.Cells[2].Text + "\"吗?')");
            }
        }

    }

}
