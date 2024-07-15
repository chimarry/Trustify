import { Component } from '@angular/core';
import { TextualContent } from '../../models/textual-content';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { MatTableDataSource } from '@angular/material/table';
import { DatePipe } from '@angular/common';
import { TrfTableComponent } from '../../../shared/trf-table/trf-table/trf-table.component';
import { UserPreferenceService } from '../../../core/services/user-preference.service';

@Component({
  selector: 'trf-textual-content',
  standalone: true,
  imports: [AppMaterialModule, DatePipe],
  templateUrl: './textual-content.component.html',
  styleUrl: './textual-content.component.css',
  providers: [DatePipe]
})
export class TextualContentComponent extends TrfTableComponent {
  public textualContents: TextualContent[] = [
    { title: "Rat i mir", text: "Ovo je knjiga o ratu i miru od Lava Tolstoja.", length: 45, createdOn: new Date(2024, 3, 5) },
    { title: "Nicija zemlja", text: "Radnja se dešava u Bosni i Hercegovini 1993. u vrijeme najtežih borbi, a priča se usredsređuje na dva vojnika, Ninu i Čikija, koji pripadaju dvjema zaraćenim stranama, i nalaze se na ničijoj zemlji, dok je treći vojnik uhvaćen u zamku mine.[1] Borba, ponekad ironična, ponekad dramatična, između dva vojnika koji se nalaze zajedno u rovu između rovova svojih vojski stoji ujedno kao alegorija tih neprijateljstava i univerzalni simbol apsurdnosti svih ratova sa svojom iracionalnom mržnjom, neefikasnim mirovnim trupama i zbunjenošću medija.[2]", length: 544 },
    { title: "Crvena Knjiga", text: "Knjiga koju je napisao Karl Gustav Jung", length: 0 }
  ]

  public override displayedColumns: string[] = ["title", "text", "length", "createdOn", "actions"]

  constructor(public date: DatePipe, userPreferenceService: UserPreferenceService) {
    super(userPreferenceService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.dataSource.data = this.textualContents;
  }

  public select(row: any): void {

  }
}
