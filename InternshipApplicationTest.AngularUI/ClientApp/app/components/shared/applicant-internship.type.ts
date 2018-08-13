export class ApplicantInternship {
    id: number;
    score: number;
    applicantPassedTheTest: boolean;
    dateTestTaken: Date;
    applicantId: number;
    internshipId: number;

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