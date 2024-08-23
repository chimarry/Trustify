import { Component, Inject } from '@angular/core';
import { TextualContentDTO } from '../../../../api/features/models';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormControl, FormGroup, FormsModule, Validators } from '@angular/forms';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { DatePipe, formatDate } from '@angular/common';

@Component({
  selector: 'trf-textual-content-details',
  standalone: true,
  imports: [FormsModule, AppMaterialModule],
  templateUrl: './textual-content-details.component.html',
  styleUrls: ['./textual-content-details.component.css', '../../../shared/styles/modal.css']
})
export class TextualContentDetailsComponent {
  public textualContent: TextualContentDTO;
  public formattedDate?: string | null;


  constructor(private dialogRef: MatDialogRef<TextualContentDetailsComponent>,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) public data: TextualContentDTO) {
    this.textualContent = data;
    if (this.textualContent.createdOn) {
      this.formattedDate = this.datePipe.transform(this.textualContent.createdOn, "dd/MM/yyyy HH:mm");
    }
  }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(null);
  }
}
