import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { DatePipe } from '@angular/common';
import { ImageContentDTO } from '../../../../api/features/models';
import { ImageContentService } from '../../../../api/features/services';
import { DomSanitizer } from '@angular/platform-browser';
import { DisplayMessageService } from '../../../core/services/display-message.service';
import { ResultMessage } from '../../../core/models/result-message';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDeleteComponent } from '../../../shared/confirm-delete/confirm-delete.component';

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
  @Output() deleted: EventEmitter<boolean> = new EventEmitter<boolean>();
  public image: any;
  public size: number = 0.0;

  constructor(private service: ImageContentService,
    private sanitizer: DomSanitizer,
    private dialog: MatDialog,
    private displayMessageService: DisplayMessageService
  ) {
  }

  ngOnInit(): void {
    this.getImage();
    if(this.imageContent?.size)
    this.size = this.imageContent?.size / 1000.0;
  }

  getImage() {
    if (this.imageContent?.imageContentId)
      this.service.getImageContentImageContentIdDownloadResponse(this.imageContent?.imageContentId).subscribe({
        next: response => {
          if (response != null && response.body != null) {
            let body = response.body['result'];
            this.image = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
              + body['fileData']);
          }
        }
      })
  }

  download() {
  }

  delete() {
    if (this.imageContent?.imageContentId) {
      this.dialog.open(ConfirmDeleteComponent, {
        panelClass: "trf-dialog-size"
      })
        .afterClosed()
        .subscribe(result => {
          if (result && this.imageContent?.imageContentId) {
            this.service.deleteImageContentImageContentId(this.imageContent?.imageContentId).subscribe({
              next: response => {
                this.displayMessageService.displayStatus((response as unknown as ResultMessage).status)
                if ((response as unknown as ResultMessage).isSuccess)
                  this.deleted.emit(true);
              }
            })
          }
        });
    }
  }
}
