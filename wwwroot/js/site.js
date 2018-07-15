﻿// Write your JavaScript code.

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
var selectRole;
var role;


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
    for (var i = 0; i < 3; i++) {
        var x = document.getElementById('Select');
        x.remove(i);
    }
    
    $.each(items, function (index, val) {
        $('input[name=id]').val(val.id);
        $('input[name=UserName]').val(val.userName);
        $('input[name=email]').val(val.email);
        $('input[name=PhoneNumber]').val(val.phoneNumber);
        document.getElementById('Select').options[0] = new Option(val.role, val.roleId);
    });
}


function EditUser(action) {
    id = $('input[name=id]')[0].value;
    email = $('input[name=email]')[0].value;
    phoneNumber = $('input[name=PhoneNumber]')[0].value;
    role = $('input'['name =Select']);
    selectRole = role.options[role.selectedIndex].text;


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
            securityStamp, twoFactorEnabled,selectRole

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