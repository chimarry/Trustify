import { Component } from '@angular/core';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';

@Component({
  selector: 'trf-header',
  standalone: true,
  imports: [AppMaterialModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  public goToMainApp() {
    window.location.href = "http://localhost:4200";
  }
}
