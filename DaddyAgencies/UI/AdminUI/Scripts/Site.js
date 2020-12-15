function OnFailure(error) {
    $(':input[type="submit"]').prop('disabled', false);
    $("#loading").css('display', 'none');
    alert(error);
}

function OnLoading() {
    $(':input[type="submit"]').prop('disabled', true);
    $("#loading").css('display', 'inline-block');
}

function ShowRemoveModal(url, id) {
    this.dataUrl = url;
    this.dataId = id;
    this.callBackUrl = callBackUrl;
    $('#removeModal').modal('show');
}

function OnRemoveModalConfirm() {
    DeleteData(dataUrl, dataId);
}

function DeleteData(url, id) {
    alert(url + "/" + id)
    $.ajax({
        type: 'post',
        url: url,
        dataType: 'json',
        data: { id: id },
        success: function (res) {
            if (res.IsSucceed)
                location.href = location.href;
            else
                alertify.error(res.Description);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alertify.error(thrownError);
        }
    });
}

function ChangeLang(lang, url) {

    $.ajax({
        url: url,
        type: 'post',
        data: { lang: lang },
        success: function (res) {
            if (res.IsCompleted)
                location.reload();
            else
                alertify.error(res.Description);
        },
        error: function () {
            alertify.error('error');
        }
    });
}

function preview_image() {
    var total_file = document.getElementById("upload_file").files.length;
    var colmd3 = "col-md-3";
    for (var i = 0; i < total_file; i++) {
        $('#image_preview').append("<div class = '" + colmd3 + "'><img src='" + URL.createObjectURL(event.target.files[i]) + "'><div>");
        $('#image_preview div').css({ "margin-bottom": "10px" });
        $('#image_preview div img').css({ "height": "150px", "width": "200px" });
    }
}

function HrefMe(url, data) {
    location.href = url + '/' + data;
}

function GoBack() {
    window.history.back();
}

function ShowInseptionDetailsModal(url, id) {
    $.ajax({
        type: 'get',
        url: url,
        data: { uid: id },
        dataType: 'html',
        success: function (htmlData) {
            $('#InseptionDetailsBodyModalId').html(htmlData);
            $('#InseptionDetailsModalId').modal('show');
        },
        error: function () {
            alert("Error!");
        }
    });
}

function ConfirmInseption(uid,url) {
    var message = $('#MessageForCustomerId').val().trim();
    var dateStr = $('#datepicker').val();
    var time = $('#timeSelector').val();
    dateArray = dateStr.split('-');
    //date = dateArray[1] + '/' + dateArray[0] + '/' + dateArray[2];
    $.ajax({
        type: 'post',
        url: url,
        dataType: 'json',
        data: { uid: uid, ConfirmedDate: dateStr, Time:time, message: message },
        success: function (data) {
                location.reload();
        },
        error: function () {
            alert('Error');
        }
    });
}



function RejectInseption(uid,url) {
    var message = $('#MessageForCustomerId').val();
    $.ajax({
        type: 'post',
        url: url,
        dataType: 'json',
        data: { uid: uid, message: message },
        success: function (data) {
            location.reload();
        },
        error: function () {
            alert('Error');
        }
    });
}

function preview_images() {
    var totalFile = document.getElementById("images").files.length;
    $('#image_preview').html('');
    for (var i = 0; i < totalFile; i++) {
        $('#image_preview').append("<div class='col-md-3'><img style='max-height: 200px;' width=100% class='img-responsive' src='" + URL.createObjectURL(event.target.files[i]) + "'></div>");
    }
}