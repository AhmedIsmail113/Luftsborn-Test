import { Injectable } from "@angular/core";
import { Params, Router, UrlSerializer } from "@angular/router";
import { environment } from "../../../environment/environment";


@Injectable({
  providedIn: 'root'
})
export class RouterHelper {

  constructor(private router: Router, private serializer: UrlSerializer) {
  }

  public getUrlWithQueryString(url: string, queryParams: Params): string {
    const fullUrl = this.router.createUrlTree([url], { queryParams: queryParams });
    return `${environment.resourceURL}${this.serializer.serialize(fullUrl)}`;
  }
}
