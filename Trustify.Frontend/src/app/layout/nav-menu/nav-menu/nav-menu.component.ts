import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { KeycloakService } from 'keycloak-angular';
import { UserPreferenceService } from '../../../main/core/services/user-preference.service';

@Component({
  selector: 'trf-nav-menu',
  standalone: true,
  imports: [RouterLink, MatIcon],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {
  constructor(private userPreferenceService:UserPreferenceService) {
  }

  ngOnInit(): void {
  }


  public isInRole(role: string) {
    return this.userPreferenceService.isInRole(role);

  }
}
