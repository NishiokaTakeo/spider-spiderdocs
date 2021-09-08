using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using lib = SpiderDocsModule.Library;

namespace SpiderDocsModule
{
	public static class Library
	{
		public const string INVALID_DATE = "  /  /";

		//Default messages------------------------------------------------------------------------------------------------------------------------------------
		public const string msg_messabox_title				= "Spider Docs";

		public const string msg_error_default				= "Sorry, an error has occurred and the system could not perform the requested operation.";

		public const string msg_error_default_update		= "Sorry, an error has occurred and the system could not be updated to the latest version";
		public const string msg_auto_update_disabled		= "The new version has been released but auto update is disabled.\n" +
															  "Please install the new version manually.";

		public const string msg_error_default_open_form		= "Sorry, an error has occurred and the system could not open this form.";
		public const string msg_error_load_local_files		= "Sorry, an error has occurred and the system could not load the files from Work Space (Local).";
		public const string msg_error_save_file				= "Sorry, an error has occurred and your file was not saved.";
        public const string msg_error_search_require        = "Please search document you want to override.";
		public const string msg_error_search				= "Sorry, an error has occurred and the system could not perform your search.";
		public const string msg_error_file_menu				= "Sorry, an error has ocurred loading the file menu.";
		public const string msg_error_getting_fileDeatils	= "Sorry, an error has ocurred loading the details for this file.";
		public const string msg_error_conection_lost		= "Connection to the server has been lost.\n\n Try connect again in few minutes.";
		public const string msg_error_conection_failed		= "Connection failed.\nIs the server running?";
		public const string msg_error_newer_version			= "Your version is newer than the version of the server.\n\n Please contact the support.";
		public const string msg_required_server_address		= "Please, enter the server address and the port number.";
		public const string msg_required_credential			= "Please, enter the user name and the password.";

		public const string msg_fatal_error					= "Spider Docs has encountered a problem and needs to close. We are sorry for the inconvenience. \n " +
															  "Please, try open the system again.";

		public const string msg_file_blocked				= "The action cannot be completed because the file is open or being used by another process. \n \n" +
															  "Please close the file and try again.";

		public const string msg_no_file_selected			= "There are no files selected";
		public const string msg_no_file_selected_NewVer		= "No file was selected on Search Grid";
		public const string msg_no_page_selected			= "There are no pages selected";
		public const string msg_not_all_ocrable				= "All files must be PDF, or images";
		public const string msg_pending_file				= "Only Pending files can be deleted";

		public const string msg_sucess_custom				= "Your profile customization has been saved";
		public const string msg_sucess_save_file			= "File successfully saved";
		public const string msg_sucess_imported_file		= "File(s) were imported successfully";
		public const string msg_sucess_deleted				= "This item has been deleted";
		public const string msg_sucess_archived				= "This file has been archived";
		public const string msg_sucess_update_review		= "This review has been updated.";
		public const string msg_date_format					= "The data you supplied must be a valid date in the format dd/mm/yyyy.";
		public const string msg_error_description			= "\n \n Error description: \n";

		public const string msg_file_opened					= "The action cannot be completed because the file is open or being used by another process. \n \nClose the file and try again.";

		public const string msg_file_save					= "Do you want to save this file to local?";
		public const string msg_file_save_db				= "Do you want to save this file to database?";

		public const string msg_start_review				= "Do you want to start this review?";
		public const string msg_finalize_review				= "Do you want to finalize this review?";
		public const string msg_passon_review				= "Do you want to pass on this review?";

		public const string msg_multiple_files_checkin		= "Checking in {0} files.";

		public const string msg_no_history					= "There is no history.";

		//Comment ---------------------------------------------------------------------------------------------------------------------------
		public const string cmt_duplication_ok 				= "Acepted to duplication.";

		//Messages for fields required ---------------------------------------------------------------------------------------------------------------------------
		//MessageBoxIcon.Error
		public const string msg_required_doc_title			= "Please, enter the document title.";
		public const string msg_required_doc_folder			= "Please, select the folder.";
        public const string msg_required_doc_save           = "Please, select the location where to save.";
        public const string msg_required_folder_name		= "Please, enter the folder name.";
        public const string msg_match_folder_name           = "Please, select the folder name from the suggestion.";
        public const string msg_required_doc_type			= "Please, select the document type.";
		public const string msg_required_name				= "Please, enter the field name.";
		public const string msg_required_type				= "Please, select the field type.";
		public const string msg_required_position			= "Please, enter the field position.";
		public const string msg_required_item				= "Please, enter the item.";
		public const string msg_required_password			= "Please, enter the password.";
		public const string msg_required_email				= "Please, enter the e-mail.";
		public const string msg_required_login				= "Please, enter the login.";
		public const string msg_required_levelPermission	= "Please, enter the level permission.";
		public const string msg_required_reason				= "Please, enter the reason.";
		public const string msg_required_location			= "Please, enter the location.";
		public const string msg_required_review_reviewer	= "Please, select reviewers.";
		public const string msg_required_review_deadline	= "Please, chose a future day for deadline.";
		public const string msg_not_all_image_files			= "Please, select only convertable files to a PDF.";

