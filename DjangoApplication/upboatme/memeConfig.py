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

memes[u'bje', u'eel', u'badjokeeel', u'badjokeel'] = \
    meme(template='bad-joke-eel')

memes[u'blb', u'badluckbrian'] = \
    meme(template='bad-luck-brian')

memes[u'losemind', u'eltm', u'losesmind', u'everyonelosestheirminds'] = \
    meme(template='everyone-loses-their-minds')

memes[u'fwp', u'firstworldproblems'] = \
    meme(template='first-world-problems')

memes[u'fry', u'futuramafry'] = \
    meme(template='futurama-fry')

memes[u'ggg', u'goodguygreg'] = \
    meme(template='good-guy-greg')

memes[u'ihyk', u'illhaveyouknow', u'spongebob'] = \
    meme(template='ill-have-you-know')

memes[u'mam', u'maliciousadvicemallard'] = \
    meme(template='malicious-advice-mallard')

memes[u'mimitw', u'tmimitw', u'themostinterestingmanintheworld', u'mostinterestingmanintheworld'] = \
    meme(template='the-most-interesting-man-in-the-world')

memes[u'oag', u'overlyattachedgirlfriend'] = \
    meme(template='overly-attached-girlfriend')

memes[u'omm', u'man', u'overlymanlyman'] = \
    meme(template='overly-manly-man')

memes[u'ss', u'scumbag-steve'] = \
    meme(template='scumbag-steve')

memes[u'sap', u'sociallyawkwardpenguin'] = \
    meme(template='socially-awkward-penguin')

memes[u'saap', u'sociallyawesomeawkwardpenguin'] = \
    meme(template='socially-awesome-awkward-penguin')

memes[u'saap2', u'sociallyawkwardawesomepenguin'] = \
    meme(template='socially-awkward-awesome-penguin')

memes[u'sk', u'successkid'] = \
    meme(template='success-kid')

memes[u'scc', u'suddenclarityclarence'] = \
    meme(template='sudden-clarity-clarence')
