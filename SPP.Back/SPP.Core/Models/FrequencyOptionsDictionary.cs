using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPP.Core.Models
{
	public class FrequencyOptionsDictionary
	{
		private readonly Dictionary<string, FrequencyOptions> _optionsStorage = new Dictionary<string, FrequencyOptions>(StringComparer.OrdinalIgnoreCase);

		public FrequencyOptionsDictionary(IEnumerable<FrequencyOptions> options)
		{
			foreach (var option in options)
			{
				Add(option);
			}
		}

		public void Add(FrequencyOptions value)
		{
			_optionsStorage.Add(value.Name, value);
		}

		public FrequencyOptions this[string index] => _optionsStorage[index];

		public IEnumerable<int> UniqueAverageUseWeekValues => _optionsStorage.Select(o => o.Value.AverageUseDays).Distinct().OrderBy(val => val);

		public IEnumerable<string> Keys => _optionsStorage.Keys;

		public Dictionary<string, FrequencyOptions> GetPossibleFrequencyForItemWithExpiration(int days)
		{
			return _optionsStorage.Where(o => o.Value.CycleLengthInDays <= days).ToDictionary(o => o.Key, o => o.Value);
		}
	}
}
