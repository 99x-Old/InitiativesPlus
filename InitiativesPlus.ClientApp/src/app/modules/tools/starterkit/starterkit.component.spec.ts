import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StarterkitComponent } from './starterkit.component';

describe('StarterkitComponent', () => {
  let component: StarterkitComponent;
  let fixture: ComponentFixture<StarterkitComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StarterkitComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StarterkitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
