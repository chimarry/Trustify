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

@Component({
  selector: 'trf-users',
  standalone: true,
  imports: [AppMaterialModule, DatePipe],
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css', '../../../shared/styles/table.css','../../../shared/styles/modal.css'],
  providers: [DatePipe]
})
export class UsersComponent extends TrfTableComponent {
  public users: User[] = [
    {
      username: "nidze", firstName: "Nikolaj", lastName: "Vasic", birthday: new Date(2024, 3, 30),
      email: "marijav@gmail.com", address: "Koste Jarica 35E, Banja Luka", isUserActive: true, lastActivity: '2024.07.23 19:14',
      roles: [{ role: "admin" } as Role],
      groups: [{ name: 'feature_admins', roles: [{ role: 'admin' } as Role, { role: 'guest' } as Role] }]
    },
    {
      username: "vanja", firstName: "Vanja", lastName: "Novakovic", birthday: new Date(1998, 12, 21),
      email: "marijav@gmail.com", address: "Koste Jarica 35E, Banja Luka", isUserActive: true, lastActivity: '2024.07.23 20:00',
      roles: [{ role: "guest" } as Role],
      groups: [{ name: "guest_group", roles: [{ role: 'guest' } as Role] }
      ]
    },
    {
      username: "vasicm", firstName: "Marija", lastName: "Vasic", birthday: new Date(12, 10, 1997),
      email: "marijav@gmail.com", address: "Koste Jarica 35E, Banja Luka", isUserActive: true,
    },
    {
      username: "vasicMarko", firstName: "Marko", lastName: "Vasic", birthday: new Date(1992, 9, 24),
      email: "vasic.marko@mail.ru", address: "Rade Vranjesevic 23, Banja Luka", isUserActive: false
    }]

  public override displayedColumns: string[] = ["username", "firstName", "lastName", "email", "address", "birthday", "isUserActive", "actions"]

  constructor(public date: DatePipe, private dialog: MatDialog, userPreferenceService: UserPreferenceService) {
    super(userPreferenceService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.dataSource.data = this.users;
  }

  public select(row: any): void {

  }


  public delete(username: string) {
    this.dialog.open(ConfirmDeleteComponent, {
      panelClass: "trf-dialog-size"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          console.log("Deleted: " + username)
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
          console.log("Deleted: ")
        }
      });
  }

  public getDetails(user: User) {
    this.dialog.open(UserDetailsComponent, {
      panelClass: "trf-dialog-size-large",
      data: user
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          console.log("Details: ")
        }
      });
  }

  public editUser(user: User) {
    this.dialog.open(EditUserComponent, {
      panelClass: "trf-dialog-size-large",
      data: user
    })
      .afterClosed()
      .subscribe(result => {
        if (result as User) {
          console.log(result.username);
          console.log(result.roles)
        }
      });
  }
}
