import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBranchJobComponent } from './create-branch-job.component';

describe('CreateBranchJobComponent', () => {
  let component: CreateBranchJobComponent;
  let fixture: ComponentFixture<CreateBranchJobComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateBranchJobComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBranchJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
