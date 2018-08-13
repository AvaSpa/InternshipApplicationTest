import { Injectable } from "@angular/core";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Test } from "./test.type";
import { TestQuestion } from "./test-question.type";

@Injectable()
export class DataService {
    private testSource = new BehaviorSubject<Test>(new Test(0, 10, [new TestQuestion(0, '', 0)]));

    currentTest = this.testSource.asObservable();

    constructor() { }

    changeTest(test: Test) {
        this.testSource.next(test);
    }

}