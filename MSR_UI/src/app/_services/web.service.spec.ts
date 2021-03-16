import { WebService } from './web.service';
import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpErrorResponse } from '@angular/common/http';
import { TASKS, MONTHS_YEARS, HELP_DESK_TICKETS, WEB_TREND, PRIVATE_DATA } from './mock';

describe('Web Service', () => {

    let webService: WebService,
        httpTestingController: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule
            ],
            providers: [
                WebService
            ]
        });

        webService = TestBed.inject(WebService),
            httpTestingController = TestBed.inject(HttpTestingController);
    });

    it('should get web data', () => {
        const selectedMonth = 2;
        const selectedYear = 2021;

        webService.getWebData(selectedMonth, selectedYear)
            .subscribe(response => {
                expect(typeof response).toBe('object');

                const task = TASKS.find(task => task.id === 4);
                expect(task.subTasks.length).toBeGreaterThan(1);
                expect(task.subTasks[0].description).toBe("Reconfiguration of Help topic on megamenu for NCES:");
            });

        const req = httpTestingController.expectOne('https://localhost:44362/api/web/GetWebData?Month=2&Year=2021');

        expect(req.request.method).toEqual("GET");

        req.flush({ payload: Object.values(TASKS) });
    });

    it('should get year and month', () => {
        webService.getYearAndMonths()
            .subscribe(task => {
                expect(task.hasOwnProperty('dates')).toBeFalsy();
                expect(task.hasOwnProperty('months')).toBeTruthy();
                expect(task.hasOwnProperty('years')).toBeTruthy();

                const monthName = task['months'][0].MonthName;
                expect(monthName).toBe('January');

                const year = task['years'][0].Year;
                expect(year).toBe('2020');
            });

        const req = httpTestingController.expectOne('https://localhost:44362/api/web/GetYearAndMonths');
        expect(req.request.method).toEqual("GET");
        req.flush(MONTHS_YEARS);
    });

    it('should get help desk get tickets', () => {
        const selectedMonth = 2;
        const selectedYear = 2021;

        webService.getHelpDeskTickets(selectedMonth, selectedYear)
            .subscribe(response => {
                expect(Object.keys(response).length).toBe(10);
                expect(typeof response).toBe('object');

                expect(response[0].id).toBe(1);
                expect(response[0].category).toBe("Content Forward");
            });

        const req = httpTestingController.expectOne('https://localhost:44362/api/web/GetHelpDeskTickets?Month=2&Year=2021');
        expect(req.request.method).toEqual("GET");
        req.flush(HELP_DESK_TICKETS);
    });

    it('should get web trend report data', () => {
        const selectedMonth = 2;
        const selectedYear = 2021;

        webService.getWebTrendReportData(selectedMonth, selectedYear)
            .subscribe(response => {
                expect(Object.keys(response).length).toBe(5);
                expect(typeof response).toBe('object');

                expect(response.hasOwnProperty('webTrendSiteData')).toBeTruthy();
                expect(response.hasOwnProperty('webTrendNAEPLinkData')).toBeTruthy();
                expect(response.hasOwnProperty('webTrendNRCLinkData')).toBeTruthy();
                expect(response.hasOwnProperty('webTrendTermData')).toBeTruthy();
                expect(response.hasOwnProperty('webTrendResourceData')).toBeTruthy();

                expect(response['webTrendSiteData'][0].name).toBe('36th');
            });

        const req = httpTestingController.expectOne('https://localhost:44362/api/web/GetWebTrendReportData?Month=2&Year=2021');
        expect(req.request.method).toEqual("GET");
        req.flush(WEB_TREND);
    });

    it('should get private data', () => {
        const selectedMonth = 2;
        const selectedYear = 2021;

        webService.getPrivateData(selectedMonth, selectedYear)
            .subscribe(response => {
                expect(Object.keys(response).length).toBe(7);
                expect(typeof response).toBe('object');

                expect(response[0].privateType).toBe('Integrated Management System');
                expect(response[0].subTasks.length).toBeGreaterThan(1);
                expect(response[0].subTasks[0].description).toBe("Reconfiguration of Help topic on megamenu for NCES:");
            });

        const req = httpTestingController.expectOne('https://localhost:44362/api/web/GetWebPrivateData?Month=2&Year=2021');
        expect(req.request.method).toEqual("GET");
        req.flush(PRIVATE_DATA);
    });

    afterEach(() => {
        httpTestingController.verify();
    });
});
