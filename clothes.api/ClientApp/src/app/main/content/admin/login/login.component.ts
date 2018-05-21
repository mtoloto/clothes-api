import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { AdminAuthService } from '../../../../core/services/admin.auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'fuse-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: fuseAnimations
})
export class FuseLoginComponent implements OnInit {
    loginForm: FormGroup;
    loginFormErrors: any;
    serverErrors: any;

    constructor(
        private fuseConfig: FuseConfigService,
        private formBuilder: FormBuilder,
		private adminAuth: AdminAuthService,
		private router: Router
    ) {
        this.fuseConfig.setConfig({
            layout: {
                navigation: 'none',
                toolbar: 'none',
                footer: 'none'
            }
        });

        this.loginFormErrors = {
            Username: {},
            Password: {}
        };
    }

    ngOnInit() {
        this.loginForm = this.formBuilder.group({
            Username: ['', [Validators.required]],
            Password: ['', Validators.required]
        });

        this.loginForm.valueChanges.subscribe(() => {
            this.onLoginFormValuesChanged();
        });
    }

    onLoginFormValuesChanged() {
        for (const field in this.loginFormErrors) {
            if (!this.loginFormErrors.hasOwnProperty(field)) {
                continue;
            }

            // Clear previous errors
            this.loginFormErrors[field] = {};

            // Get the control
            const control = this.loginForm.get(field);

            if (control && control.dirty && !control.valid) {
                this.loginFormErrors[field] = control.errors;
            }
        }
    }

    doLogin() {
        this.adminAuth.login2(this.loginForm.value)
            .subscribe(
                result => {
                    if (result) {
                        //console.log("Result", result);
                        this.router.navigate(['/admin/home']);
                    }
                },
                error => this.serverErrors = error);
    }
}
