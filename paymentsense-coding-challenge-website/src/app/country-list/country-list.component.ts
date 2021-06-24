import { Component, OnInit, ViewChild } from '@angular/core';
import { CountryService } from '../services/country.service';
import { ActivatedRoute } from "@angular/router";
import { ICountry } from '../country/country';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from "@angular/animations";
import { MatPaginator, MatTableDataSource } from '@angular/material';


@Component({
  selector: "app-country-list",
  templateUrl: "./country-list.component.html",
  styleUrls: ["./country-list.component.scss"],
  animations: [
    trigger("detailExpand", [
      state("collapsed", style({ height: "0px", minHeight: "0" })),
      state("expanded", style({ height: "*" })),
      transition(
        "expanded <=> collapsed",
        animate("225ms cubic-bezier(0.4, 0.0, 0.2, 1)")
      ),
    ]),
  ],
})
export class CountryListComponent implements OnInit {
  countries: ICountry[];
  dataSource: MatTableDataSource<ICountry>;

  errorMessage = "";
  columnsToDisplay = ["name", "flag"];
  expandedCountry: ICountry | null;
  pageSize = 20;
  pageIndex = 0;
  loading = true;

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  constructor(
    private countryService: CountryService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.queryParams.subscribe((params) => {
      this.loading = true;
      !params.search || params.search === ""
        ? this.getCountries("")
        : this.getCountries(params.search);
    });
  }

  getCountries(search: string) {
    this.countryService.getCountries(search).subscribe({
      next: (countries) => {
        this.countries = countries;
        this.dataSource = new MatTableDataSource<ICountry>(countries);
        this.dataSource.paginator = this.paginator;
        this.dataView();
        this.loading = false;
      },
      error: (err) => {
        this.errorMessage = err;
      },
    });
  }

  addCountry(country:ICountry) {
    this.countries.unshift(country);
    this.dataSource = new MatTableDataSource(this.countries);
    this.dataView();
  }
  getPaginatorData(e) {
    this.pageIndex = e.pageIndex;
    this.pageSize = e.pageSize;
    this.dataView();
  }

  private dataView() {
    const end = (this.pageIndex + 1) * this.pageSize;
    const start = this.pageIndex * this.pageSize;
    const part = this.countries.slice(start, end);
    this.dataSource = new MatTableDataSource<ICountry>(part);
  }
}
