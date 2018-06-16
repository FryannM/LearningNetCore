// Write your JavaScript code.

$('#modalEditar').on('shown.bs.modal', function () {
    $('#myInput').focus()
})

function getUser(id, action) {

    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (reponse) {
            ShowUser(reponse);
        }
    });
}

var items;
var id;
var userName;
var email;
var phoneNumber;


var accessFailedCount;
var concurrencyStamp;
var emailConfirmed;
var lockoutEnabled;
var lockoutEnd;
var normalizedUserName;
var normalizedEmail;
var passwordHash;
var phoneNumberConfirmed;
var securityStamp;
var twoFactorEnabled;


function ShowUser(reponse) {

    items = reponse;
    
    $.each(items, function (index, val) {
        $('input[name=id]').val(val.id);
        $('input[name=UserName]').val(val.userName);
        $('input[name=email]').val(val.email);
        $('input[name=PhoneNumber]').val(val.phoneNumber);
    });
}


function EditUser(action) {
    id = $('input[name=id]')[0].value;
    email = $('input[name=email]')[0].value;
    phoneNumber = $('input[name=PhoneNumber]')[0].value;


    $.each(items, function (index, val) {
        accessFailedCount = val.accessFailedCount;
        concurrencyStamp = val.concurrencyStamp;
        emailConfirmed = val.emailConfirmed;
        lockoutEnabled = val.lockoutEnabled;
        lockoutEnd = val.lockoutEnd;
        userName = val.userName;
        normalizedUserName = val.normalizedUserName;
        normalizedEmail = val.normalizedEmail;
        passwordHash = val.passwordHash;
        phoneNumberConfirmed = val.phoneNumberConfirmed;
        securityStamp = val.securityStamp;
        twoFactorEnabled = val.twoFactorEnabled;
    });

    $.ajax({
        type: 'POST',
        url: action,
        data: {
            id, userName, email, phoneNumber, accessFailedCount,
            concurrencyStamp, emailConfirmed, lockoutEnabled, lockoutEnd,
            normalizedEmail, normalizedUserName, passwordHash, phoneNumberConfirmed,
            securityStamp, twoFactorEnabled

        },


         success: function (response) {
             if (response = "Save") {
                 window.location.href = "Users"
             }
             else {
                 alert("No se pueden Editar los Datos del Usuario");

             }

        }

    });
}