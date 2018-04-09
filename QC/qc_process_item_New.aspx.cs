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
using QCProgress;


public partial class qc_process_item_New : BasePage
{
    QC oqc = new QC();

    GridViewNullData ogv = new GridViewNullData();

    protected string FormType
    {
        get
        {
            if (Request.QueryString["type"] != null)
            {
                return Request.QueryString["type"];
            }
            else
            {
                return "";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblLine.Text = Request.QueryString["line"].ToString();
            lblOrder.Text = Request.QueryString["mar"].ToString();
            lblQty.Text = Request.QueryString["qty"].ToString();
            lblID.Text = Request.QueryString["id"].ToString();

            DataTable table = oqc.GetProcessByID_New(int.Parse(lblID.Text));
            txtInspector.Text = table.Rows[0][1].ToString();

            if (table.Rows[0][2].ToString() != string.Empty)
                txtDate.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Parse(table.Rows[0][2].ToString()));
            else
                txtDate.Text = string.Empty;

            dropType.DataSource = oqc.GetProcessDefectType_New(Session["uID"].ToString());
            dropType.DataBind();
            //dropType.Items.Insert(0, new ListItem("--", "0"));
            string selectedValue = oqc.GetProcessDefectTypeById_New(lblID.Text).ToString();
            if (selectedValue != "0")
            {
                dropType.Items.FindByValue(selectedValue).Selected = true;
            }


            if (FormType == "read")
            {
                btnSave.Visible = false;
            }

            GridTypeBind();
            GridProcedureBind(0,0);

            btnBack.Attributes.Add("onclick", "window.close();");
        }
    }   

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("qc_process.aspx");
    }

    protected void GridTypeBind() 
    {
        int tID = int.Parse(dropType.SelectedValue);
        int prcID = int.Parse(lblID.Text);

        dgProcedure.DataSource = oqc.GetProcessItem_New(prcID, tID);
        dgProcedure.DataBind();
    }

    protected void GridProcedureBind(int prcdID,int prcdItemID) 
    {
        dgDefect.DataSource = oqc.GetProcessDefect_New(prcdID, prcdItemID);
        dgDefect.DataBind();
    }

    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgProcedure.SelectedIndex = -1;
        GridProcedureBind(0, 0);
        GridTypeBind();
    }

    int ntotalpro = 0; 
    protected void dgProcedure_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            if (e.Item.ItemIndex != -1)
            {
                int id = e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = id.ToString();
            }

            e.Item.Cells[1].Style.Add("text-align", "left");

            ((Button)e.Item.Cells[3].FindControl("btnAdd")).Visible = true;

            e.Item.Cells[2].Text = e.Item.Cells[6].Text.Trim();

            if (e.Item.Cells[6].Text.Trim() != string.Empty)
                ntotalpro += int.Parse(e.Item.Cells[6].Text.Trim());

            if (FormType == "read")
            {
                ((Button)e.Item.Cells[3].FindControl("btnAdd")).Text = "查看";
            }
        }
        else if (e.Item.ItemType == ListItemType.SelectedItem)
        {
            if (e.Item.ItemIndex != -1)
            {
                int id = e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = id.ToString();
            }

            ntotalpro += int.Parse(e.Item.Cells[6].Text.Trim());

            TextBox txt = (TextBox)e.Item.Cells[2].FindControl("txtTotal");

            txt.Text = e.Item.Cells[6].Text.Trim();

            ((Button)e.Item.Cells[3].FindControl("btnAdd")).Visible = false;
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            e.Item.Cells[1].Style.Add("text-align", "right");
            e.Item.Cells[1].Text = "合计数:";

            e.Item.Cells[2].Text = ntotalpro.ToString();
        }
    }

    int nTotal = 0;
    protected void dgDefect_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
        {
            if (e.Item.ItemIndex != -1)
            {
                int id = e.Item.ItemIndex + 1;
                e.Item.Cells[0].Text = id.ToString();
            }

            e.Item.Cells[1].Style.Add("text-align", "left");

            TextBox txtNum = (TextBox)e.Item.Cells[2].FindControl("txtNum");
            txtNum.Text = e.Item.Cells[4].Text;

            nTotal += int.Parse(e.Item.Cells[4].Text);
        }
        else if (e.Item.ItemType == ListItemType.Footer)
        {
            e.Item.Cells[1].Text = "合计:";
            e.Item.Cells[2].Text = nTotal.ToString();

            e.Item.Cells[1].Style.Add("text-align", "right");
        }
    }
    protected void dgProcedure_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add") 
        {
            bool bFlag = true;
            bool bDef = true;
            bool bPro = false;

            int prcdID = 0;
            int prcdItemID = 0;
            int total = 0;

            if (dgProcedure.SelectedIndex > -1)
            {
                TextBox txtTotal = (TextBox)dgProcedure.SelectedItem.Cells[2].FindControl("txtTotal");

                if (txtTotal.Text.Trim() == string.Empty)
                {
                    ltlAlert.Text = "alert('合计不能有为空!');";
                    bFlag = false;
                }
                else
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
                    if (!regex.IsMatch(txtTotal.Text.Trim()))
                    {
                        ltlAlert.Text = "alert('合计的格式不对!');";
                        bFlag = false;
                    }
                    else if (int.Parse(txtTotal.Text.Trim()) == int.Parse(dgProcedure.SelectedItem.Cells[6].Text.Trim()))
                        bPro = true;
                    else
                        total = int.Parse(txtTotal.Text.Trim());
                }

                int prcID = int.Parse(lblID.Text);
                int typeID = int.Parse(dropType.SelectedValue);

                prcdID = int.Parse(dgProcedure.SelectedItem.Cells[5].Text);

                string strDef = "";
                string strNum = "";
                int nNum = 0;
                if (FormType != "read")
                {
                    foreach (DataGridItem item in dgDefect.Items)
                    {
                        TextBox txtNum = (TextBox)item.Cells[2].FindControl("txtNum");
                        string orgNum = item.Cells[4].Text.Trim();

                        if (txtNum.Text.Trim() == string.Empty)
                        {
                            ltlAlert.Text = "alert('数量不能有为空!');";
                            bFlag = false;
                            break;
                        }
                        else
                        {
                            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d+$");
                            if (!regex.IsMatch(txtNum.Text.Trim()))
                            {
                                ltlAlert.Text = "alert('数量的格式不对!');";
                                bFlag = false;
                                break;
                            }

                            nNum += int.Parse(txtNum.Text.Trim());
                        }

                        if (txtNum.Text.Trim() != orgNum)
                        {
                            bDef = false;

                            strDef += item.Cells[3].Text.Trim() + ";";
                            strNum += txtNum.Text.Trim() + ";";
                        }
                    }

                    if (bFlag && int.Parse(txtTotal.Text.Trim()) == 0 && nNum != 0)
                    {
                        ltlAlert.Text = "alert('合计数量为0,但缺陷数量不为0,和实际情况不符!');";
                        bFlag = false;
                    }

                    if (bFlag && int.Parse(txtTotal.Text.Trim()) != 0 && nNum == 0)
                    {
                        ltlAlert.Text = "alert('合计数量不为0,但缺陷数量为0,和实际情况不符!');";
                        bFlag = false;
                    }

                    if (!(bPro && bDef)) //若右数据没有被更改，那么就不做数据库操作
                    {
                        if (bFlag && !oqc.AddProcessItem_New(prcID, typeID, prcdID, total, strDef, strNum, bPro, bDef))
                        {
                            ltlAlert.Text = "alert('保存失败!');";
                            bFlag = false;
                        }
                    }
                }
            }

            chkFlag.Checked = bFlag;

            if (bFlag)
                dgProcedure.SelectedIndex = e.Item.ItemIndex;

            prcdID = int.Parse(dgProcedure.SelectedItem.Cells[5].Text.Trim());
            prcdItemID = int.Parse(dgProcedure.SelectedItem.Cells[4].Text.Trim());

            GridTypeBind();
            GridProcedureBind(prcdID, prcdItemID);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(dgProcedure.SelectedIndex != -1)
            dgProcedure_ItemCommand(this, new DataGridCommandEventArgs(dgProcedure.SelectedItem, (Button)dgProcedure.SelectedItem.Cells[3].FindControl("btnAdd"), new CommandEventArgs("Add", null)));

        if (chkFlag.Checked == false) 
        {
            return;
        }

        if (txtInspector.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('检验人员不能为空!');";
            return;
        }
        else
        {
            //int type = 547;//检测员的roleid
            int plantId = Convert.ToInt32(Session["PlantCode"].ToString());
            String userId = txtInspector.Text.Replace("；", ";");
            if (!oqc.checkUserNo(userId, plantId))
            {
                ltlAlert.Text = "alert('检验人员填写错误')";
                txtInspector.Focus();
                return;
            }
        
        }

        if (txtDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('检验日期不能为空!');";
            return;
        }
        else 
        {
            try
            {
                DateTime _dd = DateTime.Parse(txtDate.Text.Trim());
            }
            catch 
            {
                ltlAlert.Text = "alert('检验日期格式不对!');";
                return;
            }
        }

        int prcID = int.Parse(lblID.Text);
        int total = 0;
        string inspector = txtInspector.Text.Trim();
        string checkdate = txtDate.Text.Trim();

        int nRet = oqc.UpdateProcessByID_New(prcID, total, inspector, checkdate);

        if (nRet == 1)
            ltlAlert.Text = "alert('检验员不存在');";
        else if (nRet == 0)
        {
            ltlAlert.Text = "alert('保存成功');";
        }
        else
            ltlAlert.Text = "alert('保存失败');";

        dgProcedure.SelectedIndex = -1;
        GridTypeBind();
        GridProcedureBind(0, 0);
    }   
}
