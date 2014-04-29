var builder = {
    currentMeme: null,
    lines: [],
    init: function (rootUrl, builderPath) {
        this.rootUrl = rootUrl;
        this.builderPath = builderPath;
        $(function () {
            $('#builder-form select[name=name]').change(function () {
                builder.updateMeme(true);

                $('#builder-form').find('.builder-line').remove();
                var count = $(this).find('option:selected').attr('data-line-count');
                for (var x = count; x > 0; x--) {
                    $('<input type="text" class="builder-line" value="' + (builder.lines[x-1] || '') + '" placeholder="line ' + x + '"/>').insertAfter('#builder-form select[name=name]');
                }
            });

            var typingTimer = null;
            $(document).on('keyup', 'input.builder-line', function () {
                if (typingTimer) {
                    clearTimeout(typingTimer);
                }
                typingTimer = setTimeout(function () {
                    builder.updateMeme(true);
                }, 300);
            });

            $(window).bind("popstate", function (evt) {
                var state = evt.originalEvent.state;
                if (state && state.meme) {
                    $('#builder-form select[name=name]').val(state.meme);
                    this.lines = state.lines;
                    for (var i = 0; i < this.lines.length; i++) {
                        $($('input.builder-line')[i]).val(this.lines[i]);
                    }
                    builder.updateMeme();
                }
            });

            builder.updateMeme(true);
        });
    },
    updateMeme: function (withPushState) {
        this.currentMeme = $('#builder-form select[name=name]').val();
        this.lines = [];
        var inputs = $('input.builder-line');
        for (var i = 0; i < inputs.length; i++) {
            this.lines.push($(inputs[i]).val());
        }
        var memePath = this.currentMeme + '/';
        for (var i = 0; i < this.lines.length; i++) {
            memePath += this.lines[i] + '/';
        }
        this.updatePreview(memePath);
        this.updateShareUrl(memePath);
        if (withPushState) {
            this.updateUrlState(memePath);
        }
    },
    updatePreview: function (memePath) {        
        $('#meme-preview').attr('src', this.rootUrl + memePath);
    },
    updateShareUrl: function (memePath) {
        var shareUrl = this.rootUrl + memePath.replace(/ /g, '-');
        var shareText = $('#builder-form select[name=name] :selected').text().trim();
        $('#share-url').html(shareUrl);
        $('#share-fb').attr('href', 'https://www.facebook.com/sharer/sharer.php?u=' + shareUrl);
        $('#share-tw').attr('href', 'https://twitter.com/share?url=' + shareUrl + '&via=upboatme&text=' + shareText);
        $('#share-re').attr('href', 'http://www.reddit.com/r/AdviceAnimals/submit?title=' + shareText + '&url=' + shareUrl);
        $('#share-pi').attr('href', 'http://pinterest.com/pin/create/button/?url=' + shareUrl + '&media=' + shareUrl + '&description=' + shareText);
    },
    updateUrlState: function (memePath) {
        if (this.builderPath && history && history.pushState) {
            history.pushState({ meme: this.currentMeme, lines: this.lines }, 'Builder', '/' + this.builderPath + memePath);
        }
    }
}