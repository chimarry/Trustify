import { Component } from '@angular/core';
import { Role } from '../../../core/models/role';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { Group } from '../../../core/models/group';

@Component({
  selector: 'trf-add-user',
  standalone: true,
  imports: [AppMaterialModule, ReactiveFormsModule, FormsModule],
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css','../../../shared/styles/modal.css']
})
export class AddUserComponent {
  public selectedRole: Array<Role> = [];
  public notSelectedRoles: Array<Role> = [{ role: "admin" }, { role: "regular_user" }, { role: "premium_user" }, { role: "guest" }];

  public selectedGroups: Array<Group> = [];
  public notSelectedGroups: Array<Group> = [
    {
      name: "admin_group",
      roles: [{ role: 'admin_old' }, { role: 'admin_new' }]
    },
    {
      name: "users",
      roles: [{ role: "text_editor" }, { role: 'image_editor' }]
    }
  ]

  public checkPasswords: ValidatorFn = (group: AbstractControl): ValidationErrors | null => {
    let pass = group.get("password")?.value;
    let confirmPass = group.get("confirmPassword")?.value;
    return pass === confirmPass ? null : { notSame: true }
  }

  public registerUserFormGroup: FormGroup = new FormGroup({
    username: new FormControl("", [Validators.required]),
    firstName: new FormControl("", [Validators.required]),
    lastName: new FormControl("", [Validators.required]),
    password: new FormControl("", [Validators.required]),
    birthday: new FormControl(new Date(1,1,1971), [Validators.required]),
    address: new FormControl("", Validators.required),
    email: new FormControl("", Validators.required),
    confirmPassword: new FormControl("", [Validators.required])
  }, { validators: this.checkPasswords });


  constructor(private dialogRef: MatDialogRef<AddUserComponent>) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }

  confirm() {
    this.dialogRef.close({
      username: this.registerUserFormGroup.controls['username'].value,
      password: this.registerUserFormGroup.controls['password'].value,
      firstName: this.registerUserFormGroup.controls['firstName'].value,
      lastName: this.registerUserFormGroup.controls['lastName'].value,
    });
  }

  public select(permission: Role) {
    this.selectedRole.push(permission);
    let index = this.notSelectedRoles.indexOf(permission);
    if (index !== -1) {
      this.notSelectedRoles.splice(index, 1);
    }
  }

  public remove(permission: Role) {
    this.notSelectedRoles.push(permission);
    let index = this.selectedRole.indexOf(permission);
    if (index !== -1) {
      this.selectedRole.splice(index, 1);
    }
  }

  public addToGroup(group: Group) {
    this.selectedGroups.push(group);
    let index = this.notSelectedGroups.indexOf(group);
    if (index !== -1) {
      this.notSelectedGroups.splice(index, 1);
    }
  }

  public removeFromGroup(group: Group) {
    this.notSelectedGroups.push(group);
    let index = this.selectedGroups.indexOf(group);
    if (index !== -1) {
      this.selectedGroups.splice(index, 1);
    }
  }

  public selectionChanged(event: any) {
    if (event) {
      this.notSelectedRoles.forEach(x =>
        this.selectedRole.push(x));
      this.notSelectedRoles = [];
    }
  }
}
