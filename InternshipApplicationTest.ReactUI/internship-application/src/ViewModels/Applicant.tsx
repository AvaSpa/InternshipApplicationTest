export class Applicant {
  public id: number;
  public firstName: string;
  public lastName: string;
  public phoneNumber: string;

  constructor(
    id: number,
    firstName: string,
    lastName: string,
    phoneNumber: string
  ) {
    this.id = id;
    this.firstName = firstName;
    this.lastName = lastName;
    this.phoneNumber = phoneNumber;
  }
}
