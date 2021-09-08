using System;
using Spider.IO;

namespace SpiderDocsServer
{
//-----------------------------------------------------------
	public enum MMF_Items
	{
		WindowHandle,

		Max 
	}

//-----------------------------------------------------------
	public class MMF
	{
		// do not insert or delete items in the middle to keep compatibility of memory addresses with previous versions
		static readonly int[] MMF_sz = new int[(int)MMF_Items.Max + 1]
		{
			sizeof(int),	// WindowHandle
			0				// dummy for Max
		};

		static MMF<MMF_Items> mmf = new MMF<MMF_Items>("SpiderDocsServerMemory", MMF_sz);

//-----------------------------------------------------------
		public static T ReadData<T>(MMF_Items item)
		{
			return mmf.ReadData<T>(item);
		}

//-----------------------------------------------------------
		public static void WriteData<T>(T src, MMF_Items item)
		{
			mmf.WriteData<T>(src, item);
		}
	}

//-----------------------------------------------------------
}
