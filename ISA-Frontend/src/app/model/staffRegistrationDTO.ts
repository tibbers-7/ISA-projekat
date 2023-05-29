export class StaffRegistrationDTO {
    email = ''
    password = ''
    name = ''
    surname = ''
    gender = ''
    JMBG = 0
    address = ''
    city = ''
    state = ''
    phoneNumber = ''
    idOfCenter = 0
  
    public constructor(obj?: any) {
      if (obj) {
        this.email = obj.email
        this.password = obj.password
        this.name = obj.name
        this.surname = obj.surname
        this.address = obj.address
        this.city = obj.city
        this.state = obj.state
        this.gender = obj.gender
        this.JMBG = obj.JMBG
        this.phoneNumber = obj.phoneNumber
        this.idOfCenter = obj.idOfCenter

      }
    }
  }
  