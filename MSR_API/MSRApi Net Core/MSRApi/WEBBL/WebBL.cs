using MSR.DAL.Entities;
using MSRDAL.AutoMapper;
using MSRDAL.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Type = MSR.DAL.Entities.Type;
using AutoMapper;
using TaskStatus = MSR.DAL.Entities.TaskStatus;
using MSR.DAL.Models;
using Task = MSR.DAL.Entities.Task;

namespace MSRApi.WEBBL
{
    public class WebBL
    {
        public async Task<List<TaskModel>> GetPublicSiteData(int Month, int Year)
        {
            using (var ctx = new DbEntities())
            {
                var webData = await ctx.Tasks.Where(x => x.MonthId == Month && x.Year == Year && x.Type == 1).Include(x => x.SubTasks).ThenInclude(xy => xy.JiraTickets).ToListAsync();

                var autoMapperConfig = new AutoMapperConfiguration().Configure();
                var iMapper = autoMapperConfig.CreateMapper();

                return iMapper.Map<List<MSR.DAL.Entities.Task>, List<TaskModel>>(webData);
            }
        }
        public async Task<MonthAndYearModel> GetYearAndMonths()
        {
            var response = new MonthAndYearModel();

            using (var ctx = new DbEntities())
            {
                var autoMapperConfig = new AutoMapperConfiguration().Configure();
                var iMapper = autoMapperConfig.CreateMapper();

                response.Months = iMapper.Map<List<Month>, List<MonthModel>>(await ctx.Months.ToListAsync());
                response.Years = iMapper.Map<List<Years>, List<YearModel>>(await ctx.Years.ToListAsync());
            }
            return response;
        }
        public async Task<List<HelpDeskTicket>> GetHelpDeskTickets(int Month, int Year)
        {
            List<HelpDeskTicket> list = new List<HelpDeskTicket>();

            using (var ctx = new DbEntities())
            {
                var helpTicketData = await ctx.HelpDeskTickets.Where(x => x.Month == Month && x.Year == Year).ToListAsync();

                if (helpTicketData != null)
                {
                    foreach (var item in helpTicketData)
                    {
                        HelpDeskTicket obj = new HelpDeskTicket();
                        obj.Id = item.Id;
                        obj.Category = item.Category;
                        obj.Value = item.Value;
                        list.Add(obj);
                    }
                }
                return list;
            }
        }
        public async Task<WebTrend> GetWebTrendReportData(int Month, int Year)
        {
            WebTrend list = new WebTrend();

            using (var ctx = new DbEntities())
            {
                var StatusList = await ctx.TaskStatuses.ToListAsync();

                var WebTrendSiteList = await ctx.WebTrendSiteData.Where(x => x.IsActive == true && x.Month == Month && x.Year == Year).ToListAsync();

                var webTrendSiteData = (from a in WebTrendSiteList
                                        from b in StatusList.Where(b => a.Status == b.Id)
                                        select new
                                        {
                                            a.Name,
                                            a.Description,
                                            Status = b.Description,
                                            b.Icon
                                        });

                if (webTrendSiteData != null)
                {
                    List<WebTrendSiteDataModel> SiteDataList = new List<WebTrendSiteDataModel>();
                    foreach (var item in webTrendSiteData)
                    {
                        WebTrendSiteDataModel sitedata = new WebTrendSiteDataModel();
                        sitedata.Name = item.Name;
                        sitedata.Description = item.Description;
                        sitedata.Status = item.Status;
                        sitedata.Icon = item.Icon;
                        SiteDataList.Add(sitedata);
                    }
                    list.WebTrendSiteData = SiteDataList;
                }

                var WebTrendLinkList = await ctx.WebTrendLinkData.Where(x => x.IsActive == true && x.Month == Month && x.Year == Year).ToListAsync();

                var webTrendNAEPLinkData = (from a in WebTrendLinkList
                                            from b in StatusList.Where(b => a.Status == b.Id)
                                            where a.Status == 6
                                            select new
                                            {
                                                a.Link,
                                                Status = b.Description,
                                                b.Icon
                                            });

                if (webTrendNAEPLinkData != null)
                {
                    List<WebTrendNAEPLinkDataModel> LinkDataList = new List<WebTrendNAEPLinkDataModel>();
                    foreach (var item in webTrendNAEPLinkData)
                    {
                        WebTrendNAEPLinkDataModel linkdata = new WebTrendNAEPLinkDataModel();
                        linkdata.Link = item.Link;
                        linkdata.Status = item.Status;
                        linkdata.Icon = item.Icon;
                        LinkDataList.Add(linkdata);
                    }
                    list.WebTrendNAEPLinkData = LinkDataList;
                }

                var webTrendNRCLinkData = (from a in WebTrendLinkList
                                           from b in StatusList.Where(b => a.Status == b.Id)
                                           where a.Status == 9
                                           select new
                                           {
                                               a.Link,
                                               Status = b.Description,
                                               b.Icon
                                           });

                if (webTrendNRCLinkData != null)
                {
                    List<WebTrendNRCLinkDataModel> LinkDataList = new List<WebTrendNRCLinkDataModel>();
                    foreach (var item in webTrendNAEPLinkData)
                    {
                        WebTrendNRCLinkDataModel linkdata = new WebTrendNRCLinkDataModel();
                        linkdata.Link = item.Link;
                        linkdata.Status = item.Status;
                        linkdata.Icon = item.Icon;
                        LinkDataList.Add(linkdata);
                    }
                    list.WebTrendNRCLinkData = LinkDataList;
                }

                var WbrTrendTermList = await ctx.WebTrendTermData.Where(x => x.IsActive == true && x.Month == Month && x.Year == Year).ToListAsync();

                var webTrendTermData = (from a in WbrTrendTermList
                                        from b in StatusList.Where(b => a.Status == b.Id)
                                        select new
                                        {
                                            a.Term,
                                            Status = b.Description,
                                            b.Icon
                                        });

                if (webTrendTermData != null)
                {
                    List<WebTrendTermDataModel> TermDataList = new List<WebTrendTermDataModel>();
                    foreach (var item in webTrendTermData)
                    {
                        WebTrendTermDataModel termdata = new WebTrendTermDataModel();
                        termdata.Term = item.Term;
                        termdata.Status = item.Status;
                        termdata.Icon = item.Icon;
                        TermDataList.Add(termdata);
                    }
                    list.WebTrendTermData = TermDataList;
                }

                var WebTrendResourceList = await ctx.WebTrendResourceData.Where(x => x.IsActive == true && x.Month == Month && x.Year == Year).ToListAsync();

                var webTrendResourceData = (from a in WebTrendResourceList
                                            from b in StatusList.Where(b => a.Status == b.Id)
                                            select new
                                            {
                                                a.Description,
                                                Status = b.Description,
                                                b.Icon
                                            });

                if (webTrendResourceData != null)
                {
                    List<WebTrendResourceDataModel> ResourceDataList = new List<WebTrendResourceDataModel>();
                    foreach (var item in webTrendResourceData)
                    {
                        WebTrendResourceDataModel resourcedata = new WebTrendResourceDataModel();
                        resourcedata.Description = item.Description;
                        resourcedata.Status = item.Status;
                        resourcedata.Icon = item.Icon;
                        ResourceDataList.Add(resourcedata);
                    }
                    list.WebTrendResourceData = ResourceDataList;
                }

                return list;
            }
        }
        public async Task<List<PrivateTaskModel>> GetWebPrivateData(int Month, int Year)
        {
            List<PrivateTaskModel> list = new List<PrivateTaskModel>();

            using (var ctx = new DbEntities())
            {
                var webData = await ctx.Tasks.Where(x => x.MonthId == Month && x.Year == Year && x.Type == 2 && x.IsActive == true).Include(x => x.SubTasks).ToListAsync();

                if (webData != null)
                {
                    foreach (var item in webData)
                    {
                        PrivateTaskModel obj = new PrivateTaskModel();
                        obj.StatusId = item.StatusId;
                        var masttskstat = await ctx.TaskStatuses.Where(x => x.Id == obj.StatusId).FirstOrDefaultAsync();
                        if (masttskstat != null)
                        {
                            obj.PrivateType = masttskstat.Description;
                            obj.Icon = masttskstat.Icon;
                        }
                        if (item.SubTasks.Count > 0)
                        {
                            List<SubTaskModel> list1 = new List<SubTaskModel>();
                            foreach (var item1 in item.SubTasks)
                            {
                                SubTaskModel obj1 = new SubTaskModel();
                                obj1.Id = item1.Id;
                                obj1.Description = item1.Description;
                                if (item1.JiraTickets.Count > 0)
                                {
                                    List<JiraTicketModel> list2 = new List<JiraTicketModel>();
                                    foreach (var item2 in item1.JiraTickets)
                                    {
                                        JiraTicketModel obj2 = new JiraTicketModel();
                                        obj2.JiraTicketId = item2.JiraTicketId;
                                        list2.Add(obj2);
                                    }
                                    obj1.JiraTickets = list2;
                                }
                                list1.Add(obj1);
                            }
                            obj.SubTasks = list1;
                        }
                        obj.Description = item.Description;
                        list.Add(obj);

                    }
                }
                return list;
            }
        }

