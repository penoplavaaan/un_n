from django.shortcuts import render


# Create your views here.

def article(request):
    return render(request, 'news/article.html', {})
