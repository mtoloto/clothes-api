import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AdminAuthGuard } from 'app/core/guards/admin.auth.guard';
import { LaundriesListComponent } from 'app/main/content/admin/laundries/list/list.component';
import { MatButtonModule, MatChipsModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSortModule, MatTableModule, MatTabsModule, MatSnackBarModule, MatDialogModule } from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { LaundriesListService } from 'app/main/content/admin/laundries/list/list.service';
import { LaundryComponent } from 'app/main/content/admin/laundries/laundry/laundry.component';
import { AdminLaundryService } from 'app/main/content/admin/laundries/laundry/laundry.service';
import { NgxMaskModule } from 'ngx-mask'
import { FuseConfirmDialogModule } from '@fuse/components';

const routes = [
	{
		path: 'lavanderias',
		component: LaundriesListComponent,
		resolve: {
			data: LaundriesListService
		},
		canActivate: [AdminAuthGuard]
	},
	{
		path: 'lavanderias/:id',
		component: LaundryComponent,
		resolve: {
			data: AdminLaundryService
		},
		canActivate: [AdminAuthGuard]
	}	
];

@NgModule({
	imports: [
		RouterModule.forChild(routes),
		CdkTableModule,
		MatButtonModule,
		MatChipsModule,
		MatFormFieldModule,
		MatIconModule,
		MatInputModule,
		MatPaginatorModule,
		MatRippleModule,
		MatSelectModule,
		MatSortModule,
		MatTableModule,
		MatTabsModule,
		MatSnackBarModule,
		FuseSharedModule,
		FuseWidgetModule,
		MatDialogModule, 
		FuseConfirmDialogModule,
		NgxMaskModule.forRoot()
	],
	declarations: [
		LaundriesListComponent,
		LaundryComponent 
	],
	providers: [  
		LaundriesListService,
		AdminLaundryService
	]
})
export class LaundriesModule {
}
