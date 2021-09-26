import {animate, state, style, transition, trigger} from '@angular/animations';
import {BreakpointObserver, Breakpoints, BreakpointState} from '@angular/cdk/layout';
import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NgxDropzoneChangeEvent} from 'ngx-dropzone';
import {Observable} from 'rxjs';
import {map, shareReplay} from 'rxjs/operators';

import {BackendErrorInterface} from 'src/app/shared/types/backendError.interface';
import {environment} from 'src/environments/environment';
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
  @Input('isSubmitting') isSubmittingProps: boolean = false;
  @Input('errors') errorsProps: BackendErrorInterface | null = null;

  @Output('dropzoneChange') dropzoneChangeEvent = new EventEmitter<File[]>();

  photos: File[] = [];
  rejectedPhotos: RejectedFille[] = [];
  fileSizeLimit: number = environment.fileSizeLimit;
  photoLimit: number = environment.numberOfFilesLimit;
  photoLimitExceeded: boolean = false;
  errorPhotoAnimate: boolean = false;
  private errorPhotoTimeoutId: NodeJS.Timeout | null = null;

  allowedExtensions: string[] = environment.allowedExtensions;
  acceptExtensions: string = environment.allowedExtensions
    .map((ext) => ext.substring(1))
    .map((ext) => (ext = `image/${ext}`))
    .toString();

  isHandset$: Observable<boolean>;

  constructor(private breakpointObserver: BreakpointObserver) {
    // Initialize values
    this.isHandset$ = this.breakpointObserver.observe([Breakpoints.Handset, Breakpoints.TabletPortrait]).pipe(
      map((result: BreakpointState) => result.matches),
      shareReplay()
    );
  }

  addPhoto(event: NgxDropzoneChangeEvent): void {
    //console.log(event);

    this.clearPhotoError();

    for (const file of event.addedFiles) {
      if (this.photos.length < this.photoLimit) {
        this.photos.push(file);
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
      this.photoLimitExceeded = false;
      this.errorPhotoAnimate = false;
      clearTimeout(this.errorPhotoTimeoutId);
    }

    this.errorPhotoTimeoutId = setTimeout(() => {
      this.errorPhotoAnimate = true;
      setTimeout(() => {
        this.rejectedPhotos = [];
        this.photoLimitExceeded = false;
        this.errorPhotoAnimate = false;
      }, timeDisappearanceErrorPhoto);
    }, timeShowErrorPhoto);
  }

  removePhoto(file: File): void {
    //console.log(file);
    this.photos.splice(this.photos.indexOf(file), 1);
  }

  ngOnInit(): void {}
}
