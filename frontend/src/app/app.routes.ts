import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import {FoodDiaryComponent} from './features/food-diary/food-diary.component';
import {WorkoutDiaryComponent} from './features/workout-diary/workout-diary.component';
import {BodyCompositionComponent} from './features/body-composition/body-composition.component';
import {GolfStatsComponent} from './features/golf-stats/golf-stats.component';
import {TechnicalBlogComponent} from './features/technical-blog/technical-blog.component';

export const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'food-diary', component: FoodDiaryComponent },
  { path: 'workout-diary', component: WorkoutDiaryComponent },
  { path: 'body-composition', component: BodyCompositionComponent },
  { path: 'golf-stats', component: GolfStatsComponent},
  { path: 'technical-blog', component: TechnicalBlogComponent }
];
