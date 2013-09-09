$(function () {
    $(document).foundation();
    
    $("#builder-form").on("submit", function(e) {
        var name = $(this).find('[name=name]').val();
        var top = encodeURIComponent($(this).find('[name=top]').val()) || "-";
        var bottom = encodeURIComponent($(this).find('[name=bottom]').val());

        var url = $(this).attr('action')
            + "/" + name
            + "/" + top
            + "/" + bottom;

        e.preventDefault();
        window.location.href = url;
    });

    $("#share-url").on("click", function (e) {
        if (document.selection) {
            var range = document.body.createTextRange();
            range.moveToElementText(this);
            range.select();
        } else if (window.getSelection) {
            var range = document.createRange();
            range.selectNode(this);
            window.getSelection().addRange(range);
        }
    });
});

function doListSearch(value) {
    $('.meme.panel').hide().filter(function (index) {
        return $(this).text().toLowerCase().indexOf(value) !== -1 || value === '';
    }).show();
}