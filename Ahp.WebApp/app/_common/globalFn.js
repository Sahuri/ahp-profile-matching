var AllowLoader = true;
var CURRENT_STEP = 1;


function isNullorEmpty(str) {
    var s = str || '';
    if (s.toString().trim() == '')
        return true;

    return false;
}

function isNullOrEmpty(str) {
    var s = str || '';
    if (s.toString().trim() == '')
        return true;

    return false;
}

function IsNullOrEmpty(str) {
    var s = str || '';
    if (s.toString().trim() == '')
        return true;

    return false;
}

function DateFormat(val) {
    if (val == "") {
        val = undefined;
    }

    if (val == "1900-01-01") {
        val = undefined;
    }

    value = (val != undefined) ? moment(val).format('DD.MM.YYYY') : "";
    return value;
};

function DateTimeFormat(val) {
    if (val == "") {
        val = undefined;
    }

    if (val == "1900-01-01") {
        val = undefined;
    }

    value = (val != undefined) ? moment(val).format('DD.MM.YYYY HH:mm:ss') : ""; //moment('1900-01-01').format('DD.MM.YYYY HH:mm');
    if (value === "invalid date");


    return value;
};

function TimeFormat(val) {
    value = (val != undefined) ? moment(val).format('HH:mm:ss') : ':';
    return value;
};

function IntegerFormat(val) {
    if (val == null || val == undefined || val == "") val = 0;

    value = NumberFormat(val, 0);
    return value;
}

function DecimalFormat(val) {
    if (val == null || val == undefined || val == "") val = 0;

    value = NumberFormat(val, 2);
    return value;
}

function IDNumberFormat(val, decimal) {
    if (val == null || val == undefined || val == "") val = 0;

    value = NumberFormat(val, decimal, ',', '.');
    return value;
}

//Reformat

function DateReformat(val) {
    v = (val != undefined) ? val.substr(6, 4) + "-" + val.substr(3, 2) + "-" + val.substr(0, 2) : "1900-01-01";
    value = moment(v).format('YYYY-MM-DD');
    return value;
};

function DateTimeReformat(val) {
    v = (v != undefined) ? val.substr(6, 4) + "-" + val.substr(3, 2) + "-" + val.substr(0, 2) + " " + val.substr(11, 5) : "1900-01-01 00:00";
    value = moment(v).format('YYYY-MM-DD HH:mm');
    return value;
};

function NumberFormat(number, decimals, dec_point, thousands_sep, rem_zero) {
    // http://kevin.vanzonneveld.net
    // +   original by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
    // +   improved by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
    // +     bugfix by: Michael White (http://getsprink.com)
    // +     bugfix by: Benjamin Lupton
    // +     bugfix by: Allan Jensen (http://www.winternet.no)
    // +    revised by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
    // +     bugfix by: Howard Yeend
    // +    revised by: Luke Smith (http://lucassmith.name)
    // +     bugfix by: Diogo Resende
    // +     bugfix by: Rival
    // +      input by: Kheang Hok Chin (http://www.distantia.ca/)
    // +   improved by: davook
    // +   improved by: Brett Zamir (http://brett-zamir.me)
    // +      input by: Jay Klehr
    // +   improved by: Brett Zamir (http://brett-zamir.me)
    // +      input by: Amir Habibi (http://www.residence-mixte.com/)
    // +     bugfix by: Brett Zamir (http://brett-zamir.me)
    // +   improved by: Theriault
    // +   improved by: Drew Noakes
    // *     example 1: number_format(1234.56);
    // *     returns 1: '1,235'
    // *     example 2: number_format(1234.56, 2, ',', ' ');
    // *     returns 2: '1 234,56'
    // *     example 3: number_format(1234.5678, 2, '.', '');
    // *     returns 3: '1234.57'
    // *     example 4: number_format(67, 2, ',', '.');
    // *     returns 4: '67,00'
    // *     example 5: number_format(1000);
    // *     returns 5: '1,000'
    // *     example 6: number_format(67.311, 2);
    // *     returns 6: '67.31'
    // *     example 7: number_format(1000.55, 1);
    // *     returns 7: '1,000.6'
    // *     example 8: number_format(67000, 5, ',', '.');
    // *     returns 8: '67.000,00000'
    // *     example 9: number_format(0.9, 0);
    // *     returns 9: '1'
    // *    example 10: number_format('1.20', 2);
    // *    returns 10: '1.20'
    // *    example 11: number_format('1.20', 4);
    // *    returns 11: '1.2000'
    // *    example 12: number_format('1.2000', 3);
    // *    returns 12: '1.200'
    var n = !isFinite(+number) ? 0 : +number,
        prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
        sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
        dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
        toFixedFix = function (n, prec) {
            // Fix for IE parseFloat(0.55).toFixed(0) = 0;
            var k = Math.pow(10, prec);
            return Math.round(n * k) / k;
        },
        s = (prec ? toFixedFix(n, prec) : Math.round(n)).toString().split('.');
    if (s[0].length > 3) {
        []
        s[0] = s != undefined ? s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep) : s;
    }
    if (rem_zero == undefined) {
        if ((s[1] || '').length < prec) {
            s[1] = s[1] || '';
            s[1] += new Array(prec - s[1].length + 1).join('0');
        }
    }
    else {
        if (s.length == 1) {
            return s[0];
        }
        else {
            if ((s[1] || '').length < prec) {
                s[1] = s[1] || '';
                s[1] += new Array(prec - s[1].length + 1);
            }
        }
    }

    return s.join(dec);
}

