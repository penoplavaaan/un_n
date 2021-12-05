from django.db import models

class Hashtag(models.Model):
    name = models.CharField(max_length=100)

    def __str__(self):
        return self.name

class Rubric(models.Model):
    name = models.CharField(max_length=100)

    def __str__(self):
        return self.name


class Article(models.Model):
    title = models.CharField(max_length=100)
    keyword = models.CharField(max_length=200)
    annotation = models.TextField()
    rubric = models.ForeignKey(Rubric, null=True, blank=True, on_delete=models.SET_NULL)

    def __str__(self):
        return self.title

class Articlehashtag(models.Model): 
    article_id = models.TextField()
    hashtag_id = models.TextField()
    
    def __str__(self):
        return str(self.id)

#  python3 manage.py runserver
