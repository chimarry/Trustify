import { ApplicationConfig } from '@angular/core';
import { provideRouter, withComponentInputBinding, withViewTransitions } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { DatePipe, DOCUMENT } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import {
  provideHttpClient,
  withFetch,
} from '@angular/common/http';
import { KeycloakBearerInterceptor, KeycloakService } from "keycloak-angular";
import { HTTP_INTERCEPTORS, withInterceptorsFromDi } from "@angular/common/http";
import { APP_INITIALIZER, Provider } from '@angular/core';
import { ErrorHandlingInterceptorService } from './main/core/interceptors/error-handling-interceptor.interceptor';


export function HttpLoaderFactory(http: HttpClient) {
}
function initializeKeycloak(keycloak: KeycloakService) {
  return () =>
    keycloak.init({
      // Configuration details for Keycloak
      config: {
        url: 'https://192.168.56.101:8443', // URL of the Keycloak server
        realm: 'trustify', // Realm to be used in Keycloak
        clientId: 'trustify_app' // Client ID for the application in Keycloak,
      },
      // Options for Keycloak initialization
      initOptions: {
        onLoad: 'login-required', // Action to take on load
        silentCheckSsoRedirectUri:
          window.location.origin + '/assets/silent-check-sso.html' // URI for silent SSO checks
      },
      // Enables Bearer interceptor
      enableBearerInterceptor: true,
      // Prefix for the Bearer token
      bearerPrefix: 'Bearer',
      // URLs excluded from Bearer token addition (empty by default)
      //bearerExcludedUrls: []
    });
}

// Provider for Keycloak Bearer Interceptor
const KeycloakBearerInterceptorProvider: Provider = {
  provide: HTTP_INTERCEPTORS,
  useClass: KeycloakBearerInterceptor,
  multi: true
};

// Provider for Keycloak Initialization
const KeycloakInitializerProvider: Provider = {
  provide: APP_INITIALIZER,
  useFactory: initializeKeycloak,
  multi: true,
  deps: [KeycloakService]
}


export const appConfig: ApplicationConfig = {
  providers: [
    DatePipe,
    provideHttpClient(withInterceptorsFromDi()), // Provides HttpClient with interceptors
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHandlingInterceptorService, multi: true },
    KeycloakInitializerProvider, // Initializes Keycloak
    KeycloakBearerInterceptorProvider, // Provides Keycloak Bearer Interceptor
    KeycloakService, // Service for Keycloak 
    provideRouter(routes, withViewTransitions(), withComponentInputBinding()),
    // provideHttpClient(withFetch()),
    provideAnimations(),
    { provide: Document, useExisting: DOCUMENT },
    // TranslateModule.forRoot({
    //   defaultLanguage: 'ar',
    //   loader: {
    //     provide: TranslateLoader,
    //     useFactory: HttpLoaderFactory,
    //     deps: [HttpClient]
    //   }
    // }).providers!
  ]
};

