using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Security.AccessControl;
using Spider.Types;

//-----------------------------------------------------------
// Sample code to use this class.
//-----------------------------------------------------------
//public enum MMF_Items
//{
//	SharedValue_Int = 0,
//	SharedValue_String,
//	SharedValue_Boolean,

//	Max 
//}

//public class MMF
//{
//	static readonly int[] MMF_sz = new int[(int)MMF_Items.Max + 1]
//	{
//		sizeof(int),	// SharedValue_Int
//		512,			// SharedValue_String
//		sizeof(bool),	// SharedValue_Boolean
//		0				// dummy for Max
//	};

//	static MMF<MMF_Items> mmf = new MMF<MMF_Items>("SharedValue", MMF_sz);

//	public static T ReadData<T>(MMF_Items item)
//	{
//		return mmf.ReadData<T>(item);
//	}

//	public static void WriteData<T>(T src, MMF_Items item)
//	{
//		mmf.WriteData<T>(src, item);
//	}
//}
//-----------------------------------------------------------

namespace Spider.IO
{
	/// <summary>
	/// <para>wrapper class to user MemoryMappedFile for inter process data sharing.</para>
	/// </summary>
	/// <param name="TEnumType">enum which defines variable names which will be shared.</param>
	public class MMF<TEnumType> where TEnumType : struct, IConvertible
	{
		MemoryMappedFile mmf;
		
		readonly int[] MMF_sz;
		Dictionary<int,string> items;
		
		Object thisLock = new Object();

//-----------------------------------------------------------
		///<param name="Global">
		///True: Create MMF which is shred by all users in this PC. The current user should have an administrator right.
		///False: Create MMF for current user.<br />
		///</param>
		public MMF(string Name, int[] ValueSizeArray, bool Global = false)
		{
			if(!typeof(TEnumType).IsEnum) 
			{
				throw new ArgumentException("T must be an enumerated type");

			}else
			{
				items = TypeUtilities.EnumToDictionary<TEnumType>();
			
				if(ValueSizeArray.Count() != items.Count())
					throw new ArgumentException("Number of sizes array does not match to number of specified enumerated");
			}

			MMF_sz = ValueSizeArray;

			string MMF_Name = "";

			if(Global)
				MMF_Name = "Global\\" + Name;
			else
				MMF_Name = "Local\\" + Name + Environment.UserName;

			if(Global && !Utilities.IsAdministrator())
			{
				try
				{
					mmf = MemoryMappedFile.OpenExisting(MMF_Name);

				}catch {}

			}else
			{
				MemoryMappedFileSecurity security = new MemoryMappedFileSecurity();
				security.AddAccessRule(new AccessRule<MemoryMappedFileRights>(("Everyone"), MemoryMappedFileRights.FullControl, AccessControlType.Allow));

				try
				{
					mmf = MemoryMappedFile.CreateOrOpen(
												MMF_Name,
												GetMMF_Adr(items.Count() - 1),
												MemoryMappedFileAccess.ReadWriteExecute,
												MemoryMappedFileOptions.None,
												security,
												HandleInheritability.Inheritable);
				}catch {}
			}
		}

//-----------------------------------------------------------
		/// <summary>
		/// <para>Read data from MemoryMappedFile.</para>
		/// </summary>
		public T ReadData<T>(TEnumType item)
		{
			object ans = null;

			if(mmf != null)
			{
				int size = MMF_sz[items.FirstOrDefault(a => a.Value == item.ToString()).Key];
				byte[] buf = new byte[size];

				try
				{
					MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(GetMMF_Adr(item), size);
					lock(thisLock)
					{
						accessor.ReadArray<byte>(0, buf, 0, size);
					}

					if((typeof(T) == typeof(int)) || (typeof(T) == typeof(IntPtr)))
					{
						ans = BitConverter.ToInt32(buf, 0);

					}else if(typeof(T) == typeof(uint))
					{
						ans = BitConverter.ToUInt32(buf, 0);

					}else if(typeof(T) == typeof(bool))
					{
						ans = BitConverter.ToBoolean(buf, 0);

					}else if(typeof(T) == typeof(string))
					{
						foreach(char wrk in buf)
						{
							if(wrk == '\0')
								break;
	
							ans = (string)ans + wrk;
						}
					}
				}
				catch(Exception e)
				{
					throw new SharedMemoryException(e.ToString());
				}
			}

			return TypeUtilities.ConvertFromObject<T>(ans);
		}

//-----------------------------------------------------------
		/// <summary>
		/// <para>Write data from MemoryMappedFile.</para>
		/// </summary>
		/// <param name="src">Value to write.</param>
		/// <param name="item">Variable name.</param>
		public void WriteData<T>(T src, TEnumType item)
		{
			if(mmf != null)
			{
				object val = (object)src;
				byte[] buf = new byte[0];

				if(typeof(T) == typeof(int))
					buf = BitConverter.GetBytes((int)val);
				else if(typeof(T) == typeof(uint))
					buf = BitConverter.GetBytes((uint)val);
				else if(typeof(T) == typeof(bool))
					buf = BitConverter.GetBytes((bool)val);
				else if(typeof(T) == typeof(string) && !String.IsNullOrEmpty((string)val))
					buf = Encoding.ASCII.GetBytes((string)val);
			
				int size = MMF_sz[items.FirstOrDefault(a => a.Value == item.ToString()).Key];

				byte[] fullsize_buf = new byte[size];
				Array.Clear(fullsize_buf, 0, fullsize_buf.Length);
				Array.Copy(buf, fullsize_buf, buf.Length);

				try
				{
					MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(GetMMF_Adr(item), size);
					lock(thisLock)
					{
						accessor.WriteArray<byte>(0, fullsize_buf, 0, fullsize_buf.Length);
					}
				}
				catch(Exception e)
				{
					throw new SharedMemoryException(e.ToString());
				}
			}
		}

//-----------------------------------------------------------
		int GetMMF_Adr(int idx)
		{
			int ans = 0;

			for(int i = 0; i < idx; i++)
				ans += MMF_sz[i];

			return ans; 
		}

//-----------------------------------------------------------
		int GetMMF_Adr(TEnumType item)
		{
			return GetMMF_Adr(items.FirstOrDefault(a => a.Value == item.ToString()).Key); 
		}
	}

//-----------------------------------------------------------
	[Serializable]
	public class SharedMemoryException : ApplicationException
	{
		public SharedMemoryException()
		{

		}
		public SharedMemoryException(string message)
			: base(message)
		{
		}
		public SharedMemoryException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}

//-----------------------------------------------------------
}
