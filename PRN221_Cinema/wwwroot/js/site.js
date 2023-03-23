// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.frm-genre input').on('click', function (e) {
    var genreId = e.currentTarget.defaultValue;
    var baseUrl = window.location.origin;
    window.location.href = baseUrl + "/?genreId=" + genreId;

});