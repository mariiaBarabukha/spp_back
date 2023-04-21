using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Документація для класу EstimatedQuantityOnHandDto:
///Клас EstimatedQuantityOnHandDto — це об’єкт передачі даних, який представляє приблизну кількість певного продукту в наявності. 
///Він містить кілька властивостей, які використовуються для зберігання інформації, пов’язаної з наявною кількістю, а також методи для 
///обчислення похідних значень.
///Властивості:
///LastQuantityUpdate(DateTime ?): властивість DateTime із значенням NULL, яка представляє дату останнього оновлення кількості продукту.
///LastCountQuantity (Decimal?): десяткова властивість із можливістю обнулення, яка представляє останню підраховану кількість продукту.
///NewSupplyQuantity (Decimal?): десяткова властивість із значенням nullable, яка представляє нову кількість постачання для продукту.
///LastOrderDate (DateTime?): властивість DateTime із значенням NULL, яка представляє дату останнього замовлення на продукт.
///LastOrderedQuantity (десятковий?): десяткова властивість, яка може обнулятися, представляє кількість продукту, яку було замовлено 
///востаннє.
///Quantity (десяткове число): десяткова властивість, яка представляє наявну кількість продукту.
///ADU (десятковий): десяткова властивість, яка представляє середнє щоденне використання продукту.
///Методи:
///DaysSinceLastCount(int ?): властивість цілого числа з можливістю нульового значення, яка представляє кількість днів, 
///що минули з моменту останнього оновлення кількості. Ця властивість обчислюється за допомогою властивості LastQuantityUpdate і 
///поточної дати.
/// </summary>

namespace SPP.Core.Models
{
	public class EstimatedQuantityOnHandDto
	{
		public DateTime? LastQuantityUpdate { get; set; }
		public Decimal? LastCountQuantity { get; set; }
		public Decimal? NewSupplyQuantity { get; set; }
		public DateTime? LastOrderDate { get; set; }
		public decimal? LastOrderedQuantity { get; set; }
		public decimal Quantity { get; set; }
		public decimal ADU { get; set; }
		public int? DaysSinceLastCount => LastQuantityUpdate.HasValue 
			? DateTime.Now.Date.Subtract(LastQuantityUpdate.Value.Date).Days 
			: null;

	}
}
