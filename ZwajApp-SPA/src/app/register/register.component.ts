import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { error } from 'util';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Input() valuesFromRegister: any;
  @Output() cancelRegister=new EventEmitter();
  constructor(private authservice: AuthService) { }

  ngOnInit() {
  }

  register() {
    this.authservice.register(this.model).subscribe(
      next => { console.log('تم الاشتراك') },
      error => { console.log(error) }
    )

  }

  cancel() {
    console.log('لم يتم الاشتراك');
    this.cancelRegister.emit(false);
  }
}
