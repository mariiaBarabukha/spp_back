using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPP.Core.Models
{
	public class ItemOrderRecord
	{
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public DateTime OrderDate { get; set; }
	}
}
