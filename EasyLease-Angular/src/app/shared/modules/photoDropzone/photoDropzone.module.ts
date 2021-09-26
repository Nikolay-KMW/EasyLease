import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PhotoDropzoneComponent} from './components/photoDropzone/photoDropzone.component';
import {NgxDropzoneModule} from 'ngx-dropzone';
import {MatInputModule} from '@angular/material/input';

@NgModule({
  declarations: [PhotoDropzoneComponent],
  imports: [CommonModule, NgxDropzoneModule, MatInputModule],
  exports: [PhotoDropzoneComponent],
  providers: [],
})
export class PhotoDropzoneModule {}
