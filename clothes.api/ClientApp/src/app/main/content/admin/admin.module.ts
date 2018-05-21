import { NgModule } from '@angular/core';
import { LoginModule } from './login/login.module';
import { HomeModule } from './home/home.module';
import { LaundriesModule } from 'app/main/content/admin/laundries/laundries.module';
import { DashboardModule } from 'app/main/content/admin/dashboard/dashboard.module';
import { DashboardComponent } from 'app/main/content/admin/dashboard/dashboard.component';
import { RouterModule } from '@angular/router'; 
import { ProductsModule } from 'app/main/content/admin/products/products.module';

const routes = [
	{
		path: '**',
		redirectTo: 'dashboard'
	}
];

@NgModule({
	imports: [
		LoginModule,
		HomeModule,
		LaundriesModule,
		ProductsModule,
		DashboardModule, 
		RouterModule.forChild(routes)
	]
})
export class AdminModule {
}
