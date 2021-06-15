
//for front end
$('input[type="file"]').change(function () {
    var idName = $(this).attr('class');
    idName = idName.replace('form-control', '').trim();
    var id = "#" + idName;
    var data = new FormData();
    var files = $(this).get(0).files;

    var val = $(this).val();
    var extension = (val.substr((val.lastIndexOf('.') + 1))).trim().toLowerCase();
    if (extension == 'png' || extension == 'jpg' || extension == 'jpeg') {
        data.append("Files", files[0]);
        $.ajax({
            type: "POST",
            url: "/Ajax/ConvertFileToString",
            data: data,
            processData: false,
            contentType: false,
            success: function (response) {
                $(id).val(response)
                DisplayImageInDiv(idName, response);
            },
            error: function (response) {
                alert(response)
            }
        })
    }
    
})

function ElementValidation(elementId, required = null, maxlength = null, maxlengthNumber = null) {
    if (required == 'required') {
        var isValid = RequiredHandling(elementId);
        if (isValid == false) return false;
    }
    if (maxlength == 'maxlength') {
        var isValid = MaxlengthHandling(elementId, maxlengthNumber);
        if (isValid == false) return false;
    }
    return true;
}
function FileHandling(elementId) {
    debugger;
    var element = "#" + elementId;
    var $el = $(element);
    var val = $el.val();
    removeErrorClasses(element);
    if ($el.attr('type') == 'file' && val.length > 0) {
        var extension = val.substr((val.lastIndexOf('.') + 1));
        if (extension.toLowerCase() != 'jpg' && extension.toLowerCase() != 'png' && extension.toLowerCase() != 'jpeg') {
            AddErrorClasses(element)
            AddErrorMessage(element,
                $el.attr('name') + ' must be type of jpg,png or jpeg ')
            return false;
        }
    }
    return true;
}
function RequiredHandling(elementId) {
    var element = "#" + elementId;
    var $el = $(element);
    removeErrorClasses(element);
    if (!$el.val()) {
        AddErrorClasses(element)
        AddErrorMessage(element,
            $el.attr('name') + ' is required ')
        return false;
    }
    return true;
}
function MaxlengthHandling(elementId, length) {
    var element = "#" + elementId;
    var $el = $(element);
    removeErrorClasses(element);
    if ($el.val().length > length) {
        AddErrorClasses(element)
        AddErrorMessage(element,
            $el.attr('name') + ' must be less than ' + length);
        return false;
    }
    return true;
}

function removeErrorClasses(element) {
    var $el = $(element);
    $el.parent().addClass('form-group').removeClass('error');
    $el.parent().removeClass('.error-message');
    $el.parent().find('.error-message').remove();
}
function AddErrorClasses(element) {
    var $el = $(element);
    $el.parent().removeClass('form-group');
    $el.parent().addClass('error');
}
function AddErrorMessage(element, message) {
    var $el = $(element);
    $el.parent().append('<label class="error-message">' + message + '</label>');
}