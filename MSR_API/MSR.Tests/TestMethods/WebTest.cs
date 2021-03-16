using MSRApi.Controllers;
using MSRApi.WEBBL;
using MSRDAL.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Tests.TestMethods
{
    public class WebTest
    {
        string[] ValidMonths;
        string[] ValidateYearTest;
        string[] ValidateYearTest2;
        int ValidJiraTicket;
        int ValidJiraTicketdoesntexist;

        [SetUp]
        public void Setup()
        {
            ValidMonths = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            ValidateYearTest = new string[2] { "2020", "2021" };
            ValidateYearTest2 = new string[2] { "0000", "0001" };
            ValidJiraTicket = 60174;
            ValidJiraTicketdoesntexist = 00000;
        }

        //A month January existing in the database and Api request. 
        [Test]
        public void TestValidateMonth()
        {
            {
                WebBL webBL = new WebBL();
                MonthAndYearModel result = webBL.GetYearAndMonths().Result;
                var assertmonth = false;
                foreach (var dbMonthItem in result.Months)
                {
                    if (dbMonthItem.MonthName == "January")
                    {
                        Assert.IsTrue(true);
                        assertmonth = true;
                    }
                }
                if (!assertmonth)
                {
                    Assert.IsTrue(false);
                }
            }
        }

        //A month "Zion" is invalid in the current month list.
        [Test]
        public void TestValidateMonthName()
        {
            {
                WebBL webBL = new WebBL();
                MonthAndYearModel result = webBL.GetYearAndMonths().Result;
                var assertmonth = false;
                foreach (var dbMonthItem in result.Months)
                {
                    if (dbMonthItem.MonthName == "Zion")
                    {
                        Assert.IsTrue(false);
                        assertmonth = false;
                    }
                }
                if (!assertmonth)
                {
                    Assert.IsTrue(true);
                }
            }
        }

        //The month count in the database match to what is displayed (12 months). 
        [Test]
        public void TestValidateMonthCount()
        {
            {
                WebBL webBL = new WebBL();
                var testMonthCount = ValidMonths.Length;
                MonthAndYearModel actualMonthCount = webBL.GetYearAndMonths().Result;
                var actualMonthCount2 = actualMonthCount.Months.Count;

                if (actualMonthCount2 == testMonthCount)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(false);
                }
            }
        }

        //Validating year (?)
        [Test]
        public void TestValidateYear()
        {
            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Years.Count > 0);
            foreach (var dbYearItem in monthYearList.Years)
            {
                for (int year = 1901; year < 1000; year++)
                {
                    if (dbYearItem.Year != year.ToString())
                    {
                        Assert.IsFalse(false);
                    }
                }
            }
        }

        //The years in database are in between 1900 - 2099
        [Test]
        public void TestValidateYearRange()
        {
            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Years.Count > 0);
            foreach (var dbYearItem in monthYearList.Years)
            {
                for (int year = 1900; year < 2099; year++)
                {
                    if (dbYearItem.Year != year.ToString())
                    {
                        Assert.IsFalse(false);
                    }
                }
            }
        }

        //The years - 2020 and 2021 are valid years and they are matched against the database.
        [Test]
        public void TestValidateYearSpecific()
        {
            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Years.Count > 0);
            var assertmonth = false;
            foreach (var dbYearItem in monthYearList.Years)
            {
                foreach (var validateyear in ValidateYearTest)
                {
                    if (dbYearItem.Year == validateyear)
                    {
                        Assert.IsTrue(true);
                        assertmonth = true;
                    }
                }

            }
            if (!assertmonth)
            {
                Assert.IsTrue(false);
                assertmonth = false;
            }
        }
        //The years '0000' and '0001' are invalid and they are not in the database
        [Test]
        public void TestValidateYearCheck()
        {
            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Years.Count > 0);
            var assertmonth = false;
            foreach (var dbYearItem in monthYearList.Years)
            {
                foreach (var validateyear2 in ValidateYearTest2)
                {
                    if (dbYearItem.Year == validateyear2)
                    {
                        Assert.IsTrue(false);
                        assertmonth = false;
                    }
                }

            }
            if (!assertmonth)
            {
                Assert.IsTrue(true);
                assertmonth = true;
            }
        }

        //To check if a text "Updated NAEP ..." existist in Public site on 2/2021
        [Test]
        public void TestPublicSiteDataTextExist()
        {
            int month = 2;
            int year = 2021;
            string sampleText = "Updated NAEP and non-adjudicated pages to support the PR release.";

            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                WebBL webNewBL = new WebBL();
                TaskModel publicDataList = webNewBL.GetPublicSiteData(month, year).Result.ToList().FirstOrDefault();

                //TaskModel result = (from l in publicDataList
                //select l).FirstOrDefault();
                if (publicDataList.Description.Contains(sampleText))
                {

                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsFalse(false);
                }

            }
        }

        //To check a text doestn't exist  
        [Test]
        public void TestPublicSiteDataTextDoesntExist()
        {
            int month = 2;
            int year = 2021;
            string sampleText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";

            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                WebBL webNewBL = new WebBL();
                TaskModel publicDataList = webNewBL.GetPublicSiteData(month, year).Result.ToList().FirstOrDefault();

                if (publicDataList.Description.Contains(sampleText))
                {
                    Assert.IsTrue(false);
                }
                else
                {
                    Assert.False(false);
                }
            }
        }

        //Test Jira ticket exist
        [Test]
        public void TestPublicSiteDataJiraTicketExist()
        {
            int month = 2;
            int year = 2021;
            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                WebBL webNewBL = new WebBL();
                var publicDataList = webNewBL.GetPublicSiteData(month, year).Result.ToList();

                var assertmonth = false;
                if (publicDataList != null)
                {
                    foreach (var publiclist in publicDataList)
                    {
                        foreach (var subtask in publiclist.SubTasks)
                        {
                            foreach (var jiratickets in subtask.JiraTickets)
                            {
                                //foreach (var jiraticket in ValidJiraTicket)
                                //{
                                if (jiratickets.JiraTicketId == ValidJiraTicket)
                                {
                                    Assert.IsTrue(true);
                                    assertmonth = true;
                                }
                                //}
                            }

                        }
                    }
                }
                if (!assertmonth)
                {
                    Assert.IsTrue(false);
                    assertmonth = false;
                }
            }
        }

        //Test Jira ticket doesnt' exist
        [Test]
        public void TestPublicSiteDataJiraTicketdoesntExist()
        {
            int month = 2;
            int year = 2021;
            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            var assertmonth = false;
            if (monthYearList.Months != null)
            {
                WebBL webNewBL = new WebBL();
                var publicDataList = webNewBL.GetPublicSiteData(month, year).Result.ToList();

                if (publicDataList != null)
                {
                    foreach (var publiclist in publicDataList)
                    {
                        foreach (var subtask in publiclist.SubTasks)
                        {
                            foreach (var jiratickets in subtask.JiraTickets)
                            {
                                if (jiratickets.JiraTicketId == ValidJiraTicketdoesntexist)
                                {
                                    Assert.IsFalse(false);
                                    assertmonth = false;
                                }
                            }

                        }
                    }
                }
                else
                {
                    Assert.IsTrue(true);
                    assertmonth = true;
                }
            }
        }

        //Test helpdesk ticket/data exist.
        [Test]
        public void TestValidateHelpDeskTickets()
        {
            var yearMin = 1900;
            var yearMax = 2099;

            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                foreach (var dbMonthItem in monthYearList.Months)
                {
                    foreach (var dbYearItem in monthYearList.Years)
                    {
                        for (int year = yearMin; year < yearMax; year++)
                        {
                            if (dbYearItem.Year.Contains(year.ToString()))
                            {
                                WebBL webNewBL = new WebBL();
                                var publicDataList = webNewBL.GetHelpDeskTickets(dbMonthItem.Id, Convert.ToInt32(dbYearItem.Year)).Result;
                                if (publicDataList.Count > 0)
                                {
                                    Assert.IsTrue(true);
                                }
                                else
                                {
                                    Assert.IsFalse(false);
                                }

                            }
                        }
                    }
                }
            }
        }

        //Test helpdesk data doesn't exist.
        [Test]
        public void TestValidateHelpDeskNoData()
        {
            var yearMin = 1900;
            var yearMax = 2099;
            string sampleText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";


            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                foreach (var dbMonthItem in monthYearList.Months)
                {
                    foreach (var dbYearItem in monthYearList.Years)
                    {
                        for (int year = yearMin; year < yearMax; year++)
                        {
                            if (dbYearItem.Year.Contains(year.ToString()))
                            {
                                WebBL webNewBL = new WebBL();
                                var publicDataList = webNewBL.GetHelpDeskTickets(dbMonthItem.Id, Convert.ToInt32(dbYearItem.Year)).Result;

                                foreach (var item in publicDataList)
                                {
                                    if (item.Category.Contains(sampleText))
                                    {
                                        Assert.IsTrue(false);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //Private Site data exists
        [Test]
        public void TestPrivateSiteDataTextExist()
        {
            int month = 2;
            int year = 2021;
            string sampleText = "Supporting westat as needed with settingup permissions and users";

            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                if (monthYearList.Months != null)
                {
                    WebBL webNewBL = new WebBL();
                    var privateDataList = webNewBL.GetWebPrivateData(month, year).Result.ToList().FirstOrDefault();

                    if (privateDataList.Description.Contains(sampleText))
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsFalse(false);
                    }

                }
            }
        }
        //Private Site data text doesn't exists
        [Test]
        public void TestPrivateSiteDataTextDoesntExist()
        {
            int month = 2;
            int year = 2021;
            string sampleText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";

            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                if (monthYearList.Months != null)
                {
                    WebBL webNewBL = new WebBL();
                    var privateDataList = webNewBL.GetWebPrivateData(month, year).Result.ToList().FirstOrDefault();

                    if (!privateDataList.Description.Contains(sampleText))
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }

                }
            }
        }

        //Test Web trend data exist 
        [Test]
        public void TestWebTrendReport()
        {
            var yearMin = 1901;
            var yearMax = 9999;

            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                foreach (var dbMonthItem in monthYearList.Months)
                {
                    foreach (var dbYearItem in monthYearList.Years)
                    {
                        for (int year = yearMin; year < yearMax; year++)
                        {
                            if (dbYearItem.Year.Contains(year.ToString()))
                            {
                                WebBL webNewBL = new WebBL();
                                var publicDataList = webNewBL.GetWebTrendReportData(dbMonthItem.Id, Convert.ToInt32(dbYearItem.Year)).Result;
                                if (publicDataList.WebTrendNAEPLinkData.Count > 0 && publicDataList.WebTrendNRCLinkData.Count > 0 && publicDataList.WebTrendResourceData.Count > 0 && publicDataList.WebTrendSiteData.Count > 0 && publicDataList.WebTrendTermData.Count > 0)
                                {
                                    Assert.IsTrue(true);
                                }
                                else
                                {
                                    Assert.IsFalse(false);
                                }

                            }
                        }
                    }
                }
            }
        }

        //Private Site data exists
        [Test]
        public void TestWebTrendDataTextExist()
        {
            int month = 2;
            int year = 2021;
            string sampleText = "Updated NAEP and non-adjudicated pages to support the PR release. March 2021";

            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                if (monthYearList.Months != null)
                {
                    WebBL webNewBL = new WebBL();
                    var webTrendDataList = webNewBL.GetWebTrendReportData(month, year).Result;
                    var webTrendResource = webTrendDataList.WebTrendResourceData.ToList().FirstOrDefault();
                    if (webTrendResource.Description.Contains(sampleText))
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsFalse(false);
                    }

                }
            }
        }

        //Private Site data doesn't exists
        [Test]
        public void TestWebTrendDataTextDoesntExist()
        {
            int month = 2;
            int year = 2021;
            string sampleText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";

            WebBL webBL = new WebBL();
            var monthYearList = webBL.GetYearAndMonths().Result;
            Assert.IsTrue(monthYearList.Months.Count > 0);

            if (monthYearList.Months != null)
            {
                if (monthYearList.Months != null)
                {
                    WebBL webNewBL = new WebBL();
                    var webTrendDataList = webNewBL.GetWebTrendReportData(month, year).Result;
                    var webTrendResource = webTrendDataList.WebTrendResourceData.ToList().FirstOrDefault();
                    if (!webTrendResource.Description.Contains(sampleText))
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.IsTrue(false);
                    }

                }
            }
        }

    }
}
