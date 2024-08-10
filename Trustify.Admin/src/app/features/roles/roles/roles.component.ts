import { Component } from '@angular/core';
import { TrfTableComponent } from '../../../shared/trf-table/trf-table/trf-table.component';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { MatDialog } from '@angular/material/dialog';
import { UserPreferenceService } from '../../../core/services/user-preference.service';
import { Role } from '../../../core/models/role';
import { ConfirmDeleteComponent } from '../../../shared/confirm-delete/confirm-delete/confirm-delete.component';
import { AddRoleComponent } from '../add-role/add-role.component';
import { EditRoleComponent } from '../edit-role/edit-role.component';
import { ClientsService, RolesService } from '../../../api/services';
import { ClientDTO, RoleDTO, RoleWrapper } from '../../../api/models';
import { NgFor } from '@angular/common';

@Component({
  selector: 'trf-roles',
  standalone: true,
  imports: [AppMaterialModule, NgFor],
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css', '../../../shared/styles/table.css', '../../../shared/styles/modal.css'],
})
export class RolesComponent extends TrfTableComponent {
  public selectedClient: ClientDTO | null = null;
  public clients: ClientDTO[] = [];
  public roles: RoleDTO[] = [];


  public override displayedColumns: string[] = ["role", "clientRole", "description", "actions"]

  constructor(private dialog: MatDialog, private clientService: ClientsService,
    private roleService: RolesService, userPreferenceService: UserPreferenceService) {
    super(userPreferenceService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.clientService.getApiV10Clients({} as ClientsService.GetApiV10ClientsParams)
      .subscribe({
        next: response => {
          if (response) {
            this.clients = response as ClientDTO[];
          }
        }
      })
  }

  public selectClient(event: ClientDTO) {
    this.selectedClient = event;
    this.getRoles();
  }
  public getRoles(): void {
    this.roleService.getApiV10Roles({
      clientId: this.selectedClient?.id
    } as RolesService.GetApiV10RolesParams)
      .subscribe({
        next: response => {
          this.dataSource.data = response as RoleDTO[];
        }
      })
  }

  public select(row: any): void {

  }

  public delete(role: RoleDTO) {
    this.dialog.open(ConfirmDeleteComponent, {
      panelClass: "trf-dialog-size"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.roleService.putApiV10RolesDelete({
            roleName: role.name,
            clientId: this.selectedClient?.id
          } as RolesService.PutApiV10RolesDeleteParams)
            .subscribe({
              next: response => this.getRoles()
            })
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
          var data = result as RoleWrapper;
          console.log(data)
          this.roleService.postApiV10Roles({
            body: data as RoleWrapper,
            clientId: this.selectedClient?.id
          } as RolesService.PostApiV10RolesParams)
            .subscribe({
              next: response => this.getRoles()
            })
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
          role.role = (result as Role).role;
          role.description = (result as Role).description;
          role.composite = (result as Role).composite;
        }
      });
  }
}
