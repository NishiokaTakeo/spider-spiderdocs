Option Compare Database

'-------------------------------------
' FOR BENTRAGER
'-------------------------------------

 Public Enum SD_Attr
    JobNumber = 125
    documentDate = 139
    Customername = 140
    Department = 141
    ContractType = 142
    QualSheetType = 143
    Package1 = 144
    Package2 = 145
    LeadID = 146
    Brochure = 148
    PrintAds = 149
    Magnetic = 150
    HouseType = 151
    LotRange = 152
    LoadType = 153
    Balcony = 154
    HouseDesign = 156
    PhotoType = 157
    TestSheet = 158
    MailSubject = 10000
    MailFrom = 10001
    MailTime = 10002
    MailTo = 10003
    MailIsComposed = 10004
End Enum


Public Sub UpdateAttrs(key As String, value As Variant, doc As Variant)
    Dim attr As Dictionary
    
    If Not doc.Exists("Attrs") Then
        doc.add "Attrs", New Collection
    End If
        
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
    
End Sub