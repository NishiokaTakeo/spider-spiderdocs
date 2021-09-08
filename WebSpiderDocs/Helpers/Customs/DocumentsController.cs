using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using ReportBuilder.Models;
using ReportBuilder.Utils;

namespace ReportBuilder.Controllers
{
    public class DocumentsController : DaoBase
    {
        static string conn = SpiderDocsModule.SqlOperation.DBConnectionString;

        public DocumentsController() : base(conn)
        {

        }


        ////Datasources/Fields
        //public List<tblBookMarkDataSource> GetDataSources()
        //{
        //    var tmpList = this.ExecuteSQLDynamic<tblBookMarkDataSource>(new tblBookMarkDataSource()).ToList();
        //    tmpList.Add(new tblBookMarkDataSource() { DataSourceID = 0, DataSource = "Question", DisplayName = "Questions" });
        //    return tmpList.OrderBy(o => o.DataSourceID).ToList();
        //}

        //public List<tblBookMarkDataSourceField> GetDataSourceFields()
        //{
        //    return this.ExecuteSQL<tblBookMarkDataSourceField>("Exec dbo.spcGetDataSourceFields").ToList();
        //}

        public void UpdateDataSourceFields()
        {
            var spcBookmarks = this.ExecuteSQL<tblBookMarkDataSourceField>("Exec dbo.spcGetDataSourceFieldsData").ToList();
            var tableBookmarks = this.ExecuteSQL<tblBookMarkDataSourceField>("SELECT * FROM tblBMDataSourceFields").ToList();


            var sql = @"INSERT INTO tblBMDataSourceFields
                        VALUES (@DataSourceId, @Name, @DisplayName);";


            foreach (var spcBookmark in spcBookmarks)
            {
                if (spcBookmark.DataSourceID == 0)
                {
                    //Questions
                    continue;
                }

                if (!tableBookmarks.Exists(a => a.DataSourceID == spcBookmark.DataSourceID && a.Name == spcBookmark.Name))
                {
                    var parameters = new Dictionary<string, object>() {
                        { "DataSourceId", spcBookmark.DataSourceID },
                        { "Name", spcBookmark.Name },
                        { "DisplayName", spcBookmark.Name }
                    };
                    this.ExecuteRaw(sql, parameters);
                }
            }
        }


        ////Bookmarks
        //public List<tblBookMark> GetBookmarksForTemplate(int templateId)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    String sql = "SELECT * FROM [dbo].[tblBookmark] WHERE [BookmarkID] IN (SELECT BookmarkID from [dbo].[tblDocBookMarks] WHERE DocTemplateID = '" + templateId + "')";
        //    return this.ExecuteSQL<tblBookMark>(sql, dic).ToList();
        //}

        //public String UpdateBookmarksForTemplate(int docId, int campusId, string bookmarkName, int dataSourceId, string fieldName, int questionId)
        //{
        //    var template = this.GetTemplateByDocCampusId(docId, campusId);

        //    //Delete any existing bookmarks in template for that bookmark name
        //    String sql = @"DELETE FROM [tblDocBookMarks] WHERE ID IN 
        //        (SELECT ID from [tblDocBookMarks] db
        //        INNER JOIN tblBookMark b on db.BookMarkID = b.BookMarkID 
        //        WHERE db.DocTemplateID = @TemplateId
        //        and BookMarkName = @BookmarkName);";
        //    var parameters = new Dictionary<string, object>() { { "TemplateId", template.DocTemplateID }, { "BookmarkName", bookmarkName } };
        //    this.ExecuteRaw(sql, parameters);


        //    var templateBookmarks = this.GetBookmarksForTemplate(template.DocTemplateID);
        //    if (dataSourceId == 0)
        //    {
        //        var matchedBookmarks = this.ExecuteSQLDynamic<tblBookMark>(new tblBookMark() { BookMarkName = bookmarkName, DataSourceID = dataSourceId, BMQuestionID = questionId }).ToList();
        //        if (matchedBookmarks.Count == 0)
        //        {
        //            this.ExecuteInsertSQL<tblBookMark>(new tblBookMark() { BookMarkName = bookmarkName, DataSourceID = dataSourceId, FieldName = "", BMQuestionID = questionId });
        //            matchedBookmarks = this.ExecuteSQLDynamic<tblBookMark>(new tblBookMark() { BookMarkName = bookmarkName, DataSourceID = dataSourceId, BMQuestionID = questionId }).ToList();
        //        }

