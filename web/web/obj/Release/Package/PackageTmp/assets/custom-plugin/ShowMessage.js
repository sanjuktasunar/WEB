
function ShowMessage(message, returnUrl) {
    debugger;
    if (message.length > 0) {
        var str = message.split("+");
        const wrapper = document.createElement('div');
        wrapper.innerHTML = str[0];
        if (parseInt(str[1]) >= 0) {
            swal({
                content: wrapper, 
                icon: "success",
            })
                .then(() => {
                    if (parseInt(returnUrl.length) > 0) {
                        window.location.href = returnUrl;
                    }
                    else {
                        $("#btnSave").prop('disabled', false)
                        $("#btnSave span").html('Save')
                    }
                });
        }
        else {
            
            swal({
                content: wrapper, icon: "error"
            })
            $("#btnSave").prop('disabled', false)
            $("#btnSave span").html('Save')
        }
    }
    else {
        window.location.href = "/Account/Logout"
    }
}

function ShowConfirmationMessage() {
   
}

function ShowDeleteMessage(message, returnUrl) {
    debugger;
    if (message.length > 0) {
        var str = message.split("+");
        if (str[1] == 0) {
            swal({
                text: str[0],
                icon: "success",
            })
                .then(() => {
                    if (returnUrl.length > 0) {
                        window.location.href = returnUrl;
                    }
                });
        }
        else {
            const wrapper = document.createElement('div');
            wrapper.innerHTML = message;
                swal({
                    content: wrapper, icon: "error"
                })
        }
    }
    else {
        window.location.href = "/Logout"
    }
}

