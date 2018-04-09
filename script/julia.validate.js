
    //短日期：2013-04-01
    function isDate(str) 
    { 
        var r = str.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
        
        if(r == null) return false;

        var d= new Date(r[1], r[3]-1, r[4]); 
        return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4]); 
    }

    //长日期
    function isDateTime(str) 
    {
        var reg1 = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/; //有时分秒
        var reg2 = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2})$/;//只有时分，无秒
        var r = str.match(reg1);

        if (r == null) {
            r = str.match(reg2);
            if (r == null) {
                return false;
            }
            else {
                var d = new Date(r[1], r[3] - 1, r[4], r[5], r[6]);
                return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4] && d.getHours() == r[5] && d.getMinutes() == r[6]);
            }
        }
        else {
            var d = new Date(r[1], r[3] - 1, r[4], r[5], r[6], r[7]);
            return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4] && d.getHours() == r[5] && d.getMinutes() == r[6] && d.getSeconds() == r[7]);
        }
    } 

    //整数
    function isInteger(str)
    {
        if (!(/(^-?\\d+$)/.test(str)))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    //数字
    function isNumeric(str)
    {
        if(isNaN(str))
        {
            return false;
        }
        else
        {
            return true;
        }
    }


