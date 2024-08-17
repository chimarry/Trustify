/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { UserWrapper } from '../models/user-wrapper';
import { UserIdWrapper } from '../models/user-id-wrapper';
import { UserIdGroupWrapper } from '../models/user-id-group-wrapper';
import { UserDTOResultMessage } from '../models/user-dtoresult-message';
@Injectable({
  providedIn: 'root',
})
class UsersService extends __BaseService {
  static readonly getApiV10UsersPath = '/api/v1.0/users';
  static readonly postApiV10UsersPath = '/api/v1.0/users';
  static readonly putApiV10UsersPath = '/api/v1.0/users';
  static readonly putApiV10UsersDeletePath = '/api/v1.0/users/delete';
  static readonly putApiV10UsersGroupAddPath = '/api/v1.0/users/group/add';
  static readonly putApiV10UsersGroupRemovePath = '/api/v1.0/users/group/remove';
  static readonly getApiV10UsersGroupsPath = '/api/v1.0/users/groups';
  static readonly postApiV10UsersPasswordPath = '/api/v1.0/users/password';
  static readonly putApiV10UsersUserInfoPath = '/api/v1.0/users/user-info';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Returns all users.
   * @param params The `UsersService.GetApiV10UsersParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10UsersResponse(params: UsersService.GetApiV10UsersParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/users`,
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
   * Returns all users.
   * @param params The `UsersService.GetApiV10UsersParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10Users(params: UsersService.GetApiV10UsersParams): __Observable<null> {
    return this.getApiV10UsersResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Registers new user.
   * @param params The `UsersService.PostApiV10UsersParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User's data
   *
   * - `Authorization`: Authorization
   */
  postApiV10UsersResponse(params: UsersService.PostApiV10UsersParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/v1.0/users`,
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
   * Registers new user.
   * @param params The `UsersService.PostApiV10UsersParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User's data
   *
   * - `Authorization`: Authorization
   */
  postApiV10Users(params: UsersService.PostApiV10UsersParams): __Observable<null> {
    return this.postApiV10UsersResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param params The `UsersService.PutApiV10UsersParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  putApiV10UsersResponse(params: UsersService.PutApiV10UsersParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/v1.0/users`,
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
   * @param params The `UsersService.PutApiV10UsersParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  putApiV10Users(params: UsersService.PutApiV10UsersParams): __Observable<null> {
    return this.putApiV10UsersResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Deletes user from the server.
   * @param params The `UsersService.PutApiV10UsersDeleteParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User identifier.
   *
   * - `Authorization`: Authorization
   */
  putApiV10UsersDeleteResponse(params: UsersService.PutApiV10UsersDeleteParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/v1.0/users/delete`,
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
   * Deletes user from the server.
   * @param params The `UsersService.PutApiV10UsersDeleteParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User identifier.
   *
   * - `Authorization`: Authorization
   */
  putApiV10UsersDelete(params: UsersService.PutApiV10UsersDeleteParams): __Observable<null> {
    return this.putApiV10UsersDeleteResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Adds new user in the group.
   * @param params The `UsersService.PutApiV10UsersGroupAddParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User and group identifiers
   *
   * - `Authorization`: Authorization
   */
  putApiV10UsersGroupAddResponse(params: UsersService.PutApiV10UsersGroupAddParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/v1.0/users/group/add`,
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
   * Adds new user in the group.
   * @param params The `UsersService.PutApiV10UsersGroupAddParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User and group identifiers
   *
   * - `Authorization`: Authorization
   */
  putApiV10UsersGroupAdd(params: UsersService.PutApiV10UsersGroupAddParams): __Observable<null> {
    return this.putApiV10UsersGroupAddResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Removes user from the group.
   * @param params The `UsersService.PutApiV10UsersGroupRemoveParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User and group identifiers
   *
   * - `Authorization`: Authorization
   */
  putApiV10UsersGroupRemoveResponse(params: UsersService.PutApiV10UsersGroupRemoveParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/v1.0/users/group/remove`,
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
   * Removes user from the group.
   * @param params The `UsersService.PutApiV10UsersGroupRemoveParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User and group identifiers
   *
   * - `Authorization`: Authorization
   */
  putApiV10UsersGroupRemove(params: UsersService.PutApiV10UsersGroupRemoveParams): __Observable<null> {
    return this.putApiV10UsersGroupRemoveResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Returns user groups
   * @param params The `UsersService.GetApiV10UsersGroupsParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `UserId`:
   *
   * - `Authorization`: Authorization
   */
  getApiV10UsersGroupsResponse(params: UsersService.GetApiV10UsersGroupsParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.UserId != null) __params = __params.set('UserId', params.UserId.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/users/groups`,
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
   * Returns user groups
   * @param params The `UsersService.GetApiV10UsersGroupsParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `UserId`:
   *
   * - `Authorization`: Authorization
   */
  getApiV10UsersGroups(params: UsersService.GetApiV10UsersGroupsParams): __Observable<null> {
    return this.getApiV10UsersGroupsResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Requires user to update their password upon login.
   * @param params The `UsersService.PostApiV10UsersPasswordParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User's identifier.
   *
   * - `Authorization`: Authorization
   */
  postApiV10UsersPasswordResponse(params: UsersService.PostApiV10UsersPasswordParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/v1.0/users/password`,
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
   * Requires user to update their password upon login.
   * @param params The `UsersService.PostApiV10UsersPasswordParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User's identifier.
   *
   * - `Authorization`: Authorization
   */
  postApiV10UsersPassword(params: UsersService.PostApiV10UsersPasswordParams): __Observable<null> {
    return this.postApiV10UsersPasswordResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Returns information about one user.
   * @param params The `UsersService.PutApiV10UsersUserInfoParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User identifier
   *
   * - `Authorization`: Authorization
   *
   * @return OK
   */
  putApiV10UsersUserInfoResponse(params: UsersService.PutApiV10UsersUserInfoParams): __Observable<__StrictHttpResponse<UserDTOResultMessage>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/v1.0/users/user-info`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<UserDTOResultMessage>;
      })
    );
  }
  /**
   * Returns information about one user.
   * @param params The `UsersService.PutApiV10UsersUserInfoParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: User identifier
   *
   * - `Authorization`: Authorization
   *
   * @return OK
   */
  putApiV10UsersUserInfo(params: UsersService.PutApiV10UsersUserInfoParams): __Observable<UserDTOResultMessage> {
    return this.putApiV10UsersUserInfoResponse(params).pipe(
      __map(_r => _r.body as UserDTOResultMessage)
    );
  }
}

module UsersService {

  /**
   * Parameters for getApiV10Users
   */
  export interface GetApiV10UsersParams {

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
   * Parameters for postApiV10Users
   */
  export interface PostApiV10UsersParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * User's data
     */
    body?: UserWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for putApiV10Users
   */
  export interface PutApiV10UsersParams {

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
   * Parameters for putApiV10UsersDelete
   */
  export interface PutApiV10UsersDeleteParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * User identifier.
     */
    body?: UserIdWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for putApiV10UsersGroupAdd
   */
  export interface PutApiV10UsersGroupAddParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * User and group identifiers
     */
    body?: UserIdGroupWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for putApiV10UsersGroupRemove
   */
  export interface PutApiV10UsersGroupRemoveParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * User and group identifiers
     */
    body?: UserIdGroupWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for getApiV10UsersGroups
   */
  export interface GetApiV10UsersGroupsParams {

    /**
     * API key
     */
    xApiKey?: any;
    UserId?: string;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for postApiV10UsersPassword
   */
  export interface PostApiV10UsersPasswordParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * User's identifier.
     */
    body?: UserIdWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for putApiV10UsersUserInfo
   */
  export interface PutApiV10UsersUserInfoParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * User identifier
     */
    body?: UserIdWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }
}

export { UsersService }
