import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  public form = this.formBuilder.group({
    artisteName: ['', []],
  });
  constructor(private formBuilder: FormBuilder) {}

  search(form: FormGroup) {
    console.log(form);
  }
  ngOnInit(): void {}
}
