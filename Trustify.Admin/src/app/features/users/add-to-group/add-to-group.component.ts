import { Component, Inject } from '@angular/core';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { NgFor } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { GroupDTO } from '../../../api/models';
import { GroupsService } from '../../../api/services';
import { ResultMessage } from '../../../core/models/result-message';

@Component({
  selector: 'trf-add-to-group',
  standalone: true,
  imports: [AppMaterialModule, NgFor],
  templateUrl: './add-to-group.component.html',
  styleUrls: ['./add-to-group.component.css','../../../shared/styles/modal.css']
})
export class AddToGroupComponent {
  public selectedGroup: GroupDTO | null = null;
  public userGroups: GroupDTO[] = [];
  public groups: GroupDTO[] = [];

  constructor(private dialogRef: MatDialogRef<AddToGroupComponent>,
    private groupService: GroupsService, @Inject(MAT_DIALOG_DATA) public data: GroupDTO[]) {

  }

  ngOnInit() {
    this.groupService.getApiV10Groups({}).subscribe({
      next: response => {
        if (response) {
          this.groups = (response as ResultMessage).result as GroupDTO[];
          this.groups = this.groups.filter(group => !this.contains(this.userGroups, group));
        }

      }
    })
  }

  close() {
    this.dialogRef.close(null);
  }

  confirm() {
    this.dialogRef.close({
      group: this.selectedGroup?.id
    });
  }

  selectGroup(group: GroupDTO) {
    this.selectedGroup = group;
  }


  private contains(groups: GroupDTO[], group: GroupDTO) {
    return groups.findIndex(x => x.id == group.id) !== -1;
  }
}
