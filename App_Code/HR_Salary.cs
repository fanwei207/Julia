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
    /// Summary description for HR_Salary
    /// </summary>
    public class HR_Salary
    {
        public HR_Salary()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 工资条字段
        /// </summary>
        private int s_userID;           //员工ID
        private string s_userNo;        //员工工号
        private string s_userName;      //员工姓名
        private string s_salaryDate;    //工资日期
        private decimal s_basic;        //基本工资
        private decimal s_over;         //加班工资
        private decimal s_nightMoney;   //中夜津贴
        private decimal s_holiday;      //国假工资
        private decimal s_oallowance;   //其他津贴
        private decimal s_subsidies;    //补贴
        private decimal s_duereward;    //应发金额
        private decimal s_hfound;       //公积
        private decimal s_rfound;       //养老
        private decimal s_memship;      //工会
        private decimal s_duct;         //扣款
        private decimal s_tax;          //所得税
        private decimal s_workpay;      //实发金额


        /// <summary>
        /// 其他工资相关信息
        /// </summary>
        private bool s_leave;           //是否全勤 （yes --  全勤  /  No -- 有请假）
        private decimal s_temday;       //试用期员工当月应出勤
        private bool s_fire;            //是否离职 （yes --  离职  /  No -- 在职）
        private decimal s_lastduct;     //上月余扣款
        private decimal s_currduct;     //当月余扣款     当月实扣 s_currduct + s_duct - s_lastduct
        private decimal s_restleave;    //当月年休天数  
        private decimal s_shday;        //当月国假上班天数
        private decimal s_shsalary;     //当月国假工资
        private decimal s_childwance;    //独生子女津贴
        private int s_department;        //部门 ＩＤ
        private int s_employTypeID;      //用工性质ＩＤ
        private int s_insuranceTypeID;　　//社保类型ＩＤ
        private bool s_temp;             //是否是试用期员工（yes --是  / No　--不是 ）
        private int s_workyear;           //工龄


        private int s_workshopID;       //工段
        private int s_workgroupID;      //班组
        private int s_workkindID;       //工种

        private decimal  s_sickLeave;   //病假天
        private decimal s_sickLeavePay;  //病假工资

        private decimal s_decRateDeduct;  //考评

        private decimal s_workMoney;   //本月工费
        private decimal s_maternityDays; //产假
        private decimal s_maternityPay; //产假工资

        private bool s_time; // 计件转计时

        private decimal s_realMoney; //实际工费（已结算加工单）

        private int s_userType; //员工类型

        private decimal s_leaveday;  //事假
        private decimal s_minerDays; //旷工
        private decimal s_annualLeaveDays; //年休假
        private decimal s_otherday; //其他假期


        private decimal s_allattendence;  //全勤奖
        private decimal s_wYbonus; //工龄补贴


        private decimal s_attdays;  //出勤天
        private decimal s_attHours; //出勤小时天

        private decimal s_accrFound; //养老保险
        private decimal s_acceFound; //失业保险
        private decimal s_accmFound; //医疗保险

        private decimal s_materialDeduct; //材料扣款
        private decimal s_assessDeduct; //零消耗扣款
        private decimal s_vacationDeduct; //假期扣款
        private decimal s_otherDeduct; //其他扣款
        private decimal s_ship; // 出运
        private decimal s_locFin; //库存账龄
        private decimal s_quanlity;
        private decimal s_break;

        private int s_mid;  //中班
        private int s_night; //夜班
        private int s_whole; //全夜


        private decimal s_taxRate; //税率
        private decimal s_taxDeduct; //税速算扣除数

        private bool s_exChange; // 计件计时转换   0- No  1 - Yes

        private decimal s_hightTemp; // 高温费
        private decimal s_humanAllowance;  //招工津贴
        private decimal s_normal;    //津贴
        private decimal s_other;    //其他津贴
        private decimal s_newuser;  //新工人补贴
        private decimal s_student;  //学生津贴
        private decimal s_bonus;    //奖励
        private decimal s_allMobile;    //手机费
        private decimal s_allKilometer;    //公里贴
        private decimal s_allBox;    //装箱贴
        private decimal s_allBusiness;    //出差贴
        private decimal s_allOnDuty;    //值班贴
        private decimal s_allPost;    //岗位贴


        private decimal s_coef;      //系数
        private decimal s_rate;      //平均工资率
        private decimal s_benefit;    //绩效工资

        private decimal s_original;  //  原始工时


        //实例化对象
        public int s_UserID
        {
            get { return s_userID; }
            set { s_userID = value; }
        }

        public string s_UserNo
        {
            get { return s_userNo; }
            set { s_userNo = value; }
        }

        public string s_UserName
        {
            get { return s_userName; }
            set { s_userName = value; }
        }

        public string s_SalaryDate
        {
            get { return s_salaryDate; }
            set { s_salaryDate = value; }
        }
        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal  s_Basic
        {
            get { return s_basic; }
            set { s_basic = value; }
        }
        /// <summary>
        /// 加班工资
        /// </summary>
        public decimal  s_Over
        {
            get { return s_over; }
            set { s_over = value; }
        }
        /// <summary>
        /// 夜班补贴。中夜班、夜班、全夜班天数 * 各自补贴比例
        /// </summary>
        public decimal s_NightMoney
        {
            get { return s_nightMoney; }
            set { s_nightMoney = value; }
        }
        /// <summary>
        /// s_Holiday = s_Shsalary，而s_Shsalary是国假工资。国假有汇报的时候才计算工资
        /// </summary>
        public decimal s_Holiday
        {
            get { return s_holiday; }
            set { s_holiday = value; }
        }
        /// <summary>
        /// 各种津贴之和
        /// </summary>
        public decimal s_Oallowance
        {
            get { return s_oallowance; }
            set { s_oallowance = value; }
        }
        /// <summary>
        /// 应补发的金额。当应发工资扣除社保等之后小于最低基本工资时，需要补齐金额。且，对特定人群有效（未离职、无请假、非新员工等）。
        /// </summary>
        public decimal s_Subsidies
        {
            get { return s_subsidies; }
            set { s_subsidies = value; }
        }
        /// <summary>
        /// 应发工资
        /// </summary>
        public decimal s_Duereward
        {
            get { return s_duereward; }
            set { s_duereward = value; }
        }
        /// <summary>
        /// 住房公积金
        /// </summary>
        public decimal s_Hfound
        {
            get { return s_hfound; }
            set { s_hfound = value; }
        }
        /// <summary>
        /// 养老保险 + 失业保险 + 医疗保险之和
        /// </summary>
        public decimal s_Rfound
        {
            get { return s_rfound; }
            set { s_rfound = value; }
        }

        /// <summary>
        /// 应缴公会费用。其数值 = 基本工资 * 公会费率
        /// </summary>
        public decimal s_Memship
        {
            get { return s_memship; }
            set { s_memship = value; }
        }
        /// <summary>
        /// 扣款。如果扣款比率大于工资的20%，则按工资的20%扣款，否则按实际扣款额扣发。如果是补齐的情况，则扣款累计到下个月执行
        /// </summary>
        public decimal s_Duct
        {
            get { return s_duct; }
            set { s_duct = value; }
        }
        /// <summary>
        /// 个人所得税。应发工资扣除扣款、社保等，另外，独身子女津贴是要交税的
        /// </summary>
        public decimal s_Tax
        {
            get { return s_tax; }
            set { s_tax = value; }
        }
        /// <summary>
        /// 实发工资
        /// </summary>
        public decimal s_Workpay
        {
            get { return s_workpay; }
            set { s_workpay = value; }
        }

        /// <summary>
        /// 婚假、丧假、工伤全部为0时为真
        /// </summary>
        public bool s_Leave
        {
            get { return s_leave; }
            set { s_leave = value; }
        }

        public decimal s_Temday
        {
            get { return s_temday; }
            set { s_temday = value; }
        }

        /// <summary>
        /// 是否离职。如果是在结算月离职的，则本次工资结算任按照非离职计算
        /// </summary>
        public bool s_Fire
        {
            get { return s_fire; }
            set { s_fire = value; }
        }
        /// <summary>
        /// 上月剩余待扣扣款额。本月扣款 = 上月扣款余额 + 本月发生额。但，应该减去考核奖励
        /// </summary>
        public decimal s_Lastduct
        {
            get { return s_lastduct; }
            set { s_lastduct = value; }
        }
        /// <summary>
        /// 本次结算时，应扣款金额。数值上=上月未扣金额 + 本月发生金额
        /// </summary>
        public decimal s_Currduct
        {
            get { return s_currduct; }
            set { s_currduct = value; }
        }

        /// <summary>
        /// 当月年休天数 = Convert.ToDecimal(sdtr["number"])
        /// </summary>
        public decimal s_Restleave
        {
            get { return s_restleave; }
            set { s_restleave = value; }
        }

        /// <summary>
        /// 国假工天。国假期间的工天要另外计算
        /// Math.Round(Convert.ToDecimal(sdtr["holidaycost"]) / 8, 2)
        /// </summary>
        public decimal s_Shday
        {
            get { return s_shday; }
            set { s_shday = value; }
        }
        /// <summary>
        /// 国假日工资 = 国假工天 * 基本单价 * 节假日工资率
        /// </summary>
        public decimal s_Shsalary
        {
            get { return s_shsalary; }
            set { s_shsalary = value; }
        }
        /// <summary>
        /// 独身子女津贴
        /// </summary>
        public decimal s_Childwance
        {
            get { return s_childwance; }
            set { s_childwance = value; }
        }

        public int s_Department
        {
            get { return s_department; }
            set { s_department = value; }
        }

        public int s_EmployTypeID
        {
            get { return s_employTypeID; }
            set { s_employTypeID = value; }
        }
        public int s_InsuranceTypeID
        {
            get { return s_insuranceTypeID; }
            set { s_insuranceTypeID = value; }
        }

        public bool s_Temp
        {
            get { return s_temp; }
            set { s_temp = value; }
        }
        /// <summary>
        /// 工龄。如果是在结算月或之前入职的，算满一年
        /// </summary>
        public int s_Workyear
        {
            get { return s_workyear; }
            set { s_workyear = value; }
        }

        public int s_WorkshopID
        {
            get { return s_workshopID; }
            set { s_workshopID = value; }
        }

        public int s_WorkgroupID
        {
            get { return s_workgroupID; }
            set { s_workgroupID = value; }
        }

        public int s_WorkkindID
        {
            get { return s_workkindID; }
            set { s_workkindID = value; }
        }
        /// <summary>
        /// 病假天数
        /// </summary>
        public decimal  s_SickLeave
        {
            get { return s_sickLeave; }
            set { s_sickLeave = value; }
        }
        /// <summary>
        /// 病假期间应付工资
        /// </summary>
        public decimal  s_SickLeavePay
        {
            get { return s_sickLeavePay; }
            set { s_sickLeavePay = value; }
        }

        public decimal s_DecRateDeduct
        {
            get { return s_decRateDeduct; }
            set { s_decRateDeduct = value; }
        }

        /// <summary>
        /// 本月净工天。cost里面包含国假期间的工天，所以得先减掉：Math.Round(Convert.ToDecimal(sdtr["cost"]) / 8, 2) - hrs.s_Shday（国假工时）。
        /// 如果工单工天小于国假工天，则为0
        /// </summary>
        public decimal s_WorkMoney
        {
            get { return s_workMoney; }
            set { s_workMoney = value; }
        }
        /// <summary>
        /// 产假
        /// </summary>
        public decimal s_MaternityDays
        {
            get { return s_maternityDays; }
            set { s_maternityDays = value; }
        }
        /// <summary>
        /// 产假工资 = 产假天数 * 基本工资 / 病假周期
        /// </summary>
        public decimal s_MaternityPay
        {
            get { return s_maternityPay; }
            set { s_maternityPay = value; }
        }

        public bool s_Time
        {
            get { return s_time; }
            set { s_time = value; }
        }

        public decimal s_RealMoney
        {
            get { return s_realMoney; }
            set { s_realMoney = value; }
        }

        public int s_UserType
        {
            get { return s_userType; }
            set { s_userType = value; }
        }
        /// <summary>
        /// 事假
        /// </summary>
        public decimal s_Leaveday
        {
            get { return s_leaveday; }
            set { s_leaveday = value; }
        }

        public decimal s_MinerDays
        {
            get { return s_minerDays; }
            set { s_minerDays = value; }
        }
        /// <summary>
        /// 全勤补贴，存放在tcpc0.dbo.a_Allattendence中，直接维护的就是金额
        /// </summary>
        public decimal s_Allattendence
        {
            get { return s_allattendence; }
            set { s_allattendence = value; }
        }
        /// <summary>
        /// 工龄补贴。存放在tcpc0.dbo.a_WorkYear中，直接维护的就是金额
        /// </summary>
        public decimal s_WYbonus
        {
            get { return s_wYbonus; }
            set { s_wYbonus = value; }
        }

        public decimal s_AnnualLeaveDays
        {
            get { return s_annualLeaveDays; }
            set { s_annualLeaveDays = value; }
        }

        public decimal s_Attdays
        {
            get { return s_attdays; }
            set { s_attdays = value; }
        }
        /// <summary>
        /// 考勤小时数
        /// </summary>
        public decimal s_AttHours
        {
            get { return s_attHours; }
            set { s_attHours = value; }
        }
        /// <summary>
        /// 养老保险
        /// </summary>
        public decimal s_AccrFound
        {
            get { return s_accrFound; }
            set { s_accrFound = value; }
        }

        /// <summary>
        /// 失业保险
        /// </summary>
        public decimal s_AcceFound
        {
            get { return s_acceFound; }
            set { s_acceFound = value; }
        }

        /// <summary>
        /// 医疗保险
        /// </summary>
        public decimal s_AccmFound
        {
            get { return s_accmFound; }
            set { s_accmFound = value; }
        }

        public decimal s_MaterialDeduct
        {
            get { return s_materialDeduct; }
            set { s_materialDeduct = value; }
        }

        public decimal s_AssessDeduct
        {
            get { return s_assessDeduct; }
            set { s_assessDeduct = value; }
        }

        public decimal s_VacationDeduct
        {
            get { return s_vacationDeduct; }
            set { s_vacationDeduct = value; }
        }

        public decimal s_OtherDeduct
        {
            get { return s_otherDeduct; }
            set { s_otherDeduct = value; }
        }
        /// <summary>
        /// 中夜班天数
        /// </summary>
        public int s_Mid
        {
            get { return s_mid; }
            set { s_mid = value; }
        }
        /// <summary>
        /// 夜班天数
        /// </summary>
        public int s_Night
        {
            get { return s_night; }
            set { s_night = value; }
        }
        /// <summary>
        /// 全夜班天数
        /// </summary>
        public int s_Whole
        {
            get { return s_whole; }
            set { s_whole = value; }
        }
        /// <summary>
        /// 没有折算的婚+丧+工伤假
        /// </summary>
        public decimal s_Otherday
        {
            get { return s_otherday; }
            set { s_otherday = value; }
        }

        public decimal s_TaxRate
        {
            get { return s_taxRate; }
            set { s_taxRate = value; }
        }

        public decimal s_TaxDeduct
        {
            get { return s_taxDeduct; }
            set { s_taxDeduct = value; }
        }

        public bool s_ExChange
        {
            get { return s_exChange; }
            set { s_exChange = value; }
        }


        //public decimal s_TaxDeduct
        //{
        //    get { return s_taxDeduct; }
        //    set { s_taxDeduct = value; }
        //}

        public decimal s_Ship
        {
            get { return s_ship; }
            set { s_ship = value; }
        }

        public decimal s_LocFin
        {
            get { return s_locFin; }
            set { s_locFin = value; }
        }
        /// <summary>
        /// 高温费
        /// </summary>
        public decimal s_HightTemp
        {
            get { return s_hightTemp; }
            set { s_hightTemp = value; }
        }
        /// <summary>
        /// 招工津贴
        /// </summary>
        public decimal s_HumanAllowance
        {
            get { return s_humanAllowance; }
            set { s_humanAllowance = value; }
        }
        /// <summary>
        /// 其他津贴
        /// </summary>
        public decimal s_NOrmal
        {
            get { return s_normal; }
            set { s_normal = value; }
        }
        /// <summary>
        /// 独生子女
        /// </summary>
        public decimal s_Other
        {
            get { return s_other; }
            set { s_other = value; }
        }

        public decimal s_Quanlity
        {
            get { return s_quanlity; }
            set { s_quanlity = value; }
        }

        public decimal s_Break
        {
            get { return s_break; }
            set { s_break = value; }
        }
        /// <summary>
        /// 新员工补贴
        /// </summary>
        public decimal s_Newuser
        {
            get { return s_newuser; }
            set { s_newuser = value; }
        }
        /// <summary>
        /// 学生补贴
        /// </summary>
        public decimal s_Student
        {
            get { return s_student; }
            set { s_student = value; }
        }
        /// <summary>
        /// 奖励
        /// </summary>
        public decimal s_Bonus
        {
            get { return s_bonus; }
            set { s_bonus = value; }
        }



        public decimal s_Coef
        {
            get { return s_coef; }
            set { s_coef = value; }
        }

        public decimal s_Rate
        {
            get { return s_rate; }
            set { s_rate = value; }
        }
        /// <summary>
        /// A、B类不使用这个参数
        /// </summary>
        public decimal s_Benefit
        {
            get { return s_benefit; }
            set { s_benefit = value; }
        }

        public decimal s_Original
        {
            get { return s_original; }
            set { s_original = value; }
        }

        private decimal bt_tiaoxiu;
        /// <summary>
        /// 调休补贴
        /// </summary>
        public decimal bt_TiaoXiu
        {
            get { return bt_tiaoxiu; }
            set { bt_tiaoxiu = value; }
        }

        private decimal bt_chaoe;
        /// <summary>
        /// 工单超额补贴
        /// </summary>
        public decimal bt_ChoaE
        {
            get { return bt_chaoe; }
            set { bt_chaoe = value; }
        }

        private decimal bt_newCost;
        /// <summary>
        /// 新员工工时补贴
        /// </summary>
        public decimal bt_NewCost
        {
            get { return bt_newCost; }
            set { bt_newCost = value; }
        }

        private decimal _s_MutualFunds;
        /// <summary>
        /// 互助基金（已包含在字段deduct中，这里是明细）
        /// </summary>
        public decimal s_MutualFunds
        {
            get { return _s_MutualFunds; }
            set { _s_MutualFunds = value; }
        }

        private decimal _f_Electricity;
        /// <summary>
        /// 电费（已包含在字段deduct中，这里是明细）
        /// </summary>
        public decimal f_Electricity
        {
            get { return _f_Electricity; }
            set { _f_Electricity = value; }
        }

        private decimal _f_Water;
        /// <summary>
        /// 水费（已包含在字段deduct中，这里是明细）
        /// </summary>
        public decimal f_Water
        {
            get { return _f_Water; }
            set { _f_Water = value; }
        }

        private decimal _f_Dormitory;
        /// <summary>
        /// 住宿费（已包含在字段deduct中，这里是明细）
        /// </summary>
        public decimal f_Dormitory
        {
            get { return _f_Dormitory; }
            set { _f_Dormitory = value; }
        }

        /// <summary>
        /// 手机费
        /// </summary>
        public decimal s_AllMobile
        {
            get { return s_allMobile; }
            set { s_allMobile = value; }
        }
        /// <summary>
        /// 公里贴
        /// </summary>
        public decimal s_AllKilometer
        {
            get { return s_allKilometer; }
            set { s_allKilometer = value; }
        }
        /// <summary>
        /// 装箱贴
        /// </summary>
        public decimal s_AllBox
        {
            get { return s_allBox; }
            set { s_allBox = value; }
        }
        /// <summary>
        /// 出差贴
        /// </summary>
        public decimal s_AllBusiness
        {
            get { return s_allBusiness; }
            set { s_allBusiness = value; }
        }
        /// <summary>
        /// 值班贴
        /// </summary>
        public decimal s_AllOnDuty
        {
            get { return s_allOnDuty; }
            set { s_allOnDuty = value; }
        }
        /// <summary>
        /// 岗位贴
        /// </summary>
        public decimal s_AllPost
        {
            get { return s_allPost; }
            set { s_allPost = value; }
        }

        private bool s_isProbationer;
        /// <summary>
        /// 是否是试用期员工
        /// </summary>
        public bool s_IsProbationer
        {
            get { return s_isProbationer; }
            set { s_isProbationer = value; } 
        }

        private decimal s_probWorkDays;
        /// <summary>
        /// 试用期（包括新进的）员工应出勤天数
        /// </summary>
        public decimal s_ProbWorkDays
        {
            get { return s_probWorkDays; }
            set { s_probWorkDays = value; }
        }
    }

}