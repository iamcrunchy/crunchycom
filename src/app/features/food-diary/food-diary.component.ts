import {Component, OnInit} from '@angular/core';
import {FormControl} from '@angular/forms';
import {map, Observable, startWith} from 'rxjs';

@Component({
  selector: 'app-food-diary',
  imports: [],
  templateUrl: './food-diary.component.html',
  styleUrl: './food-diary.component.css',
})
export class FoodDiaryComponent implements OnInit{
// This would normally come from your .NET API/Service
  searchControl = new FormControl('');

  // Simulated database records
  private databaseItems: string[] = [
    '.NET Core Web API',
    'Angular Reactive Forms',
    'iOS Physical Acquisition',
    'Android Logical Acquisition',
    'CompTIA A+ Core 1',
    'Cybersecurity Network Forensics',
    'Proxmox Home Server Setup'
  ];

  filteredTopics$: Observable<string[]> | undefined;

  ngOnInit() {
    // Logic to filter the datalist as the user types
    this.filteredTopics$ = this.searchControl.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || ''))
    );
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();
    return this.databaseItems.filter(option =>
      option.toLowerCase().includes(filterValue)
    );
  }
}
