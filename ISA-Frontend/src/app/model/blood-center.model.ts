
export class BloodCenter {
  name: string = '';
  id: number = 0;
  description: string = ' ';
  workTimeStart: string = ' ';
  workTimeEnd: string ='';
  avgScore: number = 0;
  
  

  public constructor(obj?: any) {
    if (obj) {
      this.id = obj.id;
      this.name = obj.name;
      this.workTimeStart = obj.workTimeStart;
      this.workTimeEnd = obj.workTimeEnd;
      this.description = obj.description;
      this.avgScore = obj.avgScore;
    }
  }
}
