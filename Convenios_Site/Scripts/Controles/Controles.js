
function Browser() {
    var ua, s, i;
    this.isIE = false;
    this.isNS = false;
    this.version = null;
    ua = navigator.userAgent;

    s = "MSIE";
    if ((i = ua.indexOf(s)) >= 0) {
        this.isIE = true;
        this.version = parseFloat(ua.substr(i + s.length));

        return;
    }

    s = "Netscape6/";
    if ((i = ua.indexOf(s)) >= 0) {
        this.isNS = true;
        this.version = parseFloat(ua.substr(i + s.length));
        return;
    }

    // Treat any other "Gecko" browser as NS 6.1.
    s = "Gecko";
    if ((i = ua.indexOf(s)) >= 0) {
        this.isNS = true;
        this.version = 6.1;
        return;
    }
}

var browser = new Browser();
var isNN = browser.isNS;
var isIE7 = browser.isIE;

function fechaCompleta(dia, mes, anio) {
    if (dia == '' || mes == '' || anio == '') {
        return false;
    }
    else {
        if (anio.length != 4) {
            return false;
        }
        else {
            return true;
        }
    }
}
function isDateError(obj) {
    try {
        var d = $('#<%= tblDate.ClientID %>').find(':input')[0].value;
        var m = $('#<%= tblDate.ClientID %>').find(':input')[1].value;
        var a = $('#<%= tblDate.ClientID %>').find(':input')[2].value;
        if (fechaCompleta(d, m, a)) {
            var retorno = true;
            var lblError = $('#<%= lbl_ErrorFecha.ClientID %>');
            if (!$('#<%= tblDate.ClientID %>').find(":input").attr("disabled") &&
                        !isDateValid(d, m, a)) {
                lblError.text('Debe ingresar una fecha válida.');
                lblError.style = "";
                retorno = false;
            }
            else {
                lblError.text('');
                lblError.style = "display:none";
                retorno = true;
            }
            return retorno;
        }
        else {
            return true;
        }
    }
    catch (e) {
        return false;
    }
} 

function isDateValid(dia, mes, anio) {
    if (parseInt(anio) > 1499 && parseInt(anio) < 2999) {
        if (parseInt(mes, 10) > 0 && parseInt(mes, 10) <= 12) {
            if (parseInt(dia, 10) > 0 && parseInt(dia, 10) <= daysInMonth(mes, anio))
                return true;
            else
                return false;
        }
        else
            return false;
    }
    else
        return false;
}

function daysInMonth(monthNum, yearNum) {
    var monthNum = Number(monthNum);
    var yearNum = Number(yearNum);    
    var d = new Date(yearNum, monthNum, 0);
    var day = d.getDate().toString();
    
    return day;
}

function cDate(sDate) {
    fecha = new Date(fecha);
    return fecha
}

function validarNumero(e) {
    try {
        tecla_codigo = (document.all) ? e.keyCode : e.which;
        if (tecla_codigo == 8) return true;
        patron = /[0-9]/;
        tecla_valor = String.fromCharCode(tecla_codigo);
        return patron.test(tecla_valor);
    }
    catch (e) {
        return false;
    }
}

/* Funciones para strings */
function trim(stringToTrim) {
    return stringToTrim.replace(/^\s+|\s+$/g, "");
}

function PadLeftCount(str, pad, count) {
    while (str.length < count) {
        str = pad + str;
    }
    return str;
}

function padLeft(obj, pad) {
    var sPad = '';
    if (obj.value.length > 0) {
        while ((sPad.length + obj.value.length) < obj.maxLength) {
            sPad += pad;
        }
        obj.value = sPad + obj.value;
    }
}

function SoloNumeros(myfield, e) {
    try
    {
        var keycode;
        if (!window.event.ctrlKey) {
            if (window.event) keycode = window.event.keyCode;
            else if (e) keycode = e.which;
            else return true;
            if (((keycode > 47) && (keycode < 58)) || (keycode == 8)) { return true; }
            else
                return false;
        }
        else
            return false;
    }
    catch (e) {
        return false;
    }
}