		//Register already in use ---------------------------------------------------------------------------------------------------------------------------
		public const string msg_doc_type_used				= "This Type of Document has already been used in some document.";
		public const string msg_folder_used					= "This Folder has already been used in some document.";
		public const string msg_attribute_item_used			= "This Item has already been used in some document.";
		public const string msg_permission_group_used		= "This Permission Group already has a registered user.";

		//Asking to delete ---------------------------------------------------------------------------------------------------------------------------
		public const string msg_ask_delete_record			= "Are you sure you want to delete the current record?";
		public const string msg_ask_delete_files			= "Are you sure you want to delete {0} files?";
		public const string msg_ask_delete_folder			= "Are you sure you want to delete this folder? ( This action will cancel the check out )";
        public const string msg_ask_delete_notification_group      = "Are you sure you want to delete the current group?\nThis will remove all reference documents' NG group.";

		public const string msg_ask_delete_favourite		= "This cannot redo. Do you want to clear your favourite property?";

        //Names existing ---------------------------------------------------------------------------------------------------------------------------
        public const string msg_existing_doc_type			= "There is already a Document Type with the same file name.  \n \n" +
															  "Please, choose another file name for this Document Type.";

        public const string msg_found_duplicate_docs        = "Cannot proceed because being duplications after the action by either:\n\n" +
                                                              "- duplication with Title in same folder\n" +
                                                              "- duplication with Title, Type across the folder\n\n" +
                                                              "Please, fix document first.";

        public const string msg_existing_folder				= "There is already a Folder with the same name.  \n \n" +
															  "Please, choose another name for this folder.";

		public const string msg_existing_file				= "There is already a file with the same name. \n \n" +
															  "Please, choose another name for this file.";

		public const string msg_warn_existing_file			= "Document(s) you are importing exists already with same name. \n \n" +
															  "I strongly recommend to use different name. \n\n"+
															  "Do you wish to continue? (No means cancell all)";

        public const string msg_warn_type_existing_file     = "There is/are already exsiting documents with same title in same folder or will be existed after the action.\n \n" +
                                                              "I strongly recommend to rename before this action. \n\n" +
                                                              "Do you wish to continue? (No means cancell all)";


        public const string msg_existing_login				= "There is already a user registered with this Login. \n \n" +
															  "Please, choose another Login for this user.";

        public const string msg_duplicate_title             = "A file that has same title was created previously. \n \n" +
                                                              "Please, version up the document instead.";
        public const string msg_existing_ng                 = "Cannot proceed. Reason: same notification group. \n \n" +
                                                              "Please, choose differnet group.";

        //Errors----------------------------------------------------------------------------------------------------------------------------------
        public const string msg_error_twain_driver			= "The system could not load twain driver. \n \n" +
															  "You will not be able to use the scanner.";

        public const string msg_error_scanner_ofline        = "The Scanner is offline. Please plugin";

        public const string msg_permission_file_not_checkOut= "This file is not checked out or not having permission.\n" +
															  "Only files that have been checked out can be checked in.";

		public const string msg_invalid_drop_file			= "At least one of files are already checked out by other user or Archived, Deleted, Invalid, and Readonly .";
		//public const string msg_checkOut_by_other			= "At least one of files are already checked out.";
		//public const string msg_status_no_allow				= "At least one of files are already Archived, Deleted, Invalid, and Readonly .";

		public const string msg_permissio_different_version	= "The current version of this file is different from the version of the file you are trying to save.";

		public const string msg_permissio_different_owner	= "This file has been checked out by another user.";

		public const string msg_permissio_checkIn			= "You cannot create a new version. \n" +
															  "Check your permissions in the folder to which this file belongs.";

		public const string msg_permissio_readOnly			= "You cannot create a new version. \n" +
															  "This file is read only";

		public const string msg_permissio_archived			= "You cannot create a new version. \n" + msg_sucess_archived;

		public const string msg_permissio_deleted			= "You cannot create a new version. \n" +
															  "This file has been deleted";

		public const string msg_permissio_denied			= "You do not have permission to perform this action.";

		public const string msg_permission_denied			= "Selected folder contains a folder you don't have permission to remove.";

		public const string msg_permission_review_checkOut	= "This file is beeing reviewd by other user.";

		public const string msg_permission_review_NextUser	= "The following users do not have enough permission.\n" +
															  "The users need permission to read, check out (if applicable) and review this document.\n\n";

		public const string msg_file_checkedOut				= "This file has been checked-out, you cannot create new version for it.";

		public const string msg_type_mismatch				= "You cannot create a new version. \n" +
															  "This file type dose not match the current version.";

