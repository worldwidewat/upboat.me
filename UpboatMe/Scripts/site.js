﻿var rootUrl = 'http://upboat.me';

$(function () {

    $(document).foundation();

    $('#create-meme').click(function () {
        var name = $('#meme-name').val();
        var first = $('#first-line').val().replace(/ /g, "-");
        if (first==="") {
            first = "-";
        }
        var second = $('#second-line').val().replace(/ /g, "-");
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
});