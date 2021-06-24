import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { ICountry } from "../country/country";
import { catchError } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class CountryService {
  constructor(private http: HttpClient) {}

  getCountries(search:string): Observable<ICountry[]> {
    let searchStr = search === "" ? "" : `?search=${search}`;
    return this.http
      .get<ICountry[]>(
        `https://localhost:44373/PaymentsenseCodingChallenge/countries${searchStr}`
      )
      .pipe(catchError(this.handleError));
  }

  handleError(err: HttpErrorResponse) {
    let errorMessage = "";

    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error has occured: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code : ${err.status}, error message is ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
