from django.conf.urls import patterns, include, url

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

    url(r'^Home/$', 'DjangoApplication.upboatme.views.home', name='home'),
    url(r'^Test/$', 'DjangoApplication.upboatme.views.test', name='test'),
    url(r'^TestText/$', 'DjangoApplication.upboatme.views.testText', name='testText'),
    url(r'^TestParams/(?P<name>[\w-]+)/(?P<first>[\.\-~!$&\'()*+,;=:@\w]+)/(?P<second>[\.\-~!$&\'()*+,;=:@\w]+)$',
        'DjangoApplication.upboatme.views.testParams', name='testParams'),
    url(r'^(?P<name>[\w-]+)/(?P<first>[\.\-~!$&\'()*+,;=:@%\w]+)/(?P<second>[\.\-~!$&\'()*+,;=:@%\w]+)$',
        'DjangoApplication.upboatme.views.make', name='make')
)
