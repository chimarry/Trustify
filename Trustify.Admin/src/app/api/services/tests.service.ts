/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
class TestsService extends __BaseService {
  static readonly getApiV10TestPath = '/api/v1.0/test';
  static readonly getApiV10TestAccessDeniedPath = '/api/v1.0/test/access-denied';
  static readonly getApiV10TestLoginPath = '/api/v1.0/test/login';
  static readonly getApiV10TestLoginCallbackPath = '/api/v1.0/test/login-callback';
  static readonly getApiV10TestSuperAdminPath = '/api/v1.0/test/super-admin';
  static readonly getApiV10TestUserInfoPath = '/api/v1.0/test/user-info';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `TestsService.GetApiV10TestParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestResponse(params: TestsService.GetApiV10TestParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/test`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param params The `TestsService.GetApiV10TestParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10Test(params: TestsService.GetApiV10TestParams): __Observable<null> {
    return this.getApiV10TestResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `TestsService.GetApiV10TestAccessDeniedParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestAccessDeniedResponse(params: TestsService.GetApiV10TestAccessDeniedParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/test/access-denied`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param params The `TestsService.GetApiV10TestAccessDeniedParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestAccessDenied(params: TestsService.GetApiV10TestAccessDeniedParams): __Observable<null> {
    return this.getApiV10TestAccessDeniedResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `TestsService.GetApiV10TestLoginParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `returnUrl`:
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestLoginResponse(params: TestsService.GetApiV10TestLoginParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.returnUrl != null) __params = __params.set('returnUrl', params.returnUrl.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/test/login`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param params The `TestsService.GetApiV10TestLoginParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `returnUrl`:
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestLogin(params: TestsService.GetApiV10TestLoginParams): __Observable<null> {
    return this.getApiV10TestLoginResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `TestsService.GetApiV10TestLoginCallbackParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestLoginCallbackResponse(params: TestsService.GetApiV10TestLoginCallbackParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/test/login-callback`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param params The `TestsService.GetApiV10TestLoginCallbackParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestLoginCallback(params: TestsService.GetApiV10TestLoginCallbackParams): __Observable<null> {
    return this.getApiV10TestLoginCallbackResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `TestsService.GetApiV10TestSuperAdminParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestSuperAdminResponse(params: TestsService.GetApiV10TestSuperAdminParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/test/super-admin`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param params The `TestsService.GetApiV10TestSuperAdminParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestSuperAdmin(params: TestsService.GetApiV10TestSuperAdminParams): __Observable<null> {
    return this.getApiV10TestSuperAdminResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `TestsService.GetApiV10TestUserInfoParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestUserInfoResponse(params: TestsService.GetApiV10TestUserInfoParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/test/user-info`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param params The `TestsService.GetApiV10TestUserInfoParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10TestUserInfo(params: TestsService.GetApiV10TestUserInfoParams): __Observable<null> {
    return this.getApiV10TestUserInfoResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }
}

module TestsService {

  /**
   * Parameters for getApiV10Test
   */
  export interface GetApiV10TestParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for getApiV10TestAccessDenied
   */
  export interface GetApiV10TestAccessDeniedParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for getApiV10TestLogin
   */
  export interface GetApiV10TestLoginParams {

    /**
     * API key
     */
    xApiKey?: any;
    returnUrl?: string;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for getApiV10TestLoginCallback
   */
  export interface GetApiV10TestLoginCallbackParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for getApiV10TestSuperAdmin
   */
  export interface GetApiV10TestSuperAdminParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for getApiV10TestUserInfo
   */
  export interface GetApiV10TestUserInfoParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Authorization
     */
    Authorization?: any;
  }
}

export { TestsService }
