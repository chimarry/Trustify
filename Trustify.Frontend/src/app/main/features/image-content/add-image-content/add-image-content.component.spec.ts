import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddImageContentComponent } from './add-image-content.component';

describe('AddImageContentComponent', () => {
  let component: AddImageContentComponent;
  let fixture: ComponentFixture<AddImageContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddImageContentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddImageContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
