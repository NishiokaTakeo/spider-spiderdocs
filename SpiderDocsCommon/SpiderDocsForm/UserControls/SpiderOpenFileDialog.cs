using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpiderDocsModule;

namespace SpiderDocsForms
{
	public class SpiderOpenFileDialog
	{
		OpenFileDialog dialog = new OpenFileDialog();
		public bool Multiselect;
		public bool RestoreDirectory;
		public string Filter;
		public string Title;

		public string FileName;
		public string[] FileNames;
        
        // Keep remain file dialog path until Exe is closed.
        static string current_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public SpiderOpenFileDialog()
		{
			dialog.InitialDirectory = current_path;
		}

		public DialogResult ShowDialog()
		{
			dialog.Multiselect = Multiselect;
			dialog.RestoreDirectory = RestoreDirectory;
			dialog.Filter = Filter;
			dialog.Title = Title;

			DialogResult ans = DialogResult.Cancel;

			if(dialog.ShowDialog() == DialogResult.OK)
			{
				ans = DialogResult.OK;

                current_path = Multiselect ? FileFolder.GetPath(dialog.FileNames[0]) : FileFolder.GetPath(dialog.FileName);

    //            if (Multiselect)
				//	SpiderDocsApplication.CurrentUserSettings.current_path = FileFolder.GetPath(dialog.FileNames[0]);
				//else
				//	SpiderDocsApplication.CurrentUserSettings.current_path = FileFolder.GetPath(dialog.FileName);

			}

			FileName = dialog.FileName;
			FileNames = dialog.FileNames;
			return ans;
		}
	}
}
