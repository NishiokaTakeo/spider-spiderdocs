using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using SpiderDocsModule;
using Spider.Net;

//-----------------------------------------------------------
namespace SpiderDocsForms
{
//-----------------------------------------------------------
	public class DmsFile : SpiderDocsForms.DmsFile<Document>
	{
	}

	public class DmsFile<T> : SpiderDocsModule.DmsFile<T> where T : Document
	{
//---------------------------------------------------------------------------------
		new public static List<string> SaveDmsFile(T doc, string path)
		{
			if(String.IsNullOrEmpty(path))
				path = ShowPathSelectDialog(GetDmsFileName(doc));

			return SpiderDocsModule.DmsFile<T>.SaveDmsFile(doc, path);
		}

//---------------------------------------------------------------------------------
		new public static List<string> SaveDmsFile(List<T> docs, string path)
		{
			if(String.IsNullOrEmpty(path))
				path = ShowPathSelectDialog("");

			return SpiderDocsModule.DmsFile<T>.SaveDmsFile(docs, path);
		}

//---------------------------------------------------------------------------------
		static string ShowPathSelectDialog(string filename)
		{
			string path = "";

			if(String.IsNullOrEmpty(filename))
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

				folderBrowserDialog.Description = Library.msg_required_doc_folder;
				folderBrowserDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

				if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
					path = folderBrowserDialog.SelectedPath + "\\";

			}else
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();

				saveFileDialog.Filter = "DMS File|*.dms";
				saveFileDialog.Title = "Save a DMS File";
				saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				saveFileDialog.FileName = filename;
				saveFileDialog.ShowDialog();

				if(saveFileDialog.FileName != "")
					path = Path.GetFullPath(saveFileDialog.FileName).Replace(Path.GetFileName(saveFileDialog.FileName), "");
			}

			return path;
		}

//---------------------------------------------------------------------------------
		public static void MailDmsFile(List<T> docs, string subject, string body)
		{
			List<string> filePath = MakeDmsFile(docs, FileFolder.GetTempFolder());

			if((subject == "") && (filePath.Count == 1))
				subject = Path.GetFileName(filePath[0]);

			new Email().OpenNewEmail(subject, body, filePath);
		}

//---------------------------------------------------------------------------------
	}
}
