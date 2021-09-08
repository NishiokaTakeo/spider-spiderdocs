using System;
using Spider.IO;

namespace SpiderDocsModule
{
//-----------------------------------------------------------
	public enum MMF_Items
	{
		GridUpdateCount = 0,
		WindowHandle,
		DmsFilePath,
		PropertyUpdateReq,
		UserId,
		WorkSpaceUpdateCount,
		SendTo,
        PreferenceChanged,
        Max 
	}

//-----------------------------------------------------------
	public class MMF
	{
		static readonly int[] MMF_sz = new int[(int)MMF_Items.Max + 1]
		{
			sizeof(uint),	// GridUpdateCount
			sizeof(int),	// WindowHandle
			512,			// DmsFilePath
			sizeof(int),	// PropertyUpdateReq
			sizeof(int),	// UserId
			sizeof(uint),	// WorkSpaceUpdateCount
			sizeof(uint),	// Sendto
            sizeof(uint),	// PreferenceChanged
			0				// dummy for Max			
		};

		static MMF<MMF_Items> mmf = new MMF<MMF_Items>("SpiderDocsClientMemory", MMF_sz);

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
