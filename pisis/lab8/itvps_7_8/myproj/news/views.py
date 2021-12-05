from django.shortcuts import render, redirect
from .models import Article, Rubric, Articlehashtag, Hashtag
from django.contrib import messages
from django.views.generic.edit import CreateView
from .forms import ArticleForm, RubricForm, CreateUserForm
from django.contrib.auth.forms import UserCreationForm
import pandas as pd
from django.contrib.auth import authenticate, login, logout
from .classifier import *
import re
import numpy as np
from nltk.corpus import stopwords
from string import punctuation
from nltk.tokenize import word_tokenize
from sklearn.model_selection import train_test_split
from sklearn.feature_extraction.text import CountVectorizer
from sklearn.feature_extraction.text import TfidfTransformer
from sklearn.naive_bayes import BernoulliNB
from nltk.stem.snowball import SnowballStemmer
import pickle


def registerPage(request):
    form = CreateUserForm()
    if request.method == 'POST':
        form = CreateUserForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request, "Вы успешно зарегистрировались!")
            return redirect('login')
        else:
            form = CreateUserForm() 
    context = {'form': form}
    return render(request, 'news/registerPage.html', context)


def loginPage(request):
    if request.method == 'POST':
        username = request.POST.get('username')
        password = request.POST.get('password')

        user = authenticate(request, username=username, password=password)

        if user is not None:
            login(request, user)
            return redirect('index')
        else:
            messages.info(request, "Проверьте правильность ввода значений")
    context = {}
    return render(request, 'news/loginPage.html', context)


def logoutUser(request):
    logout(request)
    return redirect('login')


def article(request):
    rubrics = Rubric.objects.all()
    return render(request, 'news/article.html', context={'rubrics': rubrics})


def index(request):
    articles = Article.objects.all()
    rubrics = Rubric.objects.all()
    hashtags = Hashtag.objects.all()
    articleHashtagRelationObjects = Articlehashtag.objects.all()
     
     
    for article in articles:
        print('New article. Title: '+str(article.title))
        article_hashtags = []

        for articleHashtagRelation in articleHashtagRelationObjects: 
            if (articleHashtagRelation.article_id == article.id):
                for hashtag in hashtags:
                    if articleHashtagRelation.hashtag_id == hashtag.id:
                        article_hashtags.append(hashtag.name)
        
        stringified_keywords = ""
        for keyword in article_hashtags:
            stringified_keywords += "#"+keyword + " "
        article.keyword = stringified_keywords
        article.annotation = article.annotation[:250]+"..."
      
    return render(request, 'news/index.html', context={'articles': articles, 'rubrics': rubrics})


def rubrika(request):
    rubrics = Rubric.objects.all()
    return render(request, 'news/rubrika.html', context={'rubrics': rubrics})


def by_rubric(request, rubric_id):
    articles = Article.objects.filter(rubric=rubric_id)
    rubrics = Rubric.objects.all()
    hashtags = Hashtag.objects.all()
    articleHashtagRelationObjects = Articlehashtag.objects.all()
    current_rubric = Rubric.objects.get(pk=rubric_id)

    for article in articles:
            print('New article. Title: '+str(article.title))
            article_hashtags = []

            for articleHashtagRelation in articleHashtagRelationObjects: 
                if (articleHashtagRelation.article_id == article.id):
                    for hashtag in hashtags:
                        if articleHashtagRelation.hashtag_id == hashtag.id:
                            article_hashtags.append(hashtag.name)
            
            stringified_keywords = ""
            for keyword in article_hashtags:
                stringified_keywords += "#"+keyword + " "
            article.keyword = stringified_keywords
            article.annotation = article.annotation[:250]+"..."

    context = {'articles': articles, 'rubrics': rubrics, 'current_rubric': current_rubric}
    return render(request, 'news/rubrika.html', context)


def get_one_article(request, article_id):
    current_article = Article.objects.get(pk=article_id)
    rubrics = Rubric.objects.all()
    return render(request, 'news/article.html', context={'current_article': current_article, 'rubrics': rubrics, })


def createArticle(request):
    form = ArticleForm
    result = ''
    if request.method == 'POST':
        articleForm = ArticleForm(request.POST)
        if articleForm.is_valid():
            articleForm.save()
            text = classifier([articleForm.cleaned_data.get("annotation")])
            print('-------------------------------------------')

            for i, j in text:
                print('%r => %s' % (i, Rubric.objects.get(pk=j)))
                print("Number=", j)
                result = Rubric.objects.get(pk=j)

            print('-------------------------------------------')

            messages.success(request, "Форма отправлена")
            return render(request, 'news/createArticle.html', {'form': form, 'result': result})

    return render(request, 'news/createArticle.html', {'form': form, 'result': result})


def createRubric(request):
    form = RubricForm
    if request.method == 'POST':
        rubricForm = RubricForm(request.POST)
        if rubricForm.is_valid():
            rubricForm.save()
            messages.success(request, "Форма отправлена")
            return redirect('/news/')

    return render(request, 'news/createRubric.html', {'form': form})


 
