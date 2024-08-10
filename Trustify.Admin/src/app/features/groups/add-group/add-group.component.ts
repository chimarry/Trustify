import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AppMaterialModule } from '../../../modules/app-material/app-material.module';

@Component({
  selector: 'trf-add-group',
  standalone: true,
  imports: [AppMaterialModule, FormsModule, ReactiveFormsModule],
  templateUrl: './add-group.component.html',
  styleUrls: ['./add-group.component.css','../../../shared/styles/modal.css'],
})
export class AddGroupComponent {

  public addGroupFormGroup: FormGroup = new FormGroup({
    name:new FormControl(),
    path:new FormControl()
  });


  constructor(private dialogRef: MatDialogRef<AddGroupComponent>) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }

  confirm() {
    this.dialogRef.close({
      name: this.addGroupFormGroup.controls['name'].value,
      path: this.addGroupFormGroup.controls['path'].value
    });
  }
}
