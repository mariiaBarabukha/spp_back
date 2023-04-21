using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Цей код визначає клас C# CalculationSettings у просторі імен SPP.Core.Models. Цей клас визначає кілька статичних рядкових констант,
/// які представляють параметри частоти: щодня, через день (EOD), щотижня, через тиждень (EOW), щомісяця та щокварталу.

///Цей клас також визначає статичний об’єкт FrequencyOptionsDictionary, доступний лише для читання, під назвою OPTIONS. 
///FrequencyOptionsDictionary — це настроюваний словник, який зіставляє назви опцій частоти з об’єктами FrequencyOptions, які 
///представляють різні параметри обчислення для кожної опції частоти.

///Об’єкти FrequencyOptions створюються для кожного параметра частоти та додаються до OPTIONS за допомогою конструктора 
///List<FrequencyOptions> словника FrequencyOptionsDictionary. Кожен об’єкт FrequencyOptions визначає різні параметри обчислення для 
///відповідного параметра частоти, наприклад кількість днів використання, доставки, буфера, середнього використання, тривалості циклу та 
///днів до критичної нестачі.

///Метою цього класу є забезпечення централізованого розташування для зберігання та доступу до налаштувань обчислень, пов’язаних із 
///різними параметрами частоти. Інші частини програми можуть отримати доступ до цих параметрів, посилаючись на статичні члени класу 
///CalculationSettings. Наприклад, CalculationSettings.FREQUENCY_MONTHLY поверне рядок «Monthly», 
///а CalculationSettings.OPTIONS[FREQUENCY_MONTHLY] поверне об’єкт FrequencyOptions із відповідними параметрами розрахунку для щомісячної 
///частоти.
/// </summary>
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
