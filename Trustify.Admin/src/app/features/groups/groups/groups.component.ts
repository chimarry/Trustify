import { Component } from '@angular/core';
import { TrfTableComponent } from '../../../shared/trf-table/trf-table/trf-table.component';
import { GroupDTO, GroupWrapper } from '../../../api/models';
import { DatePipe } from '@angular/common';
import { UserPreferenceService } from '../../../core/services/user-preference.service';
import { GroupsService } from '../../../api/services';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDeleteComponent } from '../../../shared/confirm-delete/confirm-delete/confirm-delete.component';
import { AddGroupComponent } from '../add-group/add-group.component';
import { GroupDetailsComponent } from '../group-details/group-details.component';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { ManageRolesComponent } from '../manage-roles/manage-roles.component';
import { Group } from '../../../core/models/group';
import { ResultMessage } from '../../../core/models/result-message';
import { DisplayMessageService } from '../../../core/services/display-message.service';

@Component({
  selector: 'trf-groups',
  standalone: true,
  imports: [AppMaterialModule],
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css', '../../../shared/styles/table.css', '../../../shared/styles/modal.css'],
})
export class GroupsComponent extends TrfTableComponent {

  public groups: GroupDTO[] = [];
  public selectedGroup?: GroupDTO | null;

  public override displayedColumns: string[] = ["name", "path", "actions"];

  constructor(private dialog: MatDialog,private displayMessageService:DisplayMessageService, private groupService: GroupsService,
    userPreferenceService: UserPreferenceService) {
    super(userPreferenceService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.getGroups();
  }

  public getGroups(): void {
    this.groupService.getApiV10Groups({})
      .subscribe({
        next: response => {
          if (response)
            this.dataSource.data = (response as ResultMessage).result as GroupDTO[];
        }
      });
  }

  public select(row: GroupDTO): void {
    this.selectedGroup = row;
  }


  public delete(groupId?: string | null) {
    this.dialog.open(ConfirmDeleteComponent, {
      panelClass: "trf-dialog-size"
    })
      .afterClosed()
      .subscribe(result => {
        if (result && groupId) {
          this.groupService.putApiV10GroupsDelete({
            groupId: groupId
          } as GroupsService.PutApiV10GroupsDeleteParams)
            .subscribe({
              next: response => {
                if (response && response as ResultMessage) {
                  this.displayMessageService.displayStatus((response as ResultMessage).status);
                }
                this.getGroups()
              }
            }
            )
        }
      });
  }

  public addNewGroup() {
    this.dialog.open(AddGroupComponent, {
      panelClass: "trf-dialog-size-large"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.groupService.postApiV10Groups({
            body: result as GroupWrapper
          } as GroupsService.PostApiV10GroupsParams)
            .subscribe({
              next: response => {
                if (response && response as ResultMessage) {
                  this.displayMessageService.displayStatus((response as ResultMessage).status);
                }
                this.getGroups();
              }
            })
        }
      });
  }

  public manageRoles(group: GroupDTO) {
    this.groupService.getApiV10GroupsGroup({
      groupId: group.id
    } as GroupsService.GetApiV10GroupsGroupParams).subscribe({
      next: response => {
        if (response) {
          this.dialog.open(ManageRolesComponent, {
            panelClass: "trf-dialog-size-large",
            data: (response as ResultMessage).result as GroupDTO
          })
            .afterClosed()
            .subscribe(
              {
                next: result => {
                  if (result) {
                    // Delete roles
                    this.groupService.putApiV10GroupsRolesDelete({
                      body:
                      {
                        groupId: group.id,
                        roleWrappers: result.notSelectedRoles,
                        clientId: result.clientId
                      }
                    } as GroupsService.PutApiV10GroupsRolesDeleteParams)
                      .subscribe({
                        next: response =>{
                          if (response && response as ResultMessage) {
                            this.displayMessageService.displayStatus((response as ResultMessage).status);
                          }
                        }
                      });
                    // Add roles
                    this.groupService.putApiV10GroupsRoles({
                      body: {
                        groupId: group.id,
                        roleWrappers: result.selectedRoles,
                        clientId: result.clientId
                      }
                    }).subscribe({
                      next: response => {
                        if (response && response as ResultMessage) {
                          this.displayMessageService.displayStatus((response as ResultMessage).status);
                        }
                      }
                    })
                  }
                }
              }
            );
        }
      }
    })

  }

  public getDetails(groupId?: string | null) {
    this.groupService.getApiV10GroupsGroup(
      {
        groupId: groupId
      } as GroupsService.GetApiV10GroupsGroupParams
    ).subscribe(response => {
      if (response) {
        this.dialog.open(GroupDetailsComponent, {
          panelClass: "trf-dialog-size-large",
          data: (response as ResultMessage).result as GroupDTO
        })
          .afterClosed()
          .subscribe(result => {
            if (result) {
              console.log("Details were shown.")
            }
          });
      }
    })
  }
}
