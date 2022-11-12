export class Form {
    donorId:number=0;
    questionIds: number[] = [];
    answers: boolean[]=[];
  
    public constructor(obj?: any) {
      if (obj) {
        this.donorId=obj.donorId;
        this.questionIds=obj.questionIds;
        this.answers=obj.answers;
      }
    }
  }
  