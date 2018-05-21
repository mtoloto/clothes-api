import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import 'hammerjs'; 
import { FuseModule } from '@fuse/fuse.module'; 
import { FuseSharedModule } from '@fuse/shared.module'; 
import { fuseConfig } from './fuse-config'; 
import { AppComponent } from './app.component';
import { FuseMainModule } from './main/main.module';
import { FuseSampleModule } from './main/content/sample/sample.module';
import { AdminAuthService } from './core/services/admin.auth.service';
import { LaundryAuthService } from './core/services/laundry.auth.service';
import { HttpModule } from '@angular/http';

const appRoutes: Routes = [
    {
        path: 'admin',
        loadChildren: './main/content/admin/admin.module#AdminModule'
    },
    {
        path: 'lavanderia',
        loadChildren: './main/content/laundry/laundry.module#LaundryModule'
    },
    {
        path: '**',
        redirectTo: 'lavanderia/login'
    }
];

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        HttpModule,
        RouterModule.forRoot(appRoutes),
        TranslateModule.forRoot(),
        // Fuse Main and Shared modules
		FuseModule.forRoot(fuseConfig),  
        FuseSharedModule,
        FuseMainModule,
        FuseSampleModule
    ],
    bootstrap: [
        AppComponent
    ],
    providers: [
        AdminAuthService,
        LaundryAuthService
    ]
})
export class AppModule {
}
