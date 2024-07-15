import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { FaConfig, FaIconComponent, FaIconLibrary, FaLayersComponent, FaStackComponent, IconDefinition } from '@fortawesome/angular-fontawesome';
import { faCoffee, faUser, faUserAlt, faBell, faFontAwesome, faIcons } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'trf-nav-menu',
  standalone: true,
  imports: [RouterLink, FaIconComponent,  FaLayersComponent, FaStackComponent, MatIcon],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {
  public faMachine: IconDefinition = faCoffee;
  public faProcess: IconDefinition = faUserAlt;
  public faProfile: IconDefinition = faUser;
  public faNotification: IconDefinition = faBell;

  
  ngOnInit(): void {

  }
}
