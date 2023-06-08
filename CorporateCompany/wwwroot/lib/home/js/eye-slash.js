$(document).ready(function () {
    $(".password-slash").on('click', function (event) {

        if ($('.text-slash').attr("type") == "text") {
            $('.text-slash').attr('type', 'password');
            $('.icon-slash').toggleClass("fa-eye-slash fa-eye");
        } else if ($('.text-slash').attr("type") == "password") {
            $('.text-slash').attr('type', 'text');
            $('.icon-slash').toggleClass("fa-eye fa-eye-slash");
        }
    });
});