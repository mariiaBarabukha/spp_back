using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Документація для класу FrequencyOptions:
///Клас FrequencyOptions — це модель даних, яка представляє параметри частоти для певного продукту або елемента асортименту. 
///Він містить кілька властивостей, які використовуються для зберігання інформації, пов’язаної з використанням, доставкою та 
///буферизацією продукту.
///Властивості:
///Name(рядок): властивість рядка, яка представляє ім’я опції частоти.
///UsageDays(десятковий): десяткова властивість, яка представляє кількість днів між використанням продукту.
///DeliveryDays(десяткове число): десяткова властивість, яка представляє кількість днів між доставками продукту.
///BufferDays(десятковий): десяткова властивість, яка представляє кількість днів, які слід додати до властивості DeliveryDays,
///щоб створити буфер між доставками та використанням продукту.
///AverageUseDays(int): цілочисельна властивість, яка представляє середню кількість днів, протягом яких продукт використовується, 
///перш ніж його потрібно буде поповнити.
///CycleLengthInDays(int): цілочисельна властивість, яка представляє тривалість усього циклу для опції частоти, включаючи використання, 
///доставку та буферні дні.
///DaysToCriticalShortage(int): ціла властивість, яка представляє кількість днів, що залишилися до моменту, коли продукт досягне рівня
///критичної нестачі.
/// </summary>
namespace SPP.Core.Models
{
	public class FrequencyOptions
	{
		public string Name { get; set; }
		public decimal UsageDays { get; set; }
		public decimal DeliveryDays { get; set; }
		public decimal BufferDays { get; set; }
		public int AverageUseDays { get; set; }
		public int CycleLengthInDays { get; set; }
		public int DaysToCriticalShortage { get; set; }
	}
}
