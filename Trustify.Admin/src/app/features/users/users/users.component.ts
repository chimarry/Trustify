import { Component } from '@angular/core';
import { TrfTableComponent } from '../../../shared/trf-table/trf-table/trf-table.component';
import { DatePipe } from '@angular/common';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { User } from '../../../core/models/user';
import { UserPreferenceService } from '../../../core/services/user-preference.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDeleteComponent } from '../../../shared/confirm-delete/confirm-delete/confirm-delete.component';
import { AddUserComponent } from '../add-user/add-user.component';
import { Role } from '../../../core/models/role';
import { UserDetailsComponent } from '../user-details/user-details.component';
import { EditUserComponent } from '../edit-user/edit-user.component';
import { UsersService } from '../../../api/services';
import { GroupDTO, UserDTO, UserWrapper } from '../../../api/models';
import { AddToGroupComponent } from '../add-to-group/add-to-group.component';
import { RemoveFromGroupComponent } from '../remove-from-group/remove-from-group.component';
import { ResultMessage } from '../../../core/models/result-message';
import { DisplayMessageService } from '../../../core/services/display-message.service';

@Component({
  selector: 'trf-users',
  standalone: true,
  imports: [AppMaterialModule, DatePipe],
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css', '../../../shared/styles/table.css', '../../../shared/styles/modal.css'],
  providers: [DatePipe]
})
export class UsersComponent extends TrfTableComponent {
  public users: UserDTO[] = [];

  public override displayedColumns: string[] = ["userName", "firstName", "lastName", "email", "createdTimestamp", "actions"]

  constructor(public date: DatePipe, private displayMessageService: DisplayMessageService,
    private dialog: MatDialog, private userService: UsersService,
    userPreferenceService: UserPreferenceService) {
    super(userPreferenceService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.getUsers();
  }

  public getUsers() {
    this.userService.getApiV10Users({})
      .subscribe({
        next: response => {
          if (response)
            this.dataSource.data = (response as ResultMessage).result as UserDTO[];
        }
      })
  }

  public select(row: any): void {

  }


  public delete(id: string) {
    this.dialog.open(ConfirmDeleteComponent, {
      panelClass: "trf-dialog-size"
    })
      .afterClosed()
      .subscribe({
        next: result => {
          if (result) {
            this.userService.putApiV10UsersDelete({
              body: {
                userId: id
              }
            }).subscribe(
              {
                next: response => {
                  if (response && response as ResultMessage) {
                    this.displayMessageService.displayStatus((response as ResultMessage).status);
                  }
                  this.getUsers()
                }
              })
          }
        }
      });
  }

  public addNewUser() {
    this.dialog.open(AddUserComponent, {
      panelClass: "trf-dialog-size-large"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.userService.postApiV10Users({
            body: result as UserWrapper
          } as UsersService.PostApiV10UsersParams)
            .subscribe({
              next: response => {
                if (response && response as ResultMessage) {
                  this.displayMessageService.displayStatus((response as ResultMessage).status);
                }
                this.getUsers();
              }
            })
        }
      });
  }

  public groupAdd(userId: string) {
    this.userService.getApiV10UsersGroups({
      UserId: userId
    } as UsersService.GetApiV10UsersGroupsParams)
      .subscribe({
        next: response => {
          if (response)
            this.dialog.open(AddToGroupComponent, {
              panelClass: "trf-dialog-size-large",
              data: (response as ResultMessage).result as GroupDTO[]
            })
              .afterClosed()
              .subscribe(result => {
                if (result) {
                  this.userService.putApiV10UsersGroupAdd({
                    body: {
                      userId: userId,
                      groupId: result.group
                    }
                  } as UsersService.PutApiV10UsersGroupAddParams)
                    .subscribe({
                      next: response => {
                        if (response && response as ResultMessage) {
                          this.displayMessageService.displayStatus((response as ResultMessage).status);
                        }
                        this.getUsers();
                      }
                    })
                }
              });
        }
      })
  }

  public groupRemove(userId: string) {
    this.userService.getApiV10UsersGroups({
      UserId: userId
    } as UsersService.GetApiV10UsersGroupsParams)
      .subscribe({
        next: response => {
          if (response)
            this.dialog.open(RemoveFromGroupComponent, {
              panelClass: "trf-dialog-size-large",
              data: (response as ResultMessage).result as GroupDTO[]
            })
              .afterClosed()
              .subscribe(result => {
                if (result) {
                  this.userService.putApiV10UsersGroupRemove({
                    body: {
                      userId: userId,
                      groupId: result.group
                    }
                  } as UsersService.PutApiV10UsersGroupRemoveParams)
                    .subscribe({
                      next: response => {
                        if (response && response as ResultMessage) {
                          this.displayMessageService.displayStatus((response as ResultMessage).status);
                        }
                        this.getUsers();
                      }
                    })
                }
              });
        }
      })
  }

  public getDetails(userId: string) {
    this.dialog.open(UserDetailsComponent, {
      panelClass: "trf-dialog-size-large",
      data: userId
    })
      .afterClosed()
      .subscribe();
  }

  public updatePassword(id: string) {

  }
}
