
//for front end
$('input[type="file"]').change(function () {
    var idName = $(this).attr('class');
    idName = idName.replace('form-control', '').trim();
    var id = "#" + idName;
    var data = new FormData();
    var files = $(this).get(0).files;
    data.append("Files", files[0]);
    $.ajax({
        type: "POST",
        url: "/Ajax/ConvertFileToString",
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {
            $(id).val(response)
        },
        error: function (response) {
            alert(response)
        }
    })
})