import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';

@Component({
  selector: 'trf-add-textual-content',
  standalone: true,
  imports: [AppMaterialModule, ReactiveFormsModule, FormsModule],
  templateUrl: './add-textual-content.component.html',
  styleUrls: ['./add-textual-content.component.css', '../../../shared/styles/modal.css']
})
export class AddTextualContentComponent {

  public addTextFormGroup: FormGroup = new FormGroup({
    title: new FormControl("", [Validators.required]),
    text: new FormControl("", [Validators.required])
  });


  constructor(private dialogRef: MatDialogRef<AddTextualContentComponent>) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }

  confirm() {
    this.dialogRef.close({
      title: this.addTextFormGroup.controls['title'].value,
      text: this.addTextFormGroup.controls['text'].value
    });
  }
}
