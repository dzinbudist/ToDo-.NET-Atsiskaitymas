$('#showModal').click(function () {
    $('form').find('input[type=text], input[type=date], input[type=number], input[type=email], textarea').val('');
    $('form').find('select').val('Low');
    $('#createModal').on('shown.bs.modal', function () {
        $('#itemEdit').focus();
    }) 
});
