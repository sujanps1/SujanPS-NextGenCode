import { TestBed } from '@angular/core/testing';

import { LoginAccountServiceService } from './login-account-service.service';

describe('LoginAccountServiceService', () => {
  let service: LoginAccountServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoginAccountServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
