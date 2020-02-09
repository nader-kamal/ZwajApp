import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode: boolean = false;
  constructor(private http: HttpClient, private authservice: AuthService,private route: Router) { }

  ngOnInit() {
    if (this.authservice.loggedIn) {
this.route.navigate(['/members']);
    }
  }

  registerToggle() {
    this.registerMode = true;
  }



  cancelRegister(mode: boolean) {
    this.registerMode = mode;
  }
}
