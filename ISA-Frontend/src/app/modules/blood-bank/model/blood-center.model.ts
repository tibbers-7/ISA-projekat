import { Appointment } from "./appointment.model";
import { User } from "./user.model";

export class BloodCenter {
  name: string = '';
  id: number = 0;
  adress: string = '';
  description: string = ' ';
  avgScore: number = 0;
  appointments: Appointment[] = [];
  staff: User[] = [];


  public constructor(obj?: any) {
    if (obj) {
      this.id = obj.id;
      this.name = obj.name;
      this.adress = obj.adress;
      this.description = obj.description;
      this.avgScore = obj.avgScore;
    }
  }
}