        public async Task<List<Type>> GetTaskType()
        {
            List<Type> Typedata = new List<Type>();

            using (var ctx = new DbEntities())
            {
                Typedata = await ctx.Types.ToListAsync();

            }


            return Typedata;
        }
        public async Task<List<TaskStatus>> GetTaskTaskStatus()
        {
            List<TaskStatus> TaskStatusdata = new List<TaskStatus>();

            using (var ctx = new DbEntities())
            {
                TaskStatusdata = await ctx.TaskStatuses.ToListAsync();

            }
            return TaskStatusdata;
        }
        public async Task<List<Month>> GetMonthData()
        {
            List<Month> Monthdata = new List<Month>();

            using (var ctx = new DbEntities())
            {
                Monthdata = await ctx.Months.ToListAsync();

            }
            return Monthdata;
        }
        public async Task<List<Years>> GetYearData()
        {
            List<Years> Yeasrdata = new List<Years>();

            using (var ctx = new DbEntities())
            {
                Yeasrdata = await ctx.Years.ToListAsync();

            }
            return Yeasrdata;
        }
        public async Task<ResponceModel> AddTaskAndSubTask(TaskAndSubTaskModel taskMst)
        {
            ResponceModel responseModel = new ResponceModel();
            try
            {
               
                taskMst.IsActive = true;
                taskMst.CreatedBy = 1;
                taskMst.CreatedDate = DateTime.Now;
                var ifexist = new Task();
                using (var ctx = new DbEntities())
                {
                     ifexist = await ctx.Tasks.Where(x => x.MonthId == taskMst.MonthId && x.Year == taskMst.Year && x.Type == taskMst.Type &&  x.StatusId == taskMst.StatusId && x.IsActive == true).FirstOrDefaultAsync();
                }
                if(ifexist == null)
                {
                    var config = new MapperConfiguration(cfg =>
                        cfg.CreateMap<TaskAndSubTaskModel, Task>()
                    );
                    var mapper = new Mapper(config);
                    var taskDTO = mapper.Map<Task>(taskMst);

                    using (var ctx = new DbEntities())
                    {
                        await ctx.Tasks.AddAsync(taskDTO);
                        await ctx.SaveChangesAsync();
                    }

                    SubTask subtask = new SubTask();
                    subtask.Description = taskMst.SubTaskDescription;
                    subtask.TaskId = taskDTO.Id;
                    subtask.IsActive = true;
                    if (subtask.Description != "")
                    {
                        using (var ctx = new DbEntities())
                        {
                            await ctx.SubTasks.AddAsync(subtask);
                            await ctx.SaveChangesAsync();
                        }
                    }
                    responseModel.success = true;
                    responseModel.message = "Operation Completed Successfully !";
                    responseModel.data = taskDTO;
                }
                else
                {
                    responseModel.success = true;
                    responseModel.message = "Task Already Exist";
                    responseModel.data = null;
                }
                    
            }
             catch(Exception ex)
            {

            }

            return responseModel;
        }
    }
}
