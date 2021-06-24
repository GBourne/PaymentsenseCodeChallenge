import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: "app-side-nav",
  templateUrl: "./side-nav.component.html",
  styleUrls: ["./side-nav.component.scss"],
})
export class SideNavComponent implements OnInit {
  searchVal: string;

  @Output() sideNavClose: EventEmitter<any>;

  constructor(private router:Router) {
    this.sideNavClose = new EventEmitter();
  }

  ngOnInit() {}

  public onSideNavClose = () => {
    this.sideNavClose.emit();
  };


}
