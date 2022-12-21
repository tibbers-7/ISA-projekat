export class Staff {
  id: number = 0;
  email: string = '';
  password: string = '';
  centerId: number = 0;
  name: string = '';
  surname: string = ''; 

  public constructor(obj?: any) {
    if (obj) {
      this.id = obj.id;
      this.email = obj.email;
      this.password = obj.password;
      this.centerId = obj.centerId;
      this.name = obj.name;
      this.surname = obj.surname;
    }
  }
}
