$(function () {
    $(document).foundation();
    
    $("#builder-form").on("submit", function(e) {
        var url = $(this).attr('action');
        url = url
            + "/" + $(this).find('[name=name]').val()
            + "/" + encodeURIComponent($(this).find('[name=top]').val())
            + "/" + encodeURIComponent($(this).find('[name=bottom]').val());

        e.preventDefault();
        window.location.href = url;
    });
});

function doListSearch(value) {
    $('.meme.panel').hide().filter(function (index) {
        return $(this).text().toLowerCase().indexOf(value) !== -1 || value === '';
    }).show();
}