import { Component } from '@angular/core';
import { Route, RouterLink, RouterModule, RouterOutlet, provideRouter } from '@angular/router';
import { NavMenuComponent } from '../../nav-menu/nav-menu/nav-menu.component';
import { HeaderComponent } from '../../header/header/header.component';
import { FooterComponent } from '../../footer/footer/footer.component';
import { routes } from './layout.routes';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import {MatGridListModule} from '@angular/material/grid-list';

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

}
