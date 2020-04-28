import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { ValueService } from '../services/value.service';
@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements OnInit {

  constructor(private valueService:ValueService) { }

  ngOnInit() {
    this.valueService.getValue().subscribe(data=>{
      console.log(data);
    });
  }

}
