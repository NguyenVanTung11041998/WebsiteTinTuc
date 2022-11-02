import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatNationalityComponent } from './creat-nationality.component';

describe('CreatNationalityComponent', () => {
  let component: CreatNationalityComponent;
  let fixture: ComponentFixture<CreatNationalityComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreatNationalityComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatNationalityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
