# Онлайн библиотека
Курсова работа за всички четни номера



## Условие
Да се създаде система обслужваща нуждите на електронен магазин за книги. Системата трябва да разполага с две презентационни части. Част едно: уеб услуги (Web API или SOAP). Част две: интуитивен потребителски интерфейс за работа.



## Задължителни параметри на проекта
Базата от данни на проекта трябва да включва следната структура:
- Book
  - Int BookId
  - String Title *
  - DateTime ReleaseDate
  - Int AuthorId *
  - Int GenreId *
  - String Description

- Writer
  - Int WriterId
  - String FirstName *
  - String LastName
  - String UserName *

- Genre
  - Int GenreId
  - String GenreName *

  

## Ограничения
Задължителните полета са обозначени със звезда. Изисквания към базата данни:
* Book.Name – допустимия брой символи е между 1 и 300.
* Book.Discription – допустимия брой символи е между 1 и 500.
* Writer.FirstName – допустимия брой символи е между 1 и 200.
* Writer.LastName – допустимия брой символи е между 1 и 200.
* Genre.GenreName – допустимия брой символи е между 1 и 100.
  

  
## Технологичен стак
- SOAP или WebAPI 2
- C# .Net
- HTML, CSS, JavaScript
