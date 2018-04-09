using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace Wage
{
    /// <summary>
    /// Summary description for hr_TimeSalary
    /// </summary>
    public class hr_TimeSalary
    {
        public hr_TimeSalary()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private int t_userID;           //工号ID
        private string t_userNo;        //工号
        private string t_userName;      //姓名
        private string t_salaryDate;    //工资日期
        private decimal t_basic;        //基本工资
        private decimal t_benefit;      //效益工资
        private decimal t_nightWork;    //中夜班费
        private decimal t_allowance;    //津贴
        private decimal t_subsidies;    //补贴
        private decimal t_assess;       //考核奖（加班费）
        private decimal t_holiday;      //国假工资
        private decimal t_duereward;    //应发金额
        private decimal t_hfound;       //公积金
        private decimal t_rfound;       // 养老+医疗+补充养老
        private decimal t_member;       //工会费
        private decimal t_deduct;       //扣款
        private decimal t_tax;          //税
        private decimal t_workpay;      //实发
      
        private bool t_fire;             //是否离职
        private decimal t_currdeduct;   //当月应扣款
        private decimal t_lastdeduct;   //当月剩余扣款
        private decimal t_annual;       //年休天数
        private decimal t_hday;         //国假天数
        private decimal t_sleave;       //病假天数
        private decimal t_sleavepay;    //病假工资
        private int t_department;       //部门ID
        private int t_employTypeID;     //用工性质ID 
        private int t_insuranceTypeID;  //保险类型ID
        private int t_worktype;         //计酬方式ID
        private decimal t_attendance;   //出勤小时
        private decimal t_attday;       //出勤天
        private int t_mid;              //中班
        private int t_night;            //夜班
        private int t_whole;            //全夜
        private decimal t_fixedsalary;  // 固定工资
        private decimal t_leave;        //事假
        private decimal t_other;        //其他请假
        private decimal t_Rate;          //考评部分
        private int t_workyear;     //工龄

        private decimal t_maternityDays;   //产假
        private decimal t_maternityPay;   //产假工资

        private decimal t_workOrder;  //计件工费

        private int t_workShopID; //工段

        private int t_userType; //员工类型

        private decimal t_fullattendence;   //全勤
        private decimal t_lengService;   //工龄补贴

        private decimal t_accrFound; //养老保险
        private decimal t_acceFound; //失业保险
        private decimal t_accmFound; //医疗保险

        private decimal t_taxRate; //税率
        private decimal t_taxDeduct; //税速算扣除数

        private decimal t_materialDeduct; //材料扣款
        private decimal t_assessDeduct; //考核扣款
        private decimal t_vacationDeduct; //假期扣款
        private decimal t_otherDeduct; //其他扣款
        private decimal t_ship; // 出运
        private decimal t_locFin; //库存账龄

        private decimal t_hightTemp; // 高温费
        private decimal t_humanAllowance;  //招工津贴
        private decimal t_normal;    //其他津贴
        private decimal t_allother;    //独生子女
        private decimal t_newuser;    //新工人补贴
        private decimal t_student; //学生津贴
        private decimal t_bonus;   //奖励 
        private decimal t_allMobile;    //手机费
        private decimal t_allKilometer;    //公里贴
        private decimal t_allBox;    //装箱贴
        private decimal t_allBusiness;    //出差贴
        private decimal t_allOnDuty;    //值班贴
        private decimal t_allPost;    //岗位贴

        private decimal t_quanlity;
        private decimal t_break;



        //Add By Shanzm 2013.01.23
        private decimal t_restTimeMoney;   //调休金额 


        //实例化对象
        public int T_userID
        {
            get { return t_userID; }
            set { t_userID = value; }
        }

        public string T_userNo
        {
            get { return t_userNo; }
            set { t_userNo = value; }
        }

        public string T_userName
        {
            get { return t_userName; }
            set { t_userName = value; }
        }

        public string T_salaryDate
        {
            get { return t_salaryDate; }
            set { t_salaryDate = value; }
        }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal T_basic
        {
            get { return t_basic; }
            set { t_basic = value; }
        }
        /// <summary>
        ///  效益工资
        /// </summary>
        public decimal T_benefit
        {
            get { return t_benefit; }
            set { t_benefit = value; }
        }
        /// <summary>
        /// 中夜津贴
        /// </summary>
        public decimal T_nightWork
        {
            get { return t_nightWork; }
            set { t_nightWork = value; }
        }
        /// <summary>
        /// 各种津贴之和
        /// </summary>
        public decimal  T_allowance
        {
            get { return t_allowance; }
            set { t_allowance = value; }
        }
        /// <summary>
        /// 应补发的金额。当应发工资扣除社保等之后小于最低基本工资时，需要补齐金额。且，对特定人群有效（未离职、无请假、非新员工等）。
        /// </summary>
        public decimal T_subsidies 
        {
            get { return t_subsidies; }
            set { t_subsidies = value; }
        }
        /// <summary>
        /// 考核奖（加班费）
        /// </summary>
        public decimal T_assess
        {
            get { return t_assess; }
            set { t_assess = value; }
        }
        /// <summary>
        /// 国假工资
        /// </summary>
        public decimal T_holiday
        {
            get { return t_holiday; }
            set { t_holiday = value; }
        }
        /// <summary>
        /// 应发工资
        /// </summary>
        public decimal T_duereward
        {
            get { return t_duereward; }
            set { t_duereward = value; }
        }

        public decimal T_hfound
        {
            get { return t_hfound; }
            set { t_hfound = value; }
        }

        public decimal T_rfound
        {
            get { return t_rfound; }
            set { t_rfound = value; }
        }

        public decimal T_member
        {
            get { return t_member; }
            set { t_member = value; }
        }

        public decimal T_deduct
        {
            get { return t_deduct; }
            set { t_deduct = value; }
        }

        public decimal T_tax
        {
            get { return t_tax; }
            set { t_tax = value; }
        }
        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal T_workpay
        {
            get { return t_workpay; }
            set { t_workpay = value; }
        }
        //End 工资条

        public bool T_fire
        {
            get { return t_fire; }
            set { t_fire = value; }
        }

        public decimal T_currdeduct
        {
            get { return t_currdeduct; }
            set { t_currdeduct = value; }
        }

        public decimal T_lastdeduct
        {
            get { return t_lastdeduct; }
            set { t_lastdeduct = value; }
        }
        /// <summary>
        /// 年休天数
        /// </summary>
        public decimal T_annual
        {
            get { return t_annual; }
            set { t_annual = value; }
        }

        public decimal T_hday
        {
            get { return t_hday; }
            set { t_hday = value; }
        }
        /// <summary>
        /// 病假天数
        /// </summary>
        public decimal T_sleave
        {
            get { return t_sleave; }
            set { t_sleave = value; }
        }
        /// <summary>
        /// 病假工资 = 病假天数 * （基本工资  / 病假周期，即病假工资单价） * 病假工资率（诸如病假时发放基本工资的80%等）
        /// </summary>
        public decimal T_sleavepay
        {
            get { return t_sleavepay; }
            set { t_sleavepay = value; }
        }

        public int T_department
        {
            get { return t_department; }
            set { t_department = value; }
        }

        public int T_employTypeID
        {
            get { return t_employTypeID; }
            set { t_employTypeID = value; }
        }

        public int T_insuranceTypeID
        {
            get { return t_insuranceTypeID; }
            set { t_insuranceTypeID = value; }
        }

        public int T_worktype
        {
            get { return t_worktype; }
            set { t_worktype = value; }
        }
        /// <summary>
        /// 出勤小时 = 考勤时间 + 调休时间 - 国假
        /// </summary>
        public decimal T_attendance
        {
            get { return t_attendance; }
            set { t_attendance = value; }
        }
        /// <summary>
        /// 出勤天 = 考勤天 + 调休天
        /// </summary>
        public decimal T_attday
        {
            get { return t_attday; }
            set { t_attday = value; }
        }

        public int T_mid
        {
            get { return t_mid; }
            set { t_mid = value; }
        }

        public int T_night
        {
            get { return t_night; }
            set { t_night = value; }
        }

        public int T_whole
        {
            get { return t_whole; }
            set { t_whole = value; }
        }
        /// <summary>
        /// 固定工资
        /// </summary>
        public decimal T_fixedsalary
        {
            get { return t_fixedsalary; }
            set { t_fixedsalary = value; }
        }
        /// <summary>
        /// 事假
        /// </summary>
        public decimal T_leave
        {
            get { return t_leave; }
            set { t_leave = value; }
        }
        /// <summary>
        /// 其他请假 = （婚+丧+工伤） / 病假周期  * 年平均出勤率
        /// </summary>
        public decimal T_other
        {
            get { return t_other; }
            set { t_other = value; }
        }

        public decimal T_Rate
        {
            get { return t_Rate; }
            set { t_Rate = value; }
        }

        public int T_workyear
        {
            get { return t_workyear; }
            set { t_workyear = value; }
        }
        /// <summary>
        /// 产假天数
        /// </summary>
        public decimal T_maternityDays
        {
            get { return t_maternityDays; }
            set { t_maternityDays = value; }
        }
        /// <summary>
        /// 产假工资 = 产假天数 * （基本工资 / 病假周期）
        /// </summary>
        public decimal T_maternityPay
        {
            get { return t_maternityPay; }
            set { t_maternityPay = value; }
        }

        public decimal T_workOrder
        {
            get { return t_workOrder; }
            set { t_workOrder = value; }
        }

        public int T_workShopID
        {
            get { return t_workShopID; }
            set { t_workShopID = value; }
        }

        public int T_userType
        {
            get { return t_userType; }
            set { t_userType = value; }
        }
        /// <summary>
        /// 全勤奖
        /// </summary>
        public decimal T_fullattendence
        {
            get { return t_fullattendence; }
            set { t_fullattendence = value; }
        }
        /// <summary>
        /// 工龄奖
        /// </summary>
        public decimal T_lengService
        {
            get { return t_lengService; }
            set { t_lengService = value; }
        }

        public decimal T_accrFound
        {
            get { return t_accrFound; }
            set { t_accrFound = value; }
        }

        public decimal T_acceFound
        {
            get { return t_acceFound; }
            set { t_acceFound = value; }
        }

        public decimal T_accmFound
        {
            get { return t_accmFound; }
            set { t_accmFound = value; }
        }

        public decimal T_taxRate
        {
            get { return t_taxRate; }
            set { t_taxRate = value; }
        }

        public decimal T_taxDeduct
        {
            get { return t_taxDeduct; }
            set { t_taxDeduct = value; }
        }

        public decimal T_materialDeduct
        {
            get { return t_materialDeduct; }
            set { t_materialDeduct = value; }
        }


        public decimal T_assessDeduct
        {
            get { return t_assessDeduct; }
            set { t_assessDeduct = value; }
        }

        public decimal T_vacationDeduct
        {
            get { return t_vacationDeduct; }
            set { t_vacationDeduct = value; }
        }

        public decimal T_otherDeduct
        {
            get { return t_otherDeduct; }
            set { t_otherDeduct = value; }
        }

        /// <summary>
        /// 高温费
        /// </summary>
        public decimal t_HightTemp
        {
            get { return t_hightTemp; }
            set { t_hightTemp = value; }
        }
        /// <summary>
        /// 招工津贴
        /// </summary>
        public decimal t_HumanAllowance
        {
            get { return t_humanAllowance; }
            set { t_humanAllowance = value; }
        }
        /// <summary>
        /// 其他津贴
        /// </summary>
        public decimal t_Normal
        {
            get { return t_normal; }
            set { t_normal = value; }
        }
        /// <summary>
        /// 独生子女
        /// </summary>
        public decimal t_Allother
        {
            get { return t_allother; }
            set { t_allother = value; }
        }

        public decimal t_Ship
        {
            get { return t_ship; }
            set { t_ship = value; }
        }

        public decimal t_LocFin
        {
            get { return t_locFin; }
            set { t_locFin = value; }
        }

        public decimal t_Quanlity
        {
            get { return t_quanlity; }
            set { t_quanlity = value; }
        }

        public decimal t_Break
        {
            get { return t_break; }
            set { t_break = value; }
        }
        /// <summary>
        /// 新员工补贴
        /// </summary>
        public decimal T_newuser
        {
            get { return t_newuser; }
            set { t_newuser = value; }
        }
        /// <summary>
        /// 学生津贴
        /// </summary>
        public decimal T_student
        {
            get { return t_student; }
            set { t_student = value; }
        }
        /// <summary>
        /// 奖金
        /// </summary>
        public decimal T_bonus
        {
            get { return t_bonus; }
            set { t_bonus = value; }
        }

        /// <summary>
        /// 调休金额：取自tcpcx.dbo.RestTime.Money，直接加至加班费中
        /// </summary>
        public decimal T_restTimeMoney
        {
            get { return t_restTimeMoney; }
            set { t_restTimeMoney = value; }
        }

        private decimal _MutualFunds;
        /// <summary>
        /// 互助基金
        /// </summary>
        public decimal T_MutualFunds
        {
            get { return _MutualFunds; }
            set { _MutualFunds = value; }
        }

        private decimal _f_Electricity;
        /// <summary>
        /// 电费
        /// </summary>
        public decimal F_Electricity
        {
            get { return _f_Electricity; }
            set { _f_Electricity = value; }
        }

        private decimal _f_Water;
        /// <summary>
        /// 水费
        /// </summary>
        public decimal F_Water
        {
            get { return _f_Water; }
            set { _f_Water = value; }
        }

        private decimal _f_Dormitory;
        /// <summary>
        /// 住宿费
        /// </summary>
        public decimal F_Dormitory
        {
            get { return _f_Dormitory; }
            set { _f_Dormitory = value; }
        }

        /// <summary>
        /// 手机费
        /// </summary>
        public decimal t_AllMobile
        {
            get { return t_allMobile; }
            set { t_allMobile = value; }
        }
        /// <summary>
        /// 公里贴
        /// </summary>
        public decimal t_AllKilometer
        {
            get { return t_allKilometer; }
            set { t_allKilometer = value; }
        }
        /// <summary>
        /// 装箱贴
        /// </summary>
        public decimal t_AllBox
        {
            get { return t_allBox; }
            set { t_allBox = value; }
        }
        /// <summary>
        /// 出差贴
        /// </summary>
        public decimal t_AllBusiness
        {
            get { return t_allBusiness; }
            set { t_allBusiness = value; }
        }
        /// <summary>
        /// 值班贴
        /// </summary>
        public decimal t_AllOnDuty
        {
            get { return t_allOnDuty; }
            set { t_allOnDuty = value; }
        }
        /// <summary>
        /// 岗位贴
        /// </summary>
        public decimal t_AllPost
        {
            get { return t_allPost; }
            set { t_allPost = value; }
        }


    }
}
