# Car Dealership Application

## Описание

Это приложение для автосалона, позволяющее управлять автомобилями, их конфигурациями и предоставлять возможность оформления покупок. Приложение разработано с использованием WPF для интерфейса пользователя и содержит модуль для логирования действий в системе, также методов, отвечающих за выполнение различных команд.

## Используемые технологии

- ![.NET Core](https://img.shields.io/badge/.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) 
- ![WPF](https://img.shields.io/badge/WPF-0078D7?style=for-the-badge&logo=microsoft&logoColor=white) 
- ![MVVM](https://img.shields.io/badge/MVVM-512BD4?style=for-the-badge&logo=visualstudio&logoColor=white) 
- ![xUnit](https://img.shields.io/badge/xUnit-7C39C9?style=for-the-badge&logo=xunit&logoColor=white)
- ![XML](https://img.shields.io/badge/XML-FF6600?style=for-the-badge&logo=xml&logoColor=white)
- ![SQLite](https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white)

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
    git clone https://github.com/gjotov/CarApp.git
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

# Внешние модули

Проект использует два внешних модуля для реализации различных методов и логирования. Эти модули находятся в папке `external-modules`.

## Модули

### CarsLibrary

Этот модуль содержит различные методы, используемые в программе для управления автомобилями и их конфигурациями.

### LoggerLibrary

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
![](https://github.com/gjotov/CarApp/blob/master/logs.png)

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
# База данных

В этом проекте в качестве базы данных используется SQLite.

### Схема базы данных
![](https://github.com/gjotov/CarApp/blob/master/Databasescr.png)

### База данных состоит из следующих таблиц:

***Cars***
- `CarId`: уникальный идентификатор автомобиля (первичный ключ).
- `Make`: марка автомобиля.
- `Model`: модель автомобиля.
- `Price`: цена автомобиля.
- `HasDiscount`: флаг, указывающий, есть ли скидка на автомобиль (true/false).
- `Discount`: размер скидки на автомобиль.
- `ProductionDate`: дата производства автомобиля.

***Configurations***
- `ConfigurationId`: уникальный идентификатор конфигурации (первичный ключ).
- `CarId`: идентификатор автомобиля (внешний ключ к таблице "Cars").
- `BodyType`: тип кузова автомобиля (фаэтон, лимузин, кабриолет и т.д.).

***Additional Service***
- ServiceId: уникальный идентификатор дополнительной услуги (первичный ключ).
- ServiceName: название дополнительной услуги.
- ServiceCost: стоимость дополнительной услуги.

***Purchares***
- `PurchaseId`: уникальный идентификатор покупки (первичный ключ).
- `CarId`: идентификатор автомобиля (внешний ключ к таблице "Cars").
- `ConfigurationId`: идентификатор конфигурации автомобиля (внешний ключ к таблице "Configurations").
- `AdditionalServiceIds`: строка с идентификаторами дополнительных услуг, разделенными запятыми.
- `TotalPrice`: общая цена покупки.
  
### Примеры использования
Используйте следующие SQL запросы для вставки данных в таблицы.
### Вставка данных в таблицу `Cars`
```sql
INSERT INTO Cars (Make, Model, Price, HasDiscount, Discount, ProductionDate)
VALUES ('Toyota', 'Camry', 30000, 1, 10, '2023-01-01');
```
### Вставка данных в таблицу `Configurations`
```sql
INSERT INTO Configurations (CarId, BodyType)
VALUES (1, 'Sedan');
```
### Вставка данных в таблицу `AdditionalServices`
```sql
INSERT INTO AdditionalServices (ServiceName, ServiceCost)
VALUES ('Extended Warranty', 1200),
       ('GPS Navigation', 300),
       ('Premium Sound System', 500);
```
### Вставка данных в таблицу `Purchases`
```sql
INSERT INTO Purchases (CarId, ConfigurationId, AdditionalServiceIds, TotalPrice)
VALUES (1, 1, '1,2', 32000);
```
## Интеграция с приложением
В проекте Visual Studio добавьте пакет SQLite через NuGet Package Manager
```bash
Install-Package System.Data.SQLite
```
### Пример использования SQLite в C#
```csharp
using System;
using System.Data.SQLite;

namespace CarApp
{
    public class DatabaseHelper
    {
        private const string ConnectionString = "Data Source=AutoDealerDB.db;Version=3;";

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string createCarsTableQuery = @"
                CREATE TABLE IF NOT EXISTS Cars (
                    CarId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Make TEXT NOT NULL,
                    Model TEXT NOT NULL,
                    Price REAL NOT NULL,
                    HasDiscount BOOLEAN NOT NULL,
                    Discount REAL NOT NULL,
                    ProductionDate TEXT NOT NULL
                );";
                ExecuteNonQuery(createCarsTableQuery, connection);

                string createConfigurationsTableQuery = @"
                CREATE TABLE IF NOT EXISTS Configurations (
                    ConfigurationId INTEGER PRIMARY KEY AUTOINCREMENT,
                    CarId INTEGER NOT NULL,
                    BodyType TEXT NOT NULL,
                    FOREIGN KEY (CarId) REFERENCES Cars (CarId)
                );";
                ExecuteNonQuery(createConfigurationsTableQuery, connection);

                string createAdditionalServicesTableQuery = @"
                CREATE TABLE IF NOT EXISTS AdditionalServices (
                    ServiceId INTEGER PRIMARY KEY AUTOINCREMENT,
                    ServiceName TEXT NOT NULL,
                    ServiceCost REAL NOT NULL
                );";
                ExecuteNonQuery(createAdditionalServicesTableQuery, connection);

                string createPurchasesTableQuery = @"
                CREATE TABLE IF NOT EXISTS Purchases (
                    PurchaseId INTEGER PRIMARY KEY AUTOINCREMENT,
                    CarId INTEGER NOT NULL,
                    ConfigurationId INTEGER NOT NULL,
                    AdditionalServiceIds TEXT NOT NULL,
                    TotalPrice REAL NOT NULL,
                    FOREIGN KEY (CarId) REFERENCES Cars (CarId),
                    FOREIGN KEY (ConfigurationId) REFERENCES Configurations (ConfigurationId)
                );";
                ExecuteNonQuery(createPurchasesTableQuery, connection);
            }
        }

        private static void ExecuteNonQuery(string query, SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public static void InsertCar(string make, string model, double price, bool hasDiscount, double discount, string productionDate)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Cars (Make, Model, Price, HasDiscount, Discount, ProductionDate) VALUES (@Make, @Model, @Price, @HasDiscount, @Discount, @ProductionDate)";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Make", make);
                    command.Parameters.AddWithValue("@Model", model);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@HasDiscount", hasDiscount);
                    command.Parameters.AddWithValue("@Discount", discount);
                    command.Parameters.AddWithValue("@ProductionDate", productionDate);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void InsertConfiguration(int carId, string bodyType)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO Configurations (CarId, BodyType) VALUES (@CarId, @BodyType)";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CarId", carId);
                    command.Parameters.AddWithValue("@BodyType", bodyType);
                command.ExecuteNonQuery();
            }
        }
    }

    public static void InsertAdditionalService(string serviceName, double serviceCost)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string query = "INSERT INTO AdditionalServices (ServiceName, ServiceCost) VALUES (@ServiceName, @ServiceCost)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ServiceName", serviceName);
                command.Parameters.AddWithValue("@ServiceCost", serviceCost);

                command.ExecuteNonQuery();
            }
        }
    }

    public static void InsertPurchase(int carId, int configurationId, string additionalServiceIds, double totalPrice)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();

            string query = "INSERT INTO Purchases (CarId, ConfigurationId, AdditionalServiceIds, TotalPrice) VALUES (@CarId, @ConfigurationId, @AdditionalServiceIds, @TotalPrice)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CarId", carId);
                command.Parameters.AddWithValue("@ConfigurationId", configurationId);
                command.Parameters.AddWithValue("@AdditionalServiceIds", additionalServiceIds);
                command.Parameters.AddWithValue("@TotalPrice", totalPrice);

                command.ExecuteNonQuery();
            }
        }
    }
}
```


