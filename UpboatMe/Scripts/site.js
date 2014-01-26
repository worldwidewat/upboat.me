$(function () {
    $(document).foundation();
    
    $("#builder-form").on("submit", function(e) {
        var name = $(this).find('[name=name]').val();

        var lines = [];
        $.each($(this).find('.builder-line'), function (index, item) {
            lines.push($(item).val() || '-');
        });
        
        var url = $(this).attr('action')
            + "/" + name
            + "/" + lines.join('/')
            + ".jpg";

        e.preventDefault();
        window.location.href = url;
    });

    $('#builder-form select[name=name]').change(function () {
        $('#builder-form').find('.builder-line').remove();

        var count = $(this).find('option:selected').attr('data-line-count');

        for (var x = count; x > 0; x--) {
            $('<input type="text" class="builder-line" placeholder="line ' + x + '"/>').insertAfter('#builder-form select[name=name]');
        }
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