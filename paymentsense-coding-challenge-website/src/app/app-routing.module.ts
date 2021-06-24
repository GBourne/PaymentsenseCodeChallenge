import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CountryListComponent } from './country-list/country-list.component';

const routes: Routes = [
  { path: "countries", component: CountryListComponent },
  { path: "", pathMatch: "full", redirectTo: "/countries" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
