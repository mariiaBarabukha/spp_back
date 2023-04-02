using SPP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
