import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class AdminProductService implements Resolve<any>
{
	routeParams: any;
	product: any;
	status: any;
	banks: any;
	onProductChanged: BehaviorSubject<any> = new BehaviorSubject({});

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
		this.routeParams = route.params;
		return new Promise((resolve, reject) => {

			Promise.all([
				this.getProduct(),
				this.getAllStatus(),
				this.getAllBanks()
			]).then(
				() => {
					resolve();
				},
				reject
				);
		});
	}

	getProduct(): Promise<any> {
		return new Promise((resolve, reject) => {
			if (this.routeParams.id === 'new') {
				this.onProductChanged.next(false);
				resolve(false);
			}
			else {
				let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
				this.http.get('api/administrator/products/' + this.routeParams.id, { headers: headers })
					.subscribe((response: any) => {
						this.product = response.products;
						this.onProductChanged.next(this.product);
						resolve(response);
					}, reject);
			}
		});
	}

	getAllStatus(): Promise<any> {
		return new Promise((resolve, reject) => {
			let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
			this.http.get('api/system/getAllStatus', { headers: headers })
				.subscribe((response: any) => {
					this.status = response;
					resolve(response);
				}, reject);
		});
	}

	getAllBanks(): Promise<any> {
		return new Promise((resolve, reject) => {
			let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
			this.http.get('api/system/getAllBanks', { headers: headers })
				.subscribe((response: any) => {
					this.banks = response;
					resolve(response);
				}, reject);
		});
	}

	saveProduct(product) {
		let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
		return new Promise((resolve, reject) => {
			this.http.put('api/administrator/products/' + product.productId, product, { headers: headers })
				.subscribe((response: any) => {
					resolve(response);
				}, reject);
		});
	}

	addProduct(product) {
		let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
		return new Promise((resolve, reject) => {
			this.http.post('api/administrator/products/', product, { headers: headers })
				.subscribe((response: any) => {
					resolve(response);
				}, reject);
		});
	}

	removeProduct(id) {
		let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
		return new Promise((resolve, reject) => {
			this.http.delete('api/administrator/products/' + id, { headers: headers })
				.subscribe((response: any) => {
					resolve(response);
				}, reject);
		}); 
	}
	
	loadZipCode(cep) { 
		return new Promise((resolve, reject) => {
			this.http.get('	https://viacep.com.br/ws/' + cep + '/json/')
				.subscribe((response: any) => {
					resolve(response);
				}, reject);
		});  
	}

	updateLogo(data: FormData, id) {
		let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
		return new Promise((resolve, reject) => {
			this.http.post('api/administrator/productsutil/updatelogo/' + id, data, { headers: headers })
				.subscribe((response: any) => {
					resolve(response);
				}, reject);
		});
	}
}
