import { Component, Input } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { ImageContent } from '../../models/image-content';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'trf-image-card',
  standalone: true,
  imports: [AppMaterialModule, DatePipe],
  templateUrl: './image-card.component.html',
  styleUrl: './image-card.component.css',
  providers: [DatePipe]
})
export class ImageCardComponent {
  @Input() imageContent?: ImageContent;
  public image: any;

  ngOnInit(): void {
    //this.image = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
    // + body['fileData']);
  }
}
