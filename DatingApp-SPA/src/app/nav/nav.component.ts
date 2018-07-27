import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public authService: AuthService,
              private alertify: AlertifyService,
              private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model)
        .subscribe(next => {
          this.alertify.success('Logged in successfully');

        }, error => {
          this.alertify.error(error);
        }, () => {
          this.router.navigate(['/members']);
        });
  }

  loggedIn() {
    return this.authService.loggedIn();
    }

  logout() {
    localStorage.removeItem('token');
    this.model = {};
    this.alertify.message('Logged off');
    this.router.navigate(['/home']);

  }

}
