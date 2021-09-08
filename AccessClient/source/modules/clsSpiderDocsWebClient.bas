Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
' Version 2.0.0
' This Class is based on SpiderDocsWebClient C# class. this is just replaced to VBA
'
' ********************* Prerequisite *********************
' Reference
'       Microsoft Script Reference
'       Microsoft Script Runtime
'       Microsoft WinHTTP Services
'       Microsoft VBScript Regular Expressions 5.5
'       Microsoft ActiveX Data Objects x.x Library
' Import
'       JsonConverter: https://github.com/VBA-tools/VBA-JSON
'       bashMD5 : http://www.di-mgt.com.au/crypto.html#MD5


Option Compare Database

Private m_download_folder As String
Private m_cookie As String
Private m_downloaded_files() As String
Private HTTPClient As Object
Private LoginedUser As Object
Private m_server_url As String
Private m_loginid As String
Private m_password As String
Private m_useAsync As Boolean

Public Property Get ServerUrl() As String
    ServerUrl = m_server_url
End Property

Public Property Get DownloadFolder() As String
    DownloadFolder = m_download_folder
End Property

Public Property Let DownloadFolder(v As String)
    m_download_folder = v
End Property

Public Property Get DownloadedFiles() As String()
    DownloadedFiles = m_downloaded_files
End Property

Public Property Let useAsync(yes As Boolean)
    m_useAsync = yes
End Property

Public Function AddDownloadedFile(v As String)
    Dim i As Integer: i = 0
    On Error GoTo ErrorHandler
    i = UBound(m_downloaded_files, 1) + 1
    ReDim Preserve m_downloaded_files(i) As String
    m_downloaded_files(i) = v
    
    Exit Function
    
ErrorHandler:
    ReDim Preserve m_downloaded_files(0)
    Resume Next
    
End Function



Private Sub Class_Initialize()
    m_useAsync = False
    'm_useAsync = False
    m_cookie = ""
    
    Set LoginedUser = JsonConverter.ParseJson("{}")
    Set HTTPClient = New WinHttp.WinHttpRequest
    
    ' Login to API
    'If LoginedUser("id") = "" Then
    '    Call Login
    'End If

    'm_cookie = ""
    'm_download_folder = "C:/SpiderDocsDownloadedFiles/" & Format(Date, "yyyymmdd")
    
    'Set LoginedUser = JsonConverter.ParseJson("{}")
    'Set HTTPClient = New WinHttp.WinHttpRequest
    
    'm_download_folder = Replace(m_download_folder, "/", "\")
    
    ' Create download folder
    'Call MakeDirectory(m_download_folder)
    
    ' Login to API
    'If LoginedUser("id") = "" Then
    '    Call Login
    'End If
    
End Sub

Private Property Get loginid() As String
    loginid = m_loginid
End Property

Private Property Get password() As String
    password = m_password
End Property

Private Property Get useAsync() As Boolean
    useAsync = m_useAsync
End Property


