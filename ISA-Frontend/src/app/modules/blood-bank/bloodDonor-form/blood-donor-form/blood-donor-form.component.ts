import { Component, OnInit } from '@angular/core';
import { Question } from '../../model/question.model';
import {QuestionService} from '../../services/question.service'

@Component({
  selector: 'blood-donor-form',
  templateUrl: './blood-donor-form.component.html',
  styleUrls: ['./blood-donor-form.component.css']
})
export class BloodDonorFormComponent implements OnInit {

  public questions: Question[]=[];

  constructor(private questionService:QuestionService) { }

  ngOnInit(): void {
    this.questionService.getQuestions().subscribe(res => {
      this.questions = res;
    });
  }

  sendForm(){

  }

}
