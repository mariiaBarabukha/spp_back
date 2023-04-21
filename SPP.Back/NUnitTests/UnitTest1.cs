using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace SPP.Core.Tests
{
    public class CalculationServiceTests
    {
        /// <summary>
        /// Назва методу: CalculateAverageDailyUse_Should_Return_Correct_Average()
        ///опис:
        ///Цей метод перевіряє метод CalculateAverageDailyUse() класу CalculationService, щоб переконатися, що він повертає правильне 
        ///середньоденне використання на основі заданого набору записів про замовлення товару.

        ///Параметри:
        ///Цей метод приймає такі параметри:

        ///orders: список об’єктів ItemOrderRecord, що представляють замовлення елементів.
        ///дні: ціле число, що позначає кількість днів, за які потрібно розрахувати середньодобове використання.
        /// Повернене значення:
        /// Цей метод нічого не повертає.

        /// Тестування:
        ///Цей метод перевіряє, чи повертає метод CalculateAverageDailyUse() класу CalculationService правильне середньодобове 
        ///використання, виконавши такі дії:
        ///Створює список об’єктів ItemOrderRecord, що представляють замовлення елементів.
        ///Створення екземпляра об’єкта CalculationService.
        ///Виклик метод CalculateAverageDailyUse() об’єкта CalculationService з параметрами orders і days.

        /// </summary>
        [Test]
        public void CalculateAverageDailyUse_Should_Return_Correct_Average()
        {
            // Arrange
            var orders = new List<ItemOrderRecord>
            {
                new ItemOrderRecord { Quantity = 10 },
                new ItemOrderRecord { Quantity = 5 },
                new ItemOrderRecord { Quantity = 7 },
                new ItemOrderRecord { Quantity = 4 },
                new ItemOrderRecord { Quantity = 8 }
            }.AsQueryable();
            var service = new CalculationService();

            // Act
            var result = service.CalculateAverageDailyUse(orders, 2);

            // Assert
            Assert.AreEqual(2.4M, result, "The average daily use was not calculated correctly.");
        }
        /// <summary>
        /// Перевірка CalculateAverageDailyUse_Should_Return_Zero_For_Negative_Ordered_Amount гарантує, що метод CalculateAverageDailyUse 
        /// класу CalculationService правильно повертає нуль, коли надається список об’єктів ItemOrderRecord із від’ємними значеннями 
        /// Quantity.
        ///Тест спочатку створює список об’єктів ItemOrderRecord із від’ємними значеннями Quantity, а потім створює новий екземпляр 
        ///класу CalculationService.
        ///Далі викликається метод CalculateAverageDailyUse із параметром від’ємних замовлень кількості та додатних днів.
        ///Нарешті, тест стверджує, що повернутий результат дорівнює нулю, що є очікуваним значенням, коли загальна замовлена сума є 
        ///від’ємною.
        ///Якщо перевірка проходить невдало, повертається повідомлення про помилку про те, що середнє щоденне використання має дорівнювати 
        ///нулю для негативної замовленої кількості.
        ///Цей тест допомагає переконатися, що метод CalculateAverageDailyUse може обробляти від’ємну кількість замовлень і 
        ///забезпечує очікуваний вихід, допомагаючи переконатися в правильності методу.
        /// </summary>
        [Test]
        public void CalculateAverageDailyUse_Should_Return_Zero_For_Negative_Ordered_Amount()
        {
            // Arrange
            var orders = new List<ItemOrderRecord>
            {
                new ItemOrderRecord { Quantity = -10 },
                new ItemOrderRecord { Quantity = -5 },
                new ItemOrderRecord { Quantity = -7 },
                new ItemOrderRecord { Quantity = -4 },
                new ItemOrderRecord { Quantity = -8 }
            }.AsQueryable();
            var service = new CalculationService();

            // Act
            var result = service.CalculateAverageDailyUse(orders, 2);

            // Assert
            Assert.AreEqual(0M, result, "The average daily use should be zero for a negative ordered amount.");
        }
        /// <summary>
        /// Документація для public void CalculateAverageDailyUse_Should_Return_Zero_For_Zero_TotalDays()
        ///опис
        /// тесовий метод перевіряє, чи метод CalculateAverageDailyUse повертає нуль, коли параметр загальної кількості днів дорівнює нулю.
        ///Параметри
        ///Цей метод не приймає жодних параметрів.
        ///Повернене значення
        /// Цей метод не повертає жодного значення.
        ///Винятки
        ///Цей метод не створює винятків.
        ///Процедура тестування
        ///Список об’єктів ItemOrderRecord створюється з різними кількостями.
        ///Створено екземпляр класу CalculationService.
        ///Метод CalculateAverageDailyUse екземпляра CalculationService викликається зі списком об’єктів ItemOrderRecord і нулем як 
        ///параметром загальної кількості днів.
        ///Очікуваний результат нульовий.
        ///Фактичний результат порівнюється з очікуваним за допомогою методу Assert.AreEqual.
        /// </summary>
        [Test]
        public void CalculateAverageDailyUse_Should_Return_Zero_For_Zero_TotalDays()
        {
            // Arrange
            var orders = new List<ItemOrderRecord>
            {
                new ItemOrderRecord { Quantity = 10 },
                new ItemOrderRecord { Quantity = 5 },
                new ItemOrderRecord { Quantity = 7 },
                new ItemOrderRecord { Quantity = 4 },
                new ItemOrderRecord { Quantity = 8 }
            }.AsQueryable();
            var service = new CalculationService();

            // Act
            var result = service.CalculateAverageDailyUse(orders, 0);

            // Assert
            Assert.AreEqual(0M, result, "The average daily use should be zero for zero total days.");
        }

        [Test]
        public void CalculateEstimatedQuantityOnHand_Returns_Null_When_Item_Is_Not_Eligible()
        {
            // Arrange
            var newSupplyOrders = new List<ItemOrderRecord>();
            var state = new ItemState { LastQuantityUpdate = DateTime.Now.AddDays(-2) };
            var ADU = 10;

            // Act
            var result = CalculateEstimatedQuantityOnHand(newSupplyOrders.AsQueryable(), state, ADU);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CalculateEstimatedQuantityOnHand_Returns_EstimatedQuantityOnHandDto_Object()
        {
            // Arrange
            var newSupplyOrders = new List<ItemOrderRecord>
        {
            new ItemOrderRecord { Quantity = 10, OrderDate = DateTime.Now.AddDays(-2) },
            new ItemOrderRecord { Quantity = 5, OrderDate = DateTime.Now.AddDays(-1) }
        };
            var state = new ItemState { LastQuantityUpdate = DateTime.Now.AddDays(-3), LastCountQuantity = 20 };
            var ADU = 2;

            // Act
            var result = CalculateEstimatedQuantityOnHand(newSupplyOrders.AsQueryable(), state, ADU);

            // Assert
            Assert.IsInstanceOf<EstimatedQuantityOnHandDto>(result);
        }

        [Test]
        public void CalculateEstimatedQuantityOnHand_Returns_Correct_Estimated_Quantity()
        {
            // Arrange
            var newSupplyOrders = new List<ItemOrderRecord>
        {
            new ItemOrderRecord { Quantity = 10, OrderDate = DateTime.Now.AddDays(-2) },
            new ItemOrderRecord { Quantity = 5, OrderDate = DateTime.Now.AddDays(-1) }
        };
            var state = new ItemState { LastQuantityUpdate = DateTime.Now.AddDays(-3), LastCountQuantity = 20 };
            var ADU = 2;

            // Act
            var result = CalculateEstimatedQuantityOnHand(newSupplyOrders.AsQueryable(), state, ADU);

            // Assert
            Assert.AreEqual(15, result.Quantity);
        }
        [Test]
        public void CalculateFrequency_Returns_Quarterly_When_No_Orders_In_Last_Two_Quarters()
        {
            // Arrange
            var orders = new List<ItemOrderRecord>();
            var hospitalCostsPerDay = new List<decimal> { 10, 15, 20 };
            var inventoryCostPerDay = 5;
            var expirationInDays = 30;

            // Act
            var result = CalculateFrequency(orders.AsQueryable(), hospitalCostsPerDay.AsQueryable(), inventoryCostPerDay, expirationInDays);

            // Assert
            Assert.AreEqual(CalculationSettings.FREQUENCY_QUARTERLY, result);
        }

        [Test]
        public void CalculateFrequency_Returns_Daily_When_Cost_Percentile_Less_Than_20()
        {
            // Arrange
            var orders = new List<ItemOrderRecord>
        {
            new ItemOrderRecord { Quantity = 10, OrderDate = DateTime.Now.AddDays(-2) },
            new ItemOrderRecord { Quantity = 5, OrderDate = DateTime.Now.AddDays(-1) }
        };
            var hospitalCostsPerDay = new List<decimal> { 10, 15, 20 };
            var inventoryCostPerDay = 5;
            var expirationInDays = 30;

            // Act
            var result = CalculateFrequency(orders.AsQueryable(), hospitalCostsPerDay.AsQueryable(), inventoryCostPerDay, expirationInDays);

            // Assert
            Assert.AreEqual(CalculationSettings.FREQUENCY_DAILY, result);
        }

        [Test]
        public void CalculateFrequency_Returns_EOD_When_Cost_Percentile_Less_Than_40()
        {
            // Arrange
            var orders = new List<ItemOrderRecord>
        {
            new ItemOrderRecord { Quantity = 10, OrderDate = DateTime.Now.AddDays(-2) },
            new ItemOrderRecord { Quantity = 5, OrderDate = DateTime.Now.AddDays(-1) }
        };
            var hospitalCostsPerDay = new List<decimal> { 10, 15, 20 };
            var inventoryCostPerDay = 5;
            var expirationInDays = 30;

            // Act
            var result = CalculateFrequency(orders.AsQueryable(), hospitalCostsPerDay.AsQueryable(), inventoryCostPerDay, expirationInDays);

            // Assert
            Assert.AreEqual(CalculationSettings.FREQUENCY_EOD, result);
        }

        [Test]
        public void CalculateFrequency_Returns_Correct_Frequency_When_Cost_Percentile_Greater_Than_40()
        {
            // Arrange
            var orders = new List<ItemOrderRecord>
        {
            new ItemOrderRecord { Quantity = 10, OrderDate = DateTime.Now.AddDays(-2) },
            new ItemOrderRecord { Quantity = 5, OrderDate = DateTime.Now.AddDays(-1) }
        };
            var hospitalCostsPerDay = new List<decimal> { 10, 15, 20 };
            var inventoryCostPerDay = 5;
            var expirationInDays = 30;

            // Act
            var result = CalculateFrequency(orders.AsQueryable(), hospitalCostsPerDay.AsQueryable(), inventoryCostPerDay, expirationInDays);

            // Assert
            Assert.AreEqual(CalculationSettings.FREQUENCY_WEEKLY, result);
        }
    }
}
