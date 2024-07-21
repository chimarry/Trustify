import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NavMenuComponent } from '../../nav-menu/nav-menu/nav-menu.component';
import { HeaderComponent } from '../../header/header/header.component';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { MatGridListModule } from '@angular/material/grid-list';
import { FooterComponent } from '../../footer/footer/footer.component';

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

  constructor() { }

  ngOnInit(): void {

  }
}
