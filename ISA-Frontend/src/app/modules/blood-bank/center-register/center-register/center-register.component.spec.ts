import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CenterRegisterComponent } from './center-register.component';

describe('CenterRegisterComponent', () => {
  let component: CenterRegisterComponent;
  let fixture: ComponentFixture<CenterRegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CenterRegisterComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CenterRegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