function NumberReformat(val) {
    if (val != undefined) {
        val = val.toString().replace(/\./g, '');
        val = val.replace(/\,/g, '.');
    }
    return val;
}
//EOF Reformat

function ReplaceNull(val) {
    if (val == null || val == undefined) val = "";

    return val;
}

function ShowLoader() {
    $('.ajax-loader').show();
}

function HideLoader() {
    $('.ajax-loader').hide();
}

function NotifBoxSuccess(title, content, btn, timeout) {
    var actionButton = btn == undefined ? "" : " <p class='text-align-right'><a href-void class='btn btn-danger btn-sm'><i class='fa fa-times'</i></a></p>";
    var opt = {
        title: title == undefined ? "" : title,
        content: content == undefined ? "" : content + actionButton,
        color: "#356E35",
        icon: "fa fa-check fadeInRight animated",
    };
    timeout == undefined ? opt.timeout = 5000 : timeout == 0 ? "" : opt.timeout = timeout;
    $.smallBox(opt);
}

function NotifBoxWarning(title, content, btn, timeout) {
    var actionButton = btn == undefined ? "" : " <p class='text-align-right'><a href-void class='btn btn-danger btn-sm'><i class='fa fa-times'</i></a></p>";
    var opt = {
        title: title == undefined ? "" : '<span style="color: #000f6a;">' + title + '</span>',
        content: content == undefined ? "" : '<span style="color: #000f6a;">' + content + actionButton + '</span>',
        color: "#ffad48",
        icon: "fa fa-info bounce animated",
    };
    timeout == undefined ? opt.timeout = 5000 : timeout == 0 ? "" : opt.timeout = timeout;
    $.smallBox(opt);
}

function NotifBoxError(title, content, btn, timeout) {
    var actionButton = btn == undefined ? "" : " <p class='text-align-right'><a href-void class='btn btn-danger btn-sm'><i class='fa fa-times'</i></a></p>";
    var opt = {
        title: title == undefined ? "" : title,
        content: content == undefined ? "" : content + actionButton,
        color: "#A90329",
        icon: "fa fa-warning bounce animated",
    };
    timeout == undefined ? opt.timeout = 5000 : timeout == 0 ? "" : opt.timeout = timeout;
    $.smallBox(opt);
}

function NotifBoxError2(title, error, status, btn, timeout) {
    if (status === 401) {
        content = "401 - Sesi Anda sudah habis.";
    }
    else {
        var content = status + (!isNullOrEmpty(error.Message) ? " - " + error.Message : "");
    }
    var actionButton = btn == undefined ? "" : " <p class='text-align-right'><a href-void class='btn btn-danger btn-sm'><i class='fa fa-times'</i></a></p>";
    var opt = {
        title: title == undefined ? "" : title,
        content: content == undefined ? "" : content + actionButton,
        color: "#A90329",
        icon: "fa fa-warning bounce animated",
    };
    timeout == undefined ? opt.timeout = 5000 : timeout == 0 ? "" : opt.timeout = timeout;
    $.smallBox(opt);

    return;
}

