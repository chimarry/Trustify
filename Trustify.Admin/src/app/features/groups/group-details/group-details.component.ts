import { Component, Inject } from '@angular/core';
import { GroupDTO } from '../../../api/models';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'trf-group-details',
  standalone: true,
  imports: [AppMaterialModule],
  templateUrl: './group-details.component.html',
  styleUrl: './group-details.component.css'
})
export class GroupDetailsComponent {
  public group: GroupDTO;

  constructor(private dialogRef: MatDialogRef<GroupDetailsComponent>, @Inject(MAT_DIALOG_DATA) public data: GroupDTO) {
    this.group = data;
  }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }

}
