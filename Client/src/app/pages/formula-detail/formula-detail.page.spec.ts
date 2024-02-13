import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormulaDetailPage } from './formula-detail.page';

describe('FormulaDetailPage', () => {
  let component: FormulaDetailPage;
  let fixture: ComponentFixture<FormulaDetailPage>;

  beforeEach(async(() => {
    fixture = TestBed.createComponent(FormulaDetailPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
