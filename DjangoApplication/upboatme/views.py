import os
from django.http import HttpResponse
from PIL import Image, ImageFont, ImageDraw


# Draws two lines of text on the specified meme template name and returns it to the user as a PNG image
def make(request, name, first, second):

    image = resolveImage(name)

    draw = ImageDraw.Draw(image)

    writeText(draw, sanitizeLine(first), (20, 20))
    writeText(draw, sanitizeLine(second), (20, 300))

    response = HttpResponse(mimetype="image/png")
    image.save(response, "PNG")

    return response


# Writes text to the image. Should be updated to wrap long lines, scale down font if necessary, etc.
def writeText(draw, text, xy):
    font = getDefaultFont(45)
    shadowColor = 'black'
    fillColor = 'white'
    x = xy[0]
    y = xy[1]
    offset = 1

    # border
    draw.text((x - offset, y - offset), text, font=font, fill=shadowColor)
    draw.text((x + offset, y - offset), text, font=font, fill=shadowColor)
    draw.text((x - offset, y + offset), text, font=font, fill=shadowColor)
    draw.text((x + offset, y + offset), text, font=font, fill=shadowColor)

    # now draw the text over it
    draw.text((x, y), text, font=font, fill=fillColor)


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