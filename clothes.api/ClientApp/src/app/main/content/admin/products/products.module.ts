import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AdminAuthGuard } from 'app/core/guards/admin.auth.guard';
import { ProductsListComponent } from 'app/main/content/admin/products/list/list.component';
import { MatButtonModule, MatChipsModule, MatFormFieldModule, MatIconModule, MatInputModule, MatPaginatorModule, MatRippleModule, MatSelectModule, MatSortModule, MatTableModule, MatTabsModule, MatSnackBarModule, MatDialogModule } from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';
import { FuseSharedModule } from '@fuse/shared.module';
import { FuseWidgetModule } from '@fuse/components';
import { ProductsListService } from 'app/main/content/admin/products/list/list.service';
import { ProductComponent } from 'app/main/content/admin/products/product/product.component';
import { AdminProductService } from 'app/main/content/admin/products/product/product.service';
import { NgxMaskModule } from 'ngx-mask'
import { FuseConfirmDialogModule } from '@fuse/components';

const routes = [
	{
		path: 'produtos',
		component: ProductsListComponent,
		resolve: {
			data: ProductsListService
		},
		canActivate: [AdminAuthGuard]
	},
	{
		path: 'produtos/:id',
		component: ProductComponent,
		resolve: {
			data: AdminProductService
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
		ProductsListComponent,
		ProductComponent 
	],
	providers: [  
		ProductsListService,
		AdminProductService
	]
})
export class ProductsModule {
}
