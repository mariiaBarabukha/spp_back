using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Документація для класу FrequencyOptionsDictionary:
///Клас FrequencyOptionsDictionary — це колекція об’єктів FrequencyOptions, які зберігаються в словнику. Він надає методи для додавання 
///та отримання об’єктів FrequencyOptions, а також обчислення та отримання додаткової інформації на основі збережених об’єктів.
///Властивості:
///UniqueAverageUseWeekValues(IEnumerable<int>): IEnumerable цілих значень, які представляють унікальні середні дні використання для всіх
///об’єктів FrequencyOptions у словнику.
///Ключі (IEnumerable<string>): IEnumerable рядків, які представляють ключі (тобто імена) усіх об’єктів FrequencyOptions у словнику.
///Методи:
///Add(FrequencyOptions value): додає об’єкт FrequencyOptions до словника.
///this[рядковий індекс] (FrequencyOptions): властивість лише для читання, яка повертає об’єкт FrequencyOptions на основі його ключа 
///(тобто назви) у словнику.
///GetPossibleFrequencyForItemWithExpiration(int days) (Dictionary<string, FrequencyOptions>): повертає словник об’єктів FrequencyOptions, 
///придатних для використання з елементом, термін дії якого закінчиться через певну кількість днів. Повернений словник містить лише 
///параметри з довжиною циклу, яка менша або дорівнює вказаній кількості днів.
///Конструктор:
///FrequencyOptionsDictionary(IEnumerable < FrequencyOptions > options): ініціалізує новий екземпляр класу FrequencyOptionsDictionary
///колекцією об’єктів FrequencyOptions. Конструктор викликає метод Add для кожного об’єкта в колекції, щоб заповнити словник.
/// </summary>
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
