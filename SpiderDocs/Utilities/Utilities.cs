using System;
using System.Windows.Forms;
using lib = SpiderDocsModule.Library;
using SpiderDocsForms;
using SpiderDocsModule;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
	class Utilities : SpiderDocsForms.Utilities
	{
//---------------------------------------------------------------------------------
		public static bool checkKeychar(int keychar)
		{
			bool handle = false;

			switch(keychar)
			{
				case 92:
				case 47:
				case 58:
				case 42:
				case 63:
				case 34:
				case 39:
				case 60:
				case 62:
				case 124:
					handle = true;
					break;
			}

			return handle;
		}

//---------------------------------------------------------------------------------
		public static int ConvDataRowId(object src)
		{
			int idx = -1;

			if(src.ToString() != "")
				idx =  Convert.ToInt32(src);

			return idx;
		}

//---------------------------------------------------------------------------------		
		//@@Mori: The following forms should use PropertyPanel user control, should not use indivdual combo boxes in each forms.
		//public static void refreshAllComboFolders()
		//{
		//	Form frmScan = new frmScan();
		//	Form frmWorkSpace = new frmWorkSpace();
		//	Form frmImportFiles = new frmImportFiles();
		//	Form frmFolderPermissions = new frmFolderPermissions();

		//	foreach(Form OpenForm in Application.OpenForms)
		//	{
		//		if(OpenForm.GetType() == frmWorkSpace.GetType())
		//			((frmWorkSpace)OpenForm).populateComboFolder();

		//		//if(OpenForm.GetType() == frmImportFiles.GetType())
		//			//((frmImportFiles)OpenForm).populateComboFolder();
  //              /*
		//		if(OpenForm.GetType() == frmScan.GetType())
		//			((frmScan)OpenForm).populateComboFolder();
  //              */
		//		if(OpenForm.GetType() == frmFolderPermissions.GetType())
		//			((frmFolderPermissions)OpenForm).populateComboFolder();
		//	}

		//	//@@Mori: Only this code should remain finally
		//	MMF.WriteData<int>(1, MMF_Items.PropertyUpdateReq);
		//}

		public static void refreshAttributes()
		{
			Form frmDocumentType = new frmDocumentType();

			foreach(Form OpenForm in Application.OpenForms)
			{
				if(OpenForm.GetType() == frmDocumentType.GetType())
					((frmDocumentType)OpenForm).populateAttributeExternal();
			}

			//@@Mori: Only this code should remain finally
			MMF.WriteData<int>(1, MMF_Items.PropertyUpdateReq);
		}

		public static void RefreshDocumentTypes()
		{
			Form frmWorkSpace = new frmWorkSpace();
			//Form frmImportFiles = new frmImportFiles();

			foreach(Form OpenForm in Application.OpenForms)
			{
				if(OpenForm.GetType() == frmWorkSpace.GetType())
					((frmWorkSpace)OpenForm).populateComboDocType();

				//if(OpenForm.GetType() == frmImportFiles.GetType())
					//((frmImportFiles)OpenForm).populateComboDocType();
			}

			//@@Mori: Only this code should remain finally
			MMF.WriteData<int>(1, MMF_Items.PropertyUpdateReq);
		}

        public static void RefreshAuthor()
        {
            Form frmWorkSpace = new frmWorkSpace();
            //Form frmImportFiles = new frmImportFiles();

            foreach (Form OpenForm in Application.OpenForms)
            {
                if (OpenForm.GetType() == frmWorkSpace.GetType())
                    ((frmWorkSpace)OpenForm).populateComboAuthor();

                //if(OpenForm.GetType() == frmImportFiles.GetType())
                //((frmImportFiles)OpenForm).populateComboDocType();
            }

            //@@Mori: Only this code should remain finally
            MMF.WriteData<int>(1, MMF_Items.PropertyUpdateReq);
        }
        //---------------------------------------------------------------------------------
        static public void LoopUntilOfficeClosed()
		{
			while(true)
			{
				if(IsOfficeProcessExists())
				{
					DialogResult ret = MessageBox.Show(
						lib.msg_close_office,
						lib.msg_messabox_title,
						MessageBoxButtons.OK, 
						MessageBoxIcon.Exclamation);
				
				}else
				{
					break;
				}
			} 
		}

//---------------------------------------------------------------------------------
	}
}
