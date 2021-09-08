using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//using PresentationControls;
using SpiderDocsForms;
using SpiderDocsModule;
using Document = SpiderDocsForms.Document;
using lib = SpiderDocsModule.Library;
using SpiderCustomComponent;

//---------------------------------------------------------------------------------
namespace SpiderDocs
{
//---------------------------------------------------------------------------------
	public partial class frmReview : Spider.Forms.FormBase
    {
//---------------------------------------------------------------------------------
		enum en_Mode
		{
			New = 0,
			Review,
			Rev_Reference,
			Reference,
			CheckOut,

			Max
		}

//---------------------------------------------------------------------------------
		en_Mode mode;	// form appearance mode
		Document doc;
		bool UpdateGrid = false;

		Review CurrentReview;

//---------------------------------------------------------------------------------
		public frmReview(int doc_id)
		{ 
			InitializeComponent();

			DialogResult = System.Windows.Forms.DialogResult.Cancel;

			LoadUser();
			doc = DocumentController.GetDocument(doc_id);
		}

//---------------------------------------------------------------------------------
		private void frmReview_Load(object sender, EventArgs e)
		{
			CurrentReview = ReviewController.GetReview(doc.id);

			if((CurrentReview == null) || (CurrentReview.status == en_ReviewStaus.Reviewed))
				CurrentReview = new Review(doc.id);
				
			dtgReview.AutoGenerateColumns = false;

			ChangeFormMode();
			PopulateControls();
			ChangeControls();

			int start_x = lblDeadlineSet.Left + lblDeadlineSet.Width + half_font_size;
			cboUser.Left = start_x;
			dtDeadline.Left = start_x;
			dtDeadlineTime.Left = dtDeadline.Left + dtDeadline.Width + half_font_size;
			ckAllowCheckOut.Left = start_x;
		}

//---------------------------------------------------------------------------------
		private void frmReview_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(UpdateGrid)
				MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.GridUpdateCount);

			MMF.WriteData<uint>(Utilities.GetTickCount(), MMF_Items.WorkSpaceUpdateCount);
		}

//---------------------------------------------------------------------------------
		void LoadUser()
		{ 
            /*
			List<User> users = UserController.GetUser(true, true, true);
			users = users.Where(a => a.id != SpiderDocsApplication.CurrentUserId).ToList();

			cboUser.DataSource = new ListSelectionWrapper<User>(users, "name", ",");
			//cboUser.DisplayMemberSingleItem = "Name";
			//cboUser.DisplayMember = "NameConcatenated";
			cboUser.ValueMember = "Selected";
			cboUser.DropDownControl.MinimumSize = new System.Drawing.Size(cboUser.Width, (int)Font.GetHeight() * 15);
            */
            UserController.GetUser(true, false).ForEach(u =>
            {
                cboUser.AddItem(new DocumentAttributeCombo()
                {
                    id = u.id,
                    //id_atb { set; get; }
                    text = u.name,
                    Selected = false
                }, false);
            });
            cboUser.DropDownControl.MinimumSize = new System.Drawing.Size(cboUser.Width, (int)Font.GetHeight() * 15);
		}

//---------------------------------------------------------------------------------
		void ChangeFormMode()
		{
			if(doc.id_status == en_file_Status.archived)
			{
				mode = en_Mode.Reference;
				return;
			}
			
			if(doc.id_status == en_file_Status.checked_out)
			{
				mode = en_Mode.CheckOut;

			}else if(CurrentReview.status == en_ReviewStaus.UnReviewed)
			{
				// check if the user who opening this form is reviewer or not
				ReviewUser review_user = CurrentReview.review_users.FirstOrDefault(a => a.id_user == SpiderDocsApplication.CurrentUserId);

				if((review_user != null) && (review_user.action == en_ReviewAction.UnReviewed))
				{
					// if so, change the form mode to review
					mode = en_Mode.Review;

				}else
				{
					mode = en_Mode.Rev_Reference;
				}

			}else
			{
				mode = en_Mode.New;
			}
		}

