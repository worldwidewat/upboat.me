$(function () {
    $(document).foundation();
    
    $("#builder-form").on("submit", function(e) {
        var url = $(this).attr('action');
        url = url + "/" + $(this).find('[name=name]').val() + "/" + $(this).find('[name=top]').val() + "/" + $(this).find('[name=bottom]').val();
        window.location.href = url;
        e.preventDefault();
    });
});

function doListSearch(value) {
    $('.meme.panel').hide().filter(function (index) {
        return $(this).text().toLowerCase().indexOf(value) !== -1 || value === '';
    }).show();
}