import { ICurrency } from "./currency";

export interface ICountry {
  name: string;
  capital: string;
  region: string;
  alpha3Code: string;
  flag:string;
  currencies: ICurrency[];
};
