import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(private _dialog: MatDialog) { }

  public showConfirmationDialog(message: string): MatDialogRef<ConfirmationDialogComponent> {

    return this._dialog.open(ConfirmationDialogComponent, {
      disableClose: true,
      data: { message: message }
    });

  }

}
