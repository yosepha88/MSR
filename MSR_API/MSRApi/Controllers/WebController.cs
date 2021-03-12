using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSR.DAL.Entities;
using MSR.DAL.Models;
using MSRApi.Helper;
using MSRApi.WEBBL;
using MSRDAL.Models;
using TaskStatus = MSR.DAL.Entities.TaskStatus;
using Type = MSR.DAL.Entities.Type;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSRApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class WebController : ControllerBase
    {

        [HttpGet]
        [Route("GetWebData")]
        public async Task<IActionResult> GetWebData(int Month, int Year)
        {
            var response = new List<TaskModel>();
            try
            {
                XMLConverter.ConvertWordtoXML();

                response = await new WebBL().GetPublicSiteData(Month, Year);
            }
            catch (Exception ex)
            {
               // LogManager.Logger.WriteException("WebController", "GetWebData", ex.Message, ex);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetYearAndMonths")]
        public async Task<IActionResult> GetYearAndMonths()
        {
            var response = new MonthAndYearModel();
            try
            {
                response = await new WebBL().GetYearAndMonths();
            }
            catch (Exception ex)
            {
               // LogManager.Logger.WriteException("WebController", "GetYearAndMonths", ex.Message, ex);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetHelpDeskTickets")]
        public async Task<IActionResult> GetHelpDeskTickets(int Month, int Year)
        {
            var response = new List<HelpDeskTicket>();
            try
            {
                response = await new WebBL().GetHelpDeskTickets(Month, Year);
            }
            catch (Exception ex)
            {
                //LogManager.Logger.WriteException("WebController", "GetHelpDeskTickets", ex.Message, ex);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetWebTrendReportData")]
        public async Task<IActionResult> GetWebTrendReportData(int Month, int Year)
        {
            var response = new WebTrend();
            try
            {
                response = await new WebBL().GetWebTrendReportData(Month, Year);
            }
            catch (Exception ex)
            {
                //LogManager.Logger.WriteException("WebController", "GetWebTrendReportData", ex.Message, ex);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetWebPrivateData")]
        public async Task<IActionResult> GetWebPrivateData(int Month, int Year)
        {
            var response = new List<PrivateTaskModel>();
            try
            {
                response = await new WebBL().GetWebPrivateData(Month, Year);
            }
            catch (Exception ex)
            {
                //LogManager.Logger.WriteException("WebController", "GetWebPrivateData", ex.Message, ex);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("GetTaskType")]
        public async Task<IActionResult> GetTaskType()
        {
            List<Type> Typedata = new List<Type>();
            try
            {
                Typedata = await new WebBL().GetTaskType();
            }
            catch (Exception ex)
            {
                // LogManager.Logger.WriteException("WebController", "GetYearAndMonths", ex.Message, ex);
            }
            return Ok(Typedata);
        }
        [HttpGet]
        [Route("GetTaskTaskStatus")]
        public async Task<IActionResult> GetTaskTaskStatus()
        {
            List<TaskStatus> Typedata = new List<TaskStatus>();
            try
            {
                Typedata = await new WebBL().GetTaskTaskStatus();
            }
            catch (Exception ex)
            {
                // LogManager.Logger.WriteException("WebController", "GetYearAndMonths", ex.Message, ex);
            }
            return Ok(Typedata);
        }
        [HttpPost]
        [Route("AddTaskAndSubTask")]
        public async Task<IActionResult> AddTaskAndSubTask(TaskAndSubTaskModel taskdetails)
        {
            ResponceModel responseModel = new ResponceModel();
            try
            {
                responseModel = await new WebBL().AddTaskAndSubTask(taskdetails);
            }
            catch (Exception ex)
            {
                // LogManager.Logger.WriteException("WebController", "GetYearAndMonths", ex.Message, ex);
            }
            return Ok(responseModel);
        }
    }
}
