export class User {
    email: string = '';
    password:string='';
    name:string='';
    adress:string='';
    phoneNumber:string='';
    gender:string='';
    jmbg:string='';
    workplace:string='';


  
    public constructor(obj?: any) {
      if (obj) {
        this.email = obj.email;
        this.password = obj.password;

        this.name = obj.name;
        this.adress = obj.adress;
        this.phoneNumber = obj.phoneNumber;
        this.gender = obj.gender;
        this.jmbg = obj.jmbg;
        this.workplace = obj.workplace;

      }
    }
  }
  