import { HttpClientTestingModule } from "@angular/common/http/testing";
import { DebugElement, NO_ERRORS_SCHEMA } from "@angular/core";
import { waitForAsync, ComponentFixture, TestBed } from "@angular/core/testing";
import { ReactiveFormsModule } from "@angular/forms";
import { By } from "@angular/platform-browser";
import { IndividualConfig, ToastrModule, ToastrService } from "ngx-toastr";
import { of } from "rxjs";
import { PublicSitesComponent } from "src/app/modules/web/public-sites/public-sites.component";
import { SafePipe } from "src/app/_pipes/safe.pipe";
import { TASKS } from "src/app/_services/mock";
import { WebService } from "src/app/_services/web.service";

const toastrService = {
  success: (
    message?: string,
    title?: string,
    override?: Partial<IndividualConfig>
  ) => {},
  error: (
    message?: string,
    title?: string,
    override?: Partial<IndividualConfig>
  ) => {},
};

describe("Public Site Component", () => {
  let component: PublicSitesComponent;
  let fixture: ComponentFixture<PublicSitesComponent>;
  let monthEl: HTMLElement;
  let de: DebugElement;
  let webService: WebService;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule,
        ToastrModule.forRoot(),
      ],
      providers: [
        WebService,
        { provide: ToastrService, useValue: toastrService },
      ],
      schemas: [NO_ERRORS_SCHEMA],
      declarations: [PublicSitesComponent, SafePipe],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicSitesComponent);
    webService = TestBed.inject(WebService);
    component = fixture.debugElement.componentInstance;
    monthEl = fixture.nativeElement.querySelector("#month");
    de = fixture.debugElement;
    fixture.detectChanges();
  });

  it("no_data_contenet should be visible, if apiResponse.length === 0", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.apiResponse.length).toBe(4);

      let dataContentDiv = fixture.nativeElement.querySelector(".head");
      expect(dataContentDiv).toBeTruthy();

      let noDataContentDiv = fixture.nativeElement.querySelector(
        ".No-Data-Content"
      );
      expect(noDataContentDiv).toBeFalsy();
    });
  });

  // Release / Enhancement
  it("if apiResponse contains one object, content-top should be visible", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.apiResponse.length).toBeGreaterThan(0);

      let dataContentDiv = fixture.nativeElement.querySelector(".content-top");
      expect(dataContentDiv).toBeTruthy();
    });
  });

  it("if apiResponse contains description, two div should be inside content-top div", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.apiResponse.length).toBeGreaterThan(0);

      let headDiv = fixture.nativeElement.querySelector(".head");
      expect(headDiv).toBeTruthy();

      let dataContentDiv = fixture.nativeElement.querySelector(".head");
      expect(dataContentDiv).toBeTruthy();
    });
  });

  // Reviewed
  it("if apiResponse contains two objects and second object has description, there must be div with apiresponse[1].description", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();

      expect(component.apiResponse.length).toBeGreaterThan(1);

      if (component.apiResponse.length > 0) {
        let reviewDescription = fixture.nativeElement.querySelector(
          ".review-description1"
        );
        expect(reviewDescription).toBeTruthy();
        expect(reviewDescription.textContent).toBe(
          component.apiResponse[1].Description.replace(/(<([^>]+)>)/gi, "")
        );
      } else {
        let noDataContentDiv = fixture.nativeElement.querySelector(
          ".nodata_content1"
        );
        expect(noDataContentDiv).toBeTruthy();
      }
    });
  });

  it("if apiResponse subtask length greater than zero, there must be li with count equals to subtask length", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.apiResponse.length).toBeGreaterThan(1);

      if (
        component.apiResponse[1].SubTasks &&
        component.apiResponse[1].SubTasks.length > 0
      ) {
        const liSubTask1 = fixture.debugElement.queryAll(
          By.css(".li-subtask1")
        );
        expect(liSubTask1.length).toBe(
          component.apiResponse[1].SubTasks.length
        );
      }
    });
  });

  it("if apiResponse subtask jiratickets length greater than zero, there must be a with count equals to jira tickets length", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.apiResponse.length).toBeGreaterThan(1);

      if (
        component.apiResponse[1].SubTasks &&
        component.apiResponse[1].SubTasks.length > 0
      ) {
        const aSubTask1 = fixture.debugElement.queryAll(
          By.css(".a-jiraticket1")
        );
        expect(aSubTask1.length).toBe(
          component.apiResponse[1].SubTasks[1].JiraTickets.length
        );
      }
    });
  });

  // Released
  it("if apiResponse contains three objects and third object has description, there must be div with apiresponse[2].description", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();

      expect(component.apiResponse.length).toBeGreaterThan(2);

      if (component.apiResponse.length > 1) {
        let reviewDescription = fixture.nativeElement.querySelector(
          ".review-description2"
        );
        expect(reviewDescription).toBeTruthy();
        expect(reviewDescription.textContent).toBe(
          component.apiResponse[2].Description.replace(/(<([^>]+)>)/gi, "")
        );
      } else {
        let noDataContentDiv = fixture.nativeElement.querySelector(
          ".nodata_content2"
        );
        expect(noDataContentDiv).toBeTruthy();
      }
    });
  });

  it("if apiResponse subtask length greater than one, there must be li with count equals to subtask length", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.apiResponse.length).toBeGreaterThan(1);

      if (
        component.apiResponse[2].SubTasks &&
        component.apiResponse[2].SubTasks.length > 0
      ) {
        const liSubTask2 = fixture.debugElement.queryAll(
          By.css(".li-subtask2")
        );
        expect(liSubTask2.length).toBe(
          component.apiResponse[2]?.SubTasks?.length || 0
        );
      }
    });
  });

  it("if apiResponse subtask jiratickets length greater than one, there must be a with count equals to jira tickets length", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.apiResponse.length).toBeGreaterThan(1);

      fixture.detectChanges();

      if (component.apiResponse[2].SubTasks) {
        expect(component.apiResponse[2].SubTasks.length).toBeGreaterThan(0);
      }

      if (
        component.apiResponse[2].SubTasks &&
        component.apiResponse[2].SubTasks.length > 0
      ) {
        const aSubTask2 = fixture.debugElement.queryAll(
          By.css(".a-jiraticket2")
        );
        expect(aSubTask2.length).toBe(
          component.apiResponse[2]?.SubTasks[0]?.JiraTickets?.length || 0
        );
      }
    });
  });

  // Tested
  it("if apiResponse contains four objects and fourth object has description, there must be div with apiresponse[3].description", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();

      expect(component.apiResponse.length).toBeGreaterThan(3);

      if (component.apiResponse.length > 2) {
        let reviewDescription = fixture.nativeElement.querySelector(
          ".review-description3"
        );
        expect(reviewDescription).toBeTruthy();
        expect(reviewDescription.textContent).toBe(
          component.apiResponse[3].Description.replace(/(<([^>]+)>)/gi, "")
        );
      } else {
        let noDataContentDiv = fixture.nativeElement.querySelector(
          ".nodata_content3"
        );
        expect(noDataContentDiv).toBeTruthy();
      }
    });
  });

  it("if apiResponse subtask length greater than zero, there must be li with count equals to subtask length", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.apiResponse.length).toBeGreaterThan(2);

      if (
        component.apiResponse[3].SubTasks &&
        component.apiResponse[3].SubTasks.length > 0
      ) {
        const liSubTask3 = fixture.debugElement.queryAll(
          By.css(".li-subtask3")
        );
        expect(liSubTask3.length).toBe(
          component.apiResponse[3]?.SubTasks?.length || 0
        );
      }
    });
  });

  it("if apiResponse subtask jiratickets length greater than one, there must be a with count equals to jira tickets length", () => {
    spyOn(webService, "getWebData").and.returnValue(of(TASKS));
    component.getMonths();
    expect(webService.getWebData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.apiResponse.length).toBeGreaterThan(2);

      fixture.detectChanges();

      if (component.apiResponse[3].SubTasks) {
        expect(component.apiResponse[3].SubTasks.length).toBeGreaterThan(0);
      }

      if (
        component.apiResponse[3].SubTasks &&
        component.apiResponse[3].SubTasks.length > 0
      ) {
        const aSubTask3 = fixture.debugElement.queryAll(
          By.css(".a-jiraticket3")
        );
        expect(aSubTask3.length).toBe(
          component.apiResponse[3]?.SubTasks[0]?.JiraTickets?.length || 0
        );
      }
    });
  });
});
