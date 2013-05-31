from django.http import HttpResponse


def home(request):
    return HttpResponse('<html><body>This be \'upboat.me\'!</body></html>')