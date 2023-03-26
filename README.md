# SenseCapitalTraineeTask

### 1) Клонировать проект https://github.com/shnircourier/SenseCapitalTraineeTask.git
### 2) Выбрать ветку master
### 3) Выполнить восстановление submodule'ей гита, командой git submodule update
### 4) Выполнить восстановление NuGet пакетов
### 5) Запустить docker compose командой  docker compose -f .\docker-compose.yml -f .\docker-compose.override.yml up или в IDE Visual Studio 2022
### 6) Перейти на по ссылке http://localhost:29000/swagger/index.html  (Если не меняли файлы env)

### Мероприятия где цена билетов = 0 считаются бесплатными и обращение к сервису оплаты не происходит

### в докере взята версия rmq с интерфейсом (http://localhost:15672 | Пароль mupassword | Логин myuser)
#### Примечание: при запуске через VisualStudio есть шанс что rabbitmq запуститься позже основного(main) проекта и тогда нужно будет нажать перезапуск
#### Примечание: если использовать команду docker compose up тогда все запуститься нормально

#### Для вызова события удаления картинки можно перейти по адресу http://localhost:26000/images/test/{id картинки}
##### Доступные Id: 641d80a8fe5581c7b7f7f304 641d80a8fe5581c7b7f7f303 641d80a8fe5581c7b7f7f302


#### Для вызова события удаления помещения можно перейти по адресу http://localhost:25000/rooms/test/{id помещения}
##### Доступные Id: 641d8164fe5581c7b7f7f307 641d8164fe5581c7b7f7f306 641d8164fe5581c7b7f7f305