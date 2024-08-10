import { Component, Inject } from '@angular/core';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';
import { NgFor } from '@angular/common';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { GroupDTO } from '../../../api/models';
import { GroupsService } from '../../../api/services';

@Component({
  selector: 'trf-remove-from-group',
  standalone: true,
  imports: [AppMaterialModule, NgFor],
  templateUrl: './remove-from-group.component.html',
  styleUrls: ['./remove-from-group.component.css','../../../shared/styles/modal.css']
})
export class RemoveFromGroupComponent {
  public selectedGroup: GroupDTO | null = null;
  public userGroups: GroupDTO[] = [];
  public groups: GroupDTO[] = [];

  constructor(private dialogRef: MatDialogRef<RemoveFromGroupComponent>,
    private groupService: GroupsService, @Inject(MAT_DIALOG_DATA) public data: GroupDTO[]) {
this.userGroups=data;
  }

  ngOnInit() {
   this.groups = this.userGroups;
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
