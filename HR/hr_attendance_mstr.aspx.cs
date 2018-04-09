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
using Wage;

public partial class HR_hr_attendance_mstr : BasePage
{
    HR hr_salary = new HR();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropDepartmentBind();

            txtYear.Text = DateTime.Now.Year.ToString();
            dropMonth.SelectedValue = DateTime.Now.Month.ToString();
            txtDay.Text = DateTime.Now.Day.ToString();

            gvAttendance.DataBind();
            lblName.Text = "";
            lbluserID.Text = "0";
        }

        #region Check Attendance information for Time
        string scr = @"<script>
            function btnSaveClick() 
            {
                 //--------- judge year  ----------- //
               var tYear = document.getElementById('" + this.txtYear.ClientID + "');";
               scr = scr + @"
                  var partter = /^[0-9]*[1-9][0-9]*$/;
                 if (tYear.value !='')
                 {
                     if (!partter.test(tYear.value))
                     {
                        alert('年份输入不规范,如2004 ！');
                        tYear.focus();
                        return false; 
                     }

                 }
                 else
                 { 
                     alert('年份不能为空！');
                     tYear.focus();
                     return false;
                 }
                 //------------judge day -----------//
              var tDay = document.getElementById('" + this.txtDay.ClientID +"');";
              scr = scr + @"
                 if (tDay.value == '')
                 {
                     alert('日期不能为空！');
                     tDay.focus();
                     return false;
                 }
                 else
                 {
                     if (!partter.test(tDay.value))
                     {
                        alert('日期必须为正整数');
                        tDay.focus();
                        return false;
                     }
                 }

                 //------judge time -------------//
              var s1 = document.getElementById('" + this.txtStartTime.ClientID +"');";
              scr = scr + @"
                  var timepartter =/^[1-9]\d*|0$/;
                  if (s1.value == '')
                  {
                      alert('上班时间1不能为空！');
                      s1.focus();
                      return false;
                  }
                  else
                  {
                      if ( (s1.value).length < 4)
                      {
                          alert('请输入正确的上班时间1，4位xxxx格式，前2位为几点，后2位是几分');
                          s1.focus();
                          return false;
                      }
                      else
                      {
                         if (!timepartter.test(s1.value))
                         {
                             alert('请输入正确的上班时间1，4位xxxx格式，前2位为几点，后2位是几分');
                             s1.focus();
                             return false;
                         }
                         if (s1.value.substring(0,2) > 23 || s1.value.substring(2,2) > 60)
                         {
                             alert('请输入正确的上班时间1，4位xxxx格式，前2位为几点，后2位是几分');
                             s1.focus();
                             return false;
                         }
                      }  
                   }

               var s2 = document.getElementById('" + this.txtEndTime.ClientID + "');";
               scr = scr + @"
                  if (s2.value == '')
                  {
                      alert('下班时间1不能为空！');
                      s2.focus();
                      return false;
                  }
                  else
                  {
                      if ( (s2.value).length < 4)
                      {
                          alert('请输入正确的下班时间1，4位xxxx格式，前2位为几点，后2位是几分');
                          s2.focus();
                          return false;
                      }
                      else
                      {
                         if (!timepartter.test(s2.value))
                         {
                             alert('请输入正确的下班时间1，4位xxxx格式，前2位为几点，后2位是几分');
                             s2.focus();
                             return false;
                         }
                         if (s2.value.substring(0,2) > 23 || s2.value.substring(2,2) > 60)
                         {
                             alert('请输入正确的下班时间1，4位xxxx格式，前2位为几点，后2位是几分');
                             s2.focus();
                             return false;
                         }
                      }  
                   }
                
               var s3 = document.getElementById('" + this.txtRestTime.ClientID + "');";
               scr = scr + @"
                   var checkfloat =/[0-9.]+/;
                   if (s3.value !='')
                   {
                        if ( ! checkfloat.test(s3.value))
                        {
                           alert('休息时间1必须为数字');
                           s3.focus();
                           return false;
                        }
                   } 

               var e1 =document.getElementById('" + this.txtSTime.ClientID + "');";
               scr = scr + @"
                  if (e1.value != '')
                  {                    
                      if ( (e1.value).length < 4)
                      {
                          alert('请输入正确的上班时间2，4位xxxx格式，前2位为几点，后2位是几分');
                          e1.focus();
                          return false;
                      }
                      else
                      {
                         if (!timepartter.test(e1.value))
                         {
                             alert('请输入正确的上班时间2，4位xxxx格式，前2位为几点，后2位是几分');
                             e1.focus();
                             return false;
                         }

                         if (e1.value.substring(0,2) > 23 || e1.value.substring(2,2) > 60)
                         {
                             alert('请输入正确的上班时间2，4位xxxx格式，前2位为几点，后2位是几分');
                             e1.focus();
                             return false;
                         }
                      }  
                   }


                 
               var e2 =document.getElementById('" + this.txtETime.ClientID + "');";
               scr = scr + @"
                  if (e2.value != '')
                  {                    
                      if ( (e2.value).length < 4)
                      {
                          alert('请输入正确的下班时间2，4位xxxx格式，前2位为几点，后2位是几分');
                          e2.focus();
                          return false;
                      }
                      else
                      {
                         if (!timepartter.test(e2.value))
                         {
                             alert('请输入正确的下班时间2，4位xxxx格式，前2位为几点，后2位是几分');
                             e2.focus();
                             return false;
                         }

                         if (e2.value.substring(0,2) > 23 || e2.value.substring(2,2) > 60)
                         {
                             alert('请输入正确的下班时间2，4位xxxx格式，前2位为几点，后2位是几分');
                             e2.focus();
                             return false;
                         }
                      }  
                   }

               var e3 = document.getElementById('" + this.txtRTime.ClientID + "');";
               scr = scr + @"
                   var checkfloat =/[0-9.]+/;
                   if (s3.value !='')
                   {
                        if ( ! checkfloat.test(s3.value))
                        {
                           alert('休息时间1必须为数字');
                           s3.focus();
                           return false;
                        }
                   } 
                
              if ((e1.value != '' && e2.value == '') || (e2.value != '' && e1.value == '') )
              {
                   alert('缺少上班时间2或下班时间2 ！');
                   e1.focus();
                   return false;
              }

              //-------------    Condition(userNo / Department)       -----------------//
              var userNo = document.getElementById('" + this.txtInputUser.ClientID + "');";
               scr = scr + @"
              var department = document.getElementById('" + this.dropDepartment.ClientID + "');";
               scr = scr + @"
                   if ( userNo.value =='' && department.value == 0)
                   {
                       alert('必须选择工号或部门');
                       department.focus();
                       return false;
                   }
               
            }
             </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Check", scr);
        #endregion

    }

    private void dropDepartmentBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropDepartment.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDepartment.Items.Add(item);
            }
        }
        dropDepartment.SelectedIndex = 0;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtDay.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('日 不能为空！');";
            return;
        }
        else
        {
            try
            {
                Int32 _n = Convert.ToInt32(txtDay.Text.Trim());
                if (_n <= 0 || _n > 31)
                {
                    ltlAlert.Text = "alert('日 只能是1 - 31之间的整数！');";
                    return;
                }
                else 
                {
                    try
                    {
                        DateTime _dt = Convert.ToDateTime(txtYear.Text.Trim() + "-" + dropMonth.SelectedValue + "-" + txtDay.Text.Trim());
                    }
                    catch
                    {
                        ltlAlert.Text = "alert('年 月 日 的组合成的日期不正确！');";
                        return;
                    }
                }
            }
            catch
            {
                ltlAlert.Text = "alert('日 只能是整数！');";
                return;
            }
        }

        gvAttendance.PageIndex = 0;
        gvAttendance.DataBind();
    }

    //Save the attendance data
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime  dtmDate = Convert.ToDateTime(txtYear.Text.Trim() +"-" + dropMonth.SelectedValue +"-" + txtDay.Text.Trim());

            // Judge Userno is variable
            if (lblName.Text.Trim().Length == 0 && dropDepartment.SelectedIndex == 0)
            {
                return;
            }

            decimal decStart1, decEnd1, decRest1, decTotal, decDays,decT1,decT2,decStart2, decEnd2, decRest2;
            decT1 =0;
            decT2 =0;
            decStart2=0;
            decEnd2 = 0;
            decRest2 =0;
            decTotal = 0;

            decStart1 = Convert.ToInt32(txtStartTime.Text.Trim().Substring(0, 2)) + Convert.ToDecimal(txtStartTime.Text.Trim().Substring(2, 2)) / 60;
            decEnd1 = Convert.ToInt32(txtEndTime.Text.Trim().Substring(0, 2)) + Convert.ToDecimal(txtEndTime.Text.Trim().Substring(2, 2)) / 60;

            decRest1 = Convert.ToDecimal(txtRestTime.Text.Trim());
            decT1 = decEnd1 - decStart1 - decRest1;

            if (decT1 <0)
            {
                decT1 =decT1 + 24;
                decEnd1 = decEnd1 +24;
            }

            if (txtSTime.Text.Trim().Length !=0)
            {
                decStart2 = Convert.ToInt32(txtSTime.Text.Trim().Substring(0, 2)) + Convert.ToDecimal(txtSTime.Text.Trim().Substring(2, 2)) / 60;
                decEnd2 = Convert.ToInt32(txtETime.Text.Trim().Substring(0, 2)) + Convert.ToDecimal(txtETime.Text.Trim().Substring(2, 2)) / 60;               
                decRest2 = Convert.ToDecimal(txtRTime.Text.Trim().Length !=0 ? txtRTime.Text.Trim() : "0");
                decT2 =decEnd2 - decStart2 - decRest2;
                 if (decT2 <0)
                {
                    decT2 =decT2 + 24;
                    decEnd2 = decEnd2 +24;
                }
            }

            decTotal = decT1 + decT2;

            if (decTotal / 8 > 1)
                decDays = 1;
            else
                decDays = decTotal / 8;

            int intMid, intNight, intWhole;
            intMid = 0;
            intNight = 0;
            intWhole = CheckNightwork(decStart1,decEnd1,decT1,decStart2,decEnd2,decT2,2);
            if (intWhole == 0)
            {
                intNight = CheckNightwork(decStart1,decEnd1,decT1,decStart2,decEnd2,decT2,1);
                if (intNight == 0)
                    intMid = CheckNightwork(decStart1,decEnd1,decT1,decStart2,decEnd2,decT2,0);
            }


            int intError = hr_salary.SaveAttendance(dtmDate.ToShortDateString(), Convert.ToInt32(dropDepartment.SelectedValue), txtInputUser.Text, lblName.Text, txtInputUser.Text.Length ==0 ? 0 : Convert.ToInt32(lbluserID.Text), Convert.ToInt32(Session["PlantCode"]),
                Convert.ToInt32(Session["Uid"]), txtStartTime.Text.Trim(), txtETime.Text.Trim().Length != 0 ? txtETime.Text.Trim() : txtEndTime.Text.Trim(), decRest1 + decRest2, decTotal, decDays, intMid, intNight, intWhole, txtStartTime.Text.Trim(), txtEndTime.Text.Trim(), decRest1, txtSTime.Text.Trim(), txtETime.Text.Trim(), decRest2, decT2);
            if (intError < 0)
            {
                string strScr = @"<script language='javascript'> alert('保存出错,请重新操作'); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SaveError", strScr);
                return;
            }
            else
            {
                if (intError == 1)
                {
                    string strScr = @"<script language='javascript'> alert('所选部门或工号数据已存在'); </script>";
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Repeat", strScr);
                    return;
                }
                else
                {
                    if (dtmDate.AddDays(1).Day == 1)
                    {
                        dropMonth.SelectedValue = dtmDate.AddDays(1).Month.ToString();
                        txtDay.Text = "1";
                    }
                    else
                    {
                        txtDay.Text = dtmDate.AddDays(1).Day.ToString();
                    }
                    txtDay.Focus();
                    gvAttendance.PageIndex = 0;
                    gvAttendance.DataBind();
                }
            }

        }
        catch
        {
            string strScr = @"<script language='javascript'> alert('日期不正确或保存出错');</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", strScr);
        }
    }

    private int CheckNightwork(decimal decS1,decimal decE1, decimal decT1,decimal decS2,decimal decE2, decimal decT2,int intType)
    {
        int intAmount = 0;
        if (decE1 == decS2)
        {
            decT1 = decT1 + decT2;
            decT2 = 0;
            decE1 = decE2;
        }

        switch (intType) 
        {
            case 0 :
                if ((decT1 >= 8 && decE1 <= 24 && decE1 >= 22) || (decT2 >= 8 && decE2 <= 24 && decE2 >= 22))
                        intAmount =1;
                    break ;
            case 1 :
                if ((decT1 >= 8 && (decE1 > 24 || (decS1 >= 0 && decS1 <= 3))) || (decT2 >= 8 && (decE2 > 24 || (decS2 >= 0 && decS2 <= 3))))
                        intAmount = 1;
                    break ;
           default:
                   if ((decT1 >= 12 && decS1 <= 22 && decE1 >= 30) || (decT2 >= 12 && decS2 <= 22 && decE2 >= 30) || (decT1 + decT2 >=24))
                        intAmount = 1;
                    break ;
        }

        return intAmount;
    }


    protected void MyRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int intFlag = 0;
        try
        {
            int intDelID = Convert.ToInt32(gvAttendance.DataKeys[e.RowIndex].Value);

            intFlag =hr_salary.DelAttendanc(intDelID, Convert.ToInt32(Session["PlantCode"]));

        }
        catch
        {
            intFlag =-1;
        }

        if (intFlag < 0)
        {
            string str = @"<script language='javascript'> alert('删除失败！'); </script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DeteErro", str);
        }
        else
        {
            gvAttendance.DataBind();
        }

    }
    protected void txtInputUser_TextChanged(object sender, EventArgs e)
    {
        string strUserName;
       
        if (txtInputUser.Text.Trim().Length != 0)
        {
            strUserName = hr_salary.CheckTimeUser(txtInputUser.Text, Convert.ToInt32(Session["PlantCode"]), txtYear.Text.Trim() + "-" + dropMonth.SelectedValue + "-" + txtDay.Text.Trim());
            if (strUserName.Length == 0 || strUserName.IndexOf(',') == -1)
            {
                string strScr = @"<script language='javascript'> alert('工号不存在或此员工属于离职员工或不是计时工！');form1.txtInputUser.focus(); </script>";
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "User", strScr);
                lblName.Text = "";
                lbluserID.Text = "0";
                return;
            }
            else
            {
                string[] strUser = strUserName.Split(',');
                lblName.Text  = strUser[1];
                lbluserID.Text  = strUser[0];
                txtStartTime.Focus();
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.ExportExcel(adam.dsn0()
                , hr_salary.Attendance(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(),
                                                Convert.ToInt32(dropDepartment.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]), chkAll.Checked, Convert.ToInt32(txtDay.Text.Trim()), 1)
                , hr_salary.Attendance(Convert.ToInt32(txtYear.Text.Trim()), Convert.ToInt32(dropMonth.SelectedValue), txtUserNo.Text.Trim(), txtUserName.Text.Trim(),
                                                Convert.ToInt32(dropDepartment.SelectedValue), Convert.ToInt32(Session["PlantCode"]), Convert.ToInt32(Session["Uid"]), chkAll.Checked, Convert.ToInt32(txtDay.Text.Trim()), 0)
                , false);
    }
}
