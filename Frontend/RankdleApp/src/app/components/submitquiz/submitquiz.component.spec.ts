import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitquizComponent } from './submitquiz.component';

describe('SubmitquizComponent', () => {
  let component: SubmitquizComponent;
  let fixture: ComponentFixture<SubmitquizComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubmitquizComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubmitquizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
