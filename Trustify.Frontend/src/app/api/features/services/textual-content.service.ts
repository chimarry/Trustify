/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { TextualContentWrapper } from '../models/textual-content-wrapper';
import { TextualContentDTO } from '../models/textual-content-dto';
@Injectable({
  providedIn: 'root',
})
class TextualContentService extends __BaseService {
  static readonly getTextualContentPath = '/textual-content';
  static readonly postTextualContentPath = '/textual-content';
  static readonly deleteTextualContentTextualContentIdPath = '/textual-content/{textualContentId}';
  static readonly getTextualContentTextualContentIdPath = '/textual-content/{textualContentId}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `TextualContentService.GetTextualContentParams` containing the following parameters:
   *
   * - `take`:
   *
   * - `skip`:
   */
  getTextualContentResponse(params: TextualContentService.GetTextualContentParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.take != null) __params = __params.set('take', params.take.toString());
    if (params.skip != null) __params = __params.set('skip', params.skip.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/textual-content`,
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
   * @param params The `TextualContentService.GetTextualContentParams` containing the following parameters:
   *
   * - `take`:
   *
   * - `skip`:
   */
  getTextualContent(params: TextualContentService.GetTextualContentParams): __Observable<null> {
    return this.getTextualContentResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param body undefined
   */
  postTextualContentResponse(body?: TextualContentWrapper): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/textual-content`,
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
   * @param body undefined
   */
  postTextualContent(body?: TextualContentWrapper): __Observable<null> {
    return this.postTextualContentResponse(body).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param textualContentId undefined
   */
  deleteTextualContentTextualContentIdResponse(textualContentId: number): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/textual-content/${encodeURIComponent(String(textualContentId))}`,
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
   * @param textualContentId undefined
   */
  deleteTextualContentTextualContentId(textualContentId: number): __Observable<null> {
    return this.deleteTextualContentTextualContentIdResponse(textualContentId).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param textualContentId undefined
   * @return OK
   */
  getTextualContentTextualContentIdResponse(textualContentId: number): __Observable<__StrictHttpResponse<TextualContentDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/textual-content/${encodeURIComponent(String(textualContentId))}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<TextualContentDTO>;
      })
    );
  }
  /**
   * @param textualContentId undefined
   * @return OK
   */
  getTextualContentTextualContentId(textualContentId: number): __Observable<TextualContentDTO> {
    return this.getTextualContentTextualContentIdResponse(textualContentId).pipe(
      __map(_r => _r.body as TextualContentDTO)
    );
  }
}

module TextualContentService {

  /**
   * Parameters for getTextualContent
   */
  export interface GetTextualContentParams {
    take?: number;
    skip?: number;
  }
}

export { TextualContentService }
