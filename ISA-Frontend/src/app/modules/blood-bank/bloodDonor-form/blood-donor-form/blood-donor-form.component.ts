import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'blood-donor-form',
  templateUrl: './blood-donor-form.component.html',
  styleUrls: ['./blood-donor-form.component.css']
})
export class BloodDonorFormComponent implements OnInit {

  public questions=[
    {text:"Have you ever volountarily donated blood or blood components?", checked:false},
    {text:"Have you ever been rejected as a blood donor?", checked:false},
    {text:"Do you currently feel healthy and rested enough to donate blood?", checked:false},
    {text:"Have you eaten anything prior to your arrival to donate blood?", checked:false},


  ]
  constructor() { }

  ngOnInit(): void {
  }

  sendForm(){

  }

}
