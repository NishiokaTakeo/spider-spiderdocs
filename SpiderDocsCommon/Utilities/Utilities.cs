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

//---------------------------------------------------------------------------------
namespace Spider
{
	public partial class Utilities
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




	}
}
