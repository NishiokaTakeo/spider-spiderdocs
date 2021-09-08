using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Net;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.Serialization;
using System.Security.Principal;
using System.Drawing;
using System.Windows.Forms;

//---------------------------------------------------------------------------------
namespace SpiderDocsModule
{
	public class Utilities// : Spider.Utilities
	{
        // static readonly string logs_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SpiderDocs\\__Logs__\\";

        // public static bool CreateLogsFolder()
        // {
        //     return FileFolder.CreateFolder(logs_path);
        // }

        // public static string GetLogsFolder()
        // {
        //     return logs_path;
        // }
//---------------------------------------------------------------------------------
// error logger --------------------------------------------------------------------
//---------------------------------------------------------------------------------
        /*
		public static bool regLog(string textrror, params string[] args)
		{
			bool ans = true;

			try
			{
				string strLogMessage = string.Empty;
                string pathLogFile = GetLogsFolder() + "SpiderDocsLog.txt";
				StreamWriter swLog;

                if (!Directory.Exists(GetLogsFolder()))
                    Directory.CreateDirectory(GetLogsFolder());

				strLogMessage = string.Format("{0}: {1}", DateTime.Now, textrror);
                
				if(!File.Exists(pathLogFile))
					swLog = new StreamWriter(pathLogFile);
				else
					swLog = File.AppendText(pathLogFile);

				swLog.WriteLine(strLogMessage);
				swLog.WriteLine();
				swLog.Close();
			}
			catch
			{
				ans = false;
			}

			return ans;
		}
        */
//---------------------------------------------------------------------------------
// Etc.
//---------------------------------------------------------------------------------
		public static string CreateExternalLink(string webService_address, int id_doc)
		{
			string url = webService_address;
			url = url.Trim();

			if(url != "")
				url += ("?id=" + id_doc);

			return url;
		}

//---------------------------------------------------------------------------------
		public static string CheckIpAddress(string IPAddress)
		{
			if(String.IsNullOrEmpty(IPAddress))
				return "";
			
			if(IsValidIPAddress(IPAddress))
				return IPAddress;

			IPHostEntry LocalEntry = Dns.GetHostEntry(IPAddress);
			foreach(IPAddress Address in LocalEntry.AddressList)
			{
				if(IsValidIPAddress(Address.ToString()))
					return Address.ToString();
			}

			return "";
		}

//---------------------------------------------------------------------------------
		public static bool IsValidIPAddress(string ipAddr)
		{
			string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
			Regex reg = new Regex(pattern, RegexOptions.Singleline | RegexOptions.ExplicitCapture);
			return reg.IsMatch(ipAddr);
		}

//---------------------------------------------------------------------------------
		[DllImport("user32.dll")]
		static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("User32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("User32.dll", EntryPoint = "UpdateWindow", CharSet = CharSet.Auto)]
		static extern bool UpdateWindow(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint = "IsIconic", CharSet = CharSet.Auto)]
		static extern bool IsIconic(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool IsWindow(IntPtr hWnd);

//---------------------------------------------------------------------------------
		public static bool IsAnotherInstance(IntPtr mainWindowHandle)
		{
			bool ans = false;

			if(IsWindow(mainWindowHandle))
			{
				if(IsIconic(mainWindowHandle))
					ShowWindow(mainWindowHandle, 1);
				else
					ShowWindow(mainWindowHandle, 9);

				UpdateWindow(mainWindowHandle);

				//bring to the front
				SetForegroundWindow(mainWindowHandle);

				ans = true;
			}

			return ans;
		}


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
