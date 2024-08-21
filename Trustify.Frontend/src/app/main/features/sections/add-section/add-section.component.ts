import { Component } from '@angular/core';
import { ImageContentDTO, SectionDTO, SectionWrapper, TextualContentDTO, TextualContentWrapper } from '../../../../api/features/models';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ImageContentService, TextualContentService } from '../../../../api/features/services';
import { ResultMessage } from '../../../core/models/result-message';
import { DomSanitizer } from '@angular/platform-browser';
import { AddImageContentComponent } from '../../image-content/add-image-content/add-image-content.component';
import { DisplayMessageService } from '../../../core/services/display-message.service';
import { AddTextualContentComponent } from '../../textual-content/add-textual-content/add-textual-content.component';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { FormsModule, NgModel, ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
class Role {
  roleId?: number;
  name?: string;
}

@Component({
  selector: 'trf-add-section',
  standalone: true,
  imports: [AppMaterialModule, FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './add-section.component.html',
  styleUrls: ['./add-section.component.css', '../../../shared/styles/modal.css']
})
export class AddSectionComponent {
  public description?: string;
  public title?: string;

  public randomText: TextualContentDTO[] = [];
  public randomImages: ImageContentDTO[] = [];

  public imageData: Map<number, any> = new Map<number, any>();

  public notSelectedRoles: Role[] = [{
    roleId: 1,
    name: "image_editor"
  },
  {
    roleId: 2,
    name: "text_editor"
  }];

  public selectedImages: ImageContentDTO[] = [];
  public selectedText: TextualContentDTO[] = [];
  public selectedRoles: Role[] = [];

  constructor(private dialogRef: MatDialogRef<AddSectionComponent>,
    private dialog: MatDialog,
    private imageService: ImageContentService,
    private sanitizer: DomSanitizer,
    private displayMessageService: DisplayMessageService,
    private textualContentService: TextualContentService
  ) {

  }

  ngOnInit() {
    this.imageData.clear();
    this.randomImages = [];
    this.randomText = [];
    this.getRandomImages();
    this.getRandomText();
  }

  close() {
    this.dialogRef.close(false);
  }

  confirm() {
    let section = {
      title: this.title,
      description: this.description,
      imageContents: this.selectedImages.map(x => x.imageContentId),
      textualContents: this.selectedText.map(x => x.textualContentId),
      roles: this.selectedRoles.map(x => x.roleId)
    } as SectionWrapper;
    this.dialogRef.close(section);
  }

  public addNewImage(): void {
    this.dialog.open(AddImageContentComponent, {
      panelClass: "trf-dialog-size-large"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.imageService.postImageContent({
            Name: result.name,
            Image: result.image
          }).subscribe({
            next: response => {
              let data = (response as unknown as ResultMessage);
              if (data.isSuccess) {
                this.randomImages.push(data.result as ImageContentDTO)
                this.downloadImage((data.result as ImageContentDTO).imageContentId)
              }
              this.displayMessageService.displayStatus(data.status)
            }
          })
        }
      });
  }

  public addNewText(): void {
    this.dialog.open(AddTextualContentComponent, {
      panelClass: "trf-dialog-size-large"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.textualContentService.postTextualContent(result as TextualContentWrapper).subscribe(
            {
              next: response => {
                let data = (response as unknown) as ResultMessage;
                if (data.isSuccess) {
                  this.randomText.push(data.result as TextualContentDTO);
                }
                this.displayMessageService.displayStatus(data.status)
              }
            }
          )
        }
      });
  }

  public selectImage(imageContend: ImageContentDTO) {
    this.selectedImages.push(imageContend)
    let index = this.randomImages.indexOf(imageContend);
    if (index !== -1) {
      this.randomImages.splice(index, 1);
    }
  }

  public deselectImage(imageContend: ImageContentDTO) {
    this.randomImages.push(imageContend);
    let index = this.selectedImages.indexOf(imageContend)
    if (index !== -1) {
      this.selectedImages.splice(index, 1);
    }
  }

  public selectText(textContent: TextualContentDTO) {
    this.selectedText.push(textContent)
    let index = this.randomText.indexOf(textContent);
    if (index !== -1) {
      this.randomText.splice(index, 1);
    }
  }

  public deselectText(textContent: TextualContentDTO) {
    this.randomText.push(textContent)
    let index = this.selectedText.indexOf(textContent);
    if (index !== -1) {
      this.selectedText.splice(index, 1);
    }
  }

  public selectRole(role: Role) {
    this.selectedRoles.push(role)
    let index = this.notSelectedRoles.indexOf(role);
    if (index !== -1) {
      this.notSelectedRoles.splice(index, 1);
    }
  }

  public deselectRole(role: Role) {
    this.notSelectedRoles.push(role)
    let index = this.selectedRoles.indexOf(role);
    if (index !== -1) {
      this.selectedRoles.splice(index, 1);
    }
  }

  public getImage(imageContentId?: number) {
    if (imageContentId)
      return this.imageData.get(imageContentId);
  }

  getRandomImages(): void {
    this.imageService.getImageContent({ take: 5, skip: 0 })
      .subscribe({
        next: response => {
          var list = (response as unknown as ResultMessage).result as ImageContentDTO[];
          list = list.slice(0, 5);
          this.randomImages = list;
          list.forEach(x => {
            if (x.imageContentId)
              this.downloadImage(x.imageContentId)
          });
        }
      })
  }

  getRandomText(): void {
    this.textualContentService.getTextualContent({ take: 5, skip: 0 })
      .subscribe({
        next: response => {
          this.randomText = (response as unknown as ResultMessage).result as TextualContentDTO[];
        }
      })
  }

  private downloadImage(imageContentId?: number) {
    if (imageContentId)
      this.imageService.getImageContentImageContentIdDownloadResponse(imageContentId).subscribe({
        next: response => {
          if (response != null && response.body != null) {
            let body = response.body['result'];
            var image = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
              + body['fileData']);
            this.imageData.set(imageContentId, image);
          }
        }
      })
  }
}
