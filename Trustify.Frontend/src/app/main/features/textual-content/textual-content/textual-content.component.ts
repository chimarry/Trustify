import { Component } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { DatePipe } from '@angular/common';
import { TrfTableComponent } from '../../../shared/trf-table/trf-table/trf-table.component';
import { UserPreferenceService } from '../../../core/services/user-preference.service';
import { TextualContentDTO, TextualContentWrapper } from '../../../../api/features/models';
import { TextualContentService } from '../../../../api/features/services';
import { ResultMessage } from '../../../core/models/result-message';
import { MatDialog } from '@angular/material/dialog';
import { DisplayMessageService } from '../../../core/services/display-message.service';
import { ConfirmDeleteComponent } from '../../../shared/confirm-delete/confirm-delete.component';
import { AddTextualContentComponent } from '../add-textual-content/add-textual-content.component';
import { EditTextualContentComponent } from '../edit-textual-content/edit-textual-content.component';
import { TextualContentDetailsComponent } from '../textual-content-details/textual-content-details.component';

@Component({
  selector: 'trf-textual-content',
  standalone: true,
  imports: [AppMaterialModule, DatePipe],
  templateUrl: './textual-content.component.html',
  styleUrls: ['./textual-content.component.css','../../../shared/styles/table.css'],
  providers: [DatePipe]
})
export class TextualContentComponent extends TrfTableComponent {
  public textualContents: TextualContentDTO[] = [];

  public override displayedColumns: string[] = ["title", "text", "length", "createdOn", "actions"]

  constructor(public date: DatePipe, private dialog: MatDialog,
    private displayMessageService: DisplayMessageService,
    userPreferenceService: UserPreferenceService,
    private textualContentService: TextualContentService) {
    super(userPreferenceService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.getTexts();
  }

  public getTexts() {
    this.textualContentService.getTextualContent({
      take: 10,
      skip: 0
    }).subscribe({
      next: response => {
        if (response) {
          console.log(response)
          var result = response as ResultMessage;
          if (result.isSuccess)
            this.dataSource.data = result.result as TextualContentDTO[];
        }
      }
    })
  }

  public select(row: any): void {

  }

  public add(): void {
    this.dialog.open(AddTextualContentComponent, {
      panelClass: "trf-dialog-size-large"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.textualContentService.postTextualContent(result as TextualContentWrapper).subscribe(
            {
              next: response => {
                if (response && response as ResultMessage) {
                  this.displayMessageService.displayStatus((response as ResultMessage).status)
                  this.getTexts();
                }
              }
            }
          )
        }
      });
  }

  public edit(row: TextualContentDTO) {
    this.dialog.open(EditTextualContentComponent, {
      panelClass: "trf-dialog-size",
      data: row
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
        }
      });
  }

  public showDetails(row:TextualContentDTO){
    this.dialog.open(TextualContentDetailsComponent, {
      panelClass: "trf-dialog-size-large",
      data: row
    })
      .afterClosed()
      .subscribe();
  }

  public delete(id: number) {
    this.dialog.open(ConfirmDeleteComponent, {
      panelClass: "trf-dialog-size"
    })
      .afterClosed()
      .subscribe(result => {
        if (result) {
          this.textualContentService.deleteTextualContentTextualContentId(id).subscribe(
            {
              next: response => {
                if (response && response as ResultMessage) {
                  this.displayMessageService.displayStatus((response as ResultMessage).status)
                  this.getTexts();
                }
              }
            }
          )
        }
      });
  }
}
