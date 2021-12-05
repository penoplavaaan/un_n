from django.urls import path
from . import views

urlpatterns = [  
    path('article/<int:article_id>', views.get_one_article, name='current_article'),
    path('', views.index, name='index'), 
    path('rubric/<int:rubric_id>', views.by_rubric, name='rubric'), 
]
