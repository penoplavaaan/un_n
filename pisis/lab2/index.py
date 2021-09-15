#Продуктовый магазин. В продуктовом магазине работает терминал, который хранит информацию о продуктах:
#цена, количество, вес, состав (белки, жиры, углеводы) и др.

products = [
    {
        'id': 1,
        'name': 'Морковь',
        'price': 30.5,
        'weight': 15,
        'kbzu': [15, 30, 40, 50],
        'number': 60,
        'discount': False
    },
    {
        'id': 2,
        'name': 'Картофель',
        'price': 14.8,
        'weight': 40,
        'kbzu': [39, 90, 10, 15],
        'number': 97,
        'discount': True
    },
    {
        'id': 3,
        'name': 'Помидор',
        'price': 130,
        'weight': 12,
        'kbzu': [34, 13, 36, 90],
        'number': 120,
        'discount': True
    },
    {
        'id': 4,
        'name': 'Огурец',
        'price': 98,
        'weight': 12,
        'kbzu': [12, 6, 8, 9],
        'number': 240,
        'discount': False
    },
    {
        'id': 5,
        'name': 'Перец болгарский',
        'price': 235,
        'weight': 12,
        'kbzu': [300, 12, 33, 56],
        'number': 86,
        'discount': False
    }
]

def obj_print(obj):
    for obj_memb in obj:
        print(obj_memb)

def add():
    new_product = {}
    new_product_id = input('\nВведите ID: ')
    new_product_name = input('\nВведите название: ')
    new_product_price = input('\nВведите цену: ')
    new_product_weight = input('\nВведите вес: ')
    new_product_k = input('\nВведите калорийность: ')

    new_product = {
        'id': new_product_id,
        'name': new_product_name,
        'price': new_product_price,
        'weight': new_product_weight,
        'kbzu': [15, 30, 40, 50],
        'number': 60,
        'discount': False
    }
    products.append(new_product)

def remove(id_or_name):
    if(type(id_or_name) == int):
        id_del = 0
        for product in products:
            if(product['id'] == id_or_name):
                products.pop(id_del)
                break
            id_del += 1
    elif(type(id_or_name) == str):
        id_del = 0
        for product in products:
            if(product['name'] == id_or_name):
                products.pop(id_del)
                break
            id_del += 1
#remove("Картофель")

def find(id_or_name):
    if(type(id_or_name) == int):
        id_find = 0
        for product in products:
            if(product['id'] == id_or_name):
                print(products[id_find])
                break
            id_find += 1
    elif(type(id_or_name) == str):
        id_find = 0
        for product in products:
            if(product['name'] == id_or_name):
                print(products[id_find])
                break
            id_find += 1
#find("Картофель")

def filter_price(greater_less_equal, value):
    for product in products:
        eval_string = 'product["price"]' + greater_less_equal + str(value)
        if eval(eval_string):
            print(product)

#filter_price('==', 14.8)
#filter_price('>', 100)

def mean_attr(attr):
    quant = len(products)
    sum_attr = 0
    for product in products:
        sum_attr += product[attr] 
    mean = sum_attr/quant
    print(mean)
mean_attr('number')


_isOver = False
while not _isOver:
    print('Что хотите?\n')
    print('1) Добавить товар\n')
    print('2) Удалить товар\n')
    print('3) Найти товар\n')
    print('4) Список всех товаров\n')
    print('5) Фильтр по цене\n')
    print('6) Среднее значение по показателю\n')
    print('0) Выход\n\n')
    choice = input('Ваш выбор: ')
    if choice == '0':
        _isOver = not _isOver
    elif choice == '1':
        new_product = []
        new_product_id = input('\nВведите ID: ')
        new_product_name = input('\nВведите название')
        new_product_ = input('\nВведите цену')









#obj_print(products)