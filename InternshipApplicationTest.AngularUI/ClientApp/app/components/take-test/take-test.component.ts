import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { timer } from 'rxjs/observable/timer';
import { DataService } from '../shared/data.service';
import { Test } from '../shared/test.type';
import { Http, Response } from '@angular/http';
import { TestAnswer } from '../shared/test-answer.type';
import { TestQuestion } from '../shared/test-question.type';
import { ApplicantInternship } from '../shared/applicant-internship.type';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'take-test',
    templateUrl: './take-test.component.html',
    styleUrls: ['./take-test.component.css']
})
export class TakeTestComponent implements OnInit {
    test: Test;

    currentQuestionAnswers: TestAnswer[] | undefined;

    baseUrl = 'http://localhost:52044/api';
    answers = new Array<TestAnswer>();
    score = 0;
    currentQuestion = new TestQuestion(0, '', 0);
    selectedAnswerId = 0;
    currentTime = '';
    secondsLeft = 0;
    subscription = new Subscription();

    // keeping for further refference
    //private sub: any;

    constructor(private router: Router,
        private http: Http,
        private data: DataService
    ) {
        this.test = new Test(0, 10, new Array<TestQuestion>());
    }

    get diagnostic() {
        return this.score;// JSON.stringify(this.currentQuestion) + ': ' + JSON.stringify(this.currentQuestionAnswers);
    }

    foundInTestQuestions(questionId: number): boolean {
        for (var i = 0; i < this.test.questions.length; i++) {
            if (this.test.questions[i].id == questionId) {
                return true;
            }
        }
        return false;
    }

    getAnswersForTestQuestions(answers: Array<TestAnswer>): Array<TestAnswer> {
        let result = new Array<TestAnswer>();

        for (var i = 0; i < answers.length; i++) {
            if (this.foundInTestQuestions(answers[i].questionId)) {
                result.push(answers[i]);
            }
        }

        return result;
    }

    getAnswersForQuestion(answers: Array<TestAnswer>, questionId: number): TestAnswer[] {
        let result = new Array<TestAnswer>();

        for (var i = 0; i < answers.length; i++) {
            if (answers[i].questionId == questionId) {
                result.push(answers[i]);
            }
        }

        return result;
    }

    twoDigitNumber(n: number): string {
        return n > 9 ? "" + n : "0" + n;
    }

    displayTime(currentSecond: number) {
        this.secondsLeft = this.test.timeLimitInMinutes * 60 - currentSecond;
        if (this.secondsLeft > 0) {
            let minute = Math.floor(this.secondsLeft / 60);
            let second = this.secondsLeft % 60;
            this.currentTime = `Time left: 00:${this.twoDigitNumber(minute)}:${this.twoDigitNumber(second)}`;
        } else {
            this.subscription.unsubscribe();
            this.endTest();
        }
    }

    ngOnInit() {
        this.data.currentTest.subscribe((test) => {
            this.test = test;
        });

        this.currentQuestion = this.test.questions[0];
        this.secondsLeft = this.test.timeLimitInMinutes * 60;

        const source = timer(1000, 1000);
        this.subscription = source.subscribe(val => this.displayTime(val));

        this.http.get(this.baseUrl + '/TestAnswer').subscribe((res: Response) => {
            this.answers = res.json();
        }, () => { }, () => {
            this.answers = this.getAnswersForTestQuestions(this.answers);
            this.currentQuestionAnswers = this.getAnswersForQuestion(this.answers, this.currentQuestion.id);
        });


        // keeping for further refference
        //this.sub = this.route.params.subscribe(params => {
        //    this.applicantId = params['applicantId'];
        //});
    }

    onSelectionChange(answer: TestAnswer) {
        this.selectedAnswerId = answer.id;
    }

    answerQuestion() {
        if (this.selectedAnswerId == this.currentQuestion.correctAnswerId) {
            this.score++;
        }
        this.selectedAnswerId = 0;
        this.test.questions.shift();
        if (this.test.questions.length > 0) {
            this.updateCurrenctQuestion();
        } else {
            this.endTest();
        }
    }

    nextQuestion() {
        let question = this.test.questions.shift();
        this.test.questions.push(question ? question : new TestQuestion(0, '', 0));
        this.updateCurrenctQuestion();
    }

    updateCurrenctQuestion() {
        this.currentQuestion = this.test.questions[0];
        this.currentQuestionAnswers = this.getAnswersForQuestion(this.answers, this.currentQuestion.id);
    }

    endTest() {
        let applicantInternship = new ApplicantInternship(0, this.score, false, new Date(), 0, 0);
        this.http.post(this.baseUrl + '/Test', applicantInternship).subscribe(
            (res: Response) => {
                let passed = false;
                if (res.status == 202) {
                    alert(`Congratulations! You passed!
Your score: ${this.score}
We will transmit to you the date and time of the selection test.`);
                }
                this.router.navigate(["home"]);
            }, (error: Response) => {
                if (error.status == 406) {
                    alert(`Sorry, you failed.
Try again next year or choose another internship!`);
                } else {
                    alert(error);
                }
                this.router.navigate(["home"]);
            }
        );
    }

    // keeping for further refference
    //ngOnDestroy() {
    //    this.sub.unsubscribe();
    //}
}
