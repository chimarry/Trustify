import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { User } from '../../../core/models/user';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { UsersService } from '../../../api/services';
import { UserDTO, UserDTOResultMessage } from '../../../api/models';
import { KeyValuePipe } from '@angular/common';
import { ResultMessage } from '../../../core/models/result-message';

@Component({
  selector: 'trf-user-details',
  standalone: true,
  imports: [AppMaterialModule, KeyValuePipe],
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css', '../../../shared/styles/modal.css']
})
export class UserDetailsComponent {
  public user: UserDTO = {} as UserDTO;

  constructor(private dialogRef: MatDialogRef<UserDetailsComponent>, private userService: UsersService,
    @Inject(MAT_DIALOG_DATA) public data: string) {
    this.userService.putApiV10UsersUserInfo({
      body: {
        userId: data
      }
    } as UsersService.PutApiV10UsersUserInfoParams)
      .subscribe({
        next: response =>{
          this.user = (response as UserDTOResultMessage).result as UserDTO;
      }
      })
  }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }
}