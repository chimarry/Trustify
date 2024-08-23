import { ChangeDetectorRef, Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NavMenuComponent } from '../../nav-menu/nav-menu/nav-menu.component';
import { HeaderComponent } from '../../header/header/header.component';
import { FooterComponent } from '../../footer/footer/footer.component';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { MatGridListModule } from '@angular/material/grid-list';
import { UserPreferenceService } from '../../../main/core/services/user-preference.service';
import { KeycloakService } from 'keycloak-angular';

@Component({
  selector: 'trf-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    NavMenuComponent,
    HeaderComponent,
    RouterLink,
    AppMaterialModule,
    MatGridListModule,
    FooterComponent],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {
  public userData?: {
    username?: string,
    firstName?: string,
    lastName?: string,
    email?: string,
    roles?: string[];
  } = {

    }
  public userRoles: { roles: string[] } = { roles: ['administrator', 'super_administrator'] }
  constructor(private userPreferenceService: UserPreferenceService, private keycloakService: KeycloakService) { }

  ngOnInit(): void {
    this.keycloakService.loadUserProfile().then(response => {
      this.userData = response as {
        username?: string,
        firstName?: string,
        lastName?: string,
        email?: string,
      };
      this.userData.roles = this.keycloakService.getUserRoles(true);
      this.userPreferenceService.saveUsername(this.userData.username ?? "");
    })
  }
}
