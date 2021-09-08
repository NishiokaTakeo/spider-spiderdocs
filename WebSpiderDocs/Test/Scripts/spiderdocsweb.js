//---------------------------------------------------------------------------------
// Example code to access to Spider Docs Web Services
// 20/04/2016 12:22 Mori
//---------------------------------------------------------------------------------
var base_url = "http://orders.contentliving.com.au/spiderdocs/";
//var base_url = "http://orders.contentliving.com.au/spiderdocs_momu/";
//var base_url = "http://ts.spiderdevelopments.com.au:8082/";
//var base_url = "http://203.153.227.179/spiderdocs/";
//var base_url = "http://192.168.1.208/spiderdocs/";
//var base_url = "http://localhost:50920/spiderdocs/";
//var base_url = "http://10.100.4.5:8082/";
//var base_url = "http://10.100.4.3:8080/";
//var base_url = "https://app.bentragerhomes.com.au/spiderdocs/";

var action_url = base_url + "External/";

var UserId = 1;

//---------------------------------------------------------------------------------
// User authentication functions.
//---------------------------------------------------------------------------------
function Login()
{
	var credential =
	{
		UserName : "administrator",
		Password_Md5 : md5("Slayer6zed")
	}

	$.ajax({
		url: base_url + 'Users/Login',
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(credential),
		success: function(Result)
		{
			// Result should be > 0 if no error
			if(Result.User.id <= 0)
			{
				alert("Error: Cannot login!");

			}else
			{
				alert("User ID: " + Result.User.id + "\n" +
					  "UserName: " + Result.User.name);
			}
		}
	});
}

function Logout()
{
	$.ajax({
		url: base_url + 'Users/Logout/' + UserId,
		type: "POST"
	});
}

//---------------------------------------------------------------------------------
// This function searches documents as per Criteria and returns document information.
//---------------------------------------------------------------------------------
function GetDocuments()
{
	// specify criteria for document search
	var Criteria =
	{
		// extensions should be fixed to image
		DocIds: [1001],
		//Extensions: new Array(".jpg", ".png", ".gif", ".bmp"),
		//DocTypeIds: new Array(1, 2), // specify document type ids which you canget from GetDocumentTypes()
		//Titles: ['Addenda_15497_rpt_s15%.pdf', 'Addenda_10000_rpt_s15%.pdf'],
		//FolderIds: [3]
	    ExcludeStatuses: [4,5]

		// specify job number
		//AttributeCriterias: {
		//	Attributes:	[{
		//			Values: {
		//				id: 125, // can be fixed 3 (Job No)
		//				atbValue: "15497" // job number (00007 is a test job number)
		//			},
		//			UseWildCard: false
		//	}]
		//}
	}

	$.ajax({
		url: action_url + 'GetDocuments/' + UserId,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(Criteria), 
		success: function(Result)
		{
			// returns multiple document infomation
			$.each(Result.Documents, function(index, value)
			{
				var JobNo;
				var Sheet;
				var PhotoType;

				for(var i = 0; i < value.Attrs.length; i++)
				{
					switch(value.Attrs[i].id)
					{
					case 3:
						JobNo = value.Attrs[i].atbValueForUI;
						break;
					case 10:
						Sheet = value.Attrs[i].atbValueForUI;
						break;
					case 18:
						PhotoType = value.Attrs[i].atbValueForUI;
						break;
					}
				}

				alert("Document ID: " + value.id + "\n" +
					  "Version ID: " + value.id_version + "\n" +
					  "Title: " + value.title + "\n" +
					  "JobNo: " + JobNo + "\n" + 
					  "Sheet: " + Sheet + "\n" + 
					  "Photo Type: " + PhotoType);
			});
		}
	});
}

//---------------------------------------------------------------------------------
// This function returns URLs of requested documents.
// Users can download the documents from the URLs.
//---------------------------------------------------------------------------------
function Export()
{
	$.ajax({
		url: action_url + 'Export/' + UserId,
		type: "POST",
		data: { 'VersionIds': [ 4040 ] }, // put document VERSION ID (NOT DOCUMENT ID) which you got from GetDocuments()
		traditional: true,
		success: function(Result)
		{
			// returns
			$.each(Result.Urls, function(index, value)
			{
				alert(value);
			});
		}
	});
}

