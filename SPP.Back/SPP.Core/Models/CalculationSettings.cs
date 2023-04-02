using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPP.Core.Models
{
	public class CalculationSettings
	{
		public static string FREQUENCY_DAILY = "Daily";
		public static string FREQUENCY_EOD = "EOD"; // every other day
		public static string FREQUENCY_WEEKLY = "Weekly";
		public static string FREQUENCY_EOW = "EOW"; // every other week
		public static string FREQUENCY_MONTHLY = "Monthly";
		public static string FREQUENCY_QUARTERLY = "Quarterly";


		public static readonly FrequencyOptionsDictionary OPTIONS = new FrequencyOptionsDictionary(new List<FrequencyOptions>
		{
			new FrequencyOptions 
			{
				Name = FREQUENCY_DAILY, 
				UsageDays = 1, 
				DeliveryDays = 1, 
				BufferDays = 1, 
				AverageUseDays = 7, 
				CycleLengthInDays = 1, 
				DaysToCriticalShortage = 1 
			},
			new FrequencyOptions 
			{ 
				Name = FREQUENCY_EOD, 
				UsageDays = 2, 
				DeliveryDays = 1, 
				BufferDays = 1, 
				AverageUseDays = 7, 
				CycleLengthInDays = 2, 
				DaysToCriticalShortage = 1 
			},
			new FrequencyOptions 
			{ 
				Name = FREQUENCY_WEEKLY, 
				UsageDays = 7, 
				DeliveryDays = 1, 
				BufferDays = 3.5M, 
				AverageUseDays = 13, 
				CycleLengthInDays = 7, 
				DaysToCriticalShortage = 7 
			},
			new FrequencyOptions 
			{ 
				Name = FREQUENCY_EOW, 
				UsageDays = 14, 
				DeliveryDays = 1, 
				BufferDays = 3.5M, 
				AverageUseDays = 13, 
				CycleLengthInDays = 14, 
				DaysToCriticalShortage = 7 
			},
			new FrequencyOptions 
			{ 
				Name = FREQUENCY_MONTHLY, 
				UsageDays = 28, 
				DeliveryDays = 2, 
				BufferDays = 7, 
				AverageUseDays = 26, 
				CycleLengthInDays = 28, 
				DaysToCriticalShortage = 14 
			},
			new FrequencyOptions 
			{ 
				Name = FREQUENCY_QUARTERLY, 
				UsageDays = 91, 
				DeliveryDays = 2, 
				BufferDays = 14,
				AverageUseDays = 52, 
				CycleLengthInDays = 91,
				DaysToCriticalShortage = 35 
			}

		});
	}
}
