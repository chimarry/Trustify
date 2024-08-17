import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../api/services';
import { catchError, map } from 'rxjs';
import { UserPreferenceService } from '../services/user-preference.service';
import { ResultMessage } from '../models/result-message';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router:Router  = inject(Router);
  const userPreferenceService = inject(UserPreferenceService);
    return authService.getApiV10Auth({} as AuthService.GetApiV10AuthParams)
      .pipe(
        map(e => {
          // userPreferenceService.setUser((e as ResultMessage).result as AuthWrapper)
          return true;
        }),
        catchError(error => {
          router.navigate(['/users']);
          throw error;
        })
      )
};
