import os
from django.http import HttpResponse
from PIL import Image


def home(request):
    return HttpResponse('<html><body>This be \'upboat.me\'!</body></html>')


def test(request):
    module_dir = os.path.dirname(__file__)  # get current directory
    file_path = os.path.join(module_dir, 'images/all-the-things-template.jpg')
    img = Image.open(file_path)
    response = HttpResponse(mimetype="image/jpeg")
    img.save(response, "JPEG")
    return response