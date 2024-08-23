import { Component, Input } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { ImageContentDTO, SectionDTO, TextualContentDTO } from '../../../../api/features/models';
import { ImageContentService, SectionsService, TextualContentService } from '../../../../api/features/services';
import { DomSanitizer } from '@angular/platform-browser';
import { ResultMessage } from '../../../core/models/result-message';
import { KeyValuePipe } from '@angular/common';

@Component({
  selector: 'trf-section',
  standalone: true,
  imports: [AppMaterialModule, KeyValuePipe],
  templateUrl: './section.component.html',
  styleUrl: './section.component.css'
})
export class SectionComponent {
  @Input() public section?: SectionDTO;
  public images: ImageContentDTO[] = [];
  public imagesData: any[] = [];
  public texts: TextualContentDTO[] = [];

  public imageCount: number = 0;
  public textCount: number = 0;
  public moreImages: boolean = true;

  constructor(private imageService: ImageContentService,
    private sectionService: SectionsService,
    private sanitizer: DomSanitizer,
    private textService: TextualContentService) {
  }

  ngOnChanges(){
    this.imagesData = [];
    this.images = [];
    this.texts = [];
    this.loadContent();
  }

  ngOnInit() {
  
   
  }

  public loadContent() {
    if (this.section?.sectionId) {
      this.sectionService.getSectionsSectionId(this.section.sectionId).subscribe({
        next:
          response => {
            this.section = (response as ResultMessage).result as SectionDTO;
            this.imageCount = this.section.imageContents?.length ?? 0;
            this.textCount = this.section.textualContents?.length ?? 0;
            this.moreImages = this.imageCount > this.textCount;
            console.log(this.moreImages)
            this.loadTextualContent();
            this.loadImageContent();
          }
      })
    }
  }

  public loadTextualContent() {
    this.section?.textualContents?.forEach(id => {
      this.textService.getTextualContentTextualContentId(id).subscribe({
        next: response => {
          var result = response as unknown as ResultMessage;
          this.texts.push(result.result as TextualContentDTO);
        }
      })
    })
  }

  public loadImageContent() {
    this.section?.imageContents?.forEach(id => {
      this.downloadImage(id);
    })
  }

  private downloadImage(imageContentId: number) {
    this.imageService.getImageContentImageContentIdDownloadResponse(imageContentId).subscribe({
      next: response => {
        if (response != null && response.body != null) {
          let body = response.body['result'];
          var image = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
            + body['fileData']);
          this.imagesData.push(image);
        }
      }
    })
  }
}
