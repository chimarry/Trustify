import { Component } from '@angular/core';
import { Role } from '../../../core/models/role';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { Group } from '../../../core/models/group';
import { Credentials, UserWrapper } from '../../../api/models';

@Component({
  selector: 'trf-add-user',
  standalone: true,
  imports: [AppMaterialModule, ReactiveFormsModule, FormsModule],
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css', '../../../shared/styles/modal.css']
})
export class AddUserComponent {
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
    emailVerified: new FormControl("true", [Validators.pattern("true|false")]),
    enabled: new FormControl("true", Validators.required),
    email: new FormControl("", Validators.required),
    confirmPassword: new FormControl("", [Validators.required]),
    requiredActions: new FormControl("UPDATE_PASSWORD,VERIFY_EMAIL",Validators.pattern("UPDATE_PASSWORD|UPDATE_PASSWORD,VERIFY_EMAIL|VERIFY_EMAIL"))
  }, { validators: this.checkPasswords });


  constructor(private dialogRef: MatDialogRef<AddUserComponent>) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }

  confirm() {
    this.dialogRef.close({
      userName: this.registerUserFormGroup.controls['username'].value,
      credentials: [
        {
          type: "password",
          value: this.registerUserFormGroup.controls['password'].value
        } as Credentials
      ],
      firstName: this.registerUserFormGroup.controls['firstName'].value,
      lastName: this.registerUserFormGroup.controls['lastName'].value,
      email: this.registerUserFormGroup.controls['email'].value,
      emailVerified: (this.registerUserFormGroup.controls['emailVerified'].value=="true"),
      enabled: (this.registerUserFormGroup.controls['enabled'].value=="true"),
      requiredActions: this.registerUserFormGroup.controls['requiredActions'].value.split(',')
    } as UserWrapper);
  }
}