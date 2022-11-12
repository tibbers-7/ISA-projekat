import { Component, OnInit } from '@angular/core';
import { Form } from '../../model/form.model';
import { Question } from '../../model/question.model';
import {QuestionService} from '../../services/question.service'
import { FormService } from '../../services/form.service';

@Component({
  selector: 'blood-donor-form',
  templateUrl: './blood-donor-form.component.html',
  styleUrls: ['./blood-donor-form.component.css']
})
export class BloodDonorFormComponent implements OnInit {

  public questions: Question[]=[];
  public form=new Form();
  public donorId:number=0;

  constructor(private questionService:QuestionService,private formService:FormService) { }

  ngOnInit(): void {
    this.questionService.getQuestions().subscribe(res => {
      this.questions = res;
    });
  }

  sendForm(){
    this.form.donorId=this.donorId;
    this.questions.forEach(element => {
      this.form.questionIds.push(element.id);
      this.form.answers.push(element.checked);
    });

    this.formService.getForm(this.form.donorId).subscribe(res=>{
      if (res===null){
        this.formService.createForm(this.form).subscribe(res => {
          console.log('created form!');
        });
      }
      else {
        this.formService.updateForm(this.form).subscribe(res => {
          console.log('updated form!');
        });
      }
    })

    

    
  }

}
