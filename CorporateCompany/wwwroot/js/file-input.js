$(document).ready(function () {
    $('#customFile').on("change", function () {
        var fileLabel = $(this).next('#customFileLabel');
        var files = $(this)[0].files;
        if (files.length > 1) {
            fileLabel.html(files.length + ' dosya seçildi');
        }
        else if (files.length == 1) {
            fileLabel.html(files[0].name);
        }
    });
});