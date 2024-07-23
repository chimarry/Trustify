import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserPreferenceService {

  private defaultLanugage: string = "en";
  public pageSizes: Array<number> = [1, 5, 10, 15, 20, 25, 50, 100];

  constructor() {
  }

  public setLanguage = (lang: string) => localStorage.setItem("lang", lang);

  public getLanguage = () => localStorage.getItem("lang") ?? this.defaultLanugage;

  public getPageSizes(maxLimit: number = 10) {
    return this.pageSizes;
  }

  public getNumberOfItems(): number {
    let item = localStorage.getItem("number");
    return item ? Number(item) : this.pageSizes[0];
  }

  public setNumberOfItems(number: number) {
    localStorage.setItem("number", number.toLocaleString());
  }
}
