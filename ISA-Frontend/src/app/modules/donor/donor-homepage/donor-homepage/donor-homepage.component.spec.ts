import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonorHomepageComponent } from './donor-homepage.component';

describe('DonorHomepageComponent', () => {
  let component: DonorHomepageComponent;
  let fixture: ComponentFixture<DonorHomepageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonorHomepageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DonorHomepageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
