import { Component } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'trf-sections',
  standalone: true,
  imports: [AppMaterialModule,DatePipe],
  templateUrl: './sections.component.html',
  styleUrl: './sections.component.css'
})
export class SectionsComponent {
  public colNumber: number = 2;
  public maxSectionsDisplayCount: number = 3;
  public canSeePrevious: boolean = true;
  public canSeeNext: boolean = true;

  private page: number = 1;

  public sectionList: { title: string, author: string, uploadedOn: Date }[] = [
    { title: "Razvoj bebe po mjesecima", author: "Marija Vasic", uploadedOn: new Date(2024, 12, 12) },
    { title: "Kako zabrinuti roditelje?", author: "Nikolaj Vasic", uploadedOn: new Date(2024, 12, 12) },
    { title: "Naljutiti zenu", author: "Marko Vasic", uploadedOn: new Date(2024, 12, 12) },
    { title: "Povratak na posao 2", author: "Vanja Novakovic", uploadedOn: new Date(2024, 12, 12) },
    { title: "Mrgud danas", author: "Njegos Dukic", uploadedOn: new Date(2024, 12, 12) }
  ]

  public displaysectionList: { title: string, author: string, uploadedOn: Date }[] = [];

  ngOnInit(): void {
    this.updatesectionList();
  }

  public pageDown() {
    if (this.page > 1) {
      this.page--;
      this.updatesectionList();
    }
  }

  public pageUp() {
    if (this.page < this.getPageCount()) {
      this.page++;
      this.updatesectionList();
    }
  }

  updatesectionList() {
    if (this.sectionList) {
      this.displaysectionList = this.sectionList.slice(this.getStart(), this.getEnd());
    }
    this.updatePageNavigation();
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
}
