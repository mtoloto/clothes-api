import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class ProductsListService implements Resolve<any>
{
	products: any[];
	onProductsChanged: BehaviorSubject<any> = new BehaviorSubject({});

	constructor(
		private http: HttpClient
	) {
	}

    /**
     * Resolve
     * @param {ActivatedRouteSnapshot} route
     * @param {RouterStateSnapshot} state
     * @returns {Observable<any> | Promise<any> | any}
     */
	resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any { 
		return new Promise((resolve, reject) => { 
			Promise.all([
				this.getProducts()
			]).then(
				() => {
					resolve();
				},
				reject
				);
		});
	}

	private getProducts(): Promise<any> {
		return new Promise((resolve, reject) => {
			let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") }); 
			this.http.get('api/administrator/products', { headers: headers })
				.subscribe((response: any) => { 
					this.products = response.products;
					this.onProductsChanged.next(this.products);
					resolve(response);
				}, reject);
		});
	}
}
