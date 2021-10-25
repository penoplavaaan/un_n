from os import name
import numpy as np
import pandas as pd

names = ["Kids_Number","Gluc_Conc","Pressure","Insul_Amount","BMI","Diabetes_Pedegree","Age","Is_Diabet"]
df = pd.read_csv('prima-indians-diabetes.csv', names=names)

#df.head(20).to_csv('1.csv')
#print(df.head)

#1.	Максимальный и средний возраст пациентов с установленным диабетом.
max_is_diabet = str(df[df['Is_Diabet'] == 1]['Age'].max())
mean_is_diabet = str(df[df['Is_Diabet'] == 1]['Age'].mean())
print('Средний возраст: ' + mean_is_diabet, 'лет, максимальный возраст: ' + max_is_diabet + ' лет')


#2.	Параметры с максимальной корреляцией между собой, значение корреляции.
correlation_df = df.corr(method='pearson')

max_corr_arr = []
for index, row in correlation_df.iterrows():
    max_corr_arr.append(row.nlargest(2).iloc[1])
max_corr_val = max(max_corr_arr)

max_corr_names = []
for name in names:
    new_correlation_df = (correlation_df[name] == max_corr_val)
    for i in range(0, len(names)):
        is_true = new_correlation_df.iloc[i]
        if(is_true):
            max_corr_names.append(new_correlation_df.to_frame().columns[0])

print('Максимальная корреляция между ' + str(max_corr_names[0]) + ' и ' + str(max_corr_names[1] )+ ', коэффициент равен: '+ str(max_corr_val))








