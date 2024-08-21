import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { KeycloakService } from 'keycloak-angular';
import { UserPreferenceService } from '../services/user-preference.service';
import { KeycloakProfile } from 'keycloak-js';

export const authGuard: CanActivateFn = (route, state) => {
  var keycloakService = inject(KeycloakService);
  var userPreferenceService = inject(UserPreferenceService);
  const isLoggedIn = keycloakService.isLoggedIn();
  if (!isLoggedIn) {
    keycloakService.login();

  }
  keycloakService.loadUserProfile().then(x => {
    userPreferenceService.saveRoles(keycloakService.getUserRoles());
  })
  return keycloakService.isLoggedIn();
};
