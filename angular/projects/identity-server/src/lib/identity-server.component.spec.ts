import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { IdentityServerComponent } from './identity-server.component';

describe('IdentityServerComponent', () => {
  let component: IdentityServerComponent;
  let fixture: ComponentFixture<IdentityServerComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ IdentityServerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(IdentityServerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
