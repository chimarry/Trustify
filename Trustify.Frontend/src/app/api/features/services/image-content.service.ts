/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { ImageContentDTO } from '../models/image-content-dto';
@Injectable({
  providedIn: 'root',
})
class ImageContentService extends __BaseService {
  static readonly getImageContentPath = '/image-content';
  static readonly postImageContentPath = '/image-content';
  static readonly deleteImageContentImageContentIdPath = '/image-content/{imageContentId}';
  static readonly getImageContentImageContentIdPath = '/image-content/{imageContentId}';
  static readonly getImageContentImageContentIdDownloadPath = '/image-content/{imageContentId}/download';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param params The `ImageContentService.GetImageContentParams` containing the following parameters:
   *
   * - `take`:
   *
   * - `skip`:
   *
   * @return OK
   */
  getImageContentResponse(params: ImageContentService.GetImageContentParams): __Observable<__StrictHttpResponse<ImageContentDTO>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (params.take != null) __params = __params.set('take', params.take.toString());
    if (params.skip != null) __params = __params.set('skip', params.skip.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/image-content`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<ImageContentDTO>;
      })
    );
  }
  /**
   * @param params The `ImageContentService.GetImageContentParams` containing the following parameters:
   *
   * - `take`:
   *
   * - `skip`:
   *
   * @return OK
   */
  getImageContent(params: ImageContentService.GetImageContentParams): __Observable<ImageContentDTO> {
    return this.getImageContentResponse(params).pipe(
      __map(_r => _r.body as ImageContentDTO)
    );
  }

  /**
   * @param params The `ImageContentService.PostImageContentParams` containing the following parameters:
   *
   * - `Name`:
   *
   * - `Image`:
   */
  postImageContentResponse(params: ImageContentService.PostImageContentParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let __formData = new FormData();
    __body = __formData;
    if (params.Name != null) { __formData.append('Name', params.Name as string | Blob);}
    if (params.Image != null) { __formData.append('Image', params.Image as string | Blob);}
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/image-content`,
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
   * @param params The `ImageContentService.PostImageContentParams` containing the following parameters:
   *
   * - `Name`:
   *
   * - `Image`:
   */
  postImageContent(params: ImageContentService.PostImageContentParams): __Observable<null> {
    return this.postImageContentResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param imageContentId undefined
   */
  deleteImageContentImageContentIdResponse(imageContentId: number): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/image-content/${encodeURIComponent(String(imageContentId))}`,
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
   * @param imageContentId undefined
   */
  deleteImageContentImageContentId(imageContentId: number): __Observable<null> {
    return this.deleteImageContentImageContentIdResponse(imageContentId).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param imageContentId undefined
   */
  getImageContentImageContentIdResponse(imageContentId: number): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/image-content/${encodeURIComponent(String(imageContentId))}`,
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
   * @param imageContentId undefined
   */
  getImageContentImageContentId(imageContentId: number): __Observable<null> {
    return this.getImageContentImageContentIdResponse(imageContentId).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param imageContentId undefined
   */
  getImageContentImageContentIdDownloadResponse(imageContentId: number): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/image-content/${encodeURIComponent(String(imageContentId))}/download`,
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
   * @param imageContentId undefined
   */
  getImageContentImageContentIdDownload(imageContentId: number): __Observable<null> {
    return this.getImageContentImageContentIdDownloadResponse(imageContentId).pipe(
      __map(_r => _r.body as null)
    );
  }
}

module ImageContentService {

  /**
   * Parameters for getImageContent
   */
  export interface GetImageContentParams {
    take?: number;
    skip?: number;
  }

  /**
   * Parameters for postImageContent
   */
  export interface PostImageContentParams {
    Name?: string;
    Image?: string;
  }
}

export { ImageContentService }
