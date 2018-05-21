import { Component, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar, MatDialogRef, MatDialog } from '@angular/material';
import { FuseConfirmDialogComponent } from '@fuse/components/confirm-dialog/confirm-dialog.component';

import 'rxjs/add/operator/startWith';
import 'rxjs/add/observable/merge';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/observable/fromEvent';
import { Subscription } from 'rxjs/Subscription';

import { fuseAnimations } from '@fuse/animations';
import { FuseUtils } from '@fuse/utils'; 2

import { Laundry } from './laundry.model';
import { AdminLaundryService } from './laundry.service';
import { Router } from '@angular/router';
import { EventValidationErrorMessage } from 'calendar-utils';
import { AbstractControl } from '@angular/forms/src/model';

@Component({
	selector: 'admin-laundry',
	templateUrl: './laundry.component.html',
	styleUrls: ['./laundry.component.scss'],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class LaundryComponent implements OnInit, OnDestroy {
	laundry = new Laundry();
	allStatus: any;
	allBanks: any;
	onLaundryChanged: Subscription;
	pageType: string;
	laundryForm: FormGroup;
	inProcess: boolean = false;
	confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
	errorsSubmit: any = [];
	url: string;


	constructor(
		private laundryService: AdminLaundryService,
		private formBuilder: FormBuilder,
		public snackBar: MatSnackBar,
		private router: Router,
		private dialog: MatDialog
	) {
	}

	ngOnInit() {
		this.allStatus = this.laundryService.status;
		this.allBanks = this.laundryService.banks;

		// Subscribe to update laundry on changes
		this.onLaundryChanged =
			this.laundryService.onLaundryChanged
				.subscribe(laundry => {
					if (laundry) {
						this.laundry = new Laundry(laundry);
						this.pageType = 'edit';
					}
					else {
						this.pageType = 'new';
						this.laundry = new Laundry();
					}
					this.laundryForm = this.createLaundryForm();
				});
	}

	ngOnDestroy() {
		this.onLaundryChanged.unsubscribe();
	}



	createLaundryForm() {
		if (this.pageType === 'new') {
			return this.formBuilder.group({
				laundryId: [this.laundry.laundryId],
				nomeFantasia: [this.laundry.nomeFantasia],
				urlAmigavel: [this.laundry.urlAmigavel],
				razaoSocial: [this.laundry.razaoSocial],
				cnpj: [this.laundry.cnpj],
				ie: [this.laundry.ie],
				logo: [this.laundry.logo],
				email: [this.laundry.identity.email, Validators.compose([Validators.email, Validators.required])],
				emailConfirm: [ '', Validators.compose([Validators.email, Validators.required, confirmPassword])],
				statusId: [this.laundry.statusId],
				address: this.formBuilder.group({
					addressId: [this.laundry.address.addressId],
					zipCode: [this.laundry.address.zipCode],
					number: [this.laundry.address.number],
					street: [this.laundry.address.street],
					neighborhood: [this.laundry.address.neighborhood],
					city: [this.laundry.address.city],
					state: [this.laundry.address.state],
					country: [this.laundry.address.country],
					complement: [this.laundry.address.complement]
				}),
				bankData: this.formBuilder.group({
					bankDataId: [this.laundry.bankData.bankDataId],
					bankId: [this.laundry.bankData.bankId],
					agency: [this.laundry.bankData.agency],
					digitAgency: [this.laundry.bankData.digitAgency],
					checkingAccount: [this.laundry.bankData.checkingAccount],
					digitCheckingAccount: [this.laundry.bankData.digitCheckingAccount]
				}),
			});
		}
		else {
			return this.formBuilder.group({
				laundryId: [this.laundry.laundryId],
				nomeFantasia: [this.laundry.nomeFantasia],
				urlAmigavel: [this.laundry.urlAmigavel],
				razaoSocial: [this.laundry.razaoSocial],
				cnpj: [this.laundry.cnpj],
				ie: [this.laundry.ie],
				logo: [this.laundry.logo],
				email: [this.laundry.identity.email, Validators.compose([Validators.email, Validators.required])], 
				statusId: [this.laundry.statusId],
				address: this.formBuilder.group({
					addressId: [this.laundry.address.addressId],
					zipCode: [this.laundry.address.zipCode],
					number: [this.laundry.address.number],
					street: [this.laundry.address.street],
					neighborhood: [this.laundry.address.neighborhood],
					city: [this.laundry.address.city],
					state: [this.laundry.address.state],
					country: [this.laundry.address.country],
					complement: [this.laundry.address.complement]
				}),
				bankData: this.formBuilder.group({
					bankDataId: [this.laundry.bankData.bankDataId],
					bankId: [this.laundry.bankData.bankId],
					agency: [this.laundry.bankData.agency],
					digitAgency: [this.laundry.bankData.digitAgency],
					checkingAccount: [this.laundry.bankData.checkingAccount],
					digitCheckingAccount: [this.laundry.bankData.digitCheckingAccount]
				}),
			});
		}
		
	}

	saveLaundry() {
		this.inProcess = true;
		const data = this.laundryForm.getRawValue();
		data.urlAmigavel = FuseUtils.handleize(data.nomeFantasia);
		this.laundryService.saveLaundry(data)
			.then((res) => {

				// Trigger the subscription with new data
				this.laundryService.onLaundryChanged.next(res);

				// Show the success message
				this.snackBar.open('Lavanderia salva', 'OK', {
					verticalPosition: 'top',
					duration: 2000
				});
				this.inProcess = false;

			},
			(err: any) => {
				console.log("Erros aconteceram", err.error);
				this.errorsSubmit = err.error;
				this.inProcess = false;
			});
	}

	addLaundry() {
		this.inProcess = true;
		const data = this.laundryForm.getRawValue();
		data.urlAmigavel = FuseUtils.handleize(data.nomeFantasia);

		data.address.addressId = 0;
		data.bankData.bankDataId = 0;
		data.bankDataId = 0;
		data.logo = 'product-image-placeholder.png';

		this.laundryService.addLaundry(data)
			.then((res: any) => {
				this.router.navigateByUrl('/admin/lavanderias/' + res.laundryId);
				// Show the success message
				this.snackBar.open('Lavanderia salva', 'OK', {
					verticalPosition: 'top',
					duration: 2000
				});
				this.inProcess = false;

			},
			(err: any) => {
				console.log("Erros aconteceram", err);
				this.inProcess = false;
			}).catch((err: any) => {
				console.log("Erros aconteceram 2", err);
				this.inProcess = false;
			});
	}

	removeLaundry() {

		this.confirmDialogRef = this.dialog.open(FuseConfirmDialogComponent, {
			disableClose: false
		});

		this.confirmDialogRef.componentInstance.confirmMessage = 'Tem certeza que deseja remover?';

		this.confirmDialogRef.afterClosed().subscribe(result => {
			if (result) {
				this.inProcess = true;
				const data = this.laundryForm.getRawValue();
				data.urlAmigavel = FuseUtils.handleize(data.nomeFantasia);

				this.laundryService.removeLaundry(data.laundryId)
					.then(() => {

						// Trigger the subscription with new data
						//this.laundryService.onLaundryChanged.next(data);

						this.router.navigateByUrl('/admin/lavanderias');
						// Show the success message
						this.snackBar.open('Lavanderia excluída', 'OK', {
							verticalPosition: 'top',
							duration: 2000
						});
						this.inProcess = false;
					});
			}
			this.confirmDialogRef = null;
		});

	}

	fileChange(files: FileList) {
		if (files == null)
			return;

		if (files[0] == null)
			return;

		if (files && files[0].size > 0) {
			var reader = new FileReader();
			reader.onloadend = (event: any) => {
				this.url = event.target.result;
				this.laundryForm.patchValue({
					logo: files[0]
				});

				this.confirmDialogRef = this.dialog.open(FuseConfirmDialogComponent, {
					disableClose: false
				});

				this.confirmDialogRef.componentInstance.confirmMessage = 'Tem certeza que deseja alterar o logo por este:';
				this.confirmDialogRef.componentInstance.innerHtml = '<div class="div-img-laundry-confirm-logo-change"><img class="img-laundry-confirm-logo-change" src ="' + this.url + '"></div>';
				this.confirmDialogRef.componentInstance.confirmButtonText = "Sim";
				this.confirmDialogRef.componentInstance.cancelButtonText = "Não";

				this.confirmDialogRef.afterClosed().subscribe(result => {
					if (result) {
						var formDataNew = new FormData();
						formDataNew.append("logo", files[0]);
						this.laundryService.updateLogo(formDataNew, this.laundry.laundryId).then((res: any) => {
							this.laundry.logo = res.logo;
						});
					}
					this.confirmDialogRef = null;
				}, rej => { 

					this.laundryForm.patchValue({
						logo: null
					});
				});
			}

			reader.readAsDataURL(files[0]);


		}
	}

	loadZipCode() {
		const data = this.laundryForm.getRawValue();
		var zipCode = data.address.zipCode;
		this.laundryService.loadZipCode(zipCode).then((res: any) => {
			console.log(res);
			if (res.erro)
				return;

			this.laundryForm.patchValue({
				laundryId: 0,
				address: {
					street: res.logradouro,
					neighborhood: res.bairro,
					city: res.localidade,
					state: res.uf,
					country: 'Brasil'
				}
			});

		});

	}

}

function confirmPassword(control: AbstractControl) {
	if (!control.parent || !control) {
		return;
	}

	const password = control.parent.get('email');
	const passwordConfirm = control.parent.get('emailConfirm');

	if (!password || !passwordConfirm) {
		return;
	}

	if (passwordConfirm.value === '') {
		return;
	}

	if (password.value !== passwordConfirm.value) {
		return {
			passwordsNotMatch: true
		};
	}
}

