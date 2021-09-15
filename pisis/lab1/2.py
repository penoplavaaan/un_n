_isOver = False
namesArr = []

while not _isOver:
    newName = input('Введите нового человека в формате "Фамилия Имя Отчество Пол Стаж" или введите "0"')
    if(newName) == '0':
        _isOver = not _isOver
    else:
        newNameArr = newName.split(' ')
        newNameArr[4] = int(newNameArr[4])
        namesArr.append(newNameArr)

namesArr = sorted(namesArr, key=lambda namesArr: namesArr[4])
print('Самый "молодой":', namesArr[0], 'Самый "молодой":', namesArr[1])

sotr_male = []
sotr_female = []

