import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    }),
    NgxSpinnerModule.forRoot({ type: 'line-scale-party' })
  ],
  exports:[
    ToastrModule,
    BsDropdownModule,
    TabsModule,
    NgxSpinnerModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class SharedModule { }
