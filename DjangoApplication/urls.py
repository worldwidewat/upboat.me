import re
from django.conf.urls import patterns, url

# Uncomment the next two lines to enable the admin:
# from django.contrib import admin
# admin.autodiscover()

urlpatterns = patterns('',
    # Examples:
    # url(r'^$', 'DjangoApplication.views.home', name='home'),
    # url(r'^DjangoApplication/', include('DjangoApplication.foo.urls')),

    # Uncomment the admin/doc line below to enable admin documentation:
    # url(r'^admin/doc/', include('django.contrib.admindocs.urls')),

    # Uncomment the next line to enable the admin:
    # url(r'^admin/', include(admin.site.urls)),

    url(r'^static/(?P<path>.*)$', 'serve'),

    url(r'^$', 'DjangoApplication.upboatme.home.index', name='home'),

    url(r'^terms[/]$', 'DjangoApplication.upboatme.home.terms', name='terms'),

    url(r'^about[/]*$', 'DjangoApplication.upboatme.home.about', name='about'),

    url(r'^(?P<name>[\w-]+)/(?P<first>[ \.\-\?~!$&\'()*+,;=:@%\w]*)/?(?P<second>[ \.\-\?~!$&\'()*+,;=:@%\w]*)$',
        'DjangoApplication.upboatme.views.make', name='make'),

    # this url dispatch business cannot (by design) match the querystring. So screw it, we'll do it live:
    url(r'^.*$', 'DjangoApplication.upboatme.views.route', name='route')
)

urlpatterns += patterns('django.contrib.staticfiles.views',
    url(r'^static/(?P<path>.*)$', 'serve'),
)