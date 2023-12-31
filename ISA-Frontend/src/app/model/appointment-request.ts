import { SafeUrl } from "@angular/platform-browser";

export class AppointmentRequest {
  id: number = 0;
  staffId: number = 0;
  donorId: number = 0;
  centerId: number = 0;
  startDate: string = '';
  duration: number = 0;
  
  public constructor(obj?: any) {
    if (obj) {

      this.id = obj.id;
      this.staffId = obj.staffId;
      this.centerId = obj.centerId;
      this.donorId = obj.donorId;
      this.duration = obj.duration;
      this.startDate = obj.startDate;
    }


  }
}
