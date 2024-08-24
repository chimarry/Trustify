import { Component, Input } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { ImageCardComponent } from '../image-card/image-card.component';
import { ImageContentDTO } from '../../../../api/features/models';
import { ImageContentService } from '../../../../api/features/services';
import { DisplayMessageService } from '../../../core/services/display-message.service';
import { ResultMessage } from '../../../core/models/result-message';
import { MatDialog } from '@angular/material/dialog';
import { AddImageContentComponent } from '../add-image-content/add-image-content.component';
import { KeycloakService } from 'keycloak-angular';
import { UserPreferenceService } from '../../../core/services/user-preference.service';

@Component({
  selector: 'trf-image-content',
  standalone: true,
  imports: [AppMaterialModule, ImageCardComponent],
  templateUrl: './image-content.component.html',
  styleUrl: './image-content.component.css'
})

export class ImageContentComponent {
  public maxImagesDisplayCount: number = 2;
  public canSeePrevious: boolean = true;
  public canSeeNext: boolean = true;

  private page: number = 1;

  public imageList: ImageContentDTO[] = [];

  public displayImageList: ImageContentDTO[] = [];

  constructor(private imageContentService: ImageContentService,
    private userPreferenceService: UserPreferenceService,
    private dialog: MatDialog,
    private displayMessageService: DisplayMessageService) { }

  ngOnInit(): void {
    this.updateImageList();
  }

  public pageDown() {
    if (this.page > 1) {
      this.page--;
      this.updateImageList();
    }
  }

  public pageUp() {
    if (this.page < this.getPageCount()) {
      this.page++;
      this.updateImageList();
    }
  }

  public isInRole(role: string) {
    return this.userPreferenceService.isInRole(role);
  }

  add(): void {
    this.dialog.open(AddImageContentComponent, {
      panelClass: "trf-dialog-size-large"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.imageContentService.postImageContent({
            Name: result.name,
            Image: result.image
          }).subscribe({
            next: response => {
              this.updateImageList();
              this.displayMessageService.displayStatus((response as unknown as ResultMessage).status)
            }
          })
        }
      });
  }

  updateImageList() {
    this.imageContentService.getImageContent({ take: 200 }).subscribe({
      next: response => {
        this.imageList = (response as ResultMessage).result as ImageContentDTO[];
        if (this.imageList) {
          this.displayImageList = this.imageList.slice(this.getStart(), this.getEnd());
        }
        this.updatePageNavigation();
      }
    })
  }

  updatePageNavigation() {
    this.canSeeNext = this.page < this.getPageCount();
    this.canSeePrevious = this.page > 1;
  }

  getStart() {
    return (this.page - 1) * this.maxImagesDisplayCount;
  }

  getEnd() {
    if (this.imageList.length != 0)
      return this.page * this.maxImagesDisplayCount;
    return 0;
  }

  getPageCount() {
    if (this.imageList != null && this.imageList.length != 0)
      return Math.ceil(this.imageList.length / this.maxImagesDisplayCount);
    return 0;
  }
}
