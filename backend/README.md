## Розгортання локального середовища

Рекомендується дотримуватися інструкції з розгортання всіх вузлів системи в `src\docs\compose-readme.md`.
Після успішного виконання ви отримаєте контейнери з базою даних (БД), API сервісом і веб-застосунками. API з контейнера не конфліктуватиме з локальним запуском API
з Visual Studio, тому що вони працюватимуть на різних портах. За потреби контейнер з API можна зупинити.

Якщо виникне потреба локально запустити тільки базу даних без використання `docker compose` з усіма вузлами, слід у консолі (Windows) виконати команду:
```
docker run -d --name itm-postgres-db -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=secret -e POSTGRES_DB=itm -p 5432:5432 postgres:16
```

У результаті у вас запуститься контейнер `itm-postgres-db` з базою даних `itm`, яка буде доступна на порту `5432` для користувача `admin` з паролем `secret`.
Саме ці параметри застосовуються в проєкті `ApiService` за замовчуванням.
За потреби змінити ці параметри, слід відредагувати файл `appsettings.json` в проєкті `ApiService` ([див. Локальний запуск WEB API п.3](#локальний-запуск-web-api)).

Для доступу до бази даних можна скористатися будь-якою сумісною клієнтською програмою, такою як DBeaver -
 це безкоштовний інструмент для роботи з різними СУБД.

## Інструкція з налаштування DBeaver

### Крок 1: Встановлення DBeaver
1. Завантажте з [офіційного сайту DBeaver](https://dbeaver.io/download/).
2. Встановіть DBeaver згідно з інструкціями для вашої операційної системи.

### Крок 2: Створення нового з'єднання
1. Запустіть DBeaver.
2. Перейдіть до `Database > New Database Connection`.
3. Виберіть PostgreSQL зі списку доступних баз даних.
4. Якщо виникне запит на завантаження драйверів PostgreSQL, то підтвердіть завантаження.

### Крок 3: Конфігурація з'єднання
1. У полі `Host` введіть `localhost`.
2. У полі `Port` введіть порт (`5432` за замовчуванням).
3. У полі `Database` введіть назву бази даних (`itm` за замовчуванням).
4. У полі `User name` введіть ім'я користувача (`admin` за замовчуванням).
5. У полі `Password` введіть пароль (`secret` за замовчуванням).

### Крок 4: Налаштування додаткових параметрів
1. Перейдіть до вкладки `Advanced`.
2. Увімкніть параметр `Replace legacy timezone`.

### Крок 5: Тестування з'єднання
1. Натисніть `Test Connection`, щоб перевірити, чи можна успішно підключитися до бази даних.
2. Якщо з'єднання успішне, натисніть `Finish`.


## Локальний запуск WEB API

Для розробки використовувалась [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/vs/community/).
Можливе використання інших IDE, які підтримують .Net 9.

1. Відкрийте файл рішення `ApiService\ApiService.sln`.
2. Зробіть проєкт `Api` стартовим.
3. Переконатися, що у файлі `appsettings.json` проєкту `Api` вказані правильні параметри підключення до бази даних.
За замовчуванням це `Host=localhost;Port=5432;Database=itm;Username=admin;Password=secret`
4. Запустіть проєкт. Якщо все виконано успішно, у браузері автоматично відкриється сторінка [Swagger](https://localhost:60961/swagger/index.html).
5. Тепер можна скористатися будь-якими ендпоінтами через Swagger.

## Створення кімнати через API
На сторінці Swagger виконайте `POST /api/rooms` з наступним Request body:
```
{
  "room": {
    "name": "Epam Celebration Room",
    "description": "20th anniversary of Epam Ukraine",
    "giftExchangeDate": "2025-11-28",
    "giftMaximumBudget": 1000
  },
  "adminUser": {
    "firstName": "Admin",
    "lastName": "User",
    "phone": "+380123456789",
    "email": "admin@mail.com",
    "deliveryInfo": "Delivery address",
    "wantSurprise": true,
    "interests": "Music, Sports"
  }
}
```
Успішна відповідь повинна виглядати подібно до:
```
{
  "room": {
    "id": 2,
    "createdOn": "2025-10-25T18:35:09.3661991Z",
    "modifiedOn": "2025-10-25T18:35:09.3661991Z",
    "adminId": 2,
    "invitationCode": "7088f82f895a4954a584b4b2438dd69f",
    "invitationNote": "Hey!\n\nJoin our Secret Nick and make this holiday season magical! 🎄\n\nYou‘ll get to surprise someone with a gift — and receive one too. 🎅✨\n\nLet the holiday fun begin! 🌟\n\n🎁 Join here:",
    "isFull": false,
    "name": "Epam Celebration Room",
    "description": "20th anniversary of Epam Ukraine",
    "giftExchangeDate": "28/11/2025 00:00:00",
    "giftMaximumBudget": 1000
  },
  "userCode": "e25aae2458564b348f9cc8428c517789"
}
```

## Додавання користувача в кімнату
Щоб додати користувача в кімнату потрібен `invitationCode` кімнати, який надається у відповіді створення кімнати.
Цей код слід застосувати в параметрі `roomCode` наступного запиту:
`POST api/users?roomCode=7088f82f895a4954a584b4b2438dd69f`
```
{
  "firstName": "Vasya",
  "lastName": "Pupkin",
  "phone": "+380123456789",
  "email": "vp@mail.com",
  "deliveryInfo": "Delivery address",
  "wantSurprise": true,
  "interests": "Games, Movies"
}
```
Успішна відповідь повинна виглядати подібно до:
```
{
  "id": 4,
  "createdOn": "2025-10-25T18:52:47.108197Z",
  "modifiedOn": "2025-10-25T18:52:47.108197Z",
  "roomId": 2,
  "isAdmin": false,
  "firstName": "Vasya",
  "lastName": "Pupkin",
  "userCode": "161bb367b76d465c8e99f5775c64543a",
  "phone": "+380123456789",
  "email": "vp@mail.com",
  "deliveryInfo": "Delivery address",
  "wantSurprise": true,
  "interests": "Games, Movies",
  "wishList": []
}
```

## Читання користувачів кімнати
Щоб прочитати користувачів кімнати потрібен `userCode` користувача, який приходить у відповіді створення користувача.
Цей код слід застосувати в наступному запиті:

`GET api/users?userCode=e25aae2458564b348f9cc8428c517789`

Успішна відповідь повинна виглядати подібно до:
```
[
  {
    "id": 2,
    "createdOn": "2025-10-25T18:35:09.365061Z",
    "modifiedOn": "2025-10-25T18:52:47.189684Z",
    "roomId": 2,
    "isAdmin": true,
    "firstName": "Admin",
    "lastName": "User",
    "userCode": "e25aae2458564b348f9cc8428c517789",
    "phone": "+380123456789",
    "email": "admin@mail.com",
    "deliveryInfo": "Delivery address",
    "wantSurprise": true,
    "interests": "Music, Sports",
    "wishList": []
  },
  {
    "id": 4,
    "createdOn": "2025-10-25T18:52:47.108197Z",
    "modifiedOn": "2025-10-25T18:52:47.108197Z",
    "roomId": 2,
    "isAdmin": false,
    "firstName": "Vasya",
    "lastName": "Pupkin",
    "userCode": "161bb367b76d465c8e99f5775c64543a",
    "phone": "+380123456789",
    "email": "vp@mail.com",
    "deliveryInfo": "Delivery address"
  }
]
```

## Домашнє завдання
**1й рівень складності**

Реалізувати базову функціональність API для видалення користувача
```
DELETE /users/[id]?userCode=[code]​
```
де:
* id - ідентифікатор користувача, якого треба видалити​
* userCode - унікальний код адміністратора кімнати

**2й рівень складності**

Додати валідації і відповідні HTTP відповіді на випадки, коли:​
* Користувача з `id` не знайдено​
* Користувача з `userCode` не знайдено​
* Користувач з `userCode` не адміністратор​
* Користувач з `userCode` і `id` належать до різних кімнат​
* Користувач з `userCode` і `id` це один і той самий користувач​
* Кімната вже закрита

**3й рівень складності**

Реалізувати тести на всі позитивні і негативні сценарії.