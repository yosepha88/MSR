import { HttpClientTestingModule } from "@angular/common/http/testing";
import { DebugElement, NO_ERRORS_SCHEMA } from "@angular/core";
import {
  waitForAsync,
  ComponentFixture,

  TestBed
} from "@angular/core/testing";
import { ReactiveFormsModule } from "@angular/forms";
import { By } from "@angular/platform-browser";
import { IndividualConfig, ToastrModule, ToastrService } from "ngx-toastr";
import { of } from "rxjs";
import { PrivateSitesComponent } from "src/app/modules/web/private-sites/private-sites.component";
import { SafePipe } from "src/app/_pipes/safe.pipe";
import { PRIVATE_DATA } from "src/app/_services/mock";
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

describe("Private Site", () => {
  let component: PrivateSitesComponent;
  let fixture: ComponentFixture<PrivateSitesComponent>;
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
      declarations: [PrivateSitesComponent, SafePipe],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrivateSitesComponent);
    webService = TestBed.inject(WebService);
    component = fixture.debugElement.componentInstance;
    de = fixture.debugElement;
    fixture.detectChanges();
  });

  it("if privatesetdata.length === 0, then data should not be available ", () => {
    spyOn(webService, "getPrivateData").and.returnValue(of(PRIVATE_DATA));
    component.getMonths();
    expect(webService.getPrivateData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.PrivateSiteData.length).toBeGreaterThan(1);

      let dataContentDiv = fixture.nativeElement.querySelector(".head");
      expect(dataContentDiv).toBeTruthy();

      let noDataContentDiv = fixture.nativeElement.querySelector(
        ".No-Data-Content"
      );
      expect(noDataContentDiv).toBeFalsy();
    });
  });

  it("check image with private type number is same as Task number", () => {
    spyOn(webService, "getPrivateData").and.returnValue(of(PRIVATE_DATA));
    component.getMonths();
    expect(webService.getPrivateData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.PrivateSiteData.length).toBeGreaterThan(1);
 
      const compiled = fixture.debugElement.nativeElement.querySelector('#image_0');
      if (compiled.sr  && component.PrivateSiteData && component.PrivateSiteData.length > 0) {
        expect(compiled.src).toContain(component.PrivateSiteData[0].Icon);
      }

      const taskType0 = fixture.nativeElement.querySelector('#taskType_0').textContent;
      if (component.PrivateSiteData && component.PrivateSiteData.length > 0) {
        expect(taskType0).toEqual(component.PrivateSiteData[0].PrivateType);
      }
    });
  });

  it("check as many anchore as it is inside subtask", () => {
    spyOn(webService, "getPrivateData").and.returnValue(of(PRIVATE_DATA));
    component.getMonths();
    expect(webService.getPrivateData).toHaveBeenCalled();

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.PrivateSiteData.length).toBeGreaterThan(0);

      if (component.PrivateSiteData[1].SubTasks && component.PrivateSiteData[1].SubTasks.length > 0) {
        const ticketLength = fixture.debugElement.queryAll(By.css(".jira-tickets"));
        const ticketCount = Number(component.PrivateSiteData[0].SubTasks[0].JiraTickets.length || 0);
        expect(ticketLength.length).toBe(ticketCount);
      }
    });
  });
});
