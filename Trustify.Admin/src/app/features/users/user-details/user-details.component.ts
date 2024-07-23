import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { User } from '../../../core/models/user';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';

@Component({
  selector: 'trf-user-details',
  standalone: true,
  imports: [AppMaterialModule],
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css', '../../../shared/styles/modal.css']
})
export class UserDetailsComponent {
  public user: User;
  constructor(private dialogRef: MatDialogRef<UserDetailsComponent>, @Inject(MAT_DIALOG_DATA) public data: User) {
    this.user = data;
  }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }
}