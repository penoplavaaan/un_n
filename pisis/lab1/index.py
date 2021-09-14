_quant_is_correct = False
while not _quant_is_correct:
    try:
        quant = input("Сколько витаминок вы хотите купить? \n")
        quant = int(quant)
    except:
        print('Введено неверное значение!')
    else:
        if(quant > 0):
            _quant_is_correct = not _quant_is_correct
        else:
            print('Вы ввели неверное количество!')
        

_price_is_correct = False
while not _price_is_correct:
    try:
        price = input("По какой цене? \n")
        price = float(price)
    except:
        print('Введено неверное значение!')
    else:
        if(price > 0.0):
            _price_is_correct = not _price_is_correct
        else:
            print('Вы ввели неверную цену!')



if(quant < 5):
    total_price = price * quant
elif(quant >= 5 and quant< 10):
    total_price = price * quant * 0.9
elif(quant>= 10):
    total_price = price * quant - (quant//10 * price)

_soc_card = 'Нет'
_disc_is_correct = False
while not _disc_is_correct:
    disc = input("Есть ли у вас социальная карта? \n")
    if(disc == 'да' or disc == 'yes'):
        total_price = round(total_price*0.9)
        _soc_card = 'Да'
        _disc_is_correct = not _disc_is_correct
    elif(disc == 'no' or disc == 'нет'):
        _disc_is_correct = not _disc_is_correct
    else:
        print('Введено неверное значение!')

total_disc = price * quant - total_price
total_disc_perc = round(total_disc/(price * quant),2)*100
print('# ЧЕК \n# Вам положена скидка в ', total_disc_perc, '%!\n# Соц. карта: ', _soc_card, '\n# Сумма покупки: ', price * quant, ' p.\n# Скидка', total_disc, ' p.\n# Итого', total_price, ' p.')
    