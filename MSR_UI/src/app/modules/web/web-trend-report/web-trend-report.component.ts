import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit, Input } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { WebTrendSiteDataModel, WebTrendNAEPLinkDataModel, WebTrendNRCLinkDataModel, WebTrendTermDataModel, WebTrendResourceDataModel } from 'src/app/_models/web.models';
import { WebService } from 'src/app/_services/web.service';
import * as $ from "jquery";
@Component({
  selector: 'app-web-trend-report',
  templateUrl: './web-trend-report.component.html',
  styleUrls: ['./web-trend-report.component.scss']
})
export class WebTrendReportComponent implements OnInit {

  @Input() demo3: {
    webTrendSiteData: WebTrendSiteDataModel[],
    webTrendNAEPLinkData: WebTrendNAEPLinkDataModel[],
    webTrendNRCLinkData: WebTrendNRCLinkDataModel[],
    webTrendTermData: WebTrendTermDataModel[],
    webTrendResourceData: WebTrendResourceDataModel[],
  };
  @Input() WebTrendlength: number;

  WebTrendData: {
    webTrendSiteData: WebTrendSiteDataModel[],
    webTrendNAEPLinkData: WebTrendNAEPLinkDataModel[],
    webTrendNRCLinkData: WebTrendNRCLinkDataModel[],
    webTrendTermData: WebTrendTermDataModel[],
    webTrendResourceData: WebTrendResourceDataModel[],
  };
  length: number;
  WebTrendSiteDataLength: number;
  WebTrendNAEPLinkDataLength:number;
  WebTrendTermDataLength:number;
  WebTrendNRCLinkDataLength:number;
  WebTrendResourceDataLength:number;
  constructor(
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private webService: WebService,
  ) { }

  ngOnInit(): void {   
    this.getMonths();
  }

  ngOnChanges() {

    // this.WebTrendData = this.demo3;
    // this.length = this.WebTrendlength;
    this.getMonths()
  }

  getMonths() {
    var selectedMonth = $("#Month").val();
    var selectedYear = $("#Year").val();
    if(selectedMonth === null && selectedYear === null)
    {
      selectedMonth = new Date().getMonth() + 1;
      selectedYear=new Date().getFullYear();
    }
  this.spinner.show();
    this.webService.getWebTrendReportData(selectedMonth,selectedYear).subscribe(data => {
      if (data) {
            this.WebTrendlength = 0;
            this.WebTrendSiteDataLength = 0;
            this.WebTrendNAEPLinkDataLength = 0;
            this.WebTrendNAEPLinkDataLength= 0;
            this.WebTrendNRCLinkDataLength = 0;
            this.WebTrendResourceDataLength = 0;
            this.demo3 = data;
            this.WebTrendData = data;
            this.length = 0;
            if(this.WebTrendData){
                if(this.WebTrendData.webTrendSiteData.length >0){
                  this.WebTrendSiteDataLength = 1;
                }
                if(this.WebTrendData.webTrendNAEPLinkData.length >0){
                  this.WebTrendNAEPLinkDataLength = 1;
                }

                if(this.WebTrendData.webTrendTermData.length >0){
                  this.WebTrendTermDataLength = 1;
                }
                if(this.WebTrendData.webTrendNRCLinkData.length >0){
                  this.WebTrendNRCLinkDataLength = 1;
                }
                if(this.WebTrendData.webTrendResourceData.length >0){
                  this.WebTrendResourceDataLength = 1;
                }
                if(this.WebTrendData.webTrendSiteData.length >0 && this.WebTrendData.webTrendNAEPLinkData.length >0 && this.WebTrendData.webTrendTermData.length >0 && this.WebTrendData.webTrendNRCLinkData.length >0 && this.WebTrendData.webTrendResourceData.length >0){
                  this.length = 1;
                }
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
