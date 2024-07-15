import { Component } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';

@Component({
    selector: 'trf-large-content',
    standalone: true,
    imports: [AppMaterialModule],
    templateUrl: './large-content.component.html',
    styleUrl: './large-content.component.css'
  })
export class LargeContentComponent {
}