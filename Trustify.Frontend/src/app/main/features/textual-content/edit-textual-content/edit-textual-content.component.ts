import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { TextualContentDTO } from '../../../../api/features/models';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';

@Component({
  selector: 'trf-edit-textual-content',
  standalone: true,
  imports: [AppMaterialModule, ReactiveFormsModule, FormsModule],
  templateUrl: './edit-textual-content.component.html',
  styleUrls: ['./edit-textual-content.component.css','../../../shared/styles/modal.css']
})
export class EditTextualContentComponent {
  public textualContent: TextualContentDTO;

  constructor(private dialogRef: MatDialogRef<EditTextualContentComponent>,
    @Inject(MAT_DIALOG_DATA) public data: TextualContentDTO) {
    this.textualContent = data;
  }

  public addTextFormGroup: FormGroup = new FormGroup({
    title: new FormControl("", [Validators.required]),
    text: new FormControl("", [Validators.required])
  });

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }

  confirm() {
    this.dialogRef.close(this.textualContent);
  }
}