' ========================================
'   Public function
' ========================================
'
Public Sub Initialize(approot As String, loginid As String, password As String, tempfolder As String)
    ' Set this instance's members
    m_server_url = approot
    m_loginid = loginid
    m_password = password
    m_download_folder = Replace(tempfolder, "/", "\")
    
    ' Create download folder
    Call MakeDirectory(m_download_folder)
    
        ' Login to API
    If LoginedUser("id") = "" Then
        Call Login
    End If

End Sub

Public Sub EnableNonAuthenticationMode(id4admin As String)
    LoginedUser("id") = id4admin
End Sub

Public Function IsLogined() As Boolean
    
    If LoginedUser("id") = "" Then
        IsLogined = False
        Else
        IsLogined = True
    End If
    
End Function

Public Sub Login()
    
    Dim body As String, url As String
    Dim Credential As Object
    Dim response As String
    Dim main_res_json As String

    ' Set Login information
    Set Credential = JsonConverter.ParseJson("{}")
    Credential("UserName") = loginid '"administrator"
    Credential("Password_Md5") = MD5_string(password) 'MD5_string("Welcome1")
    
    body = JsonConverter.ConvertToJson(Credential)
        
    'url = ServerUrl & "/spiderdocs/Users/Login"
    url = ServerUrl & "/Users/Login"

    HTTPClient.Open "POST", url, useAsync
    HTTPClient.SetRequestHeader "Content-Type", "application/json"

    response = Execute(HTTPClient, body)

    main_res_json = RegexReplace(response, "^{""User"":(.*)}$", "$1")
    Set LoginedUser = JsonConverter.ParseJson(main_res_json)

End Sub

Public Function GetDocuments(Criteria As Variant) As Variant
On Error GoTo ErrCatcher

    Dim body As String
    Dim response As String
    Dim main_res_json As String
    'Dim json As Object: Set json = JsonConverter.ParseJson("{}")
    Dim parsed As Object

    HTTPClient.Open "POST", requestPath("GetDocuments"), useAsync
    HTTPClient.SetRequestHeader "Content-Type", "application/json; charset=utf-8"

    body = JsonConverter.ConvertToJson(Criteria)

    response = Execute(HTTPClient, body)

    main_res_json = RegexReplace(response, "^{""Documents"":(.*)}$", "$1")
    Set parsed = JsonConverter.ParseJson(main_res_json)

    Set GetDocuments = parsed
    
ErrCatcher:
     
    Resume Next
    
End Function

Public Function GetDownloadUrls(ids As Variant) As Variant
    Dim body As String
    Dim response As String
    Dim main_res_json As String
    Dim json As Object: Set json = JsonConverter.ParseJson("{}")
    Dim parsed As Object

    json("VersionIds") = ids

    HTTPClient.Open "POST", requestPath("Export"), useAsync
    HTTPClient.SetRequestHeader "Content-Type", "application/json"

    body = JsonConverter.ConvertToJson(json)

    response = Execute(HTTPClient, body)

    main_res_json = RegexReplace(response, "^{""Urls"":(.*)}$", "$1")
    Set parsed = JsonConverter.ParseJson(main_res_json)
    
    Dim urls As Variant
    ReDim urls(parsed.Count - 1)
    
    Dim i As Long

    i = 0
    For Each url In parsed
      urls(i) = url
      i = i + 1
    Next url

    GetDownloadUrls = urls
End Function



Public Function UpdateProperty(document As Variant) As String
    Dim body As String
    Dim response As String
    
    HTTPClient.Open "POST", requestPath("UpdateProperty"), useAsync
    HTTPClient.SetRequestHeader "Content-Type", "application/json"
    
    Dim Criteria As Dictionary
    Set Criteria = New Dictionary
    Criteria.add "Document", document
    
    body = JsonConverter.ConvertToJson(Criteria)
    response = Execute(HTTPClient, body)
    
    UpdateProperty = RegexReplace(response, """(.*)""", "$1")
    
End Function


Public Function GetUser() As Variant
    
    Dim json As String, url As String
    Dim Credential As Object
    Dim response As String
    Dim main_res_json As String
    Dim user As Object
    Dim body As String
    
    ' Set Login information
    Set Credential = JsonConverter.ParseJson("{}")
    Credential("UserName") = loginid '"administrator"
    Credential("Password") = password
            
    HTTPClient.Open "POST", requestPath("GetUser"), useAsync
    HTTPClient.SetRequestHeader "Content-Type", "application/json"

    body = JsonConverter.ConvertToJson(Credential)

    response = Execute(HTTPClient, body)
    
    main_res_json = RegexReplace(response, "^{""User"":(.*)}$", "$1")
    Set user = JsonConverter.ParseJson(main_res_json)
    
    Set GetUser = user

End Function


Public Function UpdateUser(user As Variant) As Boolean

    Dim json As String, url As String
    Dim response As String
    Dim isSuccess As Boolean
    Dim body As String
        
    Dim Criteria As Dictionary
    Set Criteria = New Dictionary
    Criteria.add "User", user
    Criteria.add "UserName", loginid
    Criteria.add "Password", password
        
    body = JsonConverter.ConvertToJson(Criteria)
        
    HTTPClient.Open "POST", requestPath("UpdateUser"), useAsync
    HTTPClient.SetRequestHeader "Content-Type", "application/json"

    response = Execute(HTTPClient, body)
    
    UpdateUser = (response = "true")
    
End Function

'
' Return Histories , Order by id_doc, version, and date
'
Public Function GetHistories(Criteria As Variant) As Variant
On Error GoTo ErrCatcher

    Dim body As String
    Dim response As String
    Dim main_res_json As String
    'Dim json As Object: Set json = JsonConverter.ParseJson("{}")
    Dim parsed As Object

    HTTPClient.Open "POST", requestPath("GetHistories"), useAsync
    HTTPClient.SetRequestHeader "Content-Type", "application/json; charset=utf-8"

    body = JsonConverter.ConvertToJson(Criteria)

    response = Execute(HTTPClient, body)

    main_res_json = RegexReplace(response, "^{""Histories"":(.*)}$", "$1")
    Set parsed = JsonConverter.ParseJson(main_res_json)

    Set GetHistories = parsed
    
ErrCatcher:
     
    Resume Next
    
End Function


'
' Insert or Update Folder
' This function should be under the clsSpiderDocsWebClient.
'
Public Function SaveFolder(Folder As Variant) As Variant
On Error GoTo ErrCatcher

    Dim body As String
    Dim response As String
    Dim main_res_json As String
    'Dim json As Object: Set json = JsonConverter.ParseJson("{}")
    Dim parsed As Object

    HTTPClient.Open "POST", requestPath("SaveFolder"), useAsync
    HTTPClient.SetRequestHeader "Content-Type", "application/json; charset=utf-8"

    body = JsonConverter.ConvertToJson(Folder)

    response = Execute(HTTPClient, body)

    main_res_json = RegexReplace(response, "^{""Folder"":(.*)}$", "$1")
    Set parsed = JsonConverter.ParseJson(main_res_json)

    Set SaveFolder = parsed
    
ErrCatcher:
     
    Resume Next
    
End Function


Public Function SaveDoc(filePath As String, document As Variant) As String
    Const STR_BOUNDARY  As String = "----3fbd04f5-b1ed-4060-99b9-fca7ff59c113"
    Dim body As String
    Dim sPostData       As String
    Dim contenttype As String: contenttype = ContentTypeFor(filePath)
    Dim tempid As String
    Dim response As String
    
    '
    'Upload File
    '
    HTTPClient.Open "POST", requestPath("UploadFile"), useAsync
    HTTPClient.SetRequestHeader "Content-Type", "multipart/form-data; boundary=" & STR_BOUNDARY
    
    sPostData = ReadFile(filePath)

    sPostData = "--" & STR_BOUNDARY & vbCrLf & _
        "Content-Disposition: form-data; name=""file0""; filename=""" & Mid$(filePath, InStrRev(filePath, "\") + 1) & """" & vbCrLf & _
        "Content-Type: " & contenttype & vbCrLf & vbCrLf & _
        sPostData & vbCrLf & _
        "--" & STR_BOUNDARY & "--"

    response = Execute(HTTPClient, pvToByteArray(sPostData))
    tempid = RegexReplace(response, """(.*)""", "$1")
    
    '
    'Save Docs
    '
    HTTPClient.Open "POST", requestPath("Import"), useAsync
    HTTPClient.SetRequestHeader "Content-Type", "application/json"
    
    Dim Criteria As Dictionary
    Set Criteria = New Dictionary
    Criteria.add "TempId", tempid
    Criteria.add "Document", document
    
    body = JsonConverter.ConvertToJson(Criteria)
    response = Execute(HTTPClient, body)
    
    SaveDoc = RegexReplace(response, """(.*)""", "$1")
    
End Function



Private Function ReadFile(filePath As String) As String
    Dim nFile           As Integer

    nFile = FreeFile
    Open filePath For Binary Access Read As nFile
    If LOF(nFile) > 0 Then
        ReDim baBuffer(0 To LOF(nFile) - 1) As Byte
        Get nFile, , baBuffer
        ReadFile = StrConv(baBuffer, vbUnicode)
    Else
        ReadFile = ""
    End If

    Close nFile

End Function


' ========================================
'   Private function
' ========================================
'

Private Function requestPath(Path As String)
    
    If LoginedUser("id") = "" Then
        ' nologin
    End If
    
    ' return string.Format("spiderdocs/External/{0}/{1}", path, LoginedUser.id);
    requestPath = ServerUrl & "/External/" & Path & "/" & LoginedUser("id")
    
End Function


Private Function Execute(client As WinHttp.WinHttpRequest, Optional body As Variant)
    Dim cookie As String, response_body As Variant
    On Error GoTo ErrCatcher

    ' actual request. Method and Content-Type should be set in caller
    client.SetRequestHeader "Connection", "keep-alive"
    client.SetRequestHeader "User-Agent", "MSAccess/4.0 (compatible; MSIE 6.0; Windows NT 5.0)"

    If m_cookie <> "" Then
        client.SetRequestHeader "Cookie", m_cookie
    End If

    client.Send body
    'Dim a As Variant
    
    'a = client.GetAllResponseHeaders()
    ' Add cookie for next request
    m_cookie = client.GetResponseHeader("Set-Cookie")
    
    Execute = client.ResponseText
    Exit Function
    
ErrCatcher:
     
    Resume Next

End Function


' ========================================
'   VBA Only function
' ========================================
'
Private Function pvToByteArray(sText As String) As Byte()
    pvToByteArray = StrConv(sText, vbFromUnicode)
End Function

Public Function Download(url As String)
    Dim file_name As String
    HTTPClient.Open "GET", url, useAsync

    response = Execute(HTTPClient, "")
    file_name = RegexReplace(url, ".*\/([a-zA-Z0-9\_\s\.\-]+)", "$1")
    
    Dim full_path As String: full_path = DownloadFolder + "/" & file_name
    
    ' Remove before download
    FileToDelete full_path
     
    If HTTPClient.Status = 200 Then
        Set oStream = CreateObject("ADODB.Stream")
        oStream.Open
        oStream.Type = 1
        oStream.Write HTTPClient.ResponseBody
        oStream.SaveToFile full_path, 2 ' 1 = no overwrite, 2 = overwrite
        oStream.Close
    End If
    
    AddDownloadedFile full_path
    Download = full_path
    
End Function

Private Function RegexReplace(text As String, pattern As String, retMatch As String)
    Dim regEx As New RegExp

    With regEx
        .Global = True
        .Multiline = True
        .IgnoreCase = False
        .pattern = pattern
    End With
    
    RegexReplace = regEx.Replace(text, retMatch)
End Function

Private Sub FileToDelete(Path As String)

    With New FileSystemObject
        If .FileExists(Path) Then
            .DeleteFile Path
        End If
    End With
End Sub


Private Sub MakeDirectory(FolderPath As String)
    Dim Path As String: Path = FolderPath
    Dim x, i As Integer, strPath As String
    
    If Right(Path, 1) <> "\" Then
        Path = Path + "\"
    End If

    x = Split(Path, "\")

    For i = 0 To UBound(x) - 1
        strPath = strPath & x(i) & "\"
        If Not FolderExists(strPath) Then MkDir strPath
    Next i

End Sub

    'function to check if folder exist
Private Function FolderExists(FolderPath As String) As Boolean
    On Error Resume Next

    ChDir FolderPath
    If Err Then FolderExists = False Else FolderExists = True

End Function

Public Function UpdateAttrs(key As String, value As Variant, doc As Variant)
    Dim attr As Dictionary
    
    Dim i As Integer: i = 0
    For Each attr In doc("Attrs")
        i = i + 1
        If attr("id") = key Then
            doc("Attrs").Remove (i)
            Exit For
        End If
    Next
    
    i = doc("Attrs").Count
    
    'Dim i As Integer: i = docs(1)("Attrs").Count
    doc("Attrs").add New Dictionary
    doc("Attrs")(i + 1).add "id", key ' JobNumber for BTH
    doc("Attrs")(i + 1).add "atbValue", value
    
End Function


Function ContentTypeFor(fileName As String) As String
    Dim Ext As String: Ext = Right(fileName, Len(fileName) - InStrRev(fileName, "."))

    Set Dic = New Dictionary

    Dic.add "3g2", "video/3gpp2"
    Dic.add "3gp", "video/3gpp"
    Dic.add "7z", "application/x-7z-compressed"
    Dic.add "aac", "audio/aac"
    Dic.add "abw", "application/x-abiword"
    Dic.add "arc", "application/octet-stream"
    Dic.add "avi", "video/x-msvideo"
    Dic.add "azw", "application/vnd.amazon.ebook"
    Dic.add "bin", "application/octet-stream"
    Dic.add "bz", "application/x-bzip"
    Dic.add "bz2", "application/x-bzip2"
    Dic.add "csh", "application/x-csh"
    Dic.add "css", "text/css"
    Dic.add "csv", "text/csv"
    Dic.add "doc", "application/msword"
    Dic.add "docm", "application/vnd.ms-word.document.macroEnabled.12"
    Dic.add "docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
    Dic.add "dot", "application/msword"
    Dic.add "dotm", "application/vnd.ms-word.template.macroEnabled.12"
    Dic.add "dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"
    Dic.add "epub", "application/epub+zip"
    Dic.add "gif", "image/gif"
    Dic.add "htm", "text/html"
    Dic.add "html", "text/html"
    Dic.add "ico", "image/x-icon"
    Dic.add "ics", "text/calendar"
    Dic.add "jar", "application/java-archive"
    Dic.add "jpeg", "image/jpeg"
    Dic.add "jpg", "image/jpeg"
    Dic.add "js", "application/javascript"
    Dic.add "json", "application/json"
    Dic.add "mdb", "application/vnd.ms-access"
    Dic.add "mid", "audio/midi"
    Dic.add "midi", "audio/midi"
    Dic.add "mpeg", "video/mpeg"
    Dic.add "mpkg", "application/vnd.apple.installer+xml"
    Dic.add "odp", "application/vnd.oasis.opendocument.presentation"
    Dic.add "ods", "application/vnd.oasis.opendocument.spreadsheet"
    Dic.add "odt", "application/vnd.oasis.opendocument.text"
    Dic.add "oga", "audio/ogg"
    Dic.add "ogv", "video/ogg"
    Dic.add "ogx", "application/ogg"
    Dic.add "pdf", "application/pdf"
    Dic.add "png", "image/png"
    Dic.add "pot", "application/vnd.ms-powerpoint"
    Dic.add "potm", "application/vnd.ms-powerpoint.template.macroEnabled.12"
    Dic.add "potx", "application/vnd.openxmlformats-officedocument.presentationml.template"
    Dic.add "ppa", "application/vnd.ms-powerpoint"
    Dic.add "ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"
    Dic.add "pps", "application/vnd.ms-powerpoint"
    Dic.add "ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"
    Dic.add "ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"
    Dic.add "ppt", "application/vnd.ms-powerpoint"
    Dic.add "pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"
    Dic.add "pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"
    Dic.add "rar", "application/x-rar-compressed"
    Dic.add "rtf", "application/rtf"
    Dic.add "sh", "application/x-sh"
    Dic.add "svg", "image/svg+xml"
    Dic.add "swf", "application/x-shockwave-flash"
    Dic.add "tar", "application/x-tar"
    Dic.add "tiff", "image/tiff"
    Dic.add "ttf", "font/ttf"
    Dic.add "txt", "text/plain"
    Dic.add "vsd", "application/vnd.visio"
    Dic.add "wav", "audio/x-wav"
    Dic.add "weba", "audio/webm"
    Dic.add "webm", "video/webm"
    Dic.add "webp", "image/webp"
    Dic.add "woff", "font/woff"
    Dic.add "woff2", "font/woff2"
    Dic.add "xhtml", "application/xhtml+xml"
    Dic.add "xla", "application/vnd.ms-excel"
    Dic.add "xlam", "application/vnd.ms-excel.addin.macroEnabled.12"
    Dic.add "xls", "application/vnd.ms-excel"
    Dic.add "xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"
    Dic.add "xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"
    Dic.add "xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    Dic.add "xlt", "application/vnd.ms-excel"
    Dic.add "xltm", "application/vnd.ms-excel.template.macroEnabled.12"
    Dic.add "xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"
    Dic.add "xml", "application/xml"
    Dic.add "xul", "application/vnd.mozilla.xul+xml"
    Dic.add "zip", "application/zip"

    If Dic(LCase(Ext)) = "" Then
        Call MsgBox("This file isn't supported YET. Please Contact Us", vbOKOnly, "Not Support File")
    End If

    ContentTypeFor = Dic(LCase(Ext))

End Function

Public Sub Dispose()
    On Error Resume Next

    Dim strPath As String: strPath = ""
    ' add gavarage collector
    Set HTTPClient = Nothing
    
    ' remove all downloaded files
    Dim Path As Variant
    If Len(Join(m_downloaded_files)) > 0 Then
        For Each Path In DownloadedFiles
            FileToDelete CStr(Path)
        Next
    End If
   
    
    If Dir(DownloadFolder & "\*.*") = "" Then
        'Kill DownloadFolder    ' delete all files in the folder
        RmDir DownloadFolder  ' delete folder
    End If
    
    ' Remove folder if the folder does not contain any file
End Sub