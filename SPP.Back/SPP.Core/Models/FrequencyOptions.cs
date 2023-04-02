using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
