export class RegDTO {
    email: string = '';
    password:string='';
    name:string='';
    surname:string='';
    address:string='';
    gender:string='';
    jmbg:string='';
    workplace:string='';
    employmentInfo:string='';
    city:string='';
    state:string='';
    phoneNum:string='';
  
    public constructor(obj?: any) {
      if (obj) {
        this.email = obj.email;
        this.password = obj.password;

        this.name = obj.name;
        this.surname=obj.surname;

        this.address = obj.adress;
        this.city=obj.city;
        this.state=obj.state;

        this.gender = obj.gender;
        this.jmbg = obj.jmbg;

        this.workplace=obj.workplace;
        this.employmentInfo=obj.employmentInfo;

      }
    }
  }
  