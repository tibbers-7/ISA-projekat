export class Appointment {
  id: number = 0;
  staffId: number = 0;
  donorId: number = 0;
  centerId: number = 0;
  date: string = '';
  duration: number = 0;
  status: string = '';
  
  public constructor(obj?: any) {
    if (obj) {

      this.id = obj.id;
      this.staffId = obj.staffId;
      this.centerId = obj.centerId;
      this.donorId = obj.donorId;
      this.duration = obj.duration;
      this.date = obj.date;
      this.status = obj.status;
    }


  }
}
