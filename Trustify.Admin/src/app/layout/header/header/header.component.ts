import { Component } from '@angular/core';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { AuthService } from '../../../api/services';

@Component({
  selector: 'trf-header',
  standalone: true,
  imports: [AppMaterialModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  constructor(private authService: AuthService) {

  }
  public goToMainApp() {
    window.location.href = "https://192.168.100.6:4200";
  }

  public logOut() {
    this.authService.postApiV10AuthLogOut({}).subscribe({
      next: response => this.authService.getApiV10Auth({}).subscribe(),
      error: err => this.authService.getApiV10Auth({}).subscribe((res) => {
        if (!res) {
          window.location.replace('/api/v1.0/auth/login?returnUrl=' + encodeURIComponent(window.location.href));
        }
      })
    })
  }
}
