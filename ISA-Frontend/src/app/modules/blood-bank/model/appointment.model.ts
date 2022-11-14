export class Appointment {
  id: number = 0;
  staffId: number = 0;
  staffName: string = '';
  centerId: number = 0;
  date: Date = new Date();
  duration: number = 0;
  
  public constructor(obj?: any) {
    if (obj) {

      this.id = obj.id;
      this.staffId = obj.staffId;
      this.centerId = obj.centerId;
      this.duration = obj.duration;
      this.date = obj.date;
    }
  }
}
