/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { ClientDTO } from '../models/client-dto';
@Injectable({
  providedIn: 'root',
})
class ClientsService extends __BaseService {
  static readonly getApiV10ClientsPath = '/api/v1.0/clients';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `ClientsService.GetApiV10ClientsParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   *
   * @return OK
   */
  getApiV10ClientsResponse(params: ClientsService.GetApiV10ClientsParams): __Observable<__StrictHttpResponse<ClientDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.xApiKey != null) __headers = __headers.set('x-api-key', params.xApiKey.toString());
    if (params.Authorization != null) __headers = __headers.set('Authorization', params.Authorization.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/v1.0/clients`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ClientDTO>;
      })
    );
  }
  /**
   * @param params The `ClientsService.GetApiV10ClientsParams` containing the following parameters:
   *
   * - `x-api-key`: API key
   *
   * - `Authorization`: Authorization
   *
   * @return OK
   */
  getApiV10Clients(params: ClientsService.GetApiV10ClientsParams): __Observable<ClientDTO> {
    return this.getApiV10ClientsResponse(params).pipe(
      __map(_r => _r.body as ClientDTO)
    );
  }
}

module ClientsService {

  /**
   * Parameters for getApiV10Clients
   */
  export interface GetApiV10ClientsParams {

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

export { ClientsService }
