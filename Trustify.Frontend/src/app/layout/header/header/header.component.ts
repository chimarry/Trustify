import { Component } from '@angular/core';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { RouterLink } from '@angular/router';
import { KeycloakService } from 'keycloak-angular';

@Component({
  selector: 'trf-header',
  standalone: true,
  imports: [AppMaterialModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  constructor(private keycloakService: KeycloakService) {

  }

  public openAdminApp(): void {
    window.location.href = "https://192.168.100.6:4200";
  }

  public logOut() {
    this.keycloakService.logout();
    window.location.href ="https://localhost:4100";
  }
}
