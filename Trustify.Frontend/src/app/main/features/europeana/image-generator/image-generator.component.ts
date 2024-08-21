import { HttpClient, HttpHeaders, provideHttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { FormsModule, NgModel } from '@angular/forms';
import { KeycloakService } from 'keycloak-angular';
import { ImageFinderService } from '../../../../api/image-finder/services';
import { NgIf } from '@angular/common';

@Component({
  selector: 'trf-image-generator',
  standalone: true,
  imports: [AppMaterialModule, FormsModule, NgIf],
  templateUrl: './image-generator.component.html',
  styleUrl: './image-generator.component.css',
})
export class ImageGeneratorComponent {
  public image: any;
  public word?: string | null;
  public isDisabled: boolean = false;
  public textSnippet?: string | null;

  constructor(public http: HttpClient, private sanitizer: DomSanitizer, private imageFinder: ImageFinderService) {

  }

  ngOnInit(): void {

  }

  public generate(): void {
    this.imageFinder.getImageFinder(this.word ?? "mona lisa").subscribe({
      next: response => {
        if (response != null) {
          this.image = this.sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,'
            + (response as any).fileData);
        }
      }
    })
  }
}