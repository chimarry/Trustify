import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AppMaterialModule } from '../../../../modules/app-material/app-material.module';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UserPreferenceService } from '../../../core/services/user-preference.service';

@Component({
  selector: 'trf-trf-table',
  standalone: true,
  imports: [AppMaterialModule],
  templateUrl: './trf-table.component.html',
  styleUrl: './trf-table.component.css'
})
export class TrfTableComponent implements OnInit, AfterViewInit {
  @ViewChild('tbSort') productTbSort = new MatSort();
  @ViewChild('paginatorPageSize') paginatorPageSize?: MatPaginator;

  public filterPredicate: ((data: any, filter: string) => boolean) | null = null;

  public displayedColumns: string[] = [];
  public dataSource: MatTableDataSource<any> = new MatTableDataSource<any>();
  public pageSizes: Array<number>;
  public numberOfItems: number;

  constructor(protected userPreferenceService: UserPreferenceService) {
    this.pageSizes = userPreferenceService.getPageSizes();
    this.numberOfItems = userPreferenceService.getNumberOfItems();
  }

  @ViewChild(MatSort) set matSort(sort: MatSort) {
    this.dataSource.sort = sort;
  }

  ngOnInit(): void {
    if (this.filterPredicate != null) {
      this.dataSource.filterPredicate = this.filterPredicate;
    }
  }

  ngAfterViewInit() {
    this.setupPaginator();
  }

  public setupPaginator() {
    if (this.paginatorPageSize) {
      if (this.paginatorPageSize)
        this.paginatorPageSize.page.subscribe(x => {
          this.userPreferenceService.setNumberOfItems(x.pageSize)
        })
      this.paginatorPageSize.pageSize = this.numberOfItems;
      this.dataSource.paginator = this.paginatorPageSize;
    }
  }
}
