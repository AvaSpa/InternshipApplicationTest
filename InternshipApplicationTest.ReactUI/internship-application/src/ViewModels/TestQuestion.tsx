export class TestQuestion {
  public id: number;
  public statement: string;
  public correctAnswerId: number;

  constructor(id: number, statement: string, correctAnswerId: number) {
    this.id = id;
    this.statement = statement;
    this.correctAnswerId = correctAnswerId;
  }
}
