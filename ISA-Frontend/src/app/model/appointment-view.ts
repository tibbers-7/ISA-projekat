import { SafeUrl } from "@angular/platform-browser";

export class AppointmentView {
  id: number = 0;
  startDate: string = '';
  duration: number = 0;
  status: string = '';
  centerName = ''
  staffFullName: string = '';
  
  public constructor(obj?: any) {
    if (obj) {

      this.id = obj.id;
      
      this.duration = obj.duration;
      this.startDate = obj.startDate;
      this.centerName = obj.centerName;
      this.staffFullName = obj.staffFullName;
    }


  }
}
