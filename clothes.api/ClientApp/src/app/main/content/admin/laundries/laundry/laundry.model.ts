import { MatChipInputEvent } from '@angular/material';

import { FuseUtils } from '@fuse/utils';

export class Laundry {
	laundryId: number;
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

	constructor(laundry?) {
		laundry = laundry || {};
		this.laundryId = laundry.laundryId || FuseUtils.generateGUID();
		this.identityId = laundry.identityId || '';
		this.addressId = laundry.addressId || '';
		this.statusId = laundry.statusId || '';
		this.nomeFantasia = laundry.nomeFantasia || '';
		this.razaoSocial = laundry.razaoSocial || '';
		this.cnpj = laundry.cnpj || '';
		this.ie = laundry.ie || '';
		this.logo = laundry.logo || '';
		this.urlAmigavel = laundry.urlAmigavel || FuseUtils.handleize(this.razaoSocial);
		this.email = laundry.email || '';
		this.identity = laundry.identity || {};
		this.address = laundry.address || {};
		this.status = laundry.tags || {};
		this.bankData = laundry.bankData || {};
	}

}
