using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Principal;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;


//---------------------------------------------------------------------------------
namespace Spider.Data
{
	public class Utilities
	{
//---------------------------------------------------------------------------------
		/// <summary>
		/// Perform a deep Copy of the object.
		/// </summary>
		/// <typeparam name="T">The type of object being copied.</typeparam>
		/// <param name="source">The object instance to copy.</param>
		/// <returns>The copied object.</returns>
		public static T ObjectClone<T>(T source)
		{
			if(!typeof(T).IsSerializable)
			{
				throw new ArgumentException("The type must be serializable.", "source");
			}

			// Don't serialize a null object, simply return the default for that object
			if(Object.ReferenceEquals(source, null))
			{
				return default(T);
			}

			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();
			using(stream)
			{
				formatter.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}


		public static T ObjectCopyValues<T>(object src) where T : new()
		{
			T ans = new T();

			ObjectCopyValues(src, ans);

			return ans;
		}


		public static void ObjectCopyValues(object src, object dst)
		{
			PropertyInfo[] props = src.GetType().GetProperties();
			PropertyInfo[] dst_props = dst.GetType().GetProperties();

			foreach(PropertyInfo prop in props)
			{
				PropertyInfo dst_prop = dst_props.FirstOrDefault(a => a.Name == prop.Name);

				if((dst_prop != null) && (prop.PropertyType == dst_prop.PropertyType) && prop.CanWrite)
					dst_prop.SetValue(dst, prop.GetValue(src, null), null);
			}

			FieldInfo[] fields = src.GetType().GetFields();
			FieldInfo[] dst_fields = dst.GetType().GetFields();

			foreach(FieldInfo field in fields)
			{
				FieldInfo dst_field = dst_fields.FirstOrDefault(a => a.Name == field.Name);

				if((dst_field != null) && (field.FieldType == dst_field.FieldType))
					dst_field.SetValue(dst, field.GetValue(src));
			}
		}

//---------------------------------------------------------------------------------
		
		public static string GetOSArchitecture()
		{
			string pa = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine);
			return ((String.IsNullOrEmpty(pa) || String.Compare(pa, 0, "x86", 0, 3, true) == 0) ? "" : "\\Wow6432Node");
		}

