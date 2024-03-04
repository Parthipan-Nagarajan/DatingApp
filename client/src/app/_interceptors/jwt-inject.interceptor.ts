import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { take } from 'rxjs';

export const jwtInjectInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const accountService = inject(AccountService);

  accountService.currentUser$.pipe(take(1)).subscribe({
    next: user => {
      if (user) {
        req = req.clone({
          setHeaders: {
            Authorization: `Bearer ${user.token}`
          }
        })
      }
    }
  })

  return next(req);
};
