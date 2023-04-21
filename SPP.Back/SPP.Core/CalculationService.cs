using SPP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Клас CalculationService відповідає за обчислення різних показників, пов’язаних з управлінням запасами.
///Метод CalculateAverageDailyUse обчислює середнє щоденне використання продукту протягом певної кількості тижнів. Метод приймає 
///IQueryable<ItemOrderRecord>, що представляє історію замовлень товару, і ціле число тижнів, що представляє кількість тижнів, 
///які потрібно включити в обчислення. Метод спочатку обчислює загальну кількість днів, охоплених зазначеною кількістю тижнів. 
///Потім він запитує параметр orders, щоб отримати всі замовлення, розміщені протягом зазначеної кількості днів. Потім метод обчислює 
///загальну кількість товару, замовленого протягом цього періоду, і ділить її на загальну кількість днів, щоб отримати середньодобове
///використання. Якщо загальна замовлена кількість від’ємна, метод встановлює її на нуль.
///Метод CalculateEstimatedQuantityOnHand обчислює приблизну кількість товару в наявності на певний момент часу. Метод приймає 
///IQueryable<ItemOrderRecord>, що представляє нові замовлення на постачання, отримані після останнього оновлення кількості, об’єкт 
///ItemState, що представляє поточний стан товару, і десяткову дробу, що представляє середнє щоденне використання товару. Метод спочатку 
///перевіряє, чи придатний елемент для оцінки, викликаючи приватний метод IsEligibleForEstimate. Якщо елемент не відповідає вимогам, 
///метод повертає значення null. В іншому випадку метод ініціалізує новий об’єкт EstimatedQuantityOnHandDto та запитує параметр 
///newSupplyOrders, щоб отримати всі замовлення, отримані після останнього оновлення кількості. Потім метод обчислює приблизну наявну 
///кількість за допомогою формули LastCountQuantity - ADU * DaysSinceQuantityUpdate + NewSupplyQuantity, де LastCountQuantity – 
///це остання відома наявна кількість, ADU – середньодобове використання товару, а DaysSinceQuantityUpdate – кількість днів, що минули. 
///з моменту останнього оновлення кількості. Якщо обчислена величина негативна, метод встановлює її на нуль.
///Метод IsEligibleForEstimate перевіряє, чи придатний товар для розрахунку приблизної кількості. Метод приймає об’єкт ItemState, що 
///представляє поточний стан елемента. Метод повертає значення true, якщо товар має останню відому кількість у наявності, дату останнього 
///оновлення кількості, непорожню частоту та код останнього циклу підрахунку, який відрізняється від поточного коду циклу підрахунку.
///Метод CalculateFrequency обчислює рекомендовану частоту підрахунку запасів товару. Метод приймає IQueryable<ItemOrderRecord>, 
///що представляє історію замовлень товару, IQueryable<decimal>, що представляє витрати на день подібних предметів в інших лікарнях, 
///десяткове значення, що представляє вартість інвентаризації товару на день, і ціле число, що представляє кількість днів до завершення 
///терміну дії елемента. Метод спочатку перевіряє, чи були замовлення за останні 26 тижнів. Якщо ні, метод повертає «Щоквартально». 
///В іншому випадку метод обчислює середнє щоденне використання товару за останні 26 тижнів за допомогою методу CalculateAverageDailyUse. 
///Якщо середнє щоденне використання менше 0,001, метод перераховує його, використовуючи останні 52 тижні історії замовлень. Потім метод 
///обчислює процентиль вартості запасів на день відносно витрат на день подібних товарів в інших лікарнях за допомогою методу 
///GetCostPercentileInHospital. Якщо процентиль менший за 20, метод повертає "Щодня". Якщо воно менше 40, метод повертає "EOD". 
///В іншому випадку метод шукає можливі параметри частоти для елемента з указаним терміном дії та вибирає варіант із найдовшою довжиною 
///циклу.
///Метод GetCostPercentileInHospital обчислює процентиль вартості запасів на день відносно витрат на день подібних товарів в інших
/// </summary>
namespace SPP.Core
{
	public class CalculationService
	{
		public decimal CalculateAverageDailyUse(IQueryable<ItemOrderRecord> orders, int weeks)
		{
			var totalDays = 7 * weeks;
			var totalOrderedAmount = QueryLastDays(orders, totalDays).Sum(o => o.Quantity);
			if (totalOrderedAmount < 0)
			{
				totalOrderedAmount = 0;
			}
			var result = totalOrderedAmount / totalDays;
			
			return result;
		}