		public static bool Is64bitOS()
		{
			if((8 == IntPtr.Size)
			|| (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
			{
				return true;
			}

			return false;
		}

		public static bool Is64bitApp()
		{
			if(8 == IntPtr.Size)
				return true;

			return false;
		}

	
//---------------------------------------------------------------------------------
		// Utilities.GetMemberName(() => [PROPERTY]));
		// e.g. Utilities.GetMemberName(() => shireId));
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
			return expressionBody.Member.Name;
		}


//---------------------------------------------------------------------------------


 
		public static string SerializeAnObject(object AnObject)
		{
			XmlSerializer Xml_Serializer = new XmlSerializer(AnObject.GetType());
			StringWriter Writer = new StringWriter();      
 
			Xml_Serializer.Serialize(Writer, AnObject);
			return Writer.ToString();
		}

		public static T DeSerializeAnObject<T>(string XmlOfAnObject)
		{
			string dummy;

			return DeSerializeAnObject<T>(XmlOfAnObject, out dummy);
		}

		public static T DeSerializeAnObject<T>(string XmlOfAnObject, out string error)
		{       
			T AnObject = default(T);
			error = "";

			StringReader StrReader = new StringReader(XmlOfAnObject);
			XmlSerializer Xml_Serializer = new XmlSerializer(typeof(T));
			XmlTextReader XmlReader = new XmlTextReader(StrReader);

			try
			{
				AnObject = (T)Convert.ChangeType(Xml_Serializer.Deserialize(XmlReader), typeof(T));
			}
			catch(Exception e)
			{
				error = e.Message + "\n" + e.StackTrace + "\n" + e.InnerException.Message;
			}
			finally
			{
				XmlReader.Close();
				StrReader.Close();
			}

			return AnObject;
		}

		public static bool IsAdministrator()
		{
			var identity = WindowsIdentity.GetCurrent();
			var principal = new WindowsPrincipal(identity);
			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}

        #region Type Utility
        //---------------------------------------------------------------------------------
		static readonly string[] tb_ReservedWords =
		{
			"user",
			"group",
			"event"
		};

		/// <summary>
		/// <para>Add brackets to words which is reserved by SQL. (e.g. "select user from abc" becomes "select [user] from abc".)</para>
		/// <para>It does not add the brackets where they already exist. (e.g. there is no change for "select [user] from abc" as it originally has the brackets.)</para>
		/// </summary>
		/// <param name="src">SQL sentence</param>
		public static string SqlConvert(string src) 
		{
			foreach(string target in tb_ReservedWords)
				src = Regex.Replace(src, @"([^\[]?)(\b" + target + @"\b)", @"$1[" + target + @"]", RegexOptions.IgnoreCase);

			return src;
		}

//---------------------------------------------------------------------------------
		/// <summary>
		/// <para>Generate a CSV file from given DataTable</para>
		/// </summary>
		/// <param name="dt">source DataTable</param>
		/// <param name="path">path to save the CSV file</param>
		public static void ExportDataTableToCSV(DataTable dt, string path) 
		{
			StringBuilder sb = new StringBuilder(); 

			IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
											  Select(column => column.ColumnName);
			sb.AppendLine(string.Join(",", columnNames));

			foreach (DataRow row in dt.Rows)
			{
				IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
				sb.AppendLine(string.Join(",", fields));
			}

			File.WriteAllText(path, sb.ToString());
		}

        #endregion
        
        #region Type Utility


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
            if (type.IsEnum)
                type = typeof(int);

            // determine default value according to type id it is not given
            if (DefaultValue == null)
            {
                if ((type == typeof(int))
                || (type == typeof(long))
                || (type == typeof(double))
                || (type == typeof(float))
                || (type == typeof(Single))
                )
                {
                    ans = -1;
                }
                else if ((type == typeof(int?))
                || (type == typeof(long?))
                || (type == typeof(double?))
                || (type == typeof(float?))
                || (type == typeof(Single?)))
                {
                    ans = null;
                }
                else if (type == typeof(string)) ans = "";
                else if (type == typeof(bool) || type == typeof(bool?)) ans = false;
                else if (type == typeof(DateTime)) ans = new DateTime();
                else if (type == typeof(DateTime?)) ans = null;
                else if (type == typeof(uint)) ans = 0;
                else if (type == typeof(IntPtr)) ans = IntPtr.Zero;
            }

            // return default value if value is null
            if ((val == null) || (val == DBNull.Value))
                return ans;

            // convert to specified type
            try
            {
                if (type == typeof(string))
                    ans = Convert.ToString(val);
                else if (type == typeof(int) || type == typeof(int?) || type == typeof(IntPtr))
                    ans = Convert.ToInt32(val);
                else if (type == typeof(long) || type == typeof(long?))
                    ans = Convert.ToInt64(val);
                else if (type == typeof(bool) || type == typeof(bool?))
                {
                    int tmp;
                    if (int.TryParse(val.ToString(), out tmp))
                        val = tmp;

                    ans = Convert.ToBoolean(val);
                }
                else if ((type == typeof(DateTime)) || (type == typeof(DateTime?)))
                    ans = Convert.ToDateTime(val);
                else if (type == typeof(double) || type == typeof(double?))
                    ans = Convert.ToDouble(val);
                else if ((type == typeof(float)) || (type == typeof(Single))
                     || (type == typeof(float?)) || (type == typeof(Single?)))
                    ans = Convert.ToSingle(val);
                else if (type == typeof(uint))
                    ans = Convert.ToUInt32(val);

            }
            catch { }

            return ans;
        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// <para>Convert enum to Dictionary. Useful to make a data source for such as a dropdown list.</para>
        /// </summary>
        /// <param name="exclude_vals">values which will be excluded from this dictionary.</param>
        public static Dictionary<int, string> EnumToDictionary<T>(params T[] exclude_vals)
        {
            IEnumerable<KeyValuePair<int, string>> values =
                from T e in Enum.GetValues(typeof(T))
                where !exclude_vals.Contains(e)
                select new KeyValuePair<int, string>((int)(Enum.Parse(typeof(T), e.ToString())), e.ToString().Replace("_", " "));

            var list = new List<KeyValuePair<int, string>>(values.ToList());
            var dictionary = list.ToDictionary(x => x.Key, x => x.Value);

            return dictionary;
        }
        #endregion

        public static string ReplaceEOL2Space(string str)
        {
            string rep = System.Text.RegularExpressions.Regex.Replace(str, @"\t|\n|\r", " ");

            rep = System.Text.RegularExpressions.Regex.Replace(str, @"\s+", " ");

            return rep;
        }
    }
}
