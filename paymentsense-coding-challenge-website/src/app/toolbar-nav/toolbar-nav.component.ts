import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from "@angular/router";

@Component({
  selector: "app-toolbar-nav",
  templateUrl: "./toolbar-nav.component.html",
  styleUrls: ["./toolbar-nav.component.scss"],
})

export class NavToolbarComponent implements OnInit {

  @Output() sideNavToggle: EventEmitter<any>;

  searchVal:string


  constructor( private router:Router) {
    this.sideNavToggle = new EventEmitter();
  }

  ngOnInit() {

  }

  onToggleSidenav = () => {
    this.sideNavToggle.emit();
  };

}
