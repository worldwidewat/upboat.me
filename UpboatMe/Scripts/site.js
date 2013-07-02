var rootUrl = 'http://upboat.me';

$(function () {

    $(document).foundation();

    $('#create-meme').click(function () {
        var name = $('#meme-name').val();
        var sanitizeRegex = / +/g;
        var first = $('#first-line').val().replace(sanitizeRegex, "-");
        if (first==="") {
            first = "-";
        }
        var second = $('#second-line').val().replace(sanitizeRegex, "-");
        var url = '/' + name + '/' + encodeURIComponent(first) + '/' + encodeURIComponent(second);
        
        $('#share-url').val(rootUrl + url);
        $('#meme-preview').attr('src', url);
    });

    var clip = new ZeroClipboard(document.getElementById("copy-button"), {
        moviePath: "Scripts/ZeroClipboard.swf"
    });

    clip.on('complete', function (client, args) {
        $(this).addClass('success');

        var button = $(this);

        setTimeout(function () { button.removeClass('success'); }, 1000);
    });

    $('#share-url').on('mouseup touchend', function (e) {
        this.select();
    });
});

function doListSearch(value) {
    $('.meme.panel').hide().filter(function (index) {
        return $(this).text().toLowerCase().indexOf(value) !== -1 || value === '';
    }).show();
}