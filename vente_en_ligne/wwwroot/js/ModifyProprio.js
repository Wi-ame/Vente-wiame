function loadContent(action) {
    $.ajax({
        url: '/Proprietaires/' + action, // Assurez-vous que le chemin est correct
        type: 'GET',
        success: function (result) {
            $('#main-content').html(result);
        }
    });
}