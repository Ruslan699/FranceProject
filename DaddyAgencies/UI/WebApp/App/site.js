function formatMoney(n, c, d, t, smb) {
    var c = isNaN(c = Math.abs(c)) ? 2 : c,
        d = d == undefined ? "." : d,
        t = t == undefined ? "," : t,
        s = n < 0 ? "-" : "",
        i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
        j = (j = i.length) > 3 ? j % 3 : 0;

    return smb + s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

var baseUrl = '';
var regionGetUrl = baseUrl + '/Region/GetRegions';

function loading(success, data) {

}

var regions = [];
var departaments = [];
var departamentSelector = [];


function syncSelections() {
    $('.path_hover_state').removeClass("path_hover_state");
    $('.p_hover_state').removeClass("p_hover_state");

    $.each($("#regionSelector").val(), function (i, e) {
        var pathId = e + '_path';
        var labelId = e + '_label';
        $("#" + pathId).addClass("path_hover_state");
        $("#" + labelId).addClass("p_hover_state");
    });
}

function configureFilter(regionsRaw, postalCodePlaceHolder, postlalCodeNoResults) {
    regions = regionsRaw;
    departaments = regions.reduce(selectMany(x => x.Departaments), []);

    $(function () {
        $("#postalCodeSelector").chosen({
            placeholder_text: postalCodePlaceHolder,
            no_results_text: postlalCodeNoResults,
            search_contains: 'true'
        });
        $(".chosen-container").bind('keyup', function (e) {
            $('.chosen-drop').show();
        });
        $(".chosen-container").bind('mouseup', function (e) {
            $('.chosen-drop').hide();
        });
        $('.chosen-drop').hide();
    });

    //$('#regionSelector').on("changed.bs.select",
    //    function (e, clickedIndex, newValue, oldValue) {
    //        fillDepartaments(regions, $('#regionSelector').val(), $('#departamentSelector'));
    //        fillPostalCodes(departaments, $('#departamentSelector').val(), $('#postalCodeSelector'));
    //        $('.selectpicker').click();
    //        syncSelections();
    //    });

    $('#departamentSelector').on("changed.bs.select",
        function (e, clickedIndex, newValue, oldValue) {
            departamentSelector.push($('#departamentSelector').val());
            fillPostalCodes(departaments, departamentSelector, $('#postalCodeSelector'));
            $('.selectpicker').click();
        });
}

function fillDepartaments(regions, selectedRegions, selectPicker) {
    if (selectedRegions.length > 0)
        regions = regions.filter(x => selectedRegions.includes(x.Uid));

    var departaments = regions.reduce(selectMany(x => x.Departaments), [])
        .map(function (item) {
            var mapped = {};
            mapped.value = item.Uid;
            mapped.text = item.Name;
            return mapped;
        });
    setOptionsToSelectPicker(departaments, selectPicker);
}

function fillPostalCodes(departaments, selectedDepartaments, selectPicker) {
    if (selectedDepartaments.length > 0)
        departaments = departaments.filter(x => selectedDepartaments.includes(x.Uid))

    var postalCodes = departaments.reduce(selectMany(x => x.PostalCodes), [])
        .map(function (item) {
            var mapped = {};
            mapped.value = item.Uid;
            mapped.text = item.Name;
            return mapped;
        });
    setOptionsToChosen(postalCodes, selectPicker);
}


function mapOption(item) {
    return `<option value='${item.value}'>${item.text}</option>`;
}

function setOptionsToChosen(items, selectPicker) {
    var options = items.map(mapOption);
    $(selectPicker).html(options);
    $(selectPicker).trigger("chosen:updated");
}

function setOptionsToSelectPicker(items, selectPicker) {
    var options = items.map(mapOption);
    $(selectPicker).html(options);
    $(selectPicker).selectpicker('refresh');
}

function selectMany(f) {
    return function (acc, b) {
        return acc.concat(f(b));
    }
}

function validator() {
    if ($('.positive-input').val() === '0')
        $('.positive-input').val('');

    $('.positive-input').attr("min", "0");
    $('.positive-input').attr("oninput", "validity.valid||(value='');");
}

function CheckDateTypeIsValid(dateElemet) {
    var value = $(dateElemet).val();
    if (value == '')
        $(dateElemet).valid("false");
    else
        $(dateElemet).valid();
}