//---------------------------------------------------------------------------------
// This function uploads a file and then adds or updates a specific document with the uploaded file.
//---------------------------------------------------------------------------------
function UploadFile()
{
	var input = $("#txtUploadFile").get(0);
	
	if(input.files.length > 0)
	{
		var formData = new FormData();
		for(var x = 0; x < input.files.length; x++)
		{
			formData.append("file" + x, input.files[x]);
		}
		
		// It uploads the file at first and get TempId which corresponds to the uploaded file.
		$.ajax({
			url: action_url + 'UploadFile/' + UserId,
			type: "POST",
			cache : false,
			contentType : false,
			processData : false,
			data: formData,
			success: function(TempId)
			{
				// call import function once upload is finished
				Import(TempId, $("#txtUploadFile").val().split('\\').pop());
			}
		});
	}
}

//---------------------------------------------------------------------------------
// This function downloads a file from url.
//---------------------------------------------------------------------------------
function UploadRemoteFile() {
	var url = $("#urlUploadRemoteFile").val();

	var GetPrameterStartIdx = url.lastIndexOf('?');
	if (0 < GetPrameterStartIdx)
		var filename = url.substring(url.lastIndexOf('/') + 1, GetPrameterStartIdx);
	else
		var filename = url.substring(url.lastIndexOf('/') + 1);

	var data = {strURL: url}

	$.ajax({
		url: action_url + 'UploadRemoteFile/' + UserId,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(data),
		success: function (TempId) {
			Import(TempId, filename);
		}
	});
}


//---------------------------------------------------------------------------------
// Save a document which corresponds to given temp id.
//---------------------------------------------------------------------------------
function Import(TempId, filename)
{
	var data =
	{
		// give TempId always
		TempId : TempId,

		// to add new document, you need to give the following parameters to Document object.
		Document : {
			id_docType: 49,
			id_folder: 2, // enter selected folder id which you can get from GetFolders().

			title: "Job1000.txt",//filename,
			
			Attrs:
			[
				{
					id: 3,
					atbValue: "10000"
				},
				{
				    id: 4,
				    atbValue: 41
				}

			]
		}
	}

	$.ajax({
		url: action_url + 'Import/' + UserId,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(data),
		success: function(Result)
		{
			// Result should be blank if no error
			if(Result != "")
			{
				alert("Error: " + Result);
			}
		}
	});
}


//---------------------------------------------------------------------------------
// Save a document which corresponds to given temp id.
//---------------------------------------------------------------------------------
function SaveDoc(TempId, document) {
    var url = $("#urlUploadRemoteFile").val();

    var GetPrameterStartIdx = url.lastIndexOf('?');
    if (0 < GetPrameterStartIdx)
        var filename = url.substring(url.lastIndexOf('/') + 1, GetPrameterStartIdx);
    else
        var filename = url.substring(url.lastIndexOf('/') + 1);

    var data = { strURL: url }

    $.ajax({
        url: action_url + 'UploadRemoteFile/' + UserId,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        success: function (TempId) {

            var data =
                {
                    // give TempId always
                    TempId: TempId,

                    // to add new document, you need to give the following parameters to Document object.
                    Document: {
                        id_docType: 49,
                        id_folder: 2, // enter selected folder id which you can get from GetFolders().

                        title: filename,

                        Attrs:
                            [
                                {
                                    id: 3,
                                    atbValue: "10000"
                                },
                                {
                                    id: 4,
                                    atbValue: 41
                                }

                            ]
                    }
                }

            var client = new SpiderDocsClient(SpiderDocsConf);
            debugger
            client.SaveDoc(TempId, data.Document)
                .then(function (user) {

                    debugger;

                    console.log(user);
                });        }
    });


}


//---------------------------------------------------------------------------------
function Delete()
{
    debugger;

    var client = new SpiderDocsClient(SpiderDocsConf);

    client.Delete([2259], "Test" )

        .then(function (ans) {

            debugger;
        });


    return;

	// put document DOCUMENT ID which you got from GetDocuments()
	var DocumentIds = new Array(); 
	
	$.ajax({
		url: action_url + 'Delete/' + UserId,
		type: "POST",
		data:
		{
			'DocumentIds': [ 28400 ],
			'Reason': "Test"
		},
		traditional: true,
		success: function(Result)
		{
			// Result should be blank if no error
			if(Result != "")
			{
				alert("Error: " + Result);
			}
		}
	});
}

function CheckOut()
{    
    var client = new SpiderDocsClient(SpiderDocsConf);
    
    client.CheckOut([2296, 2293], [102,103])

        .then(function (ans) {

            debugger;
        });

}


