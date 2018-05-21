import { MatChipInputEvent } from '@angular/material';

import { FuseUtils } from '@fuse/utils';

export class Product {
	productId: number;
	identityId: string;
	addressId: string;
	statusId: string;
	nomeFantasia: string;
	razaoSocial: string;
	cnpj: string;
	ie: string;
	logo: string;
	urlAmigavel: string;
	email: string;
	identity: {
		firstName: string,
		email: string,
		lastName: string,
		facebookId: string,
		pictureUrl: string
	};
	address: {
		addressId: number,
		zipCode: string,
		number: string,
		street: string,
		neighborhood: string,
		city: string,
		state: string,
		country: string,
		complement: string
	};
	status: {
		id: number,
		name: string,
		description: string,
		internalDescription: string,
		blocked: boolean
	};
	bankData: {
		bankDataId: number,
		bankId: number,
		agency: string,
		digitAgency: string,
		checkingAccount: string,
		digitCheckingAccount: string
	};

	constructor(product?) {
		product = product || {};
		this.productId = product.productId || FuseUtils.generateGUID();
		this.identityId = product.identityId || '';
		this.addressId = product.addressId || '';
		this.statusId = product.statusId || '';
		this.nomeFantasia = product.nomeFantasia || '';
		this.razaoSocial = product.razaoSocial || '';
		this.cnpj = product.cnpj || '';
		this.ie = product.ie || '';
		this.logo = product.logo || '';
		this.urlAmigavel = product.urlAmigavel || FuseUtils.handleize(this.razaoSocial);
		this.email = product.email || '';
		this.identity = product.identity || {};
		this.address = product.address || {};
		this.status = product.tags || {};
		this.bankData = product.bankData || {};
	}

}
