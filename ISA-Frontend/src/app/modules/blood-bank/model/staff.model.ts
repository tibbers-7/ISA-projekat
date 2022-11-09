export class Staff {
  id: number = 0;
  email: string = '';
  password: string = '';
  name: string = '';
  idOfCenter: number = 0;

  public constructor(obj?: any) {
    if (obj) {
      this.id = obj.id;
      this.idOfCenter = obj.idOfCenter;
      this.email = obj.email;
      this.password = obj.password;
      this.name = obj.name;
      

    }
  }
}
