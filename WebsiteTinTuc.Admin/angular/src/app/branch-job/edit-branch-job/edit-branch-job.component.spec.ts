import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBranchJobComponent } from './edit-branch-job.component';

describe('EditBranchJobComponent', () => {
  let component: EditBranchJobComponent;
  let fixture: ComponentFixture<EditBranchJobComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditBranchJobComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditBranchJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
