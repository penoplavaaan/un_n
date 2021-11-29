from os import name
import numpy as np
import pandas as pd

names = ["Kids_Number","Gluc_Conc","Pressure","k_s","Insul_Amount","BMI","Diabetes_Pedegree","Age","Is_Diabet"]
df = pd.read_csv('prima-indians-diabetes.csv', names=names)

print('\n____________________________\n')
#1.	Максимальный и средний возраст пациентов с установленным диабетом.
max_is_diabet = str(df[df['Is_Diabet'] == 1]['Age'].max())
mean_is_diabet = str(df[df['Is_Diabet'] == 1]['Age'].mean())
print('Средний возраст пациентов с установленным диабетом: ' + mean_is_diabet, 'лет, максимальный возраст: ' + max_is_diabet + ' лет')


print('\n____________________________\n')
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


print('\n____________________________\n')
#3.	Доля бездетных среди пациентов с неустановленным диабетом.
df_no_diabet = df[df['Is_Diabet'] == 0]
no_diabet_count = df_no_diabet.shape[0]

no_diabet_no_kids_df = df_no_diabet[df_no_diabet['Kids_Number']==0]
no_diabet_no_kids_count = no_diabet_no_kids_df.shape[0]

no_kids_no_diabets_perc = no_diabet_no_kids_count/no_diabet_count*100

print('Всего без диабета: '+str(no_diabet_count)+", в том числе бездетных: "+str(no_diabet_no_kids_count)+', что составляет: '+str(no_kids_no_diabets_perc)+'%')


print('\n____________________________\n')
#4.	Максимальная концентрация глюкозы у пациентов старше 50 лет.
max_gluc_age_greater_then_50 = (df[df['Age'] > 50]['Gluc_Conc']).max() 
print('Максимальная концентрация глюкозы у пациентов старше 50 лет равна: '+str(max_gluc_age_greater_then_50))



print('\n____________________________\n')
#5.	Средний возраст пациентов с диастолическим давлением выше 80.
mean_age_press_greater_then_80 = df[df['Pressure'] > 80]['Age'].mean()
print('Средний возраст пациентов с диастолическим давлением выше 80 равен: ' + str(mean_age_press_greater_then_80))

print('\n____________________________\n')
#6.	Список пациентов старше 60 с уровнем инсулина выше среднего, отсортированный по возрастанию столбца Возраст.
mean_insul = df['Insul_Amount'].mean()
age_greater_then_60_df = df[df['Age']>60].sort_values(by=['Age'])
age_greater_then_60_insul_greater_then_mean_df = age_greater_then_60_df[age_greater_then_60_df['Insul_Amount'] > mean_insul]
print('Список пациентов старше 60 с уровнем инсулина выше среднего, отсортированный по возрастанию столбца Возраст:\n')
print(age_greater_then_60_insul_greater_then_mean_df)

print('\n____________________________\n')
#7.	Список записей с нулевыми значениями хотя бы одного параметра (за исключением первого и последнего столбцов).
new_names = names[1:-1:]
print(new_names)
new_df = pd.DataFrame() 

for index, row in df.iterrows(): 
    curr_row = row[new_names]
    for name in new_names:
        if(curr_row[name]==0):
            new_df = new_df.append(curr_row)
            continue
new_df = new_df.drop_duplicates()
print('Список записей с нулевыми значениями хотя бы одного параметра:\n')
print(new_df)