import os
from django.http import HttpResponse
from PIL import Image, ImageFont, ImageDraw


def home(request):
    return HttpResponse('<html><body>This be \'upboat.me\'!</body></html>')


def test(request):
    module_dir = os.path.dirname(__file__)  # get current directory
    file_path = os.path.join(module_dir, 'images/all-the-things-template.jpg')
    img = Image.open(file_path)
    response = HttpResponse(mimetype="image/jpeg")
    img.save(response, "JPEG")
    return response


def testText(request):
    module_dir = os.path.dirname(__file__)  # get current directory
    file_path = os.path.join(module_dir, 'images/all-the-things-template.png')
    font_path = os.path.join(module_dir, 'fonts/impact.ttf')

    img = Image.open(file_path)

    draw = ImageDraw.Draw(img)

    # use a truetype font
    font = ImageFont.truetype(font_path, 37)
    draw.text((10, 25), "All the things!", font=font, fill="red")

    response = HttpResponse(mimetype="image/png")
    img.save(response, "PNG")
    return response