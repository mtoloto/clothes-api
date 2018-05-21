import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class AdminLaundryService implements Resolve<any>
{
	routeParams: any;
	laundry: any;
	status: any;
	banks: any;
	onLaundryChanged: BehaviorSubject<any> = new BehaviorSubject({});

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
				this.getLaundry(),
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

	getLaundry(): Promise<any> {
		return new Promise((resolve, reject) => {
			if (this.routeParams.id === 'new') {
				this.onLaundryChanged.next(false);
				resolve(false);
			}
			else {
				let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
				this.http.get('api/administrator/laundries/' + this.routeParams.id, { headers: headers })
					.subscribe((response: any) => {
						this.laundry = response.laundries;
						this.onLaundryChanged.next(this.laundry);
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

	saveLaundry(laundry) {
		let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
		return new Promise((resolve, reject) => {
			this.http.put('api/administrator/laundries/' + laundry.laundryId, laundry, { headers: headers })
				.subscribe((response: any) => {
					resolve(response);
				}, reject);
		});
	}

	addLaundry(laundry) {
		let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
		return new Promise((resolve, reject) => {
			this.http.post('api/administrator/laundries/', laundry, { headers: headers })
				.subscribe((response: any) => {
					resolve(response);
				}, reject);
		});
	}

	removeLaundry(id) {
		let headers = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem("auth_token") });
		return new Promise((resolve, reject) => {
			this.http.delete('api/administrator/laundries/' + id, { headers: headers })
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
			this.http.post('api/administrator/laundriesutil/updatelogo/' + id, data, { headers: headers })
				.subscribe((response: any) => {
					resolve(response);
				}, reject);
		});
	}
}
