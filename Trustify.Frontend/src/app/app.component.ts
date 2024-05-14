import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppMaterialModule } from './modules/app-material/app-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LayoutComponent } from './layout/layout/layout/layout.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    AppMaterialModule,
    ReactiveFormsModule,
    FormsModule,
    LayoutComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Trustify.Frontend';
}
