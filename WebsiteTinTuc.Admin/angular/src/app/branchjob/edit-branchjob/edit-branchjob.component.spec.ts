import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBranchjobComponent } from './edit-branchjob.component';

describe('EditBranchjobComponent', () => {
  let component: EditBranchjobComponent;
  let fixture: ComponentFixture<EditBranchjobComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditBranchjobComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditBranchjobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
