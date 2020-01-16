$('#btnCreate').click(function () {
    let value = $("#itemEdit").val()
    if (value !== "") {
        $('#createModal').modal('hide');
    }
});