import { Component } from '@angular/core';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'trf-header',
  standalone: true,
  imports: [AppMaterialModule, RouterLink],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  public openAdminApp(): void {
    console.log("Open")
    window.location.href = "http://localhost:4100";
  }
}
