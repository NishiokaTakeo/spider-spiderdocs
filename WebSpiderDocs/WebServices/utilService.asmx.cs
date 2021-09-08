using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;
using ReportBuilder.Models;
//using CET.Utils;
using ReportBuilder.Controllers;
using NLog;
//using CET.Helpers;

namespace ReportBuilder.WebServices
{
    /// <summary>
    /// Summary description for utilService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class utilService : System.Web.Services.WebService
    {

        static Logger logger = LogManager.GetCurrentClassLogger();

        //static String testFlg = System.Configuration.ConfigurationManager.AppSettings["testFlg"];

     //   [WebMethod]
     //   public string SaveStudentFromTemp(string studentId, string userName, string CETStudentId)
     //   {
     //       var TheJson = "";
     //       string rtnStudentId = "";
     //       userName = userName.Replace("#quot", "'");
     //       bool bRet = false;
     //       try
     //       {
     //           if (!string.IsNullOrEmpty(studentId))
     //           {
     //               int iStudentId = int.Parse(studentId);
     //               if (iStudentId > 0)
     //               {
     //                   if (Sources.UpdateSoundcode(studentId, userName) == true)
     //                   {
     //                       // Update
     //                       bRet = Sources.UpdateStudent(studentId, userName);
     //                       if (bRet == true)
     //                       {
     //                           rtnStudentId = studentId;
     //                           // Save successfully
     //                           LogController.Add_Log("STUDENT PAGE/GENERAL DETAILS", studentId, "Student information is updated successfully.", userName);
     //                       }
     //                       else
     //                       {
     //                           // Save failure
     //                           LogController.Add_Log("STUDENT PAGE/GENERAL DETAILS", studentId, "Student information is failed to update.", userName);
     //                       }
     //                   }
     //               }
     //               else
     //               {
     //                   // Insert
     //                   bRet = Sources.InsertStudent(studentId, userName, CETStudentId, out rtnStudentId);
     //                   if (bRet == true)
     //                   {
     //                       // Save successfully
     //                       LogController.Add_Log("STUDENT PAGE/GENERAL DETAILS", rtnStudentId, "New student is inserted successfully.", userName);
     //                   }
     //                   else
     //                   {
     //                       // Save successfully
     //                       LogController.Add_Log("STUDENT PAGE/GENERAL DETAILS", rtnStudentId, "New student is failed to insert", userName);
     //                       if (!string.IsNullOrEmpty(rtnStudentId))
     //                       {
     //                           int iDeleteStudentId = int.Parse(rtnStudentId);
     //                           Sources.HardDeleteStudent(iDeleteStudentId, userName);
     //                       }
     //                   }
     //               }

     //               if (testFlg != "true")
     //               {
     //                   if( false == string.IsNullOrWhiteSpace(rtnStudentId))
     //                   {
     //                       Sources.SaveLinkedStudentItem(rtnStudentId);
     //                       Sources.SaveComboItem(rtnStudentId);
     //                   }
     //               }

     //               /*
     //               Student student = Sources.GetStudent(int.Parse(rtnStudentId));

     //               string name = string.Format("{0} {1} {2}", student.FirstName, student.MiddleName, student.Surname).Replace("  ", " ");

     //               SpiderDoscWebClient client = new SpiderDoscWebClient(new ConfigurationFactory.SpiderWebClientConf());

     //               SpiderDocsModule.DocumentAttributeCombo child = client.GetAttributeComboboxItems(int.Parse(reportAttributeId), rtnStudentId).FirstOrDefault();

					//if( child == null || child.id == 0)
     //               {
     //                   client.EditAttributeComboboxItem(int.Parse(reportAttributeId), new SpiderDocsModule.DocumentAttributeCombo() { text = rtnStudentId, children = new List<SpiderDocsModule.DocumentAttribute>() { new SpiderDocsModule.DocumentAttribute() { id = int.Parse(reportAttributeId), atbValue = name } }  });
     //               }
     //               */
     //           }
     //           if (bRet == true)
     //           {
     //               TheJson = rtnStudentId;
     //           }
     //       }
     //       catch (Exception ex)
     //       {
     //           string errMsg = Error.ErrorOutput(ex);
     //           logger.Error(ex);
     //       }
     //       return TheJson;
     //   }

     //   [WebMethod]
     //   public string GetCompany()
     //   {
     //       var TheJson = "";
     //       List<TrainingOrganisation> lstOrganisation = Sources.GetCompany();
     //       if (lstOrganisation != null)
     //       {
     //           // instantiate a serializer
     //           JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
     //           //TheSerializer.MaxJsonLength = int.MaxValue;
     //           TheJson = TheSerializer.Serialize(lstOrganisation);
     //       }
     //       return TheJson;
     //   }
     //   [WebMethod]
     //   public string GetTrainingStatus()
     //   {
     //       var TheJson = "";
     //       List<tblTrainingStatus> lstTrainingStatus = Sources.GetTrainingStatus();
     //       if (lstTrainingStatus != null)
     //       {
     //           // instantiate a serializer
     //           JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
     //           //TheSerializer.MaxJsonLength = int.MaxValue;
     //           TheJson = TheSerializer.Serialize(lstTrainingStatus);
     //       }
     //       return TheJson;
     //   }
     //   [WebMethod]
     //   public string GetTRSStatus()
     //   {
     //       var TheJson = "";
     //       List<TRSStatus> list = Sources.GetTRSStatus();
     //       if (list != null)
     //       {
     //           // instantiate a serializer
     //           JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
     //           //TheSerializer.MaxJsonLength = int.MaxValue;
     //           TheJson = TheSerializer.Serialize(list);
     //       }
     //       return TheJson;
     //   }
     //   [WebMethod]
     //   public string GetState()
     //   {
     //       var TheJson = "";
     //       List<StateIdentifier> lstOrganisation = Sources.GetState();
     //       if (lstOrganisation != null)
     //       {
     //           // instantiate a serializer
     //           JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
     //           //TheSerializer.MaxJsonLength = int.MaxValue;
     //           TheJson = TheSerializer.Serialize(lstOrganisation);
     //       }
     //       return TheJson;
     //   }
     //   [WebMethod]
     //   public string GetStatisticsItem(int TypeId)
     //   {
     //       var TheJson = "";
     //       using (StudentController sc = new StudentController())
     //       {
     //           List<tblStudentStatisticsItem> lstItems = sc.GetStatisticsItem(TypeId);
     //           if (lstItems != null)
     //           {
     //               // instantiate a serializer
     //               JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
     //               //TheSerializer.MaxJsonLength = int.MaxValue;
     //               TheJson = TheSerializer.Serialize(lstItems);
     //           }
     //       }
     //       return TheJson;
     //   }

        //[WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public Models.User GetCurrentUserInfo()
        //{
        //    Models.User clsUser = Sources.getCurrentUserDetails();
        //    return clsUser;
        //}

        //static object lockInsertClientErrorLog = new object();
        //[WebMethod]
        //public string InsertClientErrorLog(string userName, string url, string errMsg, string line)
        //{
        //    var TheJson = "";
        //    lock (lockInsertClientErrorLog)
        //    {
        //        try
        //        {
        //            // Insert
        //            bool bRet = Sources.InsertClientErrorLog(userName, url, errMsg, line);
        //            if (bRet == true)
        //            {
        //                TheJson = "ok";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex);
        //        }
        //    }
        //    return TheJson;
        //}
        //static object lockAddLog = new object();
        //[WebMethod]
        //public string Add_Log(string userName, string Log_Ref, string KeyId, string Log_Change)
        //{
        //    var TheJson = "";
        //    lock (lockAddLog)
        //    {
        //        try
        //        {
        //            bool bRet = LogController.Add_Log(Log_Ref, KeyId, Log_Change, userName);
        //            if (bRet == true)
        //            {
        //                TheJson = "ok";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            logger.Error(ex);
        //        }
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public string GetAcademicRecord()
        //{
        //    var TheJson = "";
        //    List<tblAcademicRecord> lstAcademicRecord = Sources.GetAcademicRecord();
        //    if (lstAcademicRecord != null)
        //    {
        //        // instantiate a serializer
        //        JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //        //TheSerializer.MaxJsonLength = int.MaxValue;
        //        TheJson = TheSerializer.Serialize(lstAcademicRecord);
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public string UpdateCensusDate()
        //{
        //    var TheJson = "";
        //    Utilities.UpdateAllCensusDate();
        //    return TheJson;
        //}

        //[WebMethod]
        //public string GetDiscretionaryFee(int CourseId)
        //{
        //    string DiscretionaryFee = "0";
        //    using (CourseController cc = new CourseController())
        //    {
        //        tblCourse tbl = cc.GetCourseById(CourseId);
        //        if (tbl.DiscretionaryFeeRequired == true)
        //        {
        //            DiscretionaryFee = Sources.GetValueFromMiscellaneousSetting("DiscretionaryFee");
        //        }
        //    }
        //    return DiscretionaryFee;
        //}

        //[WebMethod]
        //public string GetDiscretionaryFeeBySITY(int CourseId, List<string> InvoiceSITYIdList)
        //{
        //    string DiscretionaryFee = "0";
        //    using (CourseController cc = new CourseController())
        //    using (StudentIntakeTrainingYearController sityc = new StudentIntakeTrainingYearController())
        //    {
        //        tblCourse tbl = cc.GetCourseById(CourseId);
        //        int SITYId = (InvoiceSITYIdList.Count() == 0) ? 0 : int.Parse(InvoiceSITYIdList[0]);
        //        tblStudentIntakeTrainingYear tblSity = sityc.Get(SITYId);
        //        if (tbl.DiscretionaryFeeRequired == true && tblSity.DiscretionaryFeePaid == false)
        //        {
        //            DiscretionaryFee = Sources.GetValueFromMiscellaneousSetting("DiscretionaryFee");
        //        }
        //    }
        //    return DiscretionaryFee;
        //}

        //[WebMethod]
        //public string GetDiscountSoFarBySITY(int CourseId, List<string> InvoiceSITYIdList)
        //{
        //    string DiscountSoFar = "0";
        //    using (CourseController cc = new CourseController())
        //    using (StudentIntakeTrainingYearController sityc = new StudentIntakeTrainingYearController())
        //    {
        //        //tblCourse tbl = cc.GetCourseById(CourseId);
        //        //int SITYId = (InvoiceSITYIdList.Count() == 0) ? 0 : int.Parse(InvoiceSITYIdList[0]);
        //        //tblStudentIntakeTrainingYear tblSity = sityc.Get(SITYId);
        //        //if (tbl.DiscretionaryFeeRequired == true && tblSity.DiscretionaryFeePaid == false)
        //        //{
        //        //    DiscretionaryFee = Sources.GetValueFromMiscellaneousSetting("DiscretionaryFee");
        //        //}
        //    }
        //    return DiscountSoFar;
        //}

        //[WebMethod]
        //public string GetDiscretionaryFeeByParticipation(int CourseId, List<string> InvoiceParticipationIdList)
        //{
        //    string DiscretionaryFee = "0";
        //    using (CourseController cc = new CourseController())
        //    using (StudentIntakeTrainingYearController sityc = new StudentIntakeTrainingYearController())
        //    using (ParticipationController pc = new ParticipationController())
        //    using (FinanceController fc = new FinanceController())
        //    {
        //        // POId and Discretionary Fee
        //        foreach (string pid in InvoiceParticipationIdList)
        //        {
        //            if (!string.IsNullOrEmpty(pid))
        //            {
        //                int iPid = int.Parse(pid);
        //                tblParticipation tblP = pc.GetById(iPid);
        //                tblStudentIntakeTrainingYear tblSity = sityc.Get(tblP.SITYId);
        //                tblCourse tbl = cc.GetCourseById(CourseId);
        //                // If it hasn't paid, check the Concession Fee Type ID for each participation.
        //                // Condition: Does student type for the component(s) being invoiced have discretionary fee?
        //                // If the student has 1 component with fee type F and then another with fee type N
        //                // then we need to charge the discretionary fee (based on the unit with the N).
        //                bool bDiscretionaryCostFlg = fc.GetDiscretionaryCostFlg(tblP.ConcessionFeeTypeId);
        //                if (bDiscretionaryCostFlg && tbl.DiscretionaryFeeRequired == true && tblSity.DiscretionaryFeePaid == false)
        //                {
        //                    DiscretionaryFee = Sources.GetValueFromMiscellaneousSetting("DiscretionaryFee");
        //                }
        //            }
        //        }

        //    }
        //    return DiscretionaryFee;
        //}
        //[WebMethod]
        //public decimal GetDiscountSoFarByParticipation(int IntakeTrainingYearId, int CourseId, List<string> InvoiceParticipationIdList)
        //{
        //    decimal dDiscountSoFar = 0;
        //    using (StudentIntakeTrainingYearController sityc = new StudentIntakeTrainingYearController())
        //    using (ParticipationController pc = new ParticipationController())
        //    using (FinanceController fc = new FinanceController())
        //    using (IntakeTrainingYearController ityc = new IntakeTrainingYearController())
        //    using (StudentController sc = new StudentController())
        //    using (ClassController cc = new ClassController())
        //    {
        //        // Get the accumulated totals for this course for the TP (saved in SITY table)
        //        bool TotalCap = false;
        //        bool TotalFirstCap = false;
        //        bool DiscAdded = false;
        //        bool DiscFirstAdded = false;
        //        int SITYId = 0;
        //        int StudentId = 0;
        //        if (InvoiceParticipationIdList.Count() > 0)
        //        {
        //            tblParticipation p = pc.GetById(int.Parse(InvoiceParticipationIdList[0]));
        //            SITYId = p.SITYId;
        //            StudentId = p.StudentId;
        //        }
        //        if (SITYId > 0)
        //        {
        //            // Get the last ID
        //            int maxID = fc.getMaxStudentAccountItemId(SITYId);
        //            // Get the accumulated totals for this course for the TP (saved in SITY table)
        //            decimal RunningTotDisc = 0;
        //            decimal RunningTotForThisInvoice = 0;
        //            decimal TotFees = 0;
        //            decimal TotConcFee = 0;
        //            decimal PrevTotDisc = 0;
        //            if (maxID > 0)
        //            {
        //                RunningTotDisc = fc.get_TotalDiscount(SITYId, maxID);
        //                TotFees = fc.get_TotalFees(SITYId, maxID);
        //                TotConcFee = fc.get_ConcFees(SITYId, maxID);
        //            }
        //            tblStudentIntakeTrainingYear tblSITY = sityc.Get(SITYId);
        //            tblIntakeTrainingYear tblITY = ityc.GetIntakeTrainingYearInfo(tblSITY.IntakeTrainingYearId);
        //            tblTuitionFeeCourseCap capFull = fc.GetTuitionFeeCourseCapNoConcession(tblSITY.CourseId, tblITY.CalendarYear);
        //            tblTuitionFeeCourseCap capConc = fc.GetTuitionFeeCourseCapConcession(tblSITY.CourseId, tblITY.CalendarYear);
        //            tblTuitionFeeCourseCap capSecondary = fc.GetTuitionFeeCourseCapSecondary(tblSITY.CourseId, tblITY.CalendarYear);
        //            Student st = sc.GetByUid(StudentId);
        //            // Have we reached the Total fee cap
        //            decimal ucap = capFull.CappedFee;
        //            bool bYouth = fc.IsYouth(tblSITY.CourseId, tblITY.CalendarYear, st.DOB);
        //            if (bYouth)
        //            {
        //                ucap = capConc.CappedFee;
        //            }
        //            decimal lcap = capConc.CappedFee;
        //            // Secondary School student's fee cap.
        //            bool bSecondary = fc.IsSecondary(tblSITY.CourseId, tblITY.CalendarYear, st.DOB);
        //            if (bSecondary && capSecondary.CappedFee > 0)
        //            {
        //                ucap = capSecondary.CappedFee;
        //                lcap = capSecondary.CappedFee;
        //            }
        //            if (TotFees > ucap)
        //            {
        //                TotalCap = true;
        //            }

        //            foreach (string pid in InvoiceParticipationIdList)
        //            {
        //                if (!string.IsNullOrEmpty(pid))
        //                {
        //                    tblParticipation tblP = pc.GetById(int.Parse(pid));
        //                    // Step 2. Get Fee Cap for this item and check with dDiscountSoFar
        //                    List<tblTuitionFeeCourseCap> lstCaps = fc.GetTuitionFeeCourseCapByCourseIdAndYear(CourseId, tblITY.CalendarYear);
        //                    decimal dCappedFeeCost = fc.GetTuitionCappedFee(st, lstCaps, tblP.ConcessionFeeTypeId);
        //                    if (dCappedFeeCost > 0)
        //                    {
        //                        decimal LineTotDisc = 0;
        //                        // Step 3.
        //                        tblClass tblCls = cc.Get(tblP.ClassId);
        //                        tblComponent tblComp = ComponentController.GetComponent(tblCls.ComponentId);
        //                        string StartDateOfClass = ((DateTime)tblCls.StartDateTimeUTC).ToString("yyyy-MM-dd");
        //                        // Non-Concession Tuition Full Fee
        //                        decimal FullFee = fc.GetComponentCost_New(CourseId, tblCls.ComponentId, StartDateOfClass, 0, tblComp.NominalHours, 13);
        //                        // Concession Tuition Full Fee
        //                        decimal ConcFee = fc.GetComponentCost_New(CourseId, tblCls.ComponentId, StartDateOfClass, 1, tblComp.NominalHours, 13);
        //                        string ConcessionFeeTypeId = tblP.ConcessionFeeTypeId;
        //                        decimal dThisItemFee = (ConcessionFeeTypeId != "Z") ? ConcFee : FullFee;

        //                        /****** MARTIN CODE TO GET TotConcFee, TotFees AND TotDisc START ******/
        //                        // Consider the current figures
        //                        DiscAdded = false;
        //                        // Increase the total Fees (this always has to happen)
        //                        TotFees = TotFees + dThisItemFee;
        //                        // If this is the first time we pass the upper cap, this takes absolute preference
        //                        if (TotFees > ucap && (TotFees - ucap <= dThisItemFee))
        //                        {
        //                            PrevTotDisc = RunningTotDisc;
        //                            // Because this is the first time the TOTAL cap has been passed, the TOTAL DISCOUNT can only be what the student has paid in total - the total cap
        //                            RunningTotDisc = TotFees - ucap;
        //                            TotalCap = true;
        //                            DiscAdded = true;
        //                            LineTotDisc = RunningTotDisc - PrevTotDisc;
        //                        }
        //                        if (tblP.ConcessionFeeTypeId != "Z")
        //                        {
        //                            TotConcFee = TotConcFee + dThisItemFee; // increase the concession fees
        //                            if (!TotalCap) // The total fee cap has not been reached yet, if it has ignore the below as everything is now discount
        //                            {
        //                                if (TotConcFee > lcap) // After the current fee
        //                                {
        //                                    PrevTotDisc = RunningTotDisc;
        //                                    if (TotConcFee - lcap <= dThisItemFee) // This is the first time the conc fees have exceeded the cap
        //                                    {
        //                                        LineTotDisc = (TotConcFee - lcap);
        //                                    }
        //                                    else
        //                                    {
        //                                        LineTotDisc = dThisItemFee;
        //                                    }
        //                                    RunningTotDisc += LineTotDisc;
        //                                    DiscAdded = true;
        //                                }
        //                            }
        //                        }
        //                        if (TotFees > ucap && !DiscAdded) // Has the total fee cap been passed and no discount has been given yet
        //                        {
        //                            TotalCap = true;
        //                            PrevTotDisc = RunningTotDisc;
        //                            LineTotDisc = dThisItemFee;
        //                            RunningTotDisc += LineTotDisc;
        //                        }
        //                        RunningTotForThisInvoice += LineTotDisc;
        //                        /****** MARTIN CODE TO GET TotConcFee, TotFees AND TotDisc END ******/
        //                    }
        //                }
        //            }
        //            dDiscountSoFar = RunningTotForThisInvoice;
        //        }
        //    }
        //    return dDiscountSoFar;
        //}
        //[WebMethod]
        //public string GetCappedFeeCost(int CourseId, int IntakeTrainingYearId, int StudentId, string ConcessionTypeId)
        //{
        //    string CappedFeeCost = "0";
        //    using (StudentController sc = new StudentController())
        //    using (IntakeTrainingYearController ityc = new IntakeTrainingYearController())
        //    using (FinanceController fc = new FinanceController())
        //    {
        //        tblIntakeTrainingYear tblITY = ityc.GetIntakeTrainingYearInfo(IntakeTrainingYearId);
        //        // STEP 1. Check Capped Tuition Fee based on the year and Course ID.
        //        List<tblTuitionFeeCourseCap> lstCaps = fc.GetTuitionFeeCourseCapByCourseIdAndYear(CourseId, tblITY.CalendarYear);
        //        if (lstCaps.Count > 0)
        //        {
        //            Student st = sc.GetByUid(StudentId);
        //            // STEP 2. Check Student's Concession Type and DOB.
        //            decimal dCappedFeeCost = fc.GetTuitionCappedFee(st, lstCaps, ConcessionTypeId);
        //            if (dCappedFeeCost > 0)
        //            {
        //                CappedFeeCost = dCappedFeeCost.ToString();
        //            }
        //        }
        //    }
        //    return CappedFeeCost;
        //}
        //[WebMethod]
        //public string GetCappedFeeUsedCostByParticipation(int CourseId, int IntakeTrainingYearId, int StudentId, List<string> InvoiceParticipationIdList, string ConcessionTypeId)
        //{
        //    string AlreadySpentCap = "0";
        //    using (StudentController sc = new StudentController())
        //    using (IntakeTrainingYearController ityc = new IntakeTrainingYearController())
        //    using (FinanceController fc = new FinanceController())
        //    using (ParticipationController pc = new ParticipationController())
        //    {
        //        tblIntakeTrainingYear tblITY = ityc.GetIntakeTrainingYearInfo(IntakeTrainingYearId);
        //        // STEP 1. Check Capped Tuition Fee based on the year and Course ID.
        //        List<tblTuitionFeeCourseCap> lstCaps = fc.GetTuitionFeeCourseCapByCourseIdAndYear(CourseId, tblITY.CalendarYear);
        //        if (lstCaps.Count > 0)
        //        {
        //            Student st = sc.GetByUid(StudentId);
        //            // STEP 2. Check Student's Concession Type and DOB.
        //            decimal dCappedFeeCost = fc.GetTuitionCappedFee(st, lstCaps, ConcessionTypeId);
        //            if (dCappedFeeCost > 0)
        //            {
        //                // Step 3. Get Already spent cost and get the difference.
        //                if (InvoiceParticipationIdList.Count > 0)
        //                {
        //                    tblParticipation tblPc = pc.GetById(int.Parse(InvoiceParticipationIdList[0]));
        //                    decimal dAlreadySpentCost = fc.GetAlreadySpentCost(tblPc.SITYId);
        //                    AlreadySpentCap = dAlreadySpentCost.ToString();
        //                }
        //            }
        //        }
        //    }
        //    return AlreadySpentCap;
        //}

        //[WebMethod]
        //public string GetCappedFeeUsedCostBySITY(int CourseId, int IntakeTrainingYearId, int StudentId, List<string> InvoiceSITYIdList, string ConcessionTypeId)
        //{
        //    string AlreadySpentCap = "0";
        //    using (StudentController sc = new StudentController())
        //    using (IntakeTrainingYearController ityc = new IntakeTrainingYearController())
        //    using (FinanceController fc = new FinanceController())
        //    {
        //        tblIntakeTrainingYear tblITY = ityc.GetIntakeTrainingYearInfo(IntakeTrainingYearId);
        //        // STEP 1. Check Capped Tuition Fee based on the year and Course ID.
        //        List<tblTuitionFeeCourseCap> lstCaps = fc.GetTuitionFeeCourseCapByCourseIdAndYear(CourseId, tblITY.CalendarYear);
        //        if (lstCaps.Count > 0)
        //        {
        //            Student st = sc.GetByUid(StudentId);
        //            // STEP 2. Check Student's Concession Type and DOB.
        //            decimal dCappedFeeCost = fc.GetTuitionCappedFee(st, lstCaps, ConcessionTypeId);
        //            if (dCappedFeeCost > 0)
        //            {
        //                // Step 3. Get Already spent cost and get the difference.
        //                if (InvoiceSITYIdList.Count > 0)
        //                {
        //                    int SITYId = int.Parse(InvoiceSITYIdList[0]);
        //                    decimal dAlreadySpentCost = fc.GetAlreadySpentCost(SITYId);
        //                    AlreadySpentCap = dAlreadySpentCost.ToString();
        //                }
        //            }
        //        }
        //    }
        //    return AlreadySpentCap;
        //}
        //[WebMethod]
        //public bool IsYouth(int CourseId, int IntakeTrainingYearId, int StudentId)
        //{
        //    bool bYouth = false;
        //    using (FinanceController fc = new FinanceController())
        //    using (IntakeTrainingYearController ityc = new IntakeTrainingYearController())
        //    using (StudentController sc = new StudentController())
        //    {
        //        Student st = sc.GetByUid(StudentId);
        //        tblIntakeTrainingYear tblIty = ityc.GetIntakeTrainingYearInfo(IntakeTrainingYearId);
        //        bYouth = fc.IsYouth(CourseId, tblIty.CalendarYear, st.DOB);
        //    }
        //    return bYouth;
        //}
        //[WebMethod]
        //public bool IsSecondary(int CourseId, int IntakeTrainingYearId, int StudentId)
        //{
        //    bool bSecondary = false;
        //    using (FinanceController fc = new FinanceController())
        //    using (IntakeTrainingYearController ityc = new IntakeTrainingYearController())
        //    using (StudentController sc = new StudentController())
        //    {
        //        Student st = sc.GetByUid(StudentId);
        //        tblIntakeTrainingYear tblIty = ityc.GetIntakeTrainingYearInfo(IntakeTrainingYearId);
        //        bSecondary = fc.IsSecondary(CourseId, tblIty.CalendarYear, st.DOB);
        //    }
        //    return bSecondary;
        //}
        ////$http("/WebServices/utilService.asmx/IsConcession", { InvoiceParticipationIdList: gInvoicePidList })
        //[WebMethod]
        //public bool IsConcession(List<string> InvoiceParticipationIdList)
        //{
        //    bool bConcession = false;
        //    using (ParticipationController pc = new ParticipationController())
        //    {
        //        if (InvoiceParticipationIdList.Count() > 0)
        //        {
        //            tblParticipation tblPc = pc.GetById(int.Parse(InvoiceParticipationIdList[0]));
        //            if (tblPc.ConcessionFeeTypeId == "Z" || string.IsNullOrEmpty(tblPc.ConcessionFeeTypeId))
        //            {
        //                bConcession = false;
        //            }
        //            else
        //            {
        //                bConcession = true;
        //            }
        //        }

        //    }
        //    return bConcession;
        //}

        //[WebMethod]
        //public string GetInstructorsList()
        //{
        //    var TheJson = "";
        //    using (UserController u = new UserController())
        //    {
        //        var list = u.FindUserByField("group_id", "4");//instructors

        //        if (list != null)
        //        {
        //            TheJson = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //        }
        //    }

        //    return TheJson;
        //}
        //[WebMethod]
        //public string checkCurrentPassword(string password)
        //{
        //    var TheJson = "";
        //    using (UserController uc = new UserController())
        //    {
        //        User curUser = Sources.getCurrentUserDetails();
        //        if (curUser.user_password == password)
        //        {
        //            TheJson = "ok";
        //        }
        //        else
        //        {
        //            TheJson = "error";
        //        }
        //    }
        //    return new JavaScriptSerializer().Serialize(TheJson);

        //}
        //[WebMethod]
        //public string resetPassword(string password)
        //{
        //    var TheJson = "";
        //    using (UserController uc = new UserController())
        //    {
        //        User curUser = Sources.getCurrentUserDetails();
        //        bool bRet = uc.UpdatePassword(curUser.user_id, curUser.user_password, password);
        //        if (bRet == true)
        //        {
        //            TheJson = "ok";
        //        }
        //        else
        //        {
        //            TheJson = "error";
        //        }
        //    }
        //    return new JavaScriptSerializer().Serialize(TheJson);

        //}
        //[WebMethod]
        //public string GetStatisticsType()
        //{
        //    var TheJson = "";
        //    using (StudentController sc = new StudentController())
        //    {
        //        List<tblStudentStatisticsType> lstStatisticsType = sc.GetStatisticsType();
        //        if (lstStatisticsType.Count() % 2 != 0)
        //        {
        //            // Add empty
        //            tblStudentStatisticsType emp = new tblStudentStatisticsType();
        //            lstStatisticsType.Add(emp);

        //        }
        //        if (lstStatisticsType != null)
        //        {
        //            // instantiate a serializer
        //            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //            //TheSerializer.MaxJsonLength = int.MaxValue;
        //            TheJson = TheSerializer.Serialize(lstStatisticsType);
        //        }
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public string GetStatisticsTypeMenu()
        //{
        //    var TheJson = "";
        //    using (StudentController sc = new StudentController())
        //    {
        //        List<tblStudentStatisticsType> lstStatisticsType = sc.GetStatisticsType();
        //        if (lstStatisticsType != null)
        //        {
        //            // instantiate a serializer
        //            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //            //TheSerializer.MaxJsonLength = int.MaxValue;
        //            TheJson = TheSerializer.Serialize(lstStatisticsType);
        //        }
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public bool DeleteStatistics(int TypeId)
        //{
        //    bool bOK = false;
        //    using (StudentController sc = new StudentController())
        //    {
        //        bOK = sc.DeleteStatisticsType(TypeId);
        //    }
        //    return bOK;
        //}
        //[WebMethod]
        //public bool DeleteStatisticsItem(int ItemID)
        //{
        //    bool bOK = false;
        //    using (StudentController sc = new StudentController())
        //    {
        //        bOK = sc.DeleteStatisticsItem(ItemID);
        //    }
        //    return bOK;
        //}
        //[WebMethod]
        //public int InsertStatisticsType(string Name)
        //{
        //    int TypeId = 0;
        //    using (StudentController sc = new StudentController())
        //    {
        //        TypeId = sc.InsertStatisticsType(Name);
        //    }
        //    return TypeId;
        //}
        //[WebMethod]
        //public int InsertStatisticsItem(string Name, string Value, int TypeId)
        //{
        //    int ItemId = 0;
        //    using (StudentController sc = new StudentController())
        //    {
        //        ItemId = sc.InsertStatisticsItem(Name, Value, TypeId);
        //    }
        //    return ItemId;
        //}
        //[WebMethod]
        //public bool UpdateStatisticsType(string Name, int TypeId)
        //{
        //    bool bRet = false;
        //    using (StudentController sc = new StudentController())
        //    {
        //        bRet = sc.UpdateStatisticsType(Name, TypeId);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateStatisticsItem(string Name, string Value, int ItemID)
        //{
        //    bool bRet = false;
        //    using (StudentController sc = new StudentController())
        //    {
        //        bRet = sc.UpdateStatisticsItem(Name, Value, ItemID);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public string GetMiscellaneous()
        //{
        //    var TheJson = "";
        //    using (UtilController uc = new UtilController())
        //    {
        //        List<tblMiscellaneousSettings> lstMiscellaneousSettings = uc.GetMiscellaneous();
        //        if (lstMiscellaneousSettings != null)
        //        {
        //            // instantiate a serializer
        //            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //            //TheSerializer.MaxJsonLength = int.MaxValue;
        //            TheJson = TheSerializer.Serialize(lstMiscellaneousSettings);
        //        }
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public string GetSurveyContactStatus()
        //{
        //    var TheJson = "";
        //    using (UtilController uc = new UtilController())
        //    {
        //        List<tblSurveyContactStatus> lstSurveyContactStatus = uc.GetSurveyContactStatus();
        //        if (lstSurveyContactStatus != null)
        //        {
        //            // instantiate a serializer
        //            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //            //TheSerializer.MaxJsonLength = int.MaxValue;
        //            TheJson = TheSerializer.Serialize(lstSurveyContactStatus);
        //        }
        //    }
        //    return TheJson;
        //}

        //[WebMethod]
        //public string GetWorkingDaysYearsList(bool imported)
        //{
        //    var TheJson = "";
        //    using (UtilController uc = new UtilController())
        //    {
        //        List<tblWorkingDays_Years> lstYears = uc.GetWorkingDaysYearsList(imported);
        //        if (lstYears != null)
        //        {
        //            // instantiate a serializer
        //            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //            //TheSerializer.MaxJsonLength = int.MaxValue;
        //            TheJson = TheSerializer.Serialize(lstYears);
        //        }
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public string GetWorkingDaysByYear(string Year)
        //{
        //    var TheJson = "";
        //    using (DateController dc = new DateController())
        //    {
        //        string startDate = Year + "0101";
        //        string endDate = Year + "1231";

        //        List<tblWorkingDays> lstWorkDays = dc.GetWorkingDaysByYear(startDate, endDate);
        //        if (lstWorkDays != null)
        //        {
        //            // instantiate a serializer
        //            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //            //TheSerializer.MaxJsonLength = int.MaxValue;
        //            TheJson = TheSerializer.Serialize(lstWorkDays);
        //        }
        //    }
        //    return TheJson;
        //}
        //[WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string InsertGetWorkingDays(int Year, bool SatWorkday, bool SunWorkday, List<PublicHolidays> PublicHolidays)
        //{
        //    var TheJson = "";
        //    using (DateController dc = new DateController())
        //    {
        //        DateTime startDate = new DateTime(Year, 1, 1);
        //        DateTime endDate = new DateTime(Year, 12, 31);
        //        for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
        //        {
        //            bool bWorkday = dc.GetWorkDayFlag(dt, PublicHolidays, SatWorkday, SunWorkday);
        //            string strHoliday = dc.GetHolidayDesc(dt, PublicHolidays);
        //            tblWorkingDays day = new tblWorkingDays
        //            {
        //                Sort = dt.ToString("yyyyMMdd"),
        //                Date = dt,
        //                DOW = dt.DayOfWeek.ToString(),
        //                Workday = bWorkday,
        //                Holiday = strHoliday
        //            };
        //            dc.InsertWorkingDays(day);
        //        }
        //        // set "imported" for this year.
        //        dc.UpdateWorkingDaysYearImported(Year);
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public bool UpdateWorkday(string Sort, bool IsWorkday, string Holiday)
        //{
        //    bool bRet = false;
        //    using (DateController dc = new DateController())
        //    {
        //        bRet = dc.UpdateWorkday(Sort, IsWorkday, Holiday);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteAcademicRecord(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteAcademicRecord(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateAcademicRecord(int id, string Status)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateAcademicRecord(id, Status);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertAcademicRecord(string Status)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertAcademicRecord(Status);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateComponentAuthority(int id, string Name, string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateComponentAuthority(id, Name, Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertComponentAuthority(string Name, string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertComponentAuthority(Name, Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteComponentAuthority(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteComponentAuthority(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateAttendanceStatus(int id, string KeyCode, string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateAttendanceStatus(id, KeyCode, Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertAttendanceStatus(string KeyCode, string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertAttendanceStatus(KeyCode, Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteAttendanceStatus(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteAttendanceStatus(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateBankCreditCardOptions(int id, string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateBankCreditCardOptions(id, Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertBankCreditCardOptions(string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertBankCreditCardOptions(Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteBankCreditCardOptions(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteBankCreditCardOptions(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateConcessionFeeTypeIdentifier(string id, string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateConcessionFeeTypeIdentifier(id, Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertConcessionFeeTypeIdentifier(string id, string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertConcessionFeeTypeIdentifier(id, Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteConcessionFeeTypeIdentifier(string id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteConcessionFeeTypeIdentifier(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateContactType(int id, string ContactType, int DocTypeId)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateContactType(id, ContactType, DocTypeId);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertContactType(string ContactType, int DocTypeId)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertContactType(ContactType, DocTypeId);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteContactType(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteContactType(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateContactTypeDetail(int id, int ContactTypeId, string DetailName, int FundingModelId, bool SDUpload)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateContactTypeDetail(id, ContactTypeId, DetailName, FundingModelId, SDUpload);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertContactTypeDetail(int ContactTypeId, string DetailName, int FundingModelId, bool SDUpload)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertContactTypeDetail(ContactTypeId, DetailName, FundingModelId, SDUpload);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteContactTypeDetail(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteContactTypeDetail(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateDeliveryMethod(int id, string DeliveryMethod)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateDeliveryMethod(id, DeliveryMethod);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertDeliveryMethod(string DeliveryMethod)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertDeliveryMethod(DeliveryMethod);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteDeliveryMethod(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteDeliveryMethod(id);
        //    }
        //    return bRet;
        //}

        //[WebMethod]
        //public bool UpdateEmailSignatureLocation(int id, string LogoFile, string Company, string Street, string Suburb, string State, string Postcode,
        //    string POBoxStreet, string POBoxSuburb, string POBoxState, string POBoxPostCode, string RTO, string Telephone, string Fax, string Website, string ABN)
        //{
        //    bool bRet = false;
        //    using (EmailTemplateController etc = new EmailTemplateController())
        //    {
        //        tblEmailSignatureLocation esl = etc.GetSignatureLocationById(id);
        //        esl.LogoFile = LogoFile;
        //        esl.Company = Company;
        //        esl.Street = Street;
        //        esl.Suburb = Suburb;
        //        esl.State = State;
        //        esl.PostCode = (!string.IsNullOrEmpty(Postcode)) ? int.Parse(Postcode) : 0;
        //        esl.POBoxStreet = POBoxStreet;
        //        esl.POBoxSuburb = POBoxSuburb;
        //        esl.POBoxState = POBoxState;
        //        esl.POBoxPostCode = (!string.IsNullOrEmpty(POBoxPostCode)) ? int.Parse(POBoxPostCode) : 0;
        //        esl.RTO = RTO;
        //        esl.Telephone = Telephone;
        //        esl.Fax = Fax;
        //        esl.Website = Website;
        //        esl.ABN = ABN;
        //        bRet = etc.UpdateEmailSignatureLocation(esl);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertEmailSignatureLocation(string LogoFile, string Company, string Street, string Suburb, string State, string Postcode,
        //    string POBoxStreet, string POBoxSuburb, string POBoxState, string POBoxPostCode, string RTO, string Telephone, string Fax, string Website, string ABN)
        //{
        //    bool bRet = false;
        //    using (EmailTemplateController etc = new EmailTemplateController())
        //    {
        //        tblEmailSignatureLocation esl = new tblEmailSignatureLocation();
        //        esl.LogoFile = LogoFile;
        //        esl.Company = Company;
        //        esl.Street = Street;
        //        esl.Suburb = Suburb;
        //        esl.State = State;
        //        esl.PostCode = (!string.IsNullOrEmpty(Postcode)) ? int.Parse(Postcode) : 0;
        //        esl.POBoxStreet = POBoxStreet;
        //        esl.POBoxSuburb = POBoxSuburb;
        //        esl.POBoxState = POBoxState;
        //        esl.POBoxPostCode = (!string.IsNullOrEmpty(POBoxPostCode)) ? int.Parse(POBoxPostCode) : 0;
        //        esl.RTO = RTO;
        //        esl.Telephone = Telephone;
        //        esl.Fax = Fax;
        //        esl.Website = Website;
        //        esl.ABN = ABN;
        //        int Id = etc.InsertEmailSignatureLocation(esl);
        //        if (Id > 0)
        //        {
        //            bRet = true;
        //        }
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteEmailSignatureLocation(int id)
        //{
        //    bool bRet = false;
        //    using (EmailTemplateController etc = new EmailTemplateController())
        //    {
        //        bRet = etc.DeleteEmailSignatureLocation(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateIndentureStatus(int id, string Status)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateIndentureStatus(id, Status);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertIndentureStatus(string Status)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertIndentureStatus(Status);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteIndentureStatus(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteIndentureStatus(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateMiscellaneousSettings(int id, string Name, string Value)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateMiscellaneousSettings(id, Name, Value);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertMiscellaneousSettings(string Name, string Value)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertMiscellaneousSettings(Name, Value);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteMiscellaneousSettings(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteMiscellaneousSettings(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateQualificationType(int id, string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateQualificationType(id, Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertQualificationType(string Description)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertQualificationType(Description);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteQualificationType(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteQualificationType(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateStudentType(int id, string StudentType, string Name, string IntakeNameToAdd, string NameConvention, bool SchoolBased)
        //{
        //    bool bRet = false;
        //    using (StudentTypeController stc = new StudentTypeController())
        //    {
        //        tblStudentType tblSt = stc.GetBy(id);
        //        tblSt.StudentType = StudentType;
        //        tblSt.LongName = Name;
        //        tblSt.IntakeNameToAdd = IntakeNameToAdd;
        //        tblSt.NameConvention = NameConvention;
        //        tblSt.SchoolBased = SchoolBased;
        //        bRet = stc.UpdateTblStudentType(tblSt);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertStudentType(string StudentType, string Name, string IntakeNameToAdd, string NameConvention, bool SchoolBased)
        //{
        //    bool bRet = false;
        //    using (StudentTypeController stc = new StudentTypeController())
        //    {
        //        tblStudentType tblSt = new tblStudentType();
        //        tblSt.StudentTypeId = stc.GetMaxStudentTypeId();
        //        tblSt.StudentType = StudentType;
        //        tblSt.LongName = Name;
        //        tblSt.IntakeNameToAdd = IntakeNameToAdd;
        //        tblSt.NameConvention = NameConvention;
        //        tblSt.SchoolBased = SchoolBased;
        //        bRet = stc.InsertTblStudentType(tblSt);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteStudentType(int id)
        //{
        //    bool bRet = false;
        //    using (StudentTypeController stc = new StudentTypeController())
        //    {
        //        bRet = stc.DeleteTblStudentType(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateSurveyContactStatus(string Value, string Desc)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateSurveyContactStatus(Value, Desc);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertSurveyContactStatus(string Value, string Desc)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertSurveyContactStatus(Value, Desc);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteSurveyContactStatus(string id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteSurveyContactStatus(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateTrainingOrganisation(int id, string Name, int Type, string Street, string Street2, string Suburb, string Postcode, string State, string RegNo, string Website)
        //{
        //    bool bRet = false;
        //    using (CourseController cc = new CourseController())
        //    {
        //        tblTrainingOrganisation tblTO = cc.GetOrganisationById(id);
        //        tblTO.TrainingOrganisationName = Name;
        //        tblTO.Type = Type;
        //        tblTO.AddressLine1 = Street;
        //        tblTO.AddressLine2 = Street2;
        //        tblTO.AddressLocation = Suburb;
        //        tblTO.Postcode = Postcode;
        //        tblTO.StateIdentifier = Sources.GetStateId(State);
        //        tblTO.RegisteredNumber = RegNo;
        //        tblTO.WebsiteURL = Website;
        //        bRet = cc.UpdateTrainingOrganisation(tblTO);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertTrainingOrganisation(int Id, string Name, int Type, string Street, string Street2, string Suburb, string Postcode, string State, string RegNo, string Website)
        //{
        //    bool bRet = false;
        //    using (CourseController cc = new CourseController())
        //    {
        //        tblTrainingOrganisation table = new tblTrainingOrganisation
        //        {
        //            TrainingOrganisationId = Id,
        //            TrainingOrganisationName = Name,
        //            Type = Type,
        //            AddressLine1 = Street,
        //            AddressLine2 = Street2,
        //            AddressLocation = Suburb,
        //            Postcode = Postcode,
        //            StateIdentifier = Sources.GetStateId(State),
        //            RegisteredNumber = RegNo,
        //            WebsiteURL = Website
        //        };
        //        bRet = cc.InsertTrainingOrganisation(table);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteTrainingOrganisation(int id)
        //{
        //    bool bRet = false;
        //    using (CourseController cc = new CourseController())
        //    {
        //        bRet = cc.DeleteTrainingOrganisation(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateTrainingOrganisationDeliveryLocation(int id, string Name, int OrganizationId, string Address,
        //    string Suburb, string State, string Postcode, string Country, string GLCode, string CampusManager,
        //    string CampusCoordinator, string ContactNumber, string Email)
        //{
        //    bool bRet = false;
        //    using (CourseController cc = new CourseController())
        //    {
        //        tblTrainingOrganisationDeliveryLocation tblTODL = cc.GetOrganisationDeliveryLocationById(id);
        //        tblTODL.DeliveryLocationName = Name;
        //        tblTODL.TrainingOrganisationId = OrganizationId.ToString();
        //        if (OrganizationId > 0)
        //        {
        //            tblTrainingOrganisation org = cc.GetOrganisationById(OrganizationId);
        //            tblTODL.TrainingOrganisation = org.TrainingOrganisationName;
        //        }
        //        tblTODL.AddressLocation = Address;
        //        tblTODL.Suburb = Suburb;
        //        tblTODL.StateIdentifier = State;
        //        tblTODL.Postcode = Postcode;
        //        tblTODL.CountryIdentifier = Country;
        //        tblTODL.GLCode = GLCode;
        //        tblTODL.CampusManager = CampusManager;
        //        tblTODL.CampusCoordinator = CampusCoordinator;
        //        tblTODL.ContactNumber = ContactNumber;
        //        tblTODL.Email = Email;
        //        bRet = cc.UpdateTrainingOrganisationDeliveryLocation(tblTODL);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertTrainingOrganisationDeliveryLocation(string Name, int OrganizationId, string Address, string Suburb,
        //    string State, string Postcode, string Country, string GLCode, string CampusManager, string CampusCoordinator, string ContactNumber, string Email)
        //{
        //    bool bRet = false;
        //    using (CourseController cc = new CourseController())
        //    {
        //        string OrgName = "";
        //        if (OrganizationId > 0)
        //        {
        //            tblTrainingOrganisation org = cc.GetOrganisationById(OrganizationId);
        //            if (org != null)
        //            {
        //                OrgName = org.TrainingOrganisationName;
        //            }
        //        }
        //        tblTrainingOrganisationDeliveryLocation table = new tblTrainingOrganisationDeliveryLocation
        //        {
        //            DeliveryLocationId = cc.GetMaxDeliveryLocationId(),
        //            DeliveryLocationName = Name,
        //            TrainingOrganisationId = OrganizationId.ToString(),
        //            TrainingOrganisation = OrgName,
        //            AddressLocation = Address,
        //            Suburb = Suburb,
        //            StateIdentifier = State,
        //            Postcode = Postcode,
        //            CountryIdentifier = Country,
        //            GLCode = GLCode,
        //            CampusManager = CampusManager,
        //            CampusCoordinator = CampusCoordinator,
        //            Active = true,
        //            ContactNumber = ContactNumber,
        //            Email = Email
        //        };
        //        int ID = cc.InsertTrainingOrganisationDeliveryLocation(table);
        //        if (ID > 0)
        //        {
        //            bRet = true;
        //        }
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteTrainingOrganisationDeliveryLocation(int id)
        //{
        //    bool bRet = false;
        //    using (CourseController cc = new CourseController())
        //    {
        //        bRet = cc.DeleteTrainingOrganisationDeliveryLocation(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public string GetCountry()
        //{
        //    var TheJson = "";
        //    List<CountryIdentifier> lstCountry = Sources.GetCountry();
        //    if (lstCountry != null)
        //    {
        //        // instantiate a serializer
        //        JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
        //        //TheSerializer.MaxJsonLength = int.MaxValue;
        //        TheJson = TheSerializer.Serialize(lstCountry);
        //    }
        //    return TheJson;
        //}
        //[WebMethod]
        //public bool UpdateTrainingStatus(int id, string Status)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateTrainingStatus(id, Status);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertTrainingStatus(string Status)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertTrainingStatus(Status);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteTrainingStatus(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteTrainingStatus(id);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool UpdateTRSStatus(int id, string Status)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.UpdateTRSStatus(id, Status);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool InsertTRSStatus(string Status)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.InsertTRSStatus(Status);
        //    }
        //    return bRet;
        //}
        //[WebMethod]
        //public bool DeleteTRSStatus(int id)
        //{
        //    bool bRet = false;
        //    using (AdminController ac = new AdminController())
        //    {
        //        bRet = ac.DeleteTRSStatus(id);
        //    }
        //    return bRet;
        //}

        // Functions
    }
}
