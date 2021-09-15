mesta = [
    [0,1,1,0],
    [1,1,1,1],
    [0,1,1,1],
    [0,0,0,0]
]

_isOver = False
while not _isOver:
    choice = input('Что вы хотите сделать?\n1)Узнать количество мест\n2)Узнать, свободно ли место\n0)Выход\n')
    if choice=='0':
        _isOver = not _isOver
    elif choice == '1':
        count = sum(len(x) for x in mesta)
        count_not_zero = sum(sum(x) for x in mesta)
        free = count - count_not_zero
        print ("Свободных мест: ", free, " шт.")
    elif choice == '2':
        place_num = input('Введите номер места в формате: "РЯД_НОМЕР"')
        
        row = int(place_num.split('_')[0])-1
        col = int(place_num.split('_')[1])-1

        if mesta[row, col] == 0:
            print("МЕСТО ЗАНЯТО")
        else:
            print('МЕСТО НЕ ЗАНЯТО')

