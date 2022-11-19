import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BranchjobComponent } from './branchjob.component';

describe('BranchjobComponent', () => {
  let component: BranchjobComponent;
  let fixture: ComponentFixture<BranchjobComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BranchjobComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BranchjobComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
