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
class ImageFinderService extends __BaseService {
  static readonly getImageFinderPath = '/image-finder';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param search undefined
   */
  getImageFinderResponse(search?: string): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (search != null) __params = __params.set('search', search.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/image-finder`,
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
   * @param search undefined
   */
  getImageFinder(search?: string): __Observable<null> {
    return this.getImageFinderResponse(search).pipe(
      __map(_r => _r.body as null)
    );
  }
}

module ImageFinderService {
}

export { ImageFinderService }
