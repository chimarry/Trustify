/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { RoleDTO } from '../models/role-dto';
import { RoleWrapper } from '../models/role-wrapper';
@Injectable({
  providedIn: 'root',
})
class RolesService extends __BaseService {
  static readonly getApiV10RolesPath = '/api/v1.0/roles';
  static readonly postApiV10RolesPath = '/api/v1.0/roles';
  static readonly putApiV10RolesDeletePath = '/api/v1.0/roles/delete';
  static readonly getApiV10RolesRolePath = '/api/v1.0/roles/role';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Returns all roles that belong to one client.
   * @param params The `RolesService.GetApiV10RolesParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `clientId`:
   *
   * - `Authorization`: Authorization
   *
   * @return OK
   */
  getApiV10RolesResponse(params: RolesService.GetApiV10RolesParams): __Observable<__StrictHttpResponse<RoleDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.clientId != null) __params = __params.set('clientId', params.clientId.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/roles`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<RoleDTO>;
      })
    );
  }
  /**
   * Returns all roles that belong to one client.
   * @param params The `RolesService.GetApiV10RolesParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `clientId`:
   *
   * - `Authorization`: Authorization
   *
   * @return OK
   */
  getApiV10Roles(params: RolesService.GetApiV10RolesParams): __Observable<RoleDTO> {
    return this.getApiV10RolesResponse(params).pipe(
      __map(_r => _r.body as RoleDTO)
    );
  }

  /**
   * Adds new role to the client.
   * @param params The `RolesService.PostApiV10RolesParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `clientId`: Clients' identifier
   *
   * - `body`: Role data
   *
   * - `Authorization`: Authorization
   */
  postApiV10RolesResponse(params: RolesService.PostApiV10RolesParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.clientId != null) __params = __params.set('clientId', params.clientId.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/v1.0/roles`,
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
   * Adds new role to the client.
   * @param params The `RolesService.PostApiV10RolesParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `clientId`: Clients' identifier
   *
   * - `body`: Role data
   *
   * - `Authorization`: Authorization
   */
  postApiV10Roles(params: RolesService.PostApiV10RolesParams): __Observable<null> {
    return this.postApiV10RolesResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Delete role.
   * @param params The `RolesService.PutApiV10RolesDeleteParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `roleName`: Roles' name
   *
   * - `clientId`: Clients' identifier
   *
   * - `Authorization`: Authorization
   */
  putApiV10RolesDeleteResponse(params: RolesService.PutApiV10RolesDeleteParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.roleName != null) __params = __params.set('roleName', params.roleName.toString());
    if (params.clientId != null) __params = __params.set('clientId', params.clientId.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/v1.0/roles/delete`,
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
   * Delete role.
   * @param params The `RolesService.PutApiV10RolesDeleteParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `roleName`: Roles' name
   *
   * - `clientId`: Clients' identifier
   *
   * - `Authorization`: Authorization
   */
  putApiV10RolesDelete(params: RolesService.PutApiV10RolesDeleteParams): __Observable<null> {
    return this.putApiV10RolesDeleteResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Returns one role.
   * @param params The `RolesService.GetApiV10RolesRoleParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `roleName`: Roles' name
   *
   * - `clientId`: Clients' identifier
   *
   * - `Authorization`: Authorization
   */
  getApiV10RolesRoleResponse(params: RolesService.GetApiV10RolesRoleParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.roleName != null) __params = __params.set('roleName', params.roleName.toString());
    if (params.clientId != null) __params = __params.set('clientId', params.clientId.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/roles/role`,
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
   * Returns one role.
   * @param params The `RolesService.GetApiV10RolesRoleParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `roleName`: Roles' name
   *
   * - `clientId`: Clients' identifier
   *
   * - `Authorization`: Authorization
   */
  getApiV10RolesRole(params: RolesService.GetApiV10RolesRoleParams): __Observable<null> {
    return this.getApiV10RolesRoleResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }
}

module RolesService {

  /**
   * Parameters for getApiV10Roles
   */
  export interface GetApiV10RolesParams {

    /**
     * API key
     */
    xApiKey?: any;
    clientId?: string;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for postApiV10Roles
   */
  export interface PostApiV10RolesParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Clients' identifier
     */
    clientId?: string;

    /**
     * Role data
     */
    body?: RoleWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for putApiV10RolesDelete
   */
  export interface PutApiV10RolesDeleteParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Roles' name
     */
    roleName?: string;

    /**
     * Clients' identifier
     */
    clientId?: string;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for getApiV10RolesRole
   */
  export interface GetApiV10RolesRoleParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Roles' name
     */
    roleName?: string;

    /**
     * Clients' identifier
     */
    clientId?: string;

    /**
     * Authorization
     */
    Authorization?: any;
  }
}

export { RolesService }
