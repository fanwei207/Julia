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
    /// 工资基础信息 DB:hr_bi_mstr
    /// </summary>
    public class HR_BaseInfo
    {
        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public HR_BaseInfo()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //每月出勤天数
        private decimal _WorkDays;
        /// <summary>
        /// 每月出勤天数
        /// </summary>
        public decimal WorkDays
        {
            get { return _WorkDays; }
            set { _WorkDays = value; }
        }

        //每月出勤天数下限（计算全勤奖时使用）
        private decimal _MinWorkDays;
        /// <summary>
        /// 每月出勤天数下限
        /// </summary>
        public decimal MinWorkDays
        {
            get { return _MinWorkDays; }
            set { _MinWorkDays = value; }
        }

        //计时固定出勤天
        private decimal _FixedDays;
        /// <summary>
        /// 计时固定出勤天
        /// </summary>
        public decimal FixedDays
        {
            get { return _FixedDays; }
            set { _FixedDays = value; }
        }

        //加班工资率
        private decimal _OverTimeRate;
        /// <summary>
        /// 加班工资率
        /// </summary>
        public decimal OverTimeRate
        {
            get { return _OverTimeRate; }
            set { _OverTimeRate = value; }
        }

        //节假日工资率
        private decimal _HolidayRate;
        /// <summary>
        /// 节假日工资率
        /// </summary>
        public decimal HolidayRate
        {
            get { return _HolidayRate; }
            set { _HolidayRate = value; }
        }

        //星期六工资率
        private decimal _SaturdayRate;
        public decimal SaturdayRate
        {
            get { return _SaturdayRate; }
            set { _SaturdayRate = value; }
        }

        //中夜班开始时间
        private string _MidNightStartTime;
        /// <summary>
        /// 中夜班开始时间
        /// </summary>
        public string MidNightStartTime
        {
            get { return _MidNightStartTime; }
            set { _MidNightStartTime = value; }
        }

        //夜班开始时间
        private string _NightStartTime;
        /// <summary>
        /// 夜班开始时间
        /// </summary>
        public string NightStartTime
        {
            get { return _NightStartTime; }
            set { _NightStartTime = value; }
        }

        //全夜开始时间
        private string _WholeNightStartTime;
        /// <summary>
        /// 全夜开始时间
        /// </summary>
        public string WholeNightStartTime
        {
            get { return _WholeNightStartTime; }
            set { _WholeNightStartTime = value; }
        }

        //中夜班津贴
        private decimal _MidNightSubsidy;
        /// <summary>
        /// 中夜班津贴
        /// </summary>
        public decimal MidNightSubsidy
        {
            get { return _MidNightSubsidy; }
            set { _MidNightSubsidy = value; }
        }

        //夜班津贴
        private decimal _NightSubsidy;
        /// <summary>
        /// 夜班津贴
        /// </summary>
        public decimal NightSubsidy
        {
            get { return _NightSubsidy; }
            set { _NightSubsidy = value; }
        }

        //全夜津贴
        private decimal _WholeNightSubsidy;
        /// <summary>
        /// 全夜津贴
        /// </summary>
        public decimal WholeNightSubsidy
        {
            get { return _WholeNightSubsidy; }
            set { _WholeNightSubsidy = value; }
        }

        //上午上班时间
        private string _AMStartTime;
        /// <summary>
        /// 上午上班时间
        /// </summary>
        public string AMStartTime
        {
            get { return _AMStartTime; }
            set { _AMStartTime = value; }
        }

        //下午下班时间
        private string _PMEndTime;
        /// <summary>
        /// 下午下班时间
        /// </summary>
        public string PMEndTime
        {
            get { return _PMEndTime; }
            set { _PMEndTime = value; }
        }

        //午休时间
        private decimal  _LunchTime;
        /// <summary>
        /// 午休时间
        /// </summary>
        public decimal LunchTime
        {
            get { return _LunchTime; }
            set { _LunchTime = value; }
        }

        //晚餐时间
        private decimal _DinnerTime;
        /// <summary>
        /// 晚餐时间
        /// </summary>
        public decimal DinnerTime
        {
            get { return _DinnerTime; }
            set { _DinnerTime = value; }
        }

        //基本单价
        private decimal _BasicPrice;
        /// <summary>
        /// 基本单价
        /// </summary>
        public decimal BasicPrice
        {
            get { return _BasicPrice; }
            set { _BasicPrice = value; }
        }

        //基本工资
        private decimal _BasicWage;
        /// <summary>
        /// 基本工资。上海1450/月
        /// </summary>
        public decimal BasicWage
        {
            get { return _BasicWage; }
            set { _BasicWage = value; }
        }

        //日正常出勤率
        private int _WorkHours;
        /// <summary>
        /// 日正常出勤率
        /// </summary>
        public int WorkHours
        {
            get { return _WorkHours; }
            set { _WorkHours = value; }
        }

        //年平均出勤率
        private decimal _AvgDays;
        /// <summary>
        /// 年平均出勤率
        /// </summary>
        public decimal AvgDays
        {
            get { return _AvgDays; }
            set { _AvgDays = value; }
        }

        //工会费率
        private decimal _LaborRate;
        /// <summary>
        /// 工会费率
        /// </summary>
        public decimal LaborRate
        {
            get { return _LaborRate; }
            set { _LaborRate = value; }
        }

        //扣款比率
        private decimal _DeductRate;
        /// <summary>
        /// 扣款比率
        /// </summary>
        public decimal DeductRate
        {
            get { return _DeductRate; }
            set { _DeductRate = value; }
        }

        //个税起征点
        private int _Tax;
        /// <summary>
        /// 个税起征点
        /// </summary>
        public int Tax
        {
            get { return _Tax; }
            set { _Tax = value; }
        }


        //计时上班时间
        private string _StartTime;
        /// <summary>
        /// 计时上班时间
        /// </summary>
        public string StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        //计时下班时间
        private string _EndTime;
        /// <summary>
        /// 计时下班时间
        /// </summary>
        public string EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        //计时午休时间
        private decimal  _RestTime;
        /// <summary>
        /// 计时午休时间
        /// </summary>
        public decimal RestTime
        {
            get { return _RestTime; }
            set { _RestTime = value; }
        }

        //计时晚餐时间
        private decimal _OtherTime;
        /// <summary>
        /// 计时晚餐时间
        /// </summary>
        public decimal OtherTime
        {
            get { return _OtherTime; }
            set { _OtherTime = value; }
        }

        //计件工作小时
        private decimal _bi_Work1;
        /// <summary>
        /// 计件工作小时
        /// </summary>
        public decimal bi_Work1
        {
            get { return _bi_Work1; }
            set { _bi_Work1 = value; }
        }

        //计时工作小时
        private decimal _bi_Work2;
        /// <summary>
        /// 计时工作小时
        /// </summary>
        public decimal bi_Work2
        {
            get { return _bi_Work2; }
            set { _bi_Work2 = value; }
        }

        //中夜班计件
        private int _bi_Night1;
        /// <summary>
        /// 中夜班计件
        /// </summary>
        public int bi_Night1
        {
            get { return _bi_Night1; }
            set { _bi_Night1 = value; }
        }

        //中夜班计时
        private int _bi_Night2;
        /// <summary>
        /// 中夜班计时
        /// </summary>
        public int bi_Night2
        {
            get { return _bi_Night2; }
            set { _bi_Night2 = value; }
        }


        //病假工资率
        private decimal _SickleaveRate;
        /// <summary>
        /// 病假工资率
        /// </summary>
        public decimal SickleaveRate
        {
            get { return _SickleaveRate; }
            set { _SickleaveRate = value; }
        }

        private decimal _SickleaveDay;

        /// <summary>
        /// 病假周期
        /// </summary>
        public decimal SickleaveDay
        {
            get { return _SickleaveDay; }
            set { _SickleaveDay = value; }
        }

        private decimal _OverPrice;
        /// <summary>
        /// 加班费率（基本工资按基本单价计算，加班费按照加班费率计算）
        /// </summary>
        public decimal OverPrice
        {
            get { return _OverPrice; }
            set { _OverPrice = value; }
        }

        //社会保险基数
        private decimal _Social;
        /// <summary>
        /// 社会保险基数
        /// </summary>
        public decimal Social
        {
            get { return _Social; }
            set { _Social = value; }
        }

        //养老保险
        private decimal _Oldage;
        /// <summary>
        /// 养老保险
        /// </summary>
        public decimal Oldage
        {
            get { return _Oldage; }
            set { _Oldage = value; }
        }

        //失业保险
        private decimal _Unemploy;
        /// <summary>
        /// 失业保险
        /// </summary>
        public decimal Unemploy
        {
            get { return _Unemploy; }
            set { _Unemploy = value; }
        }

        //工伤保险
        private decimal _Injury;
        /// <summary>
        /// 工伤保险
        /// </summary>
        public decimal Injury
        {
            get { return _Injury; }
            set { _Injury = value; }
        }

        //生育保险
        private decimal _Maternity;
        /// <summary>
        /// 生育保险
        /// </summary>
        public decimal Maternity
        {
            get { return _Maternity; }
            set { _Maternity = value; }
        }

        //医疗保险
        private decimal _Health;
        /// <summary>
        /// 医疗保险
        /// </summary>
        public decimal Health
        {
            get { return _Health; }
            set { _Health = value; }
        }

        //住房公积金
        private decimal _HousingFund;
        /// <summary>
        /// 住房公积金
        /// </summary>
        public decimal HousingFund
        {
            get { return _HousingFund; }
            set { _HousingFund = value; }
        }

        //工会费
        private decimal _UnionFee;
        /// <summary>
        /// 工会费
        /// </summary>
        public decimal UnionFee
        {
            get { return _UnionFee; }
            set { _UnionFee = value; }
        }

        //农保养老保险
        private decimal _A_Oldage;
        /// <summary>
        /// 农保养老保险
        /// </summary>
        public decimal A_Oldage
        {
            get { return _A_Oldage; }
            set { _A_Oldage = value; }
        }

        //农保医疗保险
        private decimal _A_Health;
        /// <summary>
        /// 农保医疗保险
        /// </summary>
        public decimal A_Health
        {
            get { return _A_Health; }
            set { _A_Health = value; }
        }

        //农保工伤保险
        private decimal _A_Injury;
        /// <summary>
        /// 农保工伤保险
        /// </summary>
        public decimal A_Injury
        {
            get { return _A_Injury; }
            set { _A_Injury = value; }
        }

        //全勤奖上限
        private decimal _MaxAttbonus;
        /// <summary>
        /// 全勤奖上限
        /// </summary>
        public decimal MaxAttbonus
        {
            get { return _MaxAttbonus; }
            set { _MaxAttbonus = value; }
        }

        //全勤奖下限
        private decimal _MinAttbonus;
        /// <summary>
        /// 全勤奖下限
        /// </summary>
        public decimal MinAttbonus
        {
            get { return _MinAttbonus; }
            set { _MinAttbonus = value; }
        }

        //全勤奖%
        private decimal _PercentAttbonus;
        /// <summary>
        /// 全勤奖%
        /// </summary>
        public decimal PercentAttbonus
        {
            get { return _PercentAttbonus; }
            set { _PercentAttbonus = value; }
        }

        //工龄贴单价上限
        private decimal _MaxWYbonus;
        /// <summary>
        /// 工龄贴单价上限
        /// </summary>
        public decimal MaxWYbonus
        {
            get { return _MaxWYbonus; }
            set { _MaxWYbonus = value; }
        }

        //工龄贴单价下限
        private decimal _MinWYbonus;
        /// <summary>
        /// 工龄贴单价下限
        /// </summary>
        public decimal MinWYbonus
        {
            get { return _MinWYbonus; }
            set { _MinWYbonus = value; }
        }

        //工龄贴单价
        private decimal _WorkYearbonus;
        /// <summary>
        /// 工龄贴单价
        /// </summary>
        public decimal WorkYearbonus
        {
            get { return _WorkYearbonus; }
            set { _WorkYearbonus = value; }
        }

        //工龄贴%
        private decimal _PercentWYbonus;
        /// <summary>
        /// 工龄贴%
        /// </summary>
        public decimal PercentWYbonus
        {
            get { return _PercentWYbonus; }
            set { _PercentWYbonus = value; }
        }

        //只能负调整工资
        private bool _isMinus;
        /// <summary>
        /// 只能负调整工资
        /// </summary>
        public bool isMinus
        {
            get { return _isMinus; }
            set { _isMinus = value; }
        }
        //其他基本工资
        private decimal _basicWageNew;
        /// <summary>
        /// 其他基本工资：淮安结算时的基本工资和纸面基本工资不一致 
        /// </summary>
        public decimal BasicWageNew
        {
            get { return _basicWageNew; }
            set { _basicWageNew = value; }
        }
    }
}