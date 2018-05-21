import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router'; 
import { TranslateModule } from '@ngx-translate/core'; 
import { FuseSharedModule } from '@fuse/shared.module'; 
import { FuseSampleComponent } from './sample.component';
import { AdminAuthGuard } from '../../../core/guards/admin.auth.guard';

const routes = [
    {
        path: 'sample',
        component: FuseSampleComponent,
        canActivate: [AdminAuthGuard]
    }
];

@NgModule({
    declarations: [
        FuseSampleComponent
    ],
    imports: [
        RouterModule.forChild(routes), 
        TranslateModule, 
        FuseSharedModule
    ],
    exports: [
        FuseSampleComponent
    ],
    providers: [
        AdminAuthGuard
    ]
})

export class FuseSampleModule {
}