		public const string msg_checkout_archived			= "You cannot check out this file. \n" + msg_sucess_archived;
        public const string msg_checkout_already            = "Document(s) have/has been discarded check out. \n";
		public const string msg_checkout_already2            = "Document(s) have/has been checked out. \n";
        public const string msg_footer_protected			= "You cannot add a footer to this file. \n" +
															  "This file is protected.";

		public const string msg_delete_Attribute_Doc		= "This attribute has already been used in some document.";
		public const string msg_delete_Attribute_DocType	= "This attribute is already bound to some document type.";
		public const string msg_delete_Attribute_ComboBox	= "This attribute is already bound to some combo box items.";
		public const string msg_delete_Attribute_Footter	= "This attribute is already bound to the footer.";
		public const string msg_delete_All_Items			= "Least an item should exists.";

		public const string msg_not_find_file				= "Spider Docs cannot find this document.";
		public const string msg_not_find_files				= "Spider Docs cannot find these documents.";

		public const string msg_not_find_file_ext			= msg_not_find_file + "\n" +
															  "Please save as a new file from the Save New File tab or\n" +
															  "search a document to save as a new version manually.";

		public const string msg_not_find_files_ext			= msg_not_find_files + "\n\n" +
															  "{0}\n\n" +
															  "Please save as new files from the Save New File tab or\n" +
															  "search documents to save as a new version manually.";

		public const string msg_wrong_file_name				= "This file name is invalid.";

		public const string msg_error_close_scan			= "You cannot close this window during current operation is running.";

		public const string msg_error_SpiderDoc_not_open	= "To save this document you must be logged in Spider Docs.";

        public const string msg_error_ng_all                = "Cannot proceed this for All Group.";
		public const string msg_error_no_select				= "Nothing was selected.";

		public const string msg_error_aborted				= "The action has been aborted.";

		//Email messages (Common) ---------------------------------------------------------------------------------------------------------------------------
		const string msg_mail_Body0							= "Id: {0}<br>\n" +
															  "Name: {1}<br>\n" +
															  "Folder: {2}<br>\n" +
															  "Version: {3}<br>\n";

		const string msg_mail_Body4							= "Deadline: {4}<br>\n" +
															  "Comment: {5}<br>\n";

		const string msg_mail_Body_End						= "Spider Docs";

		//Email messages (Document Available) ---------------------------------------------------------------------------------------------------------------------------
		public const string msg_mail_available_Title		= "Document '{0}' is available";
		public const string msg_mail_available_Body			= "The document below has been checked back in and is now available.<br><br>\n" +
															  msg_mail_Body0 +
															  "<br>\n" +
															  msg_mail_Body_End;

		//Email messages (Review) ---------------------------------------------------------------------------------------------------------------------------
		public const string msg_mail_review_Body			= msg_mail_Body0 + msg_mail_Body4 + "<br>\n" + msg_mail_Body_End;

		public const string msg_review_PassOn_Title			= "{0} requests that the following document be reviewed.";
		public const string msg_review_PassOn				= "Please review the document below.<br><br>\n";

		public const string msg_review_Complete_Title		= "This Review is complete.";
		public const string msg_review_Complete				= "This Review is complete.<br><br>\n";

		public const string msg_review_Remainder_Title		= "Review Remainder from {0}";
		public const string msg_review_Remainder			= "The deadline of this review is closing in!<br><br>\n";

		public const string msg_review_Overdue_Title		= "Review is overdue! from {0}";
		public const string msg_review_Overdue				= "Please review the document below ASAP!<br><br>\n";

		//Installation ---------------------------------------------------------------------------------------------------------------------------
		public const string msg_update						= "New version is found. Spdier Doc starts update.\nPlease make sure all Office application is closed and click OK.";

		// msg_close_office -> There is a same message in the following file.
		//C:\Dev\SpiderDocs\Spider Docs - App Client Installer\SpiderDocsInstaller\Program.cs
		public const string msg_close_office				= "Please close all Office application before to continue.";

        //API message------------------------------------------------------------------------------------------------------------------------------------
        public const string msg_api_err_args                = "API arguments are not correct. Please check before pass them to the API";

        public const string msg_api_err_prmt                = "You do not have permission to do this. Please check permission first";
        public const string msg_api_err_archive             = "One of the documents has been archived already.";
        public const string msg_api_err_unarchive           = "You cannot cancel archives docs.\nThis might cause by:\n\t- Permissions";
        public const string msg_api_err_prmt_invaid         = "You do not have permission to do this or it hasn't been checked out yet.";

        // Folder Inheritance message------------------------------------------------------------------------------------------------------------------------------------
        public const string msg_ihr_perform_inheritance     = "The folder \"{0}\" will be INHERITED from a parent.\n\nAre you sure to make this?";
        public const string msg_ihr_perform_non_inheritance = "The folder\" {0}\" will NOT be inherit.\n\n Are you sure to make this?";
    }
}
