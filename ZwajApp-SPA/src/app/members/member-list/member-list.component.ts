import { Component, OnInit } from '@angular/core';
import { User } from '../../_models/user';
import { UserService } from '../../_services/user.service';
import { AlertifyService } from '../../_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { error } from 'protractor';


@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  users: User[];

  constructor(public authService:AuthService ,private userService: UserService, private route: ActivatedRoute,
    private alertify: AlertifyService) { }

  ngOnInit() {
     this.loadUsers();

  }





  loadUsers() {
    this.userService.GetUsers().subscribe((users:User[])=>{
      this.users=users;
    },
    error=>this.alertify.error(error)
      
    )
  }

}
