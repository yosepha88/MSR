using MSRApi.Controllers;
using MSRApi.WEBBL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Tests.TestMethods
{
    public class WebTest
    {
        string[] ValidMonths;

        [SetUp]
        public void Setup()
        {
            ValidMonths = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        }

        [Test]
        public void ValidateMonth()
        {
            try
            {
                WebBL webBL = new WebBL();
                var monthYearList = webBL.GetYearAndMonths().Result;
                Assert.IsTrue(monthYearList.Months.Count > 0);

                if (monthYearList.Months != null)
                {
                    foreach (var dbMonthItem in monthYearList.Months)
                    {
                        foreach (var arrMItem in ValidMonths)
                        {
                            if (dbMonthItem.MonthName == arrMItem)
                            {
                                Assert.IsTrue(true);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.IsFalse(false);
            }

        }

        [Test]
        public void ValidateYear()
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

        [Test]
        public void ValidatePublicSiteData()
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
                        for (yearMin = yearMin; yearMin < yearMax; yearMin++)
                        {
                            if (dbYearItem.Year.Contains(yearMin.ToString()))
                            {
                                WebBL webNewBL = new WebBL();
                                var publicDataList = webNewBL.GetPublicSiteData(dbMonthItem.Id, Convert.ToInt32(dbYearItem.Year)).Result;
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

        [Test]
        public void ValidatePrivateSiteData()
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
                                var publicDataList = webNewBL.GetWebPrivateData(dbMonthItem.Id, Convert.ToInt32(dbYearItem.Year)).Result;
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

        [Test]
        public void ValidateHelpDeskTickets()
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

        [Test]
        public void ValidateWebTrendReport()
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

    }
}
