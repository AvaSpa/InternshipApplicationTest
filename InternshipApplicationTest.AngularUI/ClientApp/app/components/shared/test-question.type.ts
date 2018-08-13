export class TestQuestion {
    id: number;
    statement: string;
    correctAnswerId: number;

    constructor(
        id: number,
        statement: string,
        correctAnswerId: number
    ) {
        this.id = id;
        this.statement = statement;
        this.correctAnswerId = correctAnswerId;
    }
}