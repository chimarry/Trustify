/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { SectionWrapper } from '../models/section-wrapper';
import { SectionDTO } from '../models/section-dto';
@Injectable({
  providedIn: 'root',
})
class SectionsService extends __BaseService {
  static readonly getSectionsPath = '/sections';
  static readonly postSectionsPath = '/sections';
  static readonly deleteSectionsSectionIdPath = '/sections/{sectionId}';
  static readonly getSectionsSectionIdPath = '/sections/{sectionId}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }
  getSectionsResponse(): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/sections`,
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
  }  getSections(): __Observable<null> {
    return this.getSectionsResponse().pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param body undefined
   */
  postSectionsResponse(body?: SectionWrapper): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = body;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/sections`,
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
  postSections(body?: SectionWrapper): __Observable<null> {
    return this.postSectionsResponse(body).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param sectionId undefined
   */
  deleteSectionsSectionIdResponse(sectionId: number): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/sections/${encodeURIComponent(String(sectionId))}`,
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
   * @param sectionId undefined
   */
  deleteSectionsSectionId(sectionId: number): __Observable<null> {
    return this.deleteSectionsSectionIdResponse(sectionId).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param sectionId undefined
   * @return OK
   */
  getSectionsSectionIdResponse(sectionId: number): __Observable<__StrictHttpResponse<SectionDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/sections/${encodeURIComponent(String(sectionId))}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<SectionDTO>;
      })
    );
  }
  /**
   * @param sectionId undefined
   * @return OK
   */
  getSectionsSectionId(sectionId: number): __Observable<SectionDTO> {
    return this.getSectionsSectionIdResponse(sectionId).pipe(
      __map(_r => _r.body as SectionDTO)
    );
  }
}

module SectionsService {
}

export { SectionsService }
