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
});

function doListSearch(value) {
    $('.meme.panel').hide().filter(function (index) {
        return $(this).text().toLowerCase().indexOf(value) !== -1 || value === '';
    }).show();
}