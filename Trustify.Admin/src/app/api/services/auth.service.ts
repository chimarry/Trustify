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
class AuthService extends __BaseService {
  static readonly getApiV10AuthPath = '/api/v1.0/auth';
  static readonly postApiV10AuthPath = '/api/v1.0/auth';
  static readonly postApiV10AuthLogOutPath = '/api/v1.0/auth/log-out';
  static readonly getApiV10AuthRolePath = '/api/v1.0/auth/role';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Check if user is logged in.
   * @param params The `AuthService.GetApiV10AuthParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10AuthResponse(params: AuthService.GetApiV10AuthParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/auth`,
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
   * Check if user is logged in.
   * @param params The `AuthService.GetApiV10AuthParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10Auth(params: AuthService.GetApiV10AuthParams): __Observable<null> {
    return this.getApiV10AuthResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Logins user to the server.
   * @param params The `AuthService.PostApiV10AuthParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  postApiV10AuthResponse(params: AuthService.PostApiV10AuthParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/v1.0/auth`,
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
   * Logins user to the server.
   * @param params The `AuthService.PostApiV10AuthParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  postApiV10Auth(params: AuthService.PostApiV10AuthParams): __Observable<null> {
    return this.postApiV10AuthResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Terminates session for the user.
   * @param params The `AuthService.PostApiV10AuthLogOutParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  postApiV10AuthLogOutResponse(params: AuthService.PostApiV10AuthLogOutParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/v1.0/auth/log-out`,
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
   * Terminates session for the user.
   * @param params The `AuthService.PostApiV10AuthLogOutParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  postApiV10AuthLogOut(params: AuthService.PostApiV10AuthLogOutParams): __Observable<null> {
    return this.postApiV10AuthLogOutResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `AuthService.GetApiV10AuthRoleParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `roleName`:
   *
   * - `Authorization`: Authorization
   */
  getApiV10AuthRoleResponse(params: AuthService.GetApiV10AuthRoleParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.roleName != null) __params = __params.set('roleName', params.roleName.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/auth/role`,
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
   * @param params The `AuthService.GetApiV10AuthRoleParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `roleName`:
   *
   * - `Authorization`: Authorization
   */
  getApiV10AuthRole(params: AuthService.GetApiV10AuthRoleParams): __Observable<null> {
    return this.getApiV10AuthRoleResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }
}

module AuthService {

  /**
   * Parameters for getApiV10Auth
   */
  export interface GetApiV10AuthParams {

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
   * Parameters for postApiV10Auth
   */
  export interface PostApiV10AuthParams {

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
   * Parameters for postApiV10AuthLogOut
   */
  export interface PostApiV10AuthLogOutParams {

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
   * Parameters for getApiV10AuthRole
   */
  export interface GetApiV10AuthRoleParams {

    /**
     * API key
     */
    xApiKey?: any;
    roleName?: string;

    /**
     * Authorization
     */
    Authorization?: any;
  }
}

export { AuthService }
