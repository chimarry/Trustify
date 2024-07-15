import { Component } from '@angular/core';
import { Route, RouterLink, RouterModule, RouterOutlet, Routes } from '@angular/router';
import { routes } from './admin-feature.routes';

@Component({
  selector: 'trf-admin-feature',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './admin-feature.component.html',
  styleUrl: './admin-feature.component.css'
})
export class AdminFeatureComponent {

}
