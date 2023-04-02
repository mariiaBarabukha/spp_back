using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
