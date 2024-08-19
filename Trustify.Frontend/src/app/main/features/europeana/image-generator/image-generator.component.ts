import { HttpClient, HttpHeaders, provideHttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { FormsModule, NgModel } from '@angular/forms';
import { KeycloakService } from 'keycloak-angular';

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

  constructor(public http: HttpClient, private sanitizer: DomSanitizer, private keycloakService:KeycloakService) {

  }

  ngOnInit(): void {
    const isLoggedIn = this.keycloakService.isLoggedIn();
    if (!isLoggedIn)
      this.keycloakService.login();

   //this.keycloakService.updateToken();

    const userRoles = this.keycloakService.getUserRoles();

    this.http.get("https://localhost:7114/features")
      .subscribe(response => {console.log(response)})
 

    if (isLoggedIn){
      console.log(userRoles)
      //this.generate();
    }      
  }

  public generate(): void {
    console.log(this.word)
    this.isDisabled = true;
    const headerDict = {
      'Authorization':''
    }
    const requestOptions = {                                                                                                                                                                                 
      headers: new HttpHeaders(headerDict), 
    };
    this.http.get("https://api.europeana.eu/api/v2/search.json?wskey=turrymeter&query=" + this.word + "&type=IMAGE",
      requestOptions)
      .subscribe(response => {
        console.log(response)
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
        console.log(uri)
        this.image = uri;
        this.isDisabled = false;
      })
  }
}