//---------------------------------------------------------------------------------
		void PopulateControls()
		{
			List<Review> ReviewHistory = ReviewController.GetReview(false, false, doc.id);
			
			List<int> user_ids = new List<int>();
			foreach(Review review in ReviewHistory)
			{
				user_ids.Add(review.owner_id);
				user_ids.AddRange(review.review_users.Select(a => a.id_user).ToList());
			}

			List<User> Users = UserController.GetUser(false, false, user_ids.Distinct().ToArray());
			
			int serial = 0;
			foreach(Review review in ReviewHistory)
			{
				serial++;
				dtgReview.Rows.Add(serial, Users.Find(a => a.id == review.owner_id).name, "Start Review", review.owner_comment);

				foreach(ReviewUser wrk in review.review_users)
				{
					serial++;

					string note = "";
					switch(wrk.action)
					{
					case en_ReviewAction.UnReviewed:
						note = "-";
						break;

					case en_ReviewAction.Start:
						note = "Start Review";
						break;
						
					case en_ReviewAction.PassOn:
						note = "Pass On";
						break;

					case en_ReviewAction.Finalize:
						note = "Finalize Review";
						break;
					}

					dtgReview.Rows.Add(serial, Users.Find(a => a.id == wrk.id_user).name, note, wrk.comment);
				}
			}

			lblDocId.Text = "ID: " + doc.id.ToString();
			lblFileName.Text = "Name: " + doc.title;
			lblFolder.Text = "Folder: " + doc.name_folder;
			lblFileVer.Text = "Version: " + doc.version.ToString();

			User owner = UserController.GetUser(true, CurrentReview.owner_id);
			if(owner != null)
			{
				lblComment.Text = "Comment from "+ owner.name + ": ";
				txtOwnerComment.Text = CurrentReview.owner_comment;
			}

			ReviewUser review_user = CurrentReview.review_users.FirstOrDefault(a => a.id_user == SpiderDocsApplication.CurrentUserId);
			if((review_user != null) && (review_user.action != en_ReviewAction.UnReviewed))
				txtComment.Text = review_user.comment;

			ckAllowCheckOut.Checked = CurrentReview.allow_checkout;
		}

//---------------------------------------------------------------------------------
		void ChangeControls()
		{
			// Header
			if(doc.id_sp_status == en_file_Sp_Status.review_overdue)
				lblStatus.Visible = true;		

			switch(mode)
			{
			case en_Mode.New:
				this.Text = "Start New Review";

				gbAction.Enabled = true;
				rbFinish.Enabled = false;
				rbStartReview.Checked = true;
				dtDeadline.Value = DateTime.Today;
				dtDeadlineTime.Value = DateTime.Now;
				txtComment.Enabled = true;

				lblComment.Visible = false;
				txtOwnerComment.Visible = false;

				btnOK.Enabled = true;
				btnCheckout.Enabled = doc.IsActionAllowed(en_Actions.CheckIn_Out);

				break;

			case en_Mode.Review:
				gbAction.Enabled = true;
				rbStartReview.Enabled = false;
				plStartReview.Enabled = false;
				txtComment.Enabled = true;					

				dtDeadline.Value = CurrentReview.deadline;
				dtDeadlineTime.Value = CurrentReview.deadline;

				btnOK.Enabled = true;

				btnCheckout.Enabled = doc.IsActionAllowed(en_Actions.CheckIn_Out);

				break;

			case en_Mode.CheckOut:
				lblStatus.Text = "Check Out";
				lblStatus.Visible = true;

				lblComment.Visible = (mode != en_Mode.New);
				txtOwnerComment.Visible = (mode != en_Mode.New);

				break;

			case en_Mode.Reference:
				txtOwnerComment.Visible = false;
				lblComment.Visible = false;

				break;

			case en_Mode.Rev_Reference:
				dtDeadline.Value = CurrentReview.deadline;
				dtDeadlineTime.Value = CurrentReview.deadline;

				break;
			}
		}

//---------------------------------------------------------------------------------
		private void rbPassOn_CheckedChanged(object sender,EventArgs e)
		{
			plStartReview.Enabled = rbStartReview.Checked;
		}

//---------------------------------------------------------------------------------
		private void dtgReview_SelectionChanged(object sender,EventArgs e)
		{
			if(0 < dtgReview.SelectedRows.Count)
				txtReviewComment.Text = dtgReview.SelectedRows[0].Cells["dtgReviewComment"].Value.ToString();
		}