function CancelCheckOut()
{
    var client = new SpiderDocsClient(SpiderDocsConf);

    client.CancelCheckOut([2306])

        .then(function (ans) {

            debugger;
        });
}

//---------------------------------------------------------------------------------
function UpdateProperty_GetDocument()
{
	var Criteria =
	{
		DocIds: [ $("#txtUpdatePropertyDocId").val() ]
	}

	$.ajax({
		url: action_url + 'GetDocuments/' + UserId,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(Criteria), 
		success: function(Result)
		{
			$.each(Result.Documents, function(index, value)
			{
				UpdateProperty(value)
			});
		}
	});
}

function UpdateProperty(doc)
{
    
	doc.title = "updated_" + doc.title;

	var args =
	{
		Document: doc
	}

	$.ajax({
		url: action_url + 'UpdateProperty/' + UserId,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(args), 
		success: function(Result)
		{
			// Result should be blank if no error
			if(Result != "")
			{
				alert("Error: " + Result);
			}
		}
	});
}

//---------------------------------------------------------------------------------
// This function returns all folders in a system.
//---------------------------------------------------------------------------------
function GetFolders()
{
	$.ajax({
		url: action_url + 'GetFolders/' + UserId,
		type: "POST",
		success: function(Result)
		{
			$.each(Result.Folders, function(index, value)
			{
				alert("ID: " + value.id + ", Name: " + value.document_folder);
			});
		}
	});
}


//---------------------------------------------------------------------------------
// This function returns all folders in a system.
//---------------------------------------------------------------------------------
function SaveFolder() {

    var client = new SpiderDocsClient(SpiderDocsConf);

    client.SaveFolder({

        id:0,
		document_folder:"test99",
		
        id_parent: 0

    })
    .then(function (user) {

        debugger;

        console.log(user);
    });
}



//---------------------------------------------------------------------------------
// This function returns all document types in a system.
//---------------------------------------------------------------------------------
function GetDocumentTypes()
{
	$.ajax({
		url: action_url + 'GetDocumentTypes/' + UserId,
		type: "POST",
		success: function(Result)
		{
			$.each(Result.DocumentTypes, function(index, value)
			{
				alert(value.id + ", " + value.type);
			});
		}
	});
}

//---------------------------------------------------------------------------------
// This function returns all attributes in a system. (not needed now)
//---------------------------------------------------------------------------------
function GetAttributes()
{
	$.ajax({
		url: action_url + 'GetAttributes/' + UserId,
		type: "POST",
		success: function(Result)
		{
			$.each(Result.Attributes, function(index, value)
			{
				alert(value.id + ", " + value.name);
			});
		}
	});
}

//---------------------------------------------------------------------------------
// This function returns combobox items for specified attribute id.
//---------------------------------------------------------------------------------
function GetAttributeComboboxItems()
{
	//$.ajax({
	//	url: action_url + 'GetAttributeComboboxItems/' + UserId,
	//	type: "POST",
	//	data: { AttributeId: 18 }, // 18 is Photo Type
	//	success: function(Result)
	//	{
	//		$.each(Result.ComboboxItems, function(index, value)
	//		{
	//			alert(value.id + ", " + value.text);
	//		});
 //       }

 //   });

    var client = new SpiderDocsClient(SpiderDocsConf);

    client.GetAttributeComboboxItems(4)
        .then(function (user) {
            alert(user);
            debugger;

            console.log(user);
        });

}

function GetAttributeComboboxItemByItemId()
{
	$.ajax({
		url: action_url + 'GetAttributeComboboxItems/' + UserId,
		type: "POST",
		data: { ItemId: 46 },
		success: function(Result)
		{
			var val = "";

			val += "ID: " + Result.ComboboxItems[0].id + "\n";
			val += "Text: " + Result.ComboboxItems[0].text + "\n";
			val += "Children:\n";

			$.each(Result.ComboboxItems[0].children, function(index, value)
			{
				val += value.atbValueForUI + "\n";
			});
			
			alert(val);
		}
	});
}