        //        var linkBookmark = matchedBookmarks.FirstOrDefault();
        //        return this.ExecuteInsertSQL<tblDocBookmarks>(new tblDocBookmarks() { DocTemplateID = template.DocTemplateID, BookmarkID = linkBookmark.BookmarkID }).ToString();
        //    }
        //    else
        //    {
        //        var matchedBookmarks = this.ExecuteSQLDynamic<tblBookMark>(new tblBookMark() { BookMarkName = bookmarkName, DataSourceID = dataSourceId, FieldName = fieldName }).ToList();
        //        if (matchedBookmarks.Count == 0)
        //        {
        //            this.ExecuteInsertSQL<tblBookMark>(new tblBookMark() { BookMarkName = bookmarkName, DataSourceID = dataSourceId, FieldName = fieldName, BMQuestionID = 0 });
        //            matchedBookmarks = this.ExecuteSQLDynamic<tblBookMark>(new tblBookMark() { BookMarkName = bookmarkName, DataSourceID = dataSourceId, FieldName = fieldName }).ToList();
        //        }

        //        var linkBookmark = matchedBookmarks.FirstOrDefault();
        //        return this.ExecuteInsertSQL<tblDocBookmarks>(new tblDocBookmarks() { DocTemplateID = template.DocTemplateID, BookmarkID = linkBookmark.BookmarkID }).ToString();
        //    }
        //}

        //public void DeleteBookMarkFromTemplate(int templateId, string bookmarkName)
        //{
        //    String sql = @"DELETE FROM [tblDocBookMarks] WHERE ID IN 
        //        (SELECT ID from [tblDocBookMarks] db
        //        INNER JOIN tblBookMark b on db.BookMarkID = b.BookMarkID 
        //        WHERE db.DocTemplateID = @TemplateId
        //        and BookMarkName = @BookmarkName);";
        //    var parameters = new Dictionary<string, object>() { { "TemplateId", templateId }, { "BookmarkName", bookmarkName } };
        //    this.ExecuteRaw(sql, parameters);
        //}

        //public void CopyTemplatesBookMarks(int oldTemplateId, int newTemplateId)
        //{
        //    var bookmarkLinks = this.ExecuteSQLDynamic<tblDocBookmarks>(new tblDocBookmarks() { DocTemplateID = oldTemplateId }).ToList();
        //    foreach (var bookmarkLink in bookmarkLinks)
        //    {
        //        this.ExecuteInsertSQL<tblDocBookmarks>(new tblDocBookmarks()
        //        {
        //            DocTemplateID = newTemplateId,
        //            BookmarkID = bookmarkLink.BookmarkID
        //        });
        //    }
        //}

        //public List<tblBookMark> GetUniqueBookMarks()
        //{
        //    var tmpList = this.ExecuteSQLDynamic<tblBookMark>(new tblBookMark()).ToList();
        //    return tmpList.GroupBy(b => b.BookMarkName).Where(b => !b.Skip(1).Any()).SelectMany(b => b).ToList();
        //}


        ////Templates
        //public tblDocTemplateList GetTemplateByDocCampusId(int docId, int campusId)
        //{
        //    var doc = this.ExecuteSQLDynamic<tblDocDetails>(new tblDocDetails() { DocID = docId }).FirstOrDefault();
        //    return this.ExecuteSQLDynamic<tblDocTemplateList>(new tblDocTemplateList() { DocTemplateID = doc.DocTemplateId, CampusID = campusId }).FirstOrDefault();
        //}

        //public tblDocTemplateList GetTemplateBySpiderDocsId(int spiderDocsId)
        //{
        //    return this.ExecuteSQLDynamic<tblDocTemplateList>(new tblDocTemplateList() { SpiderDocsID = spiderDocsId }).FirstOrDefault();
        //}

