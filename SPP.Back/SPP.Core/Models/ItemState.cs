using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Документація для класу ItemState:
///Клас ItemState представляє стан елемента в системі, яка керує запасами або запасами. Він містить інформацію про частоту 
///підрахунку одиниць, дату останнього замовлення, кількість останнього замовлення, останнє оновлення кількості, кількість останнього 
///підрахунку, а також поточний і останній коди циклу підрахунку.
///Властивості:
///Frequency(рядок): частота підрахунку елементів, наприклад щодня, щотижня або щомісяця.
///LastOrderDate(DateTime?): дата останнього розміщення замовлення на товар, якщо таке є.
///LastOrderQuantity(десяткове?): кількість останнього замовлення, розміщеного для товару, якщо таке є.
///LastQuantityUpdate(DateTime?): дата останнього оновлення кількості для товару, якщо таке є.
///LastCountQuantity(десяткове?): кількість, підрахована під час останнього підрахунку товару, якщо така була.
///CurrentCountCycleCode(рядок): код, що представляє поточний цикл підрахунку елемента.
///LastCountCycleCode(рядок?): код, що представляє останній цикл підрахунку елемента, якщо такий є.
/// </summary>
namespace SPP.Core.Models
{
	public class ItemState
	{
		public string Frequency { get; set; }
		public DateTime? LastOrderDate { get; set; }
		public decimal? LastOrderQuantity { get; set; }
		public DateTime? LastQuantityUpdate { get; set; }
		public decimal? LastCountQuantity { get; set; }
		public string CurrentCountCycleCode { get; set; }
		public string? LastCountCycleCode { get; set; }		
	}
}
