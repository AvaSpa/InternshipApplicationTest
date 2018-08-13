import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Applicant } from '../shared/applicant.type';
import { ApplicantInternship } from '../shared/applicant-internship.type';
import { Test } from '../shared/test.type';
import { Internship } from '../shared/internship.type';
import { Http, Response } from '@angular/http';
import { DataService } from '../shared/data.service';
import { TestQuestion } from '../shared/test-question.type';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    test: Test;

    baseUrl = 'http://localhost:52044/api';
    applicant = new Applicant(0, '', '', '');
    internships = new Array<Internship>();
    selectedInternshipId = 1;

    constructor(private router: Router,
        private http: Http,
        private data: DataService
    ) {
        this.test = new Test(0, 10, new Array<TestQuestion>());
    }

    get diagnostic() { return ''; /*return JSON.stringify(this.internships);*/ }

    ngOnInit(): void {
        //this.data.currentTest.subscribe(test => this.test = test);

        this.http.get(this.baseUrl + '/Internship').subscribe((res: Response) => {
            this.internships = res.json();
        });
    }

    apply() {
        this.http.post(this.baseUrl + '/Applicant', this.applicant).subscribe((res: Response) => {
            if (res.status == 200) {
                this.applicant.id = res.json();
                this.http.get(this.baseUrl + `/Test?applicantId=${this.applicant.id}&internshipId=${this.selectedInternshipId}`).subscribe((res: Response) => {
                    this.test = res.json();
                    this.data.changeTest(this.test);
                }, () => { }, () => {
                    if (this.test.applicantInternshipId > 0) {
                        this.router.navigate(["take-test"]);
                    } else {
                        alert(`Cannot apply again to this internship.
Please choose another internship!`);
                    }
                });
            }
        });
    }
}
