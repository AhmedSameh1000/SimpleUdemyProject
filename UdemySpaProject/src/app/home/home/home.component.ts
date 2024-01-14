import { AuthService } from 'src/app/Services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  Securedata = [];

  constructor(private AuthService: AuthService) {}
  ngOnInit(): void {
    this.GetData();
  }
  GetData() {
    this.AuthService.GetData().subscribe({
      next: (res: any) => {
        this.Securedata = res;
      },
    });
  }
}
