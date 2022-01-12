import { TestBed } from '@angular/core/testing';

import { IdentityServerService } from './identity-server.service';

describe('IdentityServerService', () => {
  let service: IdentityServerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(IdentityServerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
