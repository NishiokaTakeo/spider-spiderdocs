using System;
using Spider.IO;

namespace SpiderDocsForms
{
//-----------------------------------------------------------
	public enum SharedMMF_Items
	{
		ServerAddress = 0,
		ServerPort,

		Max 
	}

//-----------------------------------------------------------
	public class SharedMMF
	{
		// do not insert or delete items in the middle to keep compatibility of memory addresses with previous versions
		static readonly int[] MMF_sz = new int[(int)SharedMMF_Items.Max + 1]
		{
			100,			// ServerAddress (100 should be enough)
			sizeof(int),	// ServerPort
			0				// dummy for Max
		};

		static MMF<SharedMMF_Items> mmf = new MMF<SharedMMF_Items>("SpiderDocsLocalMachineMemory", MMF_sz, true);

//-----------------------------------------------------------
		public static T ReadData<T>(SharedMMF_Items item)
		{
			return mmf.ReadData<T>(item);
		}

//-----------------------------------------------------------
		public static void WriteData<T>(T src, SharedMMF_Items item)
		{
			mmf.WriteData<T>(src, item);
		}
	}

//-----------------------------------------------------------
}
