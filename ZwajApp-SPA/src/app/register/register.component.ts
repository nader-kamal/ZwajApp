import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from 'util';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegister=new EventEmitter();
  constructor(private authservice: AuthService,private alertifyService:AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authservice.register(this.model).subscribe(
      next => { this.alertifyService.success('تم الاشتراك') },
      error => { console.log(error) }
    )

  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
