import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { KeycloakService } from 'keycloak-angular';
import { UserPreferenceService } from '../services/user-preference.service';

export const authGuard: CanActivateFn = (route, state) => {
  var keycloakService = inject(KeycloakService);
  var userPreferenceService = inject(UserPreferenceService);
  const isLoggedIn = keycloakService.isLoggedIn();
  if (!isLoggedIn) {
    keycloakService.login();
    const userRoles = keycloakService.getUserRoles();
    userPreferenceService.saveRoles(userRoles);
  }
  return keycloakService.isLoggedIn();
};
