app.constant 'en_ReviewStaus',
	UnReviewed : 0
	Reviewed : 1
	NotInReview: 2

app.constant 'en_file_Sp_Status',
	normal: 1
	review: 2
	review_overdue: 3

app.constant 'en_ReviewAction',
	UnReviewed: 0
	Start: 1
	PassOn: 2
	Finalize: 3

app.constant 'en_file_Status',
	invalid: -1
	checked_in: 1
	checked_out: 2
	readOnly: 3
	archived: 4
	deleted: 5
app.constant 'en_Actions',
	None: 0
	CheckIn_Out: 1
	OpenRead: 2
	Export: 3
	SendByEmail: 4
	Properties: 5
	Rollback: 6
	Delete: 7
	Archive: 8
	CancelCheckOut: 9
	Review: 10
	UnArchive: 11
	ImportNewVer: 1 * 100
	Export_PDF: 3 * 100
	SendByEmail_PDF: 4 * 100

app.constant 'en_Events',
	Created: 0
	Import: 1
	Scan: 2
	NewVer: 3
	Chkin: 4
	Chkout: 5
	Read: 6
	Cancel: 7
	Email: 8
	Exp: 9
	Outlook: 10
	Login: 11
	Property: 12
	Rollback: 13
	Archive: 14
	Copy: 15
	SaveNewVer: 16
	ChgAttr: 17
	ChgDT: 18
	ChgName: 19
	ChgFolder: 20
	StartReview: 21
	FinalizeReview: 22
	PassOnReview: 23
	UpVer: 24
	DupOK: 25
	CanceledArchive: 26

app.constant 'en_FolderPermission',
	Deny: -1
	NoSetting: 0
	Allow: 1
	Both: 2 #// Both should be treated same as Deny.

app.constant 'en_DoubleClickBehavior',
	OpenToRead:  0
	CheckOut: 1
	CheckOutFooter: 2 
