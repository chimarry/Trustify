import { HttpClient, HttpHeaders, provideHttpClient } from '@angular/common/http';
import { Component, SecurityContext } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { FormsModule, NgModel } from '@angular/forms';
import { KeycloakService } from 'keycloak-angular';
import { ImageFinderService } from '../../../../api/image-finder/services';
import { NgIf } from '@angular/common';
import { ImageContentService } from '../../../../api/features/services';
import { MatDialog } from '@angular/material/dialog';
import { AddImageContentComponent } from '../../image-content/add-image-content/add-image-content.component';
import { DisplayMessageService } from '../../../core/services/display-message.service';
import { ResultMessage } from '../../../core/models/result-message';

@Component({
  selector: 'trf-image-generator',
  standalone: true,
  imports: [AppMaterialModule, FormsModule, NgIf],
  templateUrl: './image-generator.component.html',
  styleUrl: './image-generator.component.css',
})
export class ImageGeneratorComponent {
  public image: any;
  public fileData?: string | undefined;
  public word?: string | null;
  public isDisabled: boolean = false;
  public textSnippet?: string | null;

  constructor(public http: HttpClient, private sanitizer: DomSanitizer,
    private displayMessageService: DisplayMessageService,
    private imageFinder: ImageFinderService,
    private imageService: ImageContentService,
    private dialog: MatDialog) {

  }

  ngOnInit(): void {

  }

  public generate(): void {
    this.imageFinder.getImageFinder(this.word ?? "mona lisa").subscribe({
      next: response => {
        if (response != null) {
          this.fileData = (response as any).fileData;
          this.image = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
            + (response as any).fileData);
        }
      }
    })
  }

  public addImage() {
    this.dialog.open(AddImageContentComponent, {
      panelClass: "trf-dialog-size-large",
      data: this.sanitizer.sanitize(SecurityContext.URL, this.sanitizer.bypassSecurityTrustUrl(this.image))
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.imageService.postImageContent({
            Name: result.name,
            Image: this.base64toBlob(this.fileData, "image/jpg") as any
          }).subscribe({
            next: response => {
              this.displayMessageService.displayStatus((response as unknown as ResultMessage).status)
            }
          })
        }
      });


    // this.dialog.open(AddImageContentComponent, {
    //   panelClass: "trf-dialog-size-large",
    //   data: this.image
    // })
    //   .afterClosed()
    //   .subscribe(result => {
    //     if (result) {

    //       //let somedata = this.sanitizer.sanitize(SecurityContext.URL, this.sanitizer.bypassSecurityTrustUrl(result.image));
    //       let uploadData = { Name: result.name, Image: ""};
    //       this.imageService.postImageContent(uploadData as ImageContentService.PostImageContentParams)
    //         .subscribe({
    //           next: response => {
    //             this.displayMessageService.displayStatus((response as unknown as ResultMessage).status)
    //           }
    //         })
    //     }
    //   });
  }

  private base64toBlob(base64Data: string | undefined, contentType: string): Blob {
    if (base64Data) {
      contentType = contentType || '';
      const sliceSize = 1024;
      const byteCharacters = atob(base64Data);
      const bytesLength = byteCharacters.length;
      const slicesCount = Math.ceil(bytesLength / sliceSize);
      const byteArrays = new Array(slicesCount);

      for (let sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
        const begin = sliceIndex * sliceSize;
        const end = Math.min(begin + sliceSize, bytesLength);

        const bytes = new Array(end - begin);
        for (let offset = begin, i = 0; offset < end; ++i, ++offset) {
          bytes[i] = byteCharacters[offset].charCodeAt(0);
        }
        byteArrays[sliceIndex] = new Uint8Array(bytes);
      }
      return new Blob(byteArrays);
    }
    return new Blob();
  }
}