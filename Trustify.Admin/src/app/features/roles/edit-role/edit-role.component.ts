import { Component, Inject } from '@angular/core';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Role } from '../../../core/models/role';

@Component({
  selector: 'trf-edit-role',
  standalone: true,
  imports: [AppMaterialModule, ReactiveFormsModule, FormsModule],
  templateUrl: './edit-role.component.html',
  styleUrls: ['./edit-role.component.css', '../../../shared/styles/modal.css']
})
export class EditRoleComponent {

  public role: Role;

  public editRoleFormGroup: FormGroup = new FormGroup({
    role: new FormControl("", [Validators.required]),
    composite: new FormControl(false, [Validators.required]),
    description: new FormControl("", [Validators.required]),
  });


  constructor(private dialogRef: MatDialogRef<EditRoleComponent>, @Inject(MAT_DIALOG_DATA) public data: Role) {
    this.role = data;
    this.editRoleFormGroup.controls['role'].setValue(this.role.role);
    this.editRoleFormGroup.controls['composite'].setValue(this.role.composite);
    this.editRoleFormGroup.controls['description'].setValue(this.role.description);
  }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }

  confirm() {
    this.dialogRef.close({
      role: this.editRoleFormGroup.controls['role'].value,
      composite: this.editRoleFormGroup.controls['composite'].value,
      description: this.editRoleFormGroup.controls['description'].value
    });
  }
}
