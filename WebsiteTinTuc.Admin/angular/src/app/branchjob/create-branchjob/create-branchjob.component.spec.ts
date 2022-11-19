import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBranchjobComponent } from './create-branchjob.component';

describe('CreateBranchjobComponent', () => {
  let component: CreateBranchjobComponent;
  let fixture: ComponentFixture<CreateBranchjobComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateBranchjobComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateBranchjobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
