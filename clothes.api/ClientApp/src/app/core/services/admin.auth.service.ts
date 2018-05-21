import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { LoginViewModel } from '../view-models/login.view.model.cs';
import { Configs } from '../configs/configs';
import { BaseService } from './base.service';

@Injectable()
export class AdminAuthService extends BaseService {

	private loggedIn: boolean;
	private api: string;

	constructor(
		private httpClient: HttpClient,
		private http: Http
	) {
		super();
		this.loggedIn = !!localStorage.getItem("auth_token");
		this.api = Configs.Api.Address
	}

	login(loginViewModel: LoginViewModel) {

		return new Promise((resolve, reject) => {
			let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
			this.httpClient.post(this.api + "/auth/login", loginViewModel, { headers: headers })
				.subscribe(
				res => { resolve(res); },
				rej => { reject(this.handleError) });
		});

	}

	isLoggedIn() {
		return this.loggedIn;
	}

	login2(credentials) {
		let headers = new Headers();
		headers.append('Content-Type', 'application/json');

		return this.http
			.post(
			this.api + '/auth/login',
			JSON.stringify(credentials), { headers }
			)
			.map(res => res.json())
			.map(res => {
				console.log(res.auth_token);
				localStorage.setItem('auth_token', res.auth_token);
				localStorage.setItem('userType', "administrator");
				this.loggedIn = true;
				return true;
			})
			.catch(this.handleError);
	}

	getToken() {
		if (this.loggedIn)
			return 'Bearer ' + localStorage.getItem('auth_token');
		else
			return null;
	}
}
