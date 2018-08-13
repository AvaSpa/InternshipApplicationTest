export class ApplicantInternship {
  public id: number;
  public score: number;
  public applicantPassedTheTest: boolean;
  public dateTestTaken: Date;
  public applicantId: number;
  public internshipId: number;

  constructor(
    id: number,
    score: number,
    applicantPassedTheTest: boolean,
    dateTestTaken: Date,
    applicantId: number,
    internshipId: number
  ) {
    this.id = id;
    this.score = score;
    this.applicantPassedTheTest = applicantPassedTheTest;
    this.dateTestTaken = dateTestTaken;
    this.applicantId = applicantId;
    this.internshipId = internshipId;
  }
}
