import { EventEmitter, Component, OnInit, Output } from "@angular/core";
import { NgForm } from "@angular/forms";
import { ICountry } from "./country";

@Component({
  selector: "app-country",
  templateUrl: "./country.component.html",
  styleUrls: ["./country.component.scss"],
})
export class CountryComponent implements OnInit {
  @Output() countryAdded: EventEmitter<ICountry>;

  country: ICountry

  constructor() {
    this.countryAdded = new EventEmitter<ICountry>();
  }

  ngOnInit() {
    this.reset();
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      let copyCountry = Object.assign({},this.country);
      copyCountry.flag = copyCountry.flag?copyCountry.flag:'assets/missing_flag.png';
      this.countryAdded.emit(copyCountry);
      this.reset();
      form.resetForm();

    }
  }

  reset() {
    this.country = {
      name: "",
      alpha3Code: "",
      region: "",
      flag: null,
      currencies: null,
      capital: "",
    };

  }
}
