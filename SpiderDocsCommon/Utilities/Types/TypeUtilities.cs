using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//---------------------------------------------------------------------------------
namespace Spider.Types
{
	public class TypeUtilities
	{
//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Convert object to certain type with value evaluation.</para>
		/// </summary>
		/// <param name="val">target object</param>
		/// <param name="DefaultValue">Default value when converting fails</param>
		public static T ConvertFromObject<T>(object val, object DefaultValue = null)
		{
			return (T)Convert.ChangeType(ConvertFromObject(val, typeof(T), DefaultValue), typeof(T));
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Convert object to certain type with value evaluation.</para>
		/// </summary>
		/// <param name="val">target object</param>
		/// <param name="type">Type to convert</param>
		/// <param name="DefaultValue">Default value when converting fails</param>
		public static object ConvertFromObject(object val, Type type, object DefaultValue = null)
		{
			object ans = DefaultValue;

			// enum is considered as int
			if(type.IsEnum)
				type = typeof(int);

			// determine default value according to type id it is not given
			if(DefaultValue == null)
			{
				if((type == typeof(int))
				|| (type == typeof(long))
				|| (type == typeof(double))
				|| (type == typeof(float))
				|| (type == typeof(Single))
				)
				{
					ans = -1;
				}
				else if((type == typeof(int?))
				|| (type == typeof(long?))
				|| (type == typeof(double?))
				|| (type == typeof(float?))
				|| (type == typeof(Single?)))
				{
					ans = null;
				}
				else if(type == typeof(string)) ans = "";
				else if(type == typeof(bool) || type == typeof(bool?)) ans = false;
				else if(type == typeof(DateTime)) ans = new DateTime();
				else if(type == typeof(DateTime?)) ans = null;
				else if(type == typeof(uint)) ans = 0;
				else if(type == typeof(IntPtr)) ans = IntPtr.Zero;
			}

			// return default value if value is null
			if((val == null) || (val == DBNull.Value))
				return ans;

			// convert to specified type
			try
			{
				if(type == typeof(string))
					ans = Convert.ToString(val);
				else if(type == typeof(int) || type == typeof(int?) || type == typeof(IntPtr))
					ans = Convert.ToInt32(val);
				else if(type == typeof(long) || type == typeof(long?))
					ans = Convert.ToInt64(val);
				else if(type == typeof(bool) || type == typeof(bool?))
				{
					int tmp;
					if(int.TryParse(val.ToString(), out tmp))
						val = tmp;

					ans = Convert.ToBoolean(val);
				}
				else if((type == typeof(DateTime)) || (type == typeof(DateTime?)))
					ans = Convert.ToDateTime(val);
				else if(type == typeof(double) || type == typeof(double?))
					ans = Convert.ToDouble(val);
				else if((type == typeof(float)) || (type == typeof(Single))
					 || (type == typeof(float?)) || (type == typeof(Single?)))
					ans = Convert.ToSingle(val);
				else if(type == typeof(uint))
					ans = Convert.ToUInt32(val);

			}catch{}

			return ans;
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Convert enum to Dictionary. Useful to make a data source for such as a dropdown list.</para>
		/// </summary>
		/// <param name="exclude_vals">values which will be excluded from this dictionary.</param>
		public static Dictionary<int,string> EnumToDictionary<T>(params T[] exclude_vals)
		{
			IEnumerable<KeyValuePair<int,string>> values =
				from T e in Enum.GetValues(typeof(T))
				where !exclude_vals.Contains(e)
				select new KeyValuePair<int,string>((int)(Enum.Parse(typeof(T), e.ToString())), e.ToString().Replace("_", " "));

			var list = new List<KeyValuePair<int,string>>(values.ToList());
			var dictionary = list.ToDictionary(x => x.Key, x => x.Value);

			return dictionary;
		}

//---------------------------------------------------------------------------------
	}
}
