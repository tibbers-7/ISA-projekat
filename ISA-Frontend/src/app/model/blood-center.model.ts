
export class BloodCenter {
  name: string = '';
  id: number = 0;
  address: string = '';
  description: string = ' ';
  openHours: string = ' ';
  avgScore: number = 0;
  
  

  public constructor(obj?: any) {
    if (obj) {
      this.id = obj.id;
      this.name = obj.name;
      this.openHours = obj.openHours;
      this.address = obj.addressString;
      this.description = obj.description;
      this.avgScore = obj.avgScore;
    }
  }
}
