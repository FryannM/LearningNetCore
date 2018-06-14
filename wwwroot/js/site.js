// Write your JavaScript code.

$('#myModal').on('shown.bs.modal', function () {
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
function ShowUser(reponse) {

    items = reponse;

    $.each(items, function (index, val) {
        $('input[name=id]').val(val.id);
        $('input[name=UserName]').val(val.userName);
        $('input[name=Email]').val(val.Email);
        $('input[name=PhoneNumber]').val(val.PhoneNumber);


    });

   

}