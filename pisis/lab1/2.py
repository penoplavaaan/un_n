_isOver = False
namesArr = []

while not _isOver:
    newName = input('Введите нового человека в формате "Фамилия Имя Отчество Пол Стаж" или введите "0": ')
    if(newName) == '0':
        _isOver = not _isOver
    else:
        newNameArr = newName.split(' ')
        newNameArr[4] = int(newNameArr[4])
        namesArr.append(newNameArr)

namesArr = sorted(namesArr, key=lambda namesArr: namesArr[4])
print('Самый "молодой":', namesArr[0], 'Самый "старый":', namesArr[len(namesArr)-1])

sotr_male = [[0]]
sotr_female = [[0]]

start_letter = input('Введите волшебную букву: ')

for sotr in namesArr:
    if sotr[3] == 'М':
        sotr_male.append(sotr)
        if(sotr[1][0] == start_letter): sotr_male[0][0] +=1
    else:
        sotr_female.append(sotr)
        if(sotr[1][0] == start_letter): sotr_female[0][0] +=1

if(sotr_male[0][0] > sotr_female[0][0]): print('Мужиков больше')
elif(sotr_male[0][0] > sotr_female[0][0]): print('Женщин больше')
else: print('Поровну')