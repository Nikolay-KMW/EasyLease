import {animate, state, style, transition, trigger} from '@angular/animations';
import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NgxDropzoneChangeEvent} from 'ngx-dropzone';

import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {RejectedFille} from '../../types/rejectedFille.interface';

const timeShowErrorPhoto: number = 10000;
const timeDisappearanceErrorPhoto: number = 1000;

@Component({
  selector: 'el-photo-dropzone',
  templateUrl: './photoDropzone.component.html',
  styleUrls: ['./photoDropzone.component.scss'],
  animations: [
    trigger('errorPhotoAnimate', [
      state(
        'true',
        style({
          opacity: 0,
        })
      ),
      state(
        'false',
        style({
          opacity: 1,
        })
      ),
      //transition('true => false', animate(`0ms ease-in`)),
      transition('false => true', animate(`${timeDisappearanceErrorPhoto}ms ease-out`)),
    ]),
  ],
})
export class PhotoDropzoneComponent implements OnInit {
  @Input('initialValues') initialValuesProps: File[] | null = null;
  @Input('backendErrors') backendErrorsProps: BackendErrorInterface | null = null;
  @Input('namePropertyBackendError') namePropertyBackendErrorProps: string | null = null;
  @Input('expandVertically') expandVerticallyProps: boolean = false;
  @Input('maxFileSize') maxFileSizeProps: number = 1048576;
  @Input('maxNumberPhoto') maxNumberPhotoProps: number = 2;
  @Input('AllowMultipleFiles') allowMultipleFilesProps: boolean = true;
  @Input('allowedExtensions') allowedExtensionsProps: string[] = ['.jpg', '.jpeg'];

  @Output('dropzoneChange') dropzoneChangeEvent = new EventEmitter<File[]>();

  photos: File[] = [];
  rejectedPhotos: RejectedFille[] = [];
  photoLimitExceeded: boolean = false;
  errorPhotoAnimate: boolean = false;
  private errorPhotoTimeoutId: NodeJS.Timeout | null = null;

  acceptExtensions: string = this.initializeAcceptExtensions();

  constructor() {}

  private initializeAcceptExtensions(): string {
    return this.allowedExtensionsProps
      .map((ext) => ext.substring(1))
      .map((ext) => (ext = `image/${ext}`))
      .toString();
  }

  addPhoto(event: NgxDropzoneChangeEvent): void {
    //console.log(event);

    this.clearPhotoError();

    for (const file of event.addedFiles) {
      if (this.photos.length < this.maxNumberPhotoProps) {
        this.photos.push(file);
        this.dropzoneChangeEvent.emit(Object.assign([], this.photos));
      } else {
        this.photoLimitExceeded = true;
        break;
      }
    }

    this.rejectedPhotos = event.rejectedFiles as Array<RejectedFille>;
  }

  private clearPhotoError(): void {
    if (this.errorPhotoTimeoutId) {
      this.rejectedPhotos = [];
      this.backendErrorsProps = null;
      this.photoLimitExceeded = false;
      this.errorPhotoAnimate = false;
      clearTimeout(this.errorPhotoTimeoutId);
    }

    this.errorPhotoTimeoutId = setTimeout(() => {
      this.errorPhotoAnimate = true;
      setTimeout(() => {
        this.rejectedPhotos = [];
        this.backendErrorsProps = null;
        this.photoLimitExceeded = false;
        this.errorPhotoAnimate = false;
      }, timeDisappearanceErrorPhoto);
    }, timeShowErrorPhoto);
  }

  removePhoto(file: File): void {
    //console.log(file);

    this.photos.splice(this.photos.indexOf(file), 1);
    this.dropzoneChangeEvent.emit(Object.assign([], this.photos));
  }

  ngOnInit(): void {
    this.acceptExtensions = this.initializeAcceptExtensions();
    this.photos = this.initialValuesProps ? Object.assign([], this.initialValuesProps) : [];
  }
}
