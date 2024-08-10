import { Component } from '@angular/core';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Role } from '../../../core/models/role';

@Component({
  selector: 'trf-add-role',
  standalone: true,
  imports: [AppMaterialModule, ReactiveFormsModule, FormsModule],
  templateUrl: './add-role.component.html',
  styleUrls: ['./add-role.component.css', '../../../shared/styles/modal.css']
})
export class AddRoleComponent {

  public addRoleFormGroup: FormGroup = new FormGroup({
    name: new FormControl("", [Validators.required]),
    description: new FormControl("", [Validators.required]),
  });


  constructor(private dialogRef: MatDialogRef<AddRoleComponent>) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }

  confirm() {
    this.dialogRef.close({
      name: this.addRoleFormGroup.controls['name'].value,
      description: this.addRoleFormGroup.controls['description'].value
    });
  }
}
