from datetime import date
from django.shortcuts import render


def index(request):
    return render(request, 'home.html', {'current_year': date.today().year})


def terms(request):
    return render(request, 'terms.html', {})


def about(request):
    return render(request, 'about.html', {})