import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpParams } from "@angular/common/http";
import { Observable } from 'rxjs';
import { AppConstant } from '../_constants/app.constants';
import { AccountService } from './account.service';

@Injectable({
    providedIn: 'root'
})

export class WebService {

    constructor(private http: HttpClient, private accountService: AccountService) { }

    getWebData(month,year): Observable<any> {
        const opts = { params: new HttpParams({fromString: `Month=${month}&Year=${year}`}) };
        return this.http.get(AppConstant.API_URL + 'api/web/GetWebData',opts)
    }

    getYearAndMonths(): Observable<any> {
        return this.http.get(AppConstant.API_URL + 'api/web/GetYearAndMonths', { headers: this.accountService.getRequestHeader() })
    }

    getHelpDeskTickets(month,year): Observable<any> {
        const opts = { params: new HttpParams({fromString: `Month=${month}&Year=${year}`}) };
        return this.http.get(AppConstant.API_URL + 'api/web/GetHelpDeskTickets', opts)
    }

    getWebTrendReportData(month,year): Observable<any> {
        const opts = { params: new HttpParams({fromString: `Month=${month}&Year=${year}`}) };
        return this.http.get(AppConstant.API_URL + 'api/web/GetWebTrendReportData', opts)
    }

    getPrivateData(month,year): Observable<any> {
        const opts = { params: new HttpParams({fromString: `Month=${month}&Year=${year}`}) };
        return this.http.get(AppConstant.API_URL + 'api/web/GetWebPrivateData',opts)
    }
    GetTaskType(): Observable<any> {
        return this.http.get(AppConstant.API_URL + 'api/web/GetTaskType', { headers: this.accountService.getRequestHeader() })
    }
    GetTaskTaskStatus(): Observable<any> {
        return this.http.get(AppConstant.API_URL + 'api/web/GetTaskTaskStatus', { headers: this.accountService.getRequestHeader() })
    }
    AddTaskAndSubTask(obj): Observable<any> {
    
        return this.http.post(AppConstant.API_URL + 'api/web/AddTaskAndSubTask',obj, { headers: this.accountService.getRequestHeader() })
    }
    GetMonthData(): Observable<any> {
        return this.http.get(AppConstant.API_URL + 'api/web/GetMonthData', { headers: this.accountService.getRequestHeader() })
    }
    GetYearData(): Observable<any> {
        return this.http.get(AppConstant.API_URL + 'api/web/GetYearData', { headers: this.accountService.getRequestHeader() })
    }
}
