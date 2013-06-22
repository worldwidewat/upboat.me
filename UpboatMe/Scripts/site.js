
$(function () {
    $('#create-meme').click(function () {
        var name = $('#meme-name').val();
        var first = $('#first-line').val();
        var second = $('#second-line').val();
        var url = '/' + name + '/' + first + '/' + second;

        $('#meme-preview').attr('src', url);
    });
});