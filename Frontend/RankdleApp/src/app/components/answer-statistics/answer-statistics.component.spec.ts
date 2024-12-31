import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnswerStatisticsComponent } from './answer-statistics.component';

describe('AnswerStatisticsComponent', () => {
  let component: AnswerStatisticsComponent;
  let fixture: ComponentFixture<AnswerStatisticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AnswerStatisticsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnswerStatisticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
