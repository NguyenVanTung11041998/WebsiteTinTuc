import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BranchJobComponent } from './branch-job.component';

describe('BranchJobComponent', () => {
  let component: BranchJobComponent;
  let fixture: ComponentFixture<BranchJobComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BranchJobComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BranchJobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
