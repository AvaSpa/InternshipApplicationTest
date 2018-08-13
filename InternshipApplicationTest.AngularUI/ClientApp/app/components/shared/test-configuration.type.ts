export class TestConfiguration {
    id: number;
    questionNumber: number;
    minimumScore: number;
    timeLimitInMinutes: number;

    constructor(
        id: number,
        questionNumber: number,
        minimumScore: number,
        timeLimitInMinutes: number
    ) {
        this.id = id;
        this.questionNumber = questionNumber;
        this.minimumScore = minimumScore;
        this.timeLimitInMinutes = timeLimitInMinutes;
    }
}