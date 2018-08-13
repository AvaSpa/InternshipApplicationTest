import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { HomeComponent } from './components/home/home.component';
import { TakeTestComponent } from './components/take-test/take-test.component';
import { DataService } from './components/shared/data.service';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        TakeTestComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'take-test', component: TakeTestComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
        DataService
    ]
})
export class AppModuleShared {
}
