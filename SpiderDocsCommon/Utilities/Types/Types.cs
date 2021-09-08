using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//---------------------------------------------------------------------------------
namespace Spider.Types
{
	public enum ThreeStateBool
	{
		False,
		True,
		Intermidiate
	}

//---------------------------------------------------------------------------------
	public static class TypesExtenstion
	{
//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Get boolen value from ThreeStateBool</para>
		/// </summary>
		public static bool GetBool(this ThreeStateBool val)
		{
			return val == ThreeStateBool.True;
		}

//---------------------------------------------------------------------------------
		public static double RoundUp(this double input, int places)
		{
			double multiplier = Math.Pow(10, Convert.ToDouble(places));
			return Math.Ceiling(input * multiplier) / multiplier;
		}

//---------------------------------------------------------------------------------
		///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
		///<param name="items">The enumerable to search.</param>
		///<param name="predicate">The expression to test the items against.</param>
		///<returns>The index of the first matching item, or -1 if no items match.</returns>
		public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate) {
			if (items == null) throw new ArgumentNullException("items");
			if (predicate == null) throw new ArgumentNullException("predicate");

			int retVal = 0;
			foreach (var item in items) {
				if (predicate(item)) return retVal;
				retVal++;
			}
			return -1;
		}

//---------------------------------------------------------------------------------
		///<summary>Finds the index of the first occurence of an item in an enumerable.</summary>
		///<param name="items">The enumerable to search.</param>
		///<param name="item">The item to find.</param>
		///<returns>The index of the first matching item, or -1 if the item was not found.</returns>
		public static int IndexOf<T>(this IEnumerable<T> items, T item) { return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i)); }

//---------------------------------------------------------------------------------
	}
}
