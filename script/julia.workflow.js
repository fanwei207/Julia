function getSaveFormDataSqlStr() {
    var str = "Update WorkFlow.dbo.wf_FormData Set ";
    var count = 0;
    $(".FormData").each(function () {
        if (!($(this).prop("readonly") == true || $(this).prop("disabled") == true)) {
            str += $(this).attr("id");
            str += "=";
            if ($(this).attr("type") == "checkbox") {
                if ($(this).prop("checked")) {
                    str += "1";
                }
                else {
                    str += "0";
                }
            }
            else {
                if ($(this).val().trim() == "") {
                    str += "Null";
                }
                else {
                    str += "N'";
                    str += $(this).val().trim().replace("'", "''");
                    str += "'";
                }
            }
            str += ",";
            count += 1;
        }
    });
    if (count == 0) {
        str = "";
    }
    else {
        str = str.substring(0, str.length - 1);
        str += " where WFN_Nbr='"
        str += $("#txtReqNbr").val();
        str += "'";
    }
    $("#hidSql").val(str);
    return str;
}