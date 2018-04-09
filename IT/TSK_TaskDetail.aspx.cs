using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using IT;

public partial class TSK_TaskDetail : System.Web.UI.Page
{
    adamClass chk = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStdDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            txtStdHour.Text = DateTime.Now.Hour.ToString();

            #region 判断各按钮状态，及初始化信息
            DataTable table = TaskHelper.SelectTaskMstrByNbr(Request.QueryString["tskNbr"]);
            if (table == null || table.Rows.Count <= 0)
            {
                btnDone.Enabled = false;
                this.ltlAlert.Text = "alter('任务获取失败！请返回前一页！'";
            }
            #endregion

            BindCharger();
        }
    }
    protected void BindCharger()
    {
        DataTable table = TaskHelper.GetUsers(string.Empty, 404);
        dropCharger.Items.Clear();

        dropCharger.DataSource = table;
        dropCharger.DataBind();
        dropCharger.Items.Insert(0, new ListItem("请选择负责人", "0"));
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        #region 先检验开始日期格式
        if (string.IsNullOrEmpty(txtTaskDesc.Text))
        {
            ltlAlert.Text = "alert('任务内容 不能为空！');";
            return;
        }
        else if(txtTaskDesc.Text.Length < 10)
        {
            ltlAlert.Text = "alert('任务内容 至少应该10字以上！');";
            return;
        }

        if (string.IsNullOrEmpty(txtStdDate.Text))
        {
            ltlAlert.Text = "alert('开始日期 不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _d = Convert.ToDateTime(txtStdDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('开始日期 格式不正确！');";
                return;
            }
        }

        if (string.IsNullOrEmpty(txtStdHour.Text))
        {
            ltlAlert.Text = "alert('开始小时 不能为空！');";
            return;
        }
        else
        {
            try
            {
                int _n = Convert.ToInt32(txtStdHour.Text);

                if (_n < 8 || _n > 17)
                {
                    ltlAlert.Text = "alert('开始小时 只能在8：00-17：00范围内！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('开始小时 只能是数字！');";
                return;
            }
        }

        if (dropHour.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择一项 预计时长！');";
            return;
        }

        //如果选小时，则不应大于8
        if (Convert.ToDecimal(dropHour.SelectedValue) >= 8 && dropUnit.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('时长大于8小时的，应算一天！请选择单位 天！');";
            return;
        }

        if (dropCharger.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('必须选择一个 负责人！');";
            return;
        }

        if (radlType.SelectedIndex == -1)
        {
            ltlAlert.Text = "alert('必须选择一个 任务类型！');";
            return;
        }
        #endregion

        #region 再计算一次，保证前后结果一致性
        DateTime _expireDate;

        if (dropUnit.SelectedIndex == 0)
        {
            //超过当天17点的，从第二天8点追加
            DateTime _d1 = Convert.ToDateTime(txtStdDate.Text).AddHours(Convert.ToInt32(txtStdHour.Text)).AddHours(Convert.ToDouble(dropHour.SelectedValue));
            DateTime _d2 = Convert.ToDateTime(txtStdDate.Text + " 17:00:00");
            DateTime _d3 = Convert.ToDateTime(txtStdDate.Text + " 08:00:00");

            if (_d1 > _d2)
            {
                _d3 = _d3.AddDays(1).AddHours((_d1 - _d2).Hours).AddMinutes((_d1 - _d2).Minutes);
            }
            else
            {
                _d3 = _d1;
            }

            _expireDate = _d3;
        }
        else
        {
            _expireDate = Convert.ToDateTime(txtStdDate.Text).AddHours(Convert.ToInt32(txtStdHour.Text)).AddDays(Convert.ToDouble(dropHour.SelectedValue));
        }

        //如果前后计算不一致，则要重新选择
        if (Convert.ToDateTime(string.Format("{0:yyyy-MM-dd HH:mm}", _expireDate)) != Convert.ToDateTime(txtExpireDate.Text))
        {
            dropHour.SelectedIndex = 0;
            dropUnit.SelectedIndex = 0;
            dropCharger.SelectedIndex = 0;

            txtExpireDate.Text = string.Empty;

            ltlAlert.Text = "alert('开始时间 和 截至时间 计算不一致！');";

            return;
        }

        #endregion

        string _chargerName = dropCharger.SelectedItem.Text.IndexOf("--") < 0 ? dropCharger.SelectedItem.Text : dropCharger.SelectedItem.Text.Substring(0, dropCharger.SelectedItem.Text.IndexOf("--"));
        string _chargerEmail = dropCharger.SelectedItem.Text.IndexOf("--") < 0 ? string.Empty : dropCharger.SelectedItem.Text.Substring(dropCharger.SelectedItem.Text.IndexOf("--") + 2);

        if (TaskHelper.InsertTaskDet(Request.QueryString["tskNbr"], txtTaskDesc.Text
            , Convert.ToDateTime(txtStdDate.Text).AddHours(Convert.ToInt32(txtStdHour.Text)).ToString(), dropHour.SelectedItem.Text, dropUnit.SelectedItem.Text
            , txtExpireDate.Text
            , dropCharger.SelectedValue, _chargerName, _chargerEmail, Session["uID"].ToString(), Session["uName"].ToString()
            ,ckbMonopoly.Checked, radlType.SelectedValue))
        {
            ltlAlert.Text = "window.opener.location.reload();";
            ltlAlert.Text += "window.close();";
          
        }
        else
        {
            ltlAlert.Text = "alert('保存失败！');";
            return;
        }
    }
    protected void dropHour_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region 先检验开始日期格式
        if (string.IsNullOrEmpty(txtStdDate.Text))
        {
            ltlAlert.Text = "alert('开始日期 不能为空！');";
            return;
        }
        else
        {
            try
            {
                DateTime _d = Convert.ToDateTime(txtStdDate.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('开始日期 格式不正确！');";
                return;
            }
        }

        if (string.IsNullOrEmpty(txtStdHour.Text))
        {
            ltlAlert.Text = "alert('开始小时 不能为空！');";
            return;
        }
        else
        {
            try
            {
                int _n = Convert.ToInt32(txtStdHour.Text);

                if (_n < 8 || _n > 17)
                {
                    ltlAlert.Text = "alert('开始小时 只能在8：00-17：00范围内！');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('开始小时 只能是数字！');";
                return;
            }
        }
        #endregion

        if (dropUnit.SelectedIndex == 0)
        {
            //超过当天17点的，从第二天8点追加
            DateTime _d1 = Convert.ToDateTime(txtStdDate.Text).AddHours(Convert.ToInt32(txtStdHour.Text)).AddHours(Convert.ToDouble(dropHour.SelectedValue));
            DateTime _d2 = Convert.ToDateTime(txtStdDate.Text + " 17:00:00");
            DateTime _d3 = Convert.ToDateTime(txtStdDate.Text + " 08:00:00");

            if (_d1 > _d2)
            {
                _d3 = _d3.AddDays(1).AddHours((_d1 - _d2).Hours).AddMinutes((_d1 - _d2).Minutes);
            }
            else
            {
                _d3 = _d1;
            }

            txtExpireDate.Text = string.Format("{0:yyyy-MM-dd HH:mm}", _d3); 
        }
        else
        {
            txtExpireDate.Text = string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(txtStdDate.Text).AddHours(Convert.ToInt32(txtStdHour.Text)).AddDays(Convert.ToDouble(dropHour.SelectedValue)));
        }
        ChargerShow();
    }
    protected void dropCharger_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropCharger.SelectedIndex == 0)
        {
            lblMonopoly.Visible = false;
          
        }
        else
        {
            ChargerShow();
        }
    }
    protected void ChargerShow()
    {
        string _chargerID = dropCharger.SelectedValue;

        Charger item = TaskHelper.GetChargerTaskDets(_chargerID, DateTime.Now);
        if (dropCharger.SelectedIndex != 0)
        {
            if (item == null)
            {
               
                btnDone.Enabled = true;
                lblMonopoly.Visible = false;

            }
            else
            {
                if (item.Monopoly == "Yes")
                {
                    btnDone.Enabled = false;
                    lblMonopoly.Visible = true;
                    lblMonopoly.Text = "已有独占任务，不可分配其他任务";
                }
                else
                {
                    btnDone.Enabled = true;
                    lblMonopoly.Visible = false;
                }
                
            }
        }
        if(dropHour.SelectedIndex!=0 )
        {
            if (item!=null)
            {
                if (item.Hours != null)
                {
                    if (dropUnit.SelectedValue == "HOUR")
                    {
                        double huor = Convert.ToInt32(item.Hours) + Convert.ToDouble(dropHour.SelectedItem.Text);
                        if (huor > 8)
                        {
                            btnDone.Enabled = false;
                            lblMonopoly.Visible = true;
                            lblMonopoly.Text = "当天已有任务" + item.Hours + "小时，一天任务不可超过8小时";
                        }
                        else
                        {
                            btnDone.Enabled = true;
                            lblMonopoly.Visible = false;
                        }
                    }
                    else if (dropUnit.SelectedValue == "DAY")
                    {

                        DateTime _d2 = Convert.ToDateTime(txtStdDate.Text + " 17:00:00");
                        TimeSpan ts2 = _d2 - DateTime.Now; ;
                        double time2 = ts2.TotalHours + Convert.ToInt32(item.Hours);
                        if (time2 > 8)
                        {
                            btnDone.Enabled = false;
                            lblMonopoly.Visible = true;
                            lblMonopoly.Text = "当天任务超过8小时";
                        }
                        else
                        {
                            btnDone.Enabled = true;
                            lblMonopoly.Visible = false;
                        }
                    }
                }
            }
        }
    }
    protected void dropUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dropUnit.SelectedIndex == 0)
        {
            ckbMonopoly.Enabled = false;
            ckbMonopoly.Checked = false;
        }
        else
        {
            ckbMonopoly.Enabled = true;
            
        }
        ChargerShow();
    }
}