function getIndex(input) {
    var index = -1, i = 0, found = false;
    while (i < input.form.length && index == -1)
        if (input.form[i] == input) index = i;
    else i++;
    return index;
}
function nextIndexEnabled(input, i) {
    var index = i + 1;
    while (input.form[index].disabled ||
                   input.form[index].type == 'radio')
        index++;
    return index;
}
function autoTab(input, e, maxFiels) {
    try
    {
        //var x = document.getElementById('<%=txtThridField.ClientID%>');
        var keyCode = (isNN) ? e.which : e.keyCode;
        var filter = (isNN) ? [0, 8, 9] : [0, 8, 9, 16, 17, 18, 37, 38, 39, 40, 46];
        var nameLastFiel = "";
        switch (maxFiels)
        {
            case 2:
                nameLastFiel = 'txtSecondField';
                break;
            case 3:
                nameLastFiel = 'txtThridField';
            break;
        }
        switch(keyCode)
        {
        case 8 : 
            if ((input.id.indexOf('txtFirstField') == -1) &&
                isStartPos(input)) {
                var obj = input.form[getIndex(input) - 1];
                obj.focus();
                setEnd(obj);
            }
        break; 
        case 37 : 
            if (input.id.indexOf('txtFirstField') == -1) {
                if (isStartPos(input)) {
                    var obj = input.form[getIndex(input) - 1];
                    obj.focus();
                    setEnd(obj);
                }
            }
        break;
        case 39 :
            if (input.id.indexOf(nameLastFiel) == -1) {
                if (isEndPos(input)) {
                    input.value = input.value.slice(0, input.maxLength);
                    var i = nextIndexEnabled(input, getIndex(input));
                    input.form[i].focus();
                    //setEnd(input);
                    return true;
                }
            }
            break;
        case 110:
            if (input.id.indexOf(nameLastFiel) == -1) {
                if (input.value == '')
                    input.value = 0;
                input.form[nextIndexEnabled(input, getIndex(input))].focus();
                return true;
            }        
        default :
            if (input.value.length >= input.maxLength && !containsElement(filter, keyCode)) {
                
                input.value = input.value.slice(0, input.maxLength);
                if (input.id.indexOf(nameLastFiel) == -1)
                {
                    input.form[nextIndexEnabled(input, getIndex(input))].focus();
                    return true;
                }
                if (input.id.indexOf(nameLastFiel) != -1 &&
                    input.form[nextIndexEnabled(input, getIndex(input))].id.indexOf('txtFirstField') != -1) {
                    input.form[nextIndexEnabled(input, getIndex(input))].focus();
                    return true;
                }
            }
        break;
        }
    }
    catch (e) {
        return false;
    }
}

function IsBeginOrEnd(o) {
    var FieldRange = o.createTextRange();
    FieldRange.moveStart('character', o.value.length);
    return 0;
}

function setEnd(o) {
    var FieldRange = o.createTextRange();
    FieldRange.moveStart('character', o.value.length);
    FieldRange.collapse();
    FieldRange.select();
}

function containsElement(arr, ele) {
    var found = false, index = 0;
    while (!found && index < arr.length)
        if (arr[index] == ele)
        found = true;
    else
        index++;
    return found;
}

function isStartPos(textElement) {
    //save off the current value to restore it later,
    textElement.maxLength = textElement.maxLength + 1;
    var sOldText = textElement.value;

    //create a range object and save off it's text
    var objRange = document.selection.createRange().duplicate();
    var sOldRange = objRange.text;

    //set this string to a small string that will not normally be encountered
    var sWeirdString = '#';

    //insert the weirdstring where the cursor is at
    objRange.text = sOldRange + sWeirdString;
    objRange.moveStart('character', (0 - sOldRange.length - sWeirdString.length));

    //save off the new string with the weirdstring in it
    textElement.maxLength = textElement.maxLength - 1;
    var sNewText = textElement.value;

    //set the actual text value back to how it was
    objRange.text = sOldRange;

    return (sNewText.indexOf(sWeirdString) == 0);
}

function isEndPos(textElement) {
    textElement.maxLength = textElement.maxLength + 1;
    
    var sOldText = textElement.value;
    var objRange = document.selection.createRange().duplicate();
    var sOldRange = objRange.text;
    var sWeirdString = '#';

    objRange.text = sOldRange + sWeirdString;
    objRange.moveStart('character', (0 - sOldRange.length - sWeirdString.length));
    textElement.maxLength = textElement.maxLength - 1;
    
    var sNewText = textElement.value;
    objRange.text = sOldRange;

    return (sNewText.indexOf(sWeirdString) == textElement.value.length);
}


//AGREGADO SERGIO
function containsElementControl(arr, ele) {
    var found = false, index = 0;
    while (!found && index < arr.length)
        if (arr[index] == ele)
        found = true;
    else
        index++;
    return found;
}

function getIndexControl(input) {
    var index = -1, i = 0, found = false;
    while (i < input.form.length && index == -1)
        if (input.form[i] == input) index = i;
    else i++;
    return index;
}

function autoTabControl(input, len, e) {
    var keyCode = (isNN) ? e.which : e.keyCode;
    var filter = (isNN) ? [0, 8, 9] : [0, 8, 9, 16, 17, 18, 37, 38, 39, 40, 46];
    if (input.value.length >= len && !containsElementControl(filter, keyCode)) {
        input.value = input.value.slice(0, len);
        input.form[(getIndexControl(input) + 1) % input.form.length].focus();

        return true;
    }
}

function autoTabControl_conDestino(input, len, e, idObjDestino) {
    var keyCode = (isNN) ? e.which : e.keyCode;
    var filter = (isNN) ? [0, 8, 9] : [0, 8, 9, 16, 17, 18, 37, 38, 39, 40, 46];
    if (input.value.length >= len && !containsElementControl(filter, keyCode)) {
        input.value = input.value.slice(0, len);
        document.getElementById(idObjDestino).focus();
        return true;
    }
}

function validarNumeroControl(e) {
    tecla_codigo = (document.all) ? e.keyCode : e.which;
    if (tecla_codigo == 8) return true;
    patron = /[0-9]/;
    tecla_valor = String.fromCharCode(tecla_codigo);
    return patron.test(tecla_valor);
}

function validarLetraAlfabetoControl(e) {
    tecla_codigo = (document.all) ? e.keyCode : e.which;
    if (tecla_codigo == 8) return true;
    patron = /[A-Za-z]/;
    tecla_valor = String.fromCharCode(tecla_codigo);
    return patron.test(tecla_valor);
}

