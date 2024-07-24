import { HttpClient, provideHttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { FormsModule, NgModel } from '@angular/forms';

@Component({
  selector: 'trf-image-generator',
  standalone: true,
  imports: [AppMaterialModule, FormsModule],
  templateUrl: './image-generator.component.html',
  styleUrl: './image-generator.component.css',
})
export class ImageGeneratorComponent {
  public image: any = '../../../../../assets/images/noImage.png';
  public word?: string | null;
  public isDisabled: boolean = false;
  public textSnippet?: string | null;

  constructor(public http: HttpClient, private sanitizer: DomSanitizer) {

  }

  public generate(): void {
    console.log(this.word)
    this.isDisabled = true;
    this.http.get("https://api.europeana.eu/api/v2/search.json?wskey=turrymeter&query=" + this.word + "&type=IMAGE")
      .subscribe(response => {
        if (this.word) {
          var number = response.toString().indexOf(this.word);
          console.log(number)
          if (number > 10)
            this.textSnippet = response.toString().substring(number - 10, number + 100);
          console.log(this.textSnippet)
        }
        this.isDisabled = false;
        var uri;
        if ((response as any).items[0].edmPreview !== undefined)
          uri = (response as any).items[0].edmPreview[0];
        else if ((response as any).items[1].edmPreview !== undefined)
          uri = (response as any).items[1].edmPreview[0];
        else if (((response as any).items[2].edmPreview !== undefined))
          uri = (response as any).items[2].edmPreview[0];
        this.image = uri;
        this.isDisabled = false;
      })
  }
}
