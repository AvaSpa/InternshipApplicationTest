import { TestQuestion } from './test-question.type';

export class Test {
    applicantInternshipId: number;
    timeLimitInMinutes: number;
    questions: TestQuestion[];

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