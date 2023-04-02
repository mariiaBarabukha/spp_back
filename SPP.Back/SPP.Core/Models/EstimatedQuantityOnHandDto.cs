using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