		private IQueryable<ItemOrderRecord> QueryLastDays(IQueryable<ItemOrderRecord> orders, int days)
		{
			var orderHistoryLimitDate = DateTime.Today.Subtract(new TimeSpan(days, 0, 0, 0));
			return orders.Where(o => o.OrderDate >= orderHistoryLimitDate);
		}

		public EstimatedQuantityOnHandDto? CalculateEstimatedQuantityOnHand(IQueryable<ItemOrderRecord> newSupplyOrders,
			ItemState state, decimal ADU)
		{

			if (!IsEligibleForEstimate(state))
			{
				return null;
			}
			var result = new EstimatedQuantityOnHandDto();
			var newsupply = newSupplyOrders.Where(x => state.LastQuantityUpdate.HasValue && x.OrderDate >= state.LastQuantityUpdate.Value.Date);
			var news = newSupplyOrders.Where(x => state.LastQuantityUpdate.HasValue && x.OrderDate >= state.LastQuantityUpdate.Value.Date).ToList();

			result.NewSupplyQuantity = newsupply.Sum(x => x.Quantity);
			result.LastQuantityUpdate = state.LastQuantityUpdate;
			result.LastCountQuantity = state.LastCountQuantity;
			result.ADU = ADU;

			decimal daysSinceQuantityUpdate = DateTime.Now.Date.Subtract(state.LastQuantityUpdate.Value.Date).Days;

			result.Quantity = state.LastCountQuantity.Value - result.ADU * daysSinceQuantityUpdate + result.NewSupplyQuantity.Value;
			if (result.Quantity < 0) result.Quantity = 0;

			return result;
		}

		private bool IsEligibleForEstimate(ItemState state)
		{
			return state.LastCountQuantity.HasValue && state.LastCountQuantity >= 0 && state.LastQuantityUpdate.HasValue && !string.IsNullOrWhiteSpace(state.Frequency)
				&& state.LastCountCycleCode != state.CurrentCountCycleCode;
		}

		public string CalculateFrequency(IQueryable<ItemOrderRecord> orders, 
			IQueryable<decimal> hostpialCostsPerDay, decimal inventoryCostPerDay, int expirationInDays)
		{
			if (!QueryLastWeeks(orders, 26).Any())
			{
				// No orders during last 2 quarters
				return CalculationSettings.FREQUENCY_QUARTERLY;
			}

			var averageUse = CalculateAverageDailyUse(orders, 26);
			if (averageUse < 0.001M)
			{
				averageUse = CalculateAverageDailyUse(orders, 52);
			}
			
			var percentile = GetCostPercentileInHospital(hostpialCostsPerDay, inventoryCostPerDay);
			if (percentile < 20)
			{
				return CalculationSettings.FREQUENCY_DAILY;
			}
			if (percentile < 40)
			{
				return CalculationSettings.FREQUENCY_EOD;
			}

			return CalculationSettings.OPTIONS.GetPossibleFrequencyForItemWithExpiration(expirationInDays)
				.OrderByDescending(x => x.Value.CycleLengthInDays).Select(x => x.Key).First();

		}

		private decimal GetCostPercentileInHospital(IQueryable<decimal> hostpialCostsPerDay, decimal inventoryCostPerDay)
		{
			return 100M * hostpialCostsPerDay.Count(x => x >= inventoryCostPerDay) / hostpialCostsPerDay.Count();
		}

		private IQueryable<ItemOrderRecord> QueryLastWeeks(IQueryable<ItemOrderRecord> orders, int weeks)
		{
			var totalDays = 7 * weeks;
			return QueryLastDays(orders, totalDays);
		}
	}
}
