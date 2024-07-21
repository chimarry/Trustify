import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { RouterLink } from '@angular/router';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';

@Component({
  selector: 'trf-nav-menu',
  standalone: true,
  imports: [RouterLink, MatIcon, AppMaterialModule],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {
  ngOnInit(): void {

  }
}
