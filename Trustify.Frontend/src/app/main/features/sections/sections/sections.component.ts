import { Component } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { DatePipe } from '@angular/common';
import { SectionDTO, SectionWrapper } from '../../../../api/features/models';
import { SectionsService } from '../../../../api/features/services';
import { DisplayMessageService } from '../../../core/services/display-message.service';
import { ResultMessage } from '../../../core/models/result-message';
import { SectionComponent } from '../section/section.component';
import { MatDialog } from '@angular/material/dialog';
import { AddSectionComponent } from '../add-section/add-section.component';

@Component({
  selector: 'trf-sections',
  standalone: true,
  imports: [AppMaterialModule, DatePipe, SectionComponent],
  templateUrl: './sections.component.html',
  styleUrl: './sections.component.css'
})
export class SectionsComponent {
  public colNumber: number = 2;
  public maxSectionsDisplayCount: number = 3;
  public canSeePrevious: boolean = true;
  public canSeeNext: boolean = true;

  private page: number = 1;
  public sectionList: SectionDTO[] = [];
  public displaysectionList: SectionDTO[] = [];
  public selectedSection?: SectionDTO;

  constructor(private sectionService: SectionsService, private dialog: MatDialog,
    private displayMessageService: DisplayMessageService) {
  }

  ngOnInit(): void {
    this.updateSectionList();
  }

  public pageDown() {
    if (this.page > 1) {
      this.page--;
      this.updateSectionList();
    }
  }

  public pageUp() {
    if (this.page < this.getPageCount()) {
      this.page++;
      this.updateSectionList();
    }
  }

  updateSectionList() {
    this.sectionService.getSections()
      .subscribe({
        next: response => {
          var result = response as unknown as ResultMessage;
          if (result.isSuccess) {
            this.sectionList = result.result as SectionDTO[];
            this.displaysectionList = this.sectionList.slice(this.getStart(), this.getEnd());
            if (this.selectedSection == null && this.sectionList.length > 0)
              this.selectedSection = this.sectionList[0]
            this.updatePageNavigation();
          }
        }
      })

  }

  updatePageNavigation() {
    this.canSeeNext = this.page < this.getPageCount();
    this.canSeePrevious = this.page > 1;
  }

  getStart() {
    return (this.page - 1) * this.maxSectionsDisplayCount;
  }

  getEnd() {
    if (this.sectionList.length != 0)
      return this.page * this.maxSectionsDisplayCount;
    return 0;
  }

  getPageCount() {
    if (this.sectionList != null && this.sectionList.length != 0)
      return Math.ceil(this.sectionList.length / this.maxSectionsDisplayCount);
    return 0;
  }

  public selectSection(section: SectionDTO) {
    this.selectedSection = section;
  }

  public addSection() {
    this.dialog.open(AddSectionComponent, {
      panelClass: "trf-dialog-size-largest",
    })
      .afterClosed()
      .subscribe(result => {
        this.sectionService.postSections(result as SectionWrapper)
          .subscribe({
            next: response => {
              let message = (response as unknown as ResultMessage);
              this.displayMessageService.displayStatus(message.status);
              this.updateSectionList();
            }
          })
      });
  }
}
