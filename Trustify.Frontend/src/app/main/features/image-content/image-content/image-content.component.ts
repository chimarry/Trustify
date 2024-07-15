import { Component, Input } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { ImageCardComponent } from '../image-card/image-card.component';
import { ImageContent } from '../../models/image-content';

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

  public imageList: ImageContent[] = [
    { name: "FirstImage", uploadedOn: new Date(2024,12,12)  },
    { name: "FirstImage4",uploadedOn: new Date(2024,12,12)},
    { name: "FirstImage11", uploadedOn: new Date(2024,12,12) },
    { name: "FirstImage1", uploadedOn: new Date(2024,12,12) },
    { name: "FirstImage3",uploadedOn: new Date(2024,12,12)},
    { name: "FirstImage5", uploadedOn: new Date(2024,12,12) },
    { name: "FirstImage8",uploadedOn: new Date(2024,12,12) },
    { name: "SecondImage", uploadedOn: new Date(2024,12,12)}]

  public displayImageList: ImageContent[] = [];

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

  updateImageList() {
    if (this.imageList) {
      this.displayImageList = this.imageList.slice(this.getStart(), this.getEnd());
    }
    this.updatePageNavigation();
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