//---------------------------------------------------------------------------------
// This function adds a combobox item for specified attribute id.
// It does not add new item if same text exists for the specified attribute already.
//---------------------------------------------------------------------------------
function EditAttributeComboboxItem()
{
	var input = $("#txtEditAttributeComboboxItem").val();

	var data =
	{
		AttributeId: 3, // Student ID attribute
		
		Item: {
			text: input,

			children:
			[
				{
					id: 9, // Student Name attribute which is children of Student ID attribute
					atbValue: "Mori" // Student Name

				}/*,
				{
					id: 7,
					atbValue: "Mori"
				}*/
			]
		}
	}

	$.ajax({
		url: action_url + 'EditAttributeComboboxItem/' + UserId,
		type: "POST",
		contentType: "application/json; charset=utf-8",
		data: JSON.stringify(data),
		success: function(Result)
		{
			alert("Combobox Id is " + Result);
		}
	});
}



//---------------------------------------------------------------------------------
// Get User Information. 
// username and password must be same as SpiderDocsConf
//---------------------------------------------------------------------------------
function GetUser() {

	var client = new SpiderDocsClient(SpiderDocsConf);

	client.GetUser('administrator', 'Welcome1')
	
    .then(function (user) {
        debugger;
        console.log(user);
    });
}


//---------------------------------------------------------------------------------
// Update User information
// username and password must be same as SpiderDocsConf
//---------------------------------------------------------------------------------
function UpdateUser() {

	var client = new SpiderDocsClient(SpiderDocsConf);

	client.GetUser('administrator', 'Welcome1')
	
    .then(function (user) {
        user.name = 'Hyper!'
		debugger;
        return client.UpdateUser(user, 'administrator', 'Welcome1');
    })
    .then(function (user) {
        debugger;
    });
}

//---------------------------------------------------------------------------------
// Get Histories
// username and password must be same as SpiderDocsConf
//---------------------------------------------------------------------------------
function GetHistories() {
    var id_doc = $("#txtGetHistoryID").val();
	
		var client = new SpiderDocsClient(SpiderDocsConf);
	
		var criteria = {
		    DocIds: id_doc == "" ? [] : [id_doc]
		};

		client.GetHistories(criteria)
		
		.then(function (histries) {
			
			debugger;
		});
}
//---------------------------------------------------------------------------------
// GetAttributeDocType
//---------------------------------------------------------------------------------
function GetAttributeDocType() {
    debugger;

    var id_doc = $("#txtGetAttributeDocType").val();

    var client = new SpiderDocsClient(SpiderDocsConf);


    client.GetAttributeDocType(id_doc)

        .then(function (histries) {

            debugger;
        });
}

//---------------------------------------------------------------------------------
// Upload File and Import
// 
//---------------------------------------------------------------------------------
function RemoteImport() {
    var url = $('#urlUploadRemoteFile').val();
        
    var document = {
            id_docType: 49,
            id_folder: 2, // enter selected folder id which you can get from GetFolders().

            title: "Job1000.txt",
			
            Attrs:
            [
                {
                    id: 3,
                    atbValue: "10000",
                },
                {
                    id: 4,
                    atbValue: 41
                }/*,
                {
                    id: 5,
                    atbValue: "I LOVE TEXT!"
                }*/
            ],

            reason : "test"
    };
    var combo = [];
    /*
    var combo = [
        {
            id_atb: 6,
            text:"Nishioka",
            children: [
                    { atbValue: "Takeo5" ,id:9}
            ]
        },
        {
            id_atb: 6,
            text: "Binary",
            children: [
                    { atbValue: "LOVE at 2", id: 9 }
            ]
        }
    ]
    */
    var client = new SpiderDocsClient(SpiderDocsConf);

    client.RemoteImport(url, document, combo)

	.then(function (histries) {

		debugger;
	});
}


//---------------------------------------------------------------------------------
// Upload File and Import
// 
//---------------------------------------------------------------------------------
function ToPdf() {
    var versionids = $('#txtMerges').val().split(',');

    var document = {
        id_docType: 1,
        id_folder: 2, // enter selected folder id which you can get from GetFolders().

        title: "GTO.pdf",

        Attrs:
        [
            {
                id: 6,
                atbValue: 3,
            },
            {
                id: 6,
                atbValue: 2
            }
        ]
    };
    
    var client = new SpiderDocsClient(SpiderDocsConf);

    client.ToPdf(versionids, document)

	.then(function (histries) {

	    debugger;
	});
}

function _GetFolders() {
	var client = new SpiderDocsClient(SpiderDocsConf);

	client.GetFolders()

		.then(function (folders) {

			debugger;
		});
}


function GetFoldersL1() {
	var client = new SpiderDocsClient(SpiderDocsConf);

	client.GetFoldersL1(0)

		.then(function (folders) {

			debugger;
		});
}
