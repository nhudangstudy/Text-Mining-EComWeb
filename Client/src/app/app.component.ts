import { Component, OnInit } from '@angular/core';
import { Api } from 'src/Api';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Client';

  private api = new Api();
  
  ngOnInit(): void {
    this.api.weatherForecast
  }
}
