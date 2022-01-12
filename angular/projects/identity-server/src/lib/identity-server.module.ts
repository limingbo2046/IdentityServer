import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { IdentityServerComponent } from './components/identity-server.component';
import { IdentityServerRoutingModule } from './identity-server-routing.module';

@NgModule({
  declarations: [IdentityServerComponent],
  imports: [CoreModule, ThemeSharedModule, IdentityServerRoutingModule],
  exports: [IdentityServerComponent],
})
export class IdentityServerModule {
  static forChild(): ModuleWithProviders<IdentityServerModule> {
    return {
      ngModule: IdentityServerModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<IdentityServerModule> {
    return new LazyModuleFactory(IdentityServerModule.forChild());
  }
}
