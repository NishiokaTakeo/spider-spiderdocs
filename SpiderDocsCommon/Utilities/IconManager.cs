using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

//--------------------------------------------------------------------------------
namespace Spider.Drawing
{

	/// <summary>
	/// <para>This class provide icon images from a given extension.</para>
	/// <para>Images are cached so they will not be loaded twice from next time.</para>
	/// </summary>
	public class IconManager
	{
//--------------------------------------------------------------------------------
		List<icon_tb> ext = new List<icon_tb>();

		int ImageIndex;		
		List<Image> icons_l = new List<Image>();
		List<Image> icons_s = new List<Image>();

//--------------------------------------------------------------------------------
		class icon_tb
		{
			public IntPtr iIcon;
			public int index;
			public string key;
		};

//--------------------------------------------------------------------------------
		[StructLayout(LayoutKind.Sequential)]
		struct SHFILEINFO
		{
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

//--------------------------------------------------------------------------------
		class Win32
		{
			public const uint SHGFI_ICON = 0x100;
			public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
			public const uint SHGFI_SMALLICON = 0x1; // 'Small icon
			public const uint SHGFI_USEFILEATTRIBUTES = 0x10;
			public const uint SHGFI_TYPENAME = 0x400;
			public const uint SHGFI_SYSICONINDEX = 0x4000;

			public const uint FILE_ATTRIBUTE_ARCHIVE = 0x20;
			public const uint FILE_ATTRIBUTE_DIRECTORY = 0x10;
			public const uint FILE_ATTRIBUTE_HIDDEN = 0x2;
			public const uint FILE_ATTRIBUTE_NORMAL = 0x80;
			public const uint FILE_ATTRIBUTE_READONLY = 0x1;
			public const uint FILE_ATTRIBUTE_SYSTEM = 0x4;
			public const uint FILE_ATTRIBUTE_TEMPORARY = 0x100;

			[DllImport("shell32.dll")]
			public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

			[DllImport("user32.dll", CharSet = CharSet.Auto)]
			public static extern bool DestroyIcon(IntPtr handle);
		}

//--------------------------------------------------------------------------------
		public IconManager()
		{
		}		
		
//--------------------------------------------------------------------------------
		public Image GetLargeIcon(string key)
		{
			return icons_l[GetIconIdx(key, true)];
		}

//--------------------------------------------------------------------------------
		public Image GetSmallIcon(string key)
		{
			return icons_s[GetIconIdx(key, true)];
		}

//--------------------------------------------------------------------------------
		static public Bitmap GetIcoBmp(Icon ico)
		{
			Bitmap bm = new Bitmap(ico.Size.Width, ico.Size.Height); 

			using(Graphics g = Graphics.FromImage(bm))
				g.DrawIcon(ico, 0, 0); 

			return bm; 
		}

//--------------------------------------------------------------------------------
		int GetIconIdx(string key, bool fext)
		{
			int ans = 0;

			if(String.IsNullOrWhiteSpace(key))
				key = "*";

			icon_tb result = ext.Find(a => a.key == key);

			if(result == null)
			{
				uint arg_l = 0, arg_s = 0;

				SHFILEINFO shinfo_l = new SHFILEINFO();
				SHFILEINFO shinfo_s = new SHFILEINFO();

				if(fext)
				{
					arg_l = (Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON | Win32.SHGFI_USEFILEATTRIBUTES);
					arg_s = (Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON | Win32.SHGFI_USEFILEATTRIBUTES);

				}else
				{
					arg_l = (Win32.SHGFI_ICON | Win32.SHGFI_LARGEICON);
					arg_s = (Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
				}

				Win32.SHGetFileInfo(key, 0, ref shinfo_l, (uint)Marshal.SizeOf(shinfo_l), arg_l);
				Win32.SHGetFileInfo(key, 0, ref shinfo_s, (uint)Marshal.SizeOf(shinfo_s), arg_s);

				icon_tb tmp = new icon_tb();

				using(Icon tmpIco = Icon.FromHandle(shinfo_l.hIcon).Clone() as Icon)
				{
					icons_l.Add(GetIcoBmp(tmpIco));
					Win32.DestroyIcon(shinfo_l.hIcon);
				}

				using(Icon tmpIco = Icon.FromHandle(shinfo_s.hIcon).Clone() as Icon)
				{
					icons_s.Add(GetIcoBmp(tmpIco));
					Win32.DestroyIcon(shinfo_s.hIcon);
				}

				tmp.iIcon = shinfo_l.iIcon;
				tmp.index = ImageIndex++;
				tmp.key = key;
				ext.Add(tmp);

				ans = tmp.index;
			}
			else
			{
				ans = result.index;
			}

			return ans;
		}

//--------------------------------------------------------------------------------
	}
}

