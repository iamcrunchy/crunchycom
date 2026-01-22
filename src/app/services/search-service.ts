import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpParams} from '@angular/common/http';
import {catchError, Observable, retry, throwError} from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SearchService {
  // TODO: make sure to change this to HTTPS when deployed
  private readonly apiUrl = 'http://localhost:5261/api/';

  constructor(private http: HttpClient) { }

  // fetch the search data from the API
  getSearchData(searchTerm: string): Observable<string[]>{
    // set the params, use HttpParams to URL Encode the searchTerm into the URL
    const params = new HttpParams().set('searchTerm', searchTerm);

    return this.http.get<string[]>(this.apiUrl + 'search', { params }).pipe(
      retry(1),
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An error occurred: ';

    if (error.error instanceof ErrorEvent) {
      // client side or network error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // backend returned an unsuccessful response code
      errorMessage = `Server Error Code: ${error.status}, Error Message: ${error.message}`;
    }

    console.log(errorMessage);
    return throwError(() => new Error(errorMessage));
  }

}
