using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NIHApp.Infrastructure.Helpers
{
	public static class ListConvert
	{
		public static IList<T> ToGenericList<T>(IList items)
		{
			IList<T> genericList = new List<T>(items.Count);
			foreach (T item in items)
			{
				genericList.Add(item);
			}
			return genericList;
		}

		public static List<T> ToGenericIList<T>(IList items)
		{
			var genericList = new List<T>(items.Count);
			genericList.AddRange(items.Cast<T>());
			return genericList;
		}
	}
}