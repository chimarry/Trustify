import { Component, Inject } from '@angular/core';
import { ClientDTO, GroupDTO, RoleDTO } from '../../../api/models';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ClientsService, RolesService } from '../../../api/services';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { NgFor } from '@angular/common';
import { ResultMessage } from '../../../core/models/result-message';

@Component({
  selector: 'trf-manage-roles',
  standalone: true,
  imports: [AppMaterialModule, NgFor],
  templateUrl: './manage-roles.component.html',
  styleUrls: ['./manage-roles.component.css','../../../shared/styles/modal.css']
})
export class ManageRolesComponent {
  public group: GroupDTO;

  public selectedRoles: Array<string> = [];
  public notSelectedRoles: Array<string> = [];
  public clientRoles: Array<RoleDTO> = [];

  public clients: Array<ClientDTO> = [];
  public selectedClient: ClientDTO | null = null;

  constructor(private dialogRef: MatDialogRef<ManageRolesComponent>, private roleService: RolesService,
    private clientService: ClientsService,
    @Inject(MAT_DIALOG_DATA) public data: GroupDTO) {
    this.group = data;
  }

  ngOnInit() {
    this.clientService.getApiV10Clients({} as ClientsService.GetApiV10ClientsParams)
      .subscribe({
        next: response => {
          if (response) {
            this.clients = ((response as unknown) as ResultMessage).result as ClientDTO[];
          }
        }
      })

  }

  close() {
    this.dialogRef.close();
  }

  confirm() {
    this.dialogRef.close({
      selectedRoles: this.clientRoles.filter(x => this.contains(this.selectedRoles, x)),
      notSelectedRoles: this.clientRoles.filter(x => this.contains(this.notSelectedRoles, x)),
      clientId: this.selectedClient?.id
    });
  }

  private contains(array: string[], role: RoleDTO) {
    return array.findIndex(x => x == role.name) !== -1;
  }

  public select(role: string) {
    this.selectedRoles.push(role);
    let index = this.notSelectedRoles.indexOf(role);
    if (index !== -1) {
      this.notSelectedRoles.splice(index, 1);
    }
  }

  public removeFromGroup(role: string) {
    this.notSelectedRoles.push(role);
    let index = this.selectedRoles.indexOf(role);
    if (index !== -1) {
      this.selectedRoles.splice(index, 1);
    }
  }

  public addToGroup(role: string) {
    this.selectedRoles.push(role);
    let index = this.notSelectedRoles.indexOf(role);
    if (index !== -1) {
      this.notSelectedRoles.splice(index, 1);
    }
  }

  public selectionChanged(event: any) {
    if (event) {
      this.notSelectedRoles.forEach(x =>
        this.selectedRoles.push(x));
      this.notSelectedRoles = [];
    }
  }

  public selectClient(event: ClientDTO) {
    this.selectedClient = event;
    var clientId = this.selectedClient.clientId ?? "";
    this.roleService.getApiV10Roles({
      clientId: this.selectedClient.id
    } as RolesService.GetApiV10RolesParams)
      .subscribe({
        next: roleArray => {
          var roles = ((roleArray as unknown) as ResultMessage).result as RoleDTO[];
          this.clientRoles = roles;
          if (this.group.clientRoles && this.group.clientRoles[clientId]) {
            this.selectedRoles = this.group?.clientRoles[clientId];
          }
          if (roles) {
            this.notSelectedRoles = roles.filter(role => this.notContains(this.selectedRoles, role.name))
              .map(x => x.name ?? "");
          }
        }
      })
  }

  private notContains(array: string[], word: string | null | undefined) {
    if (array === undefined)
      return true;
    return array.findIndex(x => x === word) === -1;
  }
}
