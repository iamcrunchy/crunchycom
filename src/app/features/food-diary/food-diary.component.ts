import {Component, OnInit} from '@angular/core';
import {FormControl, ReactiveFormsModule} from '@angular/forms';
import {catchError, debounceTime, distinctUntilChanged, filter, map, Observable, of, startWith, switchMap} from 'rxjs';
import {SearchService} from '../../services/search-service';
import {AsyncPipe} from '@angular/common';

@Component({
  selector: 'app-food-diary',
  imports: [
    ReactiveFormsModule,
    AsyncPipe
  ],
  templateUrl: './food-diary.component.html',
  styleUrl: './food-diary.component.css',
})
export class FoodDiaryComponent implements OnInit{
// This would normally come from your .NET API/Service
  searchControl = new FormControl<string>('', { nonNullable: true});

  // declare the observable and tell Typescript that is will be defined before use
  results$!: Observable<string[]>;

  constructor(private readonly searchService: SearchService) {}

  filteredTopics$: Observable<string[]> | undefined;

  ngOnInit() {
    this.results$ = this.createResultsStream();
  }

  private createResultsStream(): Observable<string[]> {
    return this.searchControl.valueChanges.pipe(
      // Emit the current control value immediately so the stream produces an initial result set on subscribe.
      startWith(this.searchControl.value),
      // Wait for 300ms of “no typing” before continuing to avoid firing a request per keystroke.
      debounceTime(300),
      // Ignore new values if they are the same as the previous emitted value.
      distinctUntilChanged(),
      // Skip empty/whitespace-only search terms so we don’t query the API with meaningless input.
      filter(term => term.trim().length > 0),
      // For each term, call the API; if a new term arrives, cancel the previous in-flight request.
      switchMap(term =>
        this.searchService.getSearchData(term).pipe(
          // If the API call fails, log the error and recover by emitting an empty list (keeps the stream alive).
          catchError(error => {
            console.error(error);
            return of([] as string[]);
          })
        )
      )
    );
  }
}
