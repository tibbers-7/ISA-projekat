import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminNewCenterComponent } from './admin-new-center.component';

describe('AdminNewCenterComponent', () => {
  let component: AdminNewCenterComponent;
  let fixture: ComponentFixture<AdminNewCenterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminNewCenterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminNewCenterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
