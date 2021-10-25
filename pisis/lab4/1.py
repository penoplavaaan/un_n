def get_correct_form(rouble):
    rouble = str(rouble)
    if(rouble[len(rouble)-1] == '1' and rouble[len(rouble)-2] != '1'):
        return rouble + ' рубль'
    elif(rouble[len(rouble)-1] in ['2', '3', '4'] and rouble[len(rouble)-2] != '1'):
        return rouble + ' рубля'
    else:
        return rouble + ' рублей'

for i in range(0, 1002):
    print(get_correct_form(i))