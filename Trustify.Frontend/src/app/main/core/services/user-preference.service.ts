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

  public saveRoles(roles: string[]) {
    localStorage.setItem("roles", JSON.stringify(roles));
  }

  public getRoles(): string[] {
    return JSON.parse(localStorage.getItem("roles") ?? "{}") as string[];
  }

  public isInRole(role:string){
    return this.getRoles().findIndex(x=>x===role)!=-1;
  }

  public notInRole(role:string){
    return !this.isInRole(role);
  }

  public saveUsername(username: string) {
    localStorage.setItem("username", username);
  }

  public getUsername(): string {
    return localStorage.getItem("username")??"";
  }
}
