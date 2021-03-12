import { Component, OnInit } from '@angular/core';
import { MonthModel,YearModel,TaskStatusModel,TypeModel} from 'src/app/_models/web.models';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup } from '@angular/forms';
import { WebService } from 'src/app/_services/web.service';
import { HttpErrorResponse, HttpClient } from '@angular/common/http';
import * as $ from "jquery";
import {MatSelectModule} from '@angular/material/select';

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
    this.getYearAndMonths();
    this.GetTaskType();
    this.GetTaskTaskStatus();
  }

  getYearAndMonths() {
    this.spinner.show();
    this.webService.getYearAndMonths().subscribe(data => {
      if (data) {
        this.monthList = data?.months || [];
        this.yearList = data?.years || [];
      
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
    if($("#Tasktxt").val() == null || $("#Tasktxt").val() == '                           \n                            ')
    {
      this.toastr.show("Please Enter Task");    
       flag = false;
    }  
    if(SubTasktxt == '                      \n                        ')
    {
      $("#SubTasktxt").val(null); 
    }
    if(flag == true)
    {
      this.spinner.show();
      var obj = {
        Description :  $("#Tasktxt").val(),
        MonthId : Number($("#Month").val()),
        Year : Number($("#Year").val()),
        Type : Number($("#Type").val()),
        StatusId : Number($("#TaskStatus").val()),
        SubTaskDescription: $("#SubTasktxt").val()
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
    $("#Month").val(0),
    $("#Year").val(0),
   $("#Type").val(''),
   $("#TaskStatus").val(''),
   $("#SubTasktxt").val('')
  }
}
