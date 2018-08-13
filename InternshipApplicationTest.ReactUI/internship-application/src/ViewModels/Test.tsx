import { TestQuestion } from "./TestQuestion";

export class Test {
  public applicantInternshipId: number;
  public timeLimitInMinutes: number;
  public questions: TestQuestion[];

  constructor(
    applicantInternshipId: number,
    timeLimitInMinutes: number,
    questions: TestQuestion[]
  ) {
    this.applicantInternshipId = applicantInternshipId;
    this.timeLimitInMinutes = timeLimitInMinutes;
    this.questions = questions;
  }
}
