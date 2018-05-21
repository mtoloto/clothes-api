import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material';

@Component({
    selector   : 'fuse-confirm-dialog',
    templateUrl: './confirm-dialog.component.html',
    styleUrls  : ['./confirm-dialog.component.scss']
})
export class FuseConfirmDialogComponent
{
    public confirmTitle: string = "Confirmação";
	public confirmMessage: string = "Tem certeza que deseja continuar?";
	public innerHtml: string;
	public confirmButtonText: string = "Confirmar";
	public cancelButtonText: string = "Cancelar";
    constructor(public dialogRef: MatDialogRef<FuseConfirmDialogComponent>)
    {
    } 
}
