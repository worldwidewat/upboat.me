import os
import textwrap
from django.http import HttpResponse
from PIL import Image, ImageFont, ImageDraw
from DjangoApplication.upboatme.memeConfig import memes


# Draws two lines of text on the specified meme template name and returns it to the user as a PNG image
def make(request, name, first, second):
    memeKey = name.replace('-', '').lower()

    if memes.has_key(memeKey):
        config = memes[memeKey]
        firstClean = sanitizeLine(first);
        secondClean = sanitizeLine(second);
    else:
        config = memes['default']
        firstClean = sanitizeLine(u"404");
        secondClean = sanitizeLine(u"Y U NO USE VALID MEME NAME?");

    image = Image.open(getImagePath(config.template))

    writeText(image, config, firstClean, secondClean)

    response = HttpResponse(mimetype="image/png")
    image.save(response, "PNG")

    return response


# Write the first and second line of text to the image
def writeText(image, config, first, second):
    draw = ImageDraw.Draw(image)
    imageSize = image.size
    xBuffer = 30

    # Attempt to figure out how wide each line should be. Black magic.
    lineWidth = (imageSize[0] - xBuffer) / (config.fontSize / 2)

    # writeText(draw,
    # 'w: {0}, h: {1}, fw: {2} lw: {3}'.format(imageSize[0], imageSize[1], fontSize, lineWidth), (10, 200))

    font = getFont(config.font, config.fontSize)

    firstLines = textwrap.wrap(first, width=lineWidth)
    secondLines = textwrap.wrap(second, width=lineWidth)

    y_text = 0
    for line in firstLines:
        width, height = font.getsize(line)
        writeSingleLine(draw, font, config, line, (imageSize[0] / 2 - width / 2, y_text))
        y_text += height

    y_text = imageSize[1] - font.getsize(second)[1]
    for line in reversed(secondLines):
        width, height = font.getsize(line)
        writeSingleLine(draw, font, config, line, (imageSize[0] / 2 - width / 2, y_text))
        y_text -= height


# Writes text to the image. Should be updated to wrap long lines, scale down font if necessary, etc.
def writeSingleLine(draw, font, config, line, xy):
    x = xy[0]
    y = xy[1]
    offset = 1

    # border
    draw.text((x - offset, y - offset), line, font=font, fill=config.textStroke)
    draw.text((x + offset, y - offset), line, font=font, fill=config.textStroke)
    draw.text((x - offset, y + offset), line, font=font, fill=config.textStroke)
    draw.text((x + offset, y + offset), line, font=font, fill=config.textStroke)

    # now draw the text over it
    draw.text((x, y), line, font=font, fill=config.textFill)


# Gets the path where the .png image should live
def getImagePath(name):
    module_dir = os.path.dirname(__file__)
    return os.path.join(module_dir, 'images/' + name)


# Gets the default font to use at the specified size
def getFont(name, size):
    module_dir = os.path.dirname(__file__)
    font_path = os.path.join(module_dir, 'fonts/' + name)
    return ImageFont.truetype(font_path, size)


# Sanitizes the text to be written and converts to upper-case
def sanitizeLine(line):
    return line.decode('utf-8').replace('-', ' ').upper()