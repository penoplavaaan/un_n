# Generated by Django 3.1.1 on 2021-06-04 03:10

from django.db import migrations, models
import django.db.models.deletion


class Migration(migrations.Migration):

    dependencies = [
        ('news', '0004_remove_article_rubnum'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='article',
            name='rubric',
        ),
        migrations.AddField(
            model_name='rubric',
            name='article',
            field=models.ForeignKey(null=True, on_delete=django.db.models.deletion.SET_NULL, to='news.article'),
        ),
    ]