function NotifBoxErrorTable(title, error, status, $state, authSvc, btn, timeout) {
    HideLoader();

    if (status === 401) {
        content = "401 - Sesi Anda sudah habis.";
        var actionButton = btn == undefined ? "" : " <p class='text-align-right'><a href-void class='btn btn-danger btn-sm'><i class='fa fa-times'</i></a></p>";
        var opt = {
            title: title == undefined ? "" : title,
            content: content == undefined ? "" : content + actionButton,
            color: "#A90329",
            icon: "fa fa-warning bounce animated"
        };
        timeout == undefined ? opt.timeout = 5000 : timeout == 0 ? "" : opt.timeout = timeout;
        $.smallBox(opt);

        authSvc.logOut();
        $state.go('login');

        return;
    }
    else {
        HideLoader();
        var msg = isNullOrEmpty(error) ? "" : " - " + (isNullOrEmpty(error.Message) ? error : error.Message);
        var content = status + msg;
        var actionButton = btn == undefined ? "" : " <p class='text-align-right'><a href-void class='btn btn-danger btn-sm'><i class='fa fa-times'</i></a></p>";
        var opt = {
            title: title == undefined ? "" : title,
            content: content == undefined ? "" : content + actionButton,
            color: "#A90329",
            icon: "fa fa-warning bounce animated",
        };
        timeout == undefined ? opt.timeout = 5000 : timeout == 0 ? "" : opt.timeout = timeout;
        $.smallBox(opt);

        return;
    }
}

function ShowReport(reportid, pparam) {
    window.open('Report/Viewer.aspx?rptid=' + reportid + '&pparam=' + pparam);
}

function ShowReportToPDF(reportid, pparam) {
    window.open('Report/Viewer.aspx?rptid=' + reportid + '&pparam=' + pparam);
}

Array.prototype.myUnique = function () {
    var r = new Array();
    o: for (var i = 0; i < this.length; i++) {
        for (var x = 0; x < r.length; x++) {
            if (r[x] == this[i]) {
                continue o;
            }
        }
        r[r.length] = this[i];
    }
    return r;
};

function ShowBackButton(val) {
    var previousState = JSON.parse(localStorage.getItem('previousState'));
    if (previousState == null || previousState == undefined) {
        $('#backButton').addClass('hidden');
    }
    else {
        var state = previousState.state;
        if (val) {
            if (!isNullorEmpty(state)) {
                $('#backButton').removeClass();
            }
            else {
                $('#backButton').addClass('hidden');
            }
        }
        else {
            $('#backButton').addClass('hidden');
        }
    }
}

function ReplacePreviousState(state) {
    var a = localStorage.getItem(state);
    var value = JSON.stringify({
        state: state, show: true
    });
    localStorage.setItem('previousState', value);

    var previousState = JSON.parse(localStorage.getItem('previousState'));
    var state2 = previousState.state;
}

function GetColumnFromTable(tableName) {
    var columnName = [];
    var columnTitle = [];
    var xlsColumn = {};

    $('#' + tableName + ' > thead > tr > th').each(function () {
        columnName.push($(this).attr('name'));
        columnTitle.push($(this).text());
    })

    xlsColumn = {
        'ColumnName': columnName,
        'ColumnTitle': columnTitle
    }

    return xlsColumn;
};

function trimValue(val) {
    var v = IsNullOrEmpty(val) ? val : val.trim();

    return v;
}

function TrimValue(val) {
    var v = IsNullOrEmpty(val) ? val : val.trim();

    return v;
};

function ListYear(currentYear) {
    var curYear = currentYear || new Date().getFullYear();
    var years = [];
    var objYear = {};
    var year = curYear.toString();
    for (var i = 2; i >= 1; i--) {
        year = (curYear - i).toString();
        var objYear = {
            id: year,
            value: year,
            text: year
        }

        years.push(objYear);
    }

    year = (curYear).toString()
    objYear = {
        id: year,
        value: year,
        text: year
    }
    years.push(objYear);

    for (var i = 1; i <= 2; i++) {
        year = (curYear + i).toString();
        objYear = {
            id: year,
            value: year,
            text: year
        }

        years.push(objYear);
    }

    return years;
}

function GetCode(val) {
    var split = val.split('-');
    return split.length > 0 ? split[0] : "";
}

function attachmentContent(data) {
    var ext = data.split('.')[1];
    var facls = 'fa fa-file';
    switch (ext) {
        case 'pdf':
            facls = 'fa fa-file-pdf-o';
            break;
        case 'xls':
        case 'xlsx':
            facls = 'fa fa-file-excel-o';
            break;
        case 'doc':
        case 'docx':
            facls = 'fa fa-file-word-o';
            break;
        case 'ppt':
        case 'pptx':
            facls = 'fa fa-file-powerpoint-o';
            break;
        default:
            break;
    }
    var content = '<span><i class="' + facls + ' txt-color-red"> ' + data + '</i></span>';;

    return content;
}

function sumFromArray(items, prop) {
    return items.reduce(function (a, b) {
        return a + b[prop];
    }, 0);
};