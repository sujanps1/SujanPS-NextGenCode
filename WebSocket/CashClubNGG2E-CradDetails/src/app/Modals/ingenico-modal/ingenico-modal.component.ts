import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { IIngenicoModal } from './ingenico-modal.model';
import { ModalPopupService } from 'src/app/services/modal-popup.service';

@Component({
  selector: 'cc-ingenico-modal',
  templateUrl: './ingenico-modal.component.html',
  styleUrls: ['./ingenico-modal.component.css']
})
export class IngenicoModalComponent implements OnInit, OnDestroy {
  private modalSub: Subscription;
  modalStatus: boolean = false;
  modalName: string;
  imageName: string;

  constructor(private modalPopupService: ModalPopupService) { }

  ngOnInit(): void {
    this.modalSub = this.modalPopupService.ingenicoModalSub.subscribe((ingenicoModal: IIngenicoModal) => {
      this.modalStatus = ingenicoModal.ingenicoModalStatus;
      this.modalName = ingenicoModal.ingenicoModalHeader;
      this.imageName = "./assets/images/ingenico-screens/" + ingenicoModal.ingenicoModalImageName + ".png";
    });
  }

  ngOnDestroy(): void {
    this.modalSub.unsubscribe();
  }
}
