export class TestAnswer {
  public id: number;
  public content: string;
  public questionId: number;

  constructor(id: number, content: string, questionId: number) {
    this.id = id;
    this.content = content;
    this.questionId = questionId;
  }
}
