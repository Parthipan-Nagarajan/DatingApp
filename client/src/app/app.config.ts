import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import { provideToastr } from 'ngx-toastr';
import { ErrorInterceptor } from './_interceptors/error.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  provideRouter(routes),
  provideClientHydration(),
  provideAnimations(),
  provideHttpClient(
    withInterceptorsFromDi()
  ), provideToastr({
    timeOut: 3000,
    closeButton: true,
    enableHtml: true,
    easing: 'ease-in-out',
    progressBar: true,
    tapToDismiss: true
  })]
};
