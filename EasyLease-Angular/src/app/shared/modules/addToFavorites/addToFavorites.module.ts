import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AddToFavoritesComponent} from './components/addToFavorites/addToFavorites.component';
import {MatButtonModule} from '@angular/material/button';
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {AddToFavoritesService} from './services/addToFavorites.service';
import {EffectsModule} from '@ngrx/effects';
import {AddToFavoritesEffect} from './store/effects/addToFavorite.effect';

@NgModule({
  declarations: [AddToFavoritesComponent],
  imports: [CommonModule, EffectsModule.forFeature([AddToFavoritesEffect]), MatButtonModule, FontAwesomeModule],
  exports: [AddToFavoritesComponent],
  providers: [AddToFavoritesService],
})
export class AddToFavoritesModule {}
