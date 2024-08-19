import { Component, Input } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { ImageContent } from '../../models/image-content';
import { DatePipe } from '@angular/common';
import { ImageContentDTO } from '../../../../api/features/models';
import { ImageContentService } from '../../../../api/features/services';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'trf-image-card',
  standalone: true,
  imports: [AppMaterialModule, DatePipe],
  templateUrl: './image-card.component.html',
  styleUrl: './image-card.component.css',
  providers: [DatePipe]
})
export class ImageCardComponent {
  @Input() imageContent?: ImageContentDTO;
  public image: any;

  constructor(private service: ImageContentService,
    private sanitizer: DomSanitizer
  ) {

  }

  ngOnInit(): void {
    this.getImage();
  }

  getImage() {
    if (this.imageContent?.imageContentId)
      this.service.getImageContentImageContentIdDownloadResponse(this.imageContent?.imageContentId).subscribe({
        next: response => {
          if (response != null && response.body != null && response.body['result']) {
            let body = response.body['result'];
            this.image = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
              + body['fileData']);
          }
        }
      })
  }
}
