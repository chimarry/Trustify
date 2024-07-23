import { Component, Inject } from '@angular/core';
import { User } from '../../../core/models/user';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Role } from '../../../core/models/role';
import { Group } from '../../../core/models/group';

@Component({
  selector: 'trf-edit-user',
  standalone: true,
  imports: [AppMaterialModule, ReactiveFormsModule, FormsModule],
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css','../../../shared/styles/modal.css']
})
export class EditUserComponent {
  public user: User;

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

  public editUserFormGroup: FormGroup = new FormGroup({
    username: new FormControl("", [Validators.required]),
    firstName: new FormControl("", [Validators.required]),
    lastName: new FormControl("", [Validators.required]),
    password: new FormControl("", [Validators.required]),
    birthday: new FormControl(new Date(1, 1, 1971), [Validators.required]),
    address: new FormControl("", Validators.required),
    email: new FormControl("", Validators.required),
    confirmPassword: new FormControl("", [Validators.required])
  }, { validators: this.checkPasswords });


  constructor(private dialogRef: MatDialogRef<EditUserComponent>, @Inject(MAT_DIALOG_DATA) public data: User) {
    this.user = data;
    this.editUserFormGroup.controls['firstName'].setValue(this.user.firstName);
    this.editUserFormGroup.controls['lastName'].setValue(this.user.lastName);
    this.editUserFormGroup.controls['email'].setValue(this.user.email);
    this.editUserFormGroup.controls['username'].setValue(this.user.username);
    this.editUserFormGroup.controls['address'].setValue(this.user.address);
    this.editUserFormGroup.controls['birthday'].setValue(this.user.birthday);

    this.selectedRole = Object.assign([], data.roles ?? []);
    this.notSelectedRoles = this.notSelectedRoles.filter(x => !data.roles?.find(y => y === x));
    this.selectedGroups = Object.assign([], data.groups ?? []);
    this.notSelectedGroups = this.notSelectedGroups.filter(x => !data.groups?.find(y => y === x));
  }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close();
  }

  confirm() {
    this.dialogRef.close({
      username: this.editUserFormGroup.controls['username'].value,
      password: this.editUserFormGroup.controls['password'].value,
      firstName: this.editUserFormGroup.controls['firstName'].value,
      lastName: this.editUserFormGroup.controls['lastName'].value,
      email: this.editUserFormGroup.controls['email'].value,
      address: this.editUserFormGroup.controls['address'].value,
      birthday: this.editUserFormGroup.controls['birthday'].value,
      roles: this.selectedRole,
      groups: this.selectedGroups
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