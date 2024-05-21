# Car Dealership Application

## Описание

Это приложение для автосалона, позволяющее управлять автомобилями, их конфигурациями и предоставлять возможность оформления покупок. Приложение разработано с использованием WPF для интерфейса пользователя и содержит модуль для логирования действий в системе, также методов, отвечающих за выполнение различных команд.

## Используемые технологии

- ![.NET Core](https://img.shields.io/badge/.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) 
- ![WPF](https://img.shields.io/badge/WPF-0078D7?style=for-the-badge&logo=microsoft&logoColor=white) 
- ![MVVM](https://img.shields.io/badge/MVVM-512BD4?style=for-the-badge&logo=visualstudio&logoColor=white) 
- ![xUnit](https://img.shields.io/badge/xUnit-5D1F92?style=for-the-badge&logo=xunit&logoColor=white) 
- ![XML](https://img.shields.io/badge/XML-FF6600?style=for-the-badge&logo=xml&logoColor=white)
- ![SQL](https://img.shields.io/badge/SQL-4479A1?style=for-the-badge&logo=sql&logoColor=white) 


## Функциональность
![Demo](https://github.com/gjotov/CarApp/blob/master/demonstration.gif)

### Основные функции:
- Просмотр списка автомобилей в наличии и не в наличии.
- Сборка комплектации автомобиля с использованием паттерна "строитель".
- Подсчёт цены автомобиля с учётом скидки.
- Подсчёт скидки от первоначальной цены и итоговой.
- Формирование чека покупки с указанием наименования автосалона, наименования автомобиля и его комплектации, дополнительных услуг и их стоимости, а также общей цены.

## Установка

1. Склонируйте репозиторий на свой локальный компьютер:
    ```bash
    git clone https://github.com/your-repo/CarDealershipApp.git
    ```

2. Откройте решение в Visual Studio.

3. Постройте решение, чтобы убедиться, что все зависимости установлены и проект компилируется без ошибок.

## Использование

### Интерфейс пользователя

Приложение состоит из двух основных окон:
1. **Главное окно**: отображает список автомобилей в наличии и не в наличии.
2. **Окно конфигурации**: позволяет настраивать комплектацию автомобиля, просматривать цену без скидки и с учётом скидки, а также добавлять дополнительные услуги.

### Пример использования

1. Запустите приложение.
2. В главном окне выберите автомобиль из списка.
3. Нажмите кнопку "Выбрать комплектацию" для настройки комплектации автомобиля.
4. В окне конфигурации выберите дополнительные услуги и просмотрите итоговую цену.
5. Нажмите кнопку "Сформировать чек", чтобы создать чек покупки.

## Логирование

Модуль логирования записывает действия в системе в лог-файлы, которые хранятся в папке `Logs` в каталоге приложения. Логи записываются с указанием времени и типа сообщения (информация, ошибка, предупреждение).

### Пример кода для логирования

```csharp
using LoggerLibrary;

// Запись информационного сообщения
Logger.LogInfo("Приложение запущено.");

// Запись сообщения об ошибке
Logger.LogError("Произошла ошибка при открытии окна конфигурации.");

// Запись предупреждающего сообщения
Logger.LogWarning("Не выбран автомобиль для конфигурации.");
```

# Тестирование

Для проекта реализованы юнит-тесты с использованием библиотеки xUnit.

### Запуск тестов
1. Откройте Test Explorer (Тестовый проводник) в Visual Studio.
2. Запустите все тесты, чтобы убедиться в корректности работы приложения.

## Примеры тестов
### Тесты для LoggerLibrary   

```csharp
[Fact]
public void Log_CreatesLogFile()
{
    // Arrange
    string message = "Test log message";

    // Act
    Logger.Log(message);

    // Assert
    string logFilePath = Path.Combine(logDirectory, $"{DateTime.Now:yyyy-MM-dd}.log");
    Assert.True(File.Exists(logFilePath));

    string logContent = File.ReadAllText(logFilePath);
    Assert.Contains(message, logContent);
}
```
### Тесты для CarDealerLibrary
```csharp
[Fact]
public void Build_ReturnsCarWithCorrectProperties()
{
    // Arrange
    var builder = new CarBuilder()
        .SetMake("Toyota")
        .SetModel("Camry")
        .SetPrice(30000)
        .SetDiscount(10)
        .SetProductionDate(new System.DateTime(2023, 1, 1))
        .SetHasDiscount(true)
        .SetBodyType(BodyType.Sedan);

    // Act
    var car = builder.Build();

    // Assert
    Assert.Equal("Toyota", car.Make);
    Assert.Equal("Camry", car.Model);
    Assert.Equal(30000, car.Price);
    Assert.Equal(10, car.Discount);
    Assert.True(car.HasDiscount);
    Assert.Equal(new System.DateTime(2023, 1, 1), car.ProductionDate);
    Assert.Equal(BodyType.Sedan, car.BodyType);
}
```
