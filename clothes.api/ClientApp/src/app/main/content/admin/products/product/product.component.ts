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

import { Product } from './product.model';
import { AdminProductService } from './product.service';
import { Router } from '@angular/router';
import { EventValidationErrorMessage } from 'calendar-utils';
import { AbstractControl } from '@angular/forms/src/model';

@Component({
	selector: 'admin-product',
	templateUrl: './product.component.html',
	styleUrls: ['./product.component.scss'],
	encapsulation: ViewEncapsulation.None,
	animations: fuseAnimations
})
export class ProductComponent implements OnInit, OnDestroy {
	product = new Product();
	allStatus: any;
	allBanks: any;
	onProductChanged: Subscription;
	pageType: string;
	productForm: FormGroup;
	inProcess: boolean = false;
	confirmDialogRef: MatDialogRef<FuseConfirmDialogComponent>;
	errorsSubmit: any = [];
	url: string;


	constructor(
		private productService: AdminProductService,
		private formBuilder: FormBuilder,
		public snackBar: MatSnackBar,
		private router: Router,
		private dialog: MatDialog
	) {
	}

	ngOnInit() {
		this.allStatus = this.productService.status;
		this.allBanks = this.productService.banks;

		// Subscribe to update product on changes
		this.onProductChanged =
			this.productService.onProductChanged
				.subscribe(product => {
					if (product) {
						this.product = new Product(product);
						this.pageType = 'edit';
					}
					else {
						this.pageType = 'new';
						this.product = new Product();
					}
					this.productForm = this.createProductForm();
				});
	}

	ngOnDestroy() {
		this.onProductChanged.unsubscribe();
	}



	createProductForm() {
		if (this.pageType === 'new') {
			return this.formBuilder.group({
				productId: [this.product.productId],
				nomeFantasia: [this.product.nomeFantasia],
				urlAmigavel: [this.product.urlAmigavel],
				razaoSocial: [this.product.razaoSocial],
				cnpj: [this.product.cnpj],
				ie: [this.product.ie],
				logo: [this.product.logo],
				email: [this.product.identity.email, Validators.compose([Validators.email, Validators.required])],
				emailConfirm: [ '', Validators.compose([Validators.email, Validators.required, confirmPassword])],
				statusId: [this.product.statusId],
				address: this.formBuilder.group({
					addressId: [this.product.address.addressId],
					zipCode: [this.product.address.zipCode],
					number: [this.product.address.number],
					street: [this.product.address.street],
					neighborhood: [this.product.address.neighborhood],
					city: [this.product.address.city],
					state: [this.product.address.state],
					country: [this.product.address.country],
					complement: [this.product.address.complement]
				}),
				bankData: this.formBuilder.group({
					bankDataId: [this.product.bankData.bankDataId],
					bankId: [this.product.bankData.bankId],
					agency: [this.product.bankData.agency],
					digitAgency: [this.product.bankData.digitAgency],
					checkingAccount: [this.product.bankData.checkingAccount],
					digitCheckingAccount: [this.product.bankData.digitCheckingAccount]
				}),
			});
		}
		else {
			return this.formBuilder.group({
				productId: [this.product.productId],
				nomeFantasia: [this.product.nomeFantasia],
				urlAmigavel: [this.product.urlAmigavel],
				razaoSocial: [this.product.razaoSocial],
				cnpj: [this.product.cnpj],
				ie: [this.product.ie],
				logo: [this.product.logo],
				email: [this.product.identity.email, Validators.compose([Validators.email, Validators.required])], 
				statusId: [this.product.statusId],
				address: this.formBuilder.group({
					addressId: [this.product.address.addressId],
					zipCode: [this.product.address.zipCode],
					number: [this.product.address.number],
					street: [this.product.address.street],
					neighborhood: [this.product.address.neighborhood],
					city: [this.product.address.city],
					state: [this.product.address.state],
					country: [this.product.address.country],
					complement: [this.product.address.complement]
				}),
				bankData: this.formBuilder.group({
					bankDataId: [this.product.bankData.bankDataId],
					bankId: [this.product.bankData.bankId],
					agency: [this.product.bankData.agency],
					digitAgency: [this.product.bankData.digitAgency],
					checkingAccount: [this.product.bankData.checkingAccount],
					digitCheckingAccount: [this.product.bankData.digitCheckingAccount]
				}),
			});
		}
		
	}

	saveProduct() {
		this.inProcess = true;
		const data = this.productForm.getRawValue();
		data.urlAmigavel = FuseUtils.handleize(data.nomeFantasia);
		this.productService.saveProduct(data)
			.then((res) => {

				// Trigger the subscription with new data
				this.productService.onProductChanged.next(res);

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

	addProduct() {
		this.inProcess = true;
		const data = this.productForm.getRawValue();
		data.urlAmigavel = FuseUtils.handleize(data.nomeFantasia);

		data.address.addressId = 0;
		data.bankData.bankDataId = 0;
		data.bankDataId = 0;
		data.logo = 'product-image-placeholder.png';

		this.productService.addProduct(data)
			.then((res: any) => {
				this.router.navigateByUrl('/admin/lavanderias/' + res.productId);
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

	removeProduct() {

		this.confirmDialogRef = this.dialog.open(FuseConfirmDialogComponent, {
			disableClose: false
		});

		this.confirmDialogRef.componentInstance.confirmMessage = 'Tem certeza que deseja remover?';

		this.confirmDialogRef.afterClosed().subscribe(result => {
			if (result) {
				this.inProcess = true;
				const data = this.productForm.getRawValue();
				data.urlAmigavel = FuseUtils.handleize(data.nomeFantasia);

				this.productService.removeProduct(data.productId)
					.then(() => {

						// Trigger the subscription with new data
						//this.productService.onProductChanged.next(data);

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
				this.productForm.patchValue({
					logo: files[0]
				});

				this.confirmDialogRef = this.dialog.open(FuseConfirmDialogComponent, {
					disableClose: false
				});

				this.confirmDialogRef.componentInstance.confirmMessage = 'Tem certeza que deseja alterar o logo por este:';
				this.confirmDialogRef.componentInstance.innerHtml = '<div class="div-img-product-confirm-logo-change"><img class="img-product-confirm-logo-change" src ="' + this.url + '"></div>';
				this.confirmDialogRef.componentInstance.confirmButtonText = "Sim";
				this.confirmDialogRef.componentInstance.cancelButtonText = "Não";

				this.confirmDialogRef.afterClosed().subscribe(result => {
					if (result) {
						var formDataNew = new FormData();
						formDataNew.append("logo", files[0]);
						this.productService.updateLogo(formDataNew, this.product.productId).then((res: any) => {
							this.product.logo = res.logo;
						});
					}
					this.confirmDialogRef = null;
				}, rej => { 

					this.productForm.patchValue({
						logo: null
					});
				});
			}

			reader.readAsDataURL(files[0]);


		}
	}

	loadZipCode() {
		const data = this.productForm.getRawValue();
		var zipCode = data.address.zipCode;
		this.productService.loadZipCode(zipCode).then((res: any) => {
			console.log(res);
			if (res.erro)
				return;

			this.productForm.patchValue({
				productId: 0,
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

