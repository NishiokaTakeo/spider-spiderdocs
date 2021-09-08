Option Compare Database

Private m_spider_client_bth As clsSpiderDocsWebClient


Public Function CreateSpiderDocsClient4Local() As clsSpiderDocsWebClient
    
    Dim client As clsSpiderDocsWebClient
    Set client = New clsSpiderDocsWebClient
    
    Dim tempfolder As String: tempfolder = "C:/SpiderDocsDownloadedFiles/" & Format(Date, "yyyymmdd")
    Dim ServerUrl As String: ServerUrl = "http://localhost:50920/spiderdocs"
    Dim loginid As String: loginid = "Administrator"
    Dim password As String: password = "Welcome1"
    
    'Setup for all necessary property
    client.Initialize ServerUrl, loginid, password, tempfolder
    
    Set CreateSpiderDocsClient4Local = client

End Function


Public Function CreateSpiderDocsClient4CL() As clsSpiderDocsWebClient
    
    Dim client As clsSpiderDocsWebClient
    Set client = New clsSpiderDocsWebClient
    
    Dim tempfolder As String: tempfolder = "C:/SpiderDocsDownloadedFiles/" & Format(Date, "yyyymmdd")
    Dim approot As String: approot = "http://orders.contentliving.com.au/spiderdocs"
    Dim loginid As String: loginid = "Administrator"
    Dim password As String: password = "Welcome1"
    
    'Setup for all necessary property
    client.Initialize approot, loginid, password, tempfolder
    
    client.EnableNonAuthenticationMode "1"
    
    Set CreateSpiderDocsClient4CL = client

End Function

Public Function CreateSpiderDocsClient4BTH() As clsSpiderDocsWebClient

    If hasInstance(m_spider_client_bth) Then m_spider_client_bth.useAsync = False: Set CreateSpiderDocsClient4BTH = m_spider_client_bth: Exit Function
    
    Dim client As clsSpiderDocsWebClient
    Set client = New clsSpiderDocsWebClient
        
    Dim tempfolder As String: tempfolder = "C:/SpiderDocsDownloadedFiles/" & Format(Date, "yyyymmdd")
    Dim approot As String: approot = "https://app.bentragerhomes.com.au/spiderdocs"
    Dim loginid As String: loginid = "Administrator"
    Dim password As String: password = "Welcome1"
    
    'Setup for all necessary property
    client.Initialize approot, loginid, password, tempfolder
    
    Set CreateSpiderDocsClient4BTH = client
    Set m_spider_client_bth = client
End Function

Private Function hasInstance(ins As clsSpiderDocsWebClient) As Boolean

    If Not ins Is Nothing Then
        If ins.IsLogined() Then
            hasInstance = True
        
            Exit Function
        End If
    End If
    
    hasInstance = False
    
End Function