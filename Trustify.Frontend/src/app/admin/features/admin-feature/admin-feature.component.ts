import { Component } from '@angular/core';
import { Route, RouterLink, RouterOutlet, Routes } from '@angular/router';

@Component({
  selector: 'trf-admin-feature',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './admin-feature.component.html',
  styleUrl: './admin-feature.component.css'
})
export class AdminFeatureComponent {

}
