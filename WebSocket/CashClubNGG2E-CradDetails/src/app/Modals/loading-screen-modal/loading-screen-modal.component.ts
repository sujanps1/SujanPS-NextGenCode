import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ModalPopupService } from '../../services/modal-popup.service';
import { Subscription } from 'rxjs';
import { ILoadingScreenModal } from './loading-screen-modal.model';

@Component({
  selector: 'cc-loading-screen-modal',
  templateUrl: './loading-screen-modal.component.html',
  styleUrls: ['./loading-screen-modal.component.css']
})
export class LoadingScreenModalComponent implements OnInit, OnDestroy {
  isDisplay: boolean = false;
  Header: string;
  private modalSub: Subscription;

  headeravailable: boolean;

  @Input() set headervisible(param) {
    this.headeravailable = true;
  }

  constructor(public modalPopupService: ModalPopupService) { }

  ngOnInit(): void {
    this.modalSub = this.modalPopupService.loadingScreenModalSub.subscribe((loadingScreenModal: ILoadingScreenModal) => {
      this.isDisplay = loadingScreenModal.loadingScreenModalStatus;
      this.Header = loadingScreenModal.loadingScreenHeaderContent;
    });
  }

  ngOnDestroy(): void {
    this.modalSub.unsubscribe();
  }
}