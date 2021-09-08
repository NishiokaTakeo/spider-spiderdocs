$(function () {
    // Initialize modal dialog
    // attach modal-container bootstrap attributes to links with .modal-link class.
    // when a link is clicked with these attributes, bootstrap will display the href content in a modal dialog.
    $('body').on('click', '.modal-link', function (e) {
        e.preventDefault();
        $(this).attr('data-target', '#modal-container');
        $(this).attr('data-toggle','modal');

        $('.modal-content').width($(this).attr('dialogw'));
        $('.modal-content').height($(this).attr('dialogh'));
        $('.modal-content').draggable();
    });

    // Attach listener to .modal-close-btn's so that when the button is pressed the modal dialog disappears
    $('body').on('click', '.modal-close-btn', function () {
        $('#modal-container').modal('hide');
    });

    //clear modal cache, so that new content can be loaded
    $('#modal-container').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal');
    });

    $('#CancelModal').on('click', function () {
        return false;
    });
});