        //public int InsertNewTemplate(int campusId, int spiderDocsId)
        //{
        //    var templates = this.ExecuteSQLDynamic<tblDocTemplateList>(new tblDocTemplateList()).ToList();
        //    var templateId = templates.OrderByDescending(t => t.DocTemplateID).First().DocTemplateID + 1;

        //    this.ExecuteInsertSQL<tblDocTemplateList>(new tblDocTemplateList()
        //    {
        //        DocTemplateID = templateId,
        //        CampusID = campusId,
        //        SpiderDocsID = spiderDocsId
        //    });

        //    return templateId;
        //}


        ////Documents
        //public tblDocDetails GetDocument(int id)
        //{
        //    return this.ExecuteSQLDynamic<tblDocDetails>(new tblDocDetails() { DocID = id }).FirstOrDefault();
        //}

        //public List<tblDocDetails> GetDocumentByTitleAndCampus(int campusId, string title)
        //{
        //    return this.ExecuteSQLDynamic<tblDocDetails>(new tblDocDetails() { CampusId = campusId, DocTitle = title }).ToList();
        //}

        //public tblDocDetails GetDocumentByTemplateIdAndCampus(int campusId, int templateId)
        //{
        //    return this.ExecuteSQLDynamic<tblDocDetails>(new tblDocDetails() { CampusId = campusId, DocTemplateId = templateId }).FirstOrDefault();
        //}



        //public void UpdateDocumentDetails(int docId, string title, string comments, string description, int docType, int certNumber)
        //{
        //    var doc = GetDocument(docId);
        //    this.ExecuteUpdateSQL<tblDocDetails>(new tblDocDetails()
        //    {
        //        DocID = docId,
        //        DocTitle = title,
        //        DocTypeId = docType,
        //        ContactDescription = description,
        //        CertNumberId = certNumber,
        //        DocTemplateId = doc.DocTemplateId,
        //        CreatedDate = doc.CreatedDate,
        //        ModifiedDate = DateTime.Now,
        //        ContactComments = comments,
        //        CampusId = doc.CampusId
        //    });
        //}

        //public List<tblCertNumbers> GetCertificateNumbers()
        //{
        //    return this.ExecuteSQLDynamic<tblCertNumbers>(new tblCertNumbers()).ToList();
        //}

        //public int InsertDocument(string title, int type, string description, int certNumberId, int templateId, string contentComments, int campusId)
        //{
        //    return this.ExecuteInsertSQL<tblDocDetails>(new tblDocDetails()
        //    {
        //        DocTitle = title,
        //        DocTypeId = type,
        //        ContactDescription = description,
        //        CertNumberId = certNumberId,
        //        DocTemplateId = templateId,
        //        CreatedDate = DateTime.Now,
        //        ModifiedDate = DateTime.Now,
        //        ContactComments = contentComments,
        //        CampusId = campusId
        //    });
        //}

        ////Campus
        //public tblCampus GetCampus(int id)
        //{
        //    return this.ExecuteSQLDynamic<tblCampus>(new tblCampus() { Id = id }).FirstOrDefault();
        //}

        //public List<tblCampus> GetCampusWithoutDocTitle(string title)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    var sql = @"SELECT * FROM [dbo].[tblCampus] WHERE Id NOT IN 
        //    	(SELECT CampusId from tblDocDetails WHERE DocTitle = '" + title + "')";
        //    return this.ExecuteSQL<tblCampus>(sql, dic).ToList();
        //}


        ////Documents List
        //public List<viewDocumentsWithCampus> GetAllByTitle(string title)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    String sql = "SELECT * FROM [dbo].[viewDocumentsWithCampus] WHERE [DocTitle] LIKE '%" + title.Trim() + "%'";
        //    return this.ExecuteSQL<viewDocumentsWithCampus>(sql, dic).ToList();
        //}

        //public List<viewDocumentsWithCampus> GetAllByType(string typeId)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    String sql = "SELECT * FROM [dbo].[tblDocDetails] WHERE [Title] LIKE '%" + typeId.ToString().Trim() + "%'";
        //    return this.ExecuteSQL<viewDocumentsWithCampus>(sql, dic).ToList();
        //}

