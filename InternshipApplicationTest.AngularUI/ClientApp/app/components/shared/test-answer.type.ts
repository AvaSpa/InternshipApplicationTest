export class TestAnswer {
    id: number;
    content: string;
    questionId: number;

    constructor(
        id: number,
        content: string,
        questionId: number
    ) {
        this.id = id;
        this.content = content;
        this.questionId = questionId;
    }
}