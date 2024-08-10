/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { GroupWrapper } from '../models/group-wrapper';
import { GroupDTO } from '../models/group-dto';
import { GroupRolesWrapper } from '../models/group-roles-wrapper';
@Injectable({
  providedIn: 'root',
})
class GroupsService extends __BaseService {
  static readonly getApiV10GroupsPath = '/api/v1.0/groups';
  static readonly postApiV10GroupsPath = '/api/v1.0/groups';
  static readonly putApiV10GroupsDeletePath = '/api/v1.0/groups/delete';
  static readonly getApiV10GroupsGroupPath = '/api/v1.0/groups/group';
  static readonly putApiV10GroupsRolesPath = '/api/v1.0/groups/roles';
  static readonly putApiV10GroupsRolesDeletePath = '/api/v1.0/groups/roles/delete';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Returns all groups of the realm.
   * @param params The `GroupsService.GetApiV10GroupsParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10GroupsResponse(params: GroupsService.GetApiV10GroupsParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/groups`,
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
   * Returns all groups of the realm.
   * @param params The `GroupsService.GetApiV10GroupsParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   */
  getApiV10Groups(params: GroupsService.GetApiV10GroupsParams): __Observable<null> {
    return this.getApiV10GroupsResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Adds group of users to the realm.
   * @param params The `GroupsService.PostApiV10GroupsParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: Group data
   *
   * - `Authorization`: Authorization
   */
  postApiV10GroupsResponse(params: GroupsService.PostApiV10GroupsParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/v1.0/groups`,
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
   * Adds group of users to the realm.
   * @param params The `GroupsService.PostApiV10GroupsParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: Group data
   *
   * - `Authorization`: Authorization
   */
  postApiV10Groups(params: GroupsService.PostApiV10GroupsParams): __Observable<null> {
    return this.postApiV10GroupsResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Deletes selected group from the realm.
   * @param params The `GroupsService.PutApiV10GroupsDeleteParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `groupId`: The unique group identifier
   *
   * - `Authorization`: Authorization
   */
  putApiV10GroupsDeleteResponse(params: GroupsService.PutApiV10GroupsDeleteParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.groupId != null) __params = __params.set('groupId', params.groupId.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/v1.0/groups/delete`,
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
   * Deletes selected group from the realm.
   * @param params The `GroupsService.PutApiV10GroupsDeleteParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `groupId`: The unique group identifier
   *
   * - `Authorization`: Authorization
   */
  putApiV10GroupsDelete(params: GroupsService.PutApiV10GroupsDeleteParams): __Observable<null> {
    return this.putApiV10GroupsDeleteResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Returns one selected group from the realm.
   * @param params The `GroupsService.GetApiV10GroupsGroupParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `groupId`: Unique identifier of the group
   *
   * - `Authorization`: Authorization
   *
   * @return OK
   */
  getApiV10GroupsGroupResponse(params: GroupsService.GetApiV10GroupsGroupParams): __Observable<__StrictHttpResponse<GroupDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.groupId != null) __params = __params.set('groupId', params.groupId.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/groups/group`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<GroupDTO>;
      })
    );
  }
  /**
   * Returns one selected group from the realm.
   * @param params The `GroupsService.GetApiV10GroupsGroupParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `groupId`: Unique identifier of the group
   *
   * - `Authorization`: Authorization
   *
   * @return OK
   */
  getApiV10GroupsGroup(params: GroupsService.GetApiV10GroupsGroupParams): __Observable<GroupDTO> {
    return this.getApiV10GroupsGroupResponse(params).pipe(
      __map(_r => _r.body as GroupDTO)
    );
  }

  /**
   * Add clients roles to the selected group.
   * @param params The `GroupsService.PutApiV10GroupsRolesParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: Data about roles: name and identifier
   *
   * - `Authorization`: Authorization
   */
  putApiV10GroupsRolesResponse(params: GroupsService.PutApiV10GroupsRolesParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/v1.0/groups/roles`,
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
   * Add clients roles to the selected group.
   * @param params The `GroupsService.PutApiV10GroupsRolesParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: Data about roles: name and identifier
   *
   * - `Authorization`: Authorization
   */
  putApiV10GroupsRoles(params: GroupsService.PutApiV10GroupsRolesParams): __Observable<null> {
    return this.putApiV10GroupsRolesResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * Delete roles from the group.
   * @param params The `GroupsService.PutApiV10GroupsRolesDeleteParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: Data such as group identifider and roles
   *
   * - `Authorization`: Authorization
   */
  putApiV10GroupsRolesDeleteResponse(params: GroupsService.PutApiV10GroupsRolesDeleteParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    __body = params.body;
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/v1.0/groups/roles/delete`,
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
   * Delete roles from the group.
   * @param params The `GroupsService.PutApiV10GroupsRolesDeleteParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `body`: Data such as group identifider and roles
   *
   * - `Authorization`: Authorization
   */
  putApiV10GroupsRolesDelete(params: GroupsService.PutApiV10GroupsRolesDeleteParams): __Observable<null> {
    return this.putApiV10GroupsRolesDeleteResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }
}

module GroupsService {

  /**
   * Parameters for getApiV10Groups
   */
  export interface GetApiV10GroupsParams {

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
   * Parameters for postApiV10Groups
   */
  export interface PostApiV10GroupsParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Group data
     */
    body?: GroupWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for putApiV10GroupsDelete
   */
  export interface PutApiV10GroupsDeleteParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * The unique group identifier
     */
    groupId?: string;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for getApiV10GroupsGroup
   */
  export interface GetApiV10GroupsGroupParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Unique identifier of the group
     */
    groupId?: string;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for putApiV10GroupsRoles
   */
  export interface PutApiV10GroupsRolesParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Data about roles: name and identifier
     */
    body?: GroupRolesWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }

  /**
   * Parameters for putApiV10GroupsRolesDelete
   */
  export interface PutApiV10GroupsRolesDeleteParams {

    /**
     * API key
     */
    xApiKey?: any;

    /**
     * Data such as group identifider and roles
     */
    body?: GroupRolesWrapper;

    /**
     * Authorization
     */
    Authorization?: any;
  }
}

export { GroupsService }
