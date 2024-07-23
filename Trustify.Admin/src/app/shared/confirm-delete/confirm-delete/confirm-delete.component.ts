import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';

@Component({
  selector: 'trf-confirm-delete',
  standalone: true,
  imports: [AppMaterialModule],
  templateUrl: './confirm-delete.component.html',
  styleUrl: './confirm-delete.component.css'
})
export class ConfirmDeleteComponent {
  constructor(private dialogRef: MatDialogRef<ConfirmDeleteComponent>) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(false);
  }

  confirm() {
    this.dialogRef.close(true);
  }
}