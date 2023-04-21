using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Документація для класу ItemOrderRecord:
///Клас ItemOrderRecord представляє запис замовлення товару. Він містить інформацію про замовлену кількість, ціну товару та 
///дату розміщення замовлення.
///Властивості:
///Quantity(десяткове число): кількість товару, який було замовлено.
///Price(десяткова): ціна товару.
///OrderDate(DateTime): дата розміщення замовлення.
/// </summary>
namespace SPP.Core.Models
{
	public class ItemOrderRecord
	{
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public DateTime OrderDate { get; set; }
	}
}
