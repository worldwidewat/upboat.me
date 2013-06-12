import os
import textwrap
from django.http import HttpResponse
from PIL import Image, ImageFont, ImageDraw


# Draws two lines of text on the specified meme template name and returns it to the user as a PNG image
def make(request, name, first, second):

    image = resolveImage(name)

    writeText(image, sanitizeLine(first), sanitizeLine(second))

    response = HttpResponse(mimetype="image/png")
    image.save(response, "PNG")

    return response


# Write the first and second line of text to the image
def writeText(image, first, second):

    draw = ImageDraw.Draw(image)
    imageSize = image.size
    fontSize = 40
    xBuffer = 20

    # Attempt to figure out how wide each line should be. Black magic.
    lineWidth = imageSize[0] / (fontSize / 2)

    # writeText(draw,
    # 'w: {0}, h: {1}, fw: {2} lw: {3}'.format(imageSize[0], imageSize[1], fontSize, lineWidth), (10, 200))

    font = getDefaultFont(fontSize)

    firstLines = textwrap.wrap(first, width=lineWidth)
    secondLines = textwrap.wrap(second, width=lineWidth)

    y_text = 0
    for line in firstLines:
        width, height = font.getsize(line)
        writeSingleLine(draw, font, line, (xBuffer, y_text))
        y_text += height

    y_text = imageSize[1] - font.getsize(second)[1]
    for line in reversed(secondLines):
        width, height = font.getsize(line)
        writeSingleLine(draw, font, line, (xBuffer, y_text))
        y_text -= height


# Writes text to the image. Should be updated to wrap long lines, scale down font if necessary, etc.
def writeSingleLine(draw, font, line, xy):
    shadowColor = 'black'
    fillColor = 'white'
    x = xy[0]
    y = xy[1]
    offset = 1

    # border
    draw.text((x - offset, y - offset), line, font=font, fill=shadowColor)
    draw.text((x + offset, y - offset), line, font=font, fill=shadowColor)
    draw.text((x - offset, y + offset), line, font=font, fill=shadowColor)
    draw.text((x + offset, y + offset), line, font=font, fill=shadowColor)

    # now draw the text over it
    draw.text((x, y), line, font=font, fill=fillColor)


# Returns the image object if the the image can be found (otherwise None)
def resolveImage(name):
    imagePath = getImagePath(name)
    if not os.path.exists(imagePath):
        return None
    return Image.open(imagePath)


# Gets the path where the .png image SHOULD live
def getImagePath(name):
    module_dir = os.path.dirname(__file__)
    return os.path.join(module_dir, 'images/' + name + '-template.png')


# Gets the default font to use at the specified size
def getDefaultFont(size):
    module_dir = os.path.dirname(__file__)
    font_path = os.path.join(module_dir, 'fonts/impact.ttf')
    return ImageFont.truetype(font_path, size)


# Sanitizes the text to be written and converts to upper-case
def sanitizeLine(line):
    return line.decode('utf-8').replace('-', ' ').upper()