        //public List<viewDocumentsWithCampus> FindDocumentsByCreationDate(string CreatedStartDate, string CreatedEndDate)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    String sql = "SELECT * FROM [dbo].[tblDocDetails] WHERE [Title] LIKE '%" + CreatedStartDate.Trim() + "%'";
        //    return this.ExecuteSQL<viewDocumentsWithCampus>(sql, dic).ToList();
        //}

        //public List<viewDocumentsWithCampus> FindDocumentsByCampus(int[] campusIds)
        //{
        //    Dictionary<string, object> dic = new Dictionary<string, object>();
        //    String sql = "SELECT * FROM [dbo].[tblDocDetails] WHERE [Title] LIKE '%" + campusIds.ToString().Trim() + "%'";
        //    return this.ExecuteSQL<viewDocumentsWithCampus>(sql, dic).ToList();
        //}

        ////Courses
        //public List<tblCourseDocs> GetCurrentCoursesForDocument(int docId)
        //{
        //    return this.ExecuteSQLDynamic<tblCourseDocs>(new tblCourseDocs() { DocID = docId }).ToList();
        //}

        //public List<tblCourse> GetCourses()
        //{
        //    return this.ExecuteSQLDynamic<tblCourse>(new tblCourse()).ToList();
        //}

        //public tblCourse GetCourse(int courseId)
        //{
        //    return this.ExecuteSQLDynamic<tblCourse>(new tblCourse() { CourseId = courseId }).FirstOrDefault();
        //}

        //public String AddDocumentToCourse(int docId, int courseId)
        //{
        //    var matchedEntries = this.ExecuteSQLDynamic<tblCourseDocs>(new tblCourseDocs() { DocID = docId, CourseID = courseId }).ToList();
        //    if (matchedEntries.Count == 0)
        //    {
        //        this.ExecuteInsertSQL<tblCourseDocs>(new tblCourseDocs() { DocID = docId, CourseID = courseId });
        //        return "Added Link for Doc: " + docId + "  Course: " + courseId;
        //    }
        //    return "Doc: " + docId + " Already in Course: " + courseId;
        //}

        //public String DeleteDocumentFromCourse(int docId, int courseId)
        //{
        //    var item = this.ExecuteSQLDynamic<tblCourseDocs>(new tblCourseDocs() { DocID = docId, CourseID = courseId }).FirstOrDefault();
        //    this.ExecuteDeleteSQL<tblCourseDocs>(item.ID);
        //    return "Doc: " + docId + " Deleted From Course: " + courseId;
        //}

        //public void CopyDocumentsCourses(int oldDocId, int newDocId)
        //{
        //    var courseLinks = this.ExecuteSQLDynamic<tblCourseDocs>(new tblCourseDocs() { DocID = oldDocId}).ToList();
        //    foreach (var courseLink in courseLinks)
        //    {
        //        this.ExecuteInsertSQL<tblCourseDocs>(new tblCourseDocs()
        //        {
        //            DocID = newDocId,
        //            CourseID = courseLink.CourseID
        //        });
        //    }
        //}


        ////Global
        //public void DeleteDocument(int docId)
        //{
        //    var doc = GetDocument(docId);
        //    var template = GetTemplateByDocCampusId(doc.DocID, doc.CampusId);

        //    this.ExecuteRaw("DELETE FROM tblCourseDocs WHERE DocID = @DocId;", new Dictionary<string, object>() { { "DocId", doc.DocID } });
        //    this.ExecuteRaw("DELETE FROM tblDocBookMarks WHERE DocTemplateID = @TemplateId", new Dictionary<string, object>() { { "TemplateId", template.DocTemplateID } });
        //    this.ExecuteRaw("DELETE FROM tblDocTemplateList WHERE DocTemplateID = @TemplateId;", new Dictionary<string, object>() { { "TemplateId", template.DocTemplateID } });
        //    this.ExecuteRaw("DELETE FROM tblDocDetails WHERE DocID = @DocId;", new Dictionary<string, object>() { { "DocId", doc.DocID } });
        //}
    }
}