import { Component, OnInit } from '@angular/core';
import { MonthModel,YearModel,TaskStatusModel,TypeModel} from 'src/app/_models/web.models';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { WebService } from 'src/app/_services/web.service';
import { HttpErrorResponse, HttpClient } from '@angular/common/http';
import * as $ from "jquery";
import { FormBuilder, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-admin-sites',
  templateUrl: './admin-sites.component.html',
  styleUrls: ['./admin-sites.component.scss']
})
export class AdminSitesComponent implements OnInit {

  monthList: MonthModel[] = [];
  yearList: YearModel[] = [];
  typeList: TypeModel[] = [];

  taskStatusList: TaskStatusModel[] = [];
  filterForm: FormGroup;
  
  constructor(
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private webService: WebService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.GetMonthData();
    this.GetTaskType();
    this.GetTaskTaskStatus();
    this.GetYearData();   
    this.filterForm = this.fb.group({
      Yeardata: this.fb.control(new Date().getFullYear()),
      Monthdata: this.fb.control(new Date().getMonth() + 1),
      Type: this.fb.control(1),
      TaskStatusId: this.fb.control(1),
    });
  }
  
  GetMonthData() {
    this.spinner.show();
    this.webService.GetMonthData().subscribe(data => {
      if (data) {
        this.monthList = data;
      
      }
      this.spinner.hide();
    },
      (err: HttpErrorResponse) => {
        this.spinner.hide();
        this.toastr.error(err.error['Message'], 'Error');
      }
    );
  }
  GetYearData() {
    this.spinner.show();
    this.webService.GetYearData().subscribe(data => {
      if (data) {
        this.yearList = data;
      
      }
      this.spinner.hide();
    },
      (err: HttpErrorResponse) => {
        this.spinner.hide();
        this.toastr.error(err.error['Message'], 'Error');
      }
    );
  }
  GetTaskType() {
    this.spinner.show();
    this.webService.GetTaskType().subscribe(data => {
      if (data) {
     
        this.typeList = data;      
       
      }
      this.spinner.hide();
    },
      (err: HttpErrorResponse) => {
        this.spinner.hide();
        this.toastr.error(err.error['Message'], 'Error');
      }
    );
  }
  GetTaskTaskStatus() {
    this.spinner.show();
    this.webService.GetTaskTaskStatus().subscribe(data => {
      if (data) {      

        this.taskStatusList = data;
       
      }
      this.spinner.hide();
    },
      (err: HttpErrorResponse) => {
        this.spinner.hide();
        this.toastr.error(err.error['Message'], 'Error');
      }
    );
  }
  AddTaskAndSubTask() {
    var flag = true;
   var SubTasktxt = $("#SubTasktxt").val();
   var Jitatickets = $("#txtjitatickets").val();
    if($("#Tasktxt").val() == null || $("#Tasktxt").val() == '                           \n                            ')
    {
      this.toastr.show("Please Enter Task");    
       flag = false;
    }  
    if(SubTasktxt == '                      \n                        ')
    {
      $("#SubTasktxt").val(null); 
    }
    if(Jitatickets != '')
    {
      if(SubTasktxt == '                      \n                        ')
      {
        this.toastr.show("If You what To Add Jira Tickets Then Must Add Sub Task");    
       flag = false;
      }
    }
    
    if(flag == true)
    {
      this.spinner.show();
      var obj = {
        Description :  $("#Tasktxt").val(),
        SubTaskDescription :  $("#SubTasktxt").val(),
        MonthId : Number($("#Monthdata").val()),
        Year : Number($("#Yeardata").val()),
        Type : Number($("#Type").val()),
        StatusId : Number($("#TaskStatus").val()),
        JiraTicketId: Number($("#txtjitatickets").val()),
      }   
      this.webService.AddTaskAndSubTask(obj).subscribe(data => {
        if (data) {  
          if(data.success == true)    
          {         
            this.toastr.show(data.message);
            this.clear()
          }
          else
          {
            this.toastr.error(data.message);
          }
         
        }
        this.spinner.hide();
      },
        (err: HttpErrorResponse) => {
          this.spinner.hide();
          this.toastr.error(err.error['Message'], 'Error');
        }
      );
    }
 
  }
  clear() {
   $("#Tasktxt").val(''),   
   $("#Monthdata").val('')
   $("#Yeardata").val('')
   $("#Type").val('')
   $("#TaskStatus").val('')
   $("#SubTasktxt").val('')
   $("#txtjitatickets").val('')
   this.ngOnInit();
  }
}
