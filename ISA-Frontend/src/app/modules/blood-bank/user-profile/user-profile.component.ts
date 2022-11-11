import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/modules/blood-bank/model/user.model';
import { UserService } from 'src/app/modules/blood-bank/services/user.service';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent {
  
  public user: User | undefined;

  constructor(private userService: UserService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.userService.getUser(params['id']).subscribe(res => {
        this.user = res;
      })
    });
  }
  goToEditPage() {
    this.router.navigate(['/edit-user-profile/{id}', { id: this.route.snapshot.paramMap.get('id')}]);
  }
}
