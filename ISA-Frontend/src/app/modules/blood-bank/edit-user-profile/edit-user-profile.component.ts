import { Component, OnInit } from '@angular/core';
import { Router,ActivatedRoute, Params } from '@angular/router';
import { User } from '../model/user.model';
import { UserService } from '../services/user.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-edit-user-profile',
  templateUrl: './edit-user-profile.component.html',
  styleUrls: ['./edit-user-profile.component.css']
})
export class EditUserProfileComponent {
  
  public user = new User();
  apiHost: string = 'http://localhost:16177/';

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router, private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.userService.getUser(params['id']).subscribe(res => {
        this.user = res;
      })
    });
  }
  edit(): void {
    this.userService.updateUser(this.user).subscribe(res => {
      this.router.navigate(['/user-profile/{id}', { id: this.route.snapshot.paramMap.get('id') }]);
    });
  }
}
