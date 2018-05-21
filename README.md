# Education-TRPO
6 семестр. 
## Задания к лабораторным работам 
За задания выставляются оценки по шкале от 0 до 10 баллов. 
В конце курса, необходимо составить общий отчет по комплексу лабораторных работ и сдать в печатном виде. В общий отчет включается описание заданий, что конкретно реализовано, описание реализации, скриншот с исполняемым приложением (без разбиения на отдельные задания), отрывки основного кода (служебные части кода допускается сокращать). 
## Задание №1 
### Часть №1
Реализовать программу, которая считывает дату в формате ISO: “YYYY-MM-DDTHH:mm:SSZ", например:2018-01-12T12:12:43Z 2018-01-12T12:12:43+03:00. 
Вывести следующие даты:
* Начало следующего дня
* Начало следующего месяца
* Начало недели (в которую входит указанный день)
* Конец недели (в которую входит указанный день).
### Часть №2 
Написать программу которая вводит строку с клавиатуры и проверяет с помощью регулярных выражений соответствует ли данная строка формату:
* Даты
* Даты + времени
* E-Mail 
## Задание №2
По указанному [URL](http://www.neracoos.org/erddap/tabledap/E05_aanderaa_all.json?station,mooring_site_desc,water_depth,time,current_speed,current_speed_qc,current_direction,current_direction_qc,current_u,current_u_qc,current_v,current_v_qc,temperature,temperature_qc,conductivity,conductivity_qc,salinity,salinity_qc,sigma_t,sigma_t_qc,time_created,time_modified,longitude,latitude,depth&time%3E=2015-08-25T15:00:00Z&time%3C=2016-12-05T14:00:00Z) можно получить данные датчиков станции слежения за течением в Атлантическом океане. 
Реализовать программу производящую следующие действия:
1. Получить данные и разобрать с использованием стандартных библиотек.
2. Подсчитать минимальное, максимальное и среднее значение для параметров: current_speed, temperature, salinity. Если соответствующее значение поля с постфиксом _qc (например, для current_speed — current_speed_qc) не равно 0, то это означает, что произошел сбой при считывании данных с датчика и это значение не надо учитывать. Данные значения посчитать за один проход массива записей.
3. Проектировать с учетом возможности расширения числа параметров.
4. Вывести в stdout в формате json:
```json
{ 
  "current_speed": { 
    "start_date": "2015-08-15",
    "end_date": "2016-12-05",
    "num_records": 10000,
    "min_current_speed": 0.0,
    "min_time": "2015-12-10T10:40:00Z",
    "max_current_speed": 32.81,
    "max_time": "2016-04-02T21:10:00Z",
    "avg_current_speed": 22.45     
    },
    ... 
}
```
Где для каждого из трех параметров выводятся следующие значения: 
- start_date — начальная дата 
- end_date — конечная дата num_records — число записей 
- min_COLUMN — минимальное значение 
- min_time — время в которое достигнуто минимальное значение 
- max_COLUMN — максимальное значение 
- max_time — время в которое достигнуто максимальное значение 
- avg_COLUMN — среднее значение 
- COLUMN — имя параметра 

## Задание №3
Написать HTTP-сервис, который осуществляет CRUD-интерфейс к некоторому набору данных. 
В качестве примера, можно взять список пользователей или заметки. 
Должны быть определены следующие операции: 
- Получение списка записей
- Получение деталей одной записи 
- Создание записи
- Изменение записи
- Удаление записи 
## Задание №4 
Написать HTTP-сервис, который будет периодически запрашивать API данные по нескольким криптовалютам (можно использовать [вот этот сайт](https://min-api.cryptocompare.com/) и отправлять сообщения об изменениях с помощью websockets, либо с помощью http long polling. 
При подключении к серверу должны быть отправлены текущие значения данных. 
В качестве интерфейса можно использовать браузерные расширения для работы с websockets. 
 
 # Задание для экзамена
 [Full problem description](https://www.hackerrank.com/challenges/connected-cell-in-a-grid/problem)
 
 **Task** 

Given an `n*m` matrix, find and print the number of cells in the largest region in the matrix. Note that there may be more than one region in the matrix.

**Input Format**

The first line contains an integer `n`, the number of rows in the matrix. 
The second line contains an integer `m`, the number of columns in the matrix. 
Each of the next `n`lines contains `m` space-separated integers matrix[i][j].
