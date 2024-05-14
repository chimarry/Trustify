import { Route, Routes } from '@angular/router';
import { LayoutComponent } from './layout.component';

export const routes: Routes = [
  { path: '', component: LayoutComponent },
  { path: 'sample', loadComponent: () => import('../../../main/features/sample-feature/sample-feature.component').then(mod => mod.SampleFeatureComponent) },
  { path: 'admin', loadChildren: () => import('../../../admin/features/admin-feature/admin-feature.routes').then(mod => mod.routes) }
];
