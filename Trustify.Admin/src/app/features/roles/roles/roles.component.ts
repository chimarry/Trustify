import { Component } from '@angular/core';
import { TrfTableComponent } from '../../../shared/trf-table/trf-table/trf-table.component';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { MatDialog } from '@angular/material/dialog';
import { UserPreferenceService } from '../../../core/services/user-preference.service';
import { Role } from '../../../core/models/role';
import { ConfirmDeleteComponent } from '../../../shared/confirm-delete/confirm-delete/confirm-delete.component';
import { AddRoleComponent } from '../add-role/add-role.component';
import { EditRoleComponent } from '../edit-role/edit-role.component';

@Component({
  selector: 'trf-roles',
  standalone: true,
  imports: [AppMaterialModule],
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css', '../../../shared/styles/table.css', '../../../shared/styles/modal.css'],
})
export class RolesComponent extends TrfTableComponent {
  public roles: Role[] = [
    { role: "admin", composite: true, description: "Can do anything" } as Role,
    { role: "guest", composite: true, description: "Small things" } as Role,
    { role: "text_editor", composite: true, description: "Can edit and add text" } as Role,
    { role: "image_editor", composite: true, description: "Can edit and add images" } as Role,
    { role: "large_content_editor", composite: true, description: "Uploads and downloads large data, bigger than 2gb" } as Role,
    { role: "super_admin", composite: true, description: "Manages other users and administrators" } as Role,
    { role: "premium_user", composite: true, description: "With paid membership, this user can do things others cannot." } as Role,
    { role: "regular_user", composite: true, description: "Regular downloads" } as Role
  ]

  public override displayedColumns: string[] = ["role","composite","description", "actions"]

  constructor(private dialog: MatDialog, userPreferenceService: UserPreferenceService) {
    super(userPreferenceService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.dataSource.data = this.roles;
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
        }
      });
  }

  public addNewRole() {
    this.dialog.open(AddRoleComponent, {
      panelClass: "trf-dialog-size-large"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
        }
      });
  }

  public editRole(role: Role) {
    this.dialog.open(EditRoleComponent, {
      panelClass: "trf-dialog-size-large",
      data: role
    })
      .afterClosed()
      .subscribe(result => {
        if (result as Role) {
        }
      });
  }
}