//---------------------------------------------------------------------------------
		private void btnOpen_Click(object sender,EventArgs e)
		{
			if(!doc.Open())
				MessageBox.Show(lib.msg_permissio_denied, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
		}

//---------------------------------------------------------------------------------
		private void btnCheckout_Click(object sender,EventArgs e)
		{
			if(doc.CheckOut(true, false))
			{
				UpdateGrid = true;
				Close();
			}
		}

//---------------------------------------------------------------------------------
		private void btnOK_Click(object sender,EventArgs e)
		{
			bool update = false;

			if(rbFinish.Checked)
			{
                
				DialogResult ans = MessageBox.Show(lib.msg_finalize_review, lib.msg_messabox_title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

				if(ans == System.Windows.Forms.DialogResult.OK)
				{
					ReviewUser review_user = CurrentReview.review_users.Find(a => a.id_user == SpiderDocsApplication.CurrentUserId);
					review_user.FinalizeReview(doc.id_version, txtComment.Text);

					if(CurrentReview.IsAllUsersFinalized())
						CurrentReview.FinalizeReview();
									
					update = true;
				}

			}else if(rbStartReview.Checked)
			{
				bool ans = false;

				CurrentReview.owner_id = SpiderDocsApplication.CurrentUserId;
				CurrentReview.id_version = doc.id_version;
				CurrentReview.allow_checkout = ckAllowCheckOut.Checked;

				en_file_Sp_Status backup = doc.id_sp_status;
				doc.id_sp_status = en_file_Sp_Status.normal; // For check permission, this shouldn't be 'review'

                List<int> SelectedUserIds = ((CheckComboBox)cboUser).getComboValue<DocumentAttributeCombo>().Select(a => a.id).ToList();

				if(SelectedUserIds.Count <= 0)
				{
					MessageBox.Show(lib.msg_required_review_reviewer, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

				}else if((dtDeadline.Value < DateTime.Today)
					  || ((dtDeadline.Value == DateTime.Today)
					  &&  (dtDeadlineTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay)))
				{
					MessageBox.Show(lib.msg_required_review_deadline, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

				}else
				{
					List<string> error_user_names = new List<string>();
					ans = true;

					foreach(int id in SelectedUserIds)
					{
						if((!doc.IsActionAllowed(en_Actions.OpenRead, id)) ||
						   (CurrentReview.allow_checkout && !doc.IsActionAllowed(en_Actions.CheckIn_Out, id)) ||
						   (!doc.IsActionAllowed(en_Actions.Review, id)))
						{
							ans = false;
							error_user_names.Add(UserController.GetUser(false, id).name);
						}
					}

					if(!ans)
						MessageBox.Show(lib.msg_permission_review_NextUser + String.Join(", ", error_user_names), lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				}

				doc.id_sp_status = backup; // restore

				if(ans)
					update = StartReview(SelectedUserIds);
			} 
				
			if(update)
			{
				MessageBox.Show(lib.msg_sucess_update_review, lib.msg_messabox_title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
				DialogResult = System.Windows.Forms.DialogResult.OK;
				UpdateGrid = true;
				Close();
			}
		}

//---------------------------------------------------------------------------------
		bool StartReview(List<int> SelectedUserIds)
		{
			DialogResult ans;
			bool update = false;

			ans = MessageBox.Show(lib.msg_start_review, lib.msg_messabox_title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
			if(ans == System.Windows.Forms.DialogResult.OK)
			{
				CurrentReview.owner_comment = txtComment.Text;
				CurrentReview.StartReview(SelectedUserIds, GetDeadLine());
					
				update = true;
			}

			return update;
		}

//---------------------------------------------------------------------------------
		DateTime GetDeadLine()
		{
			DateTime deadline = new DateTime(dtDeadline.Value.Year,
											 dtDeadline.Value.Month,
											 dtDeadline.Value.Day,
											 dtDeadlineTime.Value.Hour,
											 dtDeadlineTime.Value.Minute,
											 dtDeadlineTime.Value.Second);

			return deadline;
		}

//---------------------------------------------------------------------------------
	}
}
