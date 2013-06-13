from DjangoApplication.upboatme.multi_key_dictionary import multi_key_dictionary


class meme():
    def __init__(self, template='404', font='Impact', fontSize=40, textFill='white', textStroke='black'):
        self.template = template + '-template.png'
        self.font = font + '.ttf'
        self.fontSize = fontSize
        self.textFill = textFill
        self.textStroke = textStroke

memes = multi_key_dictionary()

memes['default'] = meme()

memes[u'10guy', u'tenguy'] = \
    meme(template='10-guy')

memes[u'aam', u'actualadvicemallard'] = \
    meme(template='actual-advice-mallard')

memes[u'att', u'allthethings'] = \
    meme(template='all-the-things')

memes[u'blb', u'badluckbrian'] = \
    meme(template='bad-luck-brian')

memes[u'ggg', u'goodguygreg'] = \
    meme(template='good-guy-greg')

memes[u'mam', u'maliciousadvicemallard'] = \
    meme(template='malicious-advice-mallard')

memes[u'oag', u'overlyattachedgirlfriend'] = \
    meme(template='overly-attached-girlfriend')

memes[u'ss', u'scumbag-steve'] = \
    meme(template='scumbag-steve')

memes[u'sap', u'sociallyawkwardpenguin'] = \
    meme(template='socially-awkward-penguin')

memes[u'sk', u'successkid'] = \
    meme(template='success-kid')

memes[u'scc', u'suddenclarityclarence'] = \
    meme(template='sudden-clarity-clarence')
