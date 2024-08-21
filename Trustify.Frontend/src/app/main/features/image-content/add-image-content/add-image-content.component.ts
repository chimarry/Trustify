import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-add-image-content',
  standalone: true,
  imports: [AppMaterialModule, FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './add-image-content.component.html',
  styleUrls: ['./add-image-content.component.css', '../../../shared/styles/modal.css']
})
export class AddImageContentComponent {
  public url?: any;
  public file?: any;
  public name: string = "";

  constructor(private dialogRef: MatDialogRef<AddImageContentComponent>) {

  }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(false);
  }

  confirm() {
    this.dialogRef.close({ name: this.name, image: this.file });
  }

  selectFile(event: any) {
    if (!event.target.files[0] || event.target.files[0].length == 0) {
      return;
    }
    var mimeType = event.target.files[0].type;

    if (mimeType.match(/image\/*/)) {
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (_event) => {
        this.url = reader.result;
        fetch(this.url).then(response => response.blob()).then(blob => this.file = blob);
      }
    }
  }